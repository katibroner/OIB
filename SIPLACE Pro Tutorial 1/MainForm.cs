//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System;
using System.Windows.Forms;
using System.ServiceModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

#region DOC_NAMESPACES

using Asm.As.Oib.SiplacePro.Proxy.Architecture.Objects;

#endregion

#endregion

namespace OIB.Tutorial
{
    public partial class mainForm : Form
    {
        #region Constructor

        public mainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handling

        private void TestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _ToolStripStatusLabel.Text = string.Empty;
                Update();

                #region DOC_CONNECT_SPI

                Stopwatch sw = new Stopwatch();
                sw.Start();

                // Get the mex address of SPI OIB adapter
                Uri spiMexUri = OibDiscoveryHelper.GetServiceMexUri(
                    new Uri(_TextBoxDiscoveryEndpoint.Text), 
                    _LabelServiceName.Text, 
                    _LabelFactoryItemName.Text, 
                    _LabelFactoryItemType.Text,
                    _cbUseClientAuthentication.Checked);

                if (spiMexUri == null)
                {
                    MessageBox.Show("No SIPLACE Pro SPI adapter found for factory layout element production line 'Line 1'." + Environment.NewLine 
                        + "Make sure the element exists and OIB SIPLACE Pro SPI adapter is being assigned to it using OIB Configuration Editor.");
                    return;
                }

                // Convert the mex address to the tcp endpoint address
                string address = spiMexUri.ToString();
                address = address.Replace("/mex", "");  // remove the /mex uri path
                if (!address.ToLowerInvariant().EndsWith("secure")) address = $"{address}/Secure"; // secure endpoint for secure communication

                // Set the address as the current session endpoint 
                SessionManager.CurrentSessionEndpoint = new EndpointAddress(new Uri(address));

                // Set the base address for callbacks (events) of the SPI adapter
                SessionManager.CurrentCallbackEndpointBase = string.Format("net.tcp://{0}:1406/MyApplication", Environment.MachineName);

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //SessionManager.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                // Get the current session -> a new session is created for the new address
                Session session = Session.CurrentSession;

                sw.Stop();
                // Cheap check -> are we connected?
                if (session.IsConnected())
                {
                    _ToolStripStatusLabel.Text = string.Format("Service '{0}' successfully connected, time: '{1}'", 
                        address, sw.Elapsed.ToString());
                }
                else
                {
                    _ToolStripStatusLabel.Text = string.Format("Service '{0}' NOT connected", address);
                }

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show( "Error while discovering service: \n\n" + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            // Need to release all sessions, otherwise the process will not terminate
            // due to background threads inside each session.
            SessionManager.ReleaseAllSessions();
        }

        #endregion
    }
}