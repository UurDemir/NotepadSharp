using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditor.Engine;
using TextEditor.Extensions;

namespace TextEditor
{
    public partial class frmSearch : Form
    {
        private RichTextBox _txtContent;

        public frmSearch(RichTextBox txtContent)
        {
            this._txtContent = txtContent;
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var content = _txtContent.Text;
            var regexExpression = txtRegex.Text;

            if (regexExpression.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter regex expression for search !", "Regex Expression can not be empty !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            _txtContent.SuspendLayout();

            var cursorIndex = _txtContent.SelectionStart;

            _txtContent.ClearHighlightes();

            var regexEngine = new RegexEngine();

            if (regexEngine.SetRegex(regexExpression))
            {
                var matches = regexEngine.Matches(content);

                foreach (var regexMatch in matches)
                {
                    _txtContent.Select(regexMatch.StartIndex, regexMatch.EndIndex - regexMatch.StartIndex + 1);
                    _txtContent.SelectionBackColor = SearchOptions.HighlightColor;
                    _txtContent.SelectionColor = SearchOptions.TextColor;
                    _txtContent.SelectionLength = 0;
                }

                lblStatus.Text = $"{matches.Count()} matches are found !";
            }

            _txtContent.SelectionStart = cursorIndex;

            _txtContent.ResumeLayout();

            SearchOptions.Counter = 0;

        }



        private void frmSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            _txtContent.ClearHighlightes();
        }
    }
}
