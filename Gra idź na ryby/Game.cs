using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gra_idź_na_ryby
{
    public class Game
    {
        private List<Player> players;
        private Dictionary<Values, Player> books;
        private Deck stock;
        private TextBox textBox;

        public Game(string playerName, IEnumerable<string> opponentsNames, TextBox output)
        {
            Random random = new Random();
            textBox = output;
            players = new List<Player>();
            players.Add(new Player(playerName, random, output));
            foreach (string name in opponentsNames)
                players.Add(new Player(name, random, output));
            books = new Dictionary<Values, Player>();
            stock = new Deck();

            Deal();
            players[0].SortHand();
        }

        private void Deal()
        {
            stock.Shuffle();
            foreach (Player player in players)
            {
                for (int i=0; i<5; i++)
                    player.TakeCard(stock.Deal());
                PullOutBooks(player);
            }
        }

        public bool PullOutBooks(Player player)
        {
            List<Values> booksPulled = new List<Values>(player.PulOutBooks());
            foreach (Values value in booksPulled)
            {
                books.Add(value, player);
            }

            return player.CardCount == 0;
        }

        public string DescribeBooks()
        {
            string description = "";
            for (int i = 0; i < books.Count; i++)
            {
                KeyValuePair<Values, Player> book = books.ElementAt(i);
                description += book.Value.Name + " ma grupę " + Card.PluralValueName(book.Key, 4);
                if (i != books.Count - 1) description += Environment.NewLine;
            }

            return description;
        }

        public string GetWinnerName()
        {
            Dictionary<Player, int> numberOfBooks = new Dictionary<Player, int>(players.Count);
            foreach (Player player in players)
            {
                numberOfBooks.Add(player, 0);
            }

            foreach (KeyValuePair<Values, Player> book in books)
            {
                numberOfBooks[book.Value]++;
            }

            int mostBooks = numberOfBooks.Values.Max();
            List<string> winnersNames = new List<string>();
            foreach (Player player in numberOfBooks.Keys)
            {
                if (numberOfBooks[player] == mostBooks)
                    winnersNames.Add(player.Name);
            }

            string result = "";
            if (winnersNames.Count > 1)
            {
                result = "Remis pomiędzy ";
                for (int i = 0; i < winnersNames.Count; i++)
                {
                    result += winnersNames[i];
                    if (i != winnersNames.Count - 1) result += " i ";
                }
            }
            else
                result = winnersNames[0];

            return result + ": " + mostBooks + (mostBooks > 4 ? " grup" : " grupy");
        }

        public IEnumerable<string> GetPlayerCardNames() => players[0].CardNames;

        public string DescribePlayerHands()
        {
            string result = "";
            foreach(Player player in players)
            {
                result += player.Name + " ma " + player.CardCount;
                if (player.CardCount == 1)
                    result += " kartę" + Environment.NewLine;
                if (player.CardCount > 1 && player.CardCount < 5)
                    result += " karty" + Environment.NewLine;
                if (player.CardCount > 4)
                    result += " kart" + Environment.NewLine;
            }

            result += $"Na kupce pozostało {stock.Count} kart" + Environment.NewLine;
            return result;
        }
    }
}
