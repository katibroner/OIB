namespace OIB.Tutorial.SiplacePro._3
{
    partial class SPro3MainForm
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
            this.groupBoxConfiguration = new System.Windows.Forms.GroupBox();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.connectSProButton = new System.Windows.Forms.Button();
            this.SProEndpointTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitterLogMessageWindow = new System.Windows.Forms.Splitter();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.SetupPathLabel = new System.Windows.Forms.Label();
            this.setupPathTextBox = new System.Windows.Forms.TextBox();
            this.readSetupButton = new System.Windows.Forms.Button();
            this.deleteAllSpiObjectsButton = new System.Windows.Forms.Button();
            this.createBoardButton = new System.Windows.Forms.Button();
            this.createComponentsButton = new System.Windows.Forms.Button();
            this.listenDownloadEventCheckBox = new System.Windows.Forms.CheckBox();
            this.unsubscribePropertiesUpdateButton = new System.Windows.Forms.Button();
            this.subscribePropertiesUpdateButton = new System.Windows.Forms.Button();
            this.loggerListView = new OIB.Tutorial.Common.Logging.LoggerListView();
            this.groupBoxConfiguration.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxConfiguration
            // 
            this.groupBoxConfiguration.Controls.Add(this._cbUseClientAuthentication);
            this.groupBoxConfiguration.Controls.Add(this.connectSProButton);
            this.groupBoxConfiguration.Controls.Add(this.SProEndpointTextBox);
            this.groupBoxConfiguration.Controls.Add(this.label1);
            this.groupBoxConfiguration.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConfiguration.Name = "groupBoxConfiguration";
            this.groupBoxConfiguration.Size = new System.Drawing.Size(707, 76);
            this.groupBoxConfiguration.TabIndex = 1;
            this.groupBoxConfiguration.TabStop = false;
            this.groupBoxConfiguration.Text = "Configuration";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(9, 47);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 3;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // connectSProButton
            // 
            this.connectSProButton.Location = new System.Drawing.Point(619, 16);
            this.connectSProButton.Name = "connectSProButton";
            this.connectSProButton.Size = new System.Drawing.Size(75, 23);
            this.connectSProButton.TabIndex = 2;
            this.connectSProButton.Text = "Connect";
            this.connectSProButton.UseVisualStyleBackColor = true;
            this.connectSProButton.Click += new System.EventHandler(this.connectSProButton_Click);
            // 
            // SProEndpointTextBox
            // 
            this.SProEndpointTextBox.Location = new System.Drawing.Point(167, 18);
            this.SProEndpointTextBox.Name = "SProEndpointTextBox";
            this.SProEndpointTextBox.Size = new System.Drawing.Size(438, 20);
            this.SProEndpointTextBox.TabIndex = 1;
            this.SProEndpointTextBox.Text = "net.tcp://localhost:9500/Asm.As.Oib.SiplacePro/Secure";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SIPLACE Pro Adapter Endpoint";
            // 
            // splitterLogMessageWindow
            // 
            this.splitterLogMessageWindow.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterLogMessageWindow.Enabled = false;
            this.splitterLogMessageWindow.Location = new System.Drawing.Point(0, 268);
            this.splitterLogMessageWindow.Name = "splitterLogMessageWindow";
            this.splitterLogMessageWindow.Size = new System.Drawing.Size(733, 3);
            this.splitterLogMessageWindow.TabIndex = 11;
            this.splitterLogMessageWindow.TabStop = false;
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.Controls.Add(this.SetupPathLabel);
            this.panelButtons.Controls.Add(this.setupPathTextBox);
            this.panelButtons.Controls.Add(this.readSetupButton);
            this.panelButtons.Controls.Add(this.deleteAllSpiObjectsButton);
            this.panelButtons.Controls.Add(this.createBoardButton);
            this.panelButtons.Controls.Add(this.createComponentsButton);
            this.panelButtons.Controls.Add(this.listenDownloadEventCheckBox);
            this.panelButtons.Controls.Add(this.unsubscribePropertiesUpdateButton);
            this.panelButtons.Controls.Add(this.subscribePropertiesUpdateButton);
            this.panelButtons.Location = new System.Drawing.Point(0, 98);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(733, 164);
            this.panelButtons.TabIndex = 12;
            // 
            // SetupPathLabel
            // 
            this.SetupPathLabel.AutoSize = true;
            this.SetupPathLabel.Location = new System.Drawing.Point(164, 79);
            this.SetupPathLabel.Name = "SetupPathLabel";
            this.SetupPathLabel.Size = new System.Drawing.Size(60, 13);
            this.SetupPathLabel.TabIndex = 19;
            this.SetupPathLabel.Text = "Setup Path";
            // 
            // setupPathTextBox
            // 
            this.setupPathTextBox.Location = new System.Drawing.Point(161, 95);
            this.setupPathTextBox.Name = "setupPathTextBox";
            this.setupPathTextBox.Size = new System.Drawing.Size(284, 20);
            this.setupPathTextBox.TabIndex = 18;
            this.setupPathTextBox.Text = "OIB-SC-Tutorials\\X2-StartSetup @ 08-06-13 11.39.50";
            // 
            // readSetupButton
            // 
            this.readSetupButton.Location = new System.Drawing.Point(12, 93);
            this.readSetupButton.Name = "readSetupButton";
            this.readSetupButton.Size = new System.Drawing.Size(143, 23);
            this.readSetupButton.TabIndex = 17;
            this.readSetupButton.Text = "Read out Setup";
            this.readSetupButton.UseVisualStyleBackColor = true;
            this.readSetupButton.Click += new System.EventHandler(this.readSetupButton_Click);
            // 
            // deleteAllSpiObjectsButton
            // 
            this.deleteAllSpiObjectsButton.Location = new System.Drawing.Point(12, 133);
            this.deleteAllSpiObjectsButton.Name = "deleteAllSpiObjectsButton";
            this.deleteAllSpiObjectsButton.Size = new System.Drawing.Size(192, 23);
            this.deleteAllSpiObjectsButton.TabIndex = 16;
            this.deleteAllSpiObjectsButton.Text = "Delete All Tutorial\'s SPI Objects";
            this.deleteAllSpiObjectsButton.UseVisualStyleBackColor = true;
            this.deleteAllSpiObjectsButton.Click += new System.EventHandler(this.deleteAllSpiObjectsButton_Click);
            // 
            // createBoardButton
            // 
            this.createBoardButton.Location = new System.Drawing.Point(161, 48);
            this.createBoardButton.Name = "createBoardButton";
            this.createBoardButton.Size = new System.Drawing.Size(143, 23);
            this.createBoardButton.TabIndex = 15;
            this.createBoardButton.Text = "Create Board Definition";
            this.createBoardButton.UseVisualStyleBackColor = true;
            this.createBoardButton.Click += new System.EventHandler(this.createBoardButton_Click);
            // 
            // createComponentsButton
            // 
            this.createComponentsButton.Location = new System.Drawing.Point(12, 48);
            this.createComponentsButton.Name = "createComponentsButton";
            this.createComponentsButton.Size = new System.Drawing.Size(143, 23);
            this.createComponentsButton.TabIndex = 14;
            this.createComponentsButton.Text = "Create Components";
            this.createComponentsButton.UseVisualStyleBackColor = true;
            this.createComponentsButton.Click += new System.EventHandler(this.createComponentsButton_Click);
            // 
            // listenDownloadEventCheckBox
            // 
            this.listenDownloadEventCheckBox.AutoSize = true;
            this.listenDownloadEventCheckBox.Checked = true;
            this.listenDownloadEventCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listenDownloadEventCheckBox.Location = new System.Drawing.Point(451, 7);
            this.listenDownloadEventCheckBox.Name = "listenDownloadEventCheckBox";
            this.listenDownloadEventCheckBox.Size = new System.Drawing.Size(223, 17);
            this.listenDownloadEventCheckBox.TabIndex = 13;
            this.listenDownloadEventCheckBox.Text = "Listen only for \"Recipe  Download\" Event";
            this.listenDownloadEventCheckBox.UseVisualStyleBackColor = true;
            // 
            // unsubscribePropertiesUpdateButton
            // 
            this.unsubscribePropertiesUpdateButton.Enabled = false;
            this.unsubscribePropertiesUpdateButton.Location = new System.Drawing.Point(236, 3);
            this.unsubscribePropertiesUpdateButton.Name = "unsubscribePropertiesUpdateButton";
            this.unsubscribePropertiesUpdateButton.Size = new System.Drawing.Size(209, 23);
            this.unsubscribePropertiesUpdateButton.TabIndex = 12;
            this.unsubscribePropertiesUpdateButton.Text = "Unsubscribe Properties Update Events";
            this.unsubscribePropertiesUpdateButton.UseVisualStyleBackColor = true;
            this.unsubscribePropertiesUpdateButton.Click += new System.EventHandler(this.unsubscribePropertiesUpdateButton_Click);
            // 
            // subscribePropertiesUpdateButton
            // 
            this.subscribePropertiesUpdateButton.Location = new System.Drawing.Point(12, 3);
            this.subscribePropertiesUpdateButton.Name = "subscribePropertiesUpdateButton";
            this.subscribePropertiesUpdateButton.Size = new System.Drawing.Size(218, 23);
            this.subscribePropertiesUpdateButton.TabIndex = 11;
            this.subscribePropertiesUpdateButton.Text = "Subscribe for Properties Update Events";
            this.subscribePropertiesUpdateButton.UseVisualStyleBackColor = true;
            this.subscribePropertiesUpdateButton.Click += new System.EventHandler(this.subscribePropertiesUpdateButton_Click);
            // 
            // loggerListView
            // 
            this.loggerListView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loggerListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loggerListView.Location = new System.Drawing.Point(0, 271);
            this.loggerListView.Margin = new System.Windows.Forms.Padding(4);
            this.loggerListView.Name = "loggerListView";
            this.loggerListView.Size = new System.Drawing.Size(733, 178);
            this.loggerListView.TabIndex = 0;
            // 
            // SPro3MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 449);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.splitterLogMessageWindow);
            this.Controls.Add(this.groupBoxConfiguration);
            this.Controls.Add(this.loggerListView);
            this.MinimumSize = new System.Drawing.Size(749, 456);
            this.Name = "SPro3MainForm";
            this.Text = "SIPLACE Pro Tutorial 3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SPro3MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.SPro3MainForm_Shown);
            this.Resize += new System.EventHandler(this.SPro3MainForm_Resize);
            this.groupBoxConfiguration.ResumeLayout(false);
            this.groupBoxConfiguration.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.Logging.LoggerListView loggerListView;
        private System.Windows.Forms.GroupBox groupBoxConfiguration;
        private System.Windows.Forms.Button connectSProButton;
        private System.Windows.Forms.TextBox SProEndpointTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Splitter splitterLogMessageWindow;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Label SetupPathLabel;
        private System.Windows.Forms.TextBox setupPathTextBox;
        private System.Windows.Forms.Button readSetupButton;
        private System.Windows.Forms.Button deleteAllSpiObjectsButton;
        private System.Windows.Forms.Button createBoardButton;
        private System.Windows.Forms.Button createComponentsButton;
        private System.Windows.Forms.CheckBox listenDownloadEventCheckBox;
        private System.Windows.Forms.Button unsubscribePropertiesUpdateButton;
        private System.Windows.Forms.Button subscribePropertiesUpdateButton;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

