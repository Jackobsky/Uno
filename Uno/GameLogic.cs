using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    public class GameLogic
    {
        public GameLogic() { }

        public int currentPlayerIndex = 0;
        public void StartGame()
        {
            Deck deck = new Deck();
            deck.CreateDeck();
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
                ShowGameScreen(ChoosePlayerNames(), currentPlayerIndex);
            }
            else
            {
                Console.Clear();
            }
        }

        public Dictionary<int, string> ChoosePlayerNames()
        {
            //Console.WriteLine("Enter number of players (2):");
            int numPlayers = 2; //int.Parse(Console.ReadLine());
            Dictionary<int, string> playerNames = new Dictionary<int, string>();
            for (int i = 1; i <= numPlayers; i++)
            {
                Console.WriteLine($"Enter name for Player {i}:");
                string name = Console.ReadLine();
                if (name == null)
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

            if (playerNames.ContainsKey(currentPlayer + 1)) 
            {
                string playerName = playerNames[currentPlayer + 1];
                Console.WriteLine($"{playerName}'s turn");
            }
            else
            {
                Console.WriteLine("Error: Invalid player index");
            }
        }

    }
}
