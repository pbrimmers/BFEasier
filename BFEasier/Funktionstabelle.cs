namespace BFEasier
{
    using System;
    using System.Collections;
    using System.Drawing;

    /// <summary>
    /// Klasse zur Verwaltung der Daten der Funktionstabelle
    /// und zur Erstellung der graphischen Oberfläche
    /// </summary>
    public class Funktionstabelle
    {
        private static readonly Pen pen = new Pen(Properties.Settings.Default.Color, 1);
        private static readonly Pen bigPen = new Pen(Properties.Settings.Default.Color, 2);

        private readonly Int32 zeilen, spalten;
        /// <summary>
        /// Array das die Werte der Ausgabevariablen speichert.
        /// Der erste Index ist die Ausgabevariable, der zweite die Zeile
        /// </summary>
        private readonly Int32[,] werte;
        private readonly Single[] spaltenbreiten;
        private Single zeilenhoehe;
        /// <summary>
        /// Grenze zwischen Ein- und Ausgabevariablen auf der grafischen Ausgabe
        /// </summary>
        private Single ausgabe;
        private Boolean spalten_berechnet;
        private Size groesse;

        public Bitmap Grafik { get; private set; }

        /// <summary>
        /// Objekt der Klasse 'Size' mit der Grösse der Funktionstabelle auf einem 'Graphics'-Objekt
        /// </summary>
        public Size Groesse => groesse;

        public Int32 AnzahlAusgabevariablen { get; }

        public Int32 AnzahlEingabevariablen { get; }

        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Erstellt eine Funktionstabelle mit allen Ausgangswerten gleich '0'
        /// </summary>
        /// <param name="ein">Anzahl der Eingangsgrößen</param>
        /// <param name="aus">Anzahl der Ausgangsgrößen</param>
        public Funktionstabelle(Int32 ein, Int32 aus)
        {
            AnzahlEingabevariablen = ein;
            AnzahlAusgabevariablen = aus;
            spalten = ein + aus;
            zeilen = Pow2(ein);
            spaltenbreiten = new Single[spalten];
            spalten_berechnet = false;
            werte = new Int32[aus, zeilen];
            Grafik = new Bitmap(2, 2);
        }

        /// <summary>
        /// Berechnet die Potenzfunktion zu Basis 2
        /// </summary>
        /// <param name="e">Exponent vom Typ 'int'</param>
        /// <returns>Ergebnis vom Typ 'int'</returns>
        private Int32 Pow2(Int32 e)
        {
            Int32 temp = 1;
            for (Int32 i = 0; i < e; i++)
            {
                temp *= 2;
            }
            return temp;
        }

        /// <summary>
        /// Berechnet die Spaltenbreiten der einzelnen Spalten
        /// </summary>
        /// <param name="graphic">Objekt der Klasse 'Graphics', in das später gezeichnet wird</param>
        public void SpaltenBerechnen(Graphics graphic)
        {
            Single breite = 0;
            zeilenhoehe = graphic.MeasureString("1", Properties.Settings.Default.Font).Height + Properties.Settings.Default.zeilenExt;

            for (Int32 i = 0; i < spalten; i++)
            {
                spaltenbreiten[i] = graphic.MeasureString(Properties.Settings.Default.einChar + (i + 1).ToString(), Properties.Settings.Default.Font).Width + Properties.Settings.Default.spaltenExt;
                breite += spaltenbreiten[i];
                if (i < AnzahlEingabevariablen)
                {
                    ausgabe += spaltenbreiten[i];
                }
            }

            groesse = new Size((Int32)breite, (Int32)(zeilenhoehe * (zeilen + 1)));

            spalten_berechnet = true;
        }

        /// <summary>
        /// Zeichnet die Tabelle in ihr Image-Objekt
        /// </summary>
        public void ZeichneTabelle()
        {
            Grafik = new Bitmap(groesse.Width, groesse.Height);
            var graphics = Graphics.FromImage(Grafik);
            graphics.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), groesse));

            if (!spalten_berechnet)
            {
                return;
            }

            #region Horizontale Linien zeichnen
            graphics.DrawLine(pen, 0, 0, groesse.Width, 0);
            graphics.DrawLine(bigPen, 0, zeilenhoehe, groesse.Width, zeilenhoehe);
            for (Int32 i = 2; i <= zeilen; i++)
            {
                graphics.DrawLine(pen, 0, i * zeilenhoehe, groesse.Width, i * zeilenhoehe);
            }
            graphics.DrawLine(pen, 0, groesse.Height - 1, groesse.Width, groesse.Height - 1);
            #endregion

            #region Vertikale Linien zeichnen
            Single marker = 0;
            graphics.DrawLine(pen, marker, 0, marker, groesse.Height);
            for (Int32 i = 0; i < (spalten - 1); i++)
            {
                marker += spaltenbreiten[i];
                if (i == (AnzahlEingabevariablen - 1))
                {
                    graphics.DrawLine(bigPen, (Int32)Math.Round(marker), 0, (Int32)Math.Round(marker), groesse.Height);
                }
                else
                {
                    graphics.DrawLine(pen, (Int32)Math.Round(marker), 0, (Int32)Math.Round(marker), groesse.Height);
                }
            }
            graphics.DrawLine(pen, groesse.Width - 1, 0, groesse.Width - 1, groesse.Height);
            #endregion

            #region Überschriften eintragen
            Size tempSize;
            Char tempChar;
            Int32 tempInt;
            marker = 0;
            for (Int32 i = 0; i < spalten; i++)
            {
                if (i < AnzahlEingabevariablen)
                {
                    tempChar = Properties.Settings.Default.einChar;
                    tempInt = 0;
                }
                else
                {
                    tempChar = Properties.Settings.Default.ausChar;
                    tempInt = AnzahlEingabevariablen;
                }
                tempSize = graphics.MeasureString(tempChar + (i + 1 - tempInt).ToString(), Properties.Settings.Default.Font).ToSize();
                graphics.DrawString(tempChar + (i + 1 - tempInt).ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                    marker + (spaltenbreiten[i] / 2) - ((Single)tempSize.Width / 2),
                    (zeilenhoehe / 2) - ((Single)tempSize.Height / 2));
                marker += spaltenbreiten[i];

            }
            #endregion

            #region Werte eintragen
            for (Int32 j = 0; j < zeilen; j++)
            {
                marker = 0;
                for (Int32 i = 0; i < spalten; i++)
                {
                    if (i < AnzahlEingabevariablen)
                    {
                        tempChar = (Char)(Convert.ToInt32(Math.Floor((Double)(j / Pow2(AnzahlEingabevariablen - i - 1)))) % 2);
                        tempChar += (Char)48;
                    }
                    else
                    {
                        switch (werte[i - AnzahlEingabevariablen, j])
                        {
                            case 0: tempChar = '0'; break;
                            case 1: tempChar = '1'; break;
                            default: tempChar = '*'; break;
                        }
                    }
                    tempSize = graphics.MeasureString(tempChar.ToString(), Properties.Settings.Default.Font).ToSize();
                    graphics.DrawString(tempChar.ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                        marker + (spaltenbreiten[i] / 2) - ((Single)tempSize.Width / 2),
                        (zeilenhoehe / 2) - ((Single)tempSize.Height / 2) + ((j + 1) * zeilenhoehe));
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
        public void Mausklick(Int32 x, Int32 y)
        {
            // Überprüfe, ob im Bereich der Ausgabewerte
            if (x < ausgabe || y < zeilenhoehe)
            {
                return;
            }

            Single tempX = x - ausgabe;
            Single tempY = y - zeilenhoehe;

            // Spalte ermitteln
            x = 0;
            Single marker = 0;
            do
            {
                marker += spaltenbreiten[x + AnzahlEingabevariablen];
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
                case 1:
                    werte[x, y] = -1;
                    tempChar = '*'; break;
                default:
                    werte[x, y] = 0;
                    tempChar = '0'; break;
            }

            // Wert in Graphik aktualisieren
            marker -= spaltenbreiten[x + AnzahlEingabevariablen];
            marker += ausgabe;
            var graphics = Graphics.FromImage(Grafik);
            // Alten Wert weiss überdecken
            graphics.FillRectangle(Brushes.White, marker + 1, ((y + 1) * zeilenhoehe) + 1, spaltenbreiten[x + AnzahlEingabevariablen] - 3, zeilenhoehe - 3);
            // Neuen Wert eintragen
            var tempSize = graphics.MeasureString(tempChar.ToString(), Properties.Settings.Default.Font).ToSize();
            graphics.DrawString(tempChar.ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                marker + (spaltenbreiten[x + AnzahlEingabevariablen] / 2) - ((Single)tempSize.Width / 2),
                (zeilenhoehe / 2) - ((Single)tempSize.Height / 2) + ((y + 1) * zeilenhoehe));

            // Resourcen freigeben
            graphics.Dispose();
        }

        /// <summary>
        /// Setzt für eine Ausgabevariable alle Werte auf einen Wert
        /// </summary>
        /// <param name="ausgabeVar">Index der Ausgabevariablen</param>
        /// <param name="wert">Wert für die Variable, wobei Dont-Care = -1 ist</param>
        public void SetzeWerte(Int32 ausgabeVar, Int32 wert)
        {
            Single marker = ausgabe;
            var graphics = Graphics.FromImage(Grafik);
            // Marker auf die richtige Spalte setzen
            for (Int32 i = 0; i < ausgabeVar; i++)
            {
                marker += spaltenbreiten[AnzahlEingabevariablen + i];
            }
            // Zeichen ermitteln
            Char tempChar;
            switch (wert)
            {
                case 0: tempChar = '0'; break;
                case 1: tempChar = '1'; break;
                default: tempChar = '*'; break;
            }
            for (Int32 i = 0; i < zeilen; i++)
            {
                // Wert ändern
                werte[ausgabeVar, i] = wert;
                // Alten Wert weiss überdecken
                graphics.FillRectangle(Brushes.White, marker + 1, ((i + 1) * zeilenhoehe) + 1, spaltenbreiten[ausgabeVar + AnzahlEingabevariablen] - 3, zeilenhoehe - 3);
                // Neuen Wert eintragen
                var tempSize = graphics.MeasureString(tempChar.ToString(), Properties.Settings.Default.Font).ToSize();
                graphics.DrawString(tempChar.ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                    marker + (spaltenbreiten[ausgabeVar + AnzahlEingabevariablen] / 2) - ((Single)tempSize.Width / 2),
                    (zeilenhoehe / 2) - ((Single)tempSize.Height / 2) + ((i + 1) * zeilenhoehe));
            }

            // Resourcen freigeben
            graphics.Dispose();
        }

        public Term[] GibMinterme(Int32 ausgabeVar)
        {
            var minterme = new ArrayList();
            Int32[] input = new Int32[AnzahlEingabevariablen];
            for (Int32 i = 0; i < zeilen; i++)
            {
                if (werte[ausgabeVar, i] != 0)
                {
                    for (Int32 j = 0; j < AnzahlEingabevariablen; j++)
                    {
                        input[j] = Convert.ToInt32(Math.Floor((Double)(i / Pow2(AnzahlEingabevariablen - j - 1)))) % 2;
                    }
                    minterme.Add(new Term(input, werte[ausgabeVar, i] != 1));
                }
            }

            return (Term[])minterme.ToArray(typeof(Term));
        }
    }
}
