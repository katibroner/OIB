
using Asm.As.Controls.WinForms.CalendarControl.Controls;

namespace FcSmartProxyDemo
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pnlConfigAnsCmds = new System.Windows.Forms.Panel();
            this.btnCreateTestData = new System.Windows.Forms.Button();
            this.cbSubscribeUpdates = new System.Windows.Forms.CheckBox();
            this.cbFactoryElement = new System.Windows.Forms.ComboBox();
            this.lblFactoryElement = new System.Windows.Forms.Label();
            this.cbIncludeInheritance = new System.Windows.Forms.CheckBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.btnLoadAppointments = new System.Windows.Forms.Button();
            this.btnDeleteAppointment = new System.Windows.Forms.Button();
            this.btnEditAppointment = new System.Windows.Forms.Button();
            this.btnNewAppointment = new System.Windows.Forms.Button();
            this.dtLoadTo = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtLoadFrom = new System.Windows.Forms.DateTimePicker();
            this.btnLoadCalendarItems = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAppointments = new System.Windows.Forms.TabPage();
            this.dataGridAppointments = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabCalendarItems = new System.Windows.Forms.TabPage();
            this.dataGridCalendarItems = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabCalendar = new System.Windows.Forms.TabPage();
            this.calendarMonthControl = new CalendarMonthControl();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblConnectionState = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCleanup = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.pnlConfigAnsCmds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAppointments)).BeginInit();
            this.tabCalendarItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCalendarItems)).BeginInit();
            this.tabCalendar.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.pnlConfigAnsCmds, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.splitContainer, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.statusBar, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(956, 728);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // pnlConfigAnsCmds
            // 
            this.pnlConfigAnsCmds.AutoScroll = true;
            this.pnlConfigAnsCmds.AutoSize = true;
            this.pnlConfigAnsCmds.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlConfigAnsCmds.BackColor = System.Drawing.SystemColors.Window;
            this.pnlConfigAnsCmds.Controls.Add(this.btnCleanup);
            this.pnlConfigAnsCmds.Controls.Add(this.btnCreateTestData);
            this.pnlConfigAnsCmds.Controls.Add(this.cbSubscribeUpdates);
            this.pnlConfigAnsCmds.Controls.Add(this.cbFactoryElement);
            this.pnlConfigAnsCmds.Controls.Add(this.lblFactoryElement);
            this.pnlConfigAnsCmds.Controls.Add(this.cbIncludeInheritance);
            this.pnlConfigAnsCmds.Controls.Add(this.lblFrom);
            this.pnlConfigAnsCmds.Controls.Add(this.btnLoadAppointments);
            this.pnlConfigAnsCmds.Controls.Add(this.btnDeleteAppointment);
            this.pnlConfigAnsCmds.Controls.Add(this.btnEditAppointment);
            this.pnlConfigAnsCmds.Controls.Add(this.btnNewAppointment);
            this.pnlConfigAnsCmds.Controls.Add(this.dtLoadTo);
            this.pnlConfigAnsCmds.Controls.Add(this.lblTo);
            this.pnlConfigAnsCmds.Controls.Add(this.dtLoadFrom);
            this.pnlConfigAnsCmds.Controls.Add(this.btnLoadCalendarItems);
            this.pnlConfigAnsCmds.Controls.Add(this.btnConnect);
            this.pnlConfigAnsCmds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConfigAnsCmds.Location = new System.Drawing.Point(3, 3);
            this.pnlConfigAnsCmds.Name = "pnlConfigAnsCmds";
            this.pnlConfigAnsCmds.Size = new System.Drawing.Size(950, 144);
            this.pnlConfigAnsCmds.TabIndex = 5;
            // 
            // btnCreateTestData
            // 
            this.btnCreateTestData.Enabled = false;
            this.btnCreateTestData.Location = new System.Drawing.Point(9, 46);
            this.btnCreateTestData.Name = "btnCreateTestData";
            this.btnCreateTestData.Size = new System.Drawing.Size(128, 31);
            this.btnCreateTestData.TabIndex = 37;
            this.btnCreateTestData.Text = "Create Data";
            this.btnCreateTestData.UseVisualStyleBackColor = true;
            this.btnCreateTestData.Click += new System.EventHandler(this.btnCreateTestData_Click);
            // 
            // cbSubscribeUpdates
            // 
            this.cbSubscribeUpdates.AutoSize = true;
            this.cbSubscribeUpdates.Enabled = false;
            this.cbSubscribeUpdates.Location = new System.Drawing.Point(165, 89);
            this.cbSubscribeUpdates.Name = "cbSubscribeUpdates";
            this.cbSubscribeUpdates.Size = new System.Drawing.Size(160, 20);
            this.cbSubscribeUpdates.TabIndex = 36;
            this.cbSubscribeUpdates.Text = "Subscribe for updates";
            this.cbSubscribeUpdates.UseVisualStyleBackColor = true;
            // 
            // cbFactoryElement
            // 
            this.cbFactoryElement.Enabled = false;
            this.cbFactoryElement.FormattingEnabled = true;
            this.cbFactoryElement.Items.AddRange(new object[] {
            "Undefined"});
            this.cbFactoryElement.Location = new System.Drawing.Point(455, 88);
            this.cbFactoryElement.Name = "cbFactoryElement";
            this.cbFactoryElement.Size = new System.Drawing.Size(285, 24);
            this.cbFactoryElement.TabIndex = 35;
            this.cbFactoryElement.Text = "Undefined";
            this.cbFactoryElement.SelectedIndexChanged += new System.EventHandler(this.cbFactoryElement_SelectedIndexChanged);
            // 
            // lblFactoryElement
            // 
            this.lblFactoryElement.AutoSize = true;
            this.lblFactoryElement.Location = new System.Drawing.Point(343, 92);
            this.lblFactoryElement.Name = "lblFactoryElement";
            this.lblFactoryElement.Size = new System.Drawing.Size(106, 16);
            this.lblFactoryElement.TabIndex = 34;
            this.lblFactoryElement.Text = "Factory element:";
            // 
            // cbIncludeInheritance
            // 
            this.cbIncludeInheritance.AutoSize = true;
            this.cbIncludeInheritance.Enabled = false;
            this.cbIncludeInheritance.Location = new System.Drawing.Point(455, 118);
            this.cbIncludeInheritance.Name = "cbIncludeInheritance";
            this.cbIncludeInheritance.Size = new System.Drawing.Size(140, 20);
            this.cbIncludeInheritance.TabIndex = 18;
            this.cbIncludeInheritance.Text = "Include Inheritance";
            this.cbIncludeInheritance.UseVisualStyleBackColor = true;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(343, 54);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(41, 16);
            this.lblFrom.TabIndex = 10;
            this.lblFrom.Text = "From:";
            // 
            // btnLoadAppointments
            // 
            this.btnLoadAppointments.Enabled = false;
            this.btnLoadAppointments.Location = new System.Drawing.Point(165, 9);
            this.btnLoadAppointments.Name = "btnLoadAppointments";
            this.btnLoadAppointments.Size = new System.Drawing.Size(165, 31);
            this.btnLoadAppointments.TabIndex = 17;
            this.btnLoadAppointments.Text = "Load Appointments";
            this.btnLoadAppointments.UseVisualStyleBackColor = true;
            this.btnLoadAppointments.Click += new System.EventHandler(this.btnLoadAppointments_Click);
            // 
            // btnDeleteAppointment
            // 
            this.btnDeleteAppointment.AutoSize = true;
            this.btnDeleteAppointment.Enabled = false;
            this.btnDeleteAppointment.Location = new System.Drawing.Point(668, 9);
            this.btnDeleteAppointment.Name = "btnDeleteAppointment";
            this.btnDeleteAppointment.Size = new System.Drawing.Size(155, 30);
            this.btnDeleteAppointment.TabIndex = 16;
            this.btnDeleteAppointment.Text = "Delete appointment";
            this.btnDeleteAppointment.UseVisualStyleBackColor = true;
            this.btnDeleteAppointment.Click += new System.EventHandler(this.btnDeleteAppointment_Click);
            // 
            // btnEditAppointment
            // 
            this.btnEditAppointment.Enabled = false;
            this.btnEditAppointment.Location = new System.Drawing.Point(507, 9);
            this.btnEditAppointment.Name = "btnEditAppointment";
            this.btnEditAppointment.Size = new System.Drawing.Size(155, 31);
            this.btnEditAppointment.TabIndex = 15;
            this.btnEditAppointment.Text = "Edit appointment";
            this.btnEditAppointment.UseVisualStyleBackColor = true;
            this.btnEditAppointment.Click += new System.EventHandler(this.btnEditAppointment_Click);
            // 
            // btnNewAppointment
            // 
            this.btnNewAppointment.Enabled = false;
            this.btnNewAppointment.Location = new System.Drawing.Point(346, 9);
            this.btnNewAppointment.Name = "btnNewAppointment";
            this.btnNewAppointment.Size = new System.Drawing.Size(155, 31);
            this.btnNewAppointment.TabIndex = 14;
            this.btnNewAppointment.Text = "New appointment";
            this.btnNewAppointment.UseVisualStyleBackColor = true;
            this.btnNewAppointment.Click += new System.EventHandler(this.btnNewAppointment_Click);
            // 
            // dtLoadTo
            // 
            this.dtLoadTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtLoadTo.Location = new System.Drawing.Point(591, 51);
            this.dtLoadTo.Name = "dtLoadTo";
            this.dtLoadTo.Size = new System.Drawing.Size(149, 22);
            this.dtLoadTo.TabIndex = 13;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(558, 54);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 16);
            this.lblTo.TabIndex = 12;
            this.lblTo.Text = "To:";
            // 
            // dtLoadFrom
            // 
            this.dtLoadFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtLoadFrom.Location = new System.Drawing.Point(390, 51);
            this.dtLoadFrom.Name = "dtLoadFrom";
            this.dtLoadFrom.Size = new System.Drawing.Size(149, 22);
            this.dtLoadFrom.TabIndex = 11;
            // 
            // btnLoadCalendarItems
            // 
            this.btnLoadCalendarItems.Enabled = false;
            this.btnLoadCalendarItems.Location = new System.Drawing.Point(165, 46);
            this.btnLoadCalendarItems.Name = "btnLoadCalendarItems";
            this.btnLoadCalendarItems.Size = new System.Drawing.Size(165, 31);
            this.btnLoadCalendarItems.TabIndex = 9;
            this.btnLoadCalendarItems.Text = "Load Calendar Items";
            this.btnLoadCalendarItems.UseVisualStyleBackColor = true;
            this.btnLoadCalendarItems.Click += new System.EventHandler(this.btnLoadCalendarItems_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(9, 9);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(128, 31);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 153);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.textOutput);
            this.splitContainer.Size = new System.Drawing.Size(950, 546);
            this.splitContainer.SplitterDistance = 390;
            this.splitContainer.TabIndex = 18;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabAppointments);
            this.tabControl.Controls.Add(this.tabCalendarItems);
            this.tabControl.Controls.Add(this.tabCalendar);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(946, 386);
            this.tabControl.TabIndex = 3;
            // 
            // tabAppointments
            // 
            this.tabAppointments.Controls.Add(this.dataGridAppointments);
            this.tabAppointments.Location = new System.Drawing.Point(4, 25);
            this.tabAppointments.Name = "tabAppointments";
            this.tabAppointments.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppointments.Size = new System.Drawing.Size(938, 357);
            this.tabAppointments.TabIndex = 2;
            this.tabAppointments.Text = "Appointments";
            this.tabAppointments.UseVisualStyleBackColor = true;
            // 
            // dataGridAppointments
            // 
            this.dataGridAppointments.AllowUserToAddRows = false;
            this.dataGridAppointments.AllowUserToDeleteRows = false;
            this.dataGridAppointments.AllowUserToOrderColumns = true;
            this.dataGridAppointments.AllowUserToResizeRows = false;
            this.dataGridAppointments.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAppointments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewCheckBoxColumn1,
            this.Column10,
            this.Column11});
            this.dataGridAppointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridAppointments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridAppointments.Location = new System.Drawing.Point(3, 3);
            this.dataGridAppointments.MultiSelect = false;
            this.dataGridAppointments.Name = "dataGridAppointments";
            this.dataGridAppointments.ReadOnly = true;
            this.dataGridAppointments.RowHeadersWidth = 25;
            this.dataGridAppointments.RowTemplate.Height = 24;
            this.dataGridAppointments.Size = new System.Drawing.Size(932, 351);
            this.dataGridAppointments.TabIndex = 1;
            this.dataGridAppointments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridAppointments_CellDoubleClick);
            this.dataGridAppointments.SelectionChanged += new System.EventHandler(this.dataGridAppointments_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Isa95Id";
            this.dataGridViewTextBoxColumn2.HeaderText = "Factory element Id";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn3.HeaderText = "Id";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 48;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn4.HeaderText = "Name";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 74;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn5.HeaderText = "Description";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "AppointmentType";
            this.dataGridViewTextBoxColumn6.HeaderText = "Type";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 69;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "StartTime";
            this.dataGridViewTextBoxColumn7.HeaderText = "Start";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 125;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "EndTime";
            this.dataGridViewTextBoxColumn8.HeaderText = "End";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 125;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Recurring";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Recurring";
            this.dataGridViewCheckBoxColumn1.MinimumWidth = 6;
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.Width = 94;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "CustomState";
            this.Column10.HeaderText = "CustomState";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 125;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "GroupReason";
            this.Column11.HeaderText = "GroupReason";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 125;
            // 
            // tabCalendarItems
            // 
            this.tabCalendarItems.Controls.Add(this.dataGridCalendarItems);
            this.tabCalendarItems.Location = new System.Drawing.Point(4, 25);
            this.tabCalendarItems.Name = "tabCalendarItems";
            this.tabCalendarItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabCalendarItems.Size = new System.Drawing.Size(938, 357);
            this.tabCalendarItems.TabIndex = 1;
            this.tabCalendarItems.Text = "Calendar Items";
            this.tabCalendarItems.UseVisualStyleBackColor = true;
            // 
            // dataGridCalendarItems
            // 
            this.dataGridCalendarItems.AllowUserToAddRows = false;
            this.dataGridCalendarItems.AllowUserToDeleteRows = false;
            this.dataGridCalendarItems.AllowUserToOrderColumns = true;
            this.dataGridCalendarItems.AllowUserToResizeRows = false;
            this.dataGridCalendarItems.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridCalendarItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCalendarItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9,
            this.Column1,
            this.Column2,
            this.DescriptionColumn,
            this.Column3,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridCalendarItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridCalendarItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridCalendarItems.Location = new System.Drawing.Point(3, 3);
            this.dataGridCalendarItems.MultiSelect = false;
            this.dataGridCalendarItems.Name = "dataGridCalendarItems";
            this.dataGridCalendarItems.ReadOnly = true;
            this.dataGridCalendarItems.RowHeadersWidth = 25;
            this.dataGridCalendarItems.RowTemplate.Height = 24;
            this.dataGridCalendarItems.Size = new System.Drawing.Size(932, 351);
            this.dataGridCalendarItems.TabIndex = 0;
            this.dataGridCalendarItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridCalendarItems_CellDoubleClick);
            this.dataGridCalendarItems.SelectionChanged += new System.EventHandler(this.dataGridCalendarItems_SelectionChanged);
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Path";
            this.Column8.HeaderText = "Factory element";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 125;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Isa95Id";
            this.Column9.HeaderText = "Factory element Id";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 125;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Id";
            this.Column1.HeaderText = "Id";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 48;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Name";
            this.Column2.HeaderText = "Name";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 74;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.DataPropertyName = "Description";
            this.DescriptionColumn.HeaderText = "Description";
            this.DescriptionColumn.MinimumWidth = 6;
            this.DescriptionColumn.Name = "DescriptionColumn";
            this.DescriptionColumn.ReadOnly = true;
            this.DescriptionColumn.Width = 125;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "AppointmentType";
            this.Column3.HeaderText = "Type";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 69;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "StartTime";
            this.Column5.HeaderText = "Start";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "EndTime";
            this.Column6.HeaderText = "End";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 125;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column7.DataPropertyName = "Recurring";
            this.Column7.HeaderText = "Recurring";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column7.Width = 94;
            // 
            // tabCalendar
            // 
            this.tabCalendar.AutoScroll = true;
            this.tabCalendar.Controls.Add(this.calendarMonthControl);
            this.tabCalendar.Location = new System.Drawing.Point(4, 25);
            this.tabCalendar.Name = "tabCalendar";
            this.tabCalendar.Padding = new System.Windows.Forms.Padding(3);
            this.tabCalendar.Size = new System.Drawing.Size(938, 357);
            this.tabCalendar.TabIndex = 0;
            this.tabCalendar.Text = "Calendar";
            this.tabCalendar.UseVisualStyleBackColor = true;
            // 
            // calendarMonthControl
            // 
            this.calendarMonthControl.AreMonthButtonsVisible = false;
            this.calendarMonthControl.AutoSize = true;
            this.calendarMonthControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.calendarMonthControl.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.calendarMonthControl.Culture = new System.Globalization.CultureInfo("");
            this.calendarMonthControl.CurrentDate = new System.DateTime(2022, 4, 4, 14, 13, 31, 981);
            this.calendarMonthControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.calendarMonthControl.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.calendarMonthControl.Location = new System.Drawing.Point(3, 3);
            this.calendarMonthControl.Name = "calendarMonthControl";
            this.calendarMonthControl.Size = new System.Drawing.Size(932, 336);
            this.calendarMonthControl.TabIndex = 0;
            this.calendarMonthControl.UpdateOnInit = false;
            // 
            // textOutput
            // 
            this.textOutput.AcceptsReturn = true;
            this.textOutput.AcceptsTab = true;
            this.textOutput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textOutput.Location = new System.Drawing.Point(0, 0);
            this.textOutput.Multiline = true;
            this.textOutput.Name = "textOutput";
            this.textOutput.ReadOnly = true;
            this.textOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textOutput.Size = new System.Drawing.Size(946, 148);
            this.textOutput.TabIndex = 1;
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblConnectionState});
            this.statusBar.Location = new System.Drawing.Point(0, 702);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(956, 26);
            this.statusBar.TabIndex = 6;
            // 
            // lblConnectionState
            // 
            this.lblConnectionState.Name = "lblConnectionState";
            this.lblConnectionState.Size = new System.Drawing.Size(104, 20);
            this.lblConnectionState.Text = "not connected";
            // 
            // btnCleanup
            // 
            this.btnCleanup.Enabled = false;
            this.btnCleanup.Location = new System.Drawing.Point(9, 84);
            this.btnCleanup.Name = "btnCleanup";
            this.btnCleanup.Size = new System.Drawing.Size(128, 31);
            this.btnCleanup.TabIndex = 38;
            this.btnCleanup.Text = "Cleanup";
            this.btnCleanup.UseVisualStyleBackColor = true;
            this.btnCleanup.Click += new System.EventHandler(this.btnCleanup_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 728);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainForm";
            this.Text = "OIB FactoryCalendar Smart Proxy Demo";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.pnlConfigAnsCmds.ResumeLayout(false);
            this.pnlConfigAnsCmds.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabAppointments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAppointments)).EndInit();
            this.tabCalendarItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCalendarItems)).EndInit();
            this.tabCalendar.ResumeLayout(false);
            this.tabCalendar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private Asm.As.Controls.WinForms.CalendarControl.Controls.CalendarMonthControl calendarMonthControl;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.Panel pnlConfigAnsCmds;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblConnectionState;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCalendar;
        private System.Windows.Forms.TabPage tabCalendarItems;
        private System.Windows.Forms.Button btnLoadCalendarItems;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtLoadTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtLoadFrom;
        private System.Windows.Forms.DataGridView dataGridCalendarItems;
        private System.Windows.Forms.Button btnNewAppointment;
        private System.Windows.Forms.Button btnEditAppointment;
        private System.Windows.Forms.Button btnDeleteAppointment;
        private System.Windows.Forms.TabPage tabAppointments;
        private System.Windows.Forms.DataGridView dataGridAppointments;
        private System.Windows.Forms.Button btnLoadAppointments;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.CheckBox cbIncludeInheritance;
        private System.Windows.Forms.ComboBox cbFactoryElement;
        private System.Windows.Forms.Label lblFactoryElement;
        private System.Windows.Forms.CheckBox cbSubscribeUpdates;
        private System.Windows.Forms.Button btnCreateTestData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.Button btnCleanup;
    }
}

