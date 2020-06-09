namespace BFEasier
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public partial class AusgabeForm : Form
    {
        private readonly Ausgabe ausgabe;
        private readonly Int32[] letzteSelectedIndizes;
        private readonly List<Int32> ausgabeliste;

        /// <summary>
        /// Gibt den aktuell ausgewählten Index der ComboBox
        /// </summary>
        public Int32 SelectedIndex => letzteSelectedIndizes[1];

        /// <summary>
        /// Gibt den verherigen ausgewählten Index der ComboBox
        /// </summary>
        public Int32 LastIndex
        {
            get
            {
                if (letzteSelectedIndizes[0] > letzteSelectedIndizes[1])
                {
                    letzteSelectedIndizes[0]--;
                }

                return letzteSelectedIndizes[0];
            }
        }

        /// <summary>
        /// Konstruktor für das erste Standardausgabe
        /// </summary>
        /// <param name="list">List-Array mit allen Schritten der Vereinfachung aller Variablen</param>
        /// <param name="vereinfachteTerme">List mit den vereinfachten Termen</param>
        public AusgabeForm(List<List<Term[]>>[] list, List<Term[]> vereinfachteTerme)
        {
            InitializeComponent();
            ausgabe = new Ausgabe(list, vereinfachteTerme);

            MinimumSize = Size;

            ausgabeliste = new List<Int32> { -1 };
            for (var i = 0; i < list.Length; i++)
            {
                comboBox.Items.Add(Properties.Settings.Default.ausChar + (i + 1).ToString());
                ausgabeliste.Add(i);
            }
            letzteSelectedIndizes = new Int32[2];
            comboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Konstruktor, um Ausgabegrößen, die Fehler verursachen auszuschließen
        /// </summary>
        /// <param name="list">List-Array mit allen Schritten der Vereinfachung aller Variablen</param>
        /// <param name="vereinfachteTerme">List mit den vereinfachten Termen</param>
        /// <param name="errorIndizes">Int-Array mit allen Indizes, die Fehler verursachen</param>
        /// <param name="lastIndex">Letzter funktionierender Index</param>
        public AusgabeForm(List<List<Term[]>>[] list,
                           List<Term[]> vereinfachteTerme, ReadOnlySpan<Int32> errorIndizes, Int32 lastIndex) : this(list, vereinfachteTerme)
        {
            foreach (var i in errorIndizes)
            {
                ausgabeliste.RemoveAt(i);
                comboBox.Items.RemoveAt(i);
            }
            letzteSelectedIndizes[1] = lastIndex;
            comboBox.SelectedIndex = lastIndex;
            ComboBox_SelectedIndexChanged(comboBox, new EventArgs());
        }

        /// <summary>
        /// Ändert die Ausgabe entsprechend des ausgewählten Index der Combo-Box
        /// </summary>
        private void ComboBox_SelectedIndexChanged(Object sender, EventArgs e)
        {
            // Wartebalken anzeigen
            // TODO: Run background TASK with cancellation token
            //var thread = new System.Threading.Thread(Waiting.Wait);
            //thread.Start();

            // Neue Ausgabe erzeugen
            if (ausgabeliste[comboBox.SelectedIndex] == -1)
            {
                ausgabe.DrawAll();
            }
            else
            {
                ausgabe.DrawOne(ausgabeliste[comboBox.SelectedIndex]);
            }

            // Ausgabe anpassen
            pictureBoxAusgabe.Size = ausgabe.Grafik.Size;
            pictureBoxAusgabe.Image = ausgabe.Grafik;
            letzteSelectedIndizes[0] = letzteSelectedIndizes[1];
            letzteSelectedIndizes[1] = comboBox.SelectedIndex;

            //System.Threading.Thread.Sleep(100);
            //thread.Abort();
        }

        /// <summary>
        /// Öffnet einen Dialog zum speichern der Ausgabe in einer Bilddatei und speichert sie dort
        /// </summary>
        private void ButtonSpeichern_Click(Object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                var fileName = saveFileDialog.FileName;
                System.Drawing.Imaging.ImageFormat imageFormat;
                // Werte der Auswahl anpassen
                switch (saveFileDialog.FilterIndex)
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
                // Falls die Datei bereits existiert, fragen, ob überschrieben werden soll
                if (!System.IO.File.Exists(fileName) || MessageBox.Show(fileName + " existiert bereits. Überschreiben?", "Überschreiben?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ausgabe.Grafik.Save(fileName, imageFormat);
                }
            }
        }
    }
}
