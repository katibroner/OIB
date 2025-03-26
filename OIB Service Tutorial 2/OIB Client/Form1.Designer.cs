namespace OIB_Client
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
            this._ButtonSubscribe = new System.Windows.Forms.Button();
            this._ButtonUnSubscribe = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._TextBoxSubscriptionManager = new System.Windows.Forms.TextBox();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _ButtonSubscribe
            // 
            this._ButtonSubscribe.Location = new System.Drawing.Point(15, 51);
            this._ButtonSubscribe.Name = "_ButtonSubscribe";
            this._ButtonSubscribe.Size = new System.Drawing.Size(281, 23);
            this._ButtonSubscribe.TabIndex = 6;
            this._ButtonSubscribe.Text = "Subscribe for messages";
            this._ButtonSubscribe.UseVisualStyleBackColor = true;
            this._ButtonSubscribe.Click += new System.EventHandler(this.ButtonSubscribe_Click);
            // 
            // _ButtonUnSubscribe
            // 
            this._ButtonUnSubscribe.Location = new System.Drawing.Point(15, 80);
            this._ButtonUnSubscribe.Name = "_ButtonUnSubscribe";
            this._ButtonUnSubscribe.Size = new System.Drawing.Size(281, 23);
            this._ButtonUnSubscribe.TabIndex = 7;
            this._ButtonUnSubscribe.Text = "UnSubscribe for messages";
            this._ButtonUnSubscribe.UseVisualStyleBackColor = true;
            this._ButtonUnSubscribe.Click += new System.EventHandler(this._ButtonUnSubscribe_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Subscription Manager Endpoint";
            // 
            // _TextBoxSubscriptionManager
            // 
            this._TextBoxSubscriptionManager.Location = new System.Drawing.Point(15, 25);
            this._TextBoxSubscriptionManager.Name = "_TextBoxSubscriptionManager";
            this._TextBoxSubscriptionManager.Size = new System.Drawing.Size(521, 20);
            this._TextBoxSubscriptionManager.TabIndex = 8;
            this._TextBoxSubscriptionManager.Text = "http://localhost:1405/Asm.As.Oib.WS.Eventing.Services/SubscriptionManager/Reliabl" +
    "eSecure";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(302, 55);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 16;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 115);
            this.Controls.Add(this._cbUseClientAuthentication);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._TextBoxSubscriptionManager);
            this.Controls.Add(this._ButtonUnSubscribe);
            this.Controls.Add(this._ButtonSubscribe);
            this.Name = "Form1";
            this.Text = "OIB Service Tutorial - Client";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _ButtonSubscribe;
		private System.Windows.Forms.Button _ButtonUnSubscribe;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _TextBoxSubscriptionManager;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

