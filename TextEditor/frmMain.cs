﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditor.Engine;

namespace TextEditor
{
    public partial class frmMain : Form
    {
        private bool _searching = false;
        public frmMain()
        {

            
    
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            if (_searching)
                return;

            var regexEngine = new RegexEngine();
            if (regexEngine.SetRegex("(Two)"))
            {
                var matches = regexEngine.Matches(txtContent.Text);

                _searching = true;
                foreach (var regexMatch in matches)
                {
                    txtContent.Select(regexMatch.StartIndex, regexMatch.EndIndex - regexMatch.StartIndex + 1);
                    txtContent.SelectionBullet = true;
                    txtContent.SelectionLength = 0;
                }
                _searching = false;
            }


        }
    }
}