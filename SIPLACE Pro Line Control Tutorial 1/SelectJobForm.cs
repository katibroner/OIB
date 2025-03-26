//-----------------------------------------------------------------------
// <copyright file="SelectJobForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Objects;
using Asm.As.Oib.SiplacePro.Proxy.Types;
using System.ServiceModel;

namespace OIB.Tutorial
{
    public partial class SelectJobForm : Form
    {
        private readonly string _SiplaceProEndPointAddress;

        public string JobFullPath
        {
            get { return _TextBoxJobFullName.Text; }
        }

        public SelectJobForm(string siplaceProEndpointAddress)
        {
            _SiplaceProEndPointAddress = siplaceProEndpointAddress;
            if (String.IsNullOrEmpty(_SiplaceProEndPointAddress))
            {
                _SiplaceProEndPointAddress = "net.tcp://localhost:9500/Asm.As.Oib.SiplacePro/Secure";
            }

            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SessionManager.CurrentSessionEndpoint =
                    new EndpointAddress(new Uri(_SiplaceProEndPointAddress));
                SessionManager.CurrentCallbackEndpointBase =
                    string.Format("net.tcp://{0}:1406/MyApplication", Environment.MachineName);
                
                Identity identity = Session.CurrentSession.IdentityList.GetIdentity(JobFullPath, ObjectServerType.Job);
                Cursor = Cursors.Default;

                if (identity == null)
                {
                    MessageBox.Show(this, string.Format("The job '{0}' does not exist in SIPLACE Pro.", JobFullPath));
                }
                else
                {
                    Close();
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("An error has occured: {0}", ex.Message));
            }
        }
    }
}