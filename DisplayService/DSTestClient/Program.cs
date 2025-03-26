#region Copyright
// Works OIB - Copyright (C) ASMPT GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASMPT and are supplied subject to license terms.
#endregion

#region using 
using System;
using System.Windows.Forms;
#endregion

namespace DSTestClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
