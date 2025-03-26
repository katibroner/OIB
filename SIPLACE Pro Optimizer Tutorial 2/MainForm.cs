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
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;

#region DOC_NAMESPACES

using Asm.As.Oib.SiplacePro.Optimizer.Contracts.Data;
using Asm.As.Oib.SiplacePro.Optimizer.Contracts.Faults;
using Asm.As.Oib.SiplacePro.Optimizer.Proxy.Business.Objects;

#endregion

#endregion

namespace OIB.Tutorial
{
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Start Optimization

        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _ListBox.Items.Clear();
                MsgOut(string.Format($"Start the integrity check for recipe '{this._TextBoxRecipe.Text}'"));
                Update();

                // Create a tcp binding and extend the timeouts
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                binding.CloseTimeout = TimeSpan.FromSeconds(10);
                binding.OpenTimeout = TimeSpan.FromSeconds(10);
                binding.SendTimeout = TimeSpan.FromMinutes(5);
                binding.ReliableSession.Enabled = true;
                binding.PortSharingEnabled = true;
                binding.MaxBufferPoolSize = 524288;
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.ReaderQuotas.MaxArrayLength = 2147483647;
                binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                binding.ReaderQuotas.MaxDepth = 2147483647;
                binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;

                #region DOC_IC

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //Optimizer.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                using (Optimizer optimizerProxy = new Optimizer(new EndpointAddress(_TextBoxOptimizerEndpoint.Text), binding))
                {
                    List<OptimizerMessage> icMessages;
                    int errorCount, warningCount, infoCount;

                    IntegrityCheckResult icResult = optimizerProxy.CheckIntegrity(
                        _TextBoxRecipe.Text, 
                        IntegrityCheckMode.AllowHeadstepRecalcuation,
                        ICPlacePosReparMode.RepAllowRepart,
                        out icMessages,
                        out errorCount, 
                        out warningCount, 
                        out infoCount);

                    if (icResult == IntegrityCheckResult.DataOk)
                        MsgOut("Integrity check finished successfully, data was not changed.");
                    if (icResult == IntegrityCheckResult.DataOkHeadStepsRecalculated)
                        MsgOut("Integrity check finished successfully, data was re-optimized.");
                    if (icResult == IntegrityCheckResult.DataNotOk)
                        MsgOut("Integrity check failed, data is inconsistent.");

                    MsgOut("Messages: ------------------------------");
                    foreach (OptimizerMessage message in icMessages)
                    {
                        MsgOut(string.Format("Type: {0} \tCode: {1} \tText: {2}",
                            message.MessageType,
                            message.MessageCode,
                            message.Message));
                    }
                }

                #endregion
            }
            catch (FaultException<SiplaceProOptimizerFault> fault)
            {
                MsgOut(string.Format(
                    "SERVICE ERRROR: Integrity check of recipe '{0}' on the service side: '{1}'", 
                    _TextBoxRecipe.Text, fault.Message));
            }
            catch (CommunicationException ex)
            {
                MsgOut(string.Format(
                    "COMMUNICATION ERRROR: Integrity check of recipe '{0}' on the service side: {1}", 
                    _TextBoxRecipe.Text, ex.Message));

            }
            catch (Exception ex)
            {
                MsgOut(string.Format(
                    "ERRROR: Integrity check of recipe '{0}': '{1}'", 
                    _TextBoxRecipe.Text, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Messages (message view)

        public delegate void MsgOutDelegate(string message);

        public void MsgOut(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MsgOutDelegate(MsgOut), new object[] { message });
            }
            else
            {
                _ListBox.Items.Add(message);

                // Scroll to the last entry
                _ListBox.SelectedIndex = _ListBox.Items.Count - 1;
            }
        }

        #endregion
    }
}