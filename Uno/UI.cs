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
        public int numPlayers;
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
                //ShowGameScreen();
            }
            else 
            {
                Console.Clear();
            }
        }

        public void ChoosePlayerNames()
        {
            Console.WriteLine("Enter number of players (2):");
            numPlayers = int.Parse(Console.ReadLine());  
            Dictionary<int, string> playerNames = new Dictionary<int, string>();
            for (int i = 1; i <= numPlayers; i++)
            {
                Console.WriteLine($"Enter name for Player {i}:");
                string name = Console.ReadLine();
                playerNames.Add(i, name);
                Console.Clear();
            }

            for(int i = 0; i < playerNames.Count; i++)
            {

            }

        }

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


    }
}
