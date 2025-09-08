using System;
using System.Collections.Generic;

namespace Uno
{
    public class GameLogic
    {
        public GameLogic() { }

        public int currentPlayerIndex = 0;

        // Visuellt placeholder för spelarnas kortantal
        private Dictionary<int, int> playerCards = new Dictionary<int, int>();

        public void StartGame()
        {
            GameLogic GL = new GameLogic();
            GL.ShowMainMenu();
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
                var playerNames = ChoosePlayerNames();

                // Tilldela alla spelare 7 kort (placeholder)
                foreach (var player in playerNames)
                {
                    playerCards[player.Key] = 7;
                }

                ShowGameScreen(playerNames, currentPlayerIndex);
            }
            else
            {
                Console.Clear();
            }
        }

        public Dictionary<int, string> ChoosePlayerNames()
        {
            int numPlayers = 2;
            Dictionary<int, string> playerNames = new Dictionary<int, string>();
            for (int i = 1; i <= numPlayers; i++)
            {
                Console.WriteLine($"Enter name for Player {i}:");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Player" + i;
                }
                playerNames.Add(i, name);
                Console.Clear();
            }
            return playerNames;
        }

        public void ShowGameScreen(Dictionary<int, string> playerNames, int currentPlayer)
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
    }
}
