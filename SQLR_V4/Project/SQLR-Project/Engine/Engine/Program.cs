using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.Threading;
using System.Data.SqlClient; 

namespace Engine
{
    class Program
    {
        /*************************** MAIN ******************************/

        static void Main(string[] args)
        {


            Tree alberonostro;

            try
            {


                if (args.Length == 0)
                {
                    Console.WriteLine("- ENGINE modalità manuale attivata -" + Environment.NewLine);

                    Console.WriteLine("Inserire nome Database: ");
                    string DB = Console.ReadLine();

                    //Settaggio connessione a database  
                    SqlConnection myconn = new SqlConnection(string.Format("Persist Security Info=False;Integrated Security=true;Initial Catalog={0};server=(local)", DB)); 

                    SqlCommand cmd = null; //comando di select 
                    SqlDataReader myReader = null;  

                    //Tentativo di connessione al database
                    myconn.Open();
                    //connessione stabilita 

                    //numero alberi in DB 
                    int numeroAlberi = 0; 

                    cmd = new SqlCommand("SELECT Nome FROM Albero", myconn); //COMANDO SQL 

                    //settaggio un datareader sul comando appena eseguito             
                    myReader = cmd.ExecuteReader();

                    //Lettura dati dal database 
                    while (myReader.Read())  
                    {
                        numeroAlberi++; 
                    }
 
                    // Chiusura DataReader		
                    myReader.Close();

                    
                    //stampa alberi in DB 
                    cmd = new SqlCommand("SELECT Nome FROM Albero", myconn); //COMANDO SQL 

                    //settaggio datareader sul comando appena eseguito             
                    myReader = cmd.ExecuteReader();

                    int[] index = new int[numeroAlberi];
                    string[] trees = new string[numeroAlberi]; 
                     

                    int count = 0; 
                    //Lettura dati dal database 
                    while (myReader.Read()) 
                    {
                        index[count] = count;
                        trees[count] = myReader.GetString(0); 

                        Console.WriteLine((count++) + "-) " + myReader.GetString(0)); 
                    }

 
                    //Chiusura DataReader		
                    myReader.Close();


                    Console.WriteLine(Environment.NewLine + "Inserire index albero: ");
                    int scelta = int.Parse(Console.ReadLine());

                    string nalbero = trees[scelta]; 

                    Console.WriteLine("Albero '{0}' selezionato", nalbero);

                    int numVertex = 0; //contatore per il numero di vertici 

                    SqlConnection conn = new SqlConnection(string.Format("Persist Security Info=False;Integrated Security=true;Initial Catalog={0};server=(local)", DB)); 
                    string s = string.Format("SELECT * FROM Albero, Vertex WHERE Albero.Nome='{0}' AND Vertex.IdAlbero=Albero.Id ", nalbero);
                    conn.Open();
                    SqlCommand msc = new SqlCommand(s, conn);
                    SqlDataReader msdr = msc.ExecuteReader();

                    while (msdr.Read())
                    {
                        numVertex++; //contatore numero vertici 
                    }
                    msdr.Close();
                    conn.Close();


                    Console.WriteLine(Environment.NewLine + "Inserire START Vertex: " + Environment.NewLine + "Default: 0");
                    int start = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Inserire END Vertex: " + Environment.NewLine + "Default END: {0}", (numVertex-1));
                    int end = Int32.Parse(Console.ReadLine());

                    //Controllo RANGE

                    if(!(start >= 0 && start< end && start < numVertex))
                    {
                        //Range startVertex sbagliato! 
                        Console.WriteLine("Sono stati inseriti parametri non corretti");
                        System.Threading.Thread.Sleep(5000);
                        return; 
                    }

                    if (!(end > 0 && end > start && end < numVertex))
                    {
                        //Range endVertex sbagliato! 
                        Console.WriteLine("Sono stati inseriti parametri non corretti");
                        System.Threading.Thread.Sleep(5000);
                        return; 
                    }

                    //Verifica andata a buon fine, dati OK

                    Console.WriteLine(Environment.NewLine + "Parametri Inseriti dall'utente: " + Environment.NewLine + "Database scelto: " + DB + Environment.NewLine + "Nome albero: " + nalbero + Environment.NewLine + "Vertice START: " + start + Environment.NewLine + "Vertice END: " + end);

                    //creazione istanza albero tramite riceviDB() 
                    alberonostro = riceviDB(nalbero, DB);


                    string nStart = alberonostro.albero[start].getNome();
                    string nEnd = alberonostro.albero[end].getNome();

                    long[] res = alberonostro.EsecuzioneCalcolo(nStart, nEnd);

                    long resVertex = res[1];
                    long resEdge = res[0];

                    Console.WriteLine(Environment.NewLine + "La somma sui Vertex è: {0}" + Environment.NewLine + "la somma sugli Edge è: {1}", resVertex, resEdge);

                    Console.WriteLine(Environment.NewLine + "Premere [INVIO] per uscire"); 
                    Console.ReadLine();
                    return;
                }

                if (!(args[0].Equals("GUI")))
                {
                    Console.WriteLine("ERRORE GENERALE: CHIAMATA ENGINE CON PARAMETRO NON ESATTO", args[0]);
                    System.Threading.Thread.Sleep(5000);
                    return;
                }

                if (args[0].Equals("GUI")) Console.WriteLine("RILEVATA CHIAMATA DA GUI. Modalità automatica attivata");
                //Se chiamato da GUI continuo altrimenti no


                alberonostro = new Tree();             //albero sul quale eseguire il calcolo


                //Creazione pipe che si aspetta dati dalla GUI
                NamedPipeServerStream Pipe1 = new NamedPipeServerStream("Pipe1");

                Console.WriteLine("Attendendo connessione con GUI...");
                //aspetto che la GUI si connetta alla pipe
                Pipe1.WaitForConnection();

                Console.WriteLine("Connessione con la GUI stabilita");

                //ricezione messaggio 
                messaggio ricevuto = new messaggio(); //Classe che conterrà Albero, vertice START e vertice END e il futuro risultato 
                var f = new System.Xml.Serialization.XmlSerializer(typeof(messaggio));
                ricevuto = (messaggio)f.Deserialize(Pipe1); 


                //chiusura pipe ricezione dalla GUI
                Pipe1.Close();


                alberonostro = ricevuto.Albero;

                string strv = alberonostro.albero[ricevuto.startVertex].getNome();
                string endv = alberonostro.albero[ricevuto.endVertex].getNome();

                //calcoli su Albero 

                long[] risultati = alberonostro.EsecuzioneCalcolo(strv, endv);
                //l'array risultati conterrà la somma su attr1Int e attr2Int, prendendo il relativo nome (con la funzione getNome()) dalla posizione nell'array di Vertex identificato dall'indice (startVertex o endVertex)

                string msg = string.Format("La somma sui Vertici è: {0}" + Environment.NewLine + "La somma sugli Edge è: {1}", risultati[1], risultati[0]); //risultato (Conteggio su Vertici, conteggio su Archi) da mandare


                //invio messaggio tramite pipe                
                NamedPipeClientStream Pipe2 = new NamedPipeClientStream("Pipe2"); //creazione pipe che invia dati alla GUI
                Thread.Sleep(1000);
                Pipe2.Connect();


                //creazione classe messaggio che conterrà il risultato da mandare 
                messaggio invio = new messaggio();
                invio.Risposta = msg;
                f.Serialize(Pipe2, invio); //invio dati alla GUI


                //chiusura pipe invio e uscita
                Pipe2.Close();

            }
            catch (Exception ex)
            {
                //Eccezioni

                if (args.Length == 0) //stampa eccezione a video
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    System.Threading.Thread.Sleep(5000);
                    return; 
                }

                //se Engine chiamato dalla GUI invia l'eccezione ad essa 
                var f = new System.Xml.Serialization.XmlSerializer(typeof(messaggio));


                string msg = "ERROR IN ENGINE: " + ex.Message;
                //invio messaggio errore tramite pipe  
                NamedPipeClientStream Pipe2 = new NamedPipeClientStream("Pipe2"); //creazione pipe che invia dati alla GUI
                Thread.Sleep(1000);
                Pipe2.Connect();


                //creazione casse messaggio che conterrà il risultato da mandare 
                messaggio invio = new messaggio();
                invio.Risposta = msg;
                f.Serialize(Pipe2, invio); //invio i dati alla GUI


                //chiusura pipe invio e uscita
                Pipe2.Close();


            }

        }

