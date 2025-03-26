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
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using ServiceDescription = www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription;

#endregion

namespace TraceabilitySyncInterfaceTutorial
{
    public partial class MainForm : Form
    {
        #region private Fields

        // Not more than 250 lines, older one will be override 
        private const int MaxMessagesCount = 250;
        private ServiceHost _serviceHost;
        private TraceabilitySyncService _service;
        private ServiceDescription _serviceDescription;
        #endregion

        #region Construction

        public MainForm()
        {
            InitializeComponent();
            _tbOIBCoreName.Text = Environment.MachineName;
        }

        #endregion

        #region public Properties

        public int BoardValidationResult
        {
            get
            {
                if(_rbConfirmed.Checked)
                    return 2;
                if(_rbError.Checked)
                    return 1;
                if(_rbRejectedHold.Checked)
                    return 3;
                return 4;
            }
        }
 
        #endregion

        #region Start / Stop Service

        private void ButtonStartService_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                StartService();
                _btnStartService.Enabled = false;
                _btnStopService.Enabled = true;
                _btnRegister.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Got exception: " + ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonStopService_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                StopService();
                _btnStartService.Enabled = true;
                _btnStopService.Enabled = false;
                _btnRegister.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Got exception: " + ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private ServiceDescription GetServiceDescription(Uri mexEndpointAddress)
        {
            ServiceDescription serviceDescription = new ServiceDescription
                {
                    ServiceVersion = "1.2.3.4",
                    ServiceName = "ASM.TraceabilityClient.CallbackService",
                    Product = _tbProductName.Text,
                    Grouped = true,
                    Description = _tbDescription.Text
                };
            // The service name is fixed and should not change. Otherwise the Traceability Service can not find your Service 
            serviceDescription.MetadataEndpoints = new[] {mexEndpointAddress };
            return serviceDescription;
        }

        private void StartService()
        {
            AddMessageToListbox(@"Starting the service host...");

            // create the service class which implements the DownloadInterceptor OIB Contract
            _service = new TraceabilitySyncService(this, BoardValidationResult, _tbBoardValidationReason.Text);

            // create a service host for our contract implementation
            _serviceHost = new ServiceHost(_service);

            // Replace localhost to real computer name
            foreach(var se in _serviceHost.Description.Endpoints)
            {
                se.Address = ReplaceLocalhost(se.Address);
                if(se.Contract.ContractType == typeof(IMetadataExchange))
                {
                    _serviceDescription = GetServiceDescription(se.Address.Uri);
                }
            }

            // open the web service
            _serviceHost.Open();

            AddMessageToListbox(@"The service is now running...");
        }

        private void StopService()
        {
            AddMessageToListbox(@"Stopping the service host...");
            if(_serviceHost != null)
            {
                // stop the service
                CloseCommunicationObject(_serviceHost);
                _service = null;
                AddMessageToListbox(@"The service host is stopped.");
            }
        }

        private EndpointAddress ReplaceLocalhost(EndpointAddress address)
        {
            UriBuilder builder = new UriBuilder(address.Uri);
            if(builder.Host.ToLowerInvariant() == "localhost")
            {
                builder.Host = Environment.MachineName.ToLower();
            }
            return new EndpointAddress(builder.Uri);
        }

        #endregion Start / Stop Service

        #region Service Register/ Unregister

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _btnStartService.Enabled = false;

