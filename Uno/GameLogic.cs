using System;
using System.Collections.Generic;
using System.Linq;

namespace Uno
{
    internal class GameLogic
    {
        public int currentPlayerIndex = 0;
        private List<Player> players = new List<Player>();
        private Deck deck;
        private List<Card> discardPile = new List<Card>();

        public GameLogic(Deck deck)
        {
            this.deck = deck;
        }

        public void StartGame()
        {
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Uno! \nWhat do you want to do?");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Exit");

            int choice = int.Parse(Console.ReadLine() ?? "2");
            if (choice == 1)
            {
                Console.Clear();
                players = ChoosePlayerNames();

                // Ge kort till alla spelare
                foreach (var player in players)
                {
                    for(int i = 0; i < 7; i++)
                    {
                        player.Hand.Add(deck.DrawCard());
                    }
                }

                // Lägg ut första kortet på bordet
                discardPile.Add(deck.DrawCard());

                PlayTurns();
            }
        }

        static List<Player> ChoosePlayerNames()
        {
            int numPlayers = 2; // kan ändras senare
            List<Player> players = new List<Player>();

            for (int i = 1; i <= numPlayers; i++)
            {
                Console.WriteLine($"Enter name for Player {i}:");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                    name = "Player" + i;

                players.Add(new Player(name));
                Console.Clear();
            }

            return players;
        }

        public void PlayTurns()
        {
            while (true)
            {
                Console.Clear();
                ShowGameScreen();
                var currentPlayer = players[currentPlayerIndex];

                Console.WriteLine("\nYour hand:");
                for (int i = 0; i < currentPlayer.Hand.Count; i++)
                {
                    Console.Write($"{i + 1}. ");
                    currentPlayer.Hand[i].PrintColored();
                    Console.WriteLine();

                }

                Console.WriteLine("\nChoose a card to play (number) or 0 to draw:");
                int choice = int.Parse(Console.ReadLine() ?? "0");

                if (choice == 0)
                {
                    // Dra kort
                    currentPlayer.Hand.Add(deck.DrawCard());
                    Console.WriteLine("You drew a card. Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    Card selectedCard = currentPlayer.Hand[choice - 1];
                    if (CanPlayCard(selectedCard))
                    {
                        discardPile.Add(selectedCard);
                        currentPlayer.Hand.RemoveAt(choice - 1);
                        Console.WriteLine($"You played {selectedCard}. Press any key to continue.");
                        Console.ReadKey();
                        NextPlayer();
                    }
                    else
                    {
                        Console.WriteLine("You cannot play that card. Press any key to try again.");
                        Console.ReadKey();
                    }
                }

                if (currentPlayer.Hand.Count == 0)
                {
                    Console.WriteLine($"{currentPlayer.Name} wins!");
                    break;
                }
            }
        }

        private bool CanPlayCard(Card card)
        {
            Card topCard = discardPile.Last();
            return card.color == topCard.color || card.value == topCard.value || card.color == "Black";
        }

        private void NextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        public void ShowGameScreen()
        {
            Console.WriteLine("Current game state:");
            Console.WriteLine("-------------------");

            for (int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                string turnMarker = (i == currentPlayerIndex) ? " <--- Current turn" : "";
                Console.WriteLine($"{player.Name} | Cards left: {player.Hand.Count}{turnMarker}");
            }

            Console.Write("\nLast played card: ");
            discardPile.Last().PrintColored();
            Console.WriteLine();

        }
    }
}
