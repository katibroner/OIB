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
using System.Globalization;
using System.Windows.Forms;
using System.ServiceModel;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Data;

#endregion

namespace OIB.Tutorial
{
    public partial class mainForm : Form, ILog
    {
        #region Fields

        private const string s_SetupCenterServiceName = "SIPLACE.SetupCenter.MaterialControl";
        private const int _MaxMessagesCount = 250;
        private SetupCenterNotifyReceiver _SetupCenterNotifyReceiver;

        #endregion

        #region Constructor

        public mainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling

        #region DOC_MAINFORM_NotifyReceiver

        private void ButtonSubscribeToOibEvents_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                if (_SetupCenterNotifyReceiver == null)
                {
                    _SetupCenterNotifyReceiver = new SetupCenterNotifyReceiver(this, new Uri(textBoxEventing.Text), _cbUseClientAuthentication.Checked);
                }
            }
            catch (Exception ex)
            {
                _SetupCenterNotifyReceiver = null;
                MessageBox.Show("Error creating Setup Center message handling: " + ex.Message, "OIB Tutorial");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

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
                using (ServiceHelper<ISiplaceSetupCenter> setupCenterHelper = GetSetupCenterService())
                {
                    if (setupCenterHelper.Proxy.IsConnected())
                    {
                        _ToolStripStatusLabel.Text = "Setup Center is available.";
                    }
                    else
                    {
                        _ToolStripStatusLabel.Text = "Setup Center is NOT available.";
                    }
                }
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
                using (ServiceHelper<ISiplaceSetupCenter> setupCenterHelper =
                    GetSetupCenterService())
                {
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
                    PackagingUnitResult[] result = setupCenterHelper.Proxy.Create(new[] { pu });

                    if (result.Length > 0 && result[0].ResultState == 1)
                    {
                        _ToolStripStatusLabel.Text = "Packaging unit successfully imported";
                    }
                    else if (result.Length > 0 && result[0].ResultState == 2 && result[0].Messages.Length > 0)
                    {
                        _ToolStripStatusLabel.Text = string.Format("Packaging unit import failed, message: '{0}'", result[0].Messages[0].Message);
                    }
                    else
                    {
                        _ToolStripStatusLabel.Text = "Packaging unit import failed with a bad Setup Center result data";
                    }
                }
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
                using (ServiceHelper<ISiplaceSetupCenter> setupCenterHelper =
                    GetSetupCenterService(true))
                {
                    PackagingUnit pu = new PackagingUnit();
                    pu.UID = textBoxId.Text;

                    PackagingUnitResult[] result = setupCenterHelper.Proxy.Get(new [] { pu });

                    if (result.Length > 0 && result[0].ResultState == 1)
                    {
                        _ToolStripStatusLabel.Text = "Packaging unit successfully read";
                        textBoxComponent.Text = result[0].PackagingUnit.ComponentName;
                        textBoxComponent.Text = result[0].PackagingUnit.ComponentBarcode;
                        textBoxStartQuantity.Text = result[0].PackagingUnit.OriginalQuantity.ToString(CultureInfo.InvariantCulture);
                        textBoxQuantity.Text = result[0].PackagingUnit.Quantity.ToString(CultureInfo.InvariantCulture);
                        textBoxComment.Text = result[0].PackagingUnit.Comment;
                    }
                    else if (result.Length > 0 && result[0].ResultState == 2 && result[0].Messages.Length > 0)
                    {
                        _ToolStripStatusLabel.Text = string.Format("Packaging unit import failed, message: '{0}'", result[0].Messages[0].Message);
                    }
                    else
                    {
                        _ToolStripStatusLabel.Text = "Packaging unit import failed with a bad Setup Center result data";
                    }
                }
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
                using (ServiceHelper<ISiplaceSetupCenter> setupCenterHelper =
                    GetSetupCenterService(true))
                {
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
                    PackagingUnitResult[] result = setupCenterHelper.Proxy.Update(new [] { pu });

                    if (result.Length > 0 && result[0].ResultState == 1)
                    {
                        _ToolStripStatusLabel.Text = "Packaging unit successfully updated";
                    }
                    else if (result.Length > 0 && result[0].ResultState == 2 && result[0].Messages.Length > 0)
                    {
                        _ToolStripStatusLabel.Text = string.Format("Packaging unit update failed, message: '{0}'", result[0].Messages[0].Message);
                    }
                    else
                    {
                        _ToolStripStatusLabel.Text = "Packaging unit update failed with a bad Setup Center result data";
                    }
                }
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
                using (ServiceHelper<ISiplaceSetupCenter> setupCenterHelper =
                    GetSetupCenterService(true))
                {
                    PackagingUnit pu = new PackagingUnit();
                    pu.UID = textBoxId.Text;
                    PackagingUnitResult[] result = setupCenterHelper.Proxy.Delete(new [] { pu });

                    if (result.Length > 0 && result[0].ResultState == 1)
                    {
                        _ToolStripStatusLabel.Text = "Packaging unit successfully deleted";
                    }
                    else if (result.Length > 0 && result[0].ResultState == 2 && result[0].Messages.Length > 0)
                    {
                        _ToolStripStatusLabel.Text = string.Format("Packaging unit deletion failed, message: '{0}'", result[0].Messages[0].Message);
                    }
                    else
                    {
                        _ToolStripStatusLabel.Text = "Packaging unit update deletion with a bad Setup Center result data";
                    }
                }
            }
            catch (Exception ex)
            {
                Error("Error while delteting packaging unit.", ex);
            }

