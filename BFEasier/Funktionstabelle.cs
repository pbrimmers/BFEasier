using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace BFEasier
{
    /// <summary>
    /// Klasse zur Verwaltung der Daten der Funktionstabelle
    /// und zur Erstellung der graphischen Oberfläche
    /// </summary>
    public class Funktionstabelle
    {
        private static Pen pen = new Pen(Properties.Settings.Default.Color, 1);
        private static Pen bigPen = new Pen(Properties.Settings.Default.Color, 2);

        private int zeilen, spalten, anzEinVar, anzAusVar;
        /// <summary>
        /// Array das die Werte der Ausgabevariablen speichert.
        /// Der erste Index ist die Ausgabevariable, der zweite die Zeile
        /// </summary>
        private int[,] werte;
        private float[] spaltenbreiten;
        private float zeilenhoehe;
        /// <summary>
        /// Grenze zwischen Ein- und Ausgabevariablen auf der grafischen Ausgabe
        /// </summary>
        private float ausgabe;
        private bool spalten_berechnet;
        private Size groesse;
        private Bitmap grafik;

        public Bitmap Grafik
        {
            get
            {
                return grafik;
            }
        }

        /// <summary>
        /// Objekt der Klasse 'Size' mit der Grösse der Funktionstabelle auf einem 'Graphics'-Objekt
        /// </summary>
        public Size Groesse
        {
            get
            {
                return groesse;
            }
        }

        public int AnzahlAusgabevariablen
        {
            get
            {
                return anzAusVar;
            }
        }

        public int AnzahlEingabevariablen
        {
            get
            {
                return anzEinVar;
            }
        }

        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Erstellt eine Funktionstabelle mit allen Ausgangswerten gleich '0'
        /// </summary>
        /// <param name="ein">Anzahl der Eingangsgrößen</param>
        /// <param name="aus">Anzahl der Ausgangsgrößen</param>
        public Funktionstabelle(int ein, int aus)
        {
            this.anzEinVar = ein;
            this.anzAusVar = aus;
            spalten = ein + aus;
            zeilen = pow2(ein);
            spaltenbreiten = new float[spalten];
            spalten_berechnet = false;
            werte = new int[aus,zeilen];
            grafik = new Bitmap(2,2);
        }

        /// <summary>
        /// Berechnet die Potenzfunktion zu Basis 2
        /// </summary>
        /// <param name="e">Exponent vom Typ 'int'</param>
        /// <returns>Ergebnis vom Typ 'int'</returns>
        private int pow2(int e)
        {
            int temp = 1;
            for (int i = 0; i < e; i++)
            {
                temp *= 2;
            }
            return temp;
        }

        /// <summary>
        /// Berechnet die Spaltenbreiten der einzelnen Spalten
        /// </summary>
        /// <param name="graphic">Objekt der Klasse 'Graphics', in das später gezeichnet wird</param>
        public void spaltenBerechnen(Graphics graphic)
        {
            float breite = 0;
            zeilenhoehe = graphic.MeasureString("1", Properties.Settings.Default.Font).Height + Properties.Settings.Default.zeilenExt;

            for (int i = 0; i < spalten; i++)
            {
                spaltenbreiten[i] = graphic.MeasureString(Properties.Settings.Default.einChar + (i + 1).ToString(), Properties.Settings.Default.Font).Width + Properties.Settings.Default.spaltenExt;
                breite += spaltenbreiten[i];
                if(i < anzEinVar)
                    ausgabe += spaltenbreiten[i];
            }

            groesse = new Size((int)breite, (int)(zeilenhoehe * (float)(zeilen+1)));

            spalten_berechnet = true;
        }

        /// <summary>
        /// Zeichnet die Tabelle in ihr Image-Objekt
        /// </summary>
        public void zeichneTabelle()
        {
            grafik = new Bitmap(groesse.Width, groesse.Height);
            Graphics graphics = Graphics.FromImage(grafik);
            graphics.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), groesse)); 

            if (!spalten_berechnet)
                return;

            #region Horizontale Linien zeichnen
            graphics.DrawLine(pen, 0, 0, groesse.Width, 0);
            graphics.DrawLine(bigPen, 0, zeilenhoehe, groesse.Width, zeilenhoehe);
            for (int i = 2; i <= zeilen; i++)
            {
                graphics.DrawLine(pen, 0, i * zeilenhoehe, groesse.Width, i * zeilenhoehe);
            }
            graphics.DrawLine(pen, 0, groesse.Height-1, groesse.Width, groesse.Height-1);
            #endregion

            #region Vertikale Linien zeichnen
            float marker = 0;
            graphics.DrawLine(pen, marker, 0, marker, groesse.Height);
            for (int i = 0; i < (spalten - 1); i++)
            {
                marker += spaltenbreiten[i];
                if (i == (anzEinVar - 1))
                    graphics.DrawLine(bigPen, (int)Math.Round(marker), 0, (int)Math.Round(marker), groesse.Height);
                else
                    graphics.DrawLine(pen, (int)Math.Round(marker), 0, (int)Math.Round(marker), groesse.Height);
            }
            graphics.DrawLine(pen, groesse.Width - 1, 0, groesse.Width - 1, groesse.Height);
            #endregion

            #region Überschriften eintragen
            Size tempSize;
            char tempChar;
            int tempInt;
            marker = 0;
            for (int i = 0; i < spalten; i++)
            {
                if (i < anzEinVar)
                {
                    tempChar = Properties.Settings.Default.einChar;
                    tempInt = 0;
                }
                else
                {
                    tempChar = Properties.Settings.Default.ausChar;
                    tempInt = anzEinVar;
                }
                tempSize = graphics.MeasureString(tempChar + (i + 1 - tempInt).ToString(), Properties.Settings.Default.Font).ToSize();
                graphics.DrawString(tempChar + (i + 1 - tempInt).ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                    (marker + (spaltenbreiten[i] / 2)) - (float)tempSize.Width / 2,
                    zeilenhoehe / 2 - (float)tempSize.Height / 2);
                marker += spaltenbreiten[i];

            }
            #endregion

            #region Werte eintragen
            for (int j = 0; j < zeilen; j++)
            {
                marker = 0;
                for (int i = 0; i < spalten; i++)
                {
                    if (i < anzEinVar)
                    {
                        tempChar = (char)(Convert.ToInt32(Math.Floor((double)(j/pow2(anzEinVar-i-1))))%2);
                        tempChar += (char)48;
                    }
                    else
                    {
                        switch (werte[i - anzEinVar, j])
                        {
                            case 0: tempChar = '0'; break;
                            case 1: tempChar = '1'; break;
                            default: tempChar = '*'; break;
                        }
                    }
                    tempSize = graphics.MeasureString(tempChar.ToString(), Properties.Settings.Default.Font).ToSize();
                    graphics.DrawString(tempChar.ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                        (marker + (spaltenbreiten[i] / 2)) - (float)tempSize.Width / 2,
                        zeilenhoehe / 2 - (float)tempSize.Height / 2 + (j+1) * zeilenhoehe);
                    marker += spaltenbreiten[i];
                }
            }
            #endregion

            // Ressourcen freigeben
            graphics.Dispose();
        }

        /// <summary>
        /// Überprüft, ob der Mausklick eine Änderung eines Wertes bewirkt und ändert diesen gegebenfalls
        /// </summary>
        /// <param name="x">x-Koordinate des Klicks</param>
        /// <param name="y">y-Koordinate des Klicks</param>
        public void mausklick(int x, int y)
        {
            // Überprüfe, ob im Bereich der Ausgabewerte
            if ((float)x < ausgabe || (float)y < zeilenhoehe)
                return;

            float tempX = (float)x - ausgabe;
            float tempY = (float)y - zeilenhoehe;

            // Spalte ermitteln
            x = 0;
            float marker = 0;
            do
            {
                marker += spaltenbreiten[x + anzEinVar];
                x++;
            } while (marker < tempX);
            x--;

            // Zeile ermitteln
            y = Convert.ToInt32(Math.Floor(tempY / zeilenhoehe));

            // Wert ändern
            Char tempChar;
            switch (werte[x, y])
            {
                case 0:
                    werte[x, y] = 1;
                    tempChar = '1'; break;
                case 1: werte[x, y] = -1;
                    tempChar = '*'; break;
                default: werte[x, y] = 0;
                    tempChar = '0'; break;
            }

            // Wert in Graphik aktualisieren
            marker -= spaltenbreiten[x + anzEinVar];
            marker += ausgabe;
            Graphics graphics = Graphics.FromImage(grafik);
            // Alten Wert weiss überdecken
            graphics.FillRectangle(Brushes.White, marker + 1, (y+1) * zeilenhoehe + 1, spaltenbreiten[x + anzEinVar] - 3, zeilenhoehe - 3);
            // Neuen Wert eintragen
            Size tempSize = graphics.MeasureString(tempChar.ToString(), Properties.Settings.Default.Font).ToSize();
            graphics.DrawString(tempChar.ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                (marker + (spaltenbreiten[x+anzEinVar] / 2)) - (float)tempSize.Width / 2,
                zeilenhoehe / 2 - (float)tempSize.Height / 2 + (y + 1) * zeilenhoehe);

            // Resourcen freigeben
            graphics.Dispose();
        }

        /// <summary>
        /// Setzt für eine Ausgabevariable alle Werte auf einen Wert
        /// </summary>
        /// <param name="ausgabeVar">Index der Ausgabevariablen</param>
        /// <param name="wert">Wert für die Variable, wobei Dont-Care = -1 ist</param>
        public void setzeWerte(int ausgabeVar, int wert)
        {
            float marker = ausgabe;
            Graphics graphics = Graphics.FromImage(grafik);
            // Marker auf die richtige Spalte setzen
            for (int i = 0; i < ausgabeVar; i++)
            {
                marker += spaltenbreiten[anzEinVar + i];
            }
            // Zeichen ermitteln
            Char tempChar;
            switch (wert)
            {
                case 0: tempChar = '0'; break;
                case 1: tempChar = '1'; break;
                default: tempChar = '*'; break;
            }
            for (int i = 0; i < zeilen; i++ )
            {
                // Wert ändern
                werte[ausgabeVar, i] = wert;
                // Alten Wert weiss überdecken
                graphics.FillRectangle(Brushes.White, marker + 1, (i + 1) * zeilenhoehe + 1, spaltenbreiten[ausgabeVar + anzEinVar] - 3, zeilenhoehe - 3);
                // Neuen Wert eintragen
                Size tempSize = graphics.MeasureString(tempChar.ToString(), Properties.Settings.Default.Font).ToSize();
                graphics.DrawString(tempChar.ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                    (marker + (spaltenbreiten[ausgabeVar + anzEinVar] / 2)) - (float)tempSize.Width / 2,
                    zeilenhoehe / 2 - (float)tempSize.Height / 2 + (i + 1) * zeilenhoehe);
            }

            // Resourcen freigeben
            graphics.Dispose();
        }

        public Term[] gibMinterme(int ausgabeVar)
        {
            ArrayList minterme = new ArrayList();
            int[] input = new int[anzEinVar];
            for (int i = 0; i < zeilen; i++)
            {
                if (werte[ausgabeVar, i] != 0)
                {
                    for (int j = 0; j < anzEinVar; j++)
                    {
                        input[j] = Convert.ToInt32(Math.Floor((double)(i / pow2(anzEinVar - j - 1)))) % 2;
                    }
                    minterme.Add(new Term(input, werte[ausgabeVar, i] != 1));
                }
            }

            return (Term[])minterme.ToArray(typeof(Term));
        }
    }
}
