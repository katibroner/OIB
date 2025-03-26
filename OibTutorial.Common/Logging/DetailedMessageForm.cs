#region Copyright
//-----------------------------------------------------------------------
// <copyright file="DetailedMessageForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Windows.Forms;

namespace OIB.Tutorial.Common.Logging
{
    public partial class DetailedMessageForm : Form
    {
        public DetailedMessageForm(string message)
        {
            InitializeComponent();
            richTextMessage.Text = message;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
