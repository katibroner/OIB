#region Copyright
//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------
#endregion

#region Namespaces
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using OibSlData = www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;
using OIB.Tutorial.Common.Logging;
#endregion

namespace OIB.Tutorial
{
    /// <summary>
    /// The main windows implementation
    /// </summary>
    public partial class MainForm : Form, ILog
    {
        #region Fields
        private ISiplaceSetupCenterExternalControl _mes;
        private ServiceHost _mesServiceHost;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            MesContext.PackagingUnitsDataSet = new DataSet();
            MesContext.ComponentStatusDataSet = new DataSet();

            try
            {
                MesContext.PackagingUnitsDataSet.ReadXml(MesContext.PackagingUnitDataXmlFilePath);
                var puTable = MesContext.PackagingUnitsDataSet.Tables[0];
                puTable.PrimaryKey = new[] { puTable.Columns["PackagingUnitId"] };
            }
            catch (Exception ex)
            {
                ErrorMessage(string.Format("Failed to load the packaging units from xml file - '{0}'", MesContext.PackagingUnitDataXmlFilePath), ex);
            }

            try
            {
                MesContext.ComponentStatusDataSet.ReadXml(MesContext.ComponentStatusDataXmlFilePath);
                var puTable = MesContext.ComponentStatusDataSet.Tables[0];
                puTable.PrimaryKey = new[] { puTable.Columns["Name"], puTable.Columns["Barcode"] };
            }
            catch (Exception ex)
            {
                ErrorMessage($"Failed to load the component status from xml file - '{MesContext.ComponentStatusDataXmlFilePath}'", ex);
            }

            MesContext.SleepEnabled = checkBoxSleep.Enabled;
            MesContext.Sleep = (int)nudSleep.Value;
            MesContext.ModifyQuantityEnabled = checkBoxQuantity.Checked;
            MesContext.NewQuantity = (int)nudQuantity.Value;
            MesContext.ModifyOriginalQuantityEnabled = checkBoxOriginalQuantity.Checked;
            MesContext.NewOriginalQuantity = (int)nudOriginalQuantity.Value;
            MesContext.ModifyRohsEnabled = checkBoxRohs.Checked;
            MesContext.NewRohs = (int)nudRohs.Value;
            MesContext.ModifyMsdEnabled = checkBoxMsdLevel.Checked;
            MesContext.NewMsdLevel = (int)nudMsdLevel.Value;
            MesContext.NewMsdOpenDate = dateTimePickerMSD.Value.ToUniversalTime();
            MesContext.ModifySiplaceProComponentNameEnabled = checkBoxSiplaceProComponentName.Checked;
            MesContext.NewSiplaceProComponentName = textBoxSiplaceProComponentName.Text;

            textBoxXmlFilePath.Text = MesContext.PackagingUnitDataXmlFilePath;
            textBoxComponentXmlFilePath.Text = MesContext.ComponentStatusDataXmlFilePath;

            textBoxMesServiceEndpoint.Text = textBoxMesServiceEndpoint.Text.Replace("localhost", Environment.MachineName);
            textBoxServiceLocatorMexEndpoint.Text = textBoxServiceLocatorMexEndpoint.Text.Replace("localhost", Environment.MachineName);
        }
        #endregion

