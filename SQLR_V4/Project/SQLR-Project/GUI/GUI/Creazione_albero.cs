using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Creazione_albero : Form
    {
        public Creazione_albero()
        {
            InitializeComponent();
        }

        private void Creazione_albero_Load(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Controllo errori 

            if (((this.textBox1.Text).Equals("")) || ((this.textBox2.Text).Equals("")) || ((this.textBox3.Text).Equals("")) || ((this.textBox4.Text).Equals("")))
            {
                MessageBox.Show("Campi mancanti", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            //riempimento nome e Tipo albero 
            string nome = textBox1.Text;
            string Tipo = textBox2.Text;

            //riempimento SplitSize e depth 
            int splitSize, depth;

            if (!(int.TryParse(textBox3.Text, out splitSize)))
            {
                MessageBox.Show("Sono stati inseriti parametri non corretti", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            if (!(int.TryParse(textBox4.Text, out depth)))
            {
                MessageBox.Show("Sono stati inseriti parametri non corretti", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                 
            }


            //
            //Riempimento attributi 
            //
           

            //string[] attr = { "B","C","D","E","G","H","I","L" }; //attributi possibili
            bool[] check = { false, false, false, false, false, false, false, false }; //check si o no ?

            int[] chiamato = new int[8]; //i numeri qui riferiscono l'attributo checked dell'array attr tramite posizione 

            for(int i = 0; i < chiamato.Length; i++)
            {
                chiamato[i] = -1; 
            }

            int count = 0; //contatore di chiamato 


            //controllo checkbox Vertex 
            if(checkBox2.Checked)
            {
                check[0] = true;
                chiamato[count++] = 0; 
            }

            if (checkBox3.Checked)
            {
                check[1] = true;
                chiamato[count++] = 1;
            }

            if (checkBox4.Checked)
            {
                check[2] = true;
                chiamato[count++] = 2;
            }

            if (checkBox5.Checked)
            {
                check[3] = true;
                chiamato[count++] = 3;
            }


            //controllo checkbox Edge 
            if (checkBox9.Checked)
            {
                check[4] = true;
                chiamato[count++] = 4;
            }

            if (checkBox8.Checked)
            {
                check[5] = true;
                chiamato[count++] = 5;
            }

            if (checkBox7.Checked)
            {
                check[6] = true;
                chiamato[count++] = 6;
            }

            if (checkBox6.Checked)
            {
                check[7] = true;
                chiamato[count++] = 7;
            }


            //parametri corretti, determinazione checkbox scelti dall'utente terminata         
            //creazione instanza e salvataggio 

            Tree albero = new Tree(nome, Tipo, splitSize, depth);

            albero.chiamato = chiamato;

            albero.creaAlbero(); //crea fisicamente l'albero 

                     
            //
            //lavori su albero finiti, ora salvataggio l'albero in file .xml
            //
             
            saveFileDialog1.ShowDialog(); 
            
            //Se il nome del file da salvare non è una stringa vuota allora lo salvo 
            if (saveFileDialog1.FileName != "")
            {   
                //Serializzazione 
                var writer = new System.Xml.Serialization.XmlSerializer(typeof(Tree));
                var wfile = new System.IO.StreamWriter(saveFileDialog1.FileName);
                writer.Serialize(wfile, albero);
                wfile.Close();
                
                //Notifica salvataggio completato 
                MessageBox.Show("Salvataggio file completato", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //chiusura Form creazione albero
                this.Dispose(); 
            }

        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
