namespace BFEasier
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

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
        private void FormTabelle_Load(Object sender, EventArgs e)
        {
            InitialisiereFunktionstabelle();

            comboBoxWert.SelectedIndex = 1;

            // Entsprechend der Anzahl der Ausgangsvariablen die Combobox mit Elementen füllen
            for (Int32 i = 1; i <= fTabelle.AnzahlAusgabevariablen; i++)
            {
                comboBoxVariable.Items.Add(Properties.Settings.Default.ausChar + i.ToString());
            }
            comboBoxVariable.SelectedIndex = 0;
        }

        /// <summary>
        /// Berechnet die Tabellengröße und erstellt das Bild dieser und
        /// weist die entsprechenden Parameter der PictureBox zu.
        /// </summary>
        private void InitialisiereFunktionstabelle()
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
        private void FormTabelle_SizeChanged(Object sender, EventArgs e)
        {
            // TODO: Add proper resize behaviour
            //groupBoxTabelle.Size = new Size(Size.Width - gruppeTabSize.Width, Size.Height - gruppeTabSize.Height);
            //panelTabelle.Size = new Size(groupBoxTabelle.Size.Width - panelTabSize.Width, groupBoxTabelle.Size.Height - panelTabSize.Height);
            //panelRechts.Location = new Point(Size.Width - panelFunkSize.Width, panelRechts.Location.Y);
        }

        /// <summary>
        /// Ändert ggf den Wert einer Zelle
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'MouseEventArgs'</param>
        private void Tabelle_MouseClick(Object sender, MouseEventArgs e)
        {
            fTabelle.Mausklick(e.X, e.Y);
            tabelle.Image = fTabelle.Grafik;
        }

        /// <summary>
        /// Ändert ggf den Wert einer Zelle
        /// </summary>
        /// <param name="sender">Objekt, das diese Funktion aufruft</param>
        /// <param name="e">Objekt der Klasse 'MouseEventArgs'</param>
        private void Tabelle_MouseDoubleClick(Object sender, MouseEventArgs e)
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
        private void ButtonSetzen_Click(Object sender, EventArgs e)
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
        private void ButtonVereinfache_Click(Object sender, EventArgs e)
        {
            if (checkBoxAusfuehrlich.Checked)
            {
                ErweitereAusgabe();
            }
            else
            {
                EinfacheAusgabe();
            }
        }

        /// <summary>
        /// Zeigt ausführlich die Schritte zur Vereinfachung in einem extra Form
        /// </summary>
        private void ErweitereAusgabe()
        {
            var vereinfachteTerme = new System.Collections.ArrayList();
            QuineMcCluskey qmc;
            var list = new System.Collections.ArrayList[fTabelle.AnzahlAusgabevariablen];

            // TODO: Run background TASK with cancellation token
            //var thread = new System.Threading.Thread(Waiting.Wait);
            //thread.Start();

            // Für jede Ausgangsvariable den Term vereinfachen
            for (Int32 i = 0; i < fTabelle.AnzahlAusgabevariablen; i++)
            {
                // Terme auslesen und vereinfachen
                qmc = new QuineMcCluskey(fTabelle.GibMinterme(i));
                list[i] = new System.Collections.ArrayList();
                vereinfachteTerme.Add(qmc.Vereinfache(ref list[i]));
            }

            //System.Threading.Thread.Sleep(100);
            //thread.Abort();

            // Ausgabe im Form
            var errorIndizes = new System.Collections.ArrayList();
            var form = new AusgabeForm(list, vereinfachteTerme);
            Boolean just_error = true;
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
            String tempString = "";

            // TODO: Run background TASK with cancellation token
            //var thread = new System.Threading.Thread(Waiting.Wait);
            //thread.Start();

            // Für jede Ausgangsvaribale den Term vereinfachen
            for (Int32 i = 0; i < fTabelle.AnzahlAusgabevariablen; i++)
            {
                // Terme auslesen und vereinfachen
                qmc = new QuineMcCluskey(fTabelle.GibMinterme(i));
                vereinfachteTerme = qmc.Vereinfache();

                // Vereinfachten Term in den temporären String schreiben
                tempString += Properties.Settings.Default.ausChar + (i + 1).ToString() + " = ";
                tempString += vereinfachteTerme[0].ToString();
                for (Int32 j = 1; j < vereinfachteTerme.Length; j++)
                {
                    tempString += "+" + vereinfachteTerme[j].ToString();
                }
                tempString += "\r\n";
            }

            //System.Threading.Thread.Sleep(100);
            //thread.Abort();

            // Text in die Textbox schreiben
            textBoxAusgabe.Text = tempString;
        }

        private void ButtonParameterAendern_Click(Object sender, EventArgs e)
        {
            var form = new EingabeForm(fTabelle.AnzahlEingabevariablen, fTabelle.AnzahlAusgabevariablen);
            form.ShowDialog();
            fTabelle = new Funktionstabelle(form.AnzEinVar, form.AnzAusVar);
            InitialisiereFunktionstabelle();
        }

        private void ButtonSpeichern_Click(Object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                String fileName = saveFileDialog1.FileName;
                System.Drawing.Imaging.ImageFormat imageFormat;
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        if (System.IO.Path.GetExtension(fileName).ToLower() != ".jpg")
                        {
                            fileName += ".jpg";
                        }

                        break;
                    case 2:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                        if (System.IO.Path.GetExtension(fileName).ToLower() != ".bmp")
                        {
                            fileName += ".bmp";
                        }

                        break;
                    case 3:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                        if (System.IO.Path.GetExtension(fileName).ToLower() != ".gif")
                        {
                            fileName += ".gif";
                        }

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
