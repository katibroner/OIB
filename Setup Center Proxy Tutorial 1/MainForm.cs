//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region using

using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;
using Asm.As.Oib.Common.Utilities;
using Asm.As.Oib.SiplaceSetupCenter.Contracts.Data;
using Asm.As.Oib.SiplaceSetupCenter.Contracts.Faults;
using Asm.As.Oib.SiplaceSetupCenter.Proxy.Business.Objects;

#endregion

namespace OIB.Tutorial
{
    public partial class mainForm : Form, ILog
    {
        #region Fields

        private const string s_SetupCenterServiceName = "SIPLACE.SetupCenter.MaterialControl";
        private SiplaceSetupCenter _setupCenterProxy;


        #endregion

        #region Constructor

        public mainForm()
        {
            InitializeComponent();

            string coreName = EndpointHelper.GetAppSettingString("Asm.As.Oib.SetupCenter.Proxy.CoreName");
            textBoxDiscoveryEndpoint.Text = @"http://" + coreName + @":1405/Asm.As.Oib.ServiceLocator/mex";
        }

        #endregion

        #region Event Handling

        private void ButtonCreateTutorialConfiguration_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                using (OibServiceLocatorHelper slh = new OibServiceLocatorHelper(new Uri(textBoxDiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
                {
                    using (OibConfigurationManagerHelper cmh =
                        new OibConfigurationManagerHelper(slh.GetMexUriForServiceName("ConfigurationManager"), _cbUseClientAuthentication.Checked))
                    {
                        cmh.ResetFactoryLayout();
                        slh.ResetConfiguration();

                        cmh.CreateTutorialFactoryLayout();
                        slh.CreateTutorialServiceConfiguration();
                    }
                }

                _ToolStripStatusLabel.Text = "Configuration successfully created";
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error while creating the OIB configuration: " + ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonTestConnection_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                _ToolStripStatusLabel.Text = string.Empty;
                statusStrip1.Update();
                
                #region DOC_TEST_SETUP_CENTER_CONNECTED

                ConnectionStateDetails connectionState = GetSetupCenterProxy().GetConnectionStateDetails();

                _ToolStripStatusLabel.Text = "Connection: " + connectionState;
                statusStrip1.Update();

                #endregion
            }
            catch (Exception ex)
            {
                Error("Error while creating the OIB configuration.", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                #region DOC_CREATE_PACKAGING_UNIT

                PackagingUnit pu = new PackagingUnit();
                pu.UID = textBoxId.Text;
                pu.ComponentName = textBoxComponent.Text;
                pu.ComponentBarcode = textBoxComponent.Text;

                int startQuantitiy;
                if (!int.TryParse(textBoxStartQuantity.Text, out startQuantitiy))
                {
                    MessageBox.Show("Start Quantity need to be a number", "OIB Tutorial");
                    return;
                }
                pu.OriginalQuantity = startQuantitiy;

                int quantity;
                if (!int.TryParse(textBoxQuantity.Text, out quantity))
                {
                    MessageBox.Show("Quantity need to be a number", "OIB Tutorial");
                    return;
                }
                pu.Quantity = quantity;

                pu.Comment = textBoxComment.Text;
                pu.MsdLevel = 1;
                PackagingUnitResult[] result = GetSetupCenterProxy().Create(new[] { pu });

                if (result.Length > 0 && result[0].ResultState == 1)
                {
                    _ToolStripStatusLabel.Text = "Packaging unit successfully imported";
                }
                else if (result.Length > 0 && result[0].ResultState == 2 && result[0].Messages.Count > 0)
                {
                    _ToolStripStatusLabel.Text = string.Format("Packaging unit import failed, message: '{0}'", result[0].Messages[0].Message);
                }
                else
                {
                    _ToolStripStatusLabel.Text = "Packaging unit import failed with a bad Setup Center result data";
                }

                #endregion
            }
            catch (Exception ex)
            {
                Error("Error while creating packaging unit.", ex);
            }

            Cursor = Cursors.Default;
        }

        private void ButtonRead_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                #region DOC_READ_PACKAGING_UNIT

                SiplaceSetupCenter proxy = GetSetupCenterProxy(true);
                PackagingUnit pu = new PackagingUnit();
                pu.UID = textBoxId.Text;

                PackagingUnitResult[] result = proxy.Get(new[] { pu });

                if (result.Length > 0 && result[0].ResultState == 1)
                {
                    _ToolStripStatusLabel.Text = "Packaging unit successfully read";
                    textBoxComponent.Text = result[0].PackagingUnit.ComponentName;
                    textBoxComponent.Text = result[0].PackagingUnit.ComponentBarcode;
                    textBoxStartQuantity.Text = result[0].PackagingUnit.OriginalQuantity.ToString(CultureInfo.InvariantCulture);
                    textBoxQuantity.Text = result[0].PackagingUnit.Quantity.ToString(CultureInfo.InvariantCulture);
                    textBoxComment.Text = result[0].PackagingUnit.Comment;
                }
                else if (result.Length > 0 && result[0].ResultState == 2 && result[0].Messages.Count > 0)
                {
                    _ToolStripStatusLabel.Text = string.Format("Packaging unit import failed, message: '{0}'", result[0].Messages[0].Message);
                }
                else
                {
                    _ToolStripStatusLabel.Text = "Packaging unit import failed with a bad Setup Center result data";
                }

                #endregion
            }
            catch (Exception ex)
            {
                Error("Error while reading packaging unit.", ex);
            }

            Cursor = Cursors.Default;
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                #region DOC_UPDATE_PACKAGING_UNIT

                SiplaceSetupCenter proxy = GetSetupCenterProxy(true);

                PackagingUnit pu = new PackagingUnit();
                pu.UID = textBoxId.Text;
                pu.ComponentName = textBoxComponent.Text;
                pu.ComponentBarcode = textBoxComponent.Text;
                int startQuantitiy;
                if (!int.TryParse(textBoxStartQuantity.Text, out startQuantitiy))
                {
                    MessageBox.Show("Start Quantity need to be a number", "OIB Tutorial");
                    return;
                }
                pu.OriginalQuantity = startQuantitiy;

                int quantity;
                if (!int.TryParse(textBoxQuantity.Text, out quantity))
                {
                    MessageBox.Show("Quantity need to be a number", "OIB Tutorial");
                    return;
                }
                pu.Quantity = quantity;

                pu.Comment = textBoxComment.Text;
                pu.MsdLevel = 1;
                PackagingUnitResult[] result = proxy.Update(new[] { pu });

                if (result.Length > 0 && result[0].ResultState == 1)
                {
                    _ToolStripStatusLabel.Text = "Packaging unit successfully updated";
                }
                else if (result.Length > 0 && result[0].ResultState == 2 && result[0].Messages.Count > 0)
                {
                    _ToolStripStatusLabel.Text = string.Format("Packaging unit update failed, message: '{0}'", result[0].Messages[0].Message);
                }
                else
                {
                    _ToolStripStatusLabel.Text = "Packaging unit update failed with a bad Setup Center result data";
                }


                #endregion
            }
            catch (Exception ex)
            {
                Error("Error while updating packaging unit.", ex);
            }

