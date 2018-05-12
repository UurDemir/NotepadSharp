using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public static class RichTextBoxHelper
    {
        public  static void ClearHighlightes(this RichTextBox richTextBox)
        {

            var cursorIndex = richTextBox.SelectionStart;
            richTextBox.SelectionStart = 0;
            richTextBox.SelectionLength = richTextBox.Text.Length;
            richTextBox.SelectionBackColor = Color.White;
            richTextBox.SelectionColor = Color.Black;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionStart = cursorIndex;
        }
    }
}
