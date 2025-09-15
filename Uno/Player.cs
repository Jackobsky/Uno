using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    public class Player
    {
        public List<Card> Hand = new List<Card>();
        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}
