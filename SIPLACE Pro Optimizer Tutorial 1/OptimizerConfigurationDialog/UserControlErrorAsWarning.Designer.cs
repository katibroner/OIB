namespace OIB.Tutorial
{
    partial class UserControlErrorAsWarning
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlErrorAsWarning));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewWarnings = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_helpProvider = new System.Windows.Forms.HelpProvider();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarnings)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewWarnings
            // 
            this.dataGridViewWarnings.AllowUserToAddRows = false;
            this.dataGridViewWarnings.AllowUserToDeleteRows = false;
            this.dataGridViewWarnings.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridViewWarnings, "dataGridViewWarnings");
            this.dataGridViewWarnings.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewWarnings.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewWarnings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWarnings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridViewWarnings.MultiSelect = false;
            this.dataGridViewWarnings.Name = "dataGridViewWarnings";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewWarnings.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewWarnings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewWarnings.ShowEditingIcon = false;
            this.dataGridViewWarnings.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewWarnings_CurrentCellDirtyStateChanged);
            this.dataGridViewWarnings.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWarnings_CellValueChanged);
            this.dataGridViewWarnings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWarnings_CellContentClick);
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.ActiveLinkColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column2.TrackVisitedState = false;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // UserControlErrorAsWarning
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.dataGridViewWarnings);
            this.Name = "UserControlErrorAsWarning";
            this.Load += new System.EventHandler(this.UserControlErrorAsWarning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWarnings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewWarnings;
        private System.Windows.Forms.HelpProvider m_helpProvider;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewLinkColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
