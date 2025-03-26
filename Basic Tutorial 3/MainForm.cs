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
using System.Windows.Forms;

using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Data;
using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Service;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

#endregion

namespace OIB.Tutorial
{
    public partial class mainForm : Form
    {
        #region Constructor

        public mainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handing

        private void ButtonTestAddressInExeConfig_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            labelT1EnterpriseName.Text = string.Empty;
            Update();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            #region DOC_FULL_PROXY_CONFIGURATION_FROM_EXE_CONFIG

            // Create Web service proxy using endpoint address from exe.config file
            ConfigurationManagerClient configurationManagerClient = new ConfigurationManagerClient();

            #region Use Client Authentication

            // Usage of client authentication
            if (_cbUseClientAuthentication.Checked)
            {
                // Add client certificate to authenticate to service
            
                // **** IMPORTANT ****
                //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                //- uncomment to use client authentication and look at the app.config to modify binding property 
                //configurationManagerClient.ChannelFactory.Credentials.ClientCertificate.SetCertificate(
                //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
            }

            #endregion

            try
            {
                ConfigurationManager configurationManager = configurationManagerClient.Load();

                labelT1EnterpriseName.Text = configurationManager.Enterprise.Name;
                configurationManagerClient.Close();
            }
            catch (Exception ex)
            {
                configurationManagerClient.Abort();
                MessageBox.Show("Failed to access OIB configuration manager: \n\n" + ex.Message, "OIB Tutorial");
            }

            #endregion

            Cursor = Cursors.Default;
            stopwatch.Stop();
            _ToolStripStatusLabel.Text = string.Format("Time elapsed: '{0}'", stopwatch.Elapsed.ToString());
        }

        private void ButtonTestAddressFromApplication_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            labelT2EnterpriseName.Text = string.Empty;
            Update();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            #region DOC_PARTIAL_PROXY_CONFIGURATION_FROM_EXE_CONFIG

            // Create Web service proxy using endpoint address from GUI textbox
            ConfigurationManagerClient configurationManagerClient = new ConfigurationManagerClient("WSHttpBinding_IConfigurationManager");

            // **** IMPORTANT ****
            //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
            //- uncomment to use client authentication and look at the app.config to modify binding property 
            //configurationManagerClient.ChannelFactory.Credentials.ClientCertificate.SetCertificate(
            //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");

            try
            {
                ConfigurationManager configurationManager = configurationManagerClient.Load();
                labelT2EnterpriseName.Text = configurationManager.Enterprise.Name;
                _ToolStripStatusLabel.Text = string.Format("Factory Layout loaded from ConfigurationManager with Address {0}", textBoxConfigurationEndpoint.Text);

                configurationManagerClient.Close();
            }
            catch (Exception ex)
            {
                configurationManagerClient.Abort();
                MessageBox.Show("Failed to access OIB configuration manager: \n\n" + ex.Message, "OIB Tutorial");
            }

            #endregion

            stopwatch.Stop();
            _ToolStripStatusLabel.Text = string.Format("Time elapsed: '{0}'", stopwatch.Elapsed.ToString());
            Cursor = Cursors.Default;
        }

        private void ButtonTestAddressFromDiscovery_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            labelT3EnterpriseName.Text = string.Empty;
            Update();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            #region DOC_PROXY_CONFIGURATION_FROM_SERVICELOCATOR

            try
            {
                // Query OIB Configuration Service endpoint address from OIB Discovery
                Uri configurationMangerUri;
                using (OibServiceLocatorHelper serviceLocatorHelper = new OibServiceLocatorHelper(new Uri(textBoxT3DiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
                {
                    configurationMangerUri = serviceLocatorHelper.GetMexUriForServiceName("ConfigurationManager");
                }
                using (ServiceHelper<IConfigurationManager> configurationManagerHelper =
                    new ServiceHelper<IConfigurationManager>(configurationMangerUri, _cbUseClientAuthentication.Checked))
                {
                    ConfigurationManager configurationManager = configurationManagerHelper.Proxy.Load();
                    labelT3EnterpriseName.Text = configurationManager.Enterprise.Name;
                    _ToolStripStatusLabel.Text = string.Format("Factory Layout loaded from ConfigurationManager with Address {0}", configurationMangerUri.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to access OIB configuration manager: \n\n" + ex.Message, "OIB Tutorial");
            }

            #endregion

            stopwatch.Stop();
            _ToolStripStatusLabel.Text = string.Format("Time elapsed: '{0}'", stopwatch.Elapsed.ToString());
            Cursor = Cursors.Default;
        }

        #endregion
    }
}