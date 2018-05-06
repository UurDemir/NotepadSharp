using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Extensions;

namespace TextEditor.Engine.NFA
{
    public class Nfa
    {
        private State _initialState;
        public Dictionary<int, State> States { get; set; } = new Dictionary<int, State>();

        public RegexMatch Accepts(string content, int startIndex = 0)
        {
            var begining = -1;
            var index = startIndex;
            var p = new Dictionary<int, State>();
            Dictionary<int, State> t;

            while (true)
            {
                p.AddOrReplace(_initialState.Id, _initialState);

                do
                {
                    t = new Dictionary<int, State>(p);
                    var pCount = 0;

                    do
                    {
                        var loopIndex = 0;
                        foreach (var state in p.ToList())
                        {
                            if (pCount > loopIndex++)
                                continue;

                            var emptyList = new Dictionary<int, State>();

                            var epsilons = EpsilonClosures(state.Value, ref emptyList);

                            foreach (var epsilon in epsilons)
                                p.AddOrReplace(epsilon.Key, epsilon.Value);

                            pCount++;

                        }
                    } while (pCount != p.Count );

                } while (!t.Compare(p));

                if (HasAcceptingStates(t) || content.Length == index + 1)
                    break;

                var character = content[index];

                t.Clear();

                foreach (var state in p)
                {
                    foreach (var destination in state.Value.Destinations)
                    {
                        if (destination.Value == character)
                        {
                            if (state.Key == _initialState.Id)
                                begining = index;

                            t.AddOrReplace(destination.Key.Id, destination.Key);
                        }
                    }
                }

                if (HasAcceptingStates(t))
                    break;

                p = new Dictionary<int, State>(t);
                index++;
            }

            return HasAcceptingStates(t) ? new RegexMatch(begining, index, content.Substring(begining, index - begining + 1)) : null;
        }

        public static Dictionary<int, State> EpsilonClosures(State state,ref  Dictionary<int, State> neighborhoods)
        {
            neighborhoods.AddOrReplace(state.Id, state);

            foreach (var destination in state.Destinations)
            {
                if (destination.Value == State.Epsilon && destination.Key.Id != state.Id)
                {
                    EpsilonClosures(destination.Key,ref  neighborhoods);
                }
            }

            return neighborhoods;
        }

        public static Nfa SingleCharacter(char character)
        {
            var nfa = new Nfa();

            var initialState = new State(nfa.States);
            var finalStates = new State(nfa.States);

            finalStates.IsAccepting = true;

            initialState.AddDestination(finalStates, character);

            nfa._initialState = initialState;

            return nfa;
        }

        public static Nfa Merge(Nfa nfa1, Nfa nfa2)
        {
            var nfa = new Nfa();

            nfa.States = new Dictionary<int, State>(nfa1.States);

            foreach (var state in nfa2.States)
                nfa.States.AddOrReplace(state.Key, state.Value);

            var initialState = new State(nfa.States);
            nfa._initialState = initialState;

            initialState.AddDestination(nfa1._initialState, State.Epsilon);
            initialState.AddDestination(nfa2._initialState, State.Epsilon);

            return nfa;
        }

        public static Nfa Link(Nfa nfa1, Nfa nfa2)
        {
            var nfa = new Nfa();

            nfa.States = new Dictionary<int, State>(nfa1.States);

            foreach (var state in nfa2.States)
                nfa.States.AddOrReplace(state.Key, state.Value);

            nfa._initialState = nfa1._initialState;

            foreach (var state in nfa1.States)
            {
                if (state.Value.IsAccepting)
                {
                    state.Value.IsAccepting = false;
                    state.Value.AddDestination(nfa2._initialState, State.Epsilon);
                }
            }

            return nfa;
        }

        public static Nfa Star(Nfa nfa)
        {
            var newNfa = new Nfa();

            newNfa.States = nfa.States;

            newNfa._initialState = nfa._initialState;

            foreach (var state in newNfa.States)
            {
                if (state.Value.IsAccepting)
                    state.Value.AddDestination(newNfa._initialState, State.Epsilon);
            }

            newNfa._initialState.IsAccepting = true;

            return newNfa;
        }

        public static Nfa PostfixToNfa(string postfixRegex)
        {
            var nfas = new Stack<Nfa>();

            while (postfixRegex.IsNotNullOrEmpty())
            {
                var currentCharacter = postfixRegex[0];
                postfixRegex = postfixRegex.Substring(1);

                switch (currentCharacter)
                {
                    case '&':
                        Nfa nfa1 = nfas.Pop();
                        Nfa nfa2 = nfas.Pop();
                        
                        nfas.Push(Link(nfa2, nfa1));

                        break;
                    case '|':
                        Nfa nfa3 = nfas.Pop();
                        Nfa nfa4 = nfas.Pop();

                        nfas.Push(Merge(nfa4, nfa3));

                        break;
                    case '*':
                        Nfa nfa5 = nfas.Pop();

                        nfas.Push(Star(nfa5));

                        break;
                    default:
                        Nfa nfa6 = SingleCharacter(currentCharacter);
                        nfas.Push(nfa6);
                        break;
                }
            }

            return nfas.Peek();
        }

        private static bool HasAcceptingStates(Dictionary<int, State> states)
        {
            return states.Any(state => state.Value.IsAccepting);
        }
    }
}
