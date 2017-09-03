using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gra_idź_na_ryby
{
    public class Player
    {
        public string Name { get; private set; }
        private Random random;
        private Deck cards = new Deck(new Card[0]);
        private TextBox textBox;

        public Player(string name, Random random, TextBox textBox)
        {
            Name = name;
            this.random = random;
            this.textBox = textBox;

            textBox.Text += Name + " dołączył do gry" + Environment.NewLine;
        }

        public IEnumerable<Values> PulOutBooks()
        {
            return cards.PoolBooks();
        }
    }
}
