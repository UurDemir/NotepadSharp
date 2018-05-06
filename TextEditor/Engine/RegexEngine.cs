using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditor.Engine.NFA;
using TextEditor.Extensions;

namespace TextEditor.Engine
{
    public class RegexEngine
    {
        private readonly RegexConverter _converter;
        private Nfa _nonFiniteAutomata;

        public RegexEngine()
        {
            _converter = new RegexConverter();
        }

        public bool SetRegex(string regex)
        {
            try
            {
                _converter.SetInfixRegex(regex);
                _nonFiniteAutomata = Nfa.PostfixToNfa(_converter.PostfixRegex);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Please check your regex.", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public RegexMatch Match(string content, int startIndex)
        {
            return _nonFiniteAutomata.Accepts(content, startIndex);
        }

        public IEnumerable<RegexMatch> Matches(string content)
        {
            var match = Match(content, 0);
            while (match.IsNotNull())
            {
                yield return match;

                if (match.EndIndex + 1 == content.Length)
                    yield break;

                match = Match(content, match.EndIndex + 1);
            }
        }

    }
}
