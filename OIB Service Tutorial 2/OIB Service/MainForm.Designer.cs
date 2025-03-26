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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._ButtonSendEvent = new System.Windows.Forms.Button();
            this._TextBoxNotificationManager = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 87);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(543, 22);
            this.statusStrip1.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _ButtonSendEvent
            // 
            this._ButtonSendEvent.Location = new System.Drawing.Point(12, 55);
            this._ButtonSendEvent.Name = "_ButtonSendEvent";
            this._ButtonSendEvent.Size = new System.Drawing.Size(193, 23);
            this._ButtonSendEvent.TabIndex = 15;
            this._ButtonSendEvent.Text = "Send \'Hello World\' Event";
            this._ButtonSendEvent.UseVisualStyleBackColor = true;
            this._ButtonSendEvent.Click += new System.EventHandler(this.ButtonSendEvent_Click);
            // 
            // _TextBoxNotificationManager
            // 
            this._TextBoxNotificationManager.Location = new System.Drawing.Point(12, 29);
            this._TextBoxNotificationManager.Name = "_TextBoxNotificationManager";
            this._TextBoxNotificationManager.Size = new System.Drawing.Size(519, 20);
            this._TextBoxNotificationManager.TabIndex = 16;
            this._TextBoxNotificationManager.Text = "net.tcp://localhost:1406/Asm.As.Oib.WS.Eventing.Services/NotificationManagerSecure";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Notification Manager Endpoint";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(211, 59);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 18;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 109);
            this.Controls.Add(this._cbUseClientAuthentication);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._TextBoxNotificationManager);
            this.Controls.Add(this._ButtonSendEvent);
            this.Controls.Add(this.statusStrip1);
            this.Name = "mainForm";
            this.Text = "OIB Service Tutorial - Service";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
		private System.Windows.Forms.Button _ButtonSendEvent;
		private System.Windows.Forms.TextBox _TextBoxNotificationManager;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

