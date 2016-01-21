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
            saveFileDialog1.ShowDialog(); 
            
            //Se il nome del file da salvare non è una stringa vuota allora lo salvo 
            if (saveFileDialog1.FileName != "")
            {   //Serializzazione 
                var writer = new System.Xml.Serialization.XmlSerializer(typeof(Test));
                var wfile = new System.IO.StreamWriter(saveFileDialog1.FileName);
                writer.Serialize(wfile, albero);
                wfile.Close();
                
                //Notifica salvataggio completato 
                MessageBox.Show("Salvataggio file completato", "GUI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //chiusura Form crezione albero
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
