using System;
using System.Windows.Forms;

namespace BFEasier
{
    public partial class EingabeForm : Form
    {
        private readonly Boolean first;

        // Anzahl der Ein- bzw Ausgabeparameter
        public Int32 AnzEinVar { get; private set; }

        public Int32 AnzAusVar { get; private set; }

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
        public EingabeForm(Int32 ein, Int32 aus)
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
        private void buttonErstellen_Click(Object sender, EventArgs e)
        {
            AnzEinVar = comboBoxEin.SelectedIndex + 1;
            AnzAusVar = (Int32)numericUpDownAus.Value;
            if (first)
            {
                try
                {
                    var form = new FormTabelle(new Funktionstabelle(AnzEinVar, AnzAusVar));
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
            {
                Close();
            }
        }
    }
}
