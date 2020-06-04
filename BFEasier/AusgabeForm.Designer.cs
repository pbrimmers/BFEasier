namespace BFEasier
{
    partial class AusgabeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AusgabeForm));
            this.panelAusgabe = new System.Windows.Forms.Panel();
            this.pictureBoxAusgabe = new System.Windows.Forms.PictureBox();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.panelAusgabe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAusgabe)).BeginInit();
            this.SuspendLayout();
            // 
            // panelAusgabe
            // 
            this.panelAusgabe.AutoScroll = true;
            this.panelAusgabe.BackColor = System.Drawing.Color.White;
            this.panelAusgabe.Controls.Add(this.pictureBoxAusgabe);
            this.panelAusgabe.Location = new System.Drawing.Point(12, 50);
            this.panelAusgabe.Name = "panelAusgabe";
            this.panelAusgabe.Size = new System.Drawing.Size(440, 297);
            this.panelAusgabe.TabIndex = 0;
            // 
            // pictureBoxAusgabe
            // 
            this.pictureBoxAusgabe.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAusgabe.Name = "pictureBoxAusgabe";
            this.pictureBoxAusgabe.Size = new System.Drawing.Size(123, 125);
            this.pictureBoxAusgabe.TabIndex = 0;
            this.pictureBoxAusgabe.TabStop = false;
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Übersicht"});
            this.comboBox.Location = new System.Drawing.Point(358, 6);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(94, 21);
            this.comboBox.TabIndex = 1;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Blau = Primimplikant  -  Orange = Don\'t Care";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "In den Klammern steht die numerische Entsprechung des Minterms bzw die der abgede" +
                "ckten Minterme";
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Location = new System.Drawing.Point(282, 21);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(70, 23);
            this.buttonSpeichern.TabIndex = 4;
            this.buttonSpeichern.Text = "Speichern";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.ButtonSpeichern_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "JPEG-Bild|*.jpg|Bitmap-Bild|*.bmp|GIF-Bild|*.gif";
            this.saveFileDialog.OverwritePrompt = false;
            this.saveFileDialog.Title = "Bilddatei speichern";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Als Bild";
            // 
            // AusgabeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 359);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSpeichern);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.panelAusgabe);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AusgabeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ausgabe";
            this.SizeChanged += new System.EventHandler(this.AusgabeForm_SizeChanged);
            this.panelAusgabe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAusgabe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelAusgabe;
        private System.Windows.Forms.PictureBox pictureBoxAusgabe;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSpeichern;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label3;
    }
}