using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra_idź_na_ryby
{
    public class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    cards.Add( new Card( (Suits)i, (Values)j ) );
                }
            }
        }

        public Deck(IEnumerable<Card> initalCards)
        {
            cards = new List<Card>(initalCards);
        }

        public int Count => cards.Count;

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public Card Deal(int index = 0)
        {
            Card card = cards[index];
            cards.RemoveAt(index);
            return card;
        }

        public Card Peek(int index = 0)
        {
            return cards[index];
        }

        public void Shuffle()
        {
            for (int i = cards.Count-1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);

                Card buffor = cards[i];
                cards[i] = cards[j];
                cards[j] = buffor;
            }
        }

        public IEnumerable<string> GetCardNames()
        {
            List<string> nazwy = new List<string>();

            foreach (Card card in cards)
            {
                nazwy.Add(card.Name);
            }
            return nazwy;
        }

        public void SortbySuit()
        {
            cards.Sort(new CardComperer_bySuit());
        }

        public void SortByValue()
        {
            cards.Sort(new CardComparer_byValue());
        }

        public bool ContainsValue(Values value)
        {
            foreach (Card card in cards)
                if (card.Value == value)
                    return true;

            return false;
        }

        public Deck PullOutValues(Values value)
        {
            Deck deckToReturn = new Deck(new Card[0]);
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Value == value)
                    deckToReturn.Add(Deal(i));
            }

            return deckToReturn;
        }

        public bool HasBook(Values value)
        {
            int count = 0;
            foreach (Card card in cards)
            {
                if (card.Value == value)
                    count++;
            }

            return count == 4;
        }

        public IEnumerable<Values> PoolBooks()
        {
            List<Values> result = new List<Values>();
            for (int i=1; i<=13; i++)
            {
                if (HasBook((Values)i))
                {
                    result.Add((Values)i);
                    for (int j = 0; j < cards.Count; j++)
                    {
                        Card card = cards[j];
                        if (card.Value == (Values)i)
                            cards.Remove(card);
                    }
                }
            }

            return result;
        }
    }
}
