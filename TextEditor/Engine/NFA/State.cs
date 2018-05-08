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
        public int Id { get; set; }

        public bool IsAccepting { get; set; }
        public List<KeyValuePair<State, char>> Destinations { get; set; } = new List<KeyValuePair<State, char>>();

        public State()
        {

        }

        public State(List<KeyValuePair<int, State>> states)
        {
            IsAccepting = false;
            Id = SearchOptions.Counter++;
            states.Add(new KeyValuePair<int, State>(Id, this));
        }

        public void AddDestination(State destination, char symbol)
        {
            Destinations.AddOrReplace(destination, symbol);
        }

        public State Clone()
        {
            return new State { Id = Id, IsAccepting = IsAccepting, Destinations = Destinations };
        }
    }
}
