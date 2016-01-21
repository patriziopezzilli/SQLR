using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class Program
    {
    /*************************** MAIN ******************************/

        static void Main(string[] args)
        {
            Tree alberonostro = new Tree("Maestro", "A", 2, 3);             //albero esempio
            alberonostro.creaAlbero();  
            
          }
    }
    


    /*************************** CLASSE TREE ******************************/

    public class Tree
    {
        public String name;                               //nome albero
        public String type;                              //tipo albero
        public double splitSize;                        //altezza albero
        public double depth;                           //archi uscenti dal vertice
        public Vertex[] albero = new Vertex[0];       //lista degli attributi dell'albero
        public double numNodi;                       //variabile d'appoggio di valore 'numero nodi totale'

       
        public Tree(String name, String type, double splitSize, double depth)  //costruttore
        {
            this.name = name;
            this.type = type;
            this.splitSize = splitSize;
            this.depth = depth;
            

            double elevatoapotenza = Math.Pow(depth, splitSize);
            double moltiplicazione = depth * elevatoapotenza;
            double passaggio = moltiplicazione - 1;                  //calcolo num nodi tramite la formula:
            double depthnuovo = depth - 1;                          //[(depth * (depth^splitSize)) - 1] / (depth - 1)
            double numeroNodiTotali = passaggio / depthnuovo;

            this.numNodi = numeroNodiTotali;
                        
            albero = new Vertex[(int)numNodi];                  //assegna all'array 'albero' una lunghezza pari al numero totale dei vertici
        }          

        public void creaAlbero ()
        {
            double k = this.getSize();                             //variabile di appoggio richiamante il numNodi tramite 'getSize()'
            Random random = new Random();                         //variabile che assegna numeri random
                                                                          
            for (int c = 0; c < k; c++)
            {
                double num = random.Next(1, 99);                 //valore random da assegnare ai vertici
                int num2 = random.Next(1, 55);                  //valore random da assegnare agli archi

                //Console.WriteLine("FOR");

                this.albero[c] = new Vertex(c, num);

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

        public int EsecuzioneCalcolo (String a, String b)
        {
            double contVer = 0;                                       //contatore vertici
            double contArc = 0;                                      //contatore archi
            int i = 1;

            if (this.albero[0].getNome() == a)                                    //caso in cui 'a' è vertice root
            {
                double k = this.getSize();                                            //variabile 'k' inizializzata alla grandezza dell'albero
                for (i = 1; this.albero[i].getNome() != b; i++) { }                  //caso vertice padre
                // ciclo 'for' utilizzato per trovare l'indice di destinazione

                contVer += this.albero[i].getVal();                      //incrementa il risultato con il valore del vertice attuale
                contArc += this.albero[i].getPesoArco();                //incrementa il risultato con il peso dell'arco in considerazione
               
                // siamo usciti dal 'for' quindi la variabile 'i' conterrà l'indice del vertice di destinazione //

                while (this.albero[i].getNome() != a)                     //sale l'albero finchè trova il vertice di partenza passatogli
                {
                    i = i - (int)this.depth;                            //risale al padre del vertice attuale 
                    contVer += this.albero[i].getVal();                   
                    contArc += this.albero[i].getPesoArco();               
                }
                contVer += this.albero[i].getVal();
                contArc += this.albero[i].getPesoArco();

                return (int)contVer;
                return (int)contArc;
            }
                else
            {
                for (i = 0; this.albero[i].getNome() != b; i++) { }
                //ciclo per trovare vertice di destinazione

                contVer += this.albero[i].getVal();                      //incrementa il risultato con il valore del vertice attuale
                contArc += this.albero[i].getPesoArco();                //incrementa il risultato con il peso dell'arco in considerazione
                
                //siamo usciti dal 'for' quindi la variabile 'i' conterrà l'indice del vertice di destinazione//
                 
                while (this.albero[i].getNome() != a)                      //sale l'albero finchè trova il vertice di partenza
                {
                    i = i - (int)this.depth;                             //risale al padre del vertice attuale 
                    contVer += this.albero[i].getVal();
                    contArc += this.albero[i].getPesoArco();
                }
                contVer += this.albero[i].getVal();
                contArc += this.albero[i].getPesoArco();

                return (int)contVer;
                return (int)contArc;
            }
        }

        public void addVertex(Vertex A, int k)
        {
            albero[k] = A;                                       //aggiunge un vertice 'A' nella posizione 'k' dell'array 'albero'
        }

        public String getNome()
        {
            return this.name;                                //restituisce il nome dell'albero
        }

        public String getType()
        {
            return this.type;                           //restituisce il tipo dell'albero
        }

        public double getSize()
        {
            return this.numNodi;                    //restituisce la grandezza dell'albero in numero nodi totale
        }

        public Vertex getVertex(int i)
        {
            for(int j = 0; j < albero.Length; j++)
            {                                              //scorre l'intero albero alla ricerca del vertice con indice 'i'
                if(i == albero[j].getindice())            //e quando trovato ce lo restituisce in output (con indice valore)
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
        public String nome;                              //nome
        public int indice;                              //indice
        public double valore;                          //valore
        public String[] attributi;                    //lista degli attributi dei vertici
        public Edge arcoentrante = new Edge();       //arco entrante


        public Vertex()                                                             //costruttore 1
        {

        }

        public Vertex(int indice, double valore)                                //costruttore 2
        {
            this.indice = indice;
            this.valore = valore;
        }

        public Vertex(int indice, double valore, Edge arcoentrante)        //costruttore 3
        {
            this.indice = indice;
            this.valore = valore;
            this.arcoentrante = arcoentrante;


        }

        public Vertex(String nome, int indice, double valore, String[] attributi, Edge arcoentrante)    //costruttore 4
        {
            this.nome = nome;
            this.indice = indice;
            this.valore = valore;
            this.arcoentrante = arcoentrante;
            for (int i = 0; i < attributi.Length; i++)
            {
                this.attributi[i] = attributi[i];                           //riempimento attributelist del vertex
            }

        }

        public String getNome()                                     
        {
            return this.nome;                                                //restituisce il nome del Vertex
        }

        public double getVal()                             
        {
            return this.valore;                                          //restituisce il valore del vertex
        }

        public int getindice()                         
        {
            return this.indice;                                      //restituisce l'indice del vertex
        }

        public int getPesoArco()                    
        {
            return (this.arcoentrante).getPeso();                //restituisce il peso dell'arco associato al vertex
        }

        public void setVal(int a)               
        {
            this.valore = a;                                //assegna il valore 'a' al vertex
        }

        public void setPesoArco(int b)      
        {
            arcoentrante.val = b;                     //assegna il peso 'b' all'arco
        }

        public void setNome(String c)     
        { 
            this.nome = c;                       //assegna il nome 'c' al vertex
        }
    }


    /*************************** CLASSE EDGE ******************************/

    public class Edge
    {
        public int val;              //valore
        public String[] attr;       //lista degli attributi degli archi

        public Edge()             // costruttore 1
        {

        }

        public Edge(int val, String[] attr)      //costruttore 2
        {
            this.val = val;
            for (int i = 0; i < attr.Length; i++)           //riempimento attributelist dell'arco
            {
                this.attr[i] = attr[i];
            }
        }

        public int getPeso()                              
        {
            return this.val;                         //restituisce il peso dell'arco
        }

        public void setPeso(int a)                    
        {
            this.val = a;                        //assegna un valore 'a' corrispondente al peso dell'arco
        }
    }
}