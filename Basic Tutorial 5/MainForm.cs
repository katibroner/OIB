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

        #region Event Handling

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // IMPORTANT:
                // If you are about to use Client Authentication, please activate checkbox before starting this tutorial.
                UpdateFactoryLayoutComboBox();
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error starting application: " + ex.Message;
            }
        }

        private void ComboBoxFactoryElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxFactoryElements.SelectedItem != null)
                {
                    Update();
                    Cursor = Cursors.WaitCursor;
                    UpdateDisplayedEndpoints();
                    _ToolStripStatusLabel.Text = string.Format("Service addresses for factory element '{0}' updated.", comboBoxFactoryElements.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error while processing the factory element selection: " + ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonDeleteConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (OibServiceLocatorHelper slh = new OibServiceLocatorHelper(new Uri(textBoxDiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
                {
                    using (OibConfigurationManagerHelper cmh =
                        new OibConfigurationManagerHelper(slh.GetMexUriForServiceName("ConfigurationManager"), _cbUseClientAuthentication.Checked))
                    {
                        cmh.ResetFactoryLayout();
                        slh.ResetConfiguration();
                    }
                }
                UpdateFactoryLayoutComboBox();
                UpdateDisplayedEndpoints();

                _ToolStripStatusLabel.Text = "Configuration successfully reseted";
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error while deleting the OIB configuration: " + ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonCreateLayout_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Uri configManagerMexUri;
                using (OibServiceLocatorHelper slh = new OibServiceLocatorHelper(new Uri(textBoxDiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
                {
                    configManagerMexUri = slh.GetMexUriForServiceName("ConfigurationManager");
                }
                using (OibConfigurationManagerHelper cmh = new OibConfigurationManagerHelper(configManagerMexUri, _cbUseClientAuthentication.Checked))
                {
                    cmh.CreateTutorialFactoryLayout();
                }
                UpdateFactoryLayoutComboBox();
                UpdateDisplayedEndpoints();

                _ToolStripStatusLabel.Text = "Factory Layout for Nano Placement Inc. created.";
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error while creating the factory layout: " + ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ButtonConfigureServices_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (OibServiceLocatorHelper slh = new OibServiceLocatorHelper(new Uri(textBoxDiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
                {
                    slh.CreateTutorialServiceConfiguration();
                }
                _ToolStripStatusLabel.Text = "Services configured successfully.";

                UpdateDisplayedEndpoints();
            }
            catch (Exception ex)
            {
                _ToolStripStatusLabel.Text = "Error while configuring services: " + ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        /// <summary>
        /// Updates the factory layout combo box with the current OIB Factory Model
        /// including enterprise - site - area - production line.
        /// </summary>
        private void UpdateFactoryLayoutComboBox()
        {
            using (OibServiceLocatorHelper slh = 
                new OibServiceLocatorHelper(new Uri(textBoxDiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
            {
                using (OibConfigurationManagerHelper cmh = 
                    new OibConfigurationManagerHelper(slh.GetMexUriForServiceName("ConfigurationManager"), _cbUseClientAuthentication.Checked))
                {

                    comboBoxFactoryElements.Items.Clear();

                    ConfigurationManager configurationManager = cmh.Proxy.Load();
                    comboBoxFactoryElements.Items.Add(new Isa95ComboBoxItem(configurationManager.Enterprise));

                    foreach (Site site in configurationManager.Enterprise.Sites)
                    {
                        comboBoxFactoryElements.Items.Add(new Isa95ComboBoxItem(site));
                        foreach (Area area in site.Areas)
                        {
                            comboBoxFactoryElements.Items.Add(new Isa95ComboBoxItem(area));
                            foreach (ProductionLine line in area.ProductionLines)
                            {
                                comboBoxFactoryElements.Items.Add(new Isa95ComboBoxItem(line));
                            }
                        }

                        foreach (ProductionLine line in site.ProductionLines)
                        {
                            comboBoxFactoryElements.Items.Add(new Isa95ComboBoxItem(line));
                        }
                    }

                    if (comboBoxFactoryElements.Items.Count > 0)
                        comboBoxFactoryElements.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Updates the selected endpoints for the given factory layout element.
        /// </summary>
        private void UpdateDisplayedEndpoints()
        {
            if (comboBoxFactoryElements.SelectedIndex >= 0)
            {
                using (OibServiceLocatorHelper slh = new OibServiceLocatorHelper(new Uri(textBoxDiscoveryEndpoint.Text), _cbUseClientAuthentication.Checked))
                {
                    Uri configurationManagerMexUri = slh.GetMexUriForServiceName("ConfigurationManager");
                    using (OibConfigurationManagerHelper cmh = new OibConfigurationManagerHelper(configurationManagerMexUri, _cbUseClientAuthentication.Checked))
                    {
                        ConfigurationManager configurationManager = cmh.Proxy.Load();

                        Isa95ComboBoxItem item = comboBoxFactoryElements.SelectedItem as Isa95ComboBoxItem;
                        if (item != null)
                        {
                            Uri uri = slh.GetMexUriForFactoryLayoutElement("SIPLACE.Pro.SPI", configurationManager, item.Element);
                            labelSpiEndpointAddress.Text = uri == null ? string.Empty : uri.AbsoluteUri;
                            uri = slh.GetMexUriForFactoryLayoutElement("SIPLACE.Pro.Optimizer", configurationManager, item.Element);
                            labelOptimizeEndpointAddress.Text = uri == null ? string.Empty : uri.AbsoluteUri;
                            uri = slh.GetMexUriForFactoryLayoutElement("SIPLACE.Pro.LineControl", configurationManager, item.Element);
                            labelLineControlEndpointAddress.Text    = uri == null ? string.Empty : uri.AbsoluteUri;
                            uri = slh.GetMexUriForFactoryLayoutElement("SIPLACE.SetupCenter.MaterialControl", configurationManager, item.Element);
                            labelSetupCenterEndpointAddress.Text    = uri == null ? string.Empty : uri.AbsoluteUri;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Wraps a Isa95Base element to show the path of the element when using the ToSting() method 
    /// (for the use in the drop down box).
    /// </summary>
    public class Isa95ComboBoxItem
    {
        private readonly Isa95Base _Element;

        public Isa95Base Element
        {
            get { return _Element; }
        }

        public Isa95ComboBoxItem(Isa95Base element)
        {
            _Element = element;
        }

        /// <summary>
        /// Overrides the ToString method to show the Isa95Base Path in the combobox.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _Element.Path;
        }
    }

}