            Cursor = Cursors.Default;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                #region DOC_DELETE_PACKAGING_UNIT

                SiplaceSetupCenter proxy = GetSetupCenterProxy(true);
                PackagingUnit pu = new PackagingUnit();
                pu.UID = textBoxId.Text;
                PackagingUnitResult[] result = proxy.Delete(new[] { pu });

                if (result.Length > 0 && result[0].ResultState == 1)
                {
                    _ToolStripStatusLabel.Text = "Packaging unit successfully deleted";
                }
                else if (result.Length > 0 && result[0].ResultState == 2 && result[0].Messages.Count > 0)
                {
                    _ToolStripStatusLabel.Text = string.Format("Packaging unit deletion failed, message: '{0}'", result[0].Messages[0].Message);
                }
                else
                {
                    _ToolStripStatusLabel.Text = "Packaging unit update deletion with a bad Setup Center result data";
                }

                #endregion
            }
            catch (Exception ex)
            {
                Error("Error while deleting packaging unit.", ex);
            }

            Cursor = Cursors.Default;
        }

        private void ButtonLock_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                #region DOC_LOCK_PACKAGING_UNIT

                SiplaceSetupCenter proxy = GetSetupCenterProxy(true);
                PackagingUnit pu = new PackagingUnit();
                pu.UID = textBoxId.Text;
