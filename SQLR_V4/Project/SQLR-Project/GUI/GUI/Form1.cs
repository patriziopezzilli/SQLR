using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Serializzazione
using System.Xml.Serialization;
//SQL
using System.Data.SqlClient;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Creazione_albero a = new Creazione_albero();
            a.ShowDialog(); 
        }

        private void button2_Click(object sender, EventArgs e) //pulsante Update su Database 
        {
            Tree classe = null; //classe sarà l'istanza albero da ricreare dal file, ora è a null  

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {  
                //Deserializzazione
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Tree)); 
                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);

                classe = (Tree)reader.Deserialize(file);
                file.Close();

            }

            if (classe == null) return; //non mi connetto al database se non ho importato nessun file .xml 

            //Update su Database a partire da albero deserializzato   
            updateDB(classe); 

            //Compito riuscito            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Esecuzione_calcolo b = new Esecuzione_calcolo();
            b.ShowDialog(); 
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        //
        //
        //
        //FUNZIONE IMPORT DATABASE
        //
        //
        //


        public void updateDB(Tree albero)
        { 
            try
            {
                
                string comando = null;

                //Apertura connessione con Database "DB"
                SqlConnection myconn = new SqlConnection("Persist Security Info=False;Integrated Security=true;Initial Catalog=DB;server=(local)"); 

                //
                //inseriamo nome albero tipo, splitsize e depth
                //

                comando = string.Format("INSERT INTO Albero(Nome, Tipo, Split, Depth) VALUES('{0}','{1}', '{2}','{3}')", albero.name, albero.type, albero.splitSize, albero.depth); 

                //settiamo connessione
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = comando;
                cmd.Connection = myconn;

                //connessione
                myconn.Open();            
                //Query
                cmd.ExecuteNonQuery();
                //query su albero completata 


                //
                //occupiamoci degli attributi obbligatori di Vertex ed Edge 
                //

                comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "attr1Int", albero.getNome());
                //settiamo connessione
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = comando;
                cmd.Connection = myconn;

                cmd.ExecuteNonQuery();
                //query completata 

                comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "attr2Int", albero.getNome());
                //settiamo connessione
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = comando;
                cmd.Connection = myconn;

                cmd.ExecuteNonQuery();
                //query completata 


            //
            //Riempimento restanti attributi se e solo se sono stati scelti dall'utente 
            //


            for (int i = 0; i < albero.chiamato.Length; i++)
            {


                //
                //occupiamoci degli attributi di Vertex 
                //

                if (albero.chiamato[i] == 0) //controllo se attributo è stato chiamato da utente 
                {

                    comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "B", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query completata 
                }

                if (albero.chiamato[i] == 1)
                {
                    comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "C", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query completata 
                }

                if (albero.chiamato[i] == 2)
                {

                    comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "D", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query completata 
                }

                if (albero.chiamato[i] == 3)
                {

                    comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "E", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();

                }


                //
                //occupiamoci degli attributi di Edge 
                //



                if (albero.chiamato[i] == 4)
                {

                    comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "G", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query completata 

                }

                if (albero.chiamato[i] == 5)
                {
                    comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "H", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query completata 

                }

                if (albero.chiamato[i] == 6)
                {
                    comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "I", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query completata 

                }

                if (albero.chiamato[i] == 7)
                {

                    comando = string.Format("INSERT INTO AttrDef(Name, NomeAlbero) VALUES('{0}', '{1}')", "L", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query completata 
                }
            }



            //
            //occupiamoci degli AttrDefUid 
            //

            //saranno le chiavi univoche prese dal database 
            long chiaveAttr1Int = 0;
            long chiaveB = 0;
            long chiaveC = 0;
            long chiaveD = 0;
            long chiaveE = 0;
            long chiaveAttr2Int = 0;
            long chiaveG = 0;
            long chiaveH = 0;
            long chiaveI = 0;
            long chiaveL = 0;
            
            //i due array che tengono il nome dell'attributo e la chiave ad esso associata 
            string[] Attributi = new string[10];
            long[] chiavi = new long[10];


            long d = 0; //contatore che tiene la posizione degli array 
            long letti = 0; //contatore che tiene le linee lette dal database 

            comando = string.Format("SELECT AttrDefUid, Name FROM AttrDef WHERE NomeAlbero='{0}' ORDER BY Name ASC", albero.getNome());
            
            //settiamo connessione
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = comando;
            cmd.Connection = myconn;

            SqlDataReader msdr = cmd.ExecuteReader();
            while (msdr.Read())
            {
                chiavi[d] = msdr.GetInt32(0);
                Attributi[d] = msdr.GetString(1);
                d++;
                letti++; 
            }
            msdr.Close();

            //inserisco le chiavi nelle variabili 
            for (d = 0; d < letti; d++)
            {
                if (Attributi[d].Equals("attr1Int"))
                {
                    chiaveAttr1Int = chiavi[d];
                }

                if (Attributi[d].Equals("B"))
                {
                    chiaveB = chiavi[d];
                }

                if (Attributi[d].Equals("C"))
                {
                    chiaveC = chiavi[d];
                }

                if (Attributi[d].Equals("D"))
                {
                    chiaveD = chiavi[d];
                }

                if (Attributi[d].Equals("E"))
                {
                    chiaveE = chiavi[d];
                }

                if (Attributi[d].Equals("attr2Int"))
                {
                    chiaveAttr2Int = chiavi[d];
                }

                if (Attributi[d].Equals("G"))
                {
                    chiaveG = chiavi[d];
                }

                if (Attributi[d].Equals("H"))
                {
                    chiaveH = chiavi[d];
                }

                if (Attributi[d].Equals("I"))
                {
                    chiaveI = chiavi[d];
                }

                if (Attributi[d].Equals("L"))
                {
                    chiaveL = chiavi[d];
                }


            }




            //
            //Inseriamo con il for, per farlo in ogni vertice dell' albero!
            //


                long IdAlbero = 0; //futuro IdAlbero

                long EdgeUid = 0; //variabile che conterrà uno ad uno gli Id Edge presi in considerazione
                long VertexUid = 0; //variabile che conterrà uno ad uno gli Id Vertex presi in considerazione
               
                for (long i = 0; i < albero.albero.Length; i++) //for che scorre tutti i vertici 
                {
              

                    //
                    //occupiamoci dell'Id
                    //

                    comando = string.Format("SELECT Id FROM Albero WHERE Nome='{0}'", albero.getNome());
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    msdr = cmd.ExecuteReader();
                    if (msdr.Read())
                    {
                        IdAlbero = msdr.GetInt32(0); //Preleviamo l'IdAlbero 
                    }
                    msdr.Close();


                    //
                    //occupiamoci degli Edge 
                    //


                    comando = string.Format("INSERT INTO Edge(Valore, IdAlbero) VALUES({0}, {1})", albero.albero[i].arcoentrante.val, IdAlbero);
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query di valore e nome_albero si Edge[i] completata 



                    //
                    //occupiamoci dell'EdgeUid
                    //



                    comando = string.Format("SELECT EdgeUid FROM Edge WHERE IdAlbero={0} ORDER BY EdgeUid DESC", IdAlbero);
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    msdr = cmd.ExecuteReader();
                    if(msdr.Read())
                    {                   
                        EdgeUid = msdr.GetInt32(0); //Preleviamo l'EdgeUid                                          
                    }
                    msdr.Close();

                   
                    //
                    //occupiamoci dei valori di Vertex 
                    //


                    comando = string.Format("INSERT INTO Vertex(Name, Arcoentrante, IdAlbero) VALUES('{0}',{1},'{2}')", albero.albero[i].nome, EdgeUid, IdAlbero); 

                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    cmd.ExecuteNonQuery();
                    //query di nome, valore e nome_albero completata su albero[i]


                    //
                    //occupiamoci dei VertexUid 
                    //


                    comando = string.Format("SELECT VertexUid FROM Vertex WHERE IdAlbero={0} ORDER BY VertexUid DESC", IdAlbero);
                    //settiamo connessione
                    cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = comando;
                    cmd.Connection = myconn;

                    msdr = cmd.ExecuteReader();
                    if (msdr.Read())
                    {
                    VertexUid = msdr.GetInt32(0); //Preleviamo il VertexUid         
                    }                   
                                                                                           
                    msdr.Close();


                //
                //Occupiamoci di VertexAttrUsage 
                //


                comando = string.Format("INSERT INTO VertexAttrUsage(ObjectVUid, AttrDefUid, Value) VALUES({0}, {1}, '{2}')", VertexUid, chiaveAttr1Int, albero.albero[i].attr1Int);
                //settiamo connessione
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = comando;
                cmd.Connection = myconn;

                cmd.ExecuteNonQuery();
                //query completata 


                comando = string.Format("INSERT INTO EdgeAttrUsage(ObjectEUid, AttrDefUid, Value) VALUES({0},{1},'{2}')", EdgeUid, chiaveAttr2Int, albero.albero[i].arcoentrante.attr2Int);
                //settiamo connessione
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = comando;
                cmd.Connection = myconn;

                cmd.ExecuteNonQuery();
                //query completata 


                //for che inserisce nel DB solo gli attributi chiamati dall'utente 
                for (int l = 0; l < albero.chiamato.Length; l++)
                {

                    if (albero.chiamato[l] == 0)
                    {
                        comando = string.Format("INSERT INTO VertexAttrUsage(ObjectVUid, AttrDefUid, Value) VALUES({0}, {1}, '{2}')", VertexUid, chiaveB, albero.albero[i].B);
                        //settiamo connessione
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = comando;
                        cmd.Connection = myconn;

                        cmd.ExecuteNonQuery();
                        //query completata 
                    }


                    if (albero.chiamato[l] == 1)
                    {

                        comando = string.Format("INSERT INTO VertexAttrUsage(ObjectVUid, AttrDefUid, Value) VALUES({0}, {1}, '{2}')", VertexUid, chiaveC, albero.albero[i].C);
                        //settiamo connessione
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = comando;
                        cmd.Connection = myconn;

                        cmd.ExecuteNonQuery();
                        //query completata 
                    }

                    if (albero.chiamato[l] == 2)
                    {
                        comando = string.Format("INSERT INTO VertexAttrUsage(ObjectVUid, AttrDefUid, Value) VALUES({0}, {1}, '{2}')", VertexUid, chiaveD, albero.albero[i].D);
                        //settiamo connessione
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = comando;
                        cmd.Connection = myconn;

                        cmd.ExecuteNonQuery();
                        //query completata 
                    }


                    if (albero.chiamato[l] == 3)
                    {

                        comando = string.Format("INSERT INTO VertexAttrUsage(ObjectVUid, AttrDefUid, Value) VALUES({0}, {1}, '{2}')", VertexUid, chiaveE, albero.albero[i].E);
                        //settiamo connessione
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = comando;
                        cmd.Connection = myconn;

                        cmd.ExecuteNonQuery();
                        //query completata 
                        //Query VertexAttrUsage completata 
                    }


                    //
                    //adesso EdgeAttrUsage
                    //


                    if (albero.chiamato[l] == 4)
                    {

                        comando = string.Format("INSERT INTO EdgeAttrUsage(ObjectEUid, AttrDefUid, Value) VALUES({0},{1},'{2}')", EdgeUid, chiaveG, albero.albero[i].arcoentrante.G);
                        //settiamo connessione
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = comando;
                        cmd.Connection = myconn;

                        cmd.ExecuteNonQuery();
                        //query completata 
                    }


                    if (albero.chiamato[l] == 5)
                    {

                        comando = string.Format("INSERT INTO EdgeAttrUsage(ObjectEUid, AttrDefUid, Value) VALUES({0},{1},'{2}')", EdgeUid, chiaveH, albero.albero[i].arcoentrante.H);
                        //settiamo connessione
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = comando;
                        cmd.Connection = myconn;

                        cmd.ExecuteNonQuery();
                        //query completata 
                    }



                    if (albero.chiamato[l] == 6)
                    {

                        comando = string.Format("INSERT INTO EdgeAttrUsage(ObjectEUid, AttrDefUid, Value) VALUES({0},{1},'{2}')", EdgeUid, chiaveI, albero.albero[i].arcoentrante.I);
                        //settiamo connessione
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = comando;
                        cmd.Connection = myconn;

                        cmd.ExecuteNonQuery();
                        //query completata 

                    }



                    if (albero.chiamato[l] == 7)
                    {

                        comando = string.Format("INSERT INTO EdgeAttrUsage(ObjectEUid, AttrDefUid, Value) VALUES({0},{1},'{2}')", EdgeUid, chiaveL, albero.albero[i].arcoentrante.L);
                        //settiamo connessione
                        cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = comando;
                        cmd.Connection = myconn;

                        cmd.ExecuteNonQuery();
                        //query completata 
                    }

                } //fine ciclo for array chiamato 

                } //fine ciclo for array vertici 


                //importazione in DB è stata completata

                myconn.Close(); //chiusura connessione


                //Riuscita compito
                MessageBox.Show("Update su database completato", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            catch (Exception ex2)
            {
                //fallimento compito 
                MessageBox.Show("ERRORE: " + ex2.Message);
                MessageBox.Show("Update su database non riuscito", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
