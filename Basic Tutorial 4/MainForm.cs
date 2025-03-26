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

using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Service;
using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Data;

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

        #region Event Handler

        private void ButtonRefreshView_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            RefreshView();
            Cursor = Cursors.Default;
        }

        private void ButtonDeleteConfig_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DeleteConfiguration();
            Cursor = Cursors.Default;
        }

        private void ButtonCreateConfig_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CreateConfiguration();
            Cursor = Cursors.Default;
        }

        #endregion

        /// <summary>
        /// Gets the OIB Factory Layout from the OIB configuration service and displays it in the form.
        /// </summary>
        private void RefreshView()
        {
            try
            {
                #region DOC_READ_CONFIGURATION

                using (ServiceHelper<IConfigurationManager> cmh = GetConfigurationManagerHelper())
                {                    
                    // Load the configuration manager which holds the OIB Factory Layout
                    ConfigurationManager configurationManager = cmh.Proxy.Load();
                    treeViewFactoryLayout.Nodes.Clear();
                    Update();

                    // Create tree view nodes for the elements of the factory layout
                    Enterprise enterprise = configurationManager.Enterprise;
                    TreeNode enterpriseNode = new TreeNode(enterprise.Name);
                    enterpriseNode.Tag = enterprise;
                    treeViewFactoryLayout.Nodes.Add(enterpriseNode);
                    foreach (Site site in enterprise.Sites)
                    {
                        TreeNode siteNode = new TreeNode(site.Name);
                        siteNode.Tag = site;
                        enterpriseNode.Nodes.Add(siteNode);

                        foreach (ProductionLine productionLine in site.ProductionLines)
                        {
                            TreeNode productionLineNode = new TreeNode(productionLine.Name);
                            productionLineNode.Tag = productionLine;
                            siteNode.Nodes.Add(productionLineNode);
                        }

                        foreach (Area area in site.Areas)
                        {
                            TreeNode areaNode = new TreeNode(area.Name);
                            areaNode.Tag = area;
                            siteNode.Nodes.Add(areaNode);

                            foreach (ProductionLine productionLine in area.ProductionLines)
                            {
                                TreeNode productionLineNode = new TreeNode(productionLine.Name);
                                productionLineNode.Tag = productionLine;
                                areaNode.Nodes.Add(productionLineNode);
                            }
                        }
                    }

                    treeViewFactoryLayout.ExpandAll();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update OIB production lines: \n\n" + ex.Message, "OIB Tutorial");
            }
        }

        /// <summary>
        /// Create the OIB Factory Layout according to the tutorial.
        /// </summary>
        private void CreateConfiguration()
        {
            try
            {
                #region DOC_CREATE_CONFIGURATION

                using (ServiceHelper<IConfigurationManager> cmh = GetConfigurationManagerHelper())
                {
                    // 1. Load the configuration manager which holds the OIB Factory Layout
                    ConfigurationManager configurationManager = cmh.Proxy.Load();

                    // 2. Rename the enterprise object
                    Enterprise enterprise = configurationManager.Enterprise;
                    enterprise.Name = "Nano Placement Inc.";
                    enterprise.Sites.Clear();   // delte old configuration, if any, to be on the save side.

                    // 3. Create site 'Munich' and link it to the enterprise element
                    Site site = new Site();
                    site.Name = "Munich";
                    site.ProductionLines = new ProductionLineCollection();
                    enterprise.Sites.Add(site);

                    // 4. Create production line 'Line 1' and link it to the site 'Munich'
                    ProductionLine line1 = new ProductionLine();
                    line1.Name = "Line 1";
                    site.ProductionLines.Add(line1);

                    // 5. Create production line 'Line 2' and link it to the site 'Munich'
                    ProductionLine line2 = new ProductionLine();
                    line2.Name = "Line 2";
                    site.ProductionLines.Add(line2);

                    // 6. overwrite old config with new configuration.
                    cmh.Proxy.Save(configurationManager);
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create the OIB factory layout: \n\n" + ex.Message, "OIB Tutorial");
            }

            // Refresh GUI
            RefreshView();
        }

        /// <summary>
        /// Deletes the OIB Factory Layout and renames the root node back to 'Enterprise'
        /// </summary>
        private void DeleteConfiguration()
        {
            try
            {
                #region DOC_DELETE_CONFIGURATION

                using (ServiceHelper<IConfigurationManager> cmh = GetConfigurationManagerHelper())
                {
                    // 1. Load the configuration manager which holds the OIB Factory Layout
                    ConfigurationManager configurationManager = cmh.Proxy.Load();

                    // 2. Reset configuration by:
                    // - removing the object tree below enterprise and 
                    // - renameing enterprise node to the default name "Enterprise"
                    Enterprise enterprise = configurationManager.Enterprise;
                    enterprise.Sites.Clear();
                    enterprise.Name = "Enterprise";

                    // 3. Overwrite old config with new configuration.
                    cmh.Proxy.Save(configurationManager);
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the OIB facory layout: \n\n" + ex.Message, "OIB Tutorial");
            }

            // Refresh GUI
            RefreshView();
        }

        /// <summary>
        /// Create a configuration manager proxy wraped by the ServiceHelper.
        /// </summary>
        /// <returns></returns>
        private ServiceHelper<IConfigurationManager> GetConfigurationManagerHelper()
        {
            using (OibServiceLocatorHelper slh = new OibServiceLocatorHelper(new Uri(textBoxDiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
            {
                Uri configurationManagerUri = slh.GetMexUriForServiceName("ConfigurationManager");
                if (configurationManagerUri == null)
                    throw new ApplicationException("ConfigurationManager service not known by OIB.");

                ServiceHelper<IConfigurationManager> configurationManagerHelper =
                    new ServiceHelper<IConfigurationManager>(configurationManagerUri, _cbUseClientAuthentication.Checked);

                labelConfigManagerEndpoint.Text = ((IClientChannel)configurationManagerHelper.Proxy).RemoteAddress.Uri.AbsoluteUri;

                return configurationManagerHelper;
            }
        }
    }
}