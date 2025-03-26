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
            this._ButtonContinueLine = new System.Windows.Forms.Button();
            this._ButtonGetStatus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._TextBoxLineFullPath = new System.Windows.Forms.TextBox();
            this._ButtonStopLine = new System.Windows.Forms.Button();
            this._ListBox = new System.Windows.Forms.ListBox();
            this._DataGridViewProductionSchedule = new System.Windows.Forms.DataGridView();
            this._ButtonAddRecipesOfJob = new System.Windows.Forms.Button();
            this._ButtonStartRecipe = new System.Windows.Forms.Button();
            this._ButtonIntegrityCheck = new System.Windows.Forms.Button();
            this._TextBoxPSFullPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._ButtonCreateProductionSchedule = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this._rbSIPLACESubLine = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._TextBoxLineControlEndpointAddress = new System.Windows.Forms.TextBox();
            this.groupBoxStation = new System.Windows.Forms.GroupBox();
            this.buttonExternalStop = new System.Windows.Forms.Button();
            this.buttonUnblockConveyor = new System.Windows.Forms.Button();
            this.buttonBlockConveyor = new System.Windows.Forms.Button();
            this.buttonUnblockInputConveyor = new System.Windows.Forms.Button();
            this.buttonBlockInputConveyor = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxExtStopMsg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxConveyorLane = new System.Windows.Forms.TextBox();
            this.labelStationPath = new System.Windows.Forms.Label();
            this.textBoxStationPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._TextBoxSiplaceProEndpointAddress = new System.Windows.Forms.TextBox();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this._GroupBoxTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._DataGridViewProductionSchedule)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxStation.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 855);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _GroupBoxTest
            // 
            this._GroupBoxTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._GroupBoxTest.Controls.Add(this._ButtonContinueLine);
            this._GroupBoxTest.Controls.Add(this._ButtonGetStatus);
            this._GroupBoxTest.Controls.Add(this.label2);
            this._GroupBoxTest.Controls.Add(this._TextBoxLineFullPath);
            this._GroupBoxTest.Controls.Add(this._ButtonStopLine);
            this._GroupBoxTest.Location = new System.Drawing.Point(12, 99);
            this._GroupBoxTest.Name = "_GroupBoxTest";
            this._GroupBoxTest.Size = new System.Drawing.Size(859, 66);
            this._GroupBoxTest.TabIndex = 18;
            this._GroupBoxTest.TabStop = false;
            this._GroupBoxTest.Text = "Line";
            // 
            // _ButtonContinueLine
            // 
            this._ButtonContinueLine.Location = new System.Drawing.Point(447, 35);
            this._ButtonContinueLine.Name = "_ButtonContinueLine";
            this._ButtonContinueLine.Size = new System.Drawing.Size(189, 23);
            this._ButtonContinueLine.TabIndex = 49;
            this._ButtonContinueLine.Text = "Continue Line";
            this._ButtonContinueLine.UseVisualStyleBackColor = true;
            this._ButtonContinueLine.Click += new System.EventHandler(this.ButtonContinueLine_Click);
            // 
            // _ButtonGetStatus
            // 
            this._ButtonGetStatus.Location = new System.Drawing.Point(642, 35);
            this._ButtonGetStatus.Name = "_ButtonGetStatus";
            this._ButtonGetStatus.Size = new System.Drawing.Size(199, 23);
            this._ButtonGetStatus.TabIndex = 48;
            this._ButtonGetStatus.Text = "Get Line Status";
            this._ButtonGetStatus.UseVisualStyleBackColor = true;
            this._ButtonGetStatus.Click += new System.EventHandler(this.ButtonGetStatus_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Line Path";
            // 
            // _TextBoxLineFullPath
            // 
            this._TextBoxLineFullPath.Location = new System.Drawing.Point(17, 35);
            this._TextBoxLineFullPath.Name = "_TextBoxLineFullPath";
            this._TextBoxLineFullPath.Size = new System.Drawing.Size(223, 20);
            this._TextBoxLineFullPath.TabIndex = 43;
            this._TextBoxLineFullPath.Text = "OIB-SC-Tutorials\\X2";
            // 
            // _ButtonStopLine
            // 
            this._ButtonStopLine.Location = new System.Drawing.Point(246, 35);
            this._ButtonStopLine.Name = "_ButtonStopLine";
            this._ButtonStopLine.Size = new System.Drawing.Size(195, 23);
            this._ButtonStopLine.TabIndex = 47;
            this._ButtonStopLine.Text = "Stop Line";
            this._ButtonStopLine.UseVisualStyleBackColor = true;
            this._ButtonStopLine.Click += new System.EventHandler(this.ButtonStopLine_Click);
            // 
            // _ListBox
            // 
            this._ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ListBox.FormattingEnabled = true;
            this._ListBox.Location = new System.Drawing.Point(12, 514);
            this._ListBox.Name = "_ListBox";
            this._ListBox.Size = new System.Drawing.Size(859, 329);
            this._ListBox.TabIndex = 19;
            // 
            // _DataGridViewProductionSchedule
            // 
            this._DataGridViewProductionSchedule.AllowUserToAddRows = false;
            this._DataGridViewProductionSchedule.AllowUserToDeleteRows = false;
            this._DataGridViewProductionSchedule.AllowUserToResizeColumns = false;
            this._DataGridViewProductionSchedule.AllowUserToResizeRows = false;
            this._DataGridViewProductionSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._DataGridViewProductionSchedule.Location = new System.Drawing.Point(17, 82);
            this._DataGridViewProductionSchedule.MultiSelect = false;
            this._DataGridViewProductionSchedule.Name = "_DataGridViewProductionSchedule";
            this._DataGridViewProductionSchedule.ReadOnly = true;
            this._DataGridViewProductionSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._DataGridViewProductionSchedule.Size = new System.Drawing.Size(824, 128);
            this._DataGridViewProductionSchedule.TabIndex = 44;
            this._DataGridViewProductionSchedule.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGridViewProductionSchedule_MouseClick);
            // 
            // _ButtonAddRecipesOfJob
            // 
            this._ButtonAddRecipesOfJob.Location = new System.Drawing.Point(447, 43);
            this._ButtonAddRecipesOfJob.Name = "_ButtonAddRecipesOfJob";
            this._ButtonAddRecipesOfJob.Size = new System.Drawing.Size(189, 23);
            this._ButtonAddRecipesOfJob.TabIndex = 45;
            this._ButtonAddRecipesOfJob.Text = "Add Recipes of Job...";
            this._ButtonAddRecipesOfJob.UseVisualStyleBackColor = true;
            this._ButtonAddRecipesOfJob.Click += new System.EventHandler(this.ButtonAddRecipesOfJob_Click);
            // 
            // _ButtonStartRecipe
            // 
            this._ButtonStartRecipe.Location = new System.Drawing.Point(17, 216);
            this._ButtonStartRecipe.Name = "_ButtonStartRecipe";
            this._ButtonStartRecipe.Size = new System.Drawing.Size(188, 23);
            this._ButtonStartRecipe.TabIndex = 46;
            this._ButtonStartRecipe.Text = "Download (start) Recipe";
            this._ButtonStartRecipe.UseVisualStyleBackColor = true;
            this._ButtonStartRecipe.Click += new System.EventHandler(this.ButtonStartRecipe_Click);
            // 
            // _ButtonIntegrityCheck
            // 
            this._ButtonIntegrityCheck.Location = new System.Drawing.Point(653, 216);
            this._ButtonIntegrityCheck.Name = "_ButtonIntegrityCheck";
            this._ButtonIntegrityCheck.Size = new System.Drawing.Size(188, 23);
            this._ButtonIntegrityCheck.TabIndex = 48;
            this._ButtonIntegrityCheck.Text = "Integrity Check";
            this._ButtonIntegrityCheck.UseVisualStyleBackColor = true;
            this._ButtonIntegrityCheck.Click += new System.EventHandler(this.ButtonIntegrityCheck_Click);
            // 
            // _TextBoxPSFullPath
            // 
            this._TextBoxPSFullPath.Location = new System.Drawing.Point(17, 45);
            this._TextBoxPSFullPath.Name = "_TextBoxPSFullPath";
            this._TextBoxPSFullPath.Size = new System.Drawing.Size(223, 20);
            this._TextBoxPSFullPath.TabIndex = 49;
            this._TextBoxPSFullPath.Text = "OIB-SC-Tutorials\\ProductionPlan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Production Schedule Name";
            // 
            // _ButtonCreateProductionSchedule
            // 
            this._ButtonCreateProductionSchedule.Location = new System.Drawing.Point(246, 43);
            this._ButtonCreateProductionSchedule.Name = "_ButtonCreateProductionSchedule";
            this._ButtonCreateProductionSchedule.Size = new System.Drawing.Size(195, 23);
            this._ButtonCreateProductionSchedule.TabIndex = 51;
            this._ButtonCreateProductionSchedule.Text = "Get or Create Production Schedule";
            this._ButtonCreateProductionSchedule.UseVisualStyleBackColor = true;
            this._ButtonCreateProductionSchedule.Click += new System.EventHandler(this.ButtonCreateProductionSchedule_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this._rbSIPLACESubLine);
            this.groupBox1.Controls.Add(this._ButtonCreateProductionSchedule);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._TextBoxPSFullPath);
            this.groupBox1.Controls.Add(this._ButtonIntegrityCheck);
            this.groupBox1.Controls.Add(this._ButtonStartRecipe);
            this.groupBox1.Controls.Add(this._ButtonAddRecipesOfJob);
            this.groupBox1.Controls.Add(this._DataGridViewProductionSchedule);
            this.groupBox1.Location = new System.Drawing.Point(13, 263);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(859, 245);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Production Schedule";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(328, 219);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 53;
            this.radioButton2.Text = "DEK Printers";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // _rbSIPLACESubLine
            // 
            this._rbSIPLACESubLine.AutoSize = true;
            this._rbSIPLACESubLine.Checked = true;
            this._rbSIPLACESubLine.Location = new System.Drawing.Point(211, 219);
            this._rbSIPLACESubLine.Name = "_rbSIPLACESubLine";
            this._rbSIPLACESubLine.Size = new System.Drawing.Size(110, 17);
            this._rbSIPLACESubLine.TabIndex = 52;
            this._rbSIPLACESubLine.TabStop = true;
            this._rbSIPLACESubLine.Text = "SIPLACE Stations";
            this._rbSIPLACESubLine.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._TextBoxLineControlEndpointAddress);
            this.groupBox2.Location = new System.Drawing.Point(12, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 60);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line Control Endpoint Address";
            // 
            // _TextBoxLineControlEndpointAddress
            // 
            this._TextBoxLineControlEndpointAddress.Location = new System.Drawing.Point(18, 24);
            this._TextBoxLineControlEndpointAddress.Name = "_TextBoxLineControlEndpointAddress";
            this._TextBoxLineControlEndpointAddress.Size = new System.Drawing.Size(385, 20);
            this._TextBoxLineControlEndpointAddress.TabIndex = 43;
            this._TextBoxLineControlEndpointAddress.Text = "net.tcp://localhost:1406/Asm.As.Oib.SiplacePro.LineControl/ReliableSecure";
            // 
            // groupBoxStation
            // 
            this.groupBoxStation.Controls.Add(this.buttonExternalStop);
            this.groupBoxStation.Controls.Add(this.buttonUnblockConveyor);
            this.groupBoxStation.Controls.Add(this.buttonBlockConveyor);
            this.groupBoxStation.Controls.Add(this.buttonUnblockInputConveyor);
            this.groupBoxStation.Controls.Add(this.buttonBlockInputConveyor);
            this.groupBoxStation.Controls.Add(this.label5);
            this.groupBoxStation.Controls.Add(this.textBoxExtStopMsg);
            this.groupBoxStation.Controls.Add(this.label4);
            this.groupBoxStation.Controls.Add(this.textBoxConveyorLane);
            this.groupBoxStation.Controls.Add(this.labelStationPath);
            this.groupBoxStation.Controls.Add(this.textBoxStationPath);
            this.groupBoxStation.Location = new System.Drawing.Point(13, 171);
            this.groupBoxStation.Name = "groupBoxStation";
            this.groupBoxStation.Size = new System.Drawing.Size(858, 75);
            this.groupBoxStation.TabIndex = 48;
            this.groupBoxStation.TabStop = false;
            this.groupBoxStation.Text = "Station";
            // 
            // buttonExternalStop
            // 
            this.buttonExternalStop.Location = new System.Drawing.Point(746, 11);
            this.buttonExternalStop.Name = "buttonExternalStop";
            this.buttonExternalStop.Size = new System.Drawing.Size(104, 23);
            this.buttonExternalStop.TabIndex = 10;
            this.buttonExternalStop.Text = "External Stop";
            this.buttonExternalStop.UseVisualStyleBackColor = true;
            this.buttonExternalStop.Click += new System.EventHandler(this.buttonExternalStop_Click);
            // 
            // buttonUnblockConveyor
            // 
            this.buttonUnblockConveyor.Location = new System.Drawing.Point(596, 40);
            this.buttonUnblockConveyor.Name = "buttonUnblockConveyor";
            this.buttonUnblockConveyor.Size = new System.Drawing.Size(137, 23);
            this.buttonUnblockConveyor.TabIndex = 9;
            this.buttonUnblockConveyor.Text = "Unblock Conveyor";
            this.buttonUnblockConveyor.UseVisualStyleBackColor = true;
            this.buttonUnblockConveyor.Click += new System.EventHandler(this.buttonUnblockConveyor_Click);
            // 
            // buttonBlockConveyor
            // 
            this.buttonBlockConveyor.Location = new System.Drawing.Point(596, 11);
            this.buttonBlockConveyor.Name = "buttonBlockConveyor";
            this.buttonBlockConveyor.Size = new System.Drawing.Size(137, 23);
            this.buttonBlockConveyor.TabIndex = 8;
            this.buttonBlockConveyor.Text = "Block Conveyor";
            this.buttonBlockConveyor.UseVisualStyleBackColor = true;
            this.buttonBlockConveyor.Click += new System.EventHandler(this.ButtonBlockConveyor_Click);
            // 
            // buttonUnblockInputConveyor
            // 
            this.buttonUnblockInputConveyor.Location = new System.Drawing.Point(447, 40);
            this.buttonUnblockInputConveyor.Name = "buttonUnblockInputConveyor";
            this.buttonUnblockInputConveyor.Size = new System.Drawing.Size(137, 23);
            this.buttonUnblockInputConveyor.TabIndex = 7;
            this.buttonUnblockInputConveyor.Text = "Unblock Input Conveyor";
            this.buttonUnblockInputConveyor.UseVisualStyleBackColor = true;
            this.buttonUnblockInputConveyor.Click += new System.EventHandler(this.ButtonUnblockInputConveyor_Click);
            // 
            // buttonBlockInputConveyor
            // 
            this.buttonBlockInputConveyor.Location = new System.Drawing.Point(447, 11);
            this.buttonBlockInputConveyor.Name = "buttonBlockInputConveyor";
            this.buttonBlockInputConveyor.Size = new System.Drawing.Size(137, 23);
            this.buttonBlockInputConveyor.TabIndex = 6;
            this.buttonBlockInputConveyor.Text = "Block Input Conveyor";
            this.buttonBlockInputConveyor.UseVisualStyleBackColor = true;
            this.buttonBlockInputConveyor.Click += new System.EventHandler(this.ButtonBlockInputConveyor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(328, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ext. Stop Message";
            // 
            // textBoxExtStopMsg
            // 
            this.textBoxExtStopMsg.Location = new System.Drawing.Point(328, 33);
            this.textBoxExtStopMsg.Name = "textBoxExtStopMsg";
            this.textBoxExtStopMsg.Size = new System.Drawing.Size(100, 20);
            this.textBoxExtStopMsg.TabIndex = 4;
            this.textBoxExtStopMsg.Text = "OIB Tutorial";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(243, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Conveyor Lane";
            // 
            // textBoxConveyorLane
            // 
            this.textBoxConveyorLane.Location = new System.Drawing.Point(246, 33);
            this.textBoxConveyorLane.Name = "textBoxConveyorLane";
            this.textBoxConveyorLane.Size = new System.Drawing.Size(76, 20);
            this.textBoxConveyorLane.TabIndex = 2;
            this.textBoxConveyorLane.Text = "both";
            // 
            // labelStationPath
            // 
            this.labelStationPath.AutoSize = true;
            this.labelStationPath.Location = new System.Drawing.Point(14, 16);
            this.labelStationPath.Name = "labelStationPath";
            this.labelStationPath.Size = new System.Drawing.Size(65, 13);
            this.labelStationPath.TabIndex = 1;
            this.labelStationPath.Text = "Station Path";
            // 
            // textBoxStationPath
            // 
            this.textBoxStationPath.Location = new System.Drawing.Point(17, 33);
            this.textBoxStationPath.Name = "textBoxStationPath";
            this.textBoxStationPath.Size = new System.Drawing.Size(222, 20);
            this.textBoxStationPath.TabIndex = 0;
            this.textBoxStationPath.Text = "OIB-SC-Tutorials\\X2";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this._TextBoxSiplaceProEndpointAddress);
            this.groupBox3.Location = new System.Drawing.Point(451, 33);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(420, 60);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SIPLACE Pro Endpoint Address";
            // 
            // _TextBoxSiplaceProEndpointAddress
            // 
            this._TextBoxSiplaceProEndpointAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TextBoxSiplaceProEndpointAddress.Location = new System.Drawing.Point(18, 24);
            this._TextBoxSiplaceProEndpointAddress.Name = "_TextBoxSiplaceProEndpointAddress";
            this._TextBoxSiplaceProEndpointAddress.Size = new System.Drawing.Size(385, 20);
            this._TextBoxSiplaceProEndpointAddress.TabIndex = 43;
            this._TextBoxSiplaceProEndpointAddress.Text = "net.tcp://localhost:9500/Asm.As.Oib.SiplacePro/Secure";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(13, 10);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 50;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 877);
            this.Controls.Add(this._cbUseClientAuthentication);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxStation);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._ListBox);
            this.Controls.Add(this._GroupBoxTest);
            this.Controls.Add(this.statusStrip1);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OIB Tutorial";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this._GroupBoxTest.ResumeLayout(false);
            this._GroupBoxTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._DataGridViewProductionSchedule)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxStation.ResumeLayout(false);
            this.groupBoxStation.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.GroupBox _GroupBoxTest;
        private System.Windows.Forms.ListBox _ListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _TextBoxLineFullPath;
        private System.Windows.Forms.Button _ButtonStopLine;
        private System.Windows.Forms.Button _ButtonGetStatus;
        private System.Windows.Forms.Button _ButtonContinueLine;
        private System.Windows.Forms.DataGridView _DataGridViewProductionSchedule;
        private System.Windows.Forms.Button _ButtonAddRecipesOfJob;
        private System.Windows.Forms.Button _ButtonStartRecipe;
        private System.Windows.Forms.Button _ButtonIntegrityCheck;
        private System.Windows.Forms.TextBox _TextBoxPSFullPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _ButtonCreateProductionSchedule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _TextBoxLineControlEndpointAddress;
        private System.Windows.Forms.GroupBox groupBoxStation;
        private System.Windows.Forms.Button buttonExternalStop;
        private System.Windows.Forms.Button buttonUnblockConveyor;
        private System.Windows.Forms.Button buttonBlockConveyor;
        private System.Windows.Forms.Button buttonUnblockInputConveyor;
        private System.Windows.Forms.Button buttonBlockInputConveyor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxExtStopMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxConveyorLane;
        private System.Windows.Forms.Label labelStationPath;
        private System.Windows.Forms.TextBox textBoxStationPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox _TextBoxSiplaceProEndpointAddress;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton _rbSIPLACESubLine;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

