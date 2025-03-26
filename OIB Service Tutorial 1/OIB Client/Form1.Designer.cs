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
			this._ButtonDirectCall = new System.Windows.Forms.Button();
			this._ButtonCallViaServiceLocator = new System.Windows.Forms.Button();
			this._TextBoxServiceLocatorEndpoint = new System.Windows.Forms.TextBox();
			this.label = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._TextBoxDirectEndpointAddress = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// _ButtonDirectCall
			// 
			this._ButtonDirectCall.Location = new System.Drawing.Point(12, 51);
			this._ButtonDirectCall.Name = "_ButtonDirectCall";
			this._ButtonDirectCall.Size = new System.Drawing.Size(281, 23);
			this._ButtonDirectCall.TabIndex = 0;
			this._ButtonDirectCall.Text = "Call Service Directly";
			this._ButtonDirectCall.UseVisualStyleBackColor = true;
			this._ButtonDirectCall.Click += new System.EventHandler(this.ButtonDirectCall_Click);
			// 
			// _ButtonCallViaServiceLocator
			// 
			this._ButtonCallViaServiceLocator.Location = new System.Drawing.Point(12, 145);
			this._ButtonCallViaServiceLocator.Name = "_ButtonCallViaServiceLocator";
			this._ButtonCallViaServiceLocator.Size = new System.Drawing.Size(281, 23);
			this._ButtonCallViaServiceLocator.TabIndex = 1;
			this._ButtonCallViaServiceLocator.Text = "Call Service Via OIB Discovery";
			this._ButtonCallViaServiceLocator.UseVisualStyleBackColor = true;
			this._ButtonCallViaServiceLocator.Click += new System.EventHandler(this.ButtonCallViaServiceLocator_Click);
			// 
			// _TextBoxServiceLocatorEndpoint
			// 
			this._TextBoxServiceLocatorEndpoint.Location = new System.Drawing.Point(12, 119);
			this._TextBoxServiceLocatorEndpoint.Name = "_TextBoxServiceLocatorEndpoint";
			this._TextBoxServiceLocatorEndpoint.Size = new System.Drawing.Size(457, 20);
			this._TextBoxServiceLocatorEndpoint.TabIndex = 2;
            this._TextBoxServiceLocatorEndpoint.Text = "http://localhost:1405/Asm.As.Oib.servicelocator/Reliable";
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.Location = new System.Drawing.Point(9, 103);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(127, 13);
			this.label.TabIndex = 3;
			this.label.Text = "Service Locator Endpoint";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(146, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Hello World Service Endpoint";
			// 
			// _TextBoxDirectEndpointAddress
			// 
			this._TextBoxDirectEndpointAddress.Location = new System.Drawing.Point(12, 25);
			this._TextBoxDirectEndpointAddress.Name = "_TextBoxDirectEndpointAddress";
			this._TextBoxDirectEndpointAddress.Size = new System.Drawing.Size(457, 20);
			this._TextBoxDirectEndpointAddress.TabIndex = 4;
			this._TextBoxDirectEndpointAddress.Text = "http://localhost:1235/MyCompany.Serivces/HelloWorld";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 193);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._TextBoxDirectEndpointAddress);
			this.Controls.Add(this.label);
			this.Controls.Add(this._TextBoxServiceLocatorEndpoint);
			this.Controls.Add(this._ButtonCallViaServiceLocator);
			this.Controls.Add(this._ButtonDirectCall);
			this.Name = "Form1";
			this.Text = "OIB Service Tutorial - Client";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _ButtonDirectCall;
		private System.Windows.Forms.Button _ButtonCallViaServiceLocator;
		private System.Windows.Forms.TextBox _TextBoxServiceLocatorEndpoint;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _TextBoxDirectEndpointAddress;
	}
}

