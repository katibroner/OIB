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
using System.ServiceModel;

using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Service;
using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Data;
using ServiceDescription = www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription;

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

        private void ButtonTest_Click(object sender, EventArgs e)
        {
            try
            {            
                Cursor = Cursors.WaitCursor;

                #region DOC_QUERYING_CONFIG_SERVICE_MEX_OF_SERVICE_LOCATOR

                // Get the service locator
                Uri configurationManagerMexUri;
                using (ServiceHelper<IServiceLocator> serviceHelper =
                    new ServiceHelper<IServiceLocator>(new Uri(textBoxDiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
                {
                    // Show the used service locator endpoint address 
                    labelServiceLocatorEndpoint.Text = ((IClientChannel)serviceHelper.Proxy).RemoteAddress.Uri.AbsoluteUri;
                    
                    // Query the MEX address of OIB Configuration Manager
                    ServiceMatchCriteria criteria = new ServiceMatchCriteria();
                    criteria.ServiceName = "ConfigurationManager";
                    ServiceDescription[] serviceDescriptions = serviceHelper.Proxy.FindServices(criteria);

                    if (serviceDescriptions.Length == 0 || serviceDescriptions[0].MetadataEndpoints.Length == 0)
                        throw new ApplicationException("OIB does not know 'ConfigurationManager' service.");

                    configurationManagerMexUri = serviceDescriptions[0].MetadataEndpoints[0];
                }   // End of using: causes a close or abort of the open connection

                #endregion
                
                // Show the discovered configuration manager MEX address
                labelConfigManagerMex.Text = configurationManagerMexUri.AbsoluteUri;

                #region DOC_ACCESS_OF_CONFIGURATION_MANAGER

                // Get the configuration manager
                using (ServiceHelper<IConfigurationManager> configManagerHelper =
                    new ServiceHelper<IConfigurationManager>(configurationManagerMexUri, _cbUseClientAuthentication.Checked))
                {
                    // Show the used endpoint address
                    labelConfigManagerEndpoint.Text = ((IClientChannel)configManagerHelper.Proxy).RemoteAddress.Uri.AbsoluteUri;

                    // Load the configuration manager from the configuration manager service
                    ConfigurationManager configurationManager = configManagerHelper.Proxy.Load();

                    // Show the name of the top factory element 'Enterprise'
                    labelEnterpriseName.Text = configurationManager.Enterprise.Name;
                }   // End of using: causes a close or abort of the open connection

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while querying the services form OIB Service Locator.\n\n" + ex.Message, "OIB Tutorial");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}