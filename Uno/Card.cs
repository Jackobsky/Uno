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

        // This is only for debugging/plain text
        public override string ToString()
        {
            return $"{color} {value}";
        }

        // Proper colored output
        public void PrintColored()
        {
            switch (color)
            {
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "Black":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray; // make wilds stand out
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            Console.Write($"{value}");
            Console.ResetColor(); // reset after printing
        }
    }
}