            Cursor = Cursors.Default;
        }

        private void ButtonLock_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                using (ServiceHelper<ISiplaceSetupCenter> setupCenterHelper =
                    GetSetupCenterService(true))
                {
                    PackagingUnit pu = new PackagingUnit();
                    pu.UID = textBoxId.Text;
                    setupCenterHelper.Proxy.Lock(new [] { pu }, "Material defect");

                    _ToolStripStatusLabel.Text = "Packaging unit successfully locked";
                }
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
                using (ServiceHelper<ISiplaceSetupCenter> setupCenterHelper =
                    GetSetupCenterService(true))
                {
                    PackagingUnit pu = new PackagingUnit();
                    pu.UID = textBoxId.Text;
                    setupCenterHelper.Proxy.Unlock(new [] { pu });

                    _ToolStripStatusLabel.Text = "Packaging unit successfully unlocked";
                }
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error while unlocking packaging unit: " + ex.Message;
            }

            Cursor = Cursors.Default;
        }

        #region DOC_MAINFORM_CLOSE
        
        protected override void OnClosed(EventArgs e)
        {
            if (_SetupCenterNotifyReceiver != null)
            {
                _SetupCenterNotifyReceiver.Dispose();
                _SetupCenterNotifyReceiver = null;
            }
            base.OnClosed(e);
        }

        #endregion

        #endregion

        #region Get Setup Center OIB Adapter

        private ServiceHelper<ISiplaceSetupCenter> GetSetupCenterService(bool bCheckConnected)
        {
           ServiceHelper<ISiplaceSetupCenter> setupCenterHelper =
                GetSetupCenterService();

            if (bCheckConnected)
            {
                if (!setupCenterHelper.Proxy.IsConnected())
                {
                    throw new ApplicationException("Setup Center application is not available.");
                }
            }
            return setupCenterHelper;
        }

        private ServiceHelper<ISiplaceSetupCenter> GetSetupCenterService()
        {
            // Get the OIB Setup Center Adapter MEX Uri.
            Uri setupCenterUri = OibDiscoveryHelper.GetServiceMexUri(
                new Uri(textBoxDiscoveryEndpoint.Text),
                s_SetupCenterServiceName,
                textBoxFactoryLayoutElementName.Text,
                textBoxFactoryLayoutElementType.Text,
                _cbUseClientAuthentication.Checked);

            return new ServiceHelper<ISiplaceSetupCenter>(setupCenterUri, _cbUseClientAuthentication.Checked);
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

        public void InfoMessage(string message)
        {
            listBoxMessages.Items.Add( message);
            while (listBoxMessages.Items.Count > _MaxMessagesCount)
                listBoxMessages.Items.Remove(0);
            listBoxMessages.SelectedIndex = listBoxMessages.Items.Count - 1;
        }

        public void ErrorMessage(string message, Exception ex)
        {
            listBoxMessages.Items.Add(string.Format("Error: {0}", message));
            listBoxMessages.Items.Add(ex.Message);
            listBoxMessages.Items.Add(ex.StackTrace);


            while (listBoxMessages.Items.Count > _MaxMessagesCount)
                listBoxMessages.Items.Remove(0);

            listBoxMessages.SelectedIndex = listBoxMessages.Items.Count - 1;
        }

        #endregion
    }
}