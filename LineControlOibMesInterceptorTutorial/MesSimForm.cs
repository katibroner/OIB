#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace LineControlOibMesInterceptorTutorial
{
    public partial class MesSimForm : Form
    {
        #region Fields
        private static MesSimForm _form;
        private MesInterceptorWebService _service;
        private ServiceHost _serviceHost;
        private Uri _mexMesEndpoint;
        private static SynchronizationContext _uiSyncContext;
        #endregion

        #region Construction
        public MesSimForm()
        {
            InitializeComponent();

            _form = this;
        }
        #endregion

        #region internal Properties

        internal static bool VerificationResult_Continue
        {
            get { return _form.cbVerificationResult_Continue.Checked; }
        }

        internal static string VerificationResult_Reason
        {
            get { return _form.cbVerificationResult_Reason.Text; }
        }

        internal static int BlockCallbackSec
        {
            get
            {
                int nBlockSec;
                if (int.TryParse(_form._blockCallSecComboBox.SelectedItem as string, out nBlockSec))
                    return nBlockSec;
                return 0;
            }
        }

        #endregion
       
        #region Methods

        internal static void AddTrace(string message)
        {
            try
            {
                SendOrPostCallback callback =
                    delegate { _form.AddTraceInternal(message); };

                _uiSyncContext.Post(callback, message);
            }
            catch
            {
                // ignored
            }
        }

        internal static bool InitializedPingOK
        {
            get { return _form.cbInitializedPingOK.Checked; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _uiSyncContext = SynchronizationContext.Current;
                cbInitializedPingOK.Checked = true;
                cbVerificationResult_Continue.Checked = true;
                cbVerificationResult_Reason.SelectedItem = "";
                _blockCallSecComboBox.SelectedIndex = 1;

                _tbOIBCoreComputer.Text = Environment.MachineName;

               

                AddTraceInternal(string.Format("Each call is blocked for {0} sec", BlockCallbackSec));
            }
            catch(Exception ex)
            {
                AddTraceInternal(string.Format("Form1_Load(), Exception {0}", ex.Message));
            }
        }

        private void cbVerificationResult_Continue_CheckedChanged(object sender, EventArgs e)
        {
            if(cbVerificationResult_Continue.Checked)
            {
                cbVerificationResult_Reason.SelectedItem = "";
            }
        }

        private void MesSimFor_FormClosed(object sender, FormClosedEventArgs e)
        {
            _btnStopCallback_Click(sender, null);
        }

 
        private void AddTraceInternal(string message)
        {
            tbTrace.Text = string.Format("{0}\r\n{1}: {2}", tbTrace.Text, DateTime.Now.ToLongTimeString(), message);
        }

        private void btnClearTrace_Click(object sender, EventArgs e)
        {
            tbTrace.Text = "";
        }

        private void BlockCallSecComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddTraceInternal(string.Format("Each call is blocked for {0} sec", BlockCallbackSec));
        }

        private static EndpointAddress ReplaceLocalhost(EndpointAddress address)
        {
            UriBuilder builder = new UriBuilder(address.Uri);
            if(builder.Host.Equals("localhost"))
            {
                builder.Host = Environment.MachineName.ToLower();
            }
            return new EndpointAddress(builder.Uri);
        }
        #endregion

        private void _btnStartCallback_Click(object sender, EventArgs e)
        {
            try
            {
                #region  DOC_StartServiceHost
                AddTraceInternal("Starting the service host...");

                // create the service class which implements the DownloadInterceptor OIB Contract
                _service = new MesInterceptorWebService();

                // create a service host for our contract implementation
                _serviceHost = new ServiceHost(_service);

                // Replace the localhost by the real computer name when hosting the service
                foreach (var endpoint in _serviceHost.Description.Endpoints)
                {
                    endpoint.Address = ReplaceLocalhost(endpoint.Address);

                    if (endpoint.Contract.Name.ToLower() == "imetadataexchange")
                    {
                        _mexMesEndpoint = ReplaceLocalhost(endpoint.Address).Uri;
                        AddTraceInternal(string.Format("Service host started with mex endpoint {0}", _mexMesEndpoint));
                    }
                }

                // open the web service
                _serviceHost.Open();
                #endregion

                // register at oib
                OIBServiceLocatorHelper.RegisterServiceAtOIB(Constants.InterceptorServiceName, "Line Control MES Interceptor Simulator",
                    "A sample application implementing the ILineControlMesInterceptor interface",
                    _mexMesEndpoint, _tbOIBCoreComputer.Text);
                AddTraceInternal("Service successfully registered at OIB");
                _btnStartCallback.Enabled = false;
                _btnStopCallback.Enabled = true;
                _tbOIBCoreComputer.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Got exception when starting callback: " + ex);
            }
           
        }
        private void _btnStopCallback_Click(object sender, EventArgs e)
        {
            try
            {
                OIBServiceLocatorHelper.UnregisterServiceAtOIB(Constants.InterceptorServiceName,
                    "Line Control MES Interceptor Simulator",
                    "A sample application implementing the ILineControlMesInterceptor interface",
                    _mexMesEndpoint, _tbOIBCoreComputer.Text);

                // shut down the servicehost
                if (_serviceHost != null)
                {
                    _serviceHost.Close();
                }
                _btnStartCallback.Enabled = true;
                _btnStopCallback.Enabled = false;
                _tbOIBCoreComputer.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception!", MessageBoxButtons.OK);
            }
        }
    }
}