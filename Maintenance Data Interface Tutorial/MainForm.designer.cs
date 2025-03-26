namespace MaintenanceDataTutorial
{
    partial class MainForm
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
            this._textBoxCoreName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._checkBoxUseAppConfig = new System.Windows.Forms.CheckBox();
            this._buttonSubscribe = new System.Windows.Forms.Button();
            this._buttonUnsubscribe = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._labelCurrentSubscriptionId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this._labelCallbackUri = new System.Windows.Forms.Label();
            this._listViewMessages = new System.Windows.Forms.ListView();
            this._columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeaderMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._buttonInitialize = new System.Windows.Forms.Button();
            this._checkBoxUnsubscribe = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this._textBoxLineFullPath = new System.Windows.Forms.TextBox();
            this._checkBoxFilterLine = new System.Windows.Forms.CheckBox();
            this._cbPortSharing = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._buttonGetConfiguration = new System.Windows.Forms.Button();
            this._listBoxEndpoints = new System.Windows.Forms.ListBox();
            this._buttonCopyToClipboard = new System.Windows.Forms.Button();
            this._buttonClearMessages = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownPort)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _textBoxCoreName
            // 
            this._textBoxCoreName.Location = new System.Drawing.Point(14, 42);
            this._textBoxCoreName.Name = "_textBoxCoreName";
            this._textBoxCoreName.Size = new System.Drawing.Size(129, 20);
            this._textBoxCoreName.TabIndex = 0;
            this._textBoxCoreName.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Core Computer Name:";
            // 
            // _checkBoxUseAppConfig
            // 
            this._checkBoxUseAppConfig.AutoSize = true;
            this._checkBoxUseAppConfig.Location = new System.Drawing.Point(322, 42);
            this._checkBoxUseAppConfig.Name = "_checkBoxUseAppConfig";
            this._checkBoxUseAppConfig.Size = new System.Drawing.Size(152, 17);
            this._checkBoxUseAppConfig.TabIndex = 3;
            this._checkBoxUseAppConfig.Text = "Use App.config file instead";
            this._checkBoxUseAppConfig.UseVisualStyleBackColor = true;
            this._checkBoxUseAppConfig.CheckedChanged += new System.EventHandler(this.CheckBoxUseAppConfigCheckedChanged);
            // 
            // _buttonSubscribe
            // 
            this._buttonSubscribe.Location = new System.Drawing.Point(14, 106);
            this._buttonSubscribe.Name = "_buttonSubscribe";
            this._buttonSubscribe.Size = new System.Drawing.Size(191, 30);
            this._buttonSubscribe.TabIndex = 4;
            this._buttonSubscribe.Text = "Subscribe to Monitoring Events";
            this._buttonSubscribe.UseVisualStyleBackColor = true;
            this._buttonSubscribe.Click += new System.EventHandler(this.ButtonSubscribeClick);
            // 
            // _buttonUnsubscribe
            // 
            this._buttonUnsubscribe.Location = new System.Drawing.Point(223, 106);
            this._buttonUnsubscribe.Name = "_buttonUnsubscribe";
            this._buttonUnsubscribe.Size = new System.Drawing.Size(191, 30);
            this._buttonUnsubscribe.TabIndex = 5;
            this._buttonUnsubscribe.Text = "Unsubscribe from Monitoring Events";
            this._buttonUnsubscribe.UseVisualStyleBackColor = true;
            this._buttonUnsubscribe.Click += new System.EventHandler(this.ButtonUnsubscribeClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Current Subcription Id:";
            // 
            // _labelCurrentSubscriptionId
            // 
            this._labelCurrentSubscriptionId.AutoSize = true;
            this._labelCurrentSubscriptionId.Location = new System.Drawing.Point(153, 146);
            this._labelCurrentSubscriptionId.Name = "_labelCurrentSubscriptionId";
            this._labelCurrentSubscriptionId.Size = new System.Drawing.Size(104, 13);
            this._labelCurrentSubscriptionId.TabIndex = 7;
            this._labelCurrentSubscriptionId.Text = "Not yet subscribed...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Port for Event Callback:";
            // 
            // _numericUpDownPort
            // 
            this._numericUpDownPort.Location = new System.Drawing.Point(172, 41);
            this._numericUpDownPort.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this._numericUpDownPort.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._numericUpDownPort.Name = "_numericUpDownPort";
            this._numericUpDownPort.Size = new System.Drawing.Size(120, 20);
            this._numericUpDownPort.TabIndex = 10;
            this._numericUpDownPort.Value = new decimal(new int[] {
            4444,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Current Callback Uri:";
            // 
            // _labelCallbackUri
            // 
            this._labelCallbackUri.AutoSize = true;
            this._labelCallbackUri.Location = new System.Drawing.Point(153, 170);
            this._labelCallbackUri.Name = "_labelCallbackUri";
            this._labelCallbackUri.Size = new System.Drawing.Size(85, 13);
            this._labelCallbackUri.TabIndex = 12;
            this._labelCallbackUri.Text = "Not yet started...";
            // 
            // _listViewMessages
            // 
            this._listViewMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._listViewMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnHeaderTime,
            this._columnHeaderMessage});
            this._listViewMessages.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._listViewMessages.FullRowSelect = true;
            this._listViewMessages.HideSelection = false;
            this._listViewMessages.Location = new System.Drawing.Point(-2, 423);
            this._listViewMessages.Name = "_listViewMessages";
            this._listViewMessages.Size = new System.Drawing.Size(731, 115);
            this._listViewMessages.TabIndex = 13;
            this._listViewMessages.UseCompatibleStateImageBehavior = false;
            this._listViewMessages.View = System.Windows.Forms.View.Details;
            // 
            // _columnHeaderTime
            // 
            this._columnHeaderTime.Text = "Time";
            this._columnHeaderTime.Width = 80;
            // 
            // _columnHeaderMessage
            // 
            this._columnHeaderMessage.Text = "Message";
            this._columnHeaderMessage.Width = 500;
            // 
            // _buttonInitialize
            // 
            this._buttonInitialize.Location = new System.Drawing.Point(14, 65);
            this._buttonInitialize.Name = "_buttonInitialize";
            this._buttonInitialize.Size = new System.Drawing.Size(129, 30);
            this._buttonInitialize.TabIndex = 14;
            this._buttonInitialize.Text = "Connect to core";
            this._buttonInitialize.UseVisualStyleBackColor = true;
            this._buttonInitialize.Click += new System.EventHandler(this.ButtonInitializeClick);
            // 
            // _checkBoxUnsubscribe
            // 
            this._checkBoxUnsubscribe.AutoSize = true;
            this._checkBoxUnsubscribe.Location = new System.Drawing.Point(322, 61);
            this._checkBoxUnsubscribe.Name = "_checkBoxUnsubscribe";
            this._checkBoxUnsubscribe.Size = new System.Drawing.Size(128, 17);
            this._checkBoxUnsubscribe.TabIndex = 15;
            this._checkBoxUnsubscribe.Text = "Unsubscribe on close";
            this._checkBoxUnsubscribe.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._cbUseClientAuthentication);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this._textBoxLineFullPath);
            this.groupBox1.Controls.Add(this._checkBoxFilterLine);
            this.groupBox1.Controls.Add(this._cbPortSharing);
            this.groupBox1.Controls.Add(this._buttonUnsubscribe);
            this.groupBox1.Controls.Add(this._textBoxCoreName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._checkBoxUseAppConfig);
            this.groupBox1.Controls.Add(this._checkBoxUnsubscribe);
            this.groupBox1.Controls.Add(this._buttonSubscribe);
            this.groupBox1.Controls.Add(this._buttonInitialize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this._labelCurrentSubscriptionId);
            this.groupBox1.Controls.Add(this._labelCallbackUri);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this._numericUpDownPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(677, 211);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maintenance Data Interface Events";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(322, 78);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 23;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(451, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Line Full Path:";
            // 
            // _textBoxLineFullPath
            // 
            this._textBoxLineFullPath.Enabled = false;
            this._textBoxLineFullPath.Location = new System.Drawing.Point(531, 120);
            this._textBoxLineFullPath.Name = "_textBoxLineFullPath";
            this._textBoxLineFullPath.Size = new System.Drawing.Size(114, 20);
            this._textBoxLineFullPath.TabIndex = 21;
            // 
            // _checkBoxFilterLine
            // 
            this._checkBoxFilterLine.AutoSize = true;
            this._checkBoxFilterLine.Location = new System.Drawing.Point(451, 100);
            this._checkBoxFilterLine.Name = "_checkBoxFilterLine";
            this._checkBoxFilterLine.Size = new System.Drawing.Size(211, 17);
            this._checkBoxFilterLine.TabIndex = 20;
            this._checkBoxFilterLine.Text = "Filter for Line: (example: Linie 1 AbC IR)";
            this._checkBoxFilterLine.UseVisualStyleBackColor = true;
            this._checkBoxFilterLine.CheckedChanged += new System.EventHandler(this._checkBoxFilterLine_CheckedChanged);
            // 
            // _cbPortSharing
            // 
            this._cbPortSharing.AutoSize = true;
            this._cbPortSharing.Location = new System.Drawing.Point(172, 67);
            this._cbPortSharing.Name = "_cbPortSharing";
            this._cbPortSharing.Size = new System.Drawing.Size(128, 17);
            this._cbPortSharing.TabIndex = 19;
            this._cbPortSharing.Text = "Use Tcp Port Sharing";
            this._cbPortSharing.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._buttonGetConfiguration);
            this.groupBox2.Controls.Add(this._listBoxEndpoints);
            this.groupBox2.Location = new System.Drawing.Point(13, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(676, 151);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Maintenance Data Interface Endpoints";
            // 
            // _buttonGetConfiguration
            // 
            this._buttonGetConfiguration.Enabled = false;
            this._buttonGetConfiguration.Location = new System.Drawing.Point(514, 30);
            this._buttonGetConfiguration.Name = "_buttonGetConfiguration";
            this._buttonGetConfiguration.Size = new System.Drawing.Size(139, 29);
            this._buttonGetConfiguration.TabIndex = 1;
            this._buttonGetConfiguration.Text = "Get All Maintenance Data";
            this._buttonGetConfiguration.UseVisualStyleBackColor = true;
            this._buttonGetConfiguration.Click += new System.EventHandler(this.ButtonGetAllMtcDataClick);
            // 
            // _listBoxEndpoints
            // 
            this._listBoxEndpoints.FormattingEnabled = true;
            this._listBoxEndpoints.Location = new System.Drawing.Point(13, 30);
            this._listBoxEndpoints.Name = "_listBoxEndpoints";
            this._listBoxEndpoints.Size = new System.Drawing.Size(483, 95);
            this._listBoxEndpoints.TabIndex = 0;
            this._listBoxEndpoints.SelectedIndexChanged += new System.EventHandler(this.ListBoxAdaptersSelectedIndexChanged);
            // 
            // _buttonCopyToClipboard
            // 
            this._buttonCopyToClipboard.Location = new System.Drawing.Point(26, 388);
            this._buttonCopyToClipboard.Name = "_buttonCopyToClipboard";
            this._buttonCopyToClipboard.Size = new System.Drawing.Size(191, 29);
            this._buttonCopyToClipboard.TabIndex = 2;
            this._buttonCopyToClipboard.Text = "Copy Messages to Clipboard";
            this._buttonCopyToClipboard.UseVisualStyleBackColor = true;
            this._buttonCopyToClipboard.Click += new System.EventHandler(this._buttonCopyToClipboard_Click);
            // 
            // _buttonClearMessages
            // 
            this._buttonClearMessages.Location = new System.Drawing.Point(235, 388);
            this._buttonClearMessages.Name = "_buttonClearMessages";
            this._buttonClearMessages.Size = new System.Drawing.Size(191, 29);
            this._buttonClearMessages.TabIndex = 21;
            this._buttonClearMessages.Text = "Clear Messages";
            this._buttonClearMessages.UseVisualStyleBackColor = true;
            this._buttonClearMessages.Click += new System.EventHandler(this._buttonClearMessages_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 536);
            this.Controls.Add(this._buttonClearMessages);
            this.Controls.Add(this._buttonCopyToClipboard);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._listViewMessages);
            this.Name = "MainForm";
            this.Text = "Maintenance Data Interface Tutorial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1Closing);
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownPort)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _textBoxCoreName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox _checkBoxUseAppConfig;
        private System.Windows.Forms.Button _buttonSubscribe;
        private System.Windows.Forms.Button _buttonUnsubscribe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _labelCurrentSubscriptionId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _numericUpDownPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _labelCallbackUri;
        private System.Windows.Forms.ListView _listViewMessages;
        private System.Windows.Forms.ColumnHeader _columnHeaderTime;
        private System.Windows.Forms.ColumnHeader _columnHeaderMessage;
        private System.Windows.Forms.Button _buttonInitialize;
        private System.Windows.Forms.CheckBox _checkBoxUnsubscribe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button _buttonGetConfiguration;
        private System.Windows.Forms.ListBox _listBoxEndpoints;
        private System.Windows.Forms.CheckBox _cbPortSharing;
        private System.Windows.Forms.Button _buttonCopyToClipboard;
        private System.Windows.Forms.Button _buttonClearMessages;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _textBoxLineFullPath;
        private System.Windows.Forms.CheckBox _checkBoxFilterLine;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

