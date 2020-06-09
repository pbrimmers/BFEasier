namespace BFEasier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QuineMcCluskey
    {
        // List of lists to sort terms by order
        private readonly List<List<Term>> grade;
        // Anzahl der Minterme in grade
        private readonly Int32 anzMins;
        // Speichert, ob das Objekt Terme hat
        private Boolean hat_Terme;

        /// <summary>
        /// Konstruktor für die rekursiven Aufrufe zur Berechnung der Primimplikanten
        /// </summary>
        /// <param name="grade">Anzahl der möglichen Grade der Terme</param>
        public QuineMcCluskey(Int32 grade)
        {
            this.grade = new List<List<Term>>();
            for (var i = 0; i < grade; i++)
            {
                this.grade.Add(new List<Term>());
            }
            anzMins = 0;
        }

        /// <summary>
        /// Konstruktor für das erste Objekt mit den Mintermen, die vereinfacht werden sollen
        /// </summary>
        /// <param name="minterme">Term-Array mit den Mintermen</param>
        public QuineMcCluskey(Term[] minterme)
        {
            anzMins = minterme.Length;
            if (minterme.Length > 0)
            {
                hat_Terme = true;
                grade = new List<List<Term>>();
                for (var i = 0; i < minterme[0].Laenge + 1; i++)
                {
                    grade.Add(new List<Term>());
                }
                for (var i = 0; i < minterme.Length; i++)
                {
                    grade[minterme[i].Grad].Add(minterme[i]);
                }
            }
            else
            {
                grade = null;
            }
        }

        /// <summary>
        /// Fügt dem Objekt einen Term hinzu, falls dieser noch nicht enthalten ist
        /// und sortiert ihn entsprechend des Grades ein
        /// </summary>
        /// <param name="term">Objekt der Klasse Term, dass hinzugefügt werden soll</param>
        public void TermHinzufuegen(Term term)
        {
            // Noch nicht enthalten?
            if (!term.IsIn(grade[term.Grad]))
            {
                grade[term.Grad].Add(term);
            }

            hat_Terme = true;
        }

        /// <summary>
        /// Vereinfacht die Minterme
        /// </summary>
        /// <returns>Term-Array mit den vereinfachten Termen</returns>
        public Term[] Vereinfache()
        {
            // Falls keine Minterme vorhanden ist der Term immer 0
            // Tritt ein, wenn alle Ausgabewerte 0 sind
            if (grade == null)
            {
                Term[] nullterm = { Term.Nullterm() };
                return nullterm;
            }

            // Primimplikanten ermitteln
            var primimplikanten = BerechnePrimimplikanten();

            // Falls keine Primimplikaten vorhanden sind ist der Term immer 0
            // Tritt ein, wenn Dont-Care-Terme vorhanden sind und alle anderen Ausgabewerte 0
            if (primimplikanten.Length == 0)
            {
                Term[] nullterm = { Term.Nullterm() };
                return nullterm;
            }

            return TermeAuswaehlen(primimplikanten);

        }

        /// <summary>
        /// Vereinfacht die Minterme und speichert die einzelnen Schritte
        /// </summary>
        /// <param name="einzelSchritte">List, in der die Schritte gespeichert werden</param>
        /// <returns>Term-Array mit den vereinfachten Termen</returns>
        public Term[] Vereinfache(ref List<List<Term[]>> einzelSchritte)
        {
            // Falls keine Minterme vorhanden ist der Term immer 0
            // Tritt ein, wenn alle Ausgabewerte 0 sind
            if (grade == null)
            {
                return new Term[] { Term.Nullterm() };
            }

            // Primimplikanten ermitteln
            var primimplikanten = BerechnePrimimplikanten(ref einzelSchritte);

            // Falls keine Primimplikaten vorhanden sind ist der Term immer 0
            // Tritt ein, wenn Dont-Care-Terme vorhanden sind und alle anderen Ausgabewerte 0
            if (primimplikanten.Length == 0)
            {
                return new Term[] { Term.Nullterm() };
            }

            return TermeAuswaehlen(primimplikanten);
        }


        /// <summary>
        /// Wählt aus den übergebenen Primimplikanten die kürzeste Kombination von Termen, die alle Minterme abdeckt
        /// </summary>
        /// <param name="primimplikanten">Term-Array mit den Primimplikanten</param>
        /// <returns>Term-Array mit der kürzesten Kombination von Termen</returns>
        private Term[] TermeAuswaehlen(Term[] primimplikanten)
        {
            #region Alle wesentlichen Primimplikanten und die schon abgedeckten Minterme ermitteln
            var abgedeckteMinterme = new HashSet<Int32>();
            var alleMinterme = new List<Int32>();
            var wesentlichePrimimplikanten = new List<Term>();
            var dontCares = new HashSet<Int32>();
            Term tempPrimimplikant = null;
            Int32 anzahlPrimimplikaten;
            foreach (var g in grade)
            {
                foreach (var minterm in g)
                {
                    alleMinterme.Add(minterm.Minterme[0]);
                    // Dont-Care-Terme merken und überspringen
                    if (minterm.DontCare)
                    {
                        dontCares.Add(minterm.Minterme[0]);
                        continue;
                    }

                    // Bereits abgedeckte Minterme überspringen
                    if (abgedeckteMinterme.Contains(minterm.Minterme[0]))
                    {
                        continue;
                    }

                    // Für jeden Primimplikanten überprüfen, ob er den aktuellen Minterm abdeckt
                    anzahlPrimimplikaten = 0;
                    foreach (var primimplikant in primimplikanten)
                    {
                        for (var j = 0; j < primimplikant.Minterme.Length; j++)
                        {
                            if (primimplikant.Minterme[j] == minterm.Minterme[0])
                            {
                                tempPrimimplikant = primimplikant;
                                anzahlPrimimplikaten++; ;
                                break;
                            }
                        }
                    }

                    // Falls der Minterm nur von einem Primimplikaten abgedeckt wird
                    // und dieser noch nicht der Menge der wesentlichen Primimplikanten angehört,
                    // den Primimplikaten der Menge hinzufügen
                    if (anzahlPrimimplikaten == 1 && !tempPrimimplikant.IsIn(wesentlichePrimimplikanten))
                    {
                        wesentlichePrimimplikanten.Add(tempPrimimplikant);
                        // Alle vom Primimplikanten abgedeckten Minterme der Menge der abgedeckten Minterme hinzufügen
                        for (var j = 0; j < tempPrimimplikant.Minterme.Length; j++)
                        {
                            if (!abgedeckteMinterme.Contains(tempPrimimplikant.Minterme[j]))
                            {
                                abgedeckteMinterme.Add(tempPrimimplikant.Minterme[j]);
                            }
                        }
                    }
                }
            }
            #endregion

            #region Noch nicht abgedeckte Minterme ermitteln
            var restMins = new List<Int32>();
            for (var i = 0; i < anzMins; i++)
            {
                var term = alleMinterme[i];
                if (!abgedeckteMinterme.Contains(term) && !dontCares.Contains(term))
                {
                    restMins.Add(term);
                }
            }
            #endregion

            #region Restliche Primimplikanten ermitteln
            var restterme = new List<Term>();
            foreach (var term in primimplikanten)
            {
                if (!term.IsIn(wesentlichePrimimplikanten))
                {
                    foreach (var i in term.Minterme)
                    {
                        if (restMins.Contains(i) && !term.IsIn(restterme))
                        {
                            restterme.Add(term);
                            break;
                        }
                    }
                }
            }
            #endregion

            #region Ggf besten Restterme ermitteln
            if (restMins.Count != 0)
            {
                var bestRest = OptimiereRest(restMins, restterme);
                foreach (var term in bestRest)
                {
                    wesentlichePrimimplikanten.Add(term);
                }
            }
            #endregion

            return wesentlichePrimimplikanten.ToArray();
        }

        /// <summary>
        /// Optimiert die Restmatrix, sodass der Ausdruck möglichst kurz wird
        /// </summary>
        /// <param name="restMins">List mit den Indizes der restlichen Minterme</param>
        /// <param name="restPrimimplikanten">List mit den restlichen Primimplikanten</param>
        /// <returns></returns>
        private List<Term> OptimiereRest(List<Int32> restMins, List<Term> restPrimimplikanten)
        {
            // Wenn keine Minterme mehr übrig, die Rekursion abbrechen
            if (restMins.Count == 0)
            {
                return new List<Term>();
            }

            var bestRest = new List<Term>();
            var min = BerechneLaenge(restPrimimplikanten);
            foreach (var minterm in restMins)
            {
                foreach (var primimplikant in restPrimimplikanten)
                {
                    // Überprüft für jeden Minterm, ob er in einem Primimplikaten enthalten
                    for (var j = 0; j < primimplikant.Minterme.Length; j++)
                    {
                        if (primimplikant.Minterme[j] == minterm)
                        {
                            // Die Minterme für den nächsten Rekursionsschritt kopieren und die abgedeckten Minterme entfernen
                            var nextRestMins = new List<Int32>(restMins);
                            foreach (var minTerm in primimplikant.Minterme)
                            {
                                nextRestMins.Remove(minTerm);
                            }

                            // Alle Primimplikanten, die noch Minterme abdecken für den nächsten Rekursionsschritt kopieren
                            var nextRestPrimimplikanten = new List<Term>();
                            foreach (var nextPrimimplikant in restPrimimplikanten)
                            {
                                // Den gerade gewählten Primimplikanten überspringen
                                if (nextPrimimplikant == primimplikant)
                                {
                                    continue;
                                }

                                var inRest = false;
                                // Deckt der Primimplikant noch Minterme ab?
                                foreach (var minTerm in nextPrimimplikant.Minterme)
                                {
                                    if (nextRestMins.Contains(minTerm))
                                    {
                                        inRest = true;
                                        break;
                                    }
                                }
                                // Falls mindestens ein Term abgedeckt wird, den Primimplikant in die List kopieren
                                if (inRest)
                                {
                                    nextRestPrimimplikanten.Add(nextPrimimplikant);
                                }
                            }

                            // Den Rest optimieren und den aktuellen 
                            nextRestPrimimplikanten = OptimiereRest(nextRestMins, nextRestPrimimplikanten);
                            nextRestPrimimplikanten.Add(primimplikant);

                            // Überprüfen, ob die Kombination kürzer besser ist als das bisher kürzeste Kombination
                            if (BerechneLaenge(nextRestPrimimplikanten) < min)
                            {
                                // Wenn ja, Werte der Kombination speichern
                                min = BerechneLaenge(nextRestPrimimplikanten);
                                bestRest = new List<Term>(nextRestPrimimplikanten);
                            }
                            // Die Schleife abbrechen, da der Minterm schon enthalten war
                            break;
                        }
                    }
                }
            }
            return bestRest;
        }

        /// <summary>
        /// Berechnet die Anzahl der Eingangsvariablen die alle Terme einer List zusammen haben
        /// </summary>
        /// <param name="terms">List, mit den Term-Objekten, die betrachtet werden sollen</param>
        /// <returns>Anzahl der Eingangsvariablen</returns>
        private Int32 BerechneLaenge(List<Term> terms)
        {
            var laenge = 0;
            foreach (var term in terms)
            {
                laenge += term.AnzahlEingabevariablen;
            }

            return laenge;
        }

        /// <summary>
        /// Berechnet die Primimplikanten der Terme und gibt sie zurück
        /// </summary>
        /// <returns>Term-Array mit den Primimplikanten</returns>
        public Term[] BerechnePrimimplikanten()
        {
            // Falls keine Terme vorhanden sind, Array ohne Elemente zurückgeben
            if (!hat_Terme)
            {
                return new Term[0];
            }

            SubBerechnePrimimplikanten(out var qmc, out var primimplikanten);

            // Primimplikanten der nächsten Stufe ermitteln
            var returner = qmc.BerechnePrimimplikanten();
            foreach (var term in returner)
            {
                // die Primimplikanten der aktuellen Stufe hinzufügen
                primimplikanten.Add(term);
            }

            // Primimplikanten zurückgeben
            return primimplikanten.ToArray();
        }

        private void SubBerechnePrimimplikanten(out QuineMcCluskey qmc, out List<Term> primimplikanten)
        {
            qmc = new QuineMcCluskey(grade.Count - 1);

            primimplikanten = new List<Term>();

            for (var i = 0; i < grade.Count; i++)
            {
                foreach (var term1 in grade[i])
                {
                    // Bis auf die größten Grad
                    if (i < (grade.Count - 1))
                    {
                        foreach (var term2 in grade[i + 1])
                        {
                            // Alle Elemente des aktuellen Grades mit denen des nächsthöheren Grades vergleichen
                            if (term1.IstAehnlichWie(term2))
                            {
                                // Ähnliche Terme zusammenfassen und als keine Primimplikanten markieren
                                qmc.TermHinzufuegen(term1 + term2);
                                term1.Ist_Primimplikant = false;
                                term2.Ist_Primimplikant = false;
                            }
                        }
                    }
                    // Falls der Term Primimplikant ist
                    if (term1.Ist_Primimplikant)
                    {
                        // den Term der Menge der Primimplikanten hinzufügen
                        primimplikanten.Add(term1);
                    }
                }
            }
        }

        /// <summary>
        /// Berechnet die Primimplikanten der Terme und gibt sie zurück, dazu speichert er noch die einzelnen Schritte
        /// </summary>
        /// <param name="einzelSchritte">List, in der die Schritte gespeichert werden</param>
        /// <returns>Term-Array mit den Primimplikanten</returns>
        public Term[] BerechnePrimimplikanten(ref List<List<Term[]>> einzelSchritte)
        {
            // Falls keine Terme vorhanden sind, Array ohne Elemente zurückgeben
            if (!hat_Terme)
            {
                return new Term[0];
            }
            else
            {

                einzelSchritte.Add(grade.Select(l => l.ToArray()).ToList());
            }

            SubBerechnePrimimplikanten(out var qmc, out var primimplikanten);

            // Primimplikanten der nächsten Stufe ermitteln
            var returner = qmc.BerechnePrimimplikanten(ref einzelSchritte);
            foreach (var term in returner)
            {
                // die Primimplikanten der aktuellen Stufe hinzufügen
                primimplikanten.Add(term);
            }

            // Primimplikanten zurückgeben
            return primimplikanten.ToArray();
        }
    }
}
