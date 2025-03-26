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
            this._cbUseClientAuthentication = new System.Windows.Forms.CheckBox();
            this._TextBoxOptimizerEndpoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._TextBoxRecipe = new System.Windows.Forms.TextBox();
            this._ButtonStart = new System.Windows.Forms.Button();
            this._ListBox = new System.Windows.Forms.ListBox();
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
            this._GroupBoxTest.Controls.Add(this._TextBoxOptimizerEndpoint);
            this._GroupBoxTest.Controls.Add(this.label2);
            this._GroupBoxTest.Controls.Add(this.label1);
            this._GroupBoxTest.Controls.Add(this._TextBoxRecipe);
            this._GroupBoxTest.Controls.Add(this._ButtonStart);
            this._GroupBoxTest.Location = new System.Drawing.Point(12, 12);
            this._GroupBoxTest.Name = "_GroupBoxTest";
            this._GroupBoxTest.Size = new System.Drawing.Size(859, 118);
            this._GroupBoxTest.TabIndex = 18;
            this._GroupBoxTest.TabStop = false;
            this._GroupBoxTest.Text = "Integrity Check";
            // 
            // _cbUseClientAuthentication
            // 
            this._cbUseClientAuthentication.AutoSize = true;
            this._cbUseClientAuthentication.Location = new System.Drawing.Point(160, 80);
            this._cbUseClientAuthentication.Name = "_cbUseClientAuthentication";
            this._cbUseClientAuthentication.Size = new System.Drawing.Size(142, 17);
            this._cbUseClientAuthentication.TabIndex = 40;
            this._cbUseClientAuthentication.Text = "Use ClientAuthentication";
            this._cbUseClientAuthentication.UseVisualStyleBackColor = true;
            // 
            // _TextBoxOptimizerEndpoint
            // 
            this._TextBoxOptimizerEndpoint.Location = new System.Drawing.Point(160, 48);
            this._TextBoxOptimizerEndpoint.Name = "_TextBoxOptimizerEndpoint";
            this._TextBoxOptimizerEndpoint.Size = new System.Drawing.Size(493, 20);
            this._TextBoxOptimizerEndpoint.TabIndex = 39;
            this._TextBoxOptimizerEndpoint.Text = "net.tcp://localhost:1406/Asm.As.Oib.SiplacePro.Optimizer/Secure";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Optimizer endpoint";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "SIPLACE Pro Recipe Path";
            // 
            // _TextBoxRecipe
            // 
            this._TextBoxRecipe.Location = new System.Drawing.Point(160, 22);
            this._TextBoxRecipe.Name = "_TextBoxRecipe";
            this._TextBoxRecipe.Size = new System.Drawing.Size(493, 20);
            this._TextBoxRecipe.TabIndex = 36;
            this._TextBoxRecipe.Text = "OIB-SC-Tutorials\\Recipe1_Board 1";
            // 
            // _ButtonStart
            // 
            this._ButtonStart.Location = new System.Drawing.Point(25, 77);
            this._ButtonStart.Name = "_ButtonStart";
            this._ButtonStart.Size = new System.Drawing.Size(129, 23);
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
            this._ListBox.HorizontalScrollbar = true;
            this._ListBox.Location = new System.Drawing.Point(12, 136);
            this._ListBox.Name = "_ListBox";
            this._ListBox.Size = new System.Drawing.Size(859, 420);
            this._ListBox.TabIndex = 19;
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
        private System.Windows.Forms.Button _ButtonStart;
        private System.Windows.Forms.ListBox _ListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _TextBoxRecipe;
        private System.Windows.Forms.TextBox _TextBoxOptimizerEndpoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox _cbUseClientAuthentication;
    }
}

