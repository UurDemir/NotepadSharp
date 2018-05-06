using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }


        public static bool IsNotNullOrEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }
    }
}
