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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFactoryElements = new System.Windows.Forms.ComboBox();
            this.groupBoxTestResult = new System.Windows.Forms.GroupBox();
            this.labelSetupCenterEndpointAddress = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.labelLineControlEndpointAddress = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelOptimizeEndpointAddress = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelSpiEndpointAddress = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxTest = new System.Windows.Forms.GroupBox();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonConfigureServices = new System.Windows.Forms.Button();
            this.buttonDeleteConfiguration = new System.Windows.Forms.Button();
            this.buttonCreateLayout = new System.Windows.Forms.Button();
            this.textBoxDiscoveryEndpoint = new System.Windows.Forms.TextBox();
            this.labelServiceLocatorMexCaption = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.groupBoxTestResult.SuspendLayout();
            this.groupBoxTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 355);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(776, 5);
            this.statusStrip1.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 0);
            this._ToolStripStatusLabel.Spring = true;
            this._ToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select OIB Factory Layout Element";
            // 
            // comboBoxFactoryElements
            // 
            this.comboBoxFactoryElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFactoryElements.FormattingEnabled = true;
            this.comboBoxFactoryElements.Location = new System.Drawing.Point(245, 166);
            this.comboBoxFactoryElements.Name = "comboBoxFactoryElements";
            this.comboBoxFactoryElements.Size = new System.Drawing.Size(493, 21);
            this.comboBoxFactoryElements.TabIndex = 21;
            this.comboBoxFactoryElements.SelectedIndexChanged += new System.EventHandler(this.ComboBoxFactoryElements_SelectedIndexChanged);
            // 
            // groupBoxTestResult
            // 
            this.groupBoxTestResult.Controls.Add(this.labelSetupCenterEndpointAddress);
            this.groupBoxTestResult.Controls.Add(this.label15);
            this.groupBoxTestResult.Controls.Add(this.labelLineControlEndpointAddress);
            this.groupBoxTestResult.Controls.Add(this.label13);
            this.groupBoxTestResult.Controls.Add(this.labelOptimizeEndpointAddress);
            this.groupBoxTestResult.Controls.Add(this.label11);
            this.groupBoxTestResult.Controls.Add(this.labelSpiEndpointAddress);
            this.groupBoxTestResult.Controls.Add(this.label9);
            this.groupBoxTestResult.Location = new System.Drawing.Point(12, 228);
            this.groupBoxTestResult.Name = "groupBoxTestResult";
            this.groupBoxTestResult.Size = new System.Drawing.Size(757, 110);
            this.groupBoxTestResult.TabIndex = 16;
            this.groupBoxTestResult.TabStop = false;
            this.groupBoxTestResult.Text = "Test result: Discovery MEX Addresses";
            // 
            // labelSetupCenterEndpointAddress
            // 
            this.labelSetupCenterEndpointAddress.AutoSize = true;
            this.labelSetupCenterEndpointAddress.Location = new System.Drawing.Point(242, 91);
            this.labelSetupCenterEndpointAddress.Name = "labelSetupCenterEndpointAddress";
            this.labelSetupCenterEndpointAddress.Size = new System.Drawing.Size(10, 13);
            this.labelSetupCenterEndpointAddress.TabIndex = 33;
            this.labelSetupCenterEndpointAddress.Text = "-";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 91);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 32;
            this.label15.Text = "Setup Center:";
            // 
            // labelLineControlEndpointAddress
            // 
            this.labelLineControlEndpointAddress.AutoSize = true;
            this.labelLineControlEndpointAddress.Location = new System.Drawing.Point(242, 68);
            this.labelLineControlEndpointAddress.Name = "labelLineControlEndpointAddress";
            this.labelLineControlEndpointAddress.Size = new System.Drawing.Size(10, 13);
            this.labelLineControlEndpointAddress.TabIndex = 31;
            this.labelLineControlEndpointAddress.Text = "-";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Line Control:";
            // 
            // labelOptimizeEndpointAddress
            // 
            this.labelOptimizeEndpointAddress.AutoSize = true;
            this.labelOptimizeEndpointAddress.Location = new System.Drawing.Point(242, 45);
            this.labelOptimizeEndpointAddress.Name = "labelOptimizeEndpointAddress";
            this.labelOptimizeEndpointAddress.Size = new System.Drawing.Size(10, 13);
            this.labelOptimizeEndpointAddress.TabIndex = 29;
            this.labelOptimizeEndpointAddress.Text = "-";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Optimizer:";
            // 
            // labelSpiEndpointAddress
            // 
            this.labelSpiEndpointAddress.AutoSize = true;
            this.labelSpiEndpointAddress.Location = new System.Drawing.Point(242, 23);
            this.labelSpiEndpointAddress.Name = "labelSpiEndpointAddress";
            this.labelSpiEndpointAddress.Size = new System.Drawing.Size(10, 13);
            this.labelSpiEndpointAddress.TabIndex = 27;
            this.labelSpiEndpointAddress.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "SPI:";
            // 
            // groupBoxTest
            // 
            this.groupBoxTest.Controls.Add(this._cbUseClientAuthentication);
            this.groupBoxTest.Controls.Add(this.label5);
            this.groupBoxTest.Controls.Add(this.label4);
            this.groupBoxTest.Controls.Add(this.label3);
            this.groupBoxTest.Controls.Add(this.label2);
            this.groupBoxTest.Controls.Add(this.buttonConfigureServices);
            this.groupBoxTest.Controls.Add(this.buttonDeleteConfiguration);
            this.groupBoxTest.Controls.Add(this.buttonCreateLayout);
            this.groupBoxTest.Controls.Add(this.textBoxDiscoveryEndpoint);
            this.groupBoxTest.Controls.Add(this.labelServiceLocatorMexCaption);
            this.groupBoxTest.Controls.Add(this.comboBoxFactoryElements);
            this.groupBoxTest.Controls.Add(this.label1);
            this.groupBoxTest.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTest.Name = "groupBoxTest";
            this.groupBoxTest.Size = new System.Drawing.Size(756, 199);
            this.groupBoxTest.TabIndex = 17;
            this.groupBoxTest.TabStop = false;
            this.groupBoxTest.Text = "Test: OIB Discovery";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(46, 49);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(614, 17);
            this._cbUseClientAuthentication.TabIndex = 33;
            this._cbUseClientAuthentication.Text = "Use Client Authentication (If you are about to use Client Authentication, please " +
    "activate checkbox before starting this tutorial.)";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "4.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "3.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "2.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "1.";
            // 
            // buttonConfigureServices
            // 
            this.buttonConfigureServices.Location = new System.Drawing.Point(46, 132);
            this.buttonConfigureServices.Name = "buttonConfigureServices";
            this.buttonConfigureServices.Size = new System.Drawing.Size(188, 23);
            this.buttonConfigureServices.TabIndex = 28;
            this.buttonConfigureServices.Text = "Assign Services";
            this.buttonConfigureServices.UseVisualStyleBackColor = true;
            this.buttonConfigureServices.Click += new System.EventHandler(this.ButtonConfigureServices_Click);
            // 
            // buttonDeleteConfiguration
            // 
            this.buttonDeleteConfiguration.Location = new System.Drawing.Point(46, 74);
            this.buttonDeleteConfiguration.Name = "buttonDeleteConfiguration";
            this.buttonDeleteConfiguration.Size = new System.Drawing.Size(188, 23);
            this.buttonDeleteConfiguration.TabIndex = 27;
            this.buttonDeleteConfiguration.Text = "Delete Layout && Service Assignments";
            this.buttonDeleteConfiguration.UseVisualStyleBackColor = true;
            this.buttonDeleteConfiguration.Click += new System.EventHandler(this.ButtonDeleteConfiguration_Click);
            // 
            // buttonCreateLayout
            // 
            this.buttonCreateLayout.Location = new System.Drawing.Point(46, 103);
            this.buttonCreateLayout.Name = "buttonCreateLayout";
            this.buttonCreateLayout.Size = new System.Drawing.Size(188, 23);
            this.buttonCreateLayout.TabIndex = 26;
            this.buttonCreateLayout.Text = "Create Layout";
            this.buttonCreateLayout.UseVisualStyleBackColor = true;
            this.buttonCreateLayout.Click += new System.EventHandler(this.ButtonCreateLayout_Click);
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
            // mainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 360);
            this.Controls.Add(this.groupBoxTest);
            this.Controls.Add(this.groupBoxTestResult);
            this.Controls.Add(this.statusStrip1);
            this.Name = "mainForm";
            this.Text = "OIB Tutorial";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxTestResult.ResumeLayout(false);
            this.groupBoxTestResult.PerformLayout();
            this.groupBoxTest.ResumeLayout(false);
            this.groupBoxTest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFactoryElements;
        private System.Windows.Forms.GroupBox groupBoxTestResult;
        private System.Windows.Forms.Label labelSetupCenterEndpointAddress;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelLineControlEndpointAddress;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelOptimizeEndpointAddress;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelSpiEndpointAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBoxTest;
        private System.Windows.Forms.Button buttonConfigureServices;
        private System.Windows.Forms.Button buttonDeleteConfiguration;
        private System.Windows.Forms.Button buttonCreateLayout;
        private System.Windows.Forms.TextBox textBoxDiscoveryEndpoint;
        private System.Windows.Forms.Label labelServiceLocatorMexCaption;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