            public static Tree riceviDB(string scelta, string DB)
        {
            Tree albero = new Tree(); //albero vuoto da riempire 


            //Settaggio connessione a database per ripescaggio dati 
            SqlConnection myconn = new SqlConnection(string.Format("Persist Security Info=False;Integrated Security=true;Initial Catalog={0};server=(local)", DB));  

            SqlCommand cmd = null;  
            SqlDataReader myReader = null; 

            //Tentativo di connessione al database
            myconn.Open();
            //connessione stabilita 


            //
            //ripescaggio nome_albero e tipo 
            //


            cmd = new SqlCommand(string.Format("SELECT * FROM Albero WHERE Nome='{0}'", scelta), myconn); //COMANDO SQL 

            //settaggio un datareader sul comando appena eseguito             
            myReader = cmd.ExecuteReader();

            //Lettura dati dal database 
            if (myReader.Read()) //una sola volta 
            {
                albero.name = myReader.GetString(1);
                albero.type = myReader.GetString(2);
                albero.splitSize = myReader.GetInt32(3);
                albero.depth = myReader.GetInt32(4);

            }

 
            // Chiude il DataReader		
            myReader.Close();


            //
            //lettura di numNodi  
            //

            long numVertex = 0;
            cmd = new SqlCommand(string.Format("SELECT * FROM Albero, Vertex WHERE Albero.Nome='{0}' AND Vertex.IdAlbero=Albero.Id ", scelta), myconn); //COMANDO SQL 
          
            myReader = cmd.ExecuteReader();

            //Lettura dati dal database per scoprire lunghezza albero 
            while (myReader.Read())
            {
                numVertex++; //se leggo righe significa che ho vertici 
            }

            //ho scoperto il numero di vertici
            //creo l'albero di lunghezza giusta 
            albero.albero = new Vertex[numVertex];
            albero.numNodi = numVertex;

            // Chiude il DataReader		
            myReader.Close();


            //
            //lettura dei vertex 
            //


            cmd = new SqlCommand(string.Format("SELECT Vertex.Name FROM Albero, Vertex WHERE Albero.Nome='{0}' AND Vertex.IdAlbero=Albero.Id ", scelta), myconn); //COMANDO SQL 

          
            myReader = cmd.ExecuteReader();

            int count = 0;
            //Lettura dati dal database 
            while (myReader.Read())
            {
                albero.addVertex(new Vertex(count, count), count);
                albero.albero[count].nome = myReader.GetString(0);
                count++;
            }



            // Chiude il DataReader		
            myReader.Close();

            //
            //lettura degli Edge 
            //

            cmd = new SqlCommand(string.Format("SELECT Edge.Valore FROM Albero, Edge WHERE Albero.Nome='{0}' AND Edge.IdAlbero=Albero.Id ", scelta), myconn); //COMANDO SQL 

          
            myReader = cmd.ExecuteReader();

            count = 0;
            //Lettura dati dal database 
            while (myReader.Read())
            {

                albero.albero[count].arcoentrante.val = myReader.GetInt32(0);
                count++;
            }


            // Chiude il DataReader		
            myReader.Close();


            //
            //Riempimento attributi di Vertex 
            //

            cmd = new SqlCommand(string.Format("SELECT AttrDef.Name, Value FROM AttrDef, VertexAttrUsage, Vertex WHERE AttrDef.NomeAlbero='{0}' AND AttrDef.AttrDefUid=VertexAttrUsage.AttrDefUid AND Vertex.VertexUid=VertexAttrUsage.ObjectVUid", scelta), myconn); //COMANDO SQL 

         
            myReader = cmd.ExecuteReader();

            //Qui si prendono i dati giusti dal DB 
            count = 0;
            long contatore = numVertex;
            //Lettura dati dal database 
            while (myReader.Read())
            {

                if ((myReader.GetString(0)).Equals("B"))
                {
                    if (count == albero.albero.Length) count--; //corregge l'errore di importazione dell'ultima riga dal DB, che genera l'eccezione IndexOutOfBound nell'array di Vertex 
                    albero.albero[count].B = myReader.GetString(1);
                }

                if ((myReader.GetString(0)).Equals("C"))
                {
                    if (count == albero.albero.Length) count--;
                    albero.albero[count].C = myReader.GetString(1);
                }

                if ((myReader.GetString(0)).Equals("D"))
                {
                    if (count == albero.albero.Length) count--;
                    albero.albero[count].D = myReader.GetString(1);
                }

                if ((myReader.GetString(0)).Equals("E"))
                {
                    if (count == albero.albero.Length) count--;
                    albero.albero[count].E = myReader.GetString(1);
                }


                if ((myReader.GetString(0)).Equals("attr1Int"))
                {
                    albero.albero[count].attr1Int = long.Parse(myReader.GetString(1));

                    contatore--;
                    count++;
                }

            }

            //Lettura completata 

            // Chiude il DataReader		
            myReader.Close();



            //
            //Riempimento attributi di Edge 
            //

            cmd = new SqlCommand(string.Format("SELECT AttrDef.Name, Value FROM AttrDef, EdgeAttrUsage, Edge WHERE AttrDef.NomeAlbero='{0}' AND AttrDef.AttrDefUid=EdgeAttrUsage.AttrDefUid AND Edge.EdgeUid=EdgeAttrUsage.ObjectEUid", scelta), myconn); //COMANDO SQL 
            
            myReader = cmd.ExecuteReader();

            count = 0;

            contatore = numVertex;
            //Lettura dati dal database 
            while (myReader.Read())
            {


                if ((myReader.GetString(0)).Equals("G"))
                {
                    if (count == albero.albero.Length) count--;
                    albero.albero[count].arcoentrante.G = myReader.GetString(1);
                }

                if ((myReader.GetString(0)).Equals("H"))
                {
                    if (count == albero.albero.Length) count--;
                    albero.albero[count].arcoentrante.H = myReader.GetString(1);
                }

                if ((myReader.GetString(0)).Equals("I"))
                {
                    if (count == albero.albero.Length) count--;
                    albero.albero[count].arcoentrante.I = myReader.GetString(1);
                }

                if ((myReader.GetString(0)).Equals("L"))
                {
                    if (count == albero.albero.Length) count--;
                    albero.albero[count].arcoentrante.L = myReader.GetString(1);
                }

                if ((myReader.GetString(0)).Equals("attr2Int"))
                {
                    albero.albero[count].arcoentrante.attr2Int = long.Parse(myReader.GetString(1));

                    count++;
                    contatore--;
                }


            }

            //Lettura completata 

            // Chiude il DataReader		
            myReader.Close();




            //L'albero è stato ricreato

            // Chiusura connessione database 
            myconn.Close();


            //ritorno albero ricreato
            return albero;

        }


    }
    }




    //Classe messaggio, conterrà l'albero, il vertice START, il vertice END e il futuro risultato 
    public class messaggio
    {
        public Tree Albero;
        public long startVertex;
        public long endVertex;
        public string Risposta;

        public messaggio()
        {
            this.Albero = null;
            this.startVertex = 0;
            this.endVertex = 0;
            this.Risposta = "";

        }

    }


    /*************************** CLASSE TREE ******************************/

    public class Tree
    {
        public string name;                                //nome albero
        public string type;                               //tipo albero
        public long splitSize;                           //altezza albero
        public long depth;                              //archi uscenti dal vertice
        public Vertex[] albero = new Vertex[0];        //lista degli attributi dell'albero
        public long numNodi;                          //variabile d'appoggio di valore 'numero nodi totale'

        //aggiunta 
        public int[] chiamato = new int[8];       //i numeri qui riferiscono l'attributo checked dell'array attr tramite posizione 

        public Tree()                          //costruttore di Tree vuoto inserito per la serializzazione 
        {

        }


        public Tree(string name, string type, long splitSize, long depth)  //costruttore
        {
            this.name = name;
            this.type = type;
            this.splitSize = splitSize;
            this.depth = depth;


            long elevatoapotenza = (long)(System.Math.Pow(depth, splitSize));
            long moltiplicazione = depth * elevatoapotenza;
            long passaggio = moltiplicazione - 1;                  //calcolo num nodi tramite la formula:
            long depthnuovo = depth - 1;                          //[(depth * (depth^splitSize)) - 1] / (depth - 1)
            long numeroNodiTotali = passaggio / depthnuovo;

            this.numNodi = numeroNodiTotali;

            albero = new Vertex[numNodi];                  //assegna all'array 'albero' una lunghezza pari al numero totale dei vertici
        }

        public void creaAlbero()
        {
            double k = this.getSize();                                 //variabile di appoggio richiamante il numNodi tramite 'getSize()'
            System.Random random = new System.Random();               //variabile che assegna numeri random

            for (long c = 0; c < k; c++)
            {
                long num = random.Next(1, 99);                    //valore random da assegnare ai vertici
                long num2 = random.Next(1, 55);                  //valore random da assegnare agli archi

                //Console.WriteLine("FOR");

                this.albero[c] = new Vertex(c, num);

                //aggiunta 
                this.albero[c].chiamato = chiamato;
                this.albero[c].riempioAttr();

                if (c == 0)
                {
                    //Console.WriteLine("IF");
                    this.albero[c].setPesoArco(0);              //assegna il peso '0' al vertice padre o random in caso non lo sia
                }
                else
                {
                    this.albero[c].setPesoArco(num2);       //assegna il peso 'num2' (random) al vertice non padre
                }


            }
        }


        public long[] EsecuzioneCalcolo(string a, string b)
        {
            long contVer = 0;                                       //contatore vertici
            long contArc = 0;                                      //contatore archi
            long i = 1;

            //Errore, contArc non viene ritornato. Possibile soluzione: ritornare array contenente contVer e contArc 
            long[] result = new long[2];

            if (this.albero[0].getNome() == a)                                     //caso in cui 'a' è vertice root
            {
                long k = this.getSize();                                         //variabile 'k' inizializzata alla grandezza dell'albero
                for (i = 1; this.albero[i].getNome() != b; i++) { }             //caso vertice padre
                // ciclo 'for' utilizzato per trovare l'indice di destinazione

                contVer += this.albero[i].attr1Int;                          //incrementa il risultato con il valore del vertice attuale
                contArc += this.albero[i].arcoentrante.attr2Int;            //incrementa il risultato con il peso dell'arco in considerazione

                // siamo usciti dal 'for' quindi la variabile 'i' conterrà l'indice del vertice di destinazione //

                while (this.albero[i].getNome() != a)                  //sale l'albero finchè trova il vertice di partenza passatogli
                {
                    i = ((i - 1) / (this.depth));                              //risale al padre del vertice attuale 

                    if (i < 0) break;                              //se i < 0 il vertice startVertex è il vertice root

                    contVer += this.albero[i].attr1Int;
                    contArc += this.albero[i].arcoentrante.attr2Int;
                }

                if (i < 0)
                {
                    i = 0;

                    contVer += this.albero[i].attr1Int;
                    contArc += this.albero[i].arcoentrante.attr2Int;
                }
                else
                {

                    contVer += this.albero[i].attr1Int;
                    contArc += this.albero[i].arcoentrante.attr2Int;
                }

                result[0] = contArc;
                result[1] = contVer;

                return result;
                //return contVer;
                //return contArc;
            }
            else
            {
                for (i = 0; this.albero[i].getNome() != b; i++) { }
                //ciclo per trovare vertice di destinazione

                contVer += this.albero[i].attr1Int;                           //incrementa il risultato con il valore del vertice attuale
                contArc += this.albero[i].arcoentrante.attr2Int;             //incrementa il risultato con il peso dell'arco in considerazione

                //siamo usciti dal 'for' quindi la variabile 'i' conterrà l'indice del vertice di destinazione//
                i = ((i - 1) / (this.depth));

                while (this.albero[i].getNome() != a)                   //sale l'albero finchè trova il vertice di partenza
                {
                    //risale al padre del vertice attuale 
                    contVer += this.albero[i].attr1Int;
                    contArc += this.albero[i].arcoentrante.attr2Int;

                    i = ((i - 1) / (this.depth));
                }
                contVer += this.albero[i].attr1Int;
                //contArc += this.albero[i].getPesoArco();

                result[0] = contArc;
                result[1] = contVer;

                return result;
                //return contVer;
                //return contArc;
            }
        }

        public void addVertex(Vertex A, long k)
        {
            albero[k] = A;                                         //aggiunge un vertice 'A' nella posizione 'k' dell'array 'albero'
        }

        public string getNome()
        {
            return this.name;                                //restituisce il nome dell'albero
        }

        public string getType()
        {
            return this.type;                          //restituisce il tipo dell'albero
        }

        public long getSize()
        {
            return this.numNodi;                 //restituisce la grandezza dell'albero in numero nodi totale
        }

        public Vertex getVertex(long i)
        {
            for (long j = 0; j < albero.Length; j++)
            {                                               //scorre l'intero albero alla ricerca del vertice con indice 'i'
                if (i == albero[j].getindice())            //e quando trovato ce lo restituisce in output (con indice valore)
                {
                    return albero[j];
                }
            }
            return null;                             //se non presente ci restituisce il valore 'null' in output
        }

    }




    /*************************** CLASSE VERTEX ******************************/

    public class Vertex
    {
        public string nome;                                //nome
        public long indice;                               //indice
        public long valore;                              //valore
        //da controllare
        public string[] attributi;                     //lista degli attributi dei vertici

        public Edge arcoentrante = new Edge();       //arco entrante


        System.Random random = new System.Random();
        public long attr1Int;
        public string B;
        public string C;                      //Attributi Vertex inseriti da noi
        public string D;
        public string E;

        //aggiunta 
        public long count;
        public int[] chiamato = new int[8];        //i numeri qui riferiscono l'attributo checked dell'array attr tramite posizione 

        public void riempioAttr()
        {
            count = 0;
            while (count < chiamato.Length)
            {

                if ((chiamato[count]) == -1)
                {
                    count++;
                    continue;
                }

                if (chiamato[count] == 0) this.B = randomString();

                if (chiamato[count] == 1) this.C = randomString();

                if (chiamato[count] == 2) this.D = randomString();

                if (chiamato[count] == 3) this.E = randomString();

                if (chiamato[count] == 4) this.arcoentrante.G = randomString();

                if (chiamato[count] == 5) this.arcoentrante.H = randomString();

                if (chiamato[count] == 6) this.arcoentrante.I = randomString();

                if (chiamato[count] == 7) this.arcoentrante.L = randomString();

                count++;
            }
        }






        static long rdm = 0;    //variabile statica d'appoggio usata per creare stringhe numeriche randomiche

        //metodo
        public static string randomString()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var stringChars = new char[15];
            var random = new System.Random();    //randomString() che crea stringhe alfanumeriche randomiche

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length + (int)rdm) % chars.Length];
            }

            //aggiunta
            if (rdm == (long.MaxValue - 1)) rdm = 0;

            string l = string.Format("{0}", (++rdm));
            var finalString = new string(stringChars) + l;   // aggiunta di un numero in più random //

            return finalString;
        }


        //Da qui originale 

        public Vertex()                                                             //costruttore 1 (per la serializzazione)
        {

        }

        public Vertex(long indice, long valore)                                //costruttore 2
        {
            this.indice = indice;
            this.nome = string.Format("nome{0}", this.indice);
            this.valore = valore;

            //aggiunta
            this.attr1Int = ((random.Next(1, 98) * indice) % 98) + 1;
            this.arcoentrante.attr2Int = ((random.Next(1, 54) * indice) % 54) + 1;
        }

        public Vertex(long indice, long valore, Edge arcoentrante)        //costruttore 3
        {
            this.indice = indice;
            this.nome = string.Format("nome{0}", this.indice);
            this.valore = valore;
            this.arcoentrante = arcoentrante;
            //aggiunta
            this.attr1Int = ((random.Next(1, 98) * indice) % 98) + 1; //
            this.arcoentrante.attr2Int = ((random.Next(1, 54) * indice) % 54) + 1;
        }

        public Vertex(string nome, long indice, long valore, string[] attributi, Edge arcoentrante)    //costruttore 4
        {
            this.nome = nome;
            this.indice = indice;
            this.nome = string.Format("nome{0}", this.indice);
            this.valore = valore;
            this.arcoentrante = arcoentrante;
            for (long i = 0; i < attributi.Length; i++)
            {
                this.attributi[i] = attributi[i];                           //riempimento attributelist del vertex
            }
            //aggiunta
            this.attr1Int = ((random.Next(1, 98) * indice) % 98) + 1;
            this.arcoentrante.attr2Int = ((random.Next(1, 54) * indice) % 54) + 1;
        }

        public string getNome()
        {
            return this.nome;                                                //restituisce il nome del Vertex
        }

        public long getVal()
        {
            return this.valore;                                          //restituisce il valore del vertex
        }

        public long getindice()
        {
            return this.indice;                                      //restituisce l'indice del vertex
        }

        public long getPesoArco()
        {
            return (this.arcoentrante).getPeso();                //restituisce il peso dell'arco associato al vertex
        }

        public void setVal(long a)
        {
            this.valore = a;                                //assegna il valore 'a' al vertex
        }

        public void setPesoArco(long b)
        {
            arcoentrante.val = b;                     //assegna il peso 'b' all'arco
        }

        public void setNome(string c)
        {
            this.nome = c;                       //assegna il nome 'c' al vertex
        }
    }


    /*************************** CLASSE EDGE ******************************/

    public class Edge
    {
        public long val;              //valore
        public string[] attr;        //lista degli attributi degli archi

        //Attributi Edge inseriti da noi 
        public long attr2Int;
        public string G;
        public string H;
        public string I;
        public string L;

        public Edge()             // costruttore 1 (per la serializzazione)
        {

        }

        public Edge(long val, string[] attr)                     //costruttore 2
        {
            this.val = val;
            for (long i = 0; i < attr.Length; i++)           //riempimento attributelist dell'arco
            {
                this.attr[i] = attr[i];
            }
        }

        public long getPeso()
        {
            return this.val;                         //restituisce il peso dell'arco
        }

        public void setPeso(long a)
        {
            this.val = a;                        //assegna un valore 'a' corrispondente al peso dell'arco
        }
    }
