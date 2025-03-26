#region Copyright
// SIPLACE Product Name - Copyright (C) ASM AS GmbH & Co. KG 2013
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

namespace DekPrinterExternalControlTestServer
{
    partial class DekPrinterExternalControlTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DekPrinterExternalControlTestForm));
            this.label1 = new System.Windows.Forms.Label();
            this.m_textBoxOibServiceLocatorEndpoint = new System.Windows.Forms.TextBox();
            this.m_groupBoxOibServiceLocator = new System.Windows.Forms.GroupBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.m_ContextMenuStripListBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ToolStripMenuItemClearMessages = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemShowLast = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxVerifyMaterial = new System.Windows.Forms.GroupBox();
            this.comboBoxVerifyMaterialVerificationStatus = new System.Windows.Forms.ComboBox();
            this.labelVerifyMaterialVerificationStatus = new System.Windows.Forms.Label();
            this.textBoxVerifyMaterialMessage = new System.Windows.Forms.TextBox();
            this.labelVerifyMaterialMessage = new System.Windows.Forms.Label();
            this.groupBoxVerifyTool = new System.Windows.Forms.GroupBox();
            this.comboBoxVerifyToolVerificationStatus = new System.Windows.Forms.ComboBox();
            this.labelVerifyToolVerificationStatus = new System.Windows.Forms.Label();
            this.textBoxVerifyToolMessage = new System.Windows.Forms.TextBox();
            this.labelVerifyToolMessage = new System.Windows.Forms.Label();
            this.m_tabPageMesService = new System.Windows.Forms.TabPage();
            this.m_groupBoxGetPuExeError = new System.Windows.Forms.GroupBox();
            this.m_labelMesSleep = new System.Windows.Forms.Label();
            this.m_NumericUpDownMesSleep = new System.Windows.Forms.NumericUpDown();
            this.m_CheckBoxMesThrowException = new System.Windows.Forms.CheckBox();
            this.m_TextBoxMesEndpoint = new System.Windows.Forms.TextBox();
            this.m_labelMesEndpoint = new System.Windows.Forms.Label();
            this.m_tabControlServices = new System.Windows.Forms.TabControl();
            this.m_groupBoxOibServiceLocator.SuspendLayout();
            this.m_ContextMenuStripListBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBoxVerifyMaterial.SuspendLayout();
            this.groupBoxVerifyTool.SuspendLayout();
            this.m_tabPageMesService.SuspendLayout();
            this.m_groupBoxGetPuExeError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownMesSleep)).BeginInit();
            this.m_tabControlServices.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Endpoint:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_textBoxOibServiceLocatorEndpoint
            // 
            this.m_textBoxOibServiceLocatorEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBoxOibServiceLocatorEndpoint.Location = new System.Drawing.Point(61, 19);
            this.m_textBoxOibServiceLocatorEndpoint.Name = "m_textBoxOibServiceLocatorEndpoint";
            this.m_textBoxOibServiceLocatorEndpoint.ReadOnly = true;
            this.m_textBoxOibServiceLocatorEndpoint.Size = new System.Drawing.Size(599, 20);
            this.m_textBoxOibServiceLocatorEndpoint.TabIndex = 1;
            this.m_textBoxOibServiceLocatorEndpoint.Text = "http://localhost:1405/Asm.As.oib.servicelocator/Reliable";
            // 
            // m_groupBoxOibServiceLocator
            // 
            this.m_groupBoxOibServiceLocator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_groupBoxOibServiceLocator.Controls.Add(this.label1);
            this.m_groupBoxOibServiceLocator.Controls.Add(this.m_textBoxOibServiceLocatorEndpoint);
            this.m_groupBoxOibServiceLocator.Location = new System.Drawing.Point(12, 12);
            this.m_groupBoxOibServiceLocator.Name = "m_groupBoxOibServiceLocator";
            this.m_groupBoxOibServiceLocator.Size = new System.Drawing.Size(666, 49);
            this.m_groupBoxOibServiceLocator.TabIndex = 1;
            this.m_groupBoxOibServiceLocator.TabStop = false;
            this.m_groupBoxOibServiceLocator.Text = "OIB Service Locator";
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.ContextMenuStrip = this.m_ContextMenuStripListBox;
            this.listBox.HorizontalExtent = 20000;
            this.listBox.HorizontalScrollbar = true;
            this.listBox.Location = new System.Drawing.Point(12, 563);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(662, 225);
            this.listBox.TabIndex = 4;
            this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            this.listBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseMove);
            // 
            // m_ContextMenuStripListBox
            // 
            this.m_ContextMenuStripListBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItemClearMessages,
            this.m_ToolStripMenuItemShowLast});
            this.m_ContextMenuStripListBox.Name = "m_ContextMenuStripListBox";
            this.m_ContextMenuStripListBox.Size = new System.Drawing.Size(163, 48);
            // 
            // m_ToolStripMenuItemClearMessages
            // 
            this.m_ToolStripMenuItemClearMessages.Name = "m_ToolStripMenuItemClearMessages";
            this.m_ToolStripMenuItemClearMessages.Size = new System.Drawing.Size(162, 22);
            this.m_ToolStripMenuItemClearMessages.Text = "Clear Messages";
            this.m_ToolStripMenuItemClearMessages.Click += new System.EventHandler(this.m_ToolStripMenuItemClearMessages_Click);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBoxVerifyMaterial);
            this.tabPage2.Controls.Add(this.groupBoxVerifyTool);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(658, 469);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Tool & Material Verification";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBoxVerifyMaterial
            // 
            this.groupBoxVerifyMaterial.Controls.Add(this.comboBoxVerifyMaterialVerificationStatus);
            this.groupBoxVerifyMaterial.Controls.Add(this.labelVerifyMaterialVerificationStatus);
            this.groupBoxVerifyMaterial.Controls.Add(this.textBoxVerifyMaterialMessage);
            this.groupBoxVerifyMaterial.Controls.Add(this.labelVerifyMaterialMessage);
            this.groupBoxVerifyMaterial.Location = new System.Drawing.Point(298, 6);
            this.groupBoxVerifyMaterial.Name = "groupBoxVerifyMaterial";
            this.groupBoxVerifyMaterial.Size = new System.Drawing.Size(354, 241);
            this.groupBoxVerifyMaterial.TabIndex = 0;
            this.groupBoxVerifyMaterial.TabStop = false;
            this.groupBoxVerifyMaterial.Text = "Verify Material";
            // 
            // comboBoxVerifyMaterialVerificationStatus
            // 
            this.comboBoxVerifyMaterialVerificationStatus.FormattingEnabled = true;
            this.comboBoxVerifyMaterialVerificationStatus.Items.AddRange(new object[] {
            "VerificationNotRequired",
            "Verified",
            "NotVerified",
            "VerifiedWithWarning",
            "Error"});
            this.comboBoxVerifyMaterialVerificationStatus.Location = new System.Drawing.Point(137, 65);
            this.comboBoxVerifyMaterialVerificationStatus.Name = "comboBoxVerifyMaterialVerificationStatus";
            this.comboBoxVerifyMaterialVerificationStatus.Size = new System.Drawing.Size(88, 21);
            this.comboBoxVerifyMaterialVerificationStatus.TabIndex = 22;
            // 
            // labelVerifyMaterialVerificationStatus
            // 
            this.labelVerifyMaterialVerificationStatus.AutoSize = true;
            this.labelVerifyMaterialVerificationStatus.Location = new System.Drawing.Point(6, 65);
            this.labelVerifyMaterialVerificationStatus.Name = "labelVerifyMaterialVerificationStatus";
            this.labelVerifyMaterialVerificationStatus.Size = new System.Drawing.Size(92, 13);
            this.labelVerifyMaterialVerificationStatus.TabIndex = 20;
            this.labelVerifyMaterialVerificationStatus.Text = "Verification Status";
            // 
            // textBoxVerifyMaterialMessage
            // 
            this.textBoxVerifyMaterialMessage.Location = new System.Drawing.Point(137, 24);
            this.textBoxVerifyMaterialMessage.Name = "textBoxVerifyMaterialMessage";
            this.textBoxVerifyMaterialMessage.Size = new System.Drawing.Size(166, 20);
            this.textBoxVerifyMaterialMessage.TabIndex = 15;
            this.textBoxVerifyMaterialMessage.Text = "Message 2";
            // 
            // labelVerifyMaterialMessage
            // 
            this.labelVerifyMaterialMessage.AutoSize = true;
            this.labelVerifyMaterialMessage.Location = new System.Drawing.Point(6, 24);
            this.labelVerifyMaterialMessage.Name = "labelVerifyMaterialMessage";
            this.labelVerifyMaterialMessage.Size = new System.Drawing.Size(50, 13);
            this.labelVerifyMaterialMessage.TabIndex = 14;
            this.labelVerifyMaterialMessage.Text = "Message";
            // 
            // groupBoxVerifyTool
            // 
            this.groupBoxVerifyTool.Controls.Add(this.comboBoxVerifyToolVerificationStatus);
            this.groupBoxVerifyTool.Controls.Add(this.labelVerifyToolVerificationStatus);
            this.groupBoxVerifyTool.Controls.Add(this.textBoxVerifyToolMessage);
            this.groupBoxVerifyTool.Controls.Add(this.labelVerifyToolMessage);
            this.groupBoxVerifyTool.Location = new System.Drawing.Point(6, 6);
            this.groupBoxVerifyTool.Name = "groupBoxVerifyTool";
            this.groupBoxVerifyTool.Size = new System.Drawing.Size(286, 241);
            this.groupBoxVerifyTool.TabIndex = 0;
            this.groupBoxVerifyTool.TabStop = false;
            this.groupBoxVerifyTool.Text = "Verify Tool";
            // 
            // comboBoxVerifyToolVerificationStatus
            // 
            this.comboBoxVerifyToolVerificationStatus.FormattingEnabled = true;
            this.comboBoxVerifyToolVerificationStatus.Items.AddRange(new object[] {
            "VerificationNotRequired",
            "Verified",
            "NotVerified",
            "VerifiedWithWarning",
            "Error"});
            this.comboBoxVerifyToolVerificationStatus.Location = new System.Drawing.Point(114, 65);
            this.comboBoxVerifyToolVerificationStatus.Name = "comboBoxVerifyToolVerificationStatus";
            this.comboBoxVerifyToolVerificationStatus.Size = new System.Drawing.Size(88, 21);
            this.comboBoxVerifyToolVerificationStatus.TabIndex = 6;
            // 
            // labelVerifyToolVerificationStatus
            // 
            this.labelVerifyToolVerificationStatus.AutoSize = true;
            this.labelVerifyToolVerificationStatus.Location = new System.Drawing.Point(10, 65);
            this.labelVerifyToolVerificationStatus.Name = "labelVerifyToolVerificationStatus";
            this.labelVerifyToolVerificationStatus.Size = new System.Drawing.Size(92, 13);
            this.labelVerifyToolVerificationStatus.TabIndex = 4;
            this.labelVerifyToolVerificationStatus.Text = "Verification Status";
            // 
            // textBoxVerifyToolMessage
            // 
            this.textBoxVerifyToolMessage.Location = new System.Drawing.Point(114, 24);
            this.textBoxVerifyToolMessage.Name = "textBoxVerifyToolMessage";
            this.textBoxVerifyToolMessage.Size = new System.Drawing.Size(166, 20);
            this.textBoxVerifyToolMessage.TabIndex = 1;
            this.textBoxVerifyToolMessage.Text = "Message 1";
            // 
            // labelVerifyToolMessage
            // 
            this.labelVerifyToolMessage.AutoSize = true;
            this.labelVerifyToolMessage.Location = new System.Drawing.Point(7, 24);
            this.labelVerifyToolMessage.Name = "labelVerifyToolMessage";
            this.labelVerifyToolMessage.Size = new System.Drawing.Size(50, 13);
            this.labelVerifyToolMessage.TabIndex = 0;
            this.labelVerifyToolMessage.Text = "Message";
            // 
            // m_tabPageMesService
            // 
            this.m_tabPageMesService.Controls.Add(this.m_groupBoxGetPuExeError);
            this.m_tabPageMesService.Controls.Add(this.m_TextBoxMesEndpoint);
            this.m_tabPageMesService.Controls.Add(this.m_labelMesEndpoint);
            this.m_tabPageMesService.Location = new System.Drawing.Point(4, 22);
            this.m_tabPageMesService.Name = "m_tabPageMesService";
            this.m_tabPageMesService.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabPageMesService.Size = new System.Drawing.Size(658, 469);
            this.m_tabPageMesService.TabIndex = 1;
            this.m_tabPageMesService.Text = "Dek Printer External Control Service";
            this.m_tabPageMesService.UseVisualStyleBackColor = true;
            // 
            // m_groupBoxGetPuExeError
            // 
            this.m_groupBoxGetPuExeError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_groupBoxGetPuExeError.Controls.Add(this.m_labelMesSleep);
            this.m_groupBoxGetPuExeError.Controls.Add(this.m_NumericUpDownMesSleep);
            this.m_groupBoxGetPuExeError.Controls.Add(this.m_CheckBoxMesThrowException);
            this.m_groupBoxGetPuExeError.Location = new System.Drawing.Point(12, 43);
            this.m_groupBoxGetPuExeError.Name = "m_groupBoxGetPuExeError";
            this.m_groupBoxGetPuExeError.Size = new System.Drawing.Size(403, 75);
            this.m_groupBoxGetPuExeError.TabIndex = 3;
            this.m_groupBoxGetPuExeError.TabStop = false;
            this.m_groupBoxGetPuExeError.Text = "Execution Errors";
            // 
            // m_labelMesSleep
            // 
            this.m_labelMesSleep.AutoSize = true;
            this.m_labelMesSleep.Location = new System.Drawing.Point(72, 45);
            this.m_labelMesSleep.Name = "m_labelMesSleep";
            this.m_labelMesSleep.Size = new System.Drawing.Size(156, 13);
            this.m_labelMesSleep.TabIndex = 2;
            this.m_labelMesSleep.Text = "sleep seconds during execution";
            // 
            // m_NumericUpDownMesSleep
            // 
            this.m_NumericUpDownMesSleep.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_NumericUpDownMesSleep.Location = new System.Drawing.Point(6, 43);
            this.m_NumericUpDownMesSleep.Name = "m_NumericUpDownMesSleep";
            this.m_NumericUpDownMesSleep.Size = new System.Drawing.Size(60, 20);
            this.m_NumericUpDownMesSleep.TabIndex = 1;
            // 
            // m_CheckBoxMesThrowException
            // 
            this.m_CheckBoxMesThrowException.AutoSize = true;
            this.m_CheckBoxMesThrowException.Location = new System.Drawing.Point(6, 19);
            this.m_CheckBoxMesThrowException.Name = "m_CheckBoxMesThrowException";
            this.m_CheckBoxMesThrowException.Size = new System.Drawing.Size(156, 17);
            this.m_CheckBoxMesThrowException.TabIndex = 0;
            this.m_CheckBoxMesThrowException.Text = "throw exception if executed";
            this.m_CheckBoxMesThrowException.UseVisualStyleBackColor = true;
            // 
            // m_TextBoxMesEndpoint
            // 
            this.m_TextBoxMesEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TextBoxMesEndpoint.Location = new System.Drawing.Point(67, 17);
            this.m_TextBoxMesEndpoint.Name = "m_TextBoxMesEndpoint";
            this.m_TextBoxMesEndpoint.ReadOnly = true;
            this.m_TextBoxMesEndpoint.Size = new System.Drawing.Size(583, 20);
            this.m_TextBoxMesEndpoint.TabIndex = 1;
            // 
            // m_labelMesEndpoint
            // 
            this.m_labelMesEndpoint.AutoSize = true;
            this.m_labelMesEndpoint.Location = new System.Drawing.Point(9, 20);
            this.m_labelMesEndpoint.Name = "m_labelMesEndpoint";
            this.m_labelMesEndpoint.Size = new System.Drawing.Size(52, 13);
            this.m_labelMesEndpoint.TabIndex = 0;
            this.m_labelMesEndpoint.Text = "Endpoint:";
            this.m_labelMesEndpoint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_tabControlServices
            // 
            this.m_tabControlServices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControlServices.Controls.Add(this.m_tabPageMesService);
            this.m_tabControlServices.Controls.Add(this.tabPage2);
            this.m_tabControlServices.Location = new System.Drawing.Point(12, 67);
            this.m_tabControlServices.Name = "m_tabControlServices";
            this.m_tabControlServices.SelectedIndex = 0;
            this.m_tabControlServices.Size = new System.Drawing.Size(666, 495);
            this.m_tabControlServices.TabIndex = 3;
            // 
            // DekPrinterExternalControlTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 804);
            this.Controls.Add(this.m_tabControlServices);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.m_groupBoxOibServiceLocator);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DekPrinterExternalControlTestForm";
            this.Text = "Dek Printer External Control Testserver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DekPrinterExternalControlTestForm_FormClosing);
            this.Load += new System.EventHandler(this.DekPrinterExternalControlTestForm_Load);
            this.m_groupBoxOibServiceLocator.ResumeLayout(false);
            this.m_groupBoxOibServiceLocator.PerformLayout();
            this.m_ContextMenuStripListBox.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBoxVerifyMaterial.ResumeLayout(false);
            this.groupBoxVerifyMaterial.PerformLayout();
            this.groupBoxVerifyTool.ResumeLayout(false);
            this.groupBoxVerifyTool.PerformLayout();
            this.m_tabPageMesService.ResumeLayout(false);
            this.m_tabPageMesService.PerformLayout();
            this.m_groupBoxGetPuExeError.ResumeLayout(false);
            this.m_groupBoxGetPuExeError.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownMesSleep)).EndInit();
            this.m_tabControlServices.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_textBoxOibServiceLocatorEndpoint;
        private System.Windows.Forms.GroupBox m_groupBoxOibServiceLocator;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ContextMenuStrip m_ContextMenuStripListBox;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemClearMessages;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemShowLast;
        private System.Windows.Forms.SaveFileDialog m_SaveFileDialog;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxVerifyMaterial;
        private System.Windows.Forms.ComboBox comboBoxVerifyMaterialVerificationStatus;
        private System.Windows.Forms.Label labelVerifyMaterialVerificationStatus;
        private System.Windows.Forms.TextBox textBoxVerifyMaterialMessage;
        private System.Windows.Forms.Label labelVerifyMaterialMessage;
        private System.Windows.Forms.GroupBox groupBoxVerifyTool;
        private System.Windows.Forms.ComboBox comboBoxVerifyToolVerificationStatus;
        private System.Windows.Forms.Label labelVerifyToolVerificationStatus;
        private System.Windows.Forms.TextBox textBoxVerifyToolMessage;
        private System.Windows.Forms.Label labelVerifyToolMessage;
        private System.Windows.Forms.TabPage m_tabPageMesService;
        private System.Windows.Forms.GroupBox m_groupBoxGetPuExeError;
        private System.Windows.Forms.Label m_labelMesSleep;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownMesSleep;
        private System.Windows.Forms.CheckBox m_CheckBoxMesThrowException;
        private System.Windows.Forms.TextBox m_TextBoxMesEndpoint;
        private System.Windows.Forms.Label m_labelMesEndpoint;
        private System.Windows.Forms.TabControl m_tabControlServices;
    }
}

