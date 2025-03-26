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
            this.buttonCreateConfig = new System.Windows.Forms.Button();
            this.buttonDeleteConfig = new System.Windows.Forms.Button();
            this.buttonRefreshView = new System.Windows.Forms.Button();
            this.groupBoxTest = new System.Windows.Forms.GroupBox();
            this.textBoxDiscoveryEndpoint = new System.Windows.Forms.TextBox();
            this.labelServiceLocatorMexCaption = new System.Windows.Forms.Label();
            this.groupBoxTestResult = new System.Windows.Forms.GroupBox();
            this.labelFactoryLayoutCaption = new System.Windows.Forms.Label();
            this.treeViewFactoryLayout = new System.Windows.Forms.TreeView();
            this.labelConfigManagerEndpoint = new System.Windows.Forms.Label();
            this.labelConfigManagerEndpointCaption = new System.Windows.Forms.Label();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.groupBoxTest.SuspendLayout();
            this.groupBoxTestResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCreateConfig
            // 
            this.buttonCreateConfig.Location = new System.Drawing.Point(27, 53);
            this.buttonCreateConfig.Name = "buttonCreateConfig";
            this.buttonCreateConfig.Size = new System.Drawing.Size(167, 23);
            this.buttonCreateConfig.TabIndex = 7;
            this.buttonCreateConfig.Text = "Create Factory Layout";
            this.buttonCreateConfig.UseVisualStyleBackColor = true;
            this.buttonCreateConfig.Click += new System.EventHandler(this.ButtonCreateConfig_Click);
            // 
            // buttonDeleteConfig
            // 
            this.buttonDeleteConfig.Location = new System.Drawing.Point(200, 53);
            this.buttonDeleteConfig.Name = "buttonDeleteConfig";
            this.buttonDeleteConfig.Size = new System.Drawing.Size(167, 23);
            this.buttonDeleteConfig.TabIndex = 8;
            this.buttonDeleteConfig.Text = "Delete Factory Layout";
            this.buttonDeleteConfig.UseVisualStyleBackColor = true;
            this.buttonDeleteConfig.Click += new System.EventHandler(this.ButtonDeleteConfig_Click);
            // 
            // buttonRefreshView
            // 
            this.buttonRefreshView.Location = new System.Drawing.Point(373, 53);
            this.buttonRefreshView.Name = "buttonRefreshView";
            this.buttonRefreshView.Size = new System.Drawing.Size(167, 23);
            this.buttonRefreshView.TabIndex = 10;
            this.buttonRefreshView.Text = "Update View";
            this.buttonRefreshView.UseVisualStyleBackColor = true;
            this.buttonRefreshView.Click += new System.EventHandler(this.ButtonRefreshView_Click);
            // 
            // groupBoxTest
            // 
            this.groupBoxTest.Controls.Add(this._cbUseClientAuthentication);
            this.groupBoxTest.Controls.Add(this.textBoxDiscoveryEndpoint);
            this.groupBoxTest.Controls.Add(this.buttonRefreshView);
            this.groupBoxTest.Controls.Add(this.labelServiceLocatorMexCaption);
            this.groupBoxTest.Controls.Add(this.buttonCreateConfig);
            this.groupBoxTest.Controls.Add(this.buttonDeleteConfig);
            this.groupBoxTest.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTest.Name = "groupBoxTest";
            this.groupBoxTest.Size = new System.Drawing.Size(756, 92);
            this.groupBoxTest.TabIndex = 11;
            this.groupBoxTest.TabStop = false;
            this.groupBoxTest.Text = "Test: createing, reading and deleting the OIB Factory Layout";
            // 
            // textBoxDiscoveryEndpoint
            // 
            this.textBoxDiscoveryEndpoint.Location = new System.Drawing.Point(245, 23);
            this.textBoxDiscoveryEndpoint.Name = "textBoxDiscoveryEndpoint";
            this.textBoxDiscoveryEndpoint.Size = new System.Drawing.Size(493, 20);
            this.textBoxDiscoveryEndpoint.TabIndex = 7;
            this.textBoxDiscoveryEndpoint.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/mex";
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
            // groupBoxTestResult
            // 
            this.groupBoxTestResult.Controls.Add(this.labelFactoryLayoutCaption);
            this.groupBoxTestResult.Controls.Add(this.treeViewFactoryLayout);
            this.groupBoxTestResult.Controls.Add(this.labelConfigManagerEndpoint);
            this.groupBoxTestResult.Controls.Add(this.labelConfigManagerEndpointCaption);
            this.groupBoxTestResult.Location = new System.Drawing.Point(12, 110);
            this.groupBoxTestResult.Name = "groupBoxTestResult";
            this.groupBoxTestResult.Size = new System.Drawing.Size(756, 407);
            this.groupBoxTestResult.TabIndex = 12;
            this.groupBoxTestResult.TabStop = false;
            this.groupBoxTestResult.Text = "Test result";
            // 
            // labelFactoryLayoutCaption
            // 
            this.labelFactoryLayoutCaption.AutoSize = true;
            this.labelFactoryLayoutCaption.Location = new System.Drawing.Point(24, 63);
            this.labelFactoryLayoutCaption.Name = "labelFactoryLayoutCaption";
            this.labelFactoryLayoutCaption.Size = new System.Drawing.Size(139, 13);
            this.labelFactoryLayoutCaption.TabIndex = 9;
            this.labelFactoryLayoutCaption.Text = "OIB Factory Layout (ISA 95)";
            // 
            // treeViewFactoryLayout
            // 
            this.treeViewFactoryLayout.Location = new System.Drawing.Point(26, 84);
            this.treeViewFactoryLayout.Name = "treeViewFactoryLayout";
            this.treeViewFactoryLayout.Size = new System.Drawing.Size(712, 306);
            this.treeViewFactoryLayout.TabIndex = 8;
            // 
            // labelConfigManagerEndpoint
            // 
            this.labelConfigManagerEndpoint.AutoSize = true;
            this.labelConfigManagerEndpoint.Location = new System.Drawing.Point(242, 26);
            this.labelConfigManagerEndpoint.Name = "labelConfigManagerEndpoint";
            this.labelConfigManagerEndpoint.Size = new System.Drawing.Size(10, 13);
            this.labelConfigManagerEndpoint.TabIndex = 7;
            this.labelConfigManagerEndpoint.Text = "-";
            // 
            // labelConfigManagerEndpointCaption
            // 
            this.labelConfigManagerEndpointCaption.AutoSize = true;
            this.labelConfigManagerEndpointCaption.Location = new System.Drawing.Point(24, 26);
            this.labelConfigManagerEndpointCaption.Name = "labelConfigManagerEndpointCaption";
            this.labelConfigManagerEndpointCaption.Size = new System.Drawing.Size(160, 13);
            this.labelConfigManagerEndpointCaption.TabIndex = 6;
            this.labelConfigManagerEndpointCaption.Text = "Configuration manager endpoint:";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(574, 59);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 11;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 529);
            this.Controls.Add(this.groupBoxTestResult);
            this.Controls.Add(this.groupBoxTest);
            this.Name = "mainForm";
            this.Text = "OIB Tutorial";
            this.groupBoxTest.ResumeLayout(false);
            this.groupBoxTest.PerformLayout();
            this.groupBoxTestResult.ResumeLayout(false);
            this.groupBoxTestResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateConfig;
        private System.Windows.Forms.Button buttonDeleteConfig;
        private System.Windows.Forms.Button buttonRefreshView;
        private System.Windows.Forms.GroupBox groupBoxTest;
        private System.Windows.Forms.TextBox textBoxDiscoveryEndpoint;
        private System.Windows.Forms.Label labelServiceLocatorMexCaption;
        private System.Windows.Forms.GroupBox groupBoxTestResult;
        private System.Windows.Forms.Label labelFactoryLayoutCaption;
        private System.Windows.Forms.TreeView treeViewFactoryLayout;
        private System.Windows.Forms.Label labelConfigManagerEndpoint;
        private System.Windows.Forms.Label labelConfigManagerEndpointCaption;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

