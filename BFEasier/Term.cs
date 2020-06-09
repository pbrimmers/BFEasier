namespace BFEasier
{
    using System;
    using System.Collections.Generic;

    public class Term
    {
        private readonly Int32[] input;
        public Boolean Ist_Null;
        public Boolean Ist_Primimplikant;

        public Boolean DontCare { get; }

        public Int32 this[Int32 pos] => input[pos];

        public Int32 Laenge => input.Length;

        public Int32 AnzahlEingabevariablen
        {
            get
            {
                var count = 0;
                foreach (var i in input)
                {
                    if (i >= 0)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public Int32 Grad { get; }

        /// <summary>
        /// Int-Array mit den Indizes der Minterme
        /// </summary>
        public Int32[] Minterme { get; }

        /// <summary>
        /// Konstruktor für Minterme
        /// </summary>
        /// <param name="input">Int-Array mit den Werten der Eingangsvariablen</param>
        /// <param name="dontcare">Boolscher Wert, ob es sich um einen Don't-Care-Term handelt</param>
        public Term(Int32[] input, Boolean dontcare)
        {
            DontCare = dontcare;
            Grad = 0;
            this.input = new Int32[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                this.input[i] = input[i];
                if (input[i] == 1)
                {
                    Grad++;
                }
            }

            Minterme = new Int32[1];
            Minterme[0] = 0;
            for (var i = 0; i < input.Length; i++)
            {
                Minterme[0] += input[i] * (Int32)Math.Pow(2, input.Length - i - 1);
            }

            Ist_Primimplikant = !dontcare; // Dont-Care-Terme können keine Primimplikanten sein
            Ist_Null = false;
        }

        /// <summary>
        /// Konstruktor für vereinfachte Terme
        /// </summary>
        /// <param name="input">Int-Array mit den Werten der Eingangsvariablen bzw -1 für eine nicht mehr vorhandene Variable</param>
        /// <param name="dontcare">Boolscher Wert, ob es sich um einen Term handelt, der nur Don't-Care-Term abdeckt</param>
        /// <param name="minTerme">Int-Array mit den Indizes der abgedeckten Mintermen</param>
        public Term(Int32[] input, Boolean dontcare, Int32[] minTerme)
        {
            Grad = 0;
            this.input = new Int32[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                this.input[i] = input[i];
                if (input[i] == 1)
                {
                    Grad++;
                }
            }

            Minterme = new Int32[minTerme.Length];
            for (var i = 0; i < minTerme.Length; i++)
            {
                Minterme[i] = minTerme[i];
            }
            DontCare = dontcare;
            Ist_Primimplikant = !dontcare; // Dont-Care-Terme können keine Primimplikanten sein
            Ist_Null = false;
        }

        /// <summary>
        /// Überprüft, ob sich dieser Term von dem Übergebenen nur in einer Negation unterscheidet
        /// </summary>
        /// <param name="term">Term-Objekt mit dem verglichen wird</param>
        /// <returns>'true' falls sich die Terme nur in einer Negation unterscheiden, anderfalls 'false'</returns>
        public Boolean IstAehnlichWie(Term term)
        {
            var temp = false;
            for (var i = 0; i < input.Length; i++)
            {
                if (((input[i] != -1) && (term[i] == -1)) || ((input[i] == -1) && (term[i] != -1)))
                {
                    return false;
                }

                if ((input[i] != -1) && (term[i] != -1) && (term[i] != input[i]))
                {
                    if (temp == true)
                    {
                        return false;
                    }

                    temp = true;
                }
            }
            return temp;
        }

        /// <summary>
        /// Wandelt den Term in einen String um
        /// </summary>
        /// <returns>String-Objekt mit dem Inhalt des Terms</returns>
        public override String ToString()
        {
            if (Ist_Null)
            {
                return "0";
            }

            var term = "";
            var nullCounter = 0;

            for (var i = 0; i < input.Length; i++)
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
            {
                term = "1";
            }

            return term;
        }

        /// <summary>
        /// Überläd den Gleichheitsoperator
        /// </summary>
        /// <param name="term1">Objekt der Klasse Term</param>
        /// <param name="term2">Objekt der Klasse Term</param>
        /// <returns>True, wenn die Terme gleich sind, andernfalls false</returns>
        public static Boolean operator ==(Term term1, Term term2)
        {
            // Bei ungleicher Länge können die Terme nicht gleich sein
            if (term1.Laenge != term2.Laenge)
            {
                return false;
            }

            // Alle Eingangsvariablen auf Gleichheit überprüfen
            for (var i = 0; i < term1.Laenge; i++)
            {
                // Ist ein Eingabeparameter anders sind die Terme nicht gleich
                if (term1[i] != term2[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Überläd den Ungleichheitsoperator
        /// </summary>
        /// <param name="term1">Objekt der Klasse Term</param>
        /// <param name="term2">Objekt der Klasse Term</param>
        /// <returns>True, wenn die Terme ungleich sind, andernfalls false</returns>
        public static Boolean operator !=(Term term1, Term term2)
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
            var input = new Int32[term1.Laenge];
            // Gleichen Variablen übernehmen und unterschiedliche als nicht vorhanden (-1) setzen
            for (var i = 0; i < term1.Laenge; i++)
            {
                if (term1[i] == term2[i])
                {
                    input[i] = term1[i];
                }
                else
                {
                    input[i] = -1;
                }
            }

            // Minterme aus des ersten Terms übernehmen und nicht enthaltene des zweiten Terms hinzufügen
            var tempMinterme = new List<Int32>();
            foreach (var i in term1.Minterme)
            {
                tempMinterme.Add(i);
            }
            foreach (var i in term2.Minterme)
            {
                if (!tempMinterme.Contains(i))
                {
                    tempMinterme.Add(i);
                }
            }
            // Minterme sortieren
            tempMinterme.Sort();

            var tempTerm = new Term(input, term1.DontCare && term2.DontCare, tempMinterme.ToArray());
            return tempTerm;
        }

        /// <summary>
        /// Überprüft, ob der Term in dem List ist. Die Contains()-Funktion funktioniert irgendwie nicht.
        /// </summary>
        /// <param name="list">Zu überprüfendes Array mit ausschließlich Term-Objekten</param>
        /// <returns>True, falls der Term enthalten ist, andernfalls False</returns>
        public Boolean IsIn(List<Term> list) => list.Contains(this);

        /// <summary>
        /// Gibt einen Term zurück der 0 ist
        /// </summary>
        /// <returns>Term der 0 ist</returns>
        public static Term Nullterm()
        {
            Int32[] input = { -1 };
            var term = new Term(input, false, input)
            {
                Ist_Null = true
            };
            return term;
        }

        public override Boolean Equals(Object obj) => obj is Term term && this == term;

        public override Int32 GetHashCode()
        {
            var hashCode = 1396721366;
            hashCode = hashCode * -1521134295 + EqualityComparer<Int32[]>.Default.GetHashCode(input);
            hashCode = hashCode * -1521134295 + Ist_Null.GetHashCode();
            hashCode = hashCode * -1521134295 + Ist_Primimplikant.GetHashCode();
            hashCode = hashCode * -1521134295 + DontCare.GetHashCode();
            hashCode = hashCode * -1521134295 + Laenge.GetHashCode();
            hashCode = hashCode * -1521134295 + AnzahlEingabevariablen.GetHashCode();
            hashCode = hashCode * -1521134295 + Grad.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Int32[]>.Default.GetHashCode(Minterme);
            return hashCode;
        }
    }
}
