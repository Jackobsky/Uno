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
    }
}
