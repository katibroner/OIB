namespace OIB.Tutorial
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxT3 = new System.Windows.Forms.GroupBox();
            this.labelT3EnterpriseName = new System.Windows.Forms.Label();
            this.labelT3EnterpriseNameCaption = new System.Windows.Forms.Label();
            this.buttonTestAddressFromDiscovery = new System.Windows.Forms.Button();
            this.textBoxT3DiscoveryEndpoint = new System.Windows.Forms.TextBox();
            this.labelServiceLocatorMexCaption = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxT1 = new System.Windows.Forms.GroupBox();
            this.labelT1EnterpriseName = new System.Windows.Forms.Label();
            this.labelT1EnterpriseNameCaption = new System.Windows.Forms.Label();
            this.buttonTestAddressInExeConfig = new System.Windows.Forms.Button();
            this.groupBoxT2 = new System.Windows.Forms.GroupBox();
            this.labelT2EnterpriseName = new System.Windows.Forms.Label();
            this.labelT2EnterpriseNameCaption = new System.Windows.Forms.Label();
            this.buttonTestAddressFromApplication = new System.Windows.Forms.Button();
            this.textBoxConfigurationEndpoint = new System.Windows.Forms.TextBox();
            this.labelConfigServiceEndpointCaption = new System.Windows.Forms.Label();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.groupBoxT3.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBoxT1.SuspendLayout();
            this.groupBoxT2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxT3
            // 
            this.groupBoxT3.Controls.Add(this.labelT3EnterpriseName);
            this.groupBoxT3.Controls.Add(this.labelT3EnterpriseNameCaption);
            this.groupBoxT3.Controls.Add(this.buttonTestAddressFromDiscovery);
            this.groupBoxT3.Controls.Add(this.textBoxT3DiscoveryEndpoint);
            this.groupBoxT3.Controls.Add(this.labelServiceLocatorMexCaption);
            this.groupBoxT3.Location = new System.Drawing.Point(12, 246);
            this.groupBoxT3.Name = "groupBoxT3";
            this.groupBoxT3.Size = new System.Drawing.Size(756, 121);
            this.groupBoxT3.TabIndex = 6;
            this.groupBoxT3.TabStop = false;
            this.groupBoxT3.Text = "3. Alternative: Access of configuration manager using OIB service locator without" +
    " using .exe.config file";
            // 
            // labelT3EnterpriseName
            // 
            this.labelT3EnterpriseName.AutoSize = true;
            this.labelT3EnterpriseName.Location = new System.Drawing.Point(242, 90);
            this.labelT3EnterpriseName.Name = "labelT3EnterpriseName";
            this.labelT3EnterpriseName.Size = new System.Drawing.Size(10, 13);
            this.labelT3EnterpriseName.TabIndex = 20;
            this.labelT3EnterpriseName.Text = "-";
            // 
            // labelT3EnterpriseNameCaption
            // 
            this.labelT3EnterpriseNameCaption.AutoSize = true;
            this.labelT3EnterpriseNameCaption.Location = new System.Drawing.Point(24, 90);
            this.labelT3EnterpriseNameCaption.Name = "labelT3EnterpriseNameCaption";
            this.labelT3EnterpriseNameCaption.Size = new System.Drawing.Size(192, 13);
            this.labelT3EnterpriseNameCaption.TabIndex = 19;
            this.labelT3EnterpriseNameCaption.Text = "Name of the enterprise factory element:";
            // 
            // buttonTestAddressFromDiscovery
            // 
            this.buttonTestAddressFromDiscovery.Location = new System.Drawing.Point(27, 54);
            this.buttonTestAddressFromDiscovery.Name = "buttonTestAddressFromDiscovery";
            this.buttonTestAddressFromDiscovery.Size = new System.Drawing.Size(113, 23);
            this.buttonTestAddressFromDiscovery.TabIndex = 8;
            this.buttonTestAddressFromDiscovery.Text = "Test";
            this.buttonTestAddressFromDiscovery.UseVisualStyleBackColor = true;
            this.buttonTestAddressFromDiscovery.Click += new System.EventHandler(this.ButtonTestAddressFromDiscovery_Click);
            // 
            // textBoxT3DiscoveryEndpoint
            // 
            this.textBoxT3DiscoveryEndpoint.Location = new System.Drawing.Point(245, 23);
            this.textBoxT3DiscoveryEndpoint.Name = "textBoxT3DiscoveryEndpoint";
            this.textBoxT3DiscoveryEndpoint.Size = new System.Drawing.Size(493, 20);
            this.textBoxT3DiscoveryEndpoint.TabIndex = 7;
            this.textBoxT3DiscoveryEndpoint.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/mex";
            // 
            // labelServiceLocatorMexCaption
            // 
            this.labelServiceLocatorMexCaption.AutoSize = true;
            this.labelServiceLocatorMexCaption.Location = new System.Drawing.Point(24, 26);
            this.labelServiceLocatorMexCaption.Name = "labelServiceLocatorMexCaption";
            this.labelServiceLocatorMexCaption.Size = new System.Drawing.Size(123, 13);
            this.labelServiceLocatorMexCaption.TabIndex = 6;
            this.labelServiceLocatorMexCaption.Text = "OIB service locator MEX";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 393);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(780, 22);
            this.statusStrip.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.labelT1EnterpriseName);
            this.groupBoxT1.Controls.Add(this.labelT1EnterpriseNameCaption);
            this.groupBoxT1.Controls.Add(this.buttonTestAddressInExeConfig);
            this.groupBoxT1.Location = new System.Drawing.Point(12, 12);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(756, 97);
            this.groupBoxT1.TabIndex = 8;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "1. Alternative: Access of configuration manager using endpoint address and bindin" +
    "g in .exe.config file";
            // 
            // labelT1EnterpriseName
            // 
            this.labelT1EnterpriseName.AutoSize = true;
            this.labelT1EnterpriseName.Location = new System.Drawing.Point(242, 63);
            this.labelT1EnterpriseName.Name = "labelT1EnterpriseName";
            this.labelT1EnterpriseName.Size = new System.Drawing.Size(10, 13);
            this.labelT1EnterpriseName.TabIndex = 16;
            this.labelT1EnterpriseName.Text = "-";
            // 
            // labelT1EnterpriseNameCaption
            // 
            this.labelT1EnterpriseNameCaption.AutoSize = true;
            this.labelT1EnterpriseNameCaption.Location = new System.Drawing.Point(24, 63);
            this.labelT1EnterpriseNameCaption.Name = "labelT1EnterpriseNameCaption";
            this.labelT1EnterpriseNameCaption.Size = new System.Drawing.Size(192, 13);
            this.labelT1EnterpriseNameCaption.TabIndex = 15;
            this.labelT1EnterpriseNameCaption.Text = "Name of the enterprise factory element:";
            // 
            // buttonTestAddressInExeConfig
            // 
            this.buttonTestAddressInExeConfig.Location = new System.Drawing.Point(27, 28);
            this.buttonTestAddressInExeConfig.Name = "buttonTestAddressInExeConfig";
            this.buttonTestAddressInExeConfig.Size = new System.Drawing.Size(113, 23);
            this.buttonTestAddressInExeConfig.TabIndex = 8;
            this.buttonTestAddressInExeConfig.Text = "Test";
            this.buttonTestAddressInExeConfig.UseVisualStyleBackColor = true;
            this.buttonTestAddressInExeConfig.Click += new System.EventHandler(this.ButtonTestAddressInExeConfig_Click);
            // 
            // groupBoxT2
            // 
            this.groupBoxT2.Controls.Add(this.labelT2EnterpriseName);
            this.groupBoxT2.Controls.Add(this.labelT2EnterpriseNameCaption);
            this.groupBoxT2.Controls.Add(this.buttonTestAddressFromApplication);
            this.groupBoxT2.Controls.Add(this.textBoxConfigurationEndpoint);
            this.groupBoxT2.Controls.Add(this.labelConfigServiceEndpointCaption);
            this.groupBoxT2.Location = new System.Drawing.Point(12, 115);
            this.groupBoxT2.Name = "groupBoxT2";
            this.groupBoxT2.Size = new System.Drawing.Size(756, 125);
            this.groupBoxT2.TabIndex = 9;
            this.groupBoxT2.TabStop = false;
            this.groupBoxT2.Text = "2. Alternative: Access of configuration manager using endpoint address in configu" +
    "ration and binding from .exe.config file";
            // 
            // labelT2EnterpriseName
            // 
            this.labelT2EnterpriseName.AutoSize = true;
            this.labelT2EnterpriseName.Location = new System.Drawing.Point(242, 92);
            this.labelT2EnterpriseName.Name = "labelT2EnterpriseName";
            this.labelT2EnterpriseName.Size = new System.Drawing.Size(10, 13);
            this.labelT2EnterpriseName.TabIndex = 18;
            this.labelT2EnterpriseName.Text = "-";
            // 
            // labelT2EnterpriseNameCaption
            // 
            this.labelT2EnterpriseNameCaption.AutoSize = true;
            this.labelT2EnterpriseNameCaption.Location = new System.Drawing.Point(24, 92);
            this.labelT2EnterpriseNameCaption.Name = "labelT2EnterpriseNameCaption";
            this.labelT2EnterpriseNameCaption.Size = new System.Drawing.Size(192, 13);
            this.labelT2EnterpriseNameCaption.TabIndex = 17;
            this.labelT2EnterpriseNameCaption.Text = "Name of the enterprise factory element:";
            // 
            // buttonTestAddressFromApplication
            // 
            this.buttonTestAddressFromApplication.Location = new System.Drawing.Point(27, 54);
            this.buttonTestAddressFromApplication.Name = "buttonTestAddressFromApplication";
            this.buttonTestAddressFromApplication.Size = new System.Drawing.Size(113, 23);
            this.buttonTestAddressFromApplication.TabIndex = 8;
            this.buttonTestAddressFromApplication.Text = "Test";
            this.buttonTestAddressFromApplication.UseVisualStyleBackColor = true;
            this.buttonTestAddressFromApplication.Click += new System.EventHandler(this.ButtonTestAddressFromApplication_Click);
            // 
            // textBoxConfigurationEndpoint
            // 
            this.textBoxConfigurationEndpoint.Location = new System.Drawing.Point(245, 23);
            this.textBoxConfigurationEndpoint.Name = "textBoxConfigurationEndpoint";
            this.textBoxConfigurationEndpoint.Size = new System.Drawing.Size(493, 20);
            this.textBoxConfigurationEndpoint.TabIndex = 7;
            this.textBoxConfigurationEndpoint.Text = "http://localhost:1405/Asm.As.Oib.ConfigurationManager/ReliableSecure";
            // 
            // labelConfigServiceEndpointCaption
            // 
            this.labelConfigServiceEndpointCaption.AutoSize = true;
            this.labelConfigServiceEndpointCaption.Location = new System.Drawing.Point(24, 26);
            this.labelConfigServiceEndpointCaption.Name = "labelConfigServiceEndpointCaption";
            this.labelConfigServiceEndpointCaption.Size = new System.Drawing.Size(210, 13);
            this.labelConfigServiceEndpointCaption.TabIndex = 6;
            this.labelConfigServiceEndpointCaption.Text = "OIB configuration service endpoint address";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(39, 373);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 10;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 415);
            this.Controls.Add(this._cbUseClientAuthentication);
            this.Controls.Add(this.groupBoxT2);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBoxT3);
            this.Name = "mainForm";
            this.Text = "OIB Tutorial";
            this.groupBoxT3.ResumeLayout(false);
            this.groupBoxT3.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.groupBoxT2.ResumeLayout(false);
            this.groupBoxT2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxT3;
        private System.Windows.Forms.TextBox textBoxT3DiscoveryEndpoint;
        private System.Windows.Forms.Label labelServiceLocatorMexCaption;
        private System.Windows.Forms.Button buttonTestAddressFromDiscovery;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBoxT1;
        private System.Windows.Forms.Button buttonTestAddressInExeConfig;
        private System.Windows.Forms.GroupBox groupBoxT2;
        private System.Windows.Forms.Button buttonTestAddressFromApplication;
        private System.Windows.Forms.TextBox textBoxConfigurationEndpoint;
        private System.Windows.Forms.Label labelConfigServiceEndpointCaption;
        private System.Windows.Forms.Label labelT1EnterpriseName;
        private System.Windows.Forms.Label labelT1EnterpriseNameCaption;
        private System.Windows.Forms.Label labelT2EnterpriseName;
        private System.Windows.Forms.Label labelT2EnterpriseNameCaption;
        private System.Windows.Forms.Label labelT3EnterpriseName;
        private System.Windows.Forms.Label labelT3EnterpriseNameCaption;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

