namespace Uno
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.CreateDeck();
            UI ui = new UI();
            ui.ShowMainMenu();
        }
    }
}
