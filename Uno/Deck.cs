using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    internal class Deck
    {
        public Deck() { }

        private List<Card> cards = new List<Card>();

        public void CreateDeck()
        {
            for(int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    string color = "";
                    switch (i)
                    {
                        case 0:
                            color = "Red";
                            break;
                        case 1:
                            color = "Blue";
                            break;
                        case 2:
                            color = "Green";
                            break;
                        case 3:
                            color = "Yellow";
                            break;
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        cards.Add(new Card(color, j.ToString()));
                    }
                    cards.Add(new Card(color, "Skip"));
                    cards.Add(new Card(color, "Reverse"));
                    cards.Add(new Card(color, "Draw Two"));
                }
            }
            
        }
    }
}
