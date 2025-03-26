namespace LineControlOibMesInterceptorTutorial
{
	partial class MesSimForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this._blockCallSecComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearTrace = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbVerificationResult_Reason = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbVerificationResult_Continue = new System.Windows.Forms.CheckBox();
            this.cbInitializedPingOK = new System.Windows.Forms.CheckBox();
            this.tbTrace = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this._tbOIBCoreComputer = new System.Windows.Forms.TextBox();
            this._btnStartCallback = new System.Windows.Forms.Button();
            this._btnStopCallback = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.groupBox2);
            this.panelTop.Controls.Add(this._blockCallSecComboBox);
            this.panelTop.Controls.Add(this.label5);
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.btnClearTrace);
            this.panelTop.Controls.Add(this.groupBox1);
            this.panelTop.Controls.Add(this.cbInitializedPingOK);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(587, 156);
            this.panelTop.TabIndex = 0;
            // 
            // _blockCallSecComboBox
            // 
            this._blockCallSecComboBox.FormattingEnabled = true;
            this._blockCallSecComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "5",
            "10",
            "30",
            "60",
            "70",
            "90",
            "120",
            "300",
            "600",
            "1200",
            "3600"});
            this._blockCallSecComboBox.Location = new System.Drawing.Point(68, 107);
            this._blockCallSecComboBox.Name = "_blockCallSecComboBox";
            this._blockCallSecComboBox.Size = new System.Drawing.Size(60, 21);
            this._blockCallSecComboBox.TabIndex = 5;
            this._blockCallSecComboBox.SelectedIndexChanged += new System.EventHandler(this.BlockCallSecComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "sec ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Block for ";
            // 
            // btnClearTrace
            // 
            this.btnClearTrace.Location = new System.Drawing.Point(16, 131);
            this.btnClearTrace.Name = "btnClearTrace";
            this.btnClearTrace.Size = new System.Drawing.Size(98, 19);
            this.btnClearTrace.TabIndex = 0;
            this.btnClearTrace.Text = "Clear Messages";
            this.btnClearTrace.UseVisualStyleBackColor = true;
            this.btnClearTrace.Click += new System.EventHandler(this.btnClearTrace_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbVerificationResult_Reason);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbVerificationResult_Continue);
            this.groupBox1.Location = new System.Drawing.Point(173, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verification Result";
            // 
            // cbVerificationResult_Reason
            // 
            this.cbVerificationResult_Reason.FormattingEnabled = true;
            this.cbVerificationResult_Reason.Items.AddRange(new object[] {
            "",
            "Wrong Recipe",
            "Wrong Conveyor Lane",
            "Wrong PCB Barcode",
            "Wrong Printer file",
            "<return null for verification result>"});
            this.cbVerificationResult_Reason.Location = new System.Drawing.Point(60, 41);
            this.cbVerificationResult_Reason.Name = "cbVerificationResult_Reason";
            this.cbVerificationResult_Reason.Size = new System.Drawing.Size(333, 21);
            this.cbVerificationResult_Reason.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Result:";
            // 
            // cbVerificationResult_Continue
            // 
            this.cbVerificationResult_Continue.AutoSize = true;
            this.cbVerificationResult_Continue.Checked = true;
            this.cbVerificationResult_Continue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVerificationResult_Continue.Location = new System.Drawing.Point(17, 19);
            this.cbVerificationResult_Continue.Name = "cbVerificationResult_Continue";
            this.cbVerificationResult_Continue.Size = new System.Drawing.Size(357, 17);
            this.cbVerificationResult_Continue.TabIndex = 0;
            this.cbVerificationResult_Continue.Text = "OK (LineControl-RecipeDownload or RecipeActivation-on-DEK-Printer)";
            this.cbVerificationResult_Continue.UseVisualStyleBackColor = true;
            this.cbVerificationResult_Continue.CheckedChanged += new System.EventHandler(this.cbVerificationResult_Continue_CheckedChanged);
            // 
            // cbInitializedPingOK
            // 
            this.cbInitializedPingOK.AutoSize = true;
            this.cbInitializedPingOK.Checked = true;
            this.cbInitializedPingOK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInitializedPingOK.Location = new System.Drawing.Point(15, 89);
            this.cbInitializedPingOK.Name = "cbInitializedPingOK";
            this.cbInitializedPingOK.Size = new System.Drawing.Size(152, 17);
            this.cbInitializedPingOK.TabIndex = 0;
            this.cbInitializedPingOK.Text = "Initialized (Ping returns OK)";
            this.cbInitializedPingOK.UseVisualStyleBackColor = true;
            // 
            // tbTrace
            // 
            this.tbTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTrace.Location = new System.Drawing.Point(0, 156);
            this.tbTrace.Multiline = true;
            this.tbTrace.Name = "tbTrace";
            this.tbTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbTrace.Size = new System.Drawing.Size(587, 259);
            this.tbTrace.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._btnStopCallback);
            this.groupBox2.Controls.Add(this._btnStartCallback);
            this.groupBox2.Controls.Add(this._tbOIBCoreComputer);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(562, 61);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OIB ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Core computer name:";
            // 
            // _tbOIBCoreComputer
            // 
            this._tbOIBCoreComputer.Location = new System.Drawing.Point(10, 32);
            this._tbOIBCoreComputer.Name = "_tbOIBCoreComputer";
            this._tbOIBCoreComputer.Size = new System.Drawing.Size(100, 20);
            this._tbOIBCoreComputer.TabIndex = 1;
            // 
            // _btnStartCallback
            // 
            this._btnStartCallback.Location = new System.Drawing.Point(160, 11);
            this._btnStartCallback.Name = "_btnStartCallback";
            this._btnStartCallback.Size = new System.Drawing.Size(111, 41);
            this._btnStartCallback.TabIndex = 2;
            this._btnStartCallback.Text = "Start Callback and Register";
            this._btnStartCallback.UseVisualStyleBackColor = true;
            this._btnStartCallback.Click += new System.EventHandler(this._btnStartCallback_Click);
            // 
            // _btnStopCallback
            // 
            this._btnStopCallback.Enabled = false;
            this._btnStopCallback.Location = new System.Drawing.Point(286, 11);
            this._btnStopCallback.Name = "_btnStopCallback";
            this._btnStopCallback.Size = new System.Drawing.Size(117, 41);
            this._btnStopCallback.TabIndex = 3;
            this._btnStopCallback.Text = "Stop Callback and Unregister";
            this._btnStopCallback.UseVisualStyleBackColor = true;
            this._btnStopCallback.Click += new System.EventHandler(this._btnStopCallback_Click);
            // 
            // MesSimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 415);
            this.Controls.Add(this.tbTrace);
            this.Controls.Add(this.panelTop);
            this.Name = "MesSimForm";
            this.Text = "LineControlDownloadInterceptor MES Simulator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MesSimFor_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.TextBox tbTrace;
		private System.Windows.Forms.Button btnClearTrace;
		private System.Windows.Forms.CheckBox cbInitializedPingOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox cbVerificationResult_Continue;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbVerificationResult_Reason;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox _blockCallSecComboBox;
        private System.Windows.Forms.Button _btnStopCallback;
        private System.Windows.Forms.Button _btnStartCallback;
        private System.Windows.Forms.TextBox _tbOIBCoreComputer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

