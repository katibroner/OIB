//-----------------------------------------------------------------------
// <copyright file="SPro4MainForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespaces
using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;

using OIB.Tutorial.Common.Logging;

using ArchObj = Asm.As.Oib.SiplacePro.Proxy.Architecture.Objects;
using BizObj = Asm.As.Oib.SiplacePro.Proxy.Business.Objects;
using BizTypes = Asm.As.Oib.SiplacePro.Proxy.Types;
using Asm.As.Oib.SiplacePro.Proxy.Architecture.Notifications.SubscriptionService;
#endregion

namespace OIB.Tutorial.SiplacePro._4
{
    public partial class SPro4MainForm : Form, ILog
    {
        #region Fields
        private const string ComponentFolderFullTypePath = @"Component:OIB\Tutorial\SPI\4";
        private const string ComponentName = "MyComponent";
        private const string ComponentPath = @"OIB\Tutorial\SPI\4\" + ComponentName;
        private const string ComponentFullTypePath = "Component:" + ComponentPath;

        private ArchObj.Session _sessionLeft;
        private ArchObj.Session _sessionRight;
        #endregion

        #region Constructor
        public SPro4MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Implementation of ILog
        public void InfoMessage(string message)
        {
            loggerListView.InfoMessage(message);
        }

        private void InfoMessage(string message, params object[] formatArgs)
        {
            InfoMessage(string.Format(message, formatArgs));
        }

        public void ErrorMessage(string message, Exception ex = null)
        {
            loggerListView.ErrorMessage(message, ex);
        }

        public void WarnMessage(string message, Exception ex = null)
        {
            loggerListView.WarnMessage(message, ex);
        }

        public void DebugMessage(string message, Exception ex = null)
        {
            loggerListView.DebugMessage(message, ex);
        }
        #endregion

        #region UI Event Handlers

        #region Left Session
        private void buttonLeftConnectSiplacePro_Click(object sender, EventArgs e)
        {
            if (_sessionLeft != null)
                return;
            _sessionLeft = CreateSpiSession(textBoxLeftSiplaceProAdapterAddress.Text,
                                            string.Format("net.tcp://{0}:1406/SiplaceproTutorial4Left", Environment.MachineName));
            buttonLeftConnectSiplacePro.BackColor = _sessionLeft != null ? Color.Green : Color.Red;
        }

        private void buttonLeftSubscribePropertiesUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                if (_sessionLeft == null)
                {
                    ErrorMessage("LEFT Session - Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                SubscribeToAllPropertiesUpdates(_sessionLeft);

                buttonLeftUnsubscribePropertiesUpdates.Enabled = true;
                buttonLeftSubscribePropertiesUpdates.Enabled = false;

                InfoMessage("LEFT Session - Successfully subscribed to property updates.");
            }
            catch (Exception ex)
            {
                ErrorMessage("LEFT Session - Error subscribing to property updates!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonLeftUnsubscribePropertiesUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                if (_sessionLeft == null)
                {
                    ErrorMessage("LEFT Session - Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                UnsubscribeToAllPropertiesUpdates(_sessionLeft);

                buttonLeftUnsubscribePropertiesUpdates.Enabled = false;
                buttonLeftSubscribePropertiesUpdates.Enabled = true;

                InfoMessage("LEFT Session - Successfully unsubscribed to property updates");
            }
            catch (Exception ex)
            {
                ErrorMessage("LEFT Session - Error unsubscribing to property updates!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonLeftCreateComponent_Click(object sender, EventArgs e)
        {
            if (_sessionLeft == null)
            {
                ErrorMessage("LEFT Session - Connect SIPLACE Pro first.");
                return;
            }

            CreateComponent(_sessionLeft);
        }

        private void buttonLeftReadComponent_Click(object sender, EventArgs e)
        {
            if (_sessionLeft == null)
            {
                ErrorMessage("LEFT Session - Connect SIPLACE Pro first.");
                return;
            }

            ReadComponent(_sessionLeft);
        }

        private void buttonLeftUpdateComponent_Click(object sender, EventArgs e)
        {
            if (_sessionLeft == null)
            {
                ErrorMessage("LEFT Session - Connect SIPLACE Pro first.");
                return;
            }
            UpdateComponent(_sessionLeft);
        }

        private void buttonLeftDeleteComponent_Click(object sender, EventArgs e)
        {
            if (_sessionLeft == null)
            {
                ErrorMessage("LEFT Session - Connect SIPLACE Pro first.");
                return;
            }
            DeleteComponent(_sessionLeft);
        }
        #endregion

        #region Right Session
        private void buttonRightConnectSiplacePro_Click(object sender, EventArgs e)
        {
            if (_sessionRight != null)
                return;
            _sessionRight = CreateSpiSession(textBoxRightSiplaceProAdapterAddress.Text,
                                             string.Format("net.tcp://{0}:1406/SiplaceproTutorial4Right", Environment.MachineName));
            buttonRightConnectSiplacePro.BackColor = _sessionRight != null ? Color.Green : Color.Red;
        }

        private void buttonRightSubscribePropertiesUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                if (_sessionRight == null)
                {
                    ErrorMessage("RIGHT Session - Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                SubscribeToAllPropertiesUpdates(_sessionRight);

                buttonRightUnsubscribePropertiesUpdates.Enabled = true;
                buttonRightSubscribePropertiesUpdates.Enabled = false;

                InfoMessage("RIGHT Session - Successfully subscribed to property updates.");
            }
            catch (Exception ex)
            {
                ErrorMessage("RIGHT Session - Error subscribing to property updates!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonRightUnsubscribePropertiesUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                if (_sessionRight == null)
                {
                    ErrorMessage("RIGHT Session - Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                UnsubscribeToAllPropertiesUpdates(_sessionRight);

                buttonRightUnsubscribePropertiesUpdates.Enabled = false;
                buttonRightSubscribePropertiesUpdates.Enabled = true;

                InfoMessage("RIGHT Session - Successfully unsubscribed to property updates");
            }
            catch (Exception ex)
            {
                ErrorMessage("RIGHT Session - Error unsubscribing to property updates!", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonRightCreateComponent_Click(object sender, EventArgs e)
        {
            if (_sessionRight == null)
            {
                ErrorMessage("RIGHT Session - Connect SIPLACE Pro first.");
                return;
            }

            CreateComponent(_sessionRight);
        }

        private void buttonRightReadComponent_Click(object sender, EventArgs e)
        {
            if (_sessionRight == null)
            {
                ErrorMessage("RIGHT Session - Connect SIPLACE Pro first.");
                return;
            }

            ReadComponent(_sessionRight);
        }

        private void buttonRightUpdateComponent_Click(object sender, EventArgs e)
        {
            if (_sessionRight == null)
            {
                ErrorMessage("RIGHT Session - Connect SIPLACE Pro first.");
                return;
            }
            UpdateComponent(_sessionRight);
        }

        private void buttonRightDeleteComponent_Click(object sender, EventArgs e)
        {
            if (_sessionRight == null)
            {
                ErrorMessage("RIGHT Session - Connect SIPLACE Pro first.");
                return;
            }
            DeleteComponent(_sessionRight);
        }
        #endregion

        private void SPro4MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dispose the both session when closing the form
            if (_sessionLeft != null && _sessionLeft != null)
            {
                _sessionLeft.Dispose();
                _sessionLeft = null;
            }

            if (_sessionRight != null && _sessionRight != null)
            {
                _sessionRight.Dispose();
                _sessionRight = null;
            }
        }
        
        #endregion

        #region Session's Event Handlers
        private void SubscriptionService_SubscribedPropertyUpdate(object sender, PropertyUpdateArgs args)
        {
            try
            {
                // See what session this was fired from
                string sessionInfo = (_sessionLeft != null && _sessionLeft.SessionId == args.SessionId) ? "LEFT Session" : "RIGHT Session";
                InfoMessage(string.Format("{3} - Property Update: '{0}', new value: '{1}', Identity: '{2}'", args.PropertyId, args.Value, args.Identity.FullPath, sessionInfo));
            }
            catch (Exception ex)
            {
                ErrorMessage("PropertyUpdate handler failed!", ex);
            }
        }

        #endregion

        #region Private Methods
        private ArchObj.Session CreateSpiSession(string adapterEndpoint, string callbackEndpoint)
        {
            ArchObj.Session session = null;
            try
            {
                Cursor = Cursors.WaitCursor;

                #region DOC_CreateSession
                // Set the base address for callbacks (events) of the SPI adapter
                ArchObj.SessionManager.CurrentCallbackEndpointBase = callbackEndpoint;

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //ArchObj.SessionManager.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                // Create the session
                session = ArchObj.SessionManager.GetOrCreateSession(new EndpointAddress(new Uri(adapterEndpoint)));
                #endregion

                InfoMessage("Successfully connected to SIPLACE Pro Adapter – '{0}'", session.EndpointAddress);
                InfoMessage("Session ID: {0}, Database ID: '{1}'", session.SessionId, session.DatabaseId);
                InfoMessage("SPI Version: '{0}', Model Version: '{1}', Product Version: '{2}'", 
                            session.SPIVersion, session.ModelVersion, session.ProductVersion);
                InfoMessage("OIB Adapter Version: '{0}', OPIB Proxy version: '{1}'", 
                            session.OibSiplaceProAdapterVersion, session.OibSiplaceProProxyVersion);
            }
            catch (Exception ex)
            {
                ErrorMessage(string.Format("Error while connecting SIPLACE Pro at '{0}'", adapterEndpoint), ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return session;
        }

        private void SubscribeToAllPropertiesUpdates(ArchObj.Session session)
        {
            session.SubscriptionService.SubscribedPropertyUpdate += SubscriptionService_SubscribedPropertyUpdate;
            foreach (var propertyid in
                Enum.GetValues(typeof(BizTypes.PropertyId)).Cast<BizTypes.PropertyId>().Where(propertyid =>
                        session.SubscriptionService.IsValidPropertyUpdate(propertyid)))
            {
                session.SubscriptionService.SubscribeToPropertyUpdate(propertyid);
            }
        }

        private void UnsubscribeToAllPropertiesUpdates(ArchObj.Session session)
        {
            // just unsubscribe all property updates
            foreach (var propertyid in
                Enum.GetValues(typeof(BizTypes.PropertyId)).Cast<BizTypes.PropertyId>().Where(propertyid =>
                                                                                               session.SubscriptionService.IsValidPropertyUpdate(propertyid)))
            {
                session.SubscriptionService.UnsubscribeToPropertyUpdate(propertyid);
            }

            session.SubscriptionService.SubscribedPropertyUpdate -= SubscriptionService_SubscribedPropertyUpdate;
        }

        private void CreateComponent(ArchObj.Session session)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Create or get the folder 'OIB-SIPLACE Pro Tutorial'
                var folder = session.GetFolder(ComponentFolderFullTypePath);
                if (folder == null)
                    folder = session.CreateFolder(ComponentFolderFullTypePath);

                // Create component only in case object does not already exist.
                var id = session.GetIdentity(ComponentPath, BizTypes.ObjectServerType.Component);

                if (id == null)
                {
                    var component = (BizObj.Component)session.CreateObject(folder, ComponentName);
                    InfoMessage("{0} - Component '{1}' created", session.AdapterHostName, component.FullPath);
                }
                else
                    InfoMessage("{0} - Component '{1}' already exists", session.AdapterHostName, id.FullPath);
            }
            catch (Exception ex)
            {
                ErrorMessage(string.Format("{0} - Error creating component!", session.AdapterHostName), ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ReadComponent(ArchObj.Session session)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var component = (BizObj.Component)session.GetObject(ComponentFullTypePath);
                if (component != null)
                    InfoMessage("{0} - Component '{1}' successfully read.", session.AdapterHostName, component.FullPath);
                else
                    WarnMessage(string.Format("{0} - Component '{1}' does not exist.", session.AdapterHostName, ComponentPath));

            }
            catch (Exception ex)
            {
                ErrorMessage(string.Format("{0} - Error creating component!", session.AdapterHostName), ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateComponent(ArchObj.Session session)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var component = (BizObj.Component)session.GetObject(ComponentFullTypePath);
                if (component == null)
                {
                    WarnMessage(string.Format("{0} - Component '{1}' does not exist.", session.AdapterHostName, ComponentPath));
                    return;
                }

                // Start transaction on the SPI
                session.StartTransaction();
                try
                {
                    // Set a simple property
                    component.NonPolarized = false;

                    // Add non server object barcode
                    component.Barcodes.Clear();
                    #region DOC_CreateNonServerObject
                    var barcode = new BizObj.Barcode(session) 
                                                { 
                                                    BarcodeString = "1234", 
                                                    Filter = "1111" 
                                                };
                    #endregion
                    component.Barcodes.Add(barcode);

                    // Assign another server object
                    var componentShape =
                        (BizObj.ComponentShape)session.GetObject(@"OIB-SIPLACE Pro-Tutorials\CS 1", BizTypes.ObjectServerType.ComponentShape);
                    component.ComponentShape = componentShape;

                    session.Commit();
                    InfoMessage("{0} - Component '{1}' updated.", session.AdapterHostName, component.FullPath);
                }
                catch (Exception ex)
                {
                    session.Rollback();
                    ErrorMessage(string.Format("{0} - Failed to update Component '{1}'. All changes are rolled back.", session.AdapterHostName, ComponentPath), ex);
                }

            }
            catch (Exception ex)
            {
                ErrorMessage(string.Format("{0} - Error updating component!", session.AdapterHostName), ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DeleteComponent(ArchObj.Session session)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Create object 'MyComponent' on folder 'OIB-SIPLACE Pro Tutorial'
                //   if it does not already exist.
                var id = session.GetIdentity(ComponentPath, BizTypes.ObjectServerType.Component);

                if (id != null)
                {
                    session.DeleteObject(id);
                    InfoMessage("{0} - Component '{1}' successfully deleted.", session.AdapterHostName, id.FullPath);
                }
                else
                    WarnMessage(string.Format("{0} - Component '{1}' does not exist.", session.AdapterHostName, ComponentPath));

            }
            catch (Exception ex)
            {
                ErrorMessage(string.Format("{0} - Error deleting component!", session.AdapterHostName), ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SPro4MainForm_Resize_1(object sender, EventArgs e)
        {
            splitterLogMessageWindow.SplitPosition = ClientRectangle.Height - (groupBoxLeftSession.Location.Y + groupBoxLeftSession.Size.Height + 9);
        }
        #endregion


    }
}
