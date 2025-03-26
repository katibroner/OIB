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
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;

using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using System.ServiceModel.Channels;

#endregion

namespace OIB.Tutorial
{
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region DOC_SELECT_SERVICE_LOCATOR_ENDPOINT


        /// <summary>
        /// Get the service locator service endpoint using http protocol.
        /// </summary>
        /// <returns>The http endpoint of service locator service, or null, if http is not supported.</returns>
        private ServiceEndpoint GetServiceLocatorEndpoint()
        {
            // Get mex address from GUI, like http://localhost:1405/Asm.As.Oib.ServiceLocator/mex
            Uri mexAddress = new Uri(textBoxDiscoveryEndpoint.Text);

            // Get all endpoints of the service locator service using the service locator mex-address
            var client = new MetadataExchangeClient(CreateMexHttpBinding());
            MetadataSet mds = client.GetMetadata(mexAddress, MetadataExchangeClientMode.MetadataExchange);
            MetadataImporter mdi = new WsdlImporter(mds);

            // Select the http endpoint from all service locator endpoints
            foreach (ServiceEndpoint endpoint in mdi.ImportAllEndpoints())
            {
                if (endpoint.Address.Uri.Scheme == "http" && endpoint.Address.Uri.AbsolutePath.ToLower() == "/Asm.As.Oib.ServiceLocator".ToLower())
                {
                    return endpoint;
                }
            }

            // Endpoint is not supported:
            return null;
        }


        /// <summary> 
        /// /// Creates the mex HTTP binding. 
        /// </summary> 
        /// <returns> 
        /// CustomBinding for connecting to http mex endpoints 
        /// </returns> 
        private WSHttpBinding CreateMexHttpBinding()
        {
            var bind = MetadataExchangeBindings.CreateMexHttpBinding() as WSHttpBinding;
            AdjustHttpBinding(bind, false, false);
            return bind;
        }

        private void AdjustHttpBinding(WSHttpBinding bind, bool isReliableSession, bool useSecure)
        {
            if (bind != null)
            {
                bind.AllowCookies = false;
                bind.BypassProxyOnLocal = true;
                bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
                bind.OpenTimeout = new TimeSpan(0, 0, 10);
                bind.CloseTimeout = new TimeSpan(0, 0, 10);
                bind.SendTimeout = new TimeSpan(0, 0, 20);
                bind.MaxBufferPoolSize = 524288;
                bind.MaxReceivedMessageSize = 2147483647;
                bind.ReaderQuotas.MaxArrayLength = 2147483647;
                bind.ReaderQuotas.MaxBytesPerRead = 2147483647;
                bind.ReaderQuotas.MaxDepth = 2147483647;
                bind.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                bind.ReaderQuotas.MaxStringContentLength = 2147483647;
                bind.ReliableSession.Enabled = isReliableSession;
                bind.ReliableSession.Ordered = true;
                bind.TransactionFlow = false;
                bind.UseDefaultWebProxy = false;

                // Use message security
                bind.Security.Mode = useSecure ? SecurityMode.Message : SecurityMode.None;

                // Use client authentication binding properties
                bind.Security.Message.ClientCredentialType = _cbUseClientAuthentication.Checked ? MessageCredentialType.Certificate : MessageCredentialType.None;
            }
        }

        #endregion

        #region Test button click handlers

        private void ButtonTest1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                dataGridViewResult.DataSource = null;
                Update();

                #region DOC_CREATE_AND_USE_GENERATED_PROXY

                WSHttpBinding binding = new WSHttpBinding();
                AdjustHttpBinding(binding, true, true);

                // Add endpoint identity to provide information about service identity and open secure communication to service
                EndpointAddress address = new EndpointAddress(new Uri(textBoxServiceLocatorHttpUri.Text), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));
                ServiceLocatorClient proxy = new ServiceLocatorClient(binding, address);

                // Revocation list not used
                proxy.ChannelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //proxy.ChannelFactory.Credentials.ClientCertificate.SetCertificate(
                    //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
                }

                #endregion

                ShowServices(proxy.GetAllServices());
                proxy.Close();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while querying the services form OIB Service Locator.\n\n" + ex.Message, "OIB Tutorial");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonTest2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                dataGridViewResult.DataSource = null;
                Update();

                #region DOC_USING_CHANNEL_FACTORY

                WSHttpBinding binding = new WSHttpBinding();
                AdjustHttpBinding(binding, true, true);

                // Add endpoint identity to provide information about service identity and open secure communication to service
                EndpointAddress address = new EndpointAddress(new Uri(textBoxServiceLocatorHttpUriTest2.Text), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

                var factory = new ChannelFactory<IServiceLocator>(binding, address);

                // Revocation list not used
                factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //factory.Credentials.ClientCertificate.SetCertificate(
                    //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
                }

                #endregion

                IServiceLocator serviceLocator = factory.CreateChannel();
                try
                {
                    ShowServices(serviceLocator.GetAllServices());
                }
                finally
                {
                    CloseChannel(serviceLocator);                    
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while querying the services form OIB Service Locator.\n\n" + ex.Message, "OIB Tutorial");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonTest3_Click(object sender, EventArgs e)
        {
            try
            {            
                Cursor = Cursors.WaitCursor;
                dataGridViewResult.DataSource = null;
                dataGridViewResult.Update();

                #region DOC_CREATE_AND_USE_PROXY_FROM_ENDPOINT

                // Get the http service locator endpoint from its mex address
                ServiceEndpoint endpoint = GetServiceLocatorEndpoint();
                WSHttpBinding httpBinding = endpoint.Binding as WSHttpBinding;
                AdjustHttpBinding(httpBinding, false, true);

                // Activate secure communication, thereto check if secure endpoint is used
                var strEndpoint = !endpoint.Address.ToString().ToLowerInvariant().EndsWith("secure")
                    ? $"{endpoint.Address}Secure"
                    : endpoint.Address.ToString();

                // Add endpoint identity to provide information about service identity and open secure communication to service
                EndpointAddress address = new EndpointAddress(new Uri(strEndpoint), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

                // Create a runtime proxy for the service locator service
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
                    //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
                }

                #endregion

                IServiceLocator serviceLocatorProxy = factory.CreateChannel();

                try
                {
                    ShowServices(serviceLocatorProxy.GetAllServices());
                }
                finally
                {
                    CloseChannel(serviceLocatorProxy);
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while querying the services form OIB Service Locator.\n\n" + ex.Message, "OIB Tutorial");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        /// <summary>
        /// Displays the services from the service descriptions
        /// </summary>
        /// <param name="serviceDescriptions">The service descriptions to show</param>
        private void ShowServices(www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription[] serviceDescriptions)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Service Name", typeof(string));
            dataTable.Columns.Add("MEX-Address", typeof(string));

            foreach (www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription serviceDescription in
                serviceDescriptions)
            {
                DataRow row = dataTable.NewRow();
                row[0] = serviceDescription.ServiceName;
                if (serviceDescription.MetadataEndpoints != null && serviceDescription.MetadataEndpoints.Length > 0)
                {
                    row[1] = serviceDescription.MetadataEndpoints[0].AbsoluteUri;
                }
                else
                {
                    row[1] = "No MEX endpoints.";
                }
                dataTable.Rows.Add(row);
            }

            dataGridViewResult.DataSource = dataTable;
            dataGridViewResult.AutoResizeColumns();
        }

        private void CloseChannel(object toClose)
        {
            ICommunicationObject channel = toClose as ICommunicationObject;
            
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
    }
}