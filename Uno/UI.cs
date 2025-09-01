using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    public class UI
    {
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
                ChoosePlayerNames();
                ShowGameScreen();
            }
            else 
            {
                Console.Clear();
            }
        }

        public void ChoosePlayerNames()
        {
            //Console.WriteLine("Enter number of players (2):");
            int numPlayers = 2; //int.Parse(Console.ReadLine());
            Dictionary<int, string> playerNames = new Dictionary<int, string>();
            for (int i = 1; i <= numPlayers; i++)
            {
                Console.WriteLine($"Enter name for Player {i}:");
                string name = Console.ReadLine();
                playerNames.Add(i, name);
                Console.Clear();
            }

        }

        public void ShowGameScreen()
        {
            Console.Clear();
            Console.WriteLine("Player 1's turn");
        }
    }
}
