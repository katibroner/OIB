namespace OIB.Tutorial
{
    partial class ComponentStatusForm
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
            this.dataGridViewComponentStatus = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComponentStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewComponentStatus
            // 
            this.dataGridViewComponentStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComponentStatus.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewComponentStatus.Name = "dataGridViewComponentStatus";
            this.dataGridViewComponentStatus.Size = new System.Drawing.Size(776, 426);
            this.dataGridViewComponentStatus.TabIndex = 0;
            // 
            // ComponentStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewComponentStatus);
            this.Name = "ComponentStatusForm";
            this.Text = "ComponentStatusForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComponentStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewComponentStatus;
    }
}