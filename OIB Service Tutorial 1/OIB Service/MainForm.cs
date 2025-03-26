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
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;


#region DOC_NAMESPACES


#endregion

#endregion

namespace OIB.Tutorial.Service
{
    public partial class mainForm : Form
    {
        #region Fields

        private ServiceHost _ServiceHost;

        #endregion

        #region Constructor

        public mainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handling

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_ServiceHost != null)
            {
                try
                {
                    _ServiceHost.Close();
                }
                catch
                {
                    _ServiceHost.Abort();
                }
                _ServiceHost = null;
            }
            base.OnFormClosed(e);
        }

        #endregion

        private void ButtonStartService_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (_ServiceHost != null)
                    ButtonStopService_Click(this, new EventArgs());

                _ServiceHost = new ServiceHost(typeof(HelloWorldServiceImplementation));
                _ServiceHost.Open();

                _ssServiceState.Items.Clear();
                _ssServiceState.Items.Add("Service is running.");
            }
            catch (Exception ex)
            {
                _ssServiceState.Items.Clear();
                _ssServiceState.Items.Add("An error has occurred opening the Web service.");
                MessageBox.Show("An error has occurred opening the Web service: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }

        private void ButtonStopService_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (_ServiceHost != null)
                {
                    _ServiceHost.Close();
                    _ServiceHost = null;

                    _ssServiceState.Items.Clear();
                    _ssServiceState.Items.Add("Service stopped.");
                }
            }
            catch (Exception ex)
            {
                _ssServiceState.Items.Clear();
                _ssServiceState.Items.Add("An error has occurred closing the Web service.");
                MessageBox.Show("An error has occurred closing the Web service: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                IServiceLocator serviceLocatorProxy = GetServiceLocatorProxy();
                #region DOC_REGISTER

                try
                {
                    serviceLocatorProxy.RegisterService(GetServiceDescription());
                }
                finally
                {
                    CloseCommunicationObject(serviceLocatorProxy);
                }
                #endregion
                MessageBox.Show("The service was successfully registered.", "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred registering the service: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }

        private void ButtonUnregister_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                IServiceLocator serviceLocatorProxy = GetServiceLocatorProxy();
                #region DOC_UNREGISTER
                try
                {
                    serviceLocatorProxy.UnregisterService(GetServiceDescription());
                }
                finally
                {
                    CloseCommunicationObject(serviceLocatorProxy);
                }

                #endregion
                MessageBox.Show("The service was successfully unregistered.", "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred unregistering the service: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }

        private void AdjustBindingParameters(WSHttpBinding bind)
        {
            if (bind != null)
            {
                bind.AllowCookies = false;
                bind.BypassProxyOnLocal = true;
                bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
                bind.OpenTimeout = new TimeSpan(0, 0, 10);
                bind.CloseTimeout = new TimeSpan(0, 0, 10);
                bind.SendTimeout = new TimeSpan(0, 1, 0);
                bind.MaxBufferPoolSize = 524288;
                bind.MaxReceivedMessageSize = 2147483647;
                bind.ReaderQuotas.MaxArrayLength = 2147483647;
                bind.ReaderQuotas.MaxBytesPerRead = 2147483647;
                bind.ReaderQuotas.MaxDepth = 2147483647;
                bind.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                bind.ReaderQuotas.MaxStringContentLength = 2147483647;
                bind.ReliableSession.Enabled = false;
                bind.TransactionFlow = false;
                bind.UseDefaultWebProxy = false;

                // Use message security
                bind.Security.Mode = SecurityMode.Message;

                // Usage of client authentication binding properties
                bind.Security.Message.ClientCredentialType = _cbUseClientAuthentication.Checked ? MessageCredentialType.Certificate : MessageCredentialType.None;
            }
        }

        private IServiceLocator GetServiceLocatorProxy()
        {
            #region DOC_GET_SL_PROXY

            WSHttpBinding httpBinding = new WSHttpBinding();
            AdjustBindingParameters(httpBinding);
            // the ServiceLocator required Reliable binding
            httpBinding.ReliableSession.Enabled = true;

            // Add endpoint identity to provide information about service identity and open secure communication to service
            EndpointAddress address = new EndpointAddress(new Uri(_TextBoxServiceLocatorEndpoint.Text), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));
            
            var factory = new ChannelFactory<IServiceLocator>(httpBinding, address);

            // Revocation list not used
            factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            #region Use Client Authentication

            // Usage of client authentication
            if (_cbUseClientAuthentication.Checked)
            {
                // Add client certificate to authenticate to service

                // **** IMPORTANT ****
                //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                //- uncomment to use client authentication
                //factory.Credentials.ClientCertificate.SetCertificate(
                //      StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
            }

            #endregion

            IServiceLocator serviceLocatorProxy = factory.CreateChannel();

            #endregion DOC_GET_SL_PROXY

            return serviceLocatorProxy;
        }

        private void CloseCommunicationObject(object objectToClose)
        {
            ICommunicationObject channel = objectToClose as ICommunicationObject;
            if (channel == null)
                return;

            if (channel.State != CommunicationState.Faulted)
            {
                try
                {
                    channel.Close();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    try
                    {
                        channel.Abort();
                    }
                    catch
                    {
                        //
                    }
                }
            }
            else
            {
                try
                {
                    channel.Abort();
                }
                catch
                {
                    //
                }
            }
        }

        #region DOC_CREATE_SD

        private ServiceDescription GetServiceDescription()
        {
            ServiceDescription serviceDescription = new ServiceDescription();
            serviceDescription.ServiceVersion = "1.2.3.4";
            serviceDescription.ServiceName = "Hello World Service";
            serviceDescription.Product = "Hello World Product";
            serviceDescription.Grouped = true;
            serviceDescription.Description = "OIB Tutorial service to demonstrate the implementation of custom OIB services.";

            string strUri = string.Format("http://{0}:1235/MyCompany.Serivces/mex", Environment.MachineName);
            serviceDescription.MetadataEndpoints = new[] { new Uri(strUri) };
            return serviceDescription;
        }

        #endregion
    }
}