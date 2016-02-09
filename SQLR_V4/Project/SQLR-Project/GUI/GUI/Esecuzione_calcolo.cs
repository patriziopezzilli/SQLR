using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
//Pipes
using System.IO;
using System.IO.Pipes;
//Database
using System.Data.SqlClient;
//Serialization
using System.Runtime.Serialization;
//interprocess comunication 
using System.Threading; 

namespace GUI
{
    public partial class Esecuzione_calcolo : Form
    {
        public Esecuzione_calcolo()
        {
            InitializeComponent();
        }

        private void Esecuzione_calcolo_Load(object sender, EventArgs e)
        {
            //Qui riempio comboBox1 di alberi nel DB 
            try
            {

                SqlConnection conn = new SqlConnection("Persist Security Info=False;Integrated Security=true;Initial Catalog=DB;server=(local)"); 
                string s = "SELECT * FROM Albero";
                conn.Open();
                SqlCommand msc = new SqlCommand(s, conn);
                SqlDataReader msdr = msc.ExecuteReader();
                while (msdr.Read())
                {
                    comboBox1.Items.Add(msdr.GetString(1));
                }
                conn.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                this.Dispose(); 
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                //
                //Import dal database 
                //


                //passo alla funzione riceviDB() l'albero scelto dall'utente 
                Tree albero = riceviDB(comboBox1.Text);
                //albero ricreato 


                //
                //utente immette i dati 
                //
                

                //controllo correttezza TextBox Vertex START e Vertex END 
                int startVertex, endVertex; 

                if(((this.textBox1.Text).Equals("")) || ((this.textBox2.Text).Equals("")))
                {
                    MessageBox.Show("Campi mancanti", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!(int.TryParse(this.textBox1.Text, out startVertex)))
                {
                    MessageBox.Show("Sono stati inseriti parametri non corretti", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!(int.TryParse(this.textBox2.Text, out endVertex)))
                {
                    MessageBox.Show("Sono stati inseriti parametri non corretti", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Controllo RANGE 

                if(!(startVertex >= 0 && startVertex < endVertex && startVertex < albero.numNodi))
                {
                    //Range startVertex sbagliato! 
                    MessageBox.Show("Sono stati inseriti parametri non corretti", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(!(endVertex > 0 && endVertex > startVertex && endVertex <= albero.numNodi))
                {
                    //Range endVertex sbagliato! 
                    MessageBox.Show("Sono stati inseriti parametri non corretti", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Verifica andata a buon fine, dati OK


                //scelta utente: Eseguire il calcolo?
                var result = MessageBox.Show("Eseguire il calcolo?", "GUI", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);


                //scelta negativa da parte dell'utente 
                if (result == System.Windows.Forms.DialogResult.No) { return; }


                //
                //esecuzione GUI-Engine
                //

                Process myProcess = new Process();
           
                                
                myProcess.StartInfo.UseShellExecute = false;          //Attivazione Engine
                myProcess.StartInfo.FileName = "Engine.exe";         //Percorso file da avviare 
                myProcess.StartInfo.Arguments = "GUI";              //Argomento "GUI" da passare all'Engine per far capire ad esso che è stato chiamato dalla nostra GUI
                myProcess.StartInfo.CreateNoWindow = true;         //false = finestra visibile, true il contrario

                myProcess.Start(); //Processo Engine attivato 


                //
                //comunicazione con Engine.exe 
                //


                //Comunicazione con Engine.exe tramite Named Pipes 
                NamedPipeClientStream Pipe1 = new NamedPipeClientStream("Pipe1");
                Thread.Sleep(1000);         //aspetta 1 secondo per far si che l'Engine sia pronto a ricevere la connessione dalla GUI
                Pipe1.Connect();           //stabilisce connessione con Engine 
                

                //riempimento struttura messaggio 
                messaggio message = new messaggio();
                message.Albero = albero;
                message.startVertex = int.Parse(this.textBox1.Text);
                message.endVertex = int.Parse(this.textBox2.Text); 

                message.Risposta = "";

                //Invio messaggio a Engine
                var f = new System.Xml.Serialization.XmlSerializer(typeof(messaggio));
                f.Serialize(Pipe1, message); //mando il messaggio sulla pipe  

                //chiusura pipe invio 
                Pipe1.Close();

                //Qui l'Engine esegue il calcolo e ci spedisce la risposta tramite Pipe2

                //ricezione messaggio creando pipe di ricezione
                NamedPipeServerStream Pipe2 = new NamedPipeServerStream("Pipe2");
                Pipe2.WaitForConnection();


                //Ricezione messaggio da Engine
                messaggio ricevuto = new messaggio();

                ricevuto = (messaggio)f.Deserialize(Pipe2);

                //chiusura pipe ricezione
                Pipe2.Close();

                //compito riuscito

                //Stampa risultato 
                MessageBox.Show("RESULT: " + ricevuto.Risposta, "GUI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //chiusura Form esecuzione calcolo 
                this.Dispose(); 

            }
            catch (Exception ex)
            {   
                //accade eccezione, fallimento compito
                if (ex is System.ComponentModel.Win32Exception) MessageBox.Show("ERRORE: " + "File 'Engine.exe' non trovato: assicurati che il file si trovi nella stessa cartella della GUI e si chiami 'Engine.exe' "); //sollevata eccezione per Engine mancante 
                else 
                MessageBox.Show("ERRORE: " + ex.Message); //Tutte le altre possibili eccezioni 

                //Notifica calcolo non eseguito
                MessageBox.Show("Calcolo non eseguito", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numVertex = 0; //contatore per il numero di vertici 

            //query in DB per l'END Vertex 
            try
            {

                SqlConnection conn = new SqlConnection("Persist Security Info=False;Integrated Security=true;Initial Catalog=DB;server=(local)"); 
                string s = string.Format("SELECT * FROM Albero, Vertex WHERE Albero.Nome='{0}' AND Vertex.IdAlbero=Albero.Id ", comboBox1.Text);
                conn.Open();
                SqlCommand msc = new SqlCommand(s, conn);
                SqlDataReader msdr = msc.ExecuteReader();

                while (msdr.Read())
                {
                    numVertex++; //contatore numero vertici 
                }
                msdr.Close(); 
                conn.Close();

                //setto le textbox Vertex START e Vertex END 
                textBox1.Text = string.Format("{0}", 0); //Vertex START
                textBox2.Text = string.Format("{0}", (numVertex-1)); //Vertex END 

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        public Tree riceviDB(string scelta)
        {
            Tree albero = new Tree(); //albero vuoto da riempire 


            //Setto connessione a database per ripescaggio dati 
            SqlConnection myconn = new SqlConnection("Persist Security Info=False;Integrated Security=true;Initial Catalog=DB;server=(local)"); 

            SqlCommand cmd = null; //il comand o di select 
            SqlDataReader myReader = null; //il lettore 

            //Tentativo di connessione al database
            myconn.Open(); 
            //connessione stabilita 


            //
            //ripesco nomealbero, tipo, split size, depth  
            //


            cmd = new SqlCommand(string.Format("SELECT * FROM Albero WHERE Nome='{0}'", scelta), myconn); //COMANDO SQL 
                             
            //setto un datareader sul comando appena eseguito             
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

            //setto un datareader sul comando appena eseguito             
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

            //setto un datareader sul comando appena eseguito             
            myReader = cmd.ExecuteReader();

            int count = 0; 
            //Lettura dati dal database 
            while(myReader.Read()) 
            {
                albero.addVertex(new Vertex(count,count),count); 
                albero.albero[count].nome = myReader.GetString(0);
                count++; 
            }

            //Lettura dei vertex completata 

            // Chiude il DataReader		
            myReader.Close();

            //
            //lettura degli Edge 
            //

            cmd = new SqlCommand(string.Format("SELECT Edge.Valore FROM Albero, Edge WHERE Albero.Nome='{0}' AND Edge.IdAlbero=Albero.Id ", scelta), myconn); //COMANDO SQL 

            //setto un datareader sul comando appena eseguito             
            myReader = cmd.ExecuteReader();

            count = 0;
            //Lettura dati dal database 
            while (myReader.Read())
            {

                albero.albero[count].arcoentrante.val = myReader.GetInt32(0);
                count++; 
            }

            //Lettura degli Edge completata 

            // Chiude il DataReader		
            myReader.Close();


            //
            //Riempio attributi di Vertex 
            //

            cmd = new SqlCommand(string.Format("SELECT AttrDef.Name, Value FROM AttrDef, VertexAttrUsage, Vertex WHERE AttrDef.NomeAlbero='{0}' AND AttrDef.AttrDefUid=VertexAttrUsage.AttrDefUid AND Vertex.VertexUid=VertexAttrUsage.ObjectVUid", scelta), myconn); //COMANDO SQL 

            //setto un datareader sul comando appena eseguito             
            myReader = cmd.ExecuteReader();

            //Qui si prendono i dati giusti dal DB 
            count = 0;
            long contatore = numVertex; 
            //Lettura dati dal database 
            while (myReader.Read()) 
            {
               
                if ((myReader.GetString(0)).Equals("B"))
                {
                    if (count == albero.albero.Length) count--; //Serve a correggere l'errore che succede all'importazione dell'ultima riga dal DB, che fa finire OutOfBound l'array di Vertex 
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
            //Riempio attributi di Edge 
            //

            cmd = new SqlCommand(string.Format("SELECT AttrDef.Name, Value FROM AttrDef, EdgeAttrUsage, Edge WHERE AttrDef.NomeAlbero='{0}' AND AttrDef.AttrDefUid=EdgeAttrUsage.AttrDefUid AND Edge.EdgeUid=EdgeAttrUsage.ObjectEUid", scelta), myconn); //COMANDO SQL 

            //setto un datareader sul comando appena eseguito             
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
