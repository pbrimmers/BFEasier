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
    public partial class FormTabelle : Form
    {
        // Objekte zum speichern der Größendifferenz bzw relativen Position zum Form und der Elemente
        private Size gruppeTabSize, panelTabSize, panelFunkSize;
        // Objekt für die Funktionstabelle
        private Funktionstabelle fTabelle;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="fTabelle">Die zu vereinfachende und bearbeitende Funktionstabelle</param>
        public FormTabelle(Funktionstabelle fTabelle)
        {
            InitializeComponent();
            MinimumSize = Size;
            this.fTabelle = fTabelle;

            // Größen-/Positionsdifferenz speichern
            gruppeTabSize = new Size(Size.Width - groupBoxTabelle.Size.Width, Size.Height - groupBoxTabelle.Size.Height);
            panelFunkSize = new Size(Size.Width - panelRechts.Location.X, Size.Height - panelRechts.Location.Y);
            panelTabSize = new Size(groupBoxTabelle.Size.Width - panelTabelle.Size.Width, groupBoxTabelle.Size.Height - panelTabelle.Size.Height);
        }

        /// <summary>
        /// Passt die Oberfläche der Tabelle an
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'EventArgs'</param>
        private void FormTabelle_Load(object sender, EventArgs e)
        {
            initialisiereFunktionstabelle();

            comboBoxWert.SelectedIndex = 1;

            // Entsprechend der Anzahl der Ausgangsvariablen die Combobox mit Elementen füllen
            for (int i = 1; i <= fTabelle.AnzahlAusgabevariablen; i++)
            {
                comboBoxVariable.Items.Add(Properties.Settings.Default.ausChar + i.ToString());
            }
            comboBoxVariable.SelectedIndex = 0;
        }

        /// <summary>
        /// Berechnet die Tabellengröße und erstellt das Bild dieser und
        /// weist die entsprechenden Parameter der PictureBox zu.
        /// </summary>
        private void initialisiereFunktionstabelle()
        {
            fTabelle.SpaltenBerechnen(tabelle.CreateGraphics());
            tabelle.Size = fTabelle.Groesse;
            fTabelle.ZeichneTabelle();
            tabelle.Image = fTabelle.Grafik;
        }

        /// <summary>
        /// Passt die Elemente der Größe des Forms an
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'EventArgs'</param>
        private void FormTabelle_SizeChanged(object sender, EventArgs e)
        {
            groupBoxTabelle.Size = new Size(Size.Width - gruppeTabSize.Width, Size.Height - gruppeTabSize.Height);
            panelTabelle.Size = new Size(groupBoxTabelle.Size.Width - panelTabSize.Width, groupBoxTabelle.Size.Height - panelTabSize.Height);
            panelRechts.Location = new Point(Size.Width - panelFunkSize.Width, panelRechts.Location.Y);
        }

        /// <summary>
        /// Ändert ggf den Wert einer Zelle
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'MouseEventArgs'</param>
        private void tabelle_MouseClick(object sender, MouseEventArgs e)
        {
            fTabelle.Mausklick(e.X, e.Y);
            tabelle.Image = fTabelle.Grafik;
        }

        /// <summary>
        /// Ändert ggf den Wert einer Zelle
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'MouseEventArgs'</param>
        private void tabelle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            fTabelle.Mausklick(e.X, e.Y);
            tabelle.Image = fTabelle.Grafik;
        }

        /// <summary>
        /// Setzt alle Werte für die in ComboBoxVariable ausgewählte Variable
        /// auf den in ComboBoxWert gewählten Wert
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'EventArgs'</param>
        private void buttonSetzen_Click(object sender, EventArgs e)
        {
            // Werte setzen
            fTabelle.SetzeWerte(comboBoxVariable.SelectedIndex, comboBoxWert.SelectedIndex - 1);
            // PictureBox aktualisieren
            tabelle.Image = fTabelle.Grafik;
        }

        /// <summary>
        /// Vereinfacht die Funktionsterme und gibt diese aus
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'EventArgs'</param>
        private void buttonVereinfache_Click(object sender, EventArgs e)
        {
            if (checkBoxAusfuehrlich.Checked)
                ErweitereAusgabe();
            else
                EinfacheAusgabe();
        }

        /// <summary>
        /// Zeigt ausführlich die Schritte zur Vereinfachung in einem extra Form
        /// </summary>
        private void ErweitereAusgabe()
        {
            System.Collections.ArrayList vereinfachteTerme = new System.Collections.ArrayList();
            QuineMcCluskey qmc;
            System.Collections.ArrayList[] list = new System.Collections.ArrayList[fTabelle.AnzahlAusgabevariablen];

            System.Threading.Thread thread = new System.Threading.Thread(Waiting.Wait);
            thread.Start();

            // Für jede Ausgangsvariable den Term vereinfachen
            for (int i = 0; i < fTabelle.AnzahlAusgabevariablen; i++)
            {
                // Terme auslesen und vereinfachen
                qmc = new QuineMcCluskey(fTabelle.GibMinterme(i));
                list[i] = new System.Collections.ArrayList();
                vereinfachteTerme.Add(qmc.Vereinfache(ref list[i]));
            }

            System.Threading.Thread.Sleep(100);
            thread.Abort();

            // Ausgabe im Form
            System.Collections.ArrayList errorIndizes = new System.Collections.ArrayList();
            AusgabeForm form = new AusgabeForm(list, vereinfachteTerme);
            bool just_error = true;
            while (just_error)
            {
                try
                {
                    form.ShowDialog();
                    just_error = false;
                }
                catch
                {
                    MessageBox.Show("Leider ist die Darstellung zu groß!", "Darstellung nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorIndizes.Add(form.SelectedIndex);
                    form = new AusgabeForm(list, vereinfachteTerme, errorIndizes, form.LastIndex);
                }
            }
    
        }

        /// <summary>
        /// Gibt die vereinfachten Terme in der Textbox aus
        /// </summary>
        private void EinfacheAusgabe()
        {
            Term[] vereinfachteTerme;
            QuineMcCluskey qmc;
            string tempString = "";

            System.Threading.Thread thread = new System.Threading.Thread(Waiting.Wait);
            thread.Start();

            // Für jede Ausgangsvaribale den Term vereinfachen
            for (int i = 0; i < fTabelle.AnzahlAusgabevariablen; i++)
            {
                // Terme auslesen und vereinfachen
                qmc = new QuineMcCluskey(fTabelle.GibMinterme(i));
                vereinfachteTerme = qmc.Vereinfache();

                // Vereinfachten Term in den temporären String schreiben
                tempString += Properties.Settings.Default.ausChar + (i + 1).ToString() + " = ";
                tempString += vereinfachteTerme[0].ToString();
                for (int j = 1; j < vereinfachteTerme.Length; j++)
                {
                    tempString += "+" + vereinfachteTerme[j].ToString();
                }
                tempString += "\r\n";
            }

            System.Threading.Thread.Sleep(100);
            thread.Abort();

            // Text in die Textbox schreiben
            textBoxAusgabe.Text = tempString;
        }

        private void buttonParameterAendern_Click(object sender, EventArgs e)
        {
            EingabeForm form = new EingabeForm(fTabelle.AnzahlEingabevariablen, fTabelle.AnzahlAusgabevariablen);
            form.ShowDialog();
            fTabelle = new Funktionstabelle(form.AnzEinVar, form.AnzAusVar);
            initialisiereFunktionstabelle();
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                string fileName = saveFileDialog1.FileName;
                System.Drawing.Imaging.ImageFormat imageFormat;
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        if (System.IO.Path.GetExtension(fileName).ToLower() != ".jpg")
                            fileName += ".jpg";
                        break;
                    case 2:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                        if (System.IO.Path.GetExtension(fileName).ToLower() != ".bmp")
                            fileName += ".bmp";
                        break;
                    case 3:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                        if (System.IO.Path.GetExtension(fileName).ToLower() != ".gif")
                            fileName += ".gif";
                        break;
                    default:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                }
                if (!System.IO.File.Exists(fileName) || MessageBox.Show(fileName + " existiert bereits. Überschreiben?", "Überschreiben?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    fTabelle.Grafik.Save(fileName, imageFormat);
                }
            }
        }
    }
}
