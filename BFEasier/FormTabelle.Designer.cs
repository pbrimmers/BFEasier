namespace BFEasier
{
    partial class FormTabelle
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTabelle));
            this.panelTabelle = new System.Windows.Forms.Panel();
            this.tabelle = new System.Windows.Forms.PictureBox();
            this.groupBoxTabelle = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxVariable = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxWert = new System.Windows.Forms.ComboBox();
            this.buttonSetzen = new System.Windows.Forms.Button();
            this.panelRechts = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.buttonParameterAendern = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxAusfuehrlich = new System.Windows.Forms.CheckBox();
            this.textBoxAusgabe = new System.Windows.Forms.TextBox();
            this.buttonVereinfache = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panelTabelle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabelle)).BeginInit();
            this.groupBoxTabelle.SuspendLayout();
            this.panelRechts.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTabelle
            // 
            this.panelTabelle.AutoScroll = true;
            this.panelTabelle.BackColor = System.Drawing.Color.White;
            this.panelTabelle.Controls.Add(this.tabelle);
            this.panelTabelle.Location = new System.Drawing.Point(6, 19);
            this.panelTabelle.Name = "panelTabelle";
            this.panelTabelle.Size = new System.Drawing.Size(286, 395);
            this.panelTabelle.TabIndex = 0;
            // 
            // tabelle
            // 
            this.tabelle.BackColor = System.Drawing.Color.White;
            this.tabelle.Location = new System.Drawing.Point(0, 0);
            this.tabelle.Name = "tabelle";
            this.tabelle.Size = new System.Drawing.Size(262, 344);
            this.tabelle.TabIndex = 0;
            this.tabelle.TabStop = false;
            this.tabelle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabelle_MouseDoubleClick);
            this.tabelle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabelle_MouseClick);
            // 
            // groupBoxTabelle
            // 
            this.groupBoxTabelle.Controls.Add(this.panelTabelle);
            this.groupBoxTabelle.Location = new System.Drawing.Point(12, 54);
            this.groupBoxTabelle.Name = "groupBoxTabelle";
            this.groupBoxTabelle.Size = new System.Drawing.Size(298, 420);
            this.groupBoxTabelle.TabIndex = 1;
            this.groupBoxTabelle.TabStop = false;
            this.groupBoxTabelle.Text = "Funktionstabelle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Alle Werte von";
            // 
            // comboBoxVariable
            // 
            this.comboBoxVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVariable.FormattingEnabled = true;
            this.comboBoxVariable.Location = new System.Drawing.Point(86, 19);
            this.comboBoxVariable.Name = "comboBoxVariable";
            this.comboBoxVariable.Size = new System.Drawing.Size(54, 21);
            this.comboBoxVariable.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "auf";
            // 
            // comboBoxWert
            // 
            this.comboBoxWert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWert.FormattingEnabled = true;
            this.comboBoxWert.Items.AddRange(new object[] {
            "*",
            "0",
            "1"});
            this.comboBoxWert.Location = new System.Drawing.Point(165, 19);
            this.comboBoxWert.Name = "comboBoxWert";
            this.comboBoxWert.Size = new System.Drawing.Size(46, 21);
            this.comboBoxWert.TabIndex = 5;
            // 
            // buttonSetzen
            // 
            this.buttonSetzen.Location = new System.Drawing.Point(10, 46);
            this.buttonSetzen.Name = "buttonSetzen";
            this.buttonSetzen.Size = new System.Drawing.Size(75, 23);
            this.buttonSetzen.TabIndex = 6;
            this.buttonSetzen.Text = "setzen";
            this.buttonSetzen.UseVisualStyleBackColor = true;
            this.buttonSetzen.Click += new System.EventHandler(this.buttonSetzen_Click);
            // 
            // panelRechts
            // 
            this.panelRechts.Controls.Add(this.label6);
            this.panelRechts.Controls.Add(this.buttonSpeichern);
            this.panelRechts.Controls.Add(this.buttonParameterAendern);
            this.panelRechts.Controls.Add(this.label5);
            this.panelRechts.Controls.Add(this.groupBox2);
            this.panelRechts.Controls.Add(this.groupBox1);
            this.panelRechts.Location = new System.Drawing.Point(310, 36);
            this.panelRechts.Name = "panelRechts";
            this.panelRechts.Size = new System.Drawing.Size(233, 442);
            this.panelRechts.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Als Bild";
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Location = new System.Drawing.Point(53, 375);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(75, 23);
            this.buttonSpeichern.TabIndex = 13;
            this.buttonSpeichern.Text = "Speichern";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // buttonParameterAendern
            // 
            this.buttonParameterAendern.Location = new System.Drawing.Point(6, 22);
            this.buttonParameterAendern.Name = "buttonParameterAendern";
            this.buttonParameterAendern.Size = new System.Drawing.Size(215, 23);
            this.buttonParameterAendern.TabIndex = 12;
            this.buttonParameterAendern.Text = "Anzahl Ein-/Ausgabegrößen ändern";
            this.buttonParameterAendern.UseVisualStyleBackColor = true;
            this.buttonParameterAendern.Click += new System.EventHandler(this.buttonParameterAendern_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 324);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(216, 43);
            this.label5.TabIndex = 11;
            this.label5.Text = "Die Ausgabe erfolgt in DF, wobei die \'!\' eine Negation bedeuten und die Konjunkti" +
                "onen nicht ausgeschrieben werden.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxVariable);
            this.groupBox2.Controls.Add(this.buttonSetzen);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBoxWert);
            this.groupBox2.Location = new System.Drawing.Point(6, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 75);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Spalte komplett setzen";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxAusfuehrlich);
            this.groupBox1.Controls.Add(this.textBoxAusgabe);
            this.groupBox1.Controls.Add(this.buttonVereinfache);
            this.groupBox1.Location = new System.Drawing.Point(6, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 189);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vereinfachte Funktionen";
            // 
            // checkBoxAusfuehrlich
            // 
            this.checkBoxAusfuehrlich.AutoSize = true;
            this.checkBoxAusfuehrlich.Location = new System.Drawing.Point(6, 164);
            this.checkBoxAusfuehrlich.Name = "checkBoxAusfuehrlich";
            this.checkBoxAusfuehrlich.Size = new System.Drawing.Size(128, 17);
            this.checkBoxAusfuehrlich.TabIndex = 9;
            this.checkBoxAusfuehrlich.Text = "ausführliche Ausgabe";
            this.checkBoxAusfuehrlich.UseVisualStyleBackColor = true;
            // 
            // textBoxAusgabe
            // 
            this.textBoxAusgabe.BackColor = System.Drawing.Color.White;
            this.textBoxAusgabe.Location = new System.Drawing.Point(6, 19);
            this.textBoxAusgabe.Multiline = true;
            this.textBoxAusgabe.Name = "textBoxAusgabe";
            this.textBoxAusgabe.ReadOnly = true;
            this.textBoxAusgabe.Size = new System.Drawing.Size(203, 135);
            this.textBoxAusgabe.TabIndex = 7;
            // 
            // buttonVereinfache
            // 
            this.buttonVereinfache.Location = new System.Drawing.Point(134, 160);
            this.buttonVereinfache.Name = "buttonVereinfache";
            this.buttonVereinfache.Size = new System.Drawing.Size(75, 23);
            this.buttonVereinfache.TabIndex = 8;
            this.buttonVereinfache.Text = "Vereinfache";
            this.buttonVereinfache.UseVisualStyleBackColor = true;
            this.buttonVereinfache.Click += new System.EventHandler(this.buttonVereinfache_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(447, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Sie können die Werte der Ausgabe durch Klicken auf den entsprechenden Parameter ä" +
                "ndern";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(520, 33);
            this.label4.TabIndex = 11;
            this.label4.Text = "Wenn die Werte vollständig eingegeben haben, können Sie sich über den Button \"Ver" +
                "einfache\" die entsprechenden Funktionen anzeigen lassen";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JPEG-Bild|*.jpg|Bitmap-Bild|*.bmp|GIF-Bild|*.gif";
            this.saveFileDialog1.OverwritePrompt = false;
            this.saveFileDialog1.Title = "Bilddatei speichern";
            // 
            // FormTabelle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 486);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panelRechts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBoxTabelle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTabelle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BFEasier";
            this.Load += new System.EventHandler(this.FormTabelle_Load);
            this.SizeChanged += new System.EventHandler(this.FormTabelle_SizeChanged);
            this.panelTabelle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabelle)).EndInit();
            this.groupBoxTabelle.ResumeLayout(false);
            this.panelRechts.ResumeLayout(false);
            this.panelRechts.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTabelle;
        private System.Windows.Forms.PictureBox tabelle;
        private System.Windows.Forms.GroupBox groupBoxTabelle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxVariable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxWert;
        private System.Windows.Forms.Button buttonSetzen;
        private System.Windows.Forms.Panel panelRechts;
        private System.Windows.Forms.Button buttonVereinfache;
        private System.Windows.Forms.TextBox textBoxAusgabe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonParameterAendern;
        private System.Windows.Forms.CheckBox checkBoxAusfuehrlich;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonSpeichern;
        private System.Windows.Forms.Label label6;
    }
}

