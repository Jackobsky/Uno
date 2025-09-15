using System;
using System.Collections.Generic;

namespace Uno
{
    public class GameLogic
    {
        public int currentPlayerIndex = 0;
        private List<Player> players = new List<Player>();
        private Deck deck;

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

            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                Console.Clear();
                players = ChoosePlayerNames();

                // Ge kort till alla spelare
                foreach (var player in players)
                {
                    deck.GiveCards(player.Hand);
                }

                ShowGameScreen();
            }
            else
            {
                Console.Clear();
            }
        }

        static List<Player> ChoosePlayerNames()
        {
            int numPlayers = 2; // TODO: Let the user choose later
            List<Player> players = new List<Player>();

            for (int i = 1; i <= numPlayers; i++)
            {
                Console.WriteLine($"Enter name for Player {i}:");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Player" + i;
                }

                players.Add(new Player(name));
                Console.Clear();
            }

            return players;
        }

        public void ShowGameScreen()
        {
            Console.Clear();
            Console.WriteLine("Current game state:");
            Console.WriteLine("-------------------");

            foreach (var player in playerNames)
            {
                string turnMarker = (player.Key - 1 == currentPlayer) ? " <--- Current turn" : "";
                Console.WriteLine($"{player.Value} | Cards left: {playerCards[player.Key]}{turnMarker}");
            }
        }

        /*
        private void DealCards(Deck deck, List<Player> players)
        {
            foreach (var player in players)
            {
                deck.GiveCards(player.Hand);
            }
        }

        
        private void ShowGameScreen(List<Player> players)
        {
            Console.Clear();
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name} has the following cards:");
                foreach (var card in player.Hand)
                {
                    Console.WriteLine($"{card.Color} {card.Value}");
                }
                Console.WriteLine();
            }
        }
        */
    }
}
