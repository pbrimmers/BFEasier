using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BFEasier
{
    public partial class EingabeForm : Form
    {
        private bool first;
        // Anzahl der Ein- bzw Ausgabeparameter
        private int anzEinVar, anzAusVar;

        public int AnzEinVar
        {
            get
            {
                return anzEinVar;
            }
        }

        public int AnzAusVar
        {
            get
            {
                return anzAusVar;
            }
        }

        /// <summary>
        /// Konstruktor um eine neue Funktionstabelle zu erstellen
        /// </summary>
        public EingabeForm()
        {
            InitializeComponent();
            first = true;
            comboBoxEin.SelectedIndex = 0;
        }

        /// <summary>
        /// Konstruktor um Parameter zu ändern
        /// </summary>
        public EingabeForm(int ein, int aus)
        {
            InitializeComponent();
            first = false;
            comboBoxEin.SelectedIndex = ein - 1;
            numericUpDownAus.Value = aus;
            buttonErstellen.Text = "Funktionstabelle ändern";
        }

        /// <summary>
        /// Speichert die gewählten Parameter und schließt das Form
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'EventArgs'</param>
        private void buttonErstellen_Click(object sender, EventArgs e)
        {
            anzEinVar = comboBoxEin.SelectedIndex + 1;
            anzAusVar = (int)numericUpDownAus.Value;
            if (first)
            {
                try
                {
                    FormTabelle form = new FormTabelle(new Funktionstabelle(anzEinVar, anzAusVar));
                    Visible = false;
                    form.ShowDialog();
                    Close();
                }
                catch
                {
                    Visible = true;
                    MessageBox.Show("Zu viele Ausgabegrößen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                Close();
        }
    }
}
