//-----------------------------------------------------------------------
// <copyright file="UserControlPerformance.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

using System.Windows.Forms;

namespace OIB.Tutorial
{
    public partial class UserControlPerformance : UserControl
    {
        public UserControlPerformance()
        {
            InitializeComponent();
        }

        public bool DlgParamFastPerformanceLevel2
        {
            get { return m_cbxPerformanceLevel2Fast.Checked; }
            set { m_cbxPerformanceLevel2Fast.Checked = value; }
        }

    }
}