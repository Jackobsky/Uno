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
                    for (int i = 0; i < 7; i++)
                    {
                        player.Hand.Add(deck.DrawCard());
                    }
                }

                // Lägg ut första kortet på bordet
                while (true)
                {
                    discardPile.Add(deck.DrawCard());
                    var lastColor = discardPile.Last().color;
                    var lastValue = discardPile.Last().value;
                    if(lastColor != "Black" || lastValue != "Draw Two" || lastValue != "Skip" || lastValue != "Reverse") // Första kortet kan inte vara wild, +2, skip eller reverse
                    { 
                        break;
                    } 
                    else 
                    {
                        discardPile.Clear(); // Ta bort wild kortet och dra ett nytt
                    }
                       
                }
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
                ShowGameScreen(); // Already clears console


                var currentPlayer = players[currentPlayerIndex];
                Console.WriteLine("\nChoose a card to play (number) or 0 to draw:");
                int choice = int.Parse(Console.ReadLine() ?? "0");

                int choice;
                while (true)
                {
                    Console.WriteLine("\nChoose a card to play (number) or 0 to draw:");
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue; // fråga igen
                    }


                    if (choice < 0 || choice > currentPlayer.Hand.Count)
                    {
                        Console.WriteLine("Choice out of range. Try again.");
                        continue; // fråga igen
                    }

                    break; // giltigt val, lämna while
                }
   

                if (choice == 0)
                {
                    // Draw a card
                    currentPlayer.Hand.Add(deck.DrawCard());
                    Console.WriteLine("You drew a card. Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    Card selectedCard = currentPlayer.Hand[choice - 1];
                    if (CanPlayCard(selectedCard))
                    {
                        if (selectedCard.value == "Wild" || selectedCard.value == "Wild Draw Four") // Välj färg vid wild card
                        {
                            //Console.Clear();
                            ShowGameScreen();
                            Console.WriteLine("Choose a color: 1. Red 2. Blue 3. Green 4. Yellow");
                            int colorChoice = int.Parse(Console.ReadLine() ?? "1");
                            switch (colorChoice)
                            {
                                case 1:
                                    selectedCard.color = "Red";
                                    break;
                                case 2:
                                    selectedCard.color = "Blue";
                                    break;
                                case 3:
                                    selectedCard.color = "Green";
                                    break;
                                case 4:
                                    selectedCard.color = "Yellow";
                                    break;
                                default:
                                    selectedCard.color = "Red";
                                    break;
                            }

                            if(selectedCard.value == "Wild Draw Four") //Logik för att dra 4 kort
                            {
                                NextPlayer(); //skip player turn
                                var nextPlayer = players[currentPlayerIndex];
                                for (int i = 0; i < 4; i++)
                                {
                                    nextPlayer.Hand.Add(deck.DrawCard());
                                }
                                Console.WriteLine($"{nextPlayer.Name} draws 4 cards!");
                            }
                        }
                        else if (selectedCard.value == "Draw Two") // Dra 2 kort
                        {
                            NextPlayer();
                            var nextPlayer = players[currentPlayerIndex];
                            for (int i = 0; i < 2; i++)
                            {
                                nextPlayer.Hand.Add(deck.DrawCard());
                            }
                            Console.WriteLine($"{nextPlayer.Name} draws 2 cards!");
                        }
                        else if (selectedCard.value == "Skip") // Hoppa över nästa spelare
                        {
                            NextPlayer();
                            Console.WriteLine($"{players[currentPlayerIndex].Name} is skipped!");
                        }
                        else if (selectedCard.value == "Reverse") // Vänd spelordningen
                        {
                            players.Reverse();
                            currentPlayerIndex = players.Count - 1 - currentPlayerIndex; // Justera index för nuvarande spelare
                            Console.WriteLine("Play order reversed!");
                        }

                        // Lägg kortet på högen, samma för alla kort
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
                        Console.Clear();
                        ShowGameScreen();
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
            Console.Clear();
            Console.WriteLine("Current game state:");
            Console.WriteLine("-------------------\n");

            // 1. Show last played card at the top
            Console.WriteLine("Last played card:");
            Card.PrintCardsInRows(new List<Card> { discardPile.Last() });
            Console.WriteLine();

            // 2. Show all players' cards
            for (int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                string turnMarker = (i == currentPlayerIndex) ? " <--- Current turn" : "";
                Console.WriteLine($"{player.Name} | Cards left: {player.Hand.Count}{turnMarker}");

                if (i == currentPlayerIndex)
                {
                    // Current player's cards visible in game state
                    Card.PrintCardsInRows(player.Hand, maxCardsPerRow: 10, hidden: false, showIndices: true);
                }
                else
                {
                    // Other players' cards hidden
                    Card.PrintCardsInRows(player.Hand, maxCardsPerRow: 10, hidden: true);
                }

                Console.WriteLine(); // spacing between players
            }
        }


    }
}
