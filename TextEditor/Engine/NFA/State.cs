using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Extensions;

namespace TextEditor.Engine.NFA
{
    public class State
    {
        public static char Epsilon = '$';
        public static int Counter = 0;
        public int Id { get; set; }

        public bool IsAccepting { get; set; }
        public Dictionary<State, char> Destinations { get; set; } = new Dictionary<State, char>();

        public State(Dictionary<int, State> states)
        {
            IsAccepting = false;
            Id = Counter++;
            states.Add(Id, this);
        }

        public void AddDestination(State destination, char symbol)
        {
                Destinations.AddOrReplace(destination, symbol);
        }
    }
}
