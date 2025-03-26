#region Copyright

// ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

#region Using

using System;
using System.Windows.Forms;

#endregion

namespace FeederExternalControlTutorial
{
    public partial class DetailedTextForm : Form
    {
        #region Constructors

        public DetailedTextForm()
        {
            InitializeComponent();
        }

        public DetailedTextForm(string message)
            : this()
        {
            Message = message;
        }

        #endregion

        #region Properties

        public string Message
        {
            get { return richTextBox.Text; }
            set { richTextBox.Text = value; }
        }

        #endregion

        #region Methods

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}