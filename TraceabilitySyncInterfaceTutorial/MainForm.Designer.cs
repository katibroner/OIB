namespace TraceabilitySyncInterfaceTutorial
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
            this._gbCore = new System.Windows.Forms.GroupBox();
            this._tbOIBCoreName = new System.Windows.Forms.TextBox();
            this._lbCore = new System.Windows.Forms.Label();
            this._listbMessages = new System.Windows.Forms.ListBox();
            this._gbTrace = new System.Windows.Forms.GroupBox();
            this._btnStopService = new System.Windows.Forms.Button();
            this._btnStartService = new System.Windows.Forms.Button();
            this._btnUnregister = new System.Windows.Forms.Button();
            this._btnRegister = new System.Windows.Forms.Button();
            this._tbDescription = new System.Windows.Forms.TextBox();
            this._tbProductName = new System.Windows.Forms.TextBox();
            this._lbProduct = new System.Windows.Forms.Label();
            this._oibServiceNameTb = new System.Windows.Forms.TextBox();
            this._lbService = new System.Windows.Forms.Label();
            this._gbResultValues = new System.Windows.Forms.GroupBox();
            this._tbBoardValidationReason = new System.Windows.Forms.TextBox();
            this._lbReason = new System.Windows.Forms.Label();
            this._rbError = new System.Windows.Forms.RadioButton();
            this._lbResult = new System.Windows.Forms.Label();
            this._rbRejectedPass = new System.Windows.Forms.RadioButton();
            this._rbRejectedHold = new System.Windows.Forms.RadioButton();
            this._rbConfirmed = new System.Windows.Forms.RadioButton();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this._gbCore.SuspendLayout();
            this._gbTrace.SuspendLayout();
            this._gbResultValues.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gbCore
            // 
            this._gbCore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gbCore.Controls.Add(this._tbOIBCoreName);
            this._gbCore.Controls.Add(this._lbCore);
            this._gbCore.Location = new System.Drawing.Point(12, 12);
            this._gbCore.Name = "_gbCore";
            this._gbCore.Size = new System.Drawing.Size(763, 48);
            this._gbCore.TabIndex = 2;
            this._gbCore.TabStop = false;
            this._gbCore.Text = "OIB Core Computer";
            // 
            // _tbOIBCoreName
            // 
            this._tbOIBCoreName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbOIBCoreName.Location = new System.Drawing.Point(157, 16);
            this._tbOIBCoreName.Name = "_tbOIBCoreName";
            this._tbOIBCoreName.Size = new System.Drawing.Size(465, 20);
            this._tbOIBCoreName.TabIndex = 0;
            // 
            // _lbCore
            // 
            this._lbCore.AutoSize = true;
            this._lbCore.Location = new System.Drawing.Point(6, 19);
            this._lbCore.Name = "_lbCore";
            this._lbCore.Size = new System.Drawing.Size(128, 13);
            this._lbCore.TabIndex = 0;
            this._lbCore.Text = "OIB core computer name:";
            // 
            // _listbMessages
            // 
            this._listbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._listbMessages.FormattingEnabled = true;
            this._listbMessages.HorizontalScrollbar = true;
            this._listbMessages.Location = new System.Drawing.Point(12, 371);
            this._listbMessages.Name = "_listbMessages";
            this._listbMessages.Size = new System.Drawing.Size(763, 186);
            this._listbMessages.TabIndex = 0;
            // 
            // _gbTrace
            // 
            this._gbTrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gbTrace.Controls.Add(this._cbUseClientAuthentication);
            this._gbTrace.Controls.Add(this._btnStopService);
            this._gbTrace.Controls.Add(this._btnStartService);
            this._gbTrace.Controls.Add(this._btnUnregister);
            this._gbTrace.Controls.Add(this._btnRegister);
            this._gbTrace.Controls.Add(this._tbDescription);
            this._gbTrace.Controls.Add(this._tbProductName);
            this._gbTrace.Controls.Add(this._lbProduct);
            this._gbTrace.Controls.Add(this._oibServiceNameTb);
            this._gbTrace.Controls.Add(this._lbService);
            this._gbTrace.Location = new System.Drawing.Point(12, 75);
            this._gbTrace.Name = "_gbTrace";
            this._gbTrace.Size = new System.Drawing.Size(763, 170);
            this._gbTrace.TabIndex = 22;
            this._gbTrace.TabStop = false;
            this._gbTrace.Text = "Hosting Traceability Sync Service";
            // 
            // _btnStopService
            // 
            this._btnStopService.Enabled = false;
            this._btnStopService.Location = new System.Drawing.Point(139, 109);
            this._btnStopService.Name = "_btnStopService";
            this._btnStopService.Size = new System.Drawing.Size(75, 23);
            this._btnStopService.TabIndex = 4;
            this._btnStopService.Text = "Stop service";
            this._btnStopService.UseVisualStyleBackColor = true;
            this._btnStopService.Click += new System.EventHandler(this.ButtonStopService_Click);
            // 
            // _btnStartService
            // 
            this._btnStartService.Location = new System.Drawing.Point(9, 109);
            this._btnStartService.Name = "_btnStartService";
            this._btnStartService.Size = new System.Drawing.Size(82, 22);
            this._btnStartService.TabIndex = 2;
            this._btnStartService.Text = "Start service";
            this._btnStartService.UseVisualStyleBackColor = true;
            this._btnStartService.Click += new System.EventHandler(this.ButtonStartService_Click);
            // 
            // _btnUnregister
            // 
            this._btnUnregister.Enabled = false;
            this._btnUnregister.Location = new System.Drawing.Point(139, 138);
            this._btnUnregister.Name = "_btnUnregister";
            this._btnUnregister.Size = new System.Drawing.Size(110, 23);
            this._btnUnregister.TabIndex = 5;
            this._btnUnregister.Text = "Unregister Service";
            this._btnUnregister.UseVisualStyleBackColor = true;
            this._btnUnregister.Click += new System.EventHandler(this.ButtonUnregister_Click);
            // 
            // _btnRegister
            // 
            this._btnRegister.Enabled = false;
            this._btnRegister.Location = new System.Drawing.Point(9, 137);
            this._btnRegister.Name = "_btnRegister";
            this._btnRegister.Size = new System.Drawing.Size(99, 23);
            this._btnRegister.TabIndex = 3;
            this._btnRegister.Text = "Register Service";
            this._btnRegister.UseVisualStyleBackColor = true;
            this._btnRegister.Click += new System.EventHandler(this.ButtonRegister_Click);
            // 
            // _tbDescription
            // 
            this._tbDescription.Location = new System.Drawing.Point(385, 26);
            this._tbDescription.Name = "_tbDescription";
            this._tbDescription.Size = new System.Drawing.Size(250, 20);
            this._tbDescription.TabIndex = 1;
            this._tbDescription.Text = "OIB MES-Traceability Sync communication";
            // 
            // _tbProductName
            // 
            this._tbProductName.Location = new System.Drawing.Point(135, 26);
            this._tbProductName.Name = "_tbProductName";
            this._tbProductName.Size = new System.Drawing.Size(242, 20);
            this._tbProductName.TabIndex = 0;
            this._tbProductName.Text = "MES Simulator Test application";
            // 
            // _lbProduct
            // 
            this._lbProduct.AutoSize = true;
            this._lbProduct.Location = new System.Drawing.Point(6, 29);
            this._lbProduct.Name = "_lbProduct";
            this._lbProduct.Size = new System.Drawing.Size(78, 13);
            this._lbProduct.TabIndex = 19;
            this._lbProduct.Text = "Product Name:";
            // 
            // _oibServiceNameTb
            // 
            this._oibServiceNameTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._oibServiceNameTb.Enabled = false;
            this._oibServiceNameTb.Location = new System.Drawing.Point(135, 52);
            this._oibServiceNameTb.Name = "_oibServiceNameTb";
            this._oibServiceNameTb.ReadOnly = true;
            this._oibServiceNameTb.Size = new System.Drawing.Size(242, 20);
            this._oibServiceNameTb.TabIndex = 17;
            this._oibServiceNameTb.Text = "ASM.TraceabilityClient.CallbackService";
            // 
            // _lbService
            // 
            this._lbService.AutoSize = true;
            this._lbService.Location = new System.Drawing.Point(6, 55);
            this._lbService.Name = "_lbService";
            this._lbService.Size = new System.Drawing.Size(98, 13);
            this._lbService.TabIndex = 18;
            this._lbService.Text = "OIB Service Name:";
            // 
            // _gbResultValues
            // 
            this._gbResultValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gbResultValues.Controls.Add(this._tbBoardValidationReason);
            this._gbResultValues.Controls.Add(this._lbReason);
            this._gbResultValues.Controls.Add(this._rbError);
            this._gbResultValues.Controls.Add(this._lbResult);
            this._gbResultValues.Controls.Add(this._rbRejectedPass);
            this._gbResultValues.Controls.Add(this._rbRejectedHold);
            this._gbResultValues.Controls.Add(this._rbConfirmed);
            this._gbResultValues.Location = new System.Drawing.Point(13, 252);
            this._gbResultValues.Name = "_gbResultValues";
            this._gbResultValues.Size = new System.Drawing.Size(762, 103);
            this._gbResultValues.TabIndex = 23;
            this._gbResultValues.TabStop = false;
            this._gbResultValues.Text = "BoardProduced Result values";
            // 
            // _tbBoardValidationReason
            // 
            this._tbBoardValidationReason.Location = new System.Drawing.Point(156, 51);
            this._tbBoardValidationReason.Name = "_tbBoardValidationReason";
            this._tbBoardValidationReason.Size = new System.Drawing.Size(413, 20);
            this._tbBoardValidationReason.TabIndex = 5;
            this._tbBoardValidationReason.Text = "Everything is OK";
            this._tbBoardValidationReason.TextChanged += new System.EventHandler(this.BoardValidationReason_TextChanged);
            // 
            // _lbReason
            // 
            this._lbReason.AutoSize = true;
            this._lbReason.Location = new System.Drawing.Point(10, 51);
            this._lbReason.Name = "_lbReason";
            this._lbReason.Size = new System.Drawing.Size(121, 13);
            this._lbReason.TabIndex = 4;
            this._lbReason.Text = "BoardValidationReason:";
            // 
            // _rbError
            // 
            this._rbError.AutoSize = true;
            this._rbError.Location = new System.Drawing.Point(505, 19);
            this._rbError.Name = "_rbError";
            this._rbError.Size = new System.Drawing.Size(64, 17);
            this._rbError.TabIndex = 3;
            this._rbError.Text = "ERROR";
            this._rbError.UseVisualStyleBackColor = true;
            this._rbError.CheckedChanged += new System.EventHandler(this.RadioButtonsCheckedChanged);
            // 
            // _lbResult
            // 
            this._lbResult.AutoSize = true;
            this._lbResult.Location = new System.Drawing.Point(10, 21);
            this._lbResult.Name = "_lbResult";
            this._lbResult.Size = new System.Drawing.Size(114, 13);
            this._lbResult.TabIndex = 3;
            this._lbResult.Text = "BoardValidationResult:";
            // 
            // _rbRejectedPass
            // 
            this._rbRejectedPass.AutoSize = true;
            this._rbRejectedPass.Location = new System.Drawing.Point(384, 19);
            this._rbRejectedPass.Name = "_rbRejectedPass";
            this._rbRejectedPass.Size = new System.Drawing.Size(115, 17);
            this._rbRejectedPass.TabIndex = 2;
            this._rbRejectedPass.Text = "REJECTED_PASS";
            this._rbRejectedPass.UseVisualStyleBackColor = true;
            this._rbRejectedPass.CheckedChanged += new System.EventHandler(this.RadioButtonsCheckedChanged);
            // 
            // _rbRejectedHold
            // 
            this._rbRejectedHold.AutoSize = true;
            this._rbRejectedHold.Location = new System.Drawing.Point(251, 19);
            this._rbRejectedHold.Name = "_rbRejectedHold";
            this._rbRejectedHold.Size = new System.Drawing.Size(117, 17);
            this._rbRejectedHold.TabIndex = 1;
            this._rbRejectedHold.Text = "REJECTED_HOLD";
            this._rbRejectedHold.UseVisualStyleBackColor = true;
            this._rbRejectedHold.CheckedChanged += new System.EventHandler(this.RadioButtonsCheckedChanged);
            // 
            // _rbConfirmed
            // 
            this._rbConfirmed.AutoSize = true;
            this._rbConfirmed.Checked = true;
            this._rbConfirmed.Location = new System.Drawing.Point(156, 19);
            this._rbConfirmed.Name = "_rbConfirmed";
            this._rbConfirmed.Size = new System.Drawing.Size(89, 17);
            this._rbConfirmed.TabIndex = 0;
            this._rbConfirmed.TabStop = true;
            this._rbConfirmed.Text = "CONFIRMED";
            this._rbConfirmed.UseVisualStyleBackColor = true;
            this._rbConfirmed.CheckedChanged += new System.EventHandler(this.RadioButtonsCheckedChanged);
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(14, 86);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 20;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 575);
            this.Controls.Add(this._gbResultValues);
            this.Controls.Add(this._gbTrace);
            this.Controls.Add(this._listbMessages);
            this.Controls.Add(this._gbCore);
            this.Name = "MainForm";
            this.Text = "OIB ITraceabilityDataCallback Tutorial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this._gbCore.ResumeLayout(false);
            this._gbCore.PerformLayout();
            this._gbTrace.ResumeLayout(false);
            this._gbTrace.PerformLayout();
            this._gbResultValues.ResumeLayout(false);
            this._gbResultValues.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _gbCore;
        private System.Windows.Forms.TextBox _tbOIBCoreName;
        private System.Windows.Forms.Label _lbCore;
        private System.Windows.Forms.ListBox _listbMessages;
        private System.Windows.Forms.GroupBox _gbTrace;
        private System.Windows.Forms.TextBox _tbDescription;
        private System.Windows.Forms.TextBox _tbProductName;
        private System.Windows.Forms.Label _lbProduct;
        private System.Windows.Forms.TextBox _oibServiceNameTb;
        private System.Windows.Forms.Label _lbService;
        private System.Windows.Forms.Button _btnUnregister;
        private System.Windows.Forms.Button _btnRegister;
        private System.Windows.Forms.Button _btnStartService;
        private System.Windows.Forms.Button _btnStopService;
        private System.Windows.Forms.GroupBox _gbResultValues;
        private System.Windows.Forms.RadioButton _rbRejectedPass;
        private System.Windows.Forms.RadioButton _rbRejectedHold;
        private System.Windows.Forms.RadioButton _rbConfirmed;
        private System.Windows.Forms.Label _lbResult;
        private System.Windows.Forms.RadioButton _rbError;
        private System.Windows.Forms.Label _lbReason;
        private System.Windows.Forms.TextBox _tbBoardValidationReason;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}