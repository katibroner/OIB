#region Copyright

// ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using Asm.As.Oib.Common.Utilities;
using Asm.As.Oib.ConfigurationManager.Proxy.Architecture.Objects;
using Asm.As.Oib.ConfigurationManager.Proxy.Business.Objects;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;
using ServiceDescription = Asm.As.Oib.ConfigurationManager.Proxy.Business.Objects.ServiceDescription;

#endregion

namespace FeederExternalControlTutorial
{
    public partial class Form1 : Form
    {
        #region Fields

        // This name is constant and must not be changed!!!
        public const string ExternalControlOibServiceNameDefaultValue = "SIPLACE.Feeder.ExternalControl";

        private string _oldToolTipText;

        private ServiceHost _serviceHost;

        private Uri _siplaceTestFeederExternalControlMexUri;

        public DataSet FeederResultStatiSet;

        public DataTable FeederResultStatiTable;

        #endregion

        #region Constructors

        public Form1()
        {
            InitializeComponent();

            textBoxCoreComputer.Text = EndpointHelper.MachineName;

            try
            {
                try
                {
                    FeederResultStatiSet = new DataSet();
                    FeederResultStatiSet.ReadXml(Filename);
                    FeederResultStatiTable = FeederResultStatiSet.Tables[0];
                }
                catch (Exception)
                {
                    FeederResultStatiSet = new DataSet();
                }

                if (FeederResultStatiSet.Tables.Count == 0)
                {
                    string[] saColumns = {ColumnFeederId, ColumnResultState, ColumnReason};
                    string[] saTypes = {"System.String", "System.Int32", "System.String"};
                    string[] saDefaults = {"", "0", ""};
                    FeederResultStatiTable = new DataTable("Feeder Result Stati");
                    for (var i = 0; i < saColumns.Length; i++)
                    {
                        var dataColumn = new DataColumn
                        {
                            DataType = Type.GetType(saTypes[i]),
                            AllowDBNull = false,
                            Caption = saColumns[i],
                            ColumnName = saColumns[i],
                            DefaultValue = saDefaults[i]
                        };
                        FeederResultStatiTable.Columns.Add(dataColumn);
                    }
                    FeederResultStatiSet.Tables.Add(FeederResultStatiTable);
                }

                // Automatically generate the DataGridView columns.
                dataGridViewFeederResultStati.AutoGenerateColumns = true;

                // Automatically resize the visible rows.
                dataGridViewFeederResultStati.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                // Set the DataGridView control's border.
                dataGridViewFeederResultStati.BorderStyle = BorderStyle.Fixed3D;

                // Put the cells in edit mode when user enters them.
                dataGridViewFeederResultStati.EditMode = DataGridViewEditMode.EditOnEnter;

                // Set up the data source.
                var bindingSource = new BindingSource {DataSource = FeederResultStatiTable};
                dataGridViewFeederResultStati.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MsgOut("Got exception when populating data grid");
            }
        }

        #endregion

        #region Methods

        internal void MsgOut(string message)
        {
            var item = new ListBoxItem(message);
            listBoxMessages.Items.Add(item);
            if (m_ToolStripMenuItemShowLast.Checked)
            {
                listBoxMessages.TopIndex = listBoxMessages.Items.Count - 1;
            }
        }

        private ServiceDescription GetExternalControlServiceLocatorDescription()
        {
            var serviceDescription = new ServiceDescription
            {
                ServiceVersion = "1.0.0.0",
                ServiceName = ExternalControlOibServiceNameDefaultValue,
                Product = "Feeder External Control Test Server Product",
                Grouped = true,
                MetadataEndpoints = new List<Uri> {_siplaceTestFeederExternalControlMexUri}
            };

            return serviceDescription;
        }

