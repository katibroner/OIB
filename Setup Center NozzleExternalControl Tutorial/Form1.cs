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

namespace NozzleExternalControlTutorial
{
    public partial class Form1 : Form
    {
        #region Fields

        // This name is constant and must not be changed!!!
        private const string ExternalControlOibServiceNameDefaultValue = "SIPLACE.Nozzle.ExternalControl";

        private string _oldToolTipText;

        private ServiceHost _serviceHost;

        private Uri _siplaceTestNozzleExternalControlMexUri;

        private readonly DataSet _nozzleResultStatiSet;

        public readonly DataTable NozzleResultStatiTable;

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
                    _nozzleResultStatiSet = new DataSet();
                    _nozzleResultStatiSet.ReadXml(Filename);
                    NozzleResultStatiTable = _nozzleResultStatiSet.Tables[0];
                }
                catch (Exception)
                {
                    _nozzleResultStatiSet = new DataSet();
                }

                if (_nozzleResultStatiSet.Tables.Count == 0)
                {
                    string[] saColumns = {ColumnNozzleId, ColumnResultState, ColumnReason};
                    string[] saTypes = {"System.String", "System.Int32", "System.String"};
                    string[] saDefaults = {"", "0", ""};
                    NozzleResultStatiTable = new DataTable("Nozzle Result Stati");
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
                        NozzleResultStatiTable.Columns.Add(dataColumn);
                    }
                    _nozzleResultStatiSet.Tables.Add(NozzleResultStatiTable);
                }

                // Automatically generate the DataGridView columns.
                dataGridViewNozzleResultStati.AutoGenerateColumns = true;

                // Automatically resize the visible rows.
                dataGridViewNozzleResultStati.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                // Set the DataGridView control's border.
                dataGridViewNozzleResultStati.BorderStyle = BorderStyle.Fixed3D;

                // Put the cells in edit mode when user enters them.
                dataGridViewNozzleResultStati.EditMode = DataGridViewEditMode.EditOnEnter;

                // Set up the data source.
                var bindingSource = new BindingSource {DataSource = NozzleResultStatiTable};
                dataGridViewNozzleResultStati.DataSource = bindingSource;
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
                Product = "Nozzle External Control Test Server Product",
                Grouped = true,
                MetadataEndpoints = new List<Uri> {_siplaceTestNozzleExternalControlMexUri}
            };

            return serviceDescription;
        }

        private void OnButtonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNozzleIdToAdd.Text))
            {
                MsgOut("The Nozzle id is empty!");
                return;
            }

            foreach (DataRow row in NozzleResultStatiTable.Rows)
            {
                if (row[ColumnNozzleId].Equals(textBoxNozzleIdToAdd.Text))
                {
                    MsgOut("The Nozzle id already exists in the table!");
                    return;
                }
            }

            DataRow newRow = NozzleResultStatiTable.NewRow();
            newRow[ColumnNozzleId] = textBoxNozzleIdToAdd.Text;
            newRow[ColumnResultState] = numericUpDownResultStateToAdd.Value;
            newRow[ColumnReason] = textBoxReason.Text;
            NozzleResultStatiTable.Rows.Add(newRow);
        }

        private void OnButtonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewNozzleResultStati.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow selectedRow in dataGridViewNozzleResultStati.SelectedRows)
                {
                    dataGridViewNozzleResultStati.Rows.RemoveAt(selectedRow.Index);
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
                var externalControlImpl = new NozzleExternalControlServiceImplementation(this);
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
                        _siplaceTestNozzleExternalControlMexUri = new Uri(se.ListenUri.AbsoluteUri);
                    }
                    if (se.Contract.ContractType == typeof(INozzleExternalControl))
                    {
                        textBoxExternalControlEndpoint.Text = se.ListenUri.AbsoluteUri;
                    }
                }
                MsgOut("Nozzle External Control Service was started on: " + textBoxExternalControlEndpoint.Text);

                // Activate usage of secure communication
                Session.UseSecureEndpoints = true;

                // Set up the OIB Core connection
                Session.CurrentSessionEndpoint = $"net.tcp://{textBoxCoreComputer.Text}:{textBoxTcpPort.Text}/Asm.As.Oib.ConfigurationManager";

                #region Use Client Authentication

                // Usage of Client Authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service
                    // **** IMPORTANT ****
                    // The certificate value must be set to that one selected during OIB installation
                    Session.SetCertificateForClientAuthentication(StoreLocation.CurrentUser, X509FindType.FindBySubjectDistinguishedName, StoreName.My, "CN=ASM.SW.Client");
                }

                #endregion

                // Now register the service in the OIB. Only one service should be registered as "SIPLACE.Nozzle.ExternalControl"
                // for our tutorial
                ServiceLocator serviceLocator = Session.CurrentSession.ServiceLocator;
                ServiceDescription serviceDescription = GetExternalControlServiceLocatorDescription();

                // We look for already registered services and then we remove them before we create the new Service Registration
                List<ServiceDescription> registeredServices = serviceLocator.FindServices(new ServiceMatchCriteria {ServiceVersion = "1.0.0.0",
                    ServiceName = ExternalControlOibServiceNameDefaultValue, //"SIPLACE.Nozzle.ExternalControl"
                    Product = "Nozzle External Control Test Server Product",
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
                MsgOut("Nozzle External Control Service was successfully registered: " + serviceDescription);

                #endregion //DOC_StartAndRegister

                textBoxCoreComputer.Enabled = textBoxTcpPort.Enabled = buttonStart.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MsgOut("Nozzle External Control Server has got exception when starting up.");
            }
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            if (sender is ListBox listBox)
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
                _nozzleResultStatiSet.WriteXml(Filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Saving the data to file '{Filename}' failed: '{ex.Message}'!");
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
                MsgOut("Nozzle External Control Server has errors." + ex);
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

        public static string ColumnNozzleId = "NozzleId";
        public static string ColumnResultState = "ResultState";
        public static string ColumnReason = "Reason";
        private static string Filename = "NozzleResultStati.xml";

        #endregion
    }
}