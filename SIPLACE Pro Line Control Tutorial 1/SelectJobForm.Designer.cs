namespace OIB.Tutorial
{
    partial class SelectJobForm
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
            this._ButtonOk = new System.Windows.Forms.Button();
            this._ButtonCancel = new System.Windows.Forms.Button();
            this._TextBoxJobFullName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _ButtonOk
            // 
            this._ButtonOk.Location = new System.Drawing.Point(12, 38);
            this._ButtonOk.Name = "_ButtonOk";
            this._ButtonOk.Size = new System.Drawing.Size(75, 23);
            this._ButtonOk.TabIndex = 0;
            this._ButtonOk.Text = "Ok";
            this._ButtonOk.UseVisualStyleBackColor = true;
            this._ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // _ButtonCancel
            // 
            this._ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._ButtonCancel.Location = new System.Drawing.Point(93, 38);
            this._ButtonCancel.Name = "_ButtonCancel";
            this._ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this._ButtonCancel.TabIndex = 1;
            this._ButtonCancel.Text = "Cancel";
            this._ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // _TextBoxJobFullName
            // 
            this._TextBoxJobFullName.Location = new System.Drawing.Point(12, 12);
            this._TextBoxJobFullName.Name = "_TextBoxJobFullName";
            this._TextBoxJobFullName.Size = new System.Drawing.Size(286, 20);
            this._TextBoxJobFullName.TabIndex = 2;
            this._TextBoxJobFullName.Text = "OIB-SC-Tutorials\\Job 1";
            // 
            // SelectJobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 69);
            this.ControlBox = false;
            this.Controls.Add(this._TextBoxJobFullName);
            this.Controls.Add(this._ButtonCancel);
            this.Controls.Add(this._ButtonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SelectJobForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Job Full Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _ButtonOk;
        private System.Windows.Forms.Button _ButtonCancel;
        private System.Windows.Forms.TextBox _TextBoxJobFullName;
    }
}