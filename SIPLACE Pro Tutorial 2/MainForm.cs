#region Copyright

// ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows.Forms;

#region DOC_NAMESPACES

using ArchObj = Asm.As.Oib.SiplacePro.Proxy.Architecture.Objects;
using BizObj = Asm.As.Oib.SiplacePro.Proxy.Business.Objects;
using BizTypes = Asm.As.Oib.SiplacePro.Proxy.Types;

#endregion

#endregion

namespace OIB.Tutorial
{
    public partial class mainForm : Form
    {
        #region Fields

        private bool _Connected;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="mainForm"/> class.
        /// </summary>
        public mainForm()
        {
            InitializeComponent();

            _StatusStrip.Items.Clear();
            _StatusStrip.Items.Add("Not connected to SIPLACE Pro.");
        }

        #endregion

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                #region DOC_SIMPLE_ADDRESS_HANDLING

                // Set the base address for callbacks (events) of the SPI adapter
                ArchObj.SessionManager.CurrentCallbackEndpointBase =
                    string.Format("net.tcp://{0}:1406/MyApplication", Environment.MachineName);

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

                // Set the address as the current session endpoint 
                ArchObj.Session session = ArchObj.SessionManager.GetOrCreateSession(new EndpointAddress(new Uri(_TextBoxSiplaceProAddress.Text)));

                #endregion

                _Connected = session.IsServiceAlive;
                SetStatusText("Connected to SIPLACE Pro.");
            }
            catch (Exception ex)
            {
                SetStatusText("Not connected to SIPLACE Pro.");
                MessageBox.Show("Error while connecting SIPLACE Pro: \n\n" + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SetStatusText(string text)
        {
            _StatusStrip.Items.Clear();
            _StatusStrip.Items.Add(text);
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_Connected)
                {
                    MessageBox.Show("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                #region DOC_CREATE

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //ArchObj.Session.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                ArchObj.Session session = ArchObj.Session.CurrentSession;

                // Create or get the folder 'OIB-SIPLACE Pro Tutorial'
                const string folderFullTypePath = "Component:OIB-SIPLACE Pro-Tutorials";
                BizObj.Folder folder = session.GetFolder(folderFullTypePath);
                bool createdFolder = false;
                if (folder == null)
                {
                    folder = session.CreateFolder(folderFullTypePath);
                    createdFolder = true;
                }

                // Create component only in case object does not already exist.
                const string componentFullName = @"OIB-SIPLACE Pro-Tutorials\MyComponent";
                ArchObj.Identity id = session.GetIdentity(componentFullName,
                    BizTypes.ObjectServerType.Component);

                if (id == null)
                {
                    BizObj.Component component = (BizObj.Component)session.CreateObject(folder, @"MyComponent");
                    SetStatusText(string.Format("Component '{0}' {1} created", component.FullPath, createdFolder ? " with new Folder" : string.Empty));

                }
                else
                    SetStatusText(string.Format("Component '{0}' already exists", id.FullPath));

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating component: \n\n" + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_Connected)
                {
                    MessageBox.Show("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                #region DOC_READ

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //ArchObj.Session.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                ArchObj.Session session = ArchObj.Session.CurrentSession;

                const string fullTypePath = @"Component:OIB-SIPLACE Pro-Tutorials\MyComponent";
                BizObj.Component component = session.GetObject(fullTypePath)
                    as BizObj.Component;

                if (component != null)
                    SetStatusText(string.Format("Component '{0}' successfully read.", component.FullPath));
                else
                    SetStatusText(string.Format("Component '{0}' does not exist.", fullTypePath));

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating component: \n\n" + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_Connected)
                {
                    MessageBox.Show("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                #region DOC_DELETE

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //ArchObj.Session.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                ArchObj.Session session = ArchObj.Session.CurrentSession;

                const string fullPath = @"OIB-SIPLACE Pro-Tutorials\MyComponent";

                // Create object 'MyComponent' on folder 'OIB-SIPLACE Pro Tutorial'
                //   if it does not already exist.
                ArchObj.Identity id = session.GetIdentity(fullPath, BizTypes.ObjectServerType.Component);

                if (id != null)
                {
                    session.DeleteObject(id);
                    BizObj.Folder componentFolder = session.GetFolder("Component:OIB-SIPLACE Pro-Tutorials");
                    bool deletedFolder = false;
                    if (componentFolder != null)
                    {
                        List<ArchObj.Identity> childIdentities = session.IdentityList.GetChildIdentities(componentFolder);
                        List<BizObj.Folder> childFolders = session.IdentityList.GetChildFolders(componentFolder);
                        if (childFolders.Count == 0 && childIdentities.Count == 0)
                        {
                            session.DeleteFolder(componentFolder);
                            deletedFolder = true;
                        }
                    }
                    SetStatusText(string.Format("Component '{0}' {1} successfully deleted.", id.FullPath, deletedFolder? "and folder" : string.Empty));
                }
                else
                    SetStatusText(string.Format("Component '{0}' does not exist.", fullPath));

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating component: \n\n" + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_Connected)
                {
                    MessageBox.Show("Connect SIPLACE Pro first.");
                    return;
                }

                Cursor = Cursors.WaitCursor;

                #region DOC_UPDATE

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //ArchObj.Session.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                ArchObj.Session session = ArchObj.Session.CurrentSession;

                const string fullTypePath = @"Component:OIB-SIPLACE Pro-Tutorials\MyComponent";
                BizObj.Component component = session.GetObject(fullTypePath)
                    as BizObj.Component;

                if (component == null)
                {
                    SetStatusText(string.Format("Component '{0}' does not exist.", fullTypePath));
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
                    BizObj.Barcode barcode = new BizObj.Barcode(session);
                    barcode.BarcodeString = "1234";
                    barcode.Filter = "1111";
                    component.Barcodes.Add(barcode);

                    // Assign another server object
                    BizObj.ComponentShape componentShape = session.GetObject(@"OIB-SIPLACE Pro-Tutorials\CS 1",
                        BizTypes.ObjectServerType.ComponentShape) as BizObj.ComponentShape;
                    component.ComponentShape = componentShape;

                    session.Commit();
                    SetStatusText(string.Format("Component '{0}' updated.", fullTypePath));
                }
                catch
                {
                    session.Rollback();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating component: \n\n" + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_Connected)
                ArchObj.SessionManager.ReleaseAllSessions();

            base.OnClosed(e);
        }
    }
}