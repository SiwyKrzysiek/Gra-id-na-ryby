using System;
using System.Collections.Generic;
using System.Text;

namespace Gra_idź_na_ryby
{
    public class Card
    {
        private static Random random = new Random();
        public Suits Suit { get; private set; }
        public Values Value { get; private set; }
        public string Name => Value + " of " + Suit;

        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }

        public Card()
        {
            Suit = (Suits)random.Next(4);
            Value = (Values)random.Next(1, 14);
        }

        public override string ToString()
        {
            return Name;
        }

        private static string[] names0 = {"asów", "dwójek", "trójek", "czwórek", "piątek", "szóstek", "siódemek", "ósemek", "dziewiątek", "dziesiątek", "waletów", "dam", "króli"};
        private static string[] names1 = { "asa", "dwójkę", "czwórkę", "piątkę", "szóstkę", "siódemkę", "ósemkę", "dziewiątkę", "dziesiątkę", "waleta", "damę", "króla" };
        private static string[] names2AndMore = { "asy", "dwójki", "trójki", "czwórki", "piątki", "szóstki", "siódemki", "ósemki", "dziewiątki", "dziesiątki", "walety", "damy", "króle"};

        public static string PluralValueName(Values value, int count)
        {
            if (count == 0)
            {
                return names0[(int)value - 1];
            }
            else if (count == 1)
            {
                return names1[(int)value - 1];
            }
            else if (count > 1 && count < 5)
            {
                return names2AndMore[(int)value - 1];
            }
            else
            {
                throw new Exception("Count can be only 0-4");
            }
        }
    }
}
