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

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //scelta utente: Eseguire il calcolo?
            var result = MessageBox.Show("Eseguire il calcolo?", "GUI", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

            if (result == System.Windows.Forms.DialogResult.No) { return; }

            //esecuzione engine
            Process myProcess = new Process();
            //creazione istanza Albero 
            Test alberoTest = new Test();
           
            try
            {
                //Connessione a database per ripescaggio dati 
                SqlConnection myconn = new SqlConnection("Persist Security Info=False;Integrated Security=true;Initial Catalog=EngineDB;server=(local)"); //connessione a database 
                SqlCommand cmd = new SqlCommand("select * from Prova", myconn); //COMANDO SQL DI PROVA

                //MessageBox.Show("Tentativo di connessione al database");
                myconn.Open(); //apertura connessione
                //MessageBox.Show("Connessione stabilita");

                SqlDataReader myReader = cmd.ExecuteReader();

                //MessageBox.Show("Lettura dati dal database in corso...");
                //lettura dati 
                while (myReader.Read())
                {
                    alberoTest.dato1 = myReader.GetString(0);
                    alberoTest.dato2 = myReader.GetInt32(1); 
                }

                //MessageBox.Show("Lettura completata: dato1 = " + alberoTest.dato1 + " dato2 = " + alberoTest.dato2);
                
                //chiusura connessione database 
                // Chiude il DataReader		
                myReader.Close();
                // Chiude la Connessione
                myconn.Close();             
                
                //Attivazione Engine
                //MessageBox.Show("Attivazione Engine in corso...");
                myProcess.StartInfo.UseShellExecute = false; //Ottiene o imposta un valore che indica se utilizzare la shell del sistema operativo per avviare il processo.

                myProcess.StartInfo.FileName = "Engine.exe"; //Percorso file da avviare 
                myProcess.StartInfo.Arguments = "GUI"; //Argomento "GUI" da passare all'Engine per far capire ad esso che è stato chiamato dalla nostra GUI
                myProcess.StartInfo.CreateNoWindow = false; //false = finestra visibile, true il contrario
                myProcess.Start(); //Processo Engine attivato 

                //Comunicazione con Engine.exe tramite Named Pipes 
                NamedPipeClientStream Pipe1 = new NamedPipeClientStream("Pipe1");
                Thread.Sleep(1000);  //aspettiamo 1 secondo per far si che l'Engine sia pronto a ricevere la connessione
                Pipe1.Connect();
                //MessageBox.Show("Connessione con Engine stabilita", "GUI");

                //riempimento struttura messaggio 
                messaggio message = new messaggio();
                message.Albero = alberoTest;
                message.Risposta = "";

                //mando il messaggio 
                //MessageBox.Show("Invio messaggio a Engine", "GUI");
                var f = new System.Xml.Serialization.XmlSerializer(typeof(messaggio));
                f.Serialize(Pipe1, message); //mando il messaggio sulla pipe  

                //chiusura pipe invio 
                Pipe1.Close();

                //ricezione messaggio creando pipe di ricezione
                NamedPipeServerStream Pipe2 = new NamedPipeServerStream("Pipe2");
                Pipe2.WaitForConnection();


                //MessageBox.Show("Ricezione messaggio da Engine", "GUI");
                messaggio ricevuto = new messaggio();
                ricevuto = (messaggio)f.Deserialize(Pipe2);

                //chiusura pipe ricezione
                Pipe2.Close();

                //compito riuscito
                //Stampa risultato 
                MessageBox.Show("RESULT = Stampa contenuto messaggio ricevuto da Engine: " + ricevuto.Risposta, "GUI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //chiusura Form esecuzione calcolo 
                this.Dispose(); 

            }
            catch (Exception ex)
            {   //accade eccezione, fallimento compito
                if (ex is System.ComponentModel.Win32Exception) MessageBox.Show("ERRORE: " + "File 'Engine.exe' non trovato: assicurati che il file si trovi nella stessa cartella della GUI e si chiami 'Engine.exe' "); //sollevata eccezione per Engine mancante 
                else 
                MessageBox.Show("ERRORE: " + ex.Message); //Tutte le altre possibili eccezioni 

                //Notifica calcolo non eseguito
                MessageBox.Show("Calcolo non eseguito", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
