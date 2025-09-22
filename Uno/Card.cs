namespace Uno
{
    internal class Card
    {
        public string color;
        public string value;

        public Card(string color, string value)
        {
            this.color = color;
            this.value = value;
        }

        public override string ToString()
        {
            return $"{color} {value}";
        }

        // Returns box lines; if hidden = true, we show generic back
        public string[] GetCardLines(bool hidden = false)
        {
            if (hidden)
            {
                // Use different names or just return array directly
                return new string[]
                {
            "+-------+",
            "|*******|",
            "|*******|",
            "+-------+"
                };
            }

            // Normal card
            int maxWidth = Math.Max(value.Length, color.Length) + 2;
            string border = "+" + new string('-', maxWidth) + "+";
            string valueLine = "|" + CenterText(value, maxWidth) + "|";
            string colorLine = "|" + CenterText(color, maxWidth) + "|";

            return new string[] { border, valueLine, colorLine, border };
        }


        private string CenterText(string text, int width)
        {
            int padding = width - text.Length;
            int padLeft = padding / 2;
            int padRight = padding - padLeft;
            return new string(' ', padLeft) + text + new string(' ', padRight);
        }

        // Print cards in horizontal rows with wrapping
        public static void PrintCardsInRows(List<Card> cards, int maxCardsPerRow = 10, bool hidden = false, bool showIndices = false)
        {
            int totalRows = (int)Math.Ceiling(cards.Count / (double)maxCardsPerRow);
            for (int row = 0; row < totalRows; row++)
            {
                int start = row * maxCardsPerRow;
                int end = Math.Min(start + maxCardsPerRow, cards.Count);
                var rowCards = cards.GetRange(start, end - start);

                // 0. Print indices above cards if requested
                if (showIndices && !hidden)
                {
                    for (int i = 0; i < rowCards.Count; i++)
                    {
                        int cardIndex = start + i + 1;
                        string indexStr = cardIndex.ToString();
                        int cardWidth = rowCards[i].GetCardLines(hidden)[0].Length;
                        Console.Write(indexStr.PadLeft((cardWidth + indexStr.Length) / 2).PadRight(cardWidth + 2)); // center index
                    }
                    Console.WriteLine();
                }

                // 1. Print card boxes (4 lines)
                for (int line = 0; line < 4; line++)
                {
                    foreach (var card in rowCards)
                    {
                        string[] lines = card.GetCardLines(hidden);

                        if (!hidden)
                        {
                            switch (card.color)
                            {
                                case "Red": Console.ForegroundColor = ConsoleColor.Red; break;
                                case "Blue": Console.ForegroundColor = ConsoleColor.Blue; break;
                                case "Green": Console.ForegroundColor = ConsoleColor.Green; break;
                                case "Yellow": Console.ForegroundColor = ConsoleColor.Yellow; break;
                                case "Black":
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    break;
                                default: Console.ResetColor(); break;
                            }
                        }

                        Console.Write(lines[line] + "  ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(); // spacing between rows
            }
        }

    }
}
