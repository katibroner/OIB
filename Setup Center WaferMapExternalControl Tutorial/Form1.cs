#region Copyright

// SIPLACE Setup Center - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace WaferMapExternalControlTutorial
{
    public partial class Form1 : Form
    {
        #region Fields

        // This name is constant and must not be changed!!!
        public const string ExternalControlOibServiceNameDefaultValue = "SIPLACE.WaferMap.ExternalControl";

        private readonly DirectoryInfo _waferMapDirectoryInfo = new DirectoryInfo($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\WaferMaps");
        private FileSystemWatcher _fileSystemWatcher = new FileSystemWatcher();
        private string _oldToolTipText;

        private ServiceHost _serviceHost;

        private Uri _siplaceTestWaferMapExternalControlMexUri;

        #endregion

        #region Constructors

        public Form1()
        {
            InitializeComponent();

            textBoxCoreComputer.Text = EndpointHelper.MachineName;
            Bitmap bmp = Properties.Resources.Wafer;
            Icon = Icon.FromHandle(bmp.GetHicon());

            try
            {
                // Automatically generate the DataGridView columns.
                dataGridViewWaferMapResultStati.AutoGenerateColumns = true;

                // Automatically resize the visible rows.
                dataGridViewWaferMapResultStati.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                // Set the DataGridView control's border.
                dataGridViewWaferMapResultStati.BorderStyle = BorderStyle.Fixed3D;

                // Put the cells in edit mode when user enters them.
                dataGridViewWaferMapResultStati.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridViewWaferMapResultStati.DataSource = new BindingSource {DataSource = new List<WaferMapDataSource>()};
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
                Product = "WaferMap External Control Test Server Product",
                Grouped = true,
                MetadataEndpoints = new List<Uri> {_siplaceTestWaferMapExternalControlMexUri}
            };

            return serviceDescription;
        }

        private void OnButtonStart_Click(object sender, EventArgs e)
        {
            try
            {
                #region DOC_StartAndRegister

                // Set up the service host for the callback service
                var externalControlImpl = new WaferMapExternalControlServiceImplementation(this);
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
                        _siplaceTestWaferMapExternalControlMexUri = new Uri(se.ListenUri.AbsoluteUri);
                    }
                    if (se.Contract.ContractType == typeof(IWaferMapExternalControl))
                    {
                        textBoxExternalControlEndpoint.Text = se.ListenUri.AbsoluteUri;
                    }
                }
                MsgOut("WaferMap External Control Service was started on: " + textBoxExternalControlEndpoint.Text);

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
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //Session.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                // Now register the service in the OIB. Only one service should be registered as "SIPLACE.WaferMap.ExternalControl"
                // for our tutorial
                ServiceLocator serviceLocator = Session.CurrentSession.ServiceLocator;
                ServiceDescription serviceDescription = GetExternalControlServiceLocatorDescription();

                // We look for already registered services and then we remove them before we create the new Service Registration
                List<ServiceDescription> registeredServices = serviceLocator.FindServices(new ServiceMatchCriteria
                {
                    ServiceVersion = "1.0.0.0",
                    ServiceName = ExternalControlOibServiceNameDefaultValue,
                    Product = "WaferMap External Control Test Server Product",
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
                MsgOut("WaferMap External Control Service was successfully registered: " + serviceDescription);

                #endregion

                textBoxCoreComputer.Enabled = textBoxTcpPort.Enabled = buttonStart.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MsgOut("WaferMap External Control Server has got exception when starting up.");
            }
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            if (sender is ListBox listBox)
            {
                ListBox doubelClickListBox = listBox;
                var form = new DetailedTextForm(doubelClickListBox.SelectedItem.ToString());
                form.Show(this);
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
                MsgOut("WaferMap External Control Server has errors." + ex);
            }
            finally
            {
                EndpointHelper.CloseCommunicationObject(_serviceHost);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StartWaferMapFileWatcher();
            }
            catch (Exception exception)
            {
                string summary = $"{MethodBase.GetCurrentMethod().Name} failed. Reason: {exception.Message}";
                MsgOut(summary);
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
                var str = mouseMoveListBox.Items[hoverIndex].ToString();
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

        private void StartWaferMapFileWatcher()
        {
            if (_waferMapDirectoryInfo.Exists)
            {
                _fileSystemWatcher.EnableRaisingEvents = false;
                _fileSystemWatcher.Path = _waferMapDirectoryInfo.FullName;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                _fileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess
                                                  | NotifyFilters.LastWrite
                                                  | NotifyFilters.FileName
                                                  | NotifyFilters.DirectoryName;

                // Only watch XML files.
                _fileSystemWatcher.Filter = "*.xml";

                // Add event handlers.
                _fileSystemWatcher.Changed += OnDirectoryChanged;
                _fileSystemWatcher.Created += OnDirectoryChanged;
                _fileSystemWatcher.Deleted += OnDirectoryChanged;
                _fileSystemWatcher.Renamed += OnDirectoryChanged;

                OnDirectoryChanged(null, null);
                // Begin watching.
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
            else
            {
                MsgOut($"Path '{_waferMapDirectoryInfo.FullName}' for available Wafer Maps definitions doesn't exists");
            }
        }

        private void OnDirectoryChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    var datasource = dataGridViewWaferMapResultStati.DataSource as List<WaferMapDataSource>;
                    datasource?.Clear();
                    var listWaferMaps = new List<WaferMapDataSource>();
                    FileInfo[] waferMapFiles = _waferMapDirectoryInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
                    foreach (FileInfo fileInfo in waferMapFiles)
                    {
                        if (fileInfo.Exists)
                        {
                            if (CanRead(fileInfo.FullName))
                            {
                                listWaferMaps.Add(new WaferMapDataSource
                                {
                                    Id = Path.GetFileNameWithoutExtension(fileInfo.Name),
                                    FullName = fileInfo.FullName,
                                    Content = File.ReadAllText(fileInfo.FullName)
                                });
                            }
                        }
                    }

                    // Set up the data source.
                    var bindingSource = new BindingSource {DataSource = listWaferMaps};
                    dataGridViewWaferMapResultStati.DataSource = bindingSource;
                }));
            }
            catch (Exception exception)
            {
                string summary = $"{MethodBase.GetCurrentMethod().Name} failed. Reason: {exception.Message}";
                MsgOut(summary);
            }
        }

        private bool CanRead(string filePath)
        {
            try
            {
                File.Open(filePath, FileMode.Open, FileAccess.Read).Dispose();
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                _fileSystemWatcher.EnableRaisingEvents = false;
                _fileSystemWatcher = null;
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void dataGridViewWaferMapResultStati_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewWaferMapResultStati.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewWaferMapResultStati.SelectedRows[0];
                if (selectedRow.DataBoundItem is WaferMapDataSource selectedWaferMap)
                {
                    listBoxMessages.Items.Add(selectedWaferMap.Content);
                }
            }
            else
            {
                MsgOut("Please select a row!");
            }
        }


        internal FileInfo GetWaferMapFile(string fileName)
        {
            if (dataGridViewWaferMapResultStati.DataSource is BindingSource dataSource)
            {
                fileName = fileName.ToLower().EndsWith(".xml") ? fileName : $"{fileName}.xml";
                var waferMapFiles = dataSource.DataSource as List<WaferMapDataSource>;
                WaferMapDataSource waferMapData = waferMapFiles?.FirstOrDefault(x => fileName.Equals($"{x.Id}.xml", StringComparison.InvariantCultureIgnoreCase));
                if (waferMapData != null)
                {
                    return new FileInfo(waferMapData.FullName);
                }
            }

            return null;
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

    }

    public class WaferMapDataSource
    {
        #region Properties

        [DisplayName("Wafer Map Id")]
        public string Id { get; set; }

        [Browsable(false)]
        public string FullName { get; set; }

        [Browsable(false)]
        public string Content { get; set; }

        #endregion
    }
}