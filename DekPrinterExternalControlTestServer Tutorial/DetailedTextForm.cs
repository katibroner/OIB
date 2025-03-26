#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;
using System.Windows.Forms;

#endregion

namespace DekPrinterExternalControlTestServer
{
    public partial class DetailedTextForm : Form
    {
        public DetailedTextForm()
        {
            InitializeComponent();
        }

        public DetailedTextForm(string message)
            : this()
        {
            Message = message;
        }

        public string Message
        {
            get { return richTextBox.Text; }
            set { richTextBox.Text = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}