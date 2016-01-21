using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Serizlizzazione
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
            Test classe = null; //classe sarà l'istanza albero da ricreare dal file, ora è a null  

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {   //Deserializzazione
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Test)); //Test è il tipo della classe (sarà di tipo albero)
                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);

                classe = (Test)reader.Deserialize(file);
                file.Close();

                //MessageBox.Show(classe.dato1 + " " + classe.dato2); //stampo i dati in classe 
            }

            if (classe == null) return; //non mi connetto al database se non ho importato nessun file xml 

            //Connessione a Database 
            try
            {
                //Prova. Qui ci saranno i comandi per le insert nel DB
                string comando = string.Format("INSERT INTO Prova VALUES('{0}','{1}')", classe.dato1, classe.dato2); //classe è l'albero deserializzato

                //MessageBox.Show("Apertura connessione");

                SqlConnection myconn = new SqlConnection("Persist Security Info=False;Integrated Security=true;Initial Catalog=EngineDB;server=(local)"); //connessione a EngineDB
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = comando;
                cmd.Connection = myconn; 

                myconn.Open(); //apertura connessione verso il database
                //MessageBox.Show("Connessione stabilita");
                //OPERAZIONI

                //MessageBox.Show("Query in corso...");
                cmd.ExecuteNonQuery();

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

        private void button3_Click(object sender, EventArgs e)
        {
            Esecuzione_calcolo b = new Esecuzione_calcolo();
            b.ShowDialog(); 
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

    }
}