#pragma warning disable 618
                proxy.Lock(new[] { pu }, "Material defect");
#pragma warning restore 618

                _ToolStripStatusLabel.Text = "Packaging unit successfully locked";

                #endregion
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error while locking packaging unit: " + ex.Message;
            }

            Cursor = Cursors.Default;
        }

        private void ButtonUnLock_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                #region DOC_UNLOCK_PACKAGING_UNIT

                SiplaceSetupCenter proxy = GetSetupCenterProxy(true);
                PackagingUnit pu = new PackagingUnit();
                pu.UID = textBoxId.Text;
#pragma warning disable CS0618 // Type or member is obsolete
                proxy.Unlock(new[] { pu });
#pragma warning restore CS0618 // Type or member is obsolete

                _ToolStripStatusLabel.Text = "Packaging unit successfully unlocked";

                #endregion
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error while unlocking packaging unit: " + ex.Message;
            }

            Cursor = Cursors.Default;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_setupCenterProxy != null)
            {
                _setupCenterProxy.Dispose();
            }
        }

        #endregion

        #region Get Setup Center OIB Adapter

        private SiplaceSetupCenter GetSetupCenterProxy(bool bCheckConnected)
        {
            SiplaceSetupCenter setupCenterHelper = GetSetupCenterProxy();

            if (bCheckConnected)
            {
                if (!setupCenterHelper.IsConnected())
                {
                    throw new ApplicationException("Setup Center application is not available.");
                }
            }
            return setupCenterHelper;
        }

        private SiplaceSetupCenter GetSetupCenterProxy()
        {
            if (_setupCenterProxy == null)
            {
                // Get the OIB Setup Center Adapter MEX Uri.
                Uri setupCenterMexUri = OibDiscoveryHelper.GetServiceMexUri(
                    new Uri(textBoxDiscoveryEndpoint.Text),
                    s_SetupCenterServiceName,
                    textBoxFactoryLayoutElementName.Text,
                    textBoxFactoryLayoutElementType.Text,
                    _cbUseClientAuthentication.Checked);
                string scEndpoint = setupCenterMexUri.ToString().Replace(@"/mex", "");

                // Activate secure communication, thereto Check if secure endpoint is used
                var strEndpoint = !scEndpoint.ToLowerInvariant().EndsWith("secure")
                    ? $"{scEndpoint}Secure"
                    : scEndpoint;

                #region Use Client Authentication

                // Usage of Client Authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //SiplaceSetupCenter.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                _setupCenterProxy = new SiplaceSetupCenter(new EndpointAddress(strEndpoint), EndpointHelper.CreateBindingFromEndpointString(strEndpoint));
            }
            return _setupCenterProxy;
        }

        #endregion

        #region ILog Members

        public void Info(string message)
        {
            _ToolStripStatusLabel.Text = message;
        }

        public void Error(string message)
        {
            _ToolStripStatusLabel.Text = "ERROR :" + message;
        }

        public void Error(string message, Exception ex)
        {
            string messageBoxContent = message;

            if (ex != null)
            {
                if (ex is FaultException<SiplaceSetupCenterFault>)
                {
                    messageBoxContent += "\n\nOIB Setup Center Adapter threw a FAULT.";
                }
                messageBoxContent += "\n\n" + ex.Message;
                messageBoxContent += "\n\n" + ex.StackTrace;

                if (ex.InnerException != null)
                {
                    messageBoxContent += "\n\nInner Exception: ";
                    if (ex.InnerException is FaultException<SiplaceSetupCenterFault>)
                    {
                        messageBoxContent += "\n\nOIB Setup Center Adapter threw a FAULT.";
                    }
                    messageBoxContent += "\n\n" + ex.InnerException.Message;
                    messageBoxContent += "\n\n" + ex.InnerException.StackTrace;
                }

                MessageBox.Show(messageBoxContent, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}