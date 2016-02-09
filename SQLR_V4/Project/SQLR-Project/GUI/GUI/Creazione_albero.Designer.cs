using System.Windows.Forms; 

namespace GUI
{
    partial class Creazione_albero
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Creazione_albero));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Split Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Depth";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(161, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(161, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Crea albero";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML Files (*.xml)|*.xml";
            this.saveFileDialog1.InitialDirectory = Application.StartupPath ;
            this.saveFileDialog1.Title = "Salvataggio albero";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(161, 109);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nome";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(161, 135);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tipo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 164);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vertex AttrList";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(6, 125);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(33, 17);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Text = "E";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 102);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(34, 17);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "D";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 79);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(33, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "C";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 56);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(33, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "B";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(6, 33);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Attr1Int  ( * )";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox6);
            this.groupBox2.Controls.Add(this.checkBox7);
            this.groupBox2.Controls.Add(this.checkBox8);
            this.groupBox2.Controls.Add(this.checkBox9);
            this.groupBox2.Controls.Add(this.checkBox10);
            this.groupBox2.Location = new System.Drawing.Point(177, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(129, 164);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edge AttrList";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(6, 125);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(32, 17);
            this.checkBox6.TabIndex = 4;
            this.checkBox6.Text = "L";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(6, 102);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(29, 17);
            this.checkBox7.TabIndex = 3;
            this.checkBox7.Text = "I";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(6, 79);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(34, 17);
            this.checkBox8.TabIndex = 2;
            this.checkBox8.Text = "H";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(6, 56);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(34, 17);
            this.checkBox9.TabIndex = 1;
            this.checkBox9.Text = "G";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Checked = true;
            this.checkBox10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox10.Enabled = false;
            this.checkBox10.Location = new System.Drawing.Point(6, 33);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(78, 17);
            this.checkBox10.TabIndex = 0;
            this.checkBox10.Text = "attr2Int ( * )";
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.CheckedChanged += new System.EventHandler(this.checkBox10_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 366);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(303, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "( * ): L\'Engine eseguirà il calcolo della somma su questi attributi.";
            // 
            // Creazione_albero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 445);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Creazione_albero";
            this.Text = "Creazione albero";
            this.Load += new System.EventHandler(this.Creazione_albero_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
    }

    /// <summary>
    ///E' la classe da serializzare/deserializzare, è l'albero 
    /// Classi Tree, Vertex e Edge
    /// </summary>
    /// 


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

                contVer += this.albero[i].getVal();                          //incrementa il risultato con il valore del vertice attuale
                contArc += this.albero[i].getPesoArco();                    //incrementa il risultato con il peso dell'arco in considerazione

                // siamo usciti dal 'for' quindi la variabile 'i' conterrà l'indice del vertice di destinazione //

                while (this.albero[i].getNome() != a)                  //sale l'albero finchè trova il vertice di partenza passatogli
                { 
                    i = i - this.depth;                              //risale al padre del vertice attuale 
                    contVer += this.albero[i].getVal();
                    contArc += this.albero[i].getPesoArco();
                }
                contVer += this.albero[i].getVal();
                contArc += this.albero[i].getPesoArco();

               
                
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

                contVer += this.albero[i].getVal();                           //incrementa il risultato con il valore del vertice attuale
                contArc += this.albero[i].getPesoArco();                     //incrementa il risultato con il peso dell'arco in considerazione

                //siamo usciti dal 'for' quindi la variabile 'i' conterrà l'indice del vertice di destinazione//

                while (this.albero[i].getNome() != a)                   //sale l'albero finchè trova il vertice di partenza
                {
                    i = i - this.depth;                               //risale al padre del vertice attuale 
                    contVer += this.albero[i].getVal();
                    contArc += this.albero[i].getPesoArco();
                }
                contVer += this.albero[i].getVal();
                contArc += this.albero[i].getPesoArco();

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
            if (rdm == (long.MaxValue-1)) rdm = 0;

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
            this.attr1Int = ((random.Next(1, 98) * indice) % 98)+ 1;
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
}