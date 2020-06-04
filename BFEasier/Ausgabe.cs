﻿
namespace BFEasier
{
    using System;
    using System.Collections;
    using System.Drawing;

    internal class Ausgabe
    {
        private readonly ArrayList[] data;
        private readonly ArrayList terme;

        /// <summary>
        /// Bitmap mit der akutellen Ausgabe
        /// </summary>
        public Bitmap Grafik { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="list">ArrayList-Array mit allen Schritten der Vereinfachung aller Variablen</param>
        /// <param name="vereinfachteTerme">ArrayList mit den vereinfachten Termen</param>
        public Ausgabe(ArrayList[] list, ArrayList vereinfachteTerme)
        {
            data = new ArrayList[list.Length];
            for (Int32 i = 0; i < list.Length; i++)
            {
                data[i] = new ArrayList(list[i]);
            }

            terme = new ArrayList(vereinfachteTerme);
            Grafik = new Bitmap(20, 20);
        }

        /// <summary>
        /// Zeichnet alle vereinfachten Terme in das Bitmap-Objekt
        /// </summary>
        public void DrawAll()
        {
            berechneBildgroesse(out Graphics graphics, out Single width, out Single height, out Single abstand_links);

            Grafik = new Bitmap((Int32)Math.Ceiling(width), (Int32)Math.Ceiling(height));
            graphics = Graphics.FromImage(Grafik);
            graphics.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), Grafik.Size));
            Single marker_y = 0;
            Single zeilenhoehe = height / terme.Count;
            SizeF tempSize;
            // Jede Ausgabegröße einzeichenen
            for (Int32 i = 0; i < terme.Count; i++)
            {
                Single marker_x = Properties.Settings.Default.spaltenExt / 2;
                tempSize = graphics.MeasureString(Properties.Settings.Default.ausChar + (i + 1).ToString(), Properties.Settings.Default.Font);
                graphics.DrawString(Properties.Settings.Default.ausChar + (i + 1).ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color), marker_x, marker_y + (zeilenhoehe / 2) - (tempSize.Height / 2));
                marker_x += abstand_links;
                graphics.DrawString("=", Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color), marker_x, marker_y + (zeilenhoehe / 2) - (tempSize.Height / 2));
                marker_x += graphics.MeasureString("=", Properties.Settings.Default.Font).Width;
                // Alle Terme hinzufügen
                for (Int32 j = 0; j < ((Term[])terme[i]).Length; j++)
                {
                    DrawTerm(graphics, ((Term[])terme[i])[j], marker_x, marker_y + (zeilenhoehe / 2) - (tempSize.Height / 2), true, false);
                    tempSize = MeasureTerm(graphics, ((Term[])terme[i])[j], false);
                    marker_x += tempSize.Width;
                    // Außer bei dem letzten Term ein '+' anfügen
                    if (j < ((Term[])terme[i]).Length - 1)
                    {
                        graphics.DrawString(" +", Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color), marker_x, marker_y + (zeilenhoehe / 2) - (tempSize.Height / 2));
                        marker_x += graphics.MeasureString(" +", Properties.Settings.Default.Font).Width;
                    }
                }
                marker_y += zeilenhoehe;
            }
        }

        /// <summary>
        /// Berechnet die Größe des Bitmaps mit allen vereinfachten Termen
        /// </summary>
        /// <param name="graphics">Graphics-Objekt zum Schreiben auf 'bild'</param>
        /// <param name="width">Breite des Bildes</param>
        /// <param name="height">Höhe des Bildes</param>
        /// <param name="abstand_links">Abstand vom linken Bildrand bis zu den '='</param>
        private void berechneBildgroesse(out Graphics graphics, out Single width, out Single height, out Single abstand_links)
        {
            graphics = Graphics.FromImage(Grafik);
            width = 0;
            Single temp_width = 0;
            height = 0;
            abstand_links = 0;

            #region Breite berechnen
            for (Int32 i = 0; i < terme.Count; i++)
            {
                if (abstand_links < graphics.MeasureString(Properties.Settings.Default.einChar + (i + 1).ToString(), Properties.Settings.Default.Font).Width)
                {
                    abstand_links = graphics.MeasureString(Properties.Settings.Default.einChar + (i + 1).ToString(), Properties.Settings.Default.Font).Width;
                }

                temp_width = graphics.MeasureString("=", Properties.Settings.Default.Font).Width + Properties.Settings.Default.spaltenExt;
                foreach (Term term in (Term[])terme[i])
                {
                    temp_width += MeasureTerm(graphics, term, false).Width;
                }
                temp_width += (((Term[])terme[i]).Length - 1) * graphics.MeasureString(" +", Properties.Settings.Default.Font).Width;

                if (width < temp_width)
                {
                    width = temp_width;
                }
            }
            width += abstand_links;
            #endregion

            // Höhe berechnen
            height = terme.Count * (graphics.MeasureString(Properties.Settings.Default.einChar + "1", Properties.Settings.Default.Font).Height + Properties.Settings.Default.zeilenExt);
        }

        /// <summary>
        /// Zeichnet die Schritte zur Vereinfachung eines Ausgabegrößen
        /// </summary>
        /// <param name="index">Index der darzustellenden Ausgabegröße</param>
        public void DrawOne(Int32 index)
        {
            var graphics = Graphics.FromImage(Grafik);
            ArrayList terme = data[index];
            Int32[] maxAnzahlProGrad = getMaxAnzahlGrade(terme, out Boolean termeVorhanden);
            Single[] spaltenbreiten = spaltenbreitenBerechnen(graphics, terme, maxAnzahlProGrad, out Single width);
            Single zeichenhoehe = graphics.MeasureString(Properties.Settings.Default.einChar + "1", Properties.Settings.Default.Font).Width;
            Single temp_width = getBreite(graphics, (Term[])this.terme[index], index);
            Single height = berechneBildhoehe(maxAnzahlProGrad, zeichenhoehe, termeVorhanden);
            if (width < temp_width)
            {
                width = temp_width;
            }

            Grafik = new Bitmap((Int32)Math.Ceiling(width), (Int32)Math.Ceiling(height));
            graphics = Graphics.FromImage(Grafik);
            graphics.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), Grafik.Size));

            Single marker_x = Properties.Settings.Default.spaltenExt / 2;
            Single marker_y = Properties.Settings.Default.zeilenExt / 2;

            #region vereinfachten Term einzeichnen
            graphics.DrawString(Properties.Settings.Default.ausChar + (index + 1).ToString() + " =",
                Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color),
                marker_x,
                marker_y);
            marker_x += graphics.MeasureString(Properties.Settings.Default.ausChar + (index + 1).ToString() + " =", Properties.Settings.Default.Font).Width;
            for (Int32 i = 0; i < ((Term[])this.terme[index]).Length; i++)
            {
                DrawTerm(graphics, ((Term[])this.terme[index])[i], marker_x, marker_y, true, false);
                marker_x += MeasureTerm(graphics, ((Term[])this.terme[index])[i], false).Width;
                if (i + 1 < ((Term[])this.terme[index]).Length)
                {
                    graphics.DrawString(" +", Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color), marker_x, marker_y);
                    marker_x += graphics.MeasureString(" +", Properties.Settings.Default.Font).Width;
                }
            }
            #endregion

            marker_y += zeichenhoehe + Properties.Settings.Default.zeilenExt;
            if (termeVorhanden)
            {
                #region Überschriften
                marker_x = spaltenbreiten[0] + (Properties.Settings.Default.spaltenExt / 2);
                graphics.DrawString("Minterme", Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color), marker_x, marker_y);
                marker_x += spaltenbreiten[1];
                for (Int32 i = 2; i < spaltenbreiten.Length; i++)
                {
                    graphics.DrawString((i - 1).ToString() + ". Schritt", Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color), marker_x, marker_y);
                    marker_x += spaltenbreiten[i];
                }
                #endregion

                marker_y += zeichenhoehe + Properties.Settings.Default.zeilenExt;
                marker_x = Properties.Settings.Default.spaltenExt * 1 / 2;

                #region erste Spalte
                for (Int32 i = 0; i < maxAnzahlProGrad.Length; i++)
                {
                    if (maxAnzahlProGrad[i] != 0)
                    {
                        graphics.DrawString("K" + i.ToString(), Properties.Settings.Default.Font, new SolidBrush(Properties.Settings.Default.Color), (Int32)marker_x, marker_y);
                        marker_y += (maxAnzahlProGrad[i] * zeichenhoehe) + Properties.Settings.Default.zeilenExt;
                    }
                }
                #endregion

                #region vertikale Linien
                Single vert = spaltenbreiten[0];
                for (Int32 i = 0; i < spaltenbreiten.Length - 1; i++)
                {
                    graphics.DrawLine(new Pen(Properties.Settings.Default.Color, 1), vert, zeichenhoehe + Properties.Settings.Default.zeilenExt, vert, height);
                    vert += spaltenbreiten[i + 1];
                }
                #endregion

                #region horizontale Linien
                Single horz = (2 * zeichenhoehe) + Properties.Settings.Default.zeilenExt;
                foreach (Int32 i in maxAnzahlProGrad)
                {
                    if (i != 0)
                    {
                        graphics.DrawLine(new Pen(Properties.Settings.Default.Color, 1), 0, horz, width, horz);
                        horz += Properties.Settings.Default.zeilenExt + (i * zeichenhoehe);
                    }
                }
                #endregion

                #region restlicheSpalten
                for (Int32 j = 0; j < terme.Count; j++)
                {
                    marker_y = (Properties.Settings.Default.zeilenExt / 2) + (2 * (zeichenhoehe + Properties.Settings.Default.zeilenExt));
                    marker_x += spaltenbreiten[j];

                    for (Int32 i = 0; i < ((ArrayList[])terme[j]).Length; i++)
                    {
                        if (maxAnzahlProGrad[i] != 0)
                        {
                            foreach (Term term in ((ArrayList[])terme[j])[i])
                            {
                                DrawTerm(graphics, term, marker_x, marker_y, false, true);
                                marker_y += zeichenhoehe;
                            }
                            marker_y += ((maxAnzahlProGrad[i] - ((ArrayList[])terme[j])[i].Count) * zeichenhoehe) + Properties.Settings.Default.zeilenExt;
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// Berechnet die Höhe des zu zeichnenden Bildes
        /// </summary>
        /// <param name="maxAnzahlProGrad"></param>
        /// <param name="zeichenhoehe"></param>
        /// <param name="termeVorhanden"></param>
        /// <returns></returns>
        private Single berechneBildhoehe(Int32[] maxAnzahlProGrad, Single zeichenhoehe, Boolean termeVorhanden)
        {
            Single height = zeichenhoehe + Properties.Settings.Default.zeilenExt;
            if (termeVorhanden)
            {
                foreach (Int32 i in maxAnzahlProGrad)
                {
                    // Nur Spalten berücksichtigen in denen auch Terme stehen
                    if (i != 0)
                    {
                        height += Properties.Settings.Default.zeilenExt + (i * zeichenhoehe);
                    }
                }
                // Die Spaltenüberschriften berücksichtigen
                height += zeichenhoehe + Properties.Settings.Default.zeilenExt;
            }
            return height;
        }

        /// <summary>
        /// Berechnet die Breite des Ausdrucks der Vereinfachten Terme
        /// </summary>
        /// <param name="graphics">Graphics-Objekt in das gezeichnet wird</param>
        /// <param name="terme">Term-Array des vereinfachten Ausdrucks</param>
        /// <param name="index">Index der Ausgabegröße</param>
        /// <returns>Breite des Ausdrucks der Vereinfachten Terme</returns>
        private Single getBreite(Graphics graphics, Term[] terme, Int32 index)
        {
            Single width = graphics.MeasureString(Properties.Settings.Default.ausChar + (index + 1) + " =", Properties.Settings.Default.Font).Width;

            foreach (Term term in terme)
            {
                // Jeden Term berücksichtigen
                width += MeasureTerm(graphics, term, false).Width;
            }
            width += (terme.Length - 1) * graphics.MeasureString(" +", Properties.Settings.Default.Font).Width;
            width += Properties.Settings.Default.spaltenExt;

            return width;
        }

        /// <summary>
        /// Berechnet wie viele Terme maximal in einer Zeile stehen
        /// </summary>
        /// <param name="terme">Die Struktur mit allen Termen der Ausgabevariable</param>
        /// <param name="termeVorhanden">Speichert, ob überhaupt Terme vereinfacht wurden</param>
        /// <returns></returns>
        private Int32[] getMaxAnzahlGrade(ArrayList terme, out Boolean termeVorhanden)
        {
            termeVorhanden = terme.Count != 0;

            if (!termeVorhanden)
            {
                return new Int32[0];
            }

            Int32[] maxGrade = new Int32[((ArrayList[])terme[0]).Length];

            foreach (ArrayList[] listing in terme)
            {
                for (Int32 i = 0; i < listing.Length; i++)
                {
                    if (maxGrade[i] < listing[i].Count)
                    {
                        maxGrade[i] = listing[i].Count;
                    }
                }
            }
            return maxGrade;
        }

        /// <summary>
        /// Berechnet die Breite aller Spalten
        /// </summary>
        /// <param name="graphics">Graphics-Objekt in das gezeichnet wird</param>
        /// <param name="terme">Die Struktur mit allen Termen der Ausgabevariable</param>
        /// <param name="maxAnzahlProGrad">Int-Array mit der Anzahl der Terme pro Zeile</param>
        /// <param name="width">Die Gesamtbreite wird hier gespeichert</param>
        /// <returns>Float-Array mit den Breiten aller Spalten</returns>
        private Single[] spaltenbreitenBerechnen(Graphics graphics, ArrayList terme, Int32[] maxAnzahlProGrad, out Single width)
        {
            width = 0;
            Single[] spaltenbreiten = new Single[terme.Count + 1];
            Single temp_width;

            #region Erste Spalte
            foreach (Int32 i in maxAnzahlProGrad)
            {
                if (i != 0)
                {
                    temp_width = graphics.MeasureString("K" + (i + 1).ToString(), Properties.Settings.Default.Font).Width;
                    if (spaltenbreiten[0] < temp_width)
                    {
                        width += temp_width - spaltenbreiten[0];
                        spaltenbreiten[0] = temp_width;
                    }
                }
            }
            #endregion

            for (Int32 i = 1; i <= terme.Count; i++)
            {
                #region Spaltenüberschrift berücksichtigen
                if (i == 1)
                {
                    spaltenbreiten[i] = graphics.MeasureString("Minterme", Properties.Settings.Default.Font).Width + Properties.Settings.Default.spaltenExt;
                }
                else
                {
                    spaltenbreiten[i] = graphics.MeasureString((i - 1).ToString() + ". Schritt", Properties.Settings.Default.Font).Width + Properties.Settings.Default.spaltenExt;
                }

                width += spaltenbreiten[i];
                #endregion

                #region Terme der Spalte berücksichtigen
                foreach (ArrayList list in (ArrayList[])terme[i - 1])
                {
                    foreach (Term term in list)
                    {
                        temp_width = MeasureTerm(graphics, term, true).Width + Properties.Settings.Default.spaltenExt;
                        if (temp_width > spaltenbreiten[i])
                        {
                            width += temp_width - spaltenbreiten[i];
                            spaltenbreiten[i] = temp_width;
                        }
                    }
                }
                #endregion
            }
            return spaltenbreiten;
        }

        /// <summary>
        /// Berechnet die Größe die ein Term beim zeichnen in ein Graphics-Objekt benötigt
        /// </summary>
        /// <param name="graphics">Graphics-Objekt in das gezeichnet werden soll</param>
        /// <param name="term">Term der gezeichnet werden soll</param>
        /// <param name="minterme">Sollen die Minterme mitgeschreiben werden</param>
        /// <returns>SizeF-Objekt mit der Größe des Terms</returns>
        private SizeF MeasureTerm(Graphics graphics, Term term, Boolean minterme)
        {
            try
            {
                // Erzeugt einen Fehler, falls es sich nicht um eine 1 oder 0 handelt
                return graphics.MeasureString(Convert.ToInt32(term.ToString()).ToString(), Properties.Settings.Default.Font);
            }
            catch
            {
                Single width = 0;
                for (Int32 i = 0; i < term.Laenge; i++)
                {
                    if (term[i] >= 0)
                    {
                        width += graphics.MeasureString(Properties.Settings.Default.einChar + (i + 1).ToString(), Properties.Settings.Default.Font).Width - 5;
                    }
                }
                Single height = graphics.MeasureString(Properties.Settings.Default.einChar + "1", Properties.Settings.Default.Font).Height;

                if (minterme)
                {
                    String tempString = " (";
                    Boolean bindestrich;
                    for (Int32 i = 0; i < term.Minterme.Length; i++)
                    {
                        tempString += term.Minterme[i].ToString();
                        bindestrich = false;
                        while (i + 1 < term.Minterme.Length && term.Minterme[i + 1] == term.Minterme[i] + 1)
                        {
                            i++;
                            bindestrich = true;
                        }
                        if (bindestrich)
                        {
                            tempString += "-" + term.Minterme[i].ToString();
                        }
                        tempString += ",";
                    }
                    tempString = tempString.Substring(0, tempString.Length - 1) + ")";
                    width += graphics.MeasureString(tempString, Properties.Settings.Default.Font).Width;
                }

                return new SizeF(width, height);
            }
        }

        /// <summary>
        /// Zeichnet einen Term in ein Graphics-Objekt
        /// </summary>
        /// <param name="graphics">Graphics-Objekt in das gezeichnet werden soll</param>
        /// <param name="term">Term der gezeichnet werden soll</param>
        /// <param name="x">x-Position des Terms</param>
        /// <param name="y">y-Position des Terms</param>
        /// <param name="normalColor">Sollen alle Terme die gleiche Farbe haben</param>
        /// <param name="minterme">Sollen die Minterme mitgeschreiben werden</param>
        private void DrawTerm(Graphics graphics, Term term, Single x, Single y, Boolean normalColor, Boolean minterme)
        {
            Color color = Properties.Settings.Default.Color;
            if (term.Ist_Primimplikant && !normalColor)
            {
                color = Properties.Settings.Default.ColorPrim;
            }

            if (term.DontCare && !normalColor)
            {
                color = Properties.Settings.Default.ColorDontCare;
            }

            try
            {
                // Erzeugt einen Fehler, falls es sich nicht um eine 1 oder 0 handelt
                graphics.DrawString(Convert.ToInt32(term.ToString()).ToString(), Properties.Settings.Default.Font, new SolidBrush(color), x, y);
                #region Minterme zeichnen
                if (Convert.ToInt32(term.ToString()) == 1 && minterme)
                {
                    String tempString = " (";
                    Boolean bindestrich;
                    for (Int32 i = 0; i < term.Minterme.Length; i++)
                    {
                        tempString += term.Minterme[i].ToString();
                        bindestrich = false;
                        Int32 zuletztGeschrieben = term.Minterme[i];
                        while (i + 1 < term.Minterme.Length && term.Minterme[i + 1] == term.Minterme[i] + 1)
                        {
                            i++;
                            bindestrich = true;
                        }
                        if (bindestrich)
                        {
                            if (zuletztGeschrieben != term.Minterme[i] - 1)
                            {
                                tempString += "-" + term.Minterme[i].ToString();
                            }
                            else
                            {
                                tempString += "," + term.Minterme[i].ToString();
                            }
                        }
                        tempString += ",";
                    }
                    tempString = tempString.Substring(0, tempString.Length - 1) + ")";
                    graphics.DrawString(tempString, Properties.Settings.Default.Font, new SolidBrush(color), x + graphics.MeasureString("1", Properties.Settings.Default.Font).Width, y);
                }
                #endregion
            }
            catch
            {
                Single marker = 0;
                SizeF tempSize;
                for (Int32 i = 0; i < term.Laenge; i++)
                {
                    if (term[i] >= 0)
                    {
                        graphics.DrawString(Properties.Settings.Default.einChar + (i + 1).ToString(), Properties.Settings.Default.Font, new SolidBrush(color), x + marker, y);
                        tempSize = graphics.MeasureString(Properties.Settings.Default.einChar + (i + 1).ToString(), Properties.Settings.Default.Font);
                        // Ggf Negationsstrich hinzufügen
                        if (term[i] == 0)
                        {
                            graphics.DrawLine(new Pen(color, 1), x + marker + 3, y, x + marker + tempSize.Width - 5, y);
                        }

                        marker += tempSize.Width - 5;
                    }
                }
                #region Minterme zeichnen
                if (minterme)
                {
                    String tempString = " (";
                    Boolean bindestrich;
                    for (Int32 i = 0; i < term.Minterme.Length; i++)
                    {
                        tempString += term.Minterme[i].ToString();
                        bindestrich = false;
                        Int32 zuletztGeschrieben = term.Minterme[i];
                        while (i + 1 < term.Minterme.Length && term.Minterme[i + 1] == term.Minterme[i] + 1)
                        {
                            i++;
                            bindestrich = true;
                        }
                        if (bindestrich)
                        {
                            if (zuletztGeschrieben != term.Minterme[i] - 1)
                            {
                                tempString += "-" + term.Minterme[i].ToString();
                            }
                            else
                            {
                                tempString += "," + term.Minterme[i].ToString();
                            }
                        }
                        tempString += ",";
                    }
                    tempString = tempString.Substring(0, tempString.Length - 1) + ")";
                    graphics.DrawString(tempString, Properties.Settings.Default.Font, new SolidBrush(color), x + marker, y);
                }
                #endregion
            }
        }
    }
}
