using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Engine
{
    public class RegexMatch
    {
        public RegexMatch(int startIndex,int endIndex,string matchContent)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            MatchContent = matchContent;
        }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string MatchContent { get; set; }
    }
}
