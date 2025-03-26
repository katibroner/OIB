#region Copyright

// // ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// // All rights reserved.
// //
// // The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using System;
using System.Windows.Forms;

#endregion

namespace OIB.Tutorial
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}