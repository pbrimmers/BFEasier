namespace BFEasier
{
    partial class EingabeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxEin = new System.Windows.Forms.ComboBox();
            this.numericUpDownAus = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonErstellen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bitte wählen Sie die Anzahl der Ein- und Ausgabewerte";
            // 
            // comboBoxEin
            // 
            this.comboBoxEin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEin.FormattingEnabled = true;
            this.comboBoxEin.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxEin.Location = new System.Drawing.Point(12, 25);
            this.comboBoxEin.Name = "comboBoxEin";
            this.comboBoxEin.Size = new System.Drawing.Size(55, 21);
            this.comboBoxEin.TabIndex = 1;
            // 
            // numericUpDownAus
            // 
            this.numericUpDownAus.Location = new System.Drawing.Point(12, 52);
            this.numericUpDownAus.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownAus.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAus.Name = "numericUpDownAus";
            this.numericUpDownAus.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownAus.TabIndex = 2;
            this.numericUpDownAus.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Eingangswerte";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "verschiedene Ausgabegrößen";
            // 
            // buttonErstellen
            // 
            this.buttonErstellen.Location = new System.Drawing.Point(12, 78);
            this.buttonErstellen.Name = "buttonErstellen";
            this.buttonErstellen.Size = new System.Drawing.Size(136, 23);
            this.buttonErstellen.TabIndex = 5;
            this.buttonErstellen.Text = "Funktionstabelle erstellen";
            this.buttonErstellen.UseVisualStyleBackColor = true;
            this.buttonErstellen.Click += new System.EventHandler(this.buttonErstellen_Click);
            // 
            // EingabeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 112);
            this.ControlBox = false;
            this.Controls.Add(this.buttonErstellen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownAus);
            this.Controls.Add(this.comboBoxEin);
            this.Controls.Add(this.label1);
            this.Name = "EingabeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BFEasier";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxEin;
        private System.Windows.Forms.NumericUpDown numericUpDownAus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonErstellen;
    }
}