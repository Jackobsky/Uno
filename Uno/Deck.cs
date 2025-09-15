namespace Uno
{
    internal class Deck
    {
        public Deck() { }

        private List<Card> cards = new List<Card>();

        public void CreateDeck()
        {
            //Skapa nummerkort och actionkort
            for (int k = 0; k < 2; k++)
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

            //Skapa wild cards
            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Card("Black", "Wild"));
                cards.Add(new Card("Black", "Wild Draw Four"));
            }

        }

        public void ShuffleDeck()
        {
            Random rand = new Random();
            cards = cards.OrderBy(x => rand.Next()).ToList();
        }

        public void GiveCards(List<Card> hand)
        {
            for (int i = 0; i < 7; i++)
            {
                hand.Add(cards[0]);
                cards.RemoveAt(0);
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

    }
}
