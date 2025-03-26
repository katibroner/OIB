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
            this.groupBoxTest = new System.Windows.Forms.GroupBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.textBoxDiscoveryEndpoint = new System.Windows.Forms.TextBox();
            this.labelServiceLocatorMexEndpoint = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxTestResult = new System.Windows.Forms.GroupBox();
            this.labelConfigManagerEndpoint = new System.Windows.Forms.Label();
            this.labelConfigManagerEndpointCaption = new System.Windows.Forms.Label();
            this.labelServiceLocatorEndpoint = new System.Windows.Forms.Label();
            this.labelUsedServiceLocatorCaption = new System.Windows.Forms.Label();
            this.labelEnterpriseName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelConfigManagerMex = new System.Windows.Forms.Label();
            this.labelConfigManagerMexCaption = new System.Windows.Forms.Label();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.groupBoxTest.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBoxTestResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTest
            // 
            this.groupBoxTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTest.Controls.Add(this._cbUseClientAuthentication);
            this.groupBoxTest.Controls.Add(this.buttonTest);
            this.groupBoxTest.Controls.Add(this.textBoxDiscoveryEndpoint);
            this.groupBoxTest.Controls.Add(this.labelServiceLocatorMexEndpoint);
            this.groupBoxTest.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTest.Name = "groupBoxTest";
            this.groupBoxTest.Size = new System.Drawing.Size(756, 87);
            this.groupBoxTest.TabIndex = 6;
            this.groupBoxTest.TabStop = false;
            this.groupBoxTest.Text = "Accessing OIB Configuration Service using OIB Discovery";
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(27, 54);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(113, 23);
            this.buttonTest.TabIndex = 8;
            this.buttonTest.Text = "Run Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.ButtonTest_Click);
            // 
            // textBoxDiscoveryEndpoint
            // 
            this.textBoxDiscoveryEndpoint.Location = new System.Drawing.Point(245, 23);
            this.textBoxDiscoveryEndpoint.Name = "textBoxDiscoveryEndpoint";
            this.textBoxDiscoveryEndpoint.Size = new System.Drawing.Size(494, 20);
            this.textBoxDiscoveryEndpoint.TabIndex = 7;
            this.textBoxDiscoveryEndpoint.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/mex";
            // 
            // labelServiceLocatorMexEndpoint
            // 
            this.labelServiceLocatorMexEndpoint.AutoSize = true;
            this.labelServiceLocatorMexEndpoint.Location = new System.Drawing.Point(24, 26);
            this.labelServiceLocatorMexEndpoint.Name = "labelServiceLocatorMexEndpoint";
            this.labelServiceLocatorMexEndpoint.Size = new System.Drawing.Size(144, 13);
            this.labelServiceLocatorMexEndpoint.TabIndex = 6;
            this.labelServiceLocatorMexEndpoint.Text = "Service locator service MEX:";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 247);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(780, 22);
            this.statusStrip.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBoxTestResult
            // 
            this.groupBoxTestResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTestResult.Controls.Add(this.labelConfigManagerEndpoint);
            this.groupBoxTestResult.Controls.Add(this.labelConfigManagerEndpointCaption);
            this.groupBoxTestResult.Controls.Add(this.labelServiceLocatorEndpoint);
            this.groupBoxTestResult.Controls.Add(this.labelUsedServiceLocatorCaption);
            this.groupBoxTestResult.Controls.Add(this.labelEnterpriseName);
            this.groupBoxTestResult.Controls.Add(this.label1);
            this.groupBoxTestResult.Controls.Add(this.labelConfigManagerMex);
            this.groupBoxTestResult.Controls.Add(this.labelConfigManagerMexCaption);
            this.groupBoxTestResult.Location = new System.Drawing.Point(12, 105);
            this.groupBoxTestResult.Name = "groupBoxTestResult";
            this.groupBoxTestResult.Size = new System.Drawing.Size(756, 128);
            this.groupBoxTestResult.TabIndex = 11;
            this.groupBoxTestResult.TabStop = false;
            this.groupBoxTestResult.Text = "Test Result";
            // 
            // labelConfigManagerEndpoint
            // 
            this.labelConfigManagerEndpoint.AutoSize = true;
            this.labelConfigManagerEndpoint.Location = new System.Drawing.Point(242, 74);
            this.labelConfigManagerEndpoint.Name = "labelConfigManagerEndpoint";
            this.labelConfigManagerEndpoint.Size = new System.Drawing.Size(10, 13);
            this.labelConfigManagerEndpoint.TabIndex = 18;
            this.labelConfigManagerEndpoint.Text = "-";
            // 
            // labelConfigManagerEndpointCaption
            // 
            this.labelConfigManagerEndpointCaption.AutoSize = true;
            this.labelConfigManagerEndpointCaption.Location = new System.Drawing.Point(24, 74);
            this.labelConfigManagerEndpointCaption.Name = "labelConfigManagerEndpointCaption";
            this.labelConfigManagerEndpointCaption.Size = new System.Drawing.Size(199, 13);
            this.labelConfigManagerEndpointCaption.TabIndex = 17;
            this.labelConfigManagerEndpointCaption.Text = "3. Used configuration manager endpoint:";
            // 
            // labelServiceLocatorEndpoint
            // 
            this.labelServiceLocatorEndpoint.AutoSize = true;
            this.labelServiceLocatorEndpoint.Location = new System.Drawing.Point(242, 27);
            this.labelServiceLocatorEndpoint.Name = "labelServiceLocatorEndpoint";
            this.labelServiceLocatorEndpoint.Size = new System.Drawing.Size(10, 13);
            this.labelServiceLocatorEndpoint.TabIndex = 16;
            this.labelServiceLocatorEndpoint.Text = "-";
            // 
            // labelUsedServiceLocatorCaption
            // 
            this.labelUsedServiceLocatorCaption.AutoSize = true;
            this.labelUsedServiceLocatorCaption.Location = new System.Drawing.Point(24, 27);
            this.labelUsedServiceLocatorCaption.Name = "labelUsedServiceLocatorCaption";
            this.labelUsedServiceLocatorCaption.Size = new System.Drawing.Size(163, 13);
            this.labelUsedServiceLocatorCaption.TabIndex = 15;
            this.labelUsedServiceLocatorCaption.Text = "1. Used service locator endpoint:";
            // 
            // labelEnterpriseName
            // 
            this.labelEnterpriseName.AutoSize = true;
            this.labelEnterpriseName.Location = new System.Drawing.Point(242, 98);
            this.labelEnterpriseName.Name = "labelEnterpriseName";
            this.labelEnterpriseName.Size = new System.Drawing.Size(10, 13);
            this.labelEnterpriseName.TabIndex = 14;
            this.labelEnterpriseName.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "4. Name of the enterprise factory element:";
            // 
            // labelConfigManagerMex
            // 
            this.labelConfigManagerMex.AutoSize = true;
            this.labelConfigManagerMex.Location = new System.Drawing.Point(242, 50);
            this.labelConfigManagerMex.Name = "labelConfigManagerMex";
            this.labelConfigManagerMex.Size = new System.Drawing.Size(10, 13);
            this.labelConfigManagerMex.TabIndex = 12;
            this.labelConfigManagerMex.Text = "-";
            // 
            // labelConfigManagerMexCaption
            // 
            this.labelConfigManagerMexCaption.AutoSize = true;
            this.labelConfigManagerMexCaption.Location = new System.Drawing.Point(24, 50);
            this.labelConfigManagerMexCaption.Name = "labelConfigManagerMexCaption";
            this.labelConfigManagerMexCaption.Size = new System.Drawing.Size(154, 13);
            this.labelConfigManagerMexCaption.TabIndex = 6;
            this.labelConfigManagerMexCaption.Text = "2. Configuration manager MEX:";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(245, 60);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 9;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 269);
            this.Controls.Add(this.groupBoxTestResult);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBoxTest);
            this.Name = "mainForm";
            this.Text = "OIB Tutorial";
            this.groupBoxTest.ResumeLayout(false);
            this.groupBoxTest.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBoxTestResult.ResumeLayout(false);
            this.groupBoxTestResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTest;
        private System.Windows.Forms.TextBox textBoxDiscoveryEndpoint;
        private System.Windows.Forms.Label labelServiceLocatorMexEndpoint;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBoxTestResult;
        private System.Windows.Forms.Label labelConfigManagerMex;
        private System.Windows.Forms.Label labelConfigManagerMexCaption;
        private System.Windows.Forms.Label labelEnterpriseName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelConfigManagerEndpoint;
        private System.Windows.Forms.Label labelConfigManagerEndpointCaption;
        private System.Windows.Forms.Label labelServiceLocatorEndpoint;
        private System.Windows.Forms.Label labelUsedServiceLocatorCaption;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

