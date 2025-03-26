#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;
using System.Diagnostics;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;
using OibLoc = www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using OibLocData = www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;
using MesModel = www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Data;

#endregion

namespace DekPrinterExternalControlTestServer
{
    public partial class DekPrinterExternalControlTestForm : Form
    {
        #region Fields

        private string _oldToolTipText;

        // This name is constant and cannot be changed!!!
        public const string MesOibServiceNameDefaultValue = "DEK.Printer.ExternalControl";

        private readonly Uri _siplaceTestDekPrinterExternalControlMexUri;

        private readonly ServiceHost _serviceHost;

        #endregion

        #region Constructor

        public DekPrinterExternalControlTestForm()
        {
            InitializeComponent();
            OibLoc.IServiceLocator serviceLocatorProxy = null;
            try
            {
                var dekPrinterExternalControlImpl = new DekPrinterExternalControlImpl(this);
                _serviceHost = new ServiceHost(dekPrinterExternalControlImpl);
                foreach(var se in _serviceHost.Description.Endpoints)
                {
                    se.Address = ReplaceLocalhost(se.Address);
                }
                _serviceHost.Open();

                foreach(var se in _serviceHost.Description.Endpoints)
                {
                    if(se.Name.Equals("SiplaceTestDekPrinterExternalControlMex"))
                    {
                        _siplaceTestDekPrinterExternalControlMexUri = new Uri(se.ListenUri.AbsoluteUri);
                    }
                    if(se.Contract.Name == typeof(IDekPrinterExternalControl).Name)
                    {
                        m_TextBoxMesEndpoint.Text = se.ListenUri.AbsoluteUri;
                    }
                }
                MsgOut("Dek Printer External Control Service was started");

                serviceLocatorProxy = GetServiceLocatorProxy();
                var serviceDescription = GetMesServiceLocatorDescription();
                serviceLocatorProxy.RegisterService(serviceDescription);
              
                MsgOut("Dek Printer External Control Service registered at OIB Service Locator with Name '" + serviceDescription.ServiceName + "'");

                comboBoxVerifyToolVerificationStatus.SelectedIndex = 0;
                comboBoxVerifyMaterialVerificationStatus.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MsgOut("Dek Printer External Control Server has errors");
            }
            finally
            {
                ICommunicationObject communicationObject = serviceLocatorProxy as ICommunicationObject;
                if (communicationObject != null)
                {
                    communicationObject.Abort();
                }
            }
        }

      
        #endregion

        #region Private Class

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

            #region Override

            public override string ToString()
            {
                return (Message ?? "<NULL>");
            }

            #endregion
        }

        #endregion

        #region Properties

        public bool IsMesThrowException
        {
            get { return m_CheckBoxMesThrowException.Checked; }
            set { m_CheckBoxMesThrowException.Checked = value; }
        }

        public int IsMesTimeout
        {
            get { return (int) m_NumericUpDownMesSleep.Value; }
            set { m_NumericUpDownMesSleep.Value = value; }
        }

        public VerifyToolValues VerifyToolValues
        {
            get
            {
                var verifyToolValues = new VerifyToolValues
                {
                    Message = textBoxVerifyToolMessage.Text,
                    VerificationStatus = comboBoxVerifyToolVerificationStatus.SelectedIndex
                };
                return verifyToolValues;
            }
        }

        public VerifyMaterialValues VerifyMaterialValues
        {
            get
            {
                var verifyMaterialValues = new VerifyMaterialValues
                {
                    Message = textBoxVerifyMaterialMessage.Text,
                    VerificationStatus = comboBoxVerifyMaterialVerificationStatus.SelectedIndex
                };
                return verifyMaterialValues;
            }
        }

        #endregion

        #region Methods

        private EndpointAddress ReplaceLocalhost(EndpointAddress address)
        {
            UriBuilder builder = new UriBuilder(address.Uri);
            builder.Host = Environment.MachineName.ToLower();
            return new EndpointAddress(builder.Uri);
        }


        private OibLoc.IServiceLocator GetServiceLocatorProxy()
        {
            var factory = new ChannelFactory<OibLoc.IServiceLocator>("OIB_ServiceLocator");

            var serviceLocatorProxy = factory.CreateChannel();

            m_textBoxOibServiceLocatorEndpoint.Text = factory.Endpoint.Address.Uri.AbsoluteUri;

            return serviceLocatorProxy;
        }

        private OibLocData.ServiceDescription GetMesServiceLocatorDescription()
        {
            var serviceDescription = new OibLocData.ServiceDescription
            {
                ServiceVersion = "1.0.0.0",
                ServiceName = MesOibServiceNameDefaultValue,
                Product = "DEK Printer External Control Test Server Product",
                Grouped = true,
                MetadataEndpoints = new[] {_siplaceTestDekPrinterExternalControlMexUri}
            };

            return serviceDescription;
        }

        public void MsgOut(string message)
        {
            var item = new ListBoxItem(message);
            listBox.Items.Add(item);
            if(m_ToolStripMenuItemShowLast.Checked)
            {
                listBox.TopIndex = listBox.Items.Count - 1;
            }
        }

        private void listBox_MouseMove(object sender, MouseEventArgs e)
        {
            var box = sender as ListBox;
            if(box != null)
            {
                var mouseMoveListBox = box;
                var point = new Point(e.X, e.Y);
                var hoverIndex = mouseMoveListBox.IndexFromPoint(point);
                if(hoverIndex >= 0 && hoverIndex < mouseMoveListBox.Items.Count)
                {
                    var str = mouseMoveListBox.Items[hoverIndex].ToString();
                    if(str.Length > 2048)
                    {
                        str = str.Substring(0, 2048) + "...";
                    }

                    if(_oldToolTipText != str)
                    {
                        toolTip1.SetToolTip(listBox, str);
                        _oldToolTipText = str;
                    }
                }
                else
                {
                    _oldToolTipText = null;
                    toolTip1.RemoveAll();
                }
            }
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            var box = sender as ListBox;
            if(box != null)
            {
                var doubelClickListBox = box;
                var form = new DetailedTextForm(doubelClickListBox.SelectedItem.ToString());
                form.Show(this);
            }
        }

        private void m_ToolStripMenuItemClearMessages_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
        }

        private void DekPrinterExternalControlTestForm_Load(object sender, EventArgs e)
        {
            comboBoxVerifyToolVerificationStatus.SelectedIndex = 0;
            comboBoxVerifyMaterialVerificationStatus.SelectedIndex = 0;
        }


        // Unregister the service from OIB upon closing the GUI.
        private void DekPrinterExternalControlTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Service is deregistered here since this is just a GUI test application.
            // In a real-world scenario, typically the client is a windows service or equivalent.
            // In that case the registration would be done when installing the client product and deregistration
            // when uninstalling the product.
            OibLoc.IServiceLocator serviceLocatorProxy = null;
            try
            {
                if(_serviceHost != null)
                {
                    serviceLocatorProxy = GetServiceLocatorProxy();
                    var serviceDescription = GetMesServiceLocatorDescription();
                    serviceLocatorProxy.UnregisterService(serviceDescription);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            finally
            {
                ICommunicationObject communicationObject = serviceLocatorProxy as ICommunicationObject;
                if (communicationObject != null)
                {
                    communicationObject.Abort();
                }
            }
            if(_serviceHost != null)
            {
                _serviceHost.Close();
            }
        }
        #endregion
    }
}