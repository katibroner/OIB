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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonTest3 = new System.Windows.Forms.Button();
            this.textBoxDiscoveryEndpoint = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelTestResult = new System.Windows.Forms.Label();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonTest1 = new System.Windows.Forms.Button();
            this.textBoxServiceLocatorHttpUri = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonTest2 = new System.Windows.Forms.Button();
            this.textBoxServiceLocatorHttpUriTest2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonTest3);
            this.groupBox3.Controls.Add(this.textBoxDiscoveryEndpoint);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 198);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(756, 87);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test 3: Accessing OIB Service Locator using Metadata Exchange";
            // 
            // buttonTest3
            // 
            this.buttonTest3.Location = new System.Drawing.Point(27, 54);
            this.buttonTest3.Name = "buttonTest3";
            this.buttonTest3.Size = new System.Drawing.Size(81, 23);
            this.buttonTest3.TabIndex = 8;
            this.buttonTest3.Text = "Test";
            this.buttonTest3.UseVisualStyleBackColor = true;
            this.buttonTest3.Click += new System.EventHandler(this.ButtonTest3_Click);
            // 
            // textBoxDiscoveryEndpoint
            // 
            this.textBoxDiscoveryEndpoint.Location = new System.Drawing.Point(266, 23);
            this.textBoxDiscoveryEndpoint.Name = "textBoxDiscoveryEndpoint";
            this.textBoxDiscoveryEndpoint.Size = new System.Drawing.Size(409, 20);
            this.textBoxDiscoveryEndpoint.TabIndex = 7;
            this.textBoxDiscoveryEndpoint.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/mex";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "OIB Service Locator Service MEX Address";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 601);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(780, 22);
            this.statusStrip.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // labelTestResult
            // 
            this.labelTestResult.AutoSize = true;
            this.labelTestResult.Location = new System.Drawing.Point(12, 323);
            this.labelTestResult.Name = "labelTestResult";
            this.labelTestResult.Size = new System.Drawing.Size(172, 13);
            this.labelTestResult.TabIndex = 9;
            this.labelTestResult.Text = "Test Result: MEX Endpoints in OIB";
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResult.Location = new System.Drawing.Point(15, 339);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.Size = new System.Drawing.Size(753, 259);
            this.dataGridViewResult.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonTest1);
            this.groupBox1.Controls.Add(this.textBoxServiceLocatorHttpUri);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 87);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test 1: Accessing OIB Service Locator by HTTP Address";
            // 
            // buttonTest1
            // 
            this.buttonTest1.Location = new System.Drawing.Point(27, 54);
            this.buttonTest1.Name = "buttonTest1";
            this.buttonTest1.Size = new System.Drawing.Size(81, 23);
            this.buttonTest1.TabIndex = 8;
            this.buttonTest1.Text = "Test";
            this.buttonTest1.UseVisualStyleBackColor = true;
            this.buttonTest1.Click += new System.EventHandler(this.ButtonTest1_Click);
            // 
            // textBoxServiceLocatorHttpUri
            // 
            this.textBoxServiceLocatorHttpUri.Location = new System.Drawing.Point(266, 23);
            this.textBoxServiceLocatorHttpUri.Name = "textBoxServiceLocatorHttpUri";
            this.textBoxServiceLocatorHttpUri.Size = new System.Drawing.Size(409, 20);
            this.textBoxServiceLocatorHttpUri.TabIndex = 7;
            this.textBoxServiceLocatorHttpUri.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/ReliableSecure";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "OIB Service Locator Service HTTP Address";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonTest2);
            this.groupBox2.Controls.Add(this.textBoxServiceLocatorHttpUriTest2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(756, 87);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test 2: Accessing OIB Service Locator by HTTP Address using ChannelFactory";
            // 
            // buttonTest2
            // 
            this.buttonTest2.Location = new System.Drawing.Point(27, 54);
            this.buttonTest2.Name = "buttonTest2";
            this.buttonTest2.Size = new System.Drawing.Size(81, 23);
            this.buttonTest2.TabIndex = 8;
            this.buttonTest2.Text = "Test";
            this.buttonTest2.UseVisualStyleBackColor = true;
            this.buttonTest2.Click += new System.EventHandler(this.ButtonTest2_Click);
            // 
            // textBoxServiceLocatorHttpUriTest2
            // 
            this.textBoxServiceLocatorHttpUriTest2.Location = new System.Drawing.Point(266, 23);
            this.textBoxServiceLocatorHttpUriTest2.Name = "textBoxServiceLocatorHttpUriTest2";
            this.textBoxServiceLocatorHttpUriTest2.Size = new System.Drawing.Size(409, 20);
            this.textBoxServiceLocatorHttpUriTest2.TabIndex = 7;
            this.textBoxServiceLocatorHttpUriTest2.Text = "http://localhost:1405/Asm.As.Oib.ServiceLocator/ReliableSecure";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "OIB Service Locator Service HTTP Address";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(15, 291);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(142, 17);
            this._cbUseClientAuthentication.TabIndex = 13;
            this._cbUseClientAuthentication.Text = "Use ClientAuthentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 623);
            this.Controls.Add(this._cbUseClientAuthentication);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewResult);
            this.Controls.Add(this.labelTestResult);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox3);
            this.Name = "MainForm";
            this.Text = "OIB Tutorial";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxDiscoveryEndpoint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonTest3;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.Label labelTestResult;
        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonTest1;
        private System.Windows.Forms.TextBox textBoxServiceLocatorHttpUri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonTest2;
        private System.Windows.Forms.TextBox textBoxServiceLocatorHttpUriTest2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