            try
            {
                RegisterService();
                _btnUnregister.Enabled = true;
                _btnRegister.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"An error has occurred registering the service: " + ex.Message, @"OIB Tutorial",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddMessageToListbox("An error has occurred registering the service: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void RegisterService()
        {
            AddMessageToListbox("Start to register service...");
            IServiceLocator serviceLocatorProxy = null;

            try
            {
                serviceLocatorProxy = GetServiceLocatorProxy();
                AddMessageToListbox("Got channel to the Service Locator.");
                serviceLocatorProxy.RegisterService(_serviceDescription);
                AddMessageToListbox("The service was successfully registered: " + _serviceDescription.ServiceName);
            }
            finally
            {
                CloseCommunicationObject(serviceLocatorProxy);
            }
        }

        private void ButtonUnregister_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                UnregisterService();
                _btnUnregister.Enabled = false;
                _btnRegister.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(@"An error has occurred unregistering the service: " + ex.Message, @"OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddMessageToListbox("An error has occurred unregistering the service: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UnregisterService()
        {
            AddMessageToListbox("Start to unregister service...");
            IServiceLocator serviceLocatorProxy = null;

            try
            {
                serviceLocatorProxy = GetServiceLocatorProxy();
                AddMessageToListbox("Got channel to the Service Locator.");
                serviceLocatorProxy.UnregisterService(_serviceDescription);
                AddMessageToListbox("The service was successfully unregistered: " + _serviceDescription.ServiceName);
            }
            finally
            {
                CloseCommunicationObject(serviceLocatorProxy);
            }
        }

        #region DOC_GET_SL_PROXY

        private IServiceLocator GetServiceLocatorProxy()
        {
            var builder = new UriBuilder("http://localhost:1405/Asm.As.oib.servicelocatorSecure");
            builder.Host = _tbOIBCoreName.Text;

            // Add endpoint identity to provide information about service identity and open secure communication to service
            EndpointAddress slEndpoint = new EndpointAddress(builder.Uri, EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

            WSHttpBinding bind = new WSHttpBinding(SecurityMode.Message)
            {
                AllowCookies = false,
                BypassProxyOnLocal = true,
                UseDefaultWebProxy = false,
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                MaxBufferPoolSize = 524288,
                MaxReceivedMessageSize = 2147483647,
                OpenTimeout = TimeSpan.FromSeconds(10),
                CloseTimeout = TimeSpan.FromSeconds(10),
                SendTimeout = TimeSpan.FromMinutes(1), 
                ReaderQuotas =
                    {
                        MaxArrayLength = 2147483647,
                        MaxBytesPerRead = 2147483647,
                        MaxDepth = 2147483647,
                        MaxNameTableCharCount = 2147483647,
                        MaxStringContentLength = 2147483647
                    },
            };

            // Usage of client authentication  binding properties
            bind.Security.Message.ClientCredentialType = _cbUseClientAuthentication.Checked ? MessageCredentialType.Certificate : MessageCredentialType.None;

            var factory = new ChannelFactory<IServiceLocator>(bind, slEndpoint);

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
                //    StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
            }

            #endregion

            return factory.CreateChannel();
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

        #endregion DOC_GET_SL_PROXY

        #endregion Service Register/ Unregister

        #region GUI helper

        internal void AddMessageToListbox(string message)
        {
            if(InvokeRequired)
            {
                // We might be called by the WCF service thread, so invoke here
                Invoke(new Action(() => AddMessageToListbox(message) ));
            }
            try
            {
                Trace.WriteLine(message);
                _listbMessages.Items.Add(message);
                while(_listbMessages.Items.Count > MaxMessagesCount)
                {
                    _listbMessages.Items.RemoveAt(0);
                }
                _listbMessages.SelectedIndex = _listbMessages.Items.Count - 1;
            }
            catch 
            {
                // ignored
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Unregister the service here since this is just a tutorial application
                // and we do not want to leave dead registrations in the ServiceLocator.
                // In a real world scenario, the MES application would be registered once it is installed and running and unregistered once 
                // it is uninstalled again.
                if (_btnUnregister.Enabled)
                {
                    UnregisterService();
                }
                CloseCommunicationObject(_serviceHost);
            }
            catch
            {
                // ignored
            }
        }

        private void RadioButtonsCheckedChanged(object sender, EventArgs e)
        {
            if(_service != null)
            {
                _service.BoardValidationResult = BoardValidationResult;
            }
        }

        private void BoardValidationReason_TextChanged(object sender, EventArgs e)
        {
            if(_service != null)
            {
                _service.BoardValidationReason = _tbBoardValidationReason.Text;
            }
        }

        #endregion GUI helper

    }
}