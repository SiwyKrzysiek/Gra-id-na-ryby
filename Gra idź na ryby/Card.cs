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
    }
}
