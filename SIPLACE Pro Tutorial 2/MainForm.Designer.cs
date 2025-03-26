namespace OIB.Tutorial
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
            this._GroupBoxTest = new System.Windows.Forms.GroupBox();
            this._ButtonConnect = new System.Windows.Forms.Button();
            this._ButtonDelete = new System.Windows.Forms.Button();
            this._ButtonUpdate = new System.Windows.Forms.Button();
            this._ButtonRead = new System.Windows.Forms.Button();
            this._ButtonCreate = new System.Windows.Forms.Button();
            this._LabelServiceLocatorMexCaption = new System.Windows.Forms.Label();
            this._TextBoxSiplaceProAddress = new System.Windows.Forms.TextBox();
            this._GroupBoxConfiguration = new System.Windows.Forms.GroupBox();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._GroupBoxTest.SuspendLayout();
            this._GroupBoxConfiguration.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _GroupBoxTest
            // 
            this._GroupBoxTest.Controls.Add(this._ButtonConnect);
            this._GroupBoxTest.Controls.Add(this._ButtonDelete);
            this._GroupBoxTest.Controls.Add(this._ButtonUpdate);
            this._GroupBoxTest.Controls.Add(this._ButtonRead);
            this._GroupBoxTest.Controls.Add(this._ButtonCreate);
            this._GroupBoxTest.Location = new System.Drawing.Point(14, 94);
            this._GroupBoxTest.Name = "_GroupBoxTest";
            this._GroupBoxTest.Size = new System.Drawing.Size(756, 185);
            this._GroupBoxTest.TabIndex = 18;
            this._GroupBoxTest.TabStop = false;
            this._GroupBoxTest.Text = "Test: Connection Test";
            // 
            // _ButtonConnect
            // 
            this._ButtonConnect.Location = new System.Drawing.Point(25, 19);
            this._ButtonConnect.Name = "_ButtonConnect";
            this._ButtonConnect.Size = new System.Drawing.Size(188, 23);
            this._ButtonConnect.TabIndex = 33;
            this._ButtonConnect.Text = "Connect SIPLACE Pro";
            this._ButtonConnect.UseVisualStyleBackColor = true;
            this._ButtonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // _ButtonDelete
            // 
            this._ButtonDelete.Location = new System.Drawing.Point(25, 145);
            this._ButtonDelete.Name = "_ButtonDelete";
            this._ButtonDelete.Size = new System.Drawing.Size(188, 23);
            this._ButtonDelete.TabIndex = 32;
            this._ButtonDelete.Text = "Delete Component";
            this._ButtonDelete.UseVisualStyleBackColor = true;
            this._ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // _ButtonUpdate
            // 
            this._ButtonUpdate.Location = new System.Drawing.Point(25, 116);
            this._ButtonUpdate.Name = "_ButtonUpdate";
            this._ButtonUpdate.Size = new System.Drawing.Size(188, 23);
            this._ButtonUpdate.TabIndex = 31;
            this._ButtonUpdate.Text = "Update Component";
            this._ButtonUpdate.UseVisualStyleBackColor = true;
            this._ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // _ButtonRead
            // 
            this._ButtonRead.Location = new System.Drawing.Point(25, 87);
            this._ButtonRead.Name = "_ButtonRead";
            this._ButtonRead.Size = new System.Drawing.Size(188, 23);
            this._ButtonRead.TabIndex = 30;
            this._ButtonRead.Text = "Read Component";
            this._ButtonRead.UseVisualStyleBackColor = true;
            this._ButtonRead.Click += new System.EventHandler(this.ReadButton_Click);
            // 
            // _ButtonCreate
            // 
            this._ButtonCreate.Location = new System.Drawing.Point(25, 58);
            this._ButtonCreate.Name = "_ButtonCreate";
            this._ButtonCreate.Size = new System.Drawing.Size(188, 23);
            this._ButtonCreate.TabIndex = 29;
            this._ButtonCreate.Text = "Create Component";
            this._ButtonCreate.UseVisualStyleBackColor = true;
            this._ButtonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // _LabelServiceLocatorMexCaption
            // 
            this._LabelServiceLocatorMexCaption.AutoSize = true;
            this._LabelServiceLocatorMexCaption.Location = new System.Drawing.Point(24, 26);
            this._LabelServiceLocatorMexCaption.Name = "_LabelServiceLocatorMexCaption";
            this._LabelServiceLocatorMexCaption.Size = new System.Drawing.Size(151, 13);
            this._LabelServiceLocatorMexCaption.TabIndex = 6;
            this._LabelServiceLocatorMexCaption.Text = "SIPLACE Pro Adapter Address";
            // 
            // _TextBoxSiplaceProAddress
            // 
            this._TextBoxSiplaceProAddress.Location = new System.Drawing.Point(245, 23);
            this._TextBoxSiplaceProAddress.Name = "_TextBoxSiplaceProAddress";
            this._TextBoxSiplaceProAddress.Size = new System.Drawing.Size(493, 20);
            this._TextBoxSiplaceProAddress.TabIndex = 7;
            this._TextBoxSiplaceProAddress.Text = "net.tcp://localhost:9500/Asm.As.Oib.SiplacePro/Secure";
            // 
            // _GroupBoxConfiguration
            // 
            this._GroupBoxConfiguration.Controls.Add(this._cbUseClientAuthentication);
            this._GroupBoxConfiguration.Controls.Add(this._TextBoxSiplaceProAddress);
            this._GroupBoxConfiguration.Controls.Add(this._LabelServiceLocatorMexCaption);
            this._GroupBoxConfiguration.Location = new System.Drawing.Point(12, 12);
            this._GroupBoxConfiguration.Name = "_GroupBoxConfiguration";
            this._GroupBoxConfiguration.Size = new System.Drawing.Size(756, 73);
            this._GroupBoxConfiguration.TabIndex = 17;
            this._GroupBoxConfiguration.TabStop = false;
            this._GroupBoxConfiguration.Text = "Configuration";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(27, 52);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 8;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 292);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(782, 22);
            this._StatusStrip.TabIndex = 19;
            this._StatusStrip.Text = "statusStrip1";
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // mainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 314);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._GroupBoxTest);
            this.Controls.Add(this._GroupBoxConfiguration);
            this.Name = "mainForm";
            this.Text = "OIB Tutorial";
            this._GroupBoxTest.ResumeLayout(false);
            this._GroupBoxConfiguration.ResumeLayout(false);
            this._GroupBoxConfiguration.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _GroupBoxTest;
        private System.Windows.Forms.Button _ButtonCreate;
        private System.Windows.Forms.Label _LabelServiceLocatorMexCaption;
        private System.Windows.Forms.TextBox _TextBoxSiplaceProAddress;
        private System.Windows.Forms.GroupBox _GroupBoxConfiguration;
        private System.Windows.Forms.Button _ButtonDelete;
        private System.Windows.Forms.Button _ButtonUpdate;
        private System.Windows.Forms.Button _ButtonRead;
        private System.Windows.Forms.Button _ButtonConnect;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

