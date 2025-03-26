#region Copyright
//-----------------------------------------------------------------------
// <copyright file="MainForm.designer.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------
#endregion

#region Namespaces
using OIB.Tutorial.Common.Logging;
#endregion

namespace OIB.Tutorial
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
            this.labelServiceLocatorMexEndpoint = new System.Windows.Forms.Label();
            this.textBoxServiceLocatorMexEndpoint = new System.Windows.Forms.TextBox();
            this.groupBoxMesService = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxComponentXmlFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonComponentStatus = new System.Windows.Forms.Button();
            this.buttonReloadXmlComponentStatus = new System.Windows.Forms.Button();
            this.groupBoxModifyPackagingUnit = new System.Windows.Forms.GroupBox();
            this.dateTimePickerMSD = new System.Windows.Forms.DateTimePicker();
            this.textBoxSiplaceProComponentName = new System.Windows.Forms.TextBox();
            this.checkBoxSiplaceProComponentName = new System.Windows.Forms.CheckBox();
            this.labelMsdOpenDate = new System.Windows.Forms.Label();
            this.nudMsdLevel = new System.Windows.Forms.NumericUpDown();
            this.checkBoxMsdLevel = new System.Windows.Forms.CheckBox();
            this.nudRohs = new System.Windows.Forms.NumericUpDown();
            this.nudOriginalQuantity = new System.Windows.Forms.NumericUpDown();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRohs = new System.Windows.Forms.CheckBox();
            this.checkBoxOriginalQuantity = new System.Windows.Forms.CheckBox();
            this.checkBoxQuantity = new System.Windows.Forms.CheckBox();
            this.groupBoxNewPackagingUnit = new System.Windows.Forms.GroupBox();
            this.textBoxXmlFilePath = new System.Windows.Forms.TextBox();
            this.labelXmlFilePath = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonReloadAvailablePuFromXml = new System.Windows.Forms.Button();
            this.groupBoxExecutionErrors = new System.Windows.Forms.GroupBox();
            this.checkBoxSleep = new System.Windows.Forms.CheckBox();
            this.nudSleep = new System.Windows.Forms.NumericUpDown();
            this.checkBoxThrowException = new System.Windows.Forms.CheckBox();
            this.buttonUnregisterMesService = new System.Windows.Forms.Button();
            this.buttonStopMesService = new System.Windows.Forms.Button();
            this.buttonStartMesService = new System.Windows.Forms.Button();
            this.textBoxMesServiceEndpoint = new System.Windows.Forms.TextBox();
            this.labelMesServiceEndpoint = new System.Windows.Forms.Label();
            this.loggerListView = new OIB.Tutorial.Common.Logging.LoggerListView();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.groupBoxMesService.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxModifyPackagingUnit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMsdLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRohs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOriginalQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.groupBoxNewPackagingUnit.SuspendLayout();
            this.groupBoxExecutionErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleep)).BeginInit();
            this.SuspendLayout();
            // 
            // labelServiceLocatorMexEndpoint
            // 
            this.labelServiceLocatorMexEndpoint.AutoSize = true;
            this.labelServiceLocatorMexEndpoint.Location = new System.Drawing.Point(12, 15);
            this.labelServiceLocatorMexEndpoint.Name = "labelServiceLocatorMexEndpoint";
            this.labelServiceLocatorMexEndpoint.Size = new System.Drawing.Size(209, 13);
            this.labelServiceLocatorMexEndpoint.TabIndex = 1;
            this.labelServiceLocatorMexEndpoint.Text = "OIB Service Locator Service MEX Address";
            // 
            // textBoxServiceLocatorMexEndpoint
            // 
            this.textBoxServiceLocatorMexEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServiceLocatorMexEndpoint.Location = new System.Drawing.Point(227, 12);
            this.textBoxServiceLocatorMexEndpoint.Name = "textBoxServiceLocatorMexEndpoint";
            this.textBoxServiceLocatorMexEndpoint.Size = new System.Drawing.Size(488, 20);
            this.textBoxServiceLocatorMexEndpoint.TabIndex = 2;
            this.textBoxServiceLocatorMexEndpoint.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/mex";
            // 
            // groupBoxMesService
            // 
            this.groupBoxMesService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMesService.Controls.Add(this.groupBox1);
            this.groupBoxMesService.Controls.Add(this.groupBoxModifyPackagingUnit);
            this.groupBoxMesService.Controls.Add(this.groupBoxNewPackagingUnit);
            this.groupBoxMesService.Controls.Add(this.groupBoxExecutionErrors);
            this.groupBoxMesService.Controls.Add(this.buttonUnregisterMesService);
            this.groupBoxMesService.Controls.Add(this.buttonStopMesService);
            this.groupBoxMesService.Controls.Add(this.buttonStartMesService);
            this.groupBoxMesService.Controls.Add(this.textBoxMesServiceEndpoint);
            this.groupBoxMesService.Controls.Add(this.labelMesServiceEndpoint);
            this.groupBoxMesService.Location = new System.Drawing.Point(12, 48);
            this.groupBoxMesService.Name = "groupBoxMesService";
            this.groupBoxMesService.Size = new System.Drawing.Size(703, 541);
            this.groupBoxMesService.TabIndex = 3;
            this.groupBoxMesService.TabStop = false;
            this.groupBoxMesService.Text = "MES Service";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxComponentXmlFilePath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonComponentStatus);
            this.groupBox1.Controls.Add(this.buttonReloadXmlComponentStatus);
            this.groupBox1.Location = new System.Drawing.Point(9, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 75);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Component verification";
            // 
            // textBoxComponentXmlFilePath
            // 
            this.textBoxComponentXmlFilePath.Location = new System.Drawing.Point(85, 21);
            this.textBoxComponentXmlFilePath.Name = "textBoxComponentXmlFilePath";
            this.textBoxComponentXmlFilePath.Size = new System.Drawing.Size(173, 20);
            this.textBoxComponentXmlFilePath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "XML File Path";
            // 
            // buttonComponentStatus
            // 
            this.buttonComponentStatus.Location = new System.Drawing.Point(290, 43);
            this.buttonComponentStatus.Name = "buttonComponentStatus";
            this.buttonComponentStatus.Size = new System.Drawing.Size(75, 23);
            this.buttonComponentStatus.TabIndex = 1;
            this.buttonComponentStatus.Text = "Status";
            this.buttonComponentStatus.UseVisualStyleBackColor = true;
            this.buttonComponentStatus.Click += new System.EventHandler(this.buttonComponentStatus_Click);
            // 
            // buttonReloadXmlComponentStatus
            // 
            this.buttonReloadXmlComponentStatus.Location = new System.Drawing.Point(278, 14);
            this.buttonReloadXmlComponentStatus.Name = "buttonReloadXmlComponentStatus";
            this.buttonReloadXmlComponentStatus.Size = new System.Drawing.Size(98, 23);
            this.buttonReloadXmlComponentStatus.TabIndex = 0;
            this.buttonReloadXmlComponentStatus.Text = "Reload from XML";
            this.buttonReloadXmlComponentStatus.UseVisualStyleBackColor = true;
            this.buttonReloadXmlComponentStatus.Click += new System.EventHandler(this.buttonReloadXmlComponentStatus_Click);
            // 
            // groupBoxModifyPackagingUnit
            // 
            this.groupBoxModifyPackagingUnit.Controls.Add(this.dateTimePickerMSD);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.textBoxSiplaceProComponentName);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.checkBoxSiplaceProComponentName);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.labelMsdOpenDate);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.nudMsdLevel);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.checkBoxMsdLevel);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.nudRohs);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.nudOriginalQuantity);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.nudQuantity);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.checkBoxRohs);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.checkBoxOriginalQuantity);
            this.groupBoxModifyPackagingUnit.Controls.Add(this.checkBoxQuantity);
            this.groupBoxModifyPackagingUnit.Location = new System.Drawing.Point(9, 137);
            this.groupBoxModifyPackagingUnit.Name = "groupBoxModifyPackagingUnit";
            this.groupBoxModifyPackagingUnit.Size = new System.Drawing.Size(678, 121);
            this.groupBoxModifyPackagingUnit.TabIndex = 7;
            this.groupBoxModifyPackagingUnit.TabStop = false;
            this.groupBoxModifyPackagingUnit.Text = "Modify Packaging Unit";
            // 
            // dateTimePickerMSD
            // 
            this.dateTimePickerMSD.Location = new System.Drawing.Point(242, 55);
            this.dateTimePickerMSD.Name = "dateTimePickerMSD";
            this.dateTimePickerMSD.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerMSD.TabIndex = 16;
            // 
            // textBoxSiplaceProComponentName
            // 
            this.textBoxSiplaceProComponentName.Enabled = false;
            this.textBoxSiplaceProComponentName.Location = new System.Drawing.Point(180, 93);
            this.textBoxSiplaceProComponentName.Name = "textBoxSiplaceProComponentName";
            this.textBoxSiplaceProComponentName.Size = new System.Drawing.Size(185, 20);
            this.textBoxSiplaceProComponentName.TabIndex = 14;
            this.textBoxSiplaceProComponentName.TextChanged += new System.EventHandler(this.textBoxSiplaceProComponentName_TextChanged);
            // 
            // checkBoxSiplaceProComponentName
            // 
            this.checkBoxSiplaceProComponentName.AutoSize = true;
            this.checkBoxSiplaceProComponentName.Location = new System.Drawing.Point(9, 95);
            this.checkBoxSiplaceProComponentName.Name = "checkBoxSiplaceProComponentName";
            this.checkBoxSiplaceProComponentName.Size = new System.Drawing.Size(168, 17);
            this.checkBoxSiplaceProComponentName.TabIndex = 13;
            this.checkBoxSiplaceProComponentName.Text = "Siplace Pro Component Name";
            this.checkBoxSiplaceProComponentName.UseVisualStyleBackColor = true;
            this.checkBoxSiplaceProComponentName.CheckedChanged += new System.EventHandler(this.checkBoxSiplaceProComponentName_CheckedChanged);
            // 
            // labelMsdOpenDate
            // 
            this.labelMsdOpenDate.AutoSize = true;
            this.labelMsdOpenDate.Enabled = false;
            this.labelMsdOpenDate.Location = new System.Drawing.Point(151, 59);
            this.labelMsdOpenDate.Name = "labelMsdOpenDate";
            this.labelMsdOpenDate.Size = new System.Drawing.Size(85, 13);
            this.labelMsdOpenDate.TabIndex = 11;
            this.labelMsdOpenDate.Text = "MSD open date:";
            // 
            // nudMsdLevel
            // 
            this.nudMsdLevel.Enabled = false;
            this.nudMsdLevel.Location = new System.Drawing.Point(94, 55);
            this.nudMsdLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMsdLevel.Name = "nudMsdLevel";
            this.nudMsdLevel.Size = new System.Drawing.Size(41, 20);
            this.nudMsdLevel.TabIndex = 10;
            this.nudMsdLevel.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudMsdLevel.ValueChanged += new System.EventHandler(this.nudMsdLevel_ValueChanged);
            // 
            // checkBoxMsdLevel
            // 
            this.checkBoxMsdLevel.AutoSize = true;
            this.checkBoxMsdLevel.Location = new System.Drawing.Point(9, 57);
            this.checkBoxMsdLevel.Name = "checkBoxMsdLevel";
            this.checkBoxMsdLevel.Size = new System.Drawing.Size(79, 17);
            this.checkBoxMsdLevel.TabIndex = 9;
            this.checkBoxMsdLevel.Text = "MSD Level";
            this.checkBoxMsdLevel.UseVisualStyleBackColor = true;
            this.checkBoxMsdLevel.CheckedChanged += new System.EventHandler(this.checkBoxMsdLevel_CheckedChanged);
            // 
            // nudRohs
            // 
            this.nudRohs.Enabled = false;
            this.nudRohs.Location = new System.Drawing.Point(500, 19);
            this.nudRohs.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRohs.Name = "nudRohs";
            this.nudRohs.Size = new System.Drawing.Size(83, 20);
            this.nudRohs.TabIndex = 8;
            this.nudRohs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRohs.ValueChanged += new System.EventHandler(this.nudRohs_ValueChanged);
            // 
            // nudOriginalQuantity
            // 
            this.nudOriginalQuantity.Enabled = false;
            this.nudOriginalQuantity.Location = new System.Drawing.Point(315, 19);
            this.nudOriginalQuantity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudOriginalQuantity.Name = "nudOriginalQuantity";
            this.nudOriginalQuantity.Size = new System.Drawing.Size(83, 20);
            this.nudOriginalQuantity.TabIndex = 7;
            this.nudOriginalQuantity.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudOriginalQuantity.ValueChanged += new System.EventHandler(this.nudOriginalQuantity_ValueChanged);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Enabled = false;
            this.nudQuantity.Location = new System.Drawing.Point(80, 19);
            this.nudQuantity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(83, 20);
            this.nudQuantity.TabIndex = 6;
            this.nudQuantity.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            // 
            // checkBoxRohs
            // 
            this.checkBoxRohs.AutoSize = true;
            this.checkBoxRohs.Location = new System.Drawing.Point(439, 21);
            this.checkBoxRohs.Name = "checkBoxRohs";
            this.checkBoxRohs.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRohs.TabIndex = 4;
            this.checkBoxRohs.Text = "RoHS";
            this.checkBoxRohs.UseVisualStyleBackColor = true;
            this.checkBoxRohs.CheckedChanged += new System.EventHandler(this.checkBoxRohs_CheckedChanged);
            // 
            // checkBoxOriginalQuantity
            // 
            this.checkBoxOriginalQuantity.AutoSize = true;
            this.checkBoxOriginalQuantity.Location = new System.Drawing.Point(206, 21);
            this.checkBoxOriginalQuantity.Name = "checkBoxOriginalQuantity";
            this.checkBoxOriginalQuantity.Size = new System.Drawing.Size(103, 17);
            this.checkBoxOriginalQuantity.TabIndex = 1;
            this.checkBoxOriginalQuantity.Text = "Original Quantity";
            this.checkBoxOriginalQuantity.UseVisualStyleBackColor = true;
            this.checkBoxOriginalQuantity.CheckedChanged += new System.EventHandler(this.checkBoxOriginalQuantity_CheckedChanged);
            // 
            // checkBoxQuantity
            // 
            this.checkBoxQuantity.AutoSize = true;
            this.checkBoxQuantity.Location = new System.Drawing.Point(9, 21);
            this.checkBoxQuantity.Name = "checkBoxQuantity";
            this.checkBoxQuantity.Size = new System.Drawing.Size(65, 17);
            this.checkBoxQuantity.TabIndex = 0;
            this.checkBoxQuantity.Text = "Quantity";
            this.checkBoxQuantity.UseVisualStyleBackColor = true;
            this.checkBoxQuantity.CheckedChanged += new System.EventHandler(this.checkBoxQuantity_CheckedChanged);
            // 
            // groupBoxNewPackagingUnit
            // 
            this.groupBoxNewPackagingUnit.Controls.Add(this.textBoxXmlFilePath);
            this.groupBoxNewPackagingUnit.Controls.Add(this.labelXmlFilePath);
            this.groupBoxNewPackagingUnit.Controls.Add(this.button1);
            this.groupBoxNewPackagingUnit.Controls.Add(this.buttonReloadAvailablePuFromXml);
            this.groupBoxNewPackagingUnit.Location = new System.Drawing.Point(352, 51);
            this.groupBoxNewPackagingUnit.Name = "groupBoxNewPackagingUnit";
            this.groupBoxNewPackagingUnit.Size = new System.Drawing.Size(335, 80);
            this.groupBoxNewPackagingUnit.TabIndex = 6;
            this.groupBoxNewPackagingUnit.TabStop = false;
            this.groupBoxNewPackagingUnit.Text = "Request New Packaging Unit";
            // 
            // textBoxXmlFilePath
            // 
            this.textBoxXmlFilePath.Location = new System.Drawing.Point(85, 48);
            this.textBoxXmlFilePath.Name = "textBoxXmlFilePath";
            this.textBoxXmlFilePath.Size = new System.Drawing.Size(229, 20);
            this.textBoxXmlFilePath.TabIndex = 3;
            this.textBoxXmlFilePath.TextChanged += new System.EventHandler(this.textBoxXmlFilePath_TextChanged);
            // 
            // labelXmlFilePath
            // 
            this.labelXmlFilePath.AutoSize = true;
            this.labelXmlFilePath.Location = new System.Drawing.Point(6, 52);
            this.labelXmlFilePath.Name = "labelXmlFilePath";
            this.labelXmlFilePath.Size = new System.Drawing.Size(73, 13);
            this.labelXmlFilePath.TabIndex = 2;
            this.labelXmlFilePath.Text = "XML File Path";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(167, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Available Packaging Units";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonReloadAvailablePuFromXml
            // 
            this.buttonReloadAvailablePuFromXml.Location = new System.Drawing.Point(57, 17);
            this.buttonReloadAvailablePuFromXml.Name = "buttonReloadAvailablePuFromXml";
            this.buttonReloadAvailablePuFromXml.Size = new System.Drawing.Size(104, 23);
            this.buttonReloadAvailablePuFromXml.TabIndex = 0;
            this.buttonReloadAvailablePuFromXml.Text = "Reload from XML";
            this.buttonReloadAvailablePuFromXml.UseVisualStyleBackColor = true;
            this.buttonReloadAvailablePuFromXml.Click += new System.EventHandler(this.buttonReloadAvailablePuFromXml_Click);
            // 
            // groupBoxExecutionErrors
            // 
            this.groupBoxExecutionErrors.Controls.Add(this.checkBoxSleep);
            this.groupBoxExecutionErrors.Controls.Add(this.nudSleep);
            this.groupBoxExecutionErrors.Controls.Add(this.checkBoxThrowException);
            this.groupBoxExecutionErrors.Location = new System.Drawing.Point(9, 51);
            this.groupBoxExecutionErrors.Name = "groupBoxExecutionErrors";
            this.groupBoxExecutionErrors.Size = new System.Drawing.Size(337, 80);
            this.groupBoxExecutionErrors.TabIndex = 5;
            this.groupBoxExecutionErrors.TabStop = false;
            this.groupBoxExecutionErrors.Text = "Execution Errors";
            // 
            // checkBoxSleep
            // 
            this.checkBoxSleep.AutoSize = true;
            this.checkBoxSleep.Location = new System.Drawing.Point(9, 45);
            this.checkBoxSleep.Name = "checkBoxSleep";
            this.checkBoxSleep.Size = new System.Drawing.Size(151, 17);
            this.checkBoxSleep.TabIndex = 2;
            this.checkBoxSleep.Text = "Sleep During Execution (s)";
            this.checkBoxSleep.UseVisualStyleBackColor = true;
            this.checkBoxSleep.CheckedChanged += new System.EventHandler(this.checkBoxSleep_CheckedChanged);
            // 
            // nudSleep
            // 
            this.nudSleep.Enabled = false;
            this.nudSleep.Location = new System.Drawing.Point(167, 43);
            this.nudSleep.Name = "nudSleep";
            this.nudSleep.Size = new System.Drawing.Size(46, 20);
            this.nudSleep.TabIndex = 1;
            this.nudSleep.ValueChanged += new System.EventHandler(this.nudSleep_ValueChanged);
            // 
            // checkBoxThrowException
            // 
            this.checkBoxThrowException.AutoSize = true;
            this.checkBoxThrowException.Location = new System.Drawing.Point(9, 19);
            this.checkBoxThrowException.Name = "checkBoxThrowException";
            this.checkBoxThrowException.Size = new System.Drawing.Size(106, 17);
            this.checkBoxThrowException.TabIndex = 0;
            this.checkBoxThrowException.Text = "Throw Exception";
            this.checkBoxThrowException.UseVisualStyleBackColor = true;
            this.checkBoxThrowException.CheckedChanged += new System.EventHandler(this.checkBoxThrowException_CheckedChanged);
            // 
            // buttonUnregisterMesService
            // 
            this.buttonUnregisterMesService.Location = new System.Drawing.Point(525, 293);
            this.buttonUnregisterMesService.Name = "buttonUnregisterMesService";
            this.buttonUnregisterMesService.Size = new System.Drawing.Size(162, 23);
            this.buttonUnregisterMesService.TabIndex = 4;
            this.buttonUnregisterMesService.Text = "Unregister All SC MES Service";
            this.buttonUnregisterMesService.UseVisualStyleBackColor = true;
            this.buttonUnregisterMesService.Click += new System.EventHandler(this.buttonUnregisterMesService_Click);
            // 
            // buttonStopMesService
            // 
            this.buttonStopMesService.Enabled = false;
            this.buttonStopMesService.Location = new System.Drawing.Point(566, 264);
            this.buttonStopMesService.Name = "buttonStopMesService";
            this.buttonStopMesService.Size = new System.Drawing.Size(121, 23);
            this.buttonStopMesService.TabIndex = 3;
            this.buttonStopMesService.Text = "Stop SC MES Service";
            this.buttonStopMesService.UseVisualStyleBackColor = true;
            this.buttonStopMesService.Click += new System.EventHandler(this.buttonStopMesService_Click);
            // 
            // buttonStartMesService
            // 
            this.buttonStartMesService.Location = new System.Drawing.Point(440, 264);
            this.buttonStartMesService.Name = "buttonStartMesService";
            this.buttonStartMesService.Size = new System.Drawing.Size(123, 23);
            this.buttonStartMesService.TabIndex = 2;
            this.buttonStartMesService.Text = "Start SC MES Service";
            this.buttonStartMesService.UseVisualStyleBackColor = true;
            this.buttonStartMesService.Click += new System.EventHandler(this.buttonStartMesService_Click);
            // 
            // textBoxMesServiceEndpoint
            // 
            this.textBoxMesServiceEndpoint.Location = new System.Drawing.Point(100, 25);
            this.textBoxMesServiceEndpoint.Name = "textBoxMesServiceEndpoint";
            this.textBoxMesServiceEndpoint.Size = new System.Drawing.Size(587, 20);
            this.textBoxMesServiceEndpoint.TabIndex = 1;
            this.textBoxMesServiceEndpoint.Text = "http://localhost:33335/SiplaceOibTestMes";
            // 
            // labelMesServiceEndpoint
            // 
            this.labelMesServiceEndpoint.AutoSize = true;
            this.labelMesServiceEndpoint.Location = new System.Drawing.Point(6, 28);
            this.labelMesServiceEndpoint.Name = "labelMesServiceEndpoint";
            this.labelMesServiceEndpoint.Size = new System.Drawing.Size(88, 13);
            this.labelMesServiceEndpoint.TabIndex = 0;
            this.labelMesServiceEndpoint.Text = "Service Endpoint";
            // 
            // loggerListView
            // 
            this.loggerListView.Location = new System.Drawing.Point(18, 405);
            this.loggerListView.Name = "loggerListView";
            this.loggerListView.Size = new System.Drawing.Size(691, 177);
            this.loggerListView.TabIndex = 4;
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(227, 36);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 5;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 592);
            this.Controls.Add(this._cbUseClientAuthentication);
            this.Controls.Add(this.loggerListView);
            this.Controls.Add(this.groupBoxMesService);
            this.Controls.Add(this.textBoxServiceLocatorMexEndpoint);
            this.Controls.Add(this.labelServiceLocatorMexEndpoint);
            this.Name = "MainForm";
            this.Text = "Setup Center MES Tutorial";
            this.groupBoxMesService.ResumeLayout(false);
            this.groupBoxMesService.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxModifyPackagingUnit.ResumeLayout(false);
            this.groupBoxModifyPackagingUnit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMsdLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRohs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOriginalQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.groupBoxNewPackagingUnit.ResumeLayout(false);
            this.groupBoxNewPackagingUnit.PerformLayout();
            this.groupBoxExecutionErrors.ResumeLayout(false);
            this.groupBoxExecutionErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelServiceLocatorMexEndpoint;
        private System.Windows.Forms.TextBox textBoxServiceLocatorMexEndpoint;
        private System.Windows.Forms.GroupBox groupBoxMesService;
        private System.Windows.Forms.TextBox textBoxMesServiceEndpoint;
        private System.Windows.Forms.Label labelMesServiceEndpoint;
        private System.Windows.Forms.Button buttonUnregisterMesService;
        private System.Windows.Forms.Button buttonStopMesService;
        private System.Windows.Forms.Button buttonStartMesService;
        private System.Windows.Forms.GroupBox groupBoxModifyPackagingUnit;
        private System.Windows.Forms.GroupBox groupBoxNewPackagingUnit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonReloadAvailablePuFromXml;
        private System.Windows.Forms.GroupBox groupBoxExecutionErrors;
        private System.Windows.Forms.TextBox textBoxSiplaceProComponentName;
        private System.Windows.Forms.CheckBox checkBoxSiplaceProComponentName;
        private System.Windows.Forms.Label labelMsdOpenDate;
        private System.Windows.Forms.NumericUpDown nudMsdLevel;
        private System.Windows.Forms.CheckBox checkBoxMsdLevel;
        private System.Windows.Forms.NumericUpDown nudRohs;
        private System.Windows.Forms.NumericUpDown nudOriginalQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.CheckBox checkBoxRohs;
        private System.Windows.Forms.CheckBox checkBoxOriginalQuantity;
        private System.Windows.Forms.CheckBox checkBoxQuantity;
        private System.Windows.Forms.TextBox textBoxXmlFilePath;
        private System.Windows.Forms.Label labelXmlFilePath;
        private System.Windows.Forms.NumericUpDown nudSleep;
        private System.Windows.Forms.CheckBox checkBoxThrowException;
        private System.Windows.Forms.CheckBox checkBoxSleep;
        private LoggerListView loggerListView;
        private System.Windows.Forms.DateTimePicker dateTimePickerMSD;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonComponentStatus;
        private System.Windows.Forms.Button buttonReloadXmlComponentStatus;
        private System.Windows.Forms.TextBox textBoxComponentXmlFilePath;
    }
}