        #region Implementation of ILog
        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">The message as string</param>
        public void InfoMessage(string message)
        {
            loggerListView.InfoMessage(message);
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message">The message as string</param>
        /// <param name="ex">The exception</param>
        public void ErrorMessage(string message, Exception ex = null)
        {
            loggerListView.ErrorMessage(message, ex);
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">The message as string</param>
        /// <param name="ex">The exception</param>
        public void WarnMessage(string message, Exception ex = null)
        {
            loggerListView.WarnMessage(message, ex);
        }

        /// <summary>
        /// Logs a debug warning
        /// </summary>
        /// <param name="message">The message as string</param>
        /// <param name="ex">The exception</param>
        public void DebugMessage(string message, Exception ex = null)
        {
            loggerListView.DebugMessage(message, ex);
        }
        #endregion

        #region Event handlers
        private void buttonStartMesService_Click(object sender, EventArgs e)
        {
            try
            {
                StartMesService();
            }
            catch (Exception ex)
            {
                const string errorMessage = "Failed to start the SC MES service!";
                ErrorMessage(errorMessage, ex);
                MessageBox.Show(errorMessage + "\n\n" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStopMesService_Click(object sender, EventArgs e)
        {
            try
            {
                StopMesService();
            }
            catch (Exception ex)
            {
                const string errorMessage = "Failed to stop the SC MES service!";
                ErrorMessage(errorMessage, ex);
                MessageBox.Show(errorMessage + "\n\n" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReloadAvailablePuFromXml_Click(object sender, EventArgs e)
        {
            try
            {
                MesContext.PackagingUnitsDataSet.Clear();
                MesContext.PackagingUnitsDataSet.ReadXml(MesContext.PackagingUnitDataXmlFilePath);
                var puTable = MesContext.PackagingUnitsDataSet.Tables[0];
                puTable.PrimaryKey = new[] { puTable.Columns["PackagingUnitId"] };
            }
            catch (Exception ex)
            {
                const string errorMessage = "Failed to reload packaging units!";
                ErrorMessage(errorMessage, ex);
                MessageBox.Show(errorMessage + "\n\n" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new AvailablePackagingUnitsForm(MesContext.PackagingUnitsDataSet.Tables[0]);
            frm.ShowDialog();
            MesContext.PackagingUnitsDataSet.WriteXml(MesContext.PackagingUnitDataXmlFilePath);
        }

        private void checkBoxQuantity_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = ((CheckBox)sender).Checked;
            nudQuantity.Enabled = enabled;
            MesContext.ModifyQuantityEnabled = enabled;
        }

        private void checkBoxOriginalQuantity_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = ((CheckBox)sender).Checked;
            nudOriginalQuantity.Enabled = enabled;
            MesContext.ModifyOriginalQuantityEnabled = enabled;
        }

        private void checkBoxRohs_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = ((CheckBox)sender).Checked;
            nudRohs.Enabled = enabled;
            MesContext.ModifyRohsEnabled = enabled;
        }

        private void checkBoxMsdLevel_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = ((CheckBox)sender).Checked;
            nudMsdLevel.Enabled = enabled;
            labelMsdOpenDate.Enabled = enabled;
            dateTimePickerMSD.Enabled = enabled;
            MesContext.ModifyMsdEnabled = enabled;
        }

        private void checkBoxSiplaceProComponentName_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = ((CheckBox)sender).Checked;
            MesContext.ModifySiplaceProComponentNameEnabled = enabled;
            textBoxSiplaceProComponentName.Enabled = enabled;
        }

        private void checkBoxSleep_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = ((CheckBox)sender).Checked;
            nudSleep.Enabled = enabled;
            MesContext.SleepEnabled = enabled;
        }

        private void checkBoxThrowException_CheckedChanged(object sender, EventArgs e)
        {
            MesContext.ThrowExceptionEnabled = ((CheckBox)sender).Checked;
        }

        private void nudSleep_ValueChanged(object sender, EventArgs e)
        {
            MesContext.Sleep = (int)((NumericUpDown)sender).Value;
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            MesContext.NewQuantity = (int)((NumericUpDown)sender).Value;
        }

        private void nudOriginalQuantity_ValueChanged(object sender, EventArgs e)
        {
            MesContext.NewOriginalQuantity = (int)((NumericUpDown)sender).Value;
        }

        private void nudRohs_ValueChanged(object sender, EventArgs e)
        {
            MesContext.NewRohs = (int)((NumericUpDown)sender).Value;
        }

        private void nudMsdLevel_ValueChanged(object sender, EventArgs e)
        {
            MesContext.NewMsdLevel = (int)((NumericUpDown)sender).Value;
        }

        private void textBoxSiplaceProComponentName_TextChanged(object sender, EventArgs e)
        {
            MesContext.NewSiplaceProComponentName = ((TextBox)sender).Text;
        }

        private void textBoxXmlFilePath_TextChanged(object sender, EventArgs e)
        {
            MesContext.PackagingUnitDataXmlFilePath = textBoxXmlFilePath.Text;
        }

        private void buttonUnregisterMesService_Click(object sender, EventArgs e)
        {
            try
            {
                UnregisterService();
            }
            catch (Exception ex)
            {
                const string errorMessage = "Failed to unregister the SC MES services!";
                ErrorMessage(errorMessage, ex);
                MessageBox.Show(errorMessage + "\n\n" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private methods
        private void AdjustServiceUiControls(bool serviceStarted)
        {
            buttonStartMesService.BackColor = serviceStarted ? Color.Green : SystemColors.Control;
            buttonStartMesService.Enabled = !serviceStarted;
            buttonStopMesService.Enabled = serviceStarted;
            textBoxMesServiceEndpoint.Enabled = !serviceStarted;
            textBoxServiceLocatorMexEndpoint.Enabled = !serviceStarted;
            buttonUnregisterMesService.Enabled = !serviceStarted;
        }

        /// <summary>
        /// Creates a WS HTTP binding used for our MES service
        /// </summary>
        /// <returns></returns>
        private WSHttpBinding CreateHttpBinding()
        {
            var returnValue = new WSHttpBinding
            {
                OpenTimeout = new TimeSpan(0, 0, 10),
                CloseTimeout = new TimeSpan(0, 0, 10),
                SendTimeout = new TimeSpan(0, 1, 0),
                BypassProxyOnLocal = true,
                TransactionFlow = false,
                UseDefaultWebProxy = false,
                MaxReceivedMessageSize = int.MaxValue,
                ReaderQuotas =
                                          {
                                              MaxDepth = int.MaxValue,
                                              MaxStringContentLength = int.MaxValue,
                                              MaxArrayLength = int.MaxValue,
                                              MaxBytesPerRead = int.MaxValue,
                                              MaxNameTableCharCount = int.MaxValue
                                          }
            };
            returnValue.Security.Mode = SecurityMode.None;
            return returnValue;
        }

        /// <summary>
        /// Get the service locator service endpoint using http protocol.
        /// </summary>
        /// <returns>The http endpoint of service locator service, or null, if http is not supported.</returns>
        private ServiceEndpoint GetServiceLocatorEndpoint()
        {
            // Get mex address from GUI, like http://localhost:1405/Asm.As.Oib.ServiceLocator/mex
            var mexAddress = new Uri(textBoxServiceLocatorMexEndpoint.Text);

            // Get all endpoints of the service locator service using the service locator mex-address
            var client = new MetadataExchangeClient(CreateMexHttpBinding());
            MetadataSet mds = client.GetMetadata(mexAddress, MetadataExchangeClientMode.MetadataExchange);

            MetadataImporter mdi = new WsdlImporter(mds);

            // Select the http endpoint from all service locator endpoints
            return mdi.ImportAllEndpoints().FirstOrDefault(endpoint =>
                                                           endpoint.Address.Uri.Scheme == "http" &&
                                                           endpoint.Address.Uri.AbsolutePath.ToLower() == "/Asm.As.Oib.ServiceLocator".ToLower());
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
            AdjustBindingParameters(bind, false);
            return bind;
        }

        private void AdjustBindingParameters(WSHttpBinding bind, bool useSecurity)
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
                bind.Security.Mode = useSecurity ? SecurityMode.Message : SecurityMode.None;

                // Usage of client authentication  binding properties
                bind.Security.Message.ClientCredentialType = _cbUseClientAuthentication.Checked ? MessageCredentialType.Certificate : MessageCredentialType.None;
            }
        }

        /// <summary>
        /// Creates the MES service description used for registration/deregistration in Service Locatior
        /// </summary>
        /// <returns></returns>
        private OibSlData.ServiceDescription GetMesServiceLocatorDescription()
        {
            var serviceDescription = new OibSlData.ServiceDescription
            {
                ServiceName = TestMesService.MesOibServiceNameDefaultValue,
                Product = "OIB SDK Tutorial",
                Grouped = true,
                MetadataEndpoints = new[] { new Uri(textBoxMesServiceEndpoint.Text) }
            };
            var assemblyVersion = GetType().Assembly.GetName().Version;
            var version = string.Format("OIB SDK V{0}.{1}", assemblyVersion.Major, assemblyVersion.Minor);
            serviceDescription.ServiceVersion = version;

            return serviceDescription;
        }

        /// <summary>
        /// Starts our MES service
        /// </summary>
        private void StartMesService()
        {
            if (_mesServiceHost != null)
            {
                WarnMessage("The MES service is already running.");
                return;
            }

            _mes = new TestMesService(this);
            _mesServiceHost = new ServiceHost(_mes, new[] { new Uri(textBoxMesServiceEndpoint.Text) });
            _mesServiceHost.AddServiceEndpoint(typeof(ISiplaceSetupCenterExternalControl), CreateHttpBinding(), textBoxMesServiceEndpoint.Text);
            var behaviour = _mesServiceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behaviour.InstanceContextMode = InstanceContextMode.Single;

            // Enable publishing the Metadata for the MES Service
            var metaBehaviors = _mesServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() ?? new ServiceMetadataBehavior();
            metaBehaviors.HttpGetEnabled = true;
            metaBehaviors.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            _mesServiceHost.Description.Behaviors.Add(metaBehaviors);
            _mesServiceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            // Start the MES service
            _mesServiceHost.Open();

            // Register the MES Service in OIB Service Locator
            ///////////////
            // Get the http OIB Serice Locator endpoint from its mex address
            var endpoint = GetServiceLocatorEndpoint();

            // Create a runtime proxy for the service locator service
            AdjustBindingParameters(endpoint.Binding as WSHttpBinding, true);

            // Activate secure communication, thereto Check if secure endpoint is used
            var strEndpoint = !endpoint.Address.ToString().ToLowerInvariant().EndsWith("secure")
                ? $"{endpoint.Address}Secure"
                : endpoint.Address.ToString();

            // Add endpoint identity to provide information about service identity and open secure communication to service
            EndpointAddress address = new EndpointAddress(new Uri(strEndpoint), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

            var factory = new ChannelFactory<IServiceLocator>(endpoint.Binding, address);

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
                //    StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
            }

            #endregion

            var serviceLocatorProxy = factory.CreateChannel();

            try
            {
                // Register the service
                serviceLocatorProxy.RegisterService(GetMesServiceLocatorDescription());
            }
            finally
            {
                CloseChannel(serviceLocatorProxy);
            }

            AdjustServiceUiControls(true);

            InfoMessage("The MES Service was started.");
        }

        private static void CloseChannel(IServiceLocator proxy)
        {
            // Close the proxy. 
            // In case the connection is faulted or an exception has occurred: abort the proxy.
            IClientChannel channel = proxy as IClientChannel;
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

        /// <summary>
        /// Stops our MES service
        /// </summary>
        private void StopMesService()
        {
            if (_mesServiceHost != null && (_mesServiceHost.State == CommunicationState.Opened || _mesServiceHost.State == CommunicationState.Opening))
            {
                _mesServiceHost.Close();
                var retryCount = 3;
                //wait until the host is closed
                while (_mesServiceHost.State != CommunicationState.Closed)
                {
                    System.Threading.Thread.Sleep(2000);
                    retryCount--;
                    if (retryCount == 0)
                        break;
                }
                if (_mesServiceHost.State != CommunicationState.Closed)
                {
                    ErrorMessage(string.Format("Failed to stop the MES service, the state is still '{0}'", _mesServiceHost.State));
                }

                _mesServiceHost.Abort();
                _mesServiceHost = null;
                _mes = null;

                InfoMessage("The MES Service was stopped.");
                AdjustServiceUiControls(false);
            }
            else
            {
                WarnMessage("The MES Service does not run, please start first the service.");
            }
        }

        /// <summary>
        /// Unregisters all "SIPLACE.SetupCenter.ExternalControl" from Service Locator
        /// </summary>
        private void UnregisterService()
        {
            // Get the http OIB Serice Locator endpoint from its mex address
            var endpoint = GetServiceLocatorEndpoint();

            // Create a runtime proxy for the service locator service
            AdjustBindingParameters(endpoint.Binding as WSHttpBinding, true);

            // Activate secure communication, thereto Check if secure endpoint is used
            var strEndpoint = !endpoint.Address.ToString().ToLowerInvariant().EndsWith("secure")
                ? $"{endpoint.Address}Secure"
                : endpoint.Address.ToString();

            // Add endpoint identity to provide information about service identity and open secure communication to service
            EndpointAddress address = new EndpointAddress(new Uri(strEndpoint), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

            var factory = new ChannelFactory<IServiceLocator>(endpoint.Binding, address);

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

            var serviceLocatorProxy = factory.CreateChannel();

            try
            {
                var criteria = new ServiceMatchCriteria { ServiceName = TestMesService.MesOibServiceNameDefaultValue };
                // Find all 'SIPLACE.SetupCenter.ExternalControl' services
                var foundServices = serviceLocatorProxy.FindServices(criteria);
                //and unregister then
                foreach (var serviceDescription in foundServices)
                {
                    serviceLocatorProxy.UnregisterService(serviceDescription);
                }
            }
            finally
            {
                CloseChannel(serviceLocatorProxy);
            }
            InfoMessage("The SC MES Services were unregistered.");
        }
        #endregion

        private void buttonReloadXmlComponentStatus_Click(object sender, EventArgs e)
        {
            try
            {
                MesContext.ComponentStatusDataXmlFilePath = textBoxComponentXmlFilePath.Text;
                MesContext.ComponentStatusDataSet.Clear();
                MesContext.ComponentStatusDataSet.ReadXml(MesContext.ComponentStatusDataXmlFilePath);
            }
            catch (Exception ex)
            {
                const string errorMessage = "Failed to reload component status";
                ErrorMessage(errorMessage, ex);
                MessageBox.Show(errorMessage + "\n\n" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonComponentStatus_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new ComponentStatusForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                const string errorMessage = "Error";
                ErrorMessage(errorMessage, ex);
                MessageBox.Show(errorMessage + "\n\n" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}