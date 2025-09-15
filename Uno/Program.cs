namespace Uno
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.CreateDeck();
            deck.ShuffleDeck();

            GameLogic ui = new GameLogic(deck);
            ui.StartGame();

        }
    }
}
