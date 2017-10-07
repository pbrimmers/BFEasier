using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BFEasier
{
    public class Term
    {
        private bool dontcare;
        private int[] input;
        private int grad;
        private int[] minterme;
        public bool Ist_Null;
        public bool Ist_Primimplikant;

        public bool DontCare
        {
            get
            {
                return dontcare;
            }
        }

        public int this[int pos]
        {
            get
            {
                return input[pos];
            }
        }

        public int Laenge
        {
            get
            {
                return input.Length;
            }
        }

        public int AnzahlEingabevariablen
        {
            get
            {
                int count = 0;
                foreach (int i in input)
                {
                    if (i >= 0)
                        count++;
                }
                return count;
            }
        }

        public int Grad
        {
            get
            {
                return grad;
            }
        }

        /// <summary>
        /// Int-Array mit den Indizes der Minterme
        /// </summary>
        public int[] Minterme
        {
            get
            {
                return minterme;
            }
        }

        /// <summary>
        /// Konstruktor für Minterme
        /// </summary>
        /// <param name="input">Int-Array mit den Werten der Eingangsvariablen</param>
        /// <param name="dontcare">Boolscher Wert, ob es sich um einen Don't-Care-Term handelt</param>
        public Term(int[] input, bool dontcare)
        {
            this.dontcare = dontcare;
            grad = 0;
            this.input = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                this.input[i] = input[i];
                if(input[i] == 1)
                    grad++;
            }

            minterme = new int[1];
            minterme[0] = 0;
            for (int i = 0; i < input.Length; i++)
                minterme[0] += input[i] * (int)Math.Pow(2, (input.Length - i - 1));
            Ist_Primimplikant = !dontcare; // Dont-Care-Terme können keine Primimplikanten sein
            Ist_Null = false;
        }

        /// <summary>
        /// Konstruktor für vereinfachte Terme
        /// </summary>
        /// <param name="input">Int-Array mit den Werten der Eingangsvariablen bzw -1 für eine nicht mehr vorhandene Variable</param>
        /// <param name="dontcare">Boolscher Wert, ob es sich um einen Term handelt, der nur Don't-Care-Term abdeckt</param>
        /// <param name="minTerme">Int-Array mit den Indizes der abgedeckten Mintermen</param>
        public Term(int[] input, bool dontcare, int[] minTerme)
        {
            grad = 0;
            this.input = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                this.input[i] = input[i];
                if(input[i] == 1)
                    grad++;
            }

            this.minterme = new int[minTerme.Length];
            for (int i = 0; i < minTerme.Length; i++)
            {
                this.minterme[i] = minTerme[i];
            }
            this.dontcare = dontcare;
            Ist_Primimplikant = !dontcare; // Dont-Care-Terme können keine Primimplikanten sein
            Ist_Null = false;
        }

        /// <summary>
        /// Überprüft, ob sich dieser Term von dem Übergebenen nur in einer Negation unterscheidet
        /// </summary>
        /// <param name="term">Term-Objekt mit dem verglichen wird</param>
        /// <returns>'true' falls sich die Terme nur in einer Negation unterscheiden, anderfalls 'false'</returns>
        public bool aehnlich(Term term)
        {
            bool temp = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (((input[i] != -1) && (term[i] == -1)) || ((input[i] == -1) && (term[i] != -1)))
                    return false;
                if ((input[i] != -1) && (term[i] != -1) && (term[i] != input[i]))
                {
                    if (temp == true)
                        return false;
                    temp = true;
                }
            }
            return temp;
        }

        /// <summary>
        /// Wandelt den Term in einen String um
        /// </summary>
        /// <returns>String-Objekt mit dem Inhalt des Terms</returns>
        public override string ToString()
        {
            if (Ist_Null)
                return "0";

            string term = "";
            int nullCounter = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 0)
                {
                    term += "!" + Properties.Settings.Default.einChar + (i + 1).ToString();
                }
                else if (input[i] == 1)
                {
                    term += Properties.Settings.Default.einChar + (i + 1).ToString();
                }
                else
                {
                    nullCounter++;
                }
            }

            if (nullCounter == input.Length)
                term = "1";

            return term;
        }

        /// <summary>
        /// Überläd den Gleichheitsoperator
        /// </summary>
        /// <param name="term1">Objekt der Klasse Term</param>
        /// <param name="term2">Objekt der Klasse Term</param>
        /// <returns>True, wenn die Terme gleich sind, andernfalls false</returns>
        public static bool operator ==(Term term1, Term term2)
        {
            // Bei ungleicher Länge können die Terme nicht gleich sein
            if (term1.Laenge != term2.Laenge)
                return false;

            // Alle Eingangsvariablen auf Gleichheit überprüfen
            for (int i = 0; i < term1.Laenge; i++)
            {
                // Ist ein Eingabeparameter anders sind die Terme nicht gleich
                if (term1[i] != term2[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Überläd den Ungleichheitsoperator
        /// </summary>
        /// <param name="term1">Objekt der Klasse Term</param>
        /// <param name="term2">Objekt der Klasse Term</param>
        /// <returns>True, wenn die Terme ungleich sind, andernfalls false</returns>
        public static bool operator !=(Term term1, Term term2)
        {
            return !(term1 == term2);
        }

        /// <summary>
        /// Überläd den '+'-Operator um Terme zusammenzufügen
        /// </summary>
        /// <param name="term1">Objekt der Klasse Term</param>
        /// <param name="term2">Objekt der Klasse Term, das dem ersten Parameter ähnlich ist</param>
        /// <returns>Den zusammengefügten Term</returns>
        public static Term operator +(Term term1, Term term2)
        {
            int[] input = new int[term1.Laenge];
            // Gleichen Variablen übernehmen und unterschiedliche als nicht vorhanden (-1) setzen
            for(int i = 0; i < term1.Laenge; i++)
            {
                if (term1[i] == term2[i])
                    input[i] = term1[i];
                else
                    input[i] = -1;

            }

            // Minterme aus des ersten Terms übernehmen und nicht enthaltene des zweiten Terms hinzufügen
            ArrayList tempMinterme = new ArrayList();
            foreach (int i in term1.minterme)
            {
                tempMinterme.Add(i);
            }
            foreach (int i in term2.minterme)
            {
                if (!tempMinterme.Contains(i))
                    tempMinterme.Add(i);
            }
            // Minterme sortieren
            tempMinterme.Sort();

            Term tempTerm = new Term(input, term1.DontCare && term2.DontCare, (int[])tempMinterme.ToArray(typeof(int)));
            return tempTerm;
        }

        /// <summary>
        /// Überprüft, ob der Term in dem ArrayList ist. Die Contains()-Funktion funktioniert irgendwie nicht.
        /// </summary>
        /// <param name="list">Zu überprüfendes Array mit ausschließlich Term-Objekten</param>
        /// <returns>True, falls der Term enthalten ist, andernfalls False</returns>
        public bool IsIn(ArrayList list)
        {
            foreach (Term term in list)
            {
                if (term == this)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Gibt einen Term zurück der 0 ist
        /// </summary>
        /// <returns>Term der 0 ist</returns>
        public static Term Nullterm()
        {
            int[] input = { -1 };
            Term term = new Term(input, false, input)
            {
                Ist_Null = true
            };
            return term;
        }

        public override bool Equals(object obj)
        {
            var term = obj as Term;
            return term != null &&
                   dontcare == term.dontcare &&
                   EqualityComparer<int[]>.Default.Equals(input, term.input) &&
                   grad == term.grad &&
                   EqualityComparer<int[]>.Default.Equals(minterme, term.minterme) &&
                   Ist_Null == term.Ist_Null &&
                   Ist_Primimplikant == term.Ist_Primimplikant &&
                   DontCare == term.DontCare &&
                   Laenge == term.Laenge &&
                   AnzahlEingabevariablen == term.AnzahlEingabevariablen &&
                   Grad == term.Grad &&
                   EqualityComparer<int[]>.Default.Equals(Minterme, term.Minterme);
        }

        public override int GetHashCode()
        {
            var hashCode = 634771311;
            hashCode = hashCode * -1521134295 + dontcare.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(input);
            hashCode = hashCode * -1521134295 + grad.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(minterme);
            hashCode = hashCode * -1521134295 + Ist_Null.GetHashCode();
            hashCode = hashCode * -1521134295 + Ist_Primimplikant.GetHashCode();
            hashCode = hashCode * -1521134295 + DontCare.GetHashCode();
            hashCode = hashCode * -1521134295 + Laenge.GetHashCode();
            hashCode = hashCode * -1521134295 + AnzahlEingabevariablen.GetHashCode();
            hashCode = hashCode * -1521134295 + Grad.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(Minterme);
            return hashCode;
        }
    }
}
