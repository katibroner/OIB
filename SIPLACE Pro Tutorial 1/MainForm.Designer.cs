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
            this._GroupBoxTest = new System.Windows.Forms.GroupBox();
            this._ButtonTest = new System.Windows.Forms.Button();
            this._LabelServiceLocatorMexCaption = new System.Windows.Forms.Label();
            this._TextBoxDiscoveryEndpoint = new System.Windows.Forms.TextBox();
            this._GroupBoxConfiguration = new System.Windows.Forms.GroupBox();
            this._LabelFactoryItemType = new System.Windows.Forms.Label();
            this._LabelFactoryItemName = new System.Windows.Forms.Label();
            this._LabelServiceName = new System.Windows.Forms.Label();
            this._LabelFactoryItemTypeCaption = new System.Windows.Forms.Label();
            this._LabelFactoryItemNameCaption = new System.Windows.Forms.Label();
            this._LabelServiceNameCaption = new System.Windows.Forms.Label();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this._GroupBoxTest.SuspendLayout();
            this._GroupBoxConfiguration.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(782, 22);
            this.statusStrip1.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _GroupBoxTest
            // 
            this._GroupBoxTest.Controls.Add(this._cbUseClientAuthentication);
            this._GroupBoxTest.Controls.Add(this._ButtonTest);
            this._GroupBoxTest.Location = new System.Drawing.Point(14, 149);
            this._GroupBoxTest.Name = "_GroupBoxTest";
            this._GroupBoxTest.Size = new System.Drawing.Size(756, 72);
            this._GroupBoxTest.TabIndex = 18;
            this._GroupBoxTest.TabStop = false;
            this._GroupBoxTest.Text = "Test: Connection Test";
            // 
            // _ButtonTest
            // 
            this._ButtonTest.Location = new System.Drawing.Point(25, 29);
            this._ButtonTest.Name = "_ButtonTest";
            this._ButtonTest.Size = new System.Drawing.Size(188, 23);
            this._ButtonTest.TabIndex = 29;
            this._ButtonTest.Text = "Test Connection";
            this._ButtonTest.UseVisualStyleBackColor = true;
            this._ButtonTest.Click += new System.EventHandler(this.TestConnection_Click);
            // 
            // _LabelServiceLocatorMexCaption
            // 
            this._LabelServiceLocatorMexCaption.AutoSize = true;
            this._LabelServiceLocatorMexCaption.Location = new System.Drawing.Point(24, 26);
            this._LabelServiceLocatorMexCaption.Name = "_LabelServiceLocatorMexCaption";
            this._LabelServiceLocatorMexCaption.Size = new System.Drawing.Size(123, 13);
            this._LabelServiceLocatorMexCaption.TabIndex = 6;
            this._LabelServiceLocatorMexCaption.Text = "OIB service locator MEX";
            // 
            // _TextBoxDiscoveryEndpoint
            // 
            this._TextBoxDiscoveryEndpoint.Location = new System.Drawing.Point(245, 23);
            this._TextBoxDiscoveryEndpoint.Name = "_TextBoxDiscoveryEndpoint";
            this._TextBoxDiscoveryEndpoint.Size = new System.Drawing.Size(493, 20);
            this._TextBoxDiscoveryEndpoint.TabIndex = 7;
            this._TextBoxDiscoveryEndpoint.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/mex";
            // 
            // _GroupBoxConfiguration
            // 
            this._GroupBoxConfiguration.Controls.Add(this._LabelFactoryItemType);
            this._GroupBoxConfiguration.Controls.Add(this._LabelFactoryItemName);
            this._GroupBoxConfiguration.Controls.Add(this._LabelServiceName);
            this._GroupBoxConfiguration.Controls.Add(this._LabelFactoryItemTypeCaption);
            this._GroupBoxConfiguration.Controls.Add(this._LabelFactoryItemNameCaption);
            this._GroupBoxConfiguration.Controls.Add(this._LabelServiceNameCaption);
            this._GroupBoxConfiguration.Controls.Add(this._TextBoxDiscoveryEndpoint);
            this._GroupBoxConfiguration.Controls.Add(this._LabelServiceLocatorMexCaption);
            this._GroupBoxConfiguration.Location = new System.Drawing.Point(12, 12);
            this._GroupBoxConfiguration.Name = "_GroupBoxConfiguration";
            this._GroupBoxConfiguration.Size = new System.Drawing.Size(756, 131);
            this._GroupBoxConfiguration.TabIndex = 17;
            this._GroupBoxConfiguration.TabStop = false;
            this._GroupBoxConfiguration.Text = "Test: OIB Discovery";
            // 
            // _LabelFactoryItemType
            // 
            this._LabelFactoryItemType.AutoSize = true;
            this._LabelFactoryItemType.Location = new System.Drawing.Point(242, 98);
            this._LabelFactoryItemType.Name = "_LabelFactoryItemType";
            this._LabelFactoryItemType.Size = new System.Drawing.Size(78, 13);
            this._LabelFactoryItemType.TabIndex = 13;
            this._LabelFactoryItemType.Text = "ProductionLine";
            // 
            // _LabelFactoryItemName
            // 
            this._LabelFactoryItemName.AutoSize = true;
            this._LabelFactoryItemName.Location = new System.Drawing.Point(242, 76);
            this._LabelFactoryItemName.Name = "_LabelFactoryItemName";
            this._LabelFactoryItemName.Size = new System.Drawing.Size(36, 13);
            this._LabelFactoryItemName.TabIndex = 12;
            this._LabelFactoryItemName.Text = "Line 1";
            // 
            // _LabelServiceName
            // 
            this._LabelServiceName.AutoSize = true;
            this._LabelServiceName.Location = new System.Drawing.Point(242, 54);
            this._LabelServiceName.Name = "_LabelServiceName";
            this._LabelServiceName.Size = new System.Drawing.Size(90, 13);
            this._LabelServiceName.TabIndex = 11;
            this._LabelServiceName.Text = "SIPLACE.Pro.SPI";
            // 
            // _LabelFactoryItemTypeCaption
            // 
            this._LabelFactoryItemTypeCaption.AutoSize = true;
            this._LabelFactoryItemTypeCaption.Location = new System.Drawing.Point(24, 98);
            this._LabelFactoryItemTypeCaption.Name = "_LabelFactoryItemTypeCaption";
            this._LabelFactoryItemTypeCaption.Size = new System.Drawing.Size(92, 13);
            this._LabelFactoryItemTypeCaption.TabIndex = 10;
            this._LabelFactoryItemTypeCaption.Text = "Factory Item Type";
            // 
            // _LabelFactoryItemNameCaption
            // 
            this._LabelFactoryItemNameCaption.AutoSize = true;
            this._LabelFactoryItemNameCaption.Location = new System.Drawing.Point(24, 76);
            this._LabelFactoryItemNameCaption.Name = "_LabelFactoryItemNameCaption";
            this._LabelFactoryItemNameCaption.Size = new System.Drawing.Size(96, 13);
            this._LabelFactoryItemNameCaption.TabIndex = 9;
            this._LabelFactoryItemNameCaption.Text = "Factory Item Name";
            // 
            // _LabelServiceNameCaption
            // 
            this._LabelServiceNameCaption.AutoSize = true;
            this._LabelServiceNameCaption.Location = new System.Drawing.Point(24, 54);
            this._LabelServiceNameCaption.Name = "_LabelServiceNameCaption";
            this._LabelServiceNameCaption.Size = new System.Drawing.Size(140, 13);
            this._LabelServiceNameCaption.TabIndex = 8;
            this._LabelServiceNameCaption.Text = "SIPLACE Pro Service Name";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(238, 33);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 30;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 261);
            this.Controls.Add(this._GroupBoxTest);
            this.Controls.Add(this._GroupBoxConfiguration);
            this.Controls.Add(this.statusStrip1);
            this.Name = "mainForm";
            this.Text = "OIB Tutorial";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this._GroupBoxTest.ResumeLayout(false);
            this._GroupBoxTest.PerformLayout();
            this._GroupBoxConfiguration.ResumeLayout(false);
            this._GroupBoxConfiguration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.GroupBox _GroupBoxTest;
        private System.Windows.Forms.Button _ButtonTest;
        private System.Windows.Forms.Label _LabelServiceLocatorMexCaption;
        private System.Windows.Forms.TextBox _TextBoxDiscoveryEndpoint;
        private System.Windows.Forms.GroupBox _GroupBoxConfiguration;
        private System.Windows.Forms.Label _LabelFactoryItemType;
        private System.Windows.Forms.Label _LabelServiceName;
        private System.Windows.Forms.Label _LabelFactoryItemTypeCaption;
        private System.Windows.Forms.Label _LabelFactoryItemNameCaption;
        private System.Windows.Forms.Label _LabelServiceNameCaption;
        private System.Windows.Forms.Label _LabelFactoryItemName;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

