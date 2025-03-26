namespace OIB.Tutorial.Service
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
            this._ssServiceState = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._ButtonRegister = new System.Windows.Forms.Button();
            this._ButtonStartService = new System.Windows.Forms.Button();
            this._ButtonStopService = new System.Windows.Forms.Button();
            this._ButtonUnregister = new System.Windows.Forms.Button();
            this._TextBoxServiceLocatorEndpoint = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this._ssServiceState.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ssServiceState
            // 
            this._ssServiceState.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this._ssServiceState.Location = new System.Drawing.Point(0, 212);
            this._ssServiceState.Name = "_ssServiceState";
            this._ssServiceState.Size = new System.Drawing.Size(483, 22);
            this._ssServiceState.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _ButtonRegister
            // 
            this._ButtonRegister.Location = new System.Drawing.Point(15, 141);
            this._ButtonRegister.Name = "_ButtonRegister";
            this._ButtonRegister.Size = new System.Drawing.Size(176, 23);
            this._ButtonRegister.TabIndex = 9;
            this._ButtonRegister.Text = "Register Service at OIB";
            this._ButtonRegister.UseVisualStyleBackColor = true;
            this._ButtonRegister.Click += new System.EventHandler(this.ButtonRegister_Click);
            // 
            // _ButtonStartService
            // 
            this._ButtonStartService.Location = new System.Drawing.Point(15, 12);
            this._ButtonStartService.Name = "_ButtonStartService";
            this._ButtonStartService.Size = new System.Drawing.Size(176, 23);
            this._ButtonStartService.TabIndex = 10;
            this._ButtonStartService.Text = "Start Service";
            this._ButtonStartService.UseVisualStyleBackColor = true;
            this._ButtonStartService.Click += new System.EventHandler(this.ButtonStartService_Click);
            // 
            // _ButtonStopService
            // 
            this._ButtonStopService.Location = new System.Drawing.Point(15, 41);
            this._ButtonStopService.Name = "_ButtonStopService";
            this._ButtonStopService.Size = new System.Drawing.Size(176, 23);
            this._ButtonStopService.TabIndex = 11;
            this._ButtonStopService.Text = "Stop Service";
            this._ButtonStopService.UseVisualStyleBackColor = true;
            this._ButtonStopService.Click += new System.EventHandler(this.ButtonStopService_Click);
            // 
            // _ButtonUnregister
            // 
            this._ButtonUnregister.Location = new System.Drawing.Point(15, 170);
            this._ButtonUnregister.Name = "_ButtonUnregister";
            this._ButtonUnregister.Size = new System.Drawing.Size(176, 23);
            this._ButtonUnregister.TabIndex = 12;
            this._ButtonUnregister.Text = "Unregister Service at OIB";
            this._ButtonUnregister.UseVisualStyleBackColor = true;
            this._ButtonUnregister.Click += new System.EventHandler(this.ButtonUnregister_Click);
            // 
            // _TextBoxServiceLocatorEndpoint
            // 
            this._TextBoxServiceLocatorEndpoint.Location = new System.Drawing.Point(12, 108);
            this._TextBoxServiceLocatorEndpoint.Name = "_TextBoxServiceLocatorEndpoint";
            this._TextBoxServiceLocatorEndpoint.Size = new System.Drawing.Size(457, 20);
            this._TextBoxServiceLocatorEndpoint.TabIndex = 13;
            this._TextBoxServiceLocatorEndpoint.Text = "http://localhost:1405/Asm.As.Oib.servicelocator/ReliableSecure";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(12, 92);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(127, 13);
            this.label.TabIndex = 14;
            this.label.Text = "Service Locator Endpoint";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(226, 145);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 15;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 234);
            this.Controls.Add(this._cbUseClientAuthentication);
            this.Controls.Add(this.label);
            this.Controls.Add(this._TextBoxServiceLocatorEndpoint);
            this.Controls.Add(this._ButtonUnregister);
            this.Controls.Add(this._ButtonStopService);
            this.Controls.Add(this._ButtonStartService);
            this.Controls.Add(this._ButtonRegister);
            this.Controls.Add(this._ssServiceState);
            this.Name = "mainForm";
            this.Text = "OIB Service Tutorial - Service";
            this._ssServiceState.ResumeLayout(false);
            this._ssServiceState.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip _ssServiceState;
		private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
		private System.Windows.Forms.Button _ButtonRegister;
		private System.Windows.Forms.Button _ButtonStartService;
		private System.Windows.Forms.Button _ButtonStopService;
		private System.Windows.Forms.Button _ButtonUnregister;
		private System.Windows.Forms.TextBox _TextBoxServiceLocatorEndpoint;
		private System.Windows.Forms.Label label;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