        private void OnButtonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFeederIdToAdd.Text))
            {
                MsgOut("The feeder id is empty!");
                return;
            }

            foreach (DataRow row in FeederResultStatiTable.Rows)
            {
                if (row[ColumnFeederId].Equals(textBoxFeederIdToAdd.Text))
                {
                    MsgOut("The feeder id already exists in the table!");
                    return;
                }
            }

            DataRow newRow = FeederResultStatiTable.NewRow();
            newRow[ColumnFeederId] = textBoxFeederIdToAdd.Text;
            newRow[ColumnResultState] = numericUpDownResultStateToAdd.Value;
            newRow[ColumnReason] = textBoxReason.Text;
            FeederResultStatiTable.Rows.Add(newRow);
        }

        private void OnButtonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewFeederResultStati.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow selectedRow in dataGridViewFeederResultStati.SelectedRows)
                {
                    dataGridViewFeederResultStati.Rows.RemoveAt(selectedRow.Index);
                }
            }
            else
            {
                MsgOut("Please select a row!");
            }
        }

        private void OnButtonStart_Click(object sender, EventArgs e)
        {
            try
            {

#region DOC_StartAndRegister
                // Set up the service host for the callback service
                var externalControlImpl = new FeederExternalControlServiceImplementation(this);
                _serviceHost = new ServiceHost(externalControlImpl);
                foreach (ServiceEndpoint se in _serviceHost.Description.Endpoints)
                {
                    se.Address = EndpointHelper.ReplaceLocalhost(se.Address);
                }

                EndpointHelper.OpenServiceHost(_serviceHost);

                foreach (ServiceEndpoint se in _serviceHost.Description.Endpoints)
                {
                    if (se.Contract.ContractType == typeof(IMetadataExchange))
                    {
                        _siplaceTestFeederExternalControlMexUri = new Uri(se.ListenUri.AbsoluteUri);
                    }
                    if (se.Contract.ContractType == typeof(ISiplaceFeederExternalControl))
                    {
                        textBoxExternalControlEndpoint.Text = se.ListenUri.AbsoluteUri;
                    }
                }
                MsgOut("Feeder External Control Service was started on: " + textBoxExternalControlEndpoint.Text);

                // Activate secure communication
                Session.UseSecureEndpoints = true;

                // Set up the OIB Core connection
                Session.CurrentSessionEndpoint = string.Format("net.tcp://{0}:{1}/Asm.As.Oib.ConfigurationManager", textBoxCoreComputer.Text, textBoxTcpPort.Text);

                #region Use Client Authentication

                // Usage of Client Authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication
                    //Session.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                // Now register the service in the OIB. Only one service should be registered as "SIPLACE.Feeder.ExternalControl"
                // for our tutorial
                ServiceLocator serviceLocator = Session.CurrentSession.ServiceLocator;
                ServiceDescription serviceDescription = GetExternalControlServiceLocatorDescription();

                // We look for already registered services and then we remove them before we create the new Service Registration
                List<ServiceDescription> registeredServices = serviceLocator.FindServices(new ServiceMatchCriteria {ServiceVersion = "1.0.0.0",
                    ServiceName = ExternalControlOibServiceNameDefaultValue,
                    Product = "Feeder External Control Test Server Product",
                    Grouped = true,
                    CheckProduct = true,
                    CheckGrouped = true,
                    CheckServiceName = true,
                    CheckServiceVersion = true
                });

                foreach (ServiceDescription service in registeredServices)
                {
                    serviceLocator.UnregisterService(service);
                    MsgOut("Unregistered old Service: " + service);
                }

                serviceLocator.RegisterService(serviceDescription);
                MsgOut("Feeder External Control Service was successfully registered: " + serviceDescription);

#endregion //DOC_StartAndRegister

                textBoxCoreComputer.Enabled = textBoxTcpPort.Enabled = buttonStart.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MsgOut("Feeder External Control Server has got exception when starting up.");
            }
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox != null)
            {
                string selectedItem = listBox.SelectedItem.ToString();
                var form = new DetailedTextForm(selectedItem);
                form.Show(this);
            }
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                FeederResultStatiSet.WriteXml(Filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Saving the data to file '{0}' failed: '{1}'!", Filename, ex.Message));
            }
        }

        // Unregister the service from OIB upon closing the GUI.
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // Service is deregistered here since this is just a GUI test application.
            // In a real-world scenario, typically the client is a windows service or equivalent.
            // In that case the registration would be done when installing the client product and deregistration
            // when uninstalling the product.
            try
            {
                if (_serviceHost != null)
                {
                    ServiceLocator serviceLocatorProxy = Session.CurrentSession.ServiceLocator;
                    ServiceDescription serviceDescription = GetExternalControlServiceLocatorDescription();
                    serviceLocatorProxy.UnregisterService(serviceDescription);
                }
            }
            catch (Exception ex)
            {
                MsgOut("Feeder External Control Server has errors." + ex);
            }
            finally
            {
                EndpointHelper.CloseCommunicationObject(_serviceHost);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is ListBox)) return;
            var mouseMoveListBox = (ListBox) sender;
            var point = new Point(e.X, e.Y);
            int hoverIndex = mouseMoveListBox.IndexFromPoint(point);
            if (hoverIndex >= 0 && hoverIndex < mouseMoveListBox.Items.Count)
            {
                string str = mouseMoveListBox.Items[hoverIndex].ToString();
                if (str.Length > 2048)
                {
                    str = str.Substring(0, 2048) + "...";
                }

                if (_oldToolTipText != str)
                {
                    toolTip.SetToolTip(listBoxMessages, str);
                    _oldToolTipText = str;
                }
            }
            else
            {
                _oldToolTipText = null;
                toolTip.RemoveAll();
            }
        }

        private void OnToolStripMenuItemClearMessages_Click(object sender, EventArgs e)
        {
            listBoxMessages.Items.Clear();
        }

        #endregion

        #region Nested Types

        private class ListBoxItem
        {
            #region Constructors

            public ListBoxItem(string message)
            {
                Message = message;
            }

            #endregion

            #region Properties

            private string Message { get; }

            #endregion

            #region Methods

            #region Override

            public override string ToString()
            {
                return (Message ?? "<NULL>");
            }

            #endregion

            #endregion
        }

        #endregion

        #region Constants

        public static string ColumnFeederId = "FeederId";
        public static string ColumnResultState = "ResultState";
        public static string ColumnReason = "Reason";
        public static string Filename = "FeederResultStati.xml";

        #endregion
    }
}