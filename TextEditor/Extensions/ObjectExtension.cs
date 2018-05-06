using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object obj) => obj == null;
        public static bool IsNotNull(this object obj) => obj != null;
    }
}
