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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._GroupBoxTest = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this._TextBoxJob = new System.Windows.Forms.TextBox();
            this._ButtonMessages = new System.Windows.Forms.Button();
            this._ButtonOptimizationParameters = new System.Windows.Forms.Button();
            this._ButtonCancel = new System.Windows.Forms.Button();
            this._ButtonStop = new System.Windows.Forms.Button();
            this._ButtonStart = new System.Windows.Forms.Button();
            this._ListBox = new System.Windows.Forms.ListBox();
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this._GroupBoxTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 575);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 7;
            // 
            // _ToolStripStatusLabel
            // 
            this._ToolStripStatusLabel.Name = "_ToolStripStatusLabel";
            this._ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _GroupBoxTest
            // 
            this._GroupBoxTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._GroupBoxTest.Controls.Add(this._cbUseClientAuthentication);
            this._GroupBoxTest.Controls.Add(this.label1);
            this._GroupBoxTest.Controls.Add(this._TextBoxJob);
            this._GroupBoxTest.Controls.Add(this._ButtonMessages);
            this._GroupBoxTest.Controls.Add(this._ButtonOptimizationParameters);
            this._GroupBoxTest.Controls.Add(this._ButtonCancel);
            this._GroupBoxTest.Controls.Add(this._ButtonStop);
            this._GroupBoxTest.Controls.Add(this._ButtonStart);
            this._GroupBoxTest.Location = new System.Drawing.Point(12, 12);
            this._GroupBoxTest.Name = "_GroupBoxTest";
            this._GroupBoxTest.Size = new System.Drawing.Size(859, 231);
            this._GroupBoxTest.TabIndex = 18;
            this._GroupBoxTest.TabStop = false;
            this._GroupBoxTest.Text = "Test: Connection Test";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "SIPLACE Pro Job Path";
            // 
            // _TextBoxJob
            // 
            this._TextBoxJob.Location = new System.Drawing.Point(243, 22);
            this._TextBoxJob.Name = "_TextBoxJob";
            this._TextBoxJob.Size = new System.Drawing.Size(493, 20);
            this._TextBoxJob.TabIndex = 36;
            this._TextBoxJob.Text = "OIB-SC-Tutorials\\Job 1";
            // 
            // _ButtonMessages
            // 
            this._ButtonMessages.Location = new System.Drawing.Point(25, 194);
            this._ButtonMessages.Name = "_ButtonMessages";
            this._ButtonMessages.Size = new System.Drawing.Size(188, 23);
            this._ButtonMessages.TabIndex = 35;
            this._ButtonMessages.Text = "Optimization Messages";
            this._ButtonMessages.UseVisualStyleBackColor = true;
            this._ButtonMessages.Click += new System.EventHandler(this.ButtonMessages_Click);
            // 
            // _ButtonOptimizationParameters
            // 
            this._ButtonOptimizationParameters.Location = new System.Drawing.Point(25, 78);
            this._ButtonOptimizationParameters.Name = "_ButtonOptimizationParameters";
            this._ButtonOptimizationParameters.Size = new System.Drawing.Size(188, 23);
            this._ButtonOptimizationParameters.TabIndex = 34;
            this._ButtonOptimizationParameters.Text = "Optimization Parameters...";
            this._ButtonOptimizationParameters.UseVisualStyleBackColor = true;
            this._ButtonOptimizationParameters.Click += new System.EventHandler(this.ButtonOptimizationParameters_Click);
            // 
            // _ButtonCancel
            // 
            this._ButtonCancel.Enabled = false;
            this._ButtonCancel.Location = new System.Drawing.Point(25, 165);
            this._ButtonCancel.Name = "_ButtonCancel";
            this._ButtonCancel.Size = new System.Drawing.Size(188, 23);
            this._ButtonCancel.TabIndex = 32;
            this._ButtonCancel.Text = "Cancel";
            this._ButtonCancel.UseVisualStyleBackColor = true;
            this._ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // _ButtonStop
            // 
            this._ButtonStop.Enabled = false;
            this._ButtonStop.Location = new System.Drawing.Point(25, 136);
            this._ButtonStop.Name = "_ButtonStop";
            this._ButtonStop.Size = new System.Drawing.Size(188, 23);
            this._ButtonStop.TabIndex = 31;
            this._ButtonStop.Text = "Stop";
            this._ButtonStop.UseVisualStyleBackColor = true;
            this._ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // _ButtonStart
            // 
            this._ButtonStart.Location = new System.Drawing.Point(25, 107);
            this._ButtonStart.Name = "_ButtonStart";
            this._ButtonStart.Size = new System.Drawing.Size(188, 23);
            this._ButtonStart.TabIndex = 30;
            this._ButtonStart.Text = "Start";
            this._ButtonStart.UseVisualStyleBackColor = true;
            this._ButtonStart.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // _ListBox
            // 
            this._ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ListBox.FormattingEnabled = true;
            this._ListBox.Location = new System.Drawing.Point(12, 249);
            this._ListBox.Name = "_ListBox";
            this._ListBox.Size = new System.Drawing.Size(859, 316);
            this._ListBox.TabIndex = 19;
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(25, 55);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(145, 17);
            this._cbUseClientAuthentication.TabIndex = 38;
            this._cbUseClientAuthentication.Text = "Use Client Authentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 597);
            this.Controls.Add(this._ListBox);
            this.Controls.Add(this._GroupBoxTest);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "OIB Tutorial";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this._GroupBoxTest.ResumeLayout(false);
            this._GroupBoxTest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _ToolStripStatusLabel;
        private System.Windows.Forms.GroupBox _GroupBoxTest;
        private System.Windows.Forms.Button _ButtonCancel;
        private System.Windows.Forms.Button _ButtonStop;
        private System.Windows.Forms.Button _ButtonStart;
        private System.Windows.Forms.Button _ButtonMessages;
        private System.Windows.Forms.Button _ButtonOptimizationParameters;
        private System.Windows.Forms.ListBox _ListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _TextBoxJob;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

