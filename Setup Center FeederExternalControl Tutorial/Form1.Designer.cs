namespace FeederExternalControlTutorial
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.m_tabControlServices = new System.Windows.Forms.TabControl();
            this.m_tabPageMesService = new System.Windows.Forms.TabPage();
            this.labelReason = new System.Windows.Forms.Label();
            this.textBoxReason = new System.Windows.Forms.TextBox();
            this.labelResultStateToAdd = new System.Windows.Forms.Label();
            this.numericUpDownResultStateToAdd = new System.Windows.Forms.NumericUpDown();
            this.labelFeederIdToAdd = new System.Windows.Forms.Label();
            this.textBoxFeederIdToAdd = new System.Windows.Forms.TextBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridViewFeederResultStati = new System.Windows.Forms.DataGridView();
            this.textBoxExternalControlEndpoint = new System.Windows.Forms.TextBox();
            this.labelExternalControlEndpoint = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelOibServicelocatorEndpoint = new System.Windows.Forms.Label();
            this.textBoxCoreComputer = new System.Windows.Forms.TextBox();
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.contextMenuStripListBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ToolStripMenuItemClearMessages = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemShowLast = new System.Windows.Forms.ToolStripMenuItem();
            this.m_groupBoxOibServiceLocator = new System.Windows.Forms.GroupBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTcpPort = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.m_tabControlServices.SuspendLayout();
            this.m_tabPageMesService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResultStateToAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeederResultStati)).BeginInit();
            this.contextMenuStripListBox.SuspendLayout();
            this.m_groupBoxOibServiceLocator.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabControlServices
            // 
            this.m_tabControlServices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControlServices.Controls.Add(this.m_tabPageMesService);
            this.m_tabControlServices.Location = new System.Drawing.Point(14, 69);
            this.m_tabControlServices.Name = "m_tabControlServices";
            this.m_tabControlServices.SelectedIndex = 0;
            this.m_tabControlServices.Size = new System.Drawing.Size(672, 495);
            this.m_tabControlServices.TabIndex = 6;
            // 
            // m_tabPageMesService
            // 
            this.m_tabPageMesService.Controls.Add(this.labelReason);
            this.m_tabPageMesService.Controls.Add(this.textBoxReason);
            this.m_tabPageMesService.Controls.Add(this.labelResultStateToAdd);
            this.m_tabPageMesService.Controls.Add(this.numericUpDownResultStateToAdd);
            this.m_tabPageMesService.Controls.Add(this.labelFeederIdToAdd);
            this.m_tabPageMesService.Controls.Add(this.textBoxFeederIdToAdd);
            this.m_tabPageMesService.Controls.Add(this.buttonRemove);
            this.m_tabPageMesService.Controls.Add(this.buttonAdd);
            this.m_tabPageMesService.Controls.Add(this.dataGridViewFeederResultStati);
            this.m_tabPageMesService.Controls.Add(this.textBoxExternalControlEndpoint);
            this.m_tabPageMesService.Controls.Add(this.labelExternalControlEndpoint);
            this.m_tabPageMesService.Location = new System.Drawing.Point(4, 22);
            this.m_tabPageMesService.Name = "m_tabPageMesService";
            this.m_tabPageMesService.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabPageMesService.Size = new System.Drawing.Size(664, 469);
            this.m_tabPageMesService.TabIndex = 1;
            this.m_tabPageMesService.Text = "Feeder External Control Service";
            this.m_tabPageMesService.UseVisualStyleBackColor = true;
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.Location = new System.Drawing.Point(395, 137);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(44, 13);
            this.labelReason.TabIndex = 11;
            this.labelReason.Text = "Reason";
            // 
            // textBoxReason
            // 
            this.textBoxReason.Location = new System.Drawing.Point(395, 153);
            this.textBoxReason.Name = "textBoxReason";
            this.textBoxReason.Size = new System.Drawing.Size(128, 20);
            this.textBoxReason.TabIndex = 10;
            // 
            // labelResultStateToAdd
            // 
            this.labelResultStateToAdd.AutoSize = true;
            this.labelResultStateToAdd.Location = new System.Drawing.Point(395, 92);
            this.labelResultStateToAdd.Name = "labelResultStateToAdd";
            this.labelResultStateToAdd.Size = new System.Drawing.Size(65, 13);
            this.labelResultStateToAdd.TabIndex = 9;
            this.labelResultStateToAdd.Text = "Result State";
            // 
            // numericUpDownResultStateToAdd
            // 
            this.numericUpDownResultStateToAdd.Location = new System.Drawing.Point(395, 108);
            this.numericUpDownResultStateToAdd.Name = "numericUpDownResultStateToAdd";
            this.numericUpDownResultStateToAdd.Size = new System.Drawing.Size(125, 20);
            this.numericUpDownResultStateToAdd.TabIndex = 8;
            // 
            // labelFeederIdToAdd
            // 
            this.labelFeederIdToAdd.AutoSize = true;
            this.labelFeederIdToAdd.Location = new System.Drawing.Point(392, 48);
            this.labelFeederIdToAdd.Name = "labelFeederIdToAdd";
            this.labelFeederIdToAdd.Size = new System.Drawing.Size(46, 13);
            this.labelFeederIdToAdd.TabIndex = 7;
            this.labelFeederIdToAdd.Text = "Feeer Id";
            // 
            // textBoxFeederIdToAdd
            // 
            this.textBoxFeederIdToAdd.Location = new System.Drawing.Point(396, 65);
            this.textBoxFeederIdToAdd.Name = "textBoxFeederIdToAdd";
            this.textBoxFeederIdToAdd.Size = new System.Drawing.Size(128, 20);
            this.textBoxFeederIdToAdd.TabIndex = 6;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(395, 426);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(128, 23);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.OnButtonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(396, 180);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(128, 23);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.OnButtonAdd_Click);
            // 
            // dataGridViewFeederResultStati
            // 
            this.dataGridViewFeederResultStati.AllowUserToAddRows = false;
            this.dataGridViewFeederResultStati.AllowUserToDeleteRows = false;
            this.dataGridViewFeederResultStati.AllowUserToResizeRows = false;
            this.dataGridViewFeederResultStati.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFeederResultStati.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewFeederResultStati.Location = new System.Drawing.Point(12, 49);
            this.dataGridViewFeederResultStati.MultiSelect = false;
            this.dataGridViewFeederResultStati.Name = "dataGridViewFeederResultStati";
            this.dataGridViewFeederResultStati.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFeederResultStati.Size = new System.Drawing.Size(374, 402);
            this.dataGridViewFeederResultStati.TabIndex = 4;
            // 
            // textBoxExternalControlEndpoint
            // 
            this.textBoxExternalControlEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExternalControlEndpoint.Location = new System.Drawing.Point(67, 17);
            this.textBoxExternalControlEndpoint.Name = "textBoxExternalControlEndpoint";
            this.textBoxExternalControlEndpoint.ReadOnly = true;
            this.textBoxExternalControlEndpoint.Size = new System.Drawing.Size(589, 20);
            this.textBoxExternalControlEndpoint.TabIndex = 1;
            // 
            // labelExternalControlEndpoint
            // 
            this.labelExternalControlEndpoint.AutoSize = true;
            this.labelExternalControlEndpoint.Location = new System.Drawing.Point(9, 20);
            this.labelExternalControlEndpoint.Name = "labelExternalControlEndpoint";
            this.labelExternalControlEndpoint.Size = new System.Drawing.Size(52, 13);
            this.labelExternalControlEndpoint.TabIndex = 0;
            this.labelExternalControlEndpoint.Text = "Endpoint:";
            this.labelExternalControlEndpoint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOibServicelocatorEndpoint
            // 
            this.labelOibServicelocatorEndpoint.AutoSize = true;
            this.labelOibServicelocatorEndpoint.Location = new System.Drawing.Point(3, 22);
            this.labelOibServicelocatorEndpoint.Name = "labelOibServicelocatorEndpoint";
            this.labelOibServicelocatorEndpoint.Size = new System.Drawing.Size(83, 13);
            this.labelOibServicelocatorEndpoint.TabIndex = 0;
            this.labelOibServicelocatorEndpoint.Text = "ComputerName:";
            this.labelOibServicelocatorEndpoint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCoreComputer
            // 
            this.textBoxCoreComputer.Location = new System.Drawing.Point(90, 19);
            this.textBoxCoreComputer.Name = "textBoxCoreComputer";
            this.textBoxCoreComputer.Size = new System.Drawing.Size(160, 20);
            this.textBoxCoreComputer.TabIndex = 1;
            this.textBoxCoreComputer.Text = "localhost";
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMessages.ContextMenuStrip = this.contextMenuStripListBox;
            this.listBoxMessages.HorizontalExtent = 20000;
            this.listBoxMessages.HorizontalScrollbar = true;
            this.listBoxMessages.Location = new System.Drawing.Point(15, 565);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.Size = new System.Drawing.Size(664, 225);
            this.listBoxMessages.TabIndex = 7;
            this.listBoxMessages.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            this.listBoxMessages.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            // 
            // contextMenuStripListBox
            // 
            this.contextMenuStripListBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItemClearMessages,
            this.m_ToolStripMenuItemShowLast});
            this.contextMenuStripListBox.Name = "m_ContextMenuStripListBox";
            this.contextMenuStripListBox.Size = new System.Drawing.Size(163, 48);
            // 
            // m_ToolStripMenuItemClearMessages
            // 
            this.m_ToolStripMenuItemClearMessages.Name = "m_ToolStripMenuItemClearMessages";
            this.m_ToolStripMenuItemClearMessages.Size = new System.Drawing.Size(162, 22);
            this.m_ToolStripMenuItemClearMessages.Text = "Clear Messages";
            this.m_ToolStripMenuItemClearMessages.Click += new System.EventHandler(this.OnToolStripMenuItemClearMessages_Click);
            // 
            // m_ToolStripMenuItemShowLast
            // 
            this.m_ToolStripMenuItemShowLast.Checked = true;
            this.m_ToolStripMenuItemShowLast.CheckOnClick = true;
            this.m_ToolStripMenuItemShowLast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_ToolStripMenuItemShowLast.Name = "m_ToolStripMenuItemShowLast";
            this.m_ToolStripMenuItemShowLast.Size = new System.Drawing.Size(162, 22);
            this.m_ToolStripMenuItemShowLast.Text = "Automatic Scroll";
            // 
            // m_groupBoxOibServiceLocator
            // 
            this.m_groupBoxOibServiceLocator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_groupBoxOibServiceLocator.Controls.Add(this._cbUseClientAuthentication);
            this.m_groupBoxOibServiceLocator.Controls.Add(this.buttonStart);
            this.m_groupBoxOibServiceLocator.Controls.Add(this.label1);
            this.m_groupBoxOibServiceLocator.Controls.Add(this.textBoxTcpPort);
            this.m_groupBoxOibServiceLocator.Controls.Add(this.labelOibServicelocatorEndpoint);
            this.m_groupBoxOibServiceLocator.Controls.Add(this.textBoxCoreComputer);
            this.m_groupBoxOibServiceLocator.Location = new System.Drawing.Point(14, 14);
            this.m_groupBoxOibServiceLocator.Name = "m_groupBoxOibServiceLocator";
            this.m_groupBoxOibServiceLocator.Size = new System.Drawing.Size(672, 49);
            this.m_groupBoxOibServiceLocator.TabIndex = 5;
            this.m_groupBoxOibServiceLocator.TabStop = false;
            this.m_groupBoxOibServiceLocator.Text = "OIB Core Properties:";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(383, 17);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(138, 23);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start callback and register";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.OnButtonStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tcp Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTcpPort
            // 
            this.textBoxTcpPort.Location = new System.Drawing.Point(308, 19);
            this.textBoxTcpPort.Name = "textBoxTcpPort";
            this.textBoxTcpPort.Size = new System.Drawing.Size(69, 20);
            this.textBoxTcpPort.TabIndex = 3;
            this.textBoxTcpPort.Text = "1406";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(524, 21);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 5;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 804);
            this.Controls.Add(this.m_tabControlServices);
            this.Controls.Add(this.listBoxMessages);
            this.Controls.Add(this.m_groupBoxOibServiceLocator);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Setup Center Feeder External Control Turorial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.m_tabControlServices.ResumeLayout(false);
            this.m_tabPageMesService.ResumeLayout(false);
            this.m_tabPageMesService.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResultStateToAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeederResultStati)).EndInit();
            this.contextMenuStripListBox.ResumeLayout(false);
            this.m_groupBoxOibServiceLocator.ResumeLayout(false);
            this.m_groupBoxOibServiceLocator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl m_tabControlServices;
        private System.Windows.Forms.TabPage m_tabPageMesService;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.TextBox textBoxReason;
        private System.Windows.Forms.Label labelResultStateToAdd;
        private System.Windows.Forms.NumericUpDown numericUpDownResultStateToAdd;
        private System.Windows.Forms.Label labelFeederIdToAdd;
        private System.Windows.Forms.TextBox textBoxFeederIdToAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DataGridView dataGridViewFeederResultStati;
        private System.Windows.Forms.TextBox textBoxExternalControlEndpoint;
        private System.Windows.Forms.Label labelExternalControlEndpoint;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelOibServicelocatorEndpoint;
        private System.Windows.Forms.TextBox textBoxCoreComputer;
        private System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListBox;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemClearMessages;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemShowLast;
        private System.Windows.Forms.GroupBox m_groupBoxOibServiceLocator;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTcpPort;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

