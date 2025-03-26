#region Copyright

// // ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// // All rights reserved.
// //
// // The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    ///     Summary description for UserControlLoggingSpot.
    /// </summary>
    public class UserControlLoggingSpot : UserControl
    {
        private Button btnFileSelect;
        private Label label1;
        private TextBox tbxFileName;
        private CheckBox checkBox1;
        private Label label2;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public delegate void EventHandler(object sender, EventArgs e);

        public event EventHandler TreeUpdate;

        public UserControlLoggingSpot()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            tbxFileName.Enabled = btnFileSelect.Enabled = checkBox1.Checked;
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (UserControlLoggingSpot));
            this.btnFileSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFileSelect
            // 
            this.btnFileSelect.AccessibleDescription = resources.GetString("btnFileSelect.AccessibleDescription");
            this.btnFileSelect.AccessibleName = resources.GetString("btnFileSelect.AccessibleName");
            this.btnFileSelect.Anchor = ((System.Windows.Forms.AnchorStyles) (resources.GetObject("btnFileSelect.Anchor")));
            this.btnFileSelect.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("btnFileSelect.BackgroundImage")));
            this.btnFileSelect.Dock = ((System.Windows.Forms.DockStyle) (resources.GetObject("btnFileSelect.Dock")));
            this.btnFileSelect.Enabled = ((bool) (resources.GetObject("btnFileSelect.Enabled")));
            this.btnFileSelect.FlatStyle = ((System.Windows.Forms.FlatStyle) (resources.GetObject("btnFileSelect.FlatStyle")));
            this.btnFileSelect.Font = ((System.Drawing.Font) (resources.GetObject("btnFileSelect.Font")));
            this.btnFileSelect.Image = ((System.Drawing.Image) (resources.GetObject("btnFileSelect.Image")));
            this.btnFileSelect.ImageAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("btnFileSelect.ImageAlign")));
            this.btnFileSelect.ImageIndex = ((int) (resources.GetObject("btnFileSelect.ImageIndex")));
            this.btnFileSelect.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("btnFileSelect.ImeMode")));
            this.btnFileSelect.Location = ((System.Drawing.Point) (resources.GetObject("btnFileSelect.Location")));
            this.btnFileSelect.Name = "btnFileSelect";
            this.btnFileSelect.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("btnFileSelect.RightToLeft")));
            this.btnFileSelect.Size = ((System.Drawing.Size) (resources.GetObject("btnFileSelect.Size")));
            this.btnFileSelect.TabIndex = ((int) (resources.GetObject("btnFileSelect.TabIndex")));
            this.btnFileSelect.Text = resources.GetString("btnFileSelect.Text");
            this.btnFileSelect.TextAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("btnFileSelect.TextAlign")));
            this.btnFileSelect.Visible = ((bool) (resources.GetObject("btnFileSelect.Visible")));
            this.btnFileSelect.Click += new System.EventHandler(this.btnFileSelect_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
            this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles) (resources.GetObject("label1.Anchor")));
            this.label1.AutoSize = ((bool) (resources.GetObject("label1.AutoSize")));
            this.label1.Dock = ((System.Windows.Forms.DockStyle) (resources.GetObject("label1.Dock")));
            this.label1.Enabled = ((bool) (resources.GetObject("label1.Enabled")));
            this.label1.Font = ((System.Drawing.Font) (resources.GetObject("label1.Font")));
            this.label1.Image = ((System.Drawing.Image) (resources.GetObject("label1.Image")));
            this.label1.ImageAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("label1.ImageAlign")));
            this.label1.ImageIndex = ((int) (resources.GetObject("label1.ImageIndex")));
            this.label1.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("label1.ImeMode")));
            this.label1.Location = ((System.Drawing.Point) (resources.GetObject("label1.Location")));
            this.label1.Name = "label1";
            this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("label1.RightToLeft")));
            this.label1.Size = ((System.Drawing.Size) (resources.GetObject("label1.Size")));
            this.label1.TabIndex = ((int) (resources.GetObject("label1.TabIndex")));
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("label1.TextAlign")));
            this.label1.Visible = ((bool) (resources.GetObject("label1.Visible")));
            // 
            // tbxFileName
            // 
            this.tbxFileName.AccessibleDescription = resources.GetString("tbxFileName.AccessibleDescription");
            this.tbxFileName.AccessibleName = resources.GetString("tbxFileName.AccessibleName");
            this.tbxFileName.Anchor = ((System.Windows.Forms.AnchorStyles) (resources.GetObject("tbxFileName.Anchor")));
            this.tbxFileName.AutoSize = ((bool) (resources.GetObject("tbxFileName.AutoSize")));
            this.tbxFileName.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("tbxFileName.BackgroundImage")));
            this.tbxFileName.Dock = ((System.Windows.Forms.DockStyle) (resources.GetObject("tbxFileName.Dock")));
            this.tbxFileName.Enabled = ((bool) (resources.GetObject("tbxFileName.Enabled")));
            this.tbxFileName.Font = ((System.Drawing.Font) (resources.GetObject("tbxFileName.Font")));
            this.tbxFileName.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("tbxFileName.ImeMode")));
            this.tbxFileName.Location = ((System.Drawing.Point) (resources.GetObject("tbxFileName.Location")));
            this.tbxFileName.MaxLength = ((int) (resources.GetObject("tbxFileName.MaxLength")));
            this.tbxFileName.Multiline = ((bool) (resources.GetObject("tbxFileName.Multiline")));
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.PasswordChar = ((char) (resources.GetObject("tbxFileName.PasswordChar")));
            this.tbxFileName.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("tbxFileName.RightToLeft")));
            this.tbxFileName.ScrollBars = ((System.Windows.Forms.ScrollBars) (resources.GetObject("tbxFileName.ScrollBars")));
            this.tbxFileName.Size = ((System.Drawing.Size) (resources.GetObject("tbxFileName.Size")));
            this.tbxFileName.TabIndex = ((int) (resources.GetObject("tbxFileName.TabIndex")));
            this.tbxFileName.Text = resources.GetString("tbxFileName.Text");
            this.tbxFileName.TextAlign = ((System.Windows.Forms.HorizontalAlignment) (resources.GetObject("tbxFileName.TextAlign")));
            this.tbxFileName.Visible = ((bool) (resources.GetObject("tbxFileName.Visible")));
            this.tbxFileName.WordWrap = ((bool) (resources.GetObject("tbxFileName.WordWrap")));
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = resources.GetString("checkBox1.AccessibleDescription");
            this.checkBox1.AccessibleName = resources.GetString("checkBox1.AccessibleName");
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles) (resources.GetObject("checkBox1.Anchor")));
            this.checkBox1.Appearance = ((System.Windows.Forms.Appearance) (resources.GetObject("checkBox1.Appearance")));
            this.checkBox1.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("checkBox1.BackgroundImage")));
            this.checkBox1.CheckAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("checkBox1.CheckAlign")));
            this.checkBox1.Dock = ((System.Windows.Forms.DockStyle) (resources.GetObject("checkBox1.Dock")));
            this.checkBox1.Enabled = ((bool) (resources.GetObject("checkBox1.Enabled")));
            this.checkBox1.FlatStyle = ((System.Windows.Forms.FlatStyle) (resources.GetObject("checkBox1.FlatStyle")));
            this.checkBox1.Font = ((System.Drawing.Font) (resources.GetObject("checkBox1.Font")));
            this.checkBox1.Image = ((System.Drawing.Image) (resources.GetObject("checkBox1.Image")));
            this.checkBox1.ImageAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("checkBox1.ImageAlign")));
            this.checkBox1.ImageIndex = ((int) (resources.GetObject("checkBox1.ImageIndex")));
            this.checkBox1.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("checkBox1.ImeMode")));
            this.checkBox1.Location = ((System.Drawing.Point) (resources.GetObject("checkBox1.Location")));
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("checkBox1.RightToLeft")));
            this.checkBox1.Size = ((System.Drawing.Size) (resources.GetObject("checkBox1.Size")));
            this.checkBox1.TabIndex = ((int) (resources.GetObject("checkBox1.TabIndex")));
            this.checkBox1.Text = resources.GetString("checkBox1.Text");
            this.checkBox1.TextAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("checkBox1.TextAlign")));
            this.checkBox1.Visible = ((bool) (resources.GetObject("checkBox1.Visible")));
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
            this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles) (resources.GetObject("label2.Anchor")));
            this.label2.AutoSize = ((bool) (resources.GetObject("label2.AutoSize")));
            this.label2.Dock = ((System.Windows.Forms.DockStyle) (resources.GetObject("label2.Dock")));
            this.label2.Enabled = ((bool) (resources.GetObject("label2.Enabled")));
            this.label2.Font = ((System.Drawing.Font) (resources.GetObject("label2.Font")));
            this.label2.Image = ((System.Drawing.Image) (resources.GetObject("label2.Image")));
            this.label2.ImageAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("label2.ImageAlign")));
            this.label2.ImageIndex = ((int) (resources.GetObject("label2.ImageIndex")));
            this.label2.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("label2.ImeMode")));
            this.label2.Location = ((System.Drawing.Point) (resources.GetObject("label2.Location")));
            this.label2.Name = "label2";
            this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("label2.RightToLeft")));
            this.label2.Size = ((System.Drawing.Size) (resources.GetObject("label2.Size")));
            this.label2.TabIndex = ((int) (resources.GetObject("label2.TabIndex")));
            this.label2.Text = resources.GetString("label2.Text");
            this.label2.TextAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("label2.TextAlign")));
            this.label2.Visible = ((bool) (resources.GetObject("label2.Visible")));
            // 
            // UserControlLoggingSpot
            // 
            this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
            this.AccessibleName = resources.GetString("$this.AccessibleName");
            this.AutoScroll = ((bool) (resources.GetObject("$this.AutoScroll")));
            this.AutoScrollMargin = ((System.Drawing.Size) (resources.GetObject("$this.AutoScrollMargin")));
            this.AutoScrollMinSize = ((System.Drawing.Size) (resources.GetObject("$this.AutoScrollMinSize")));
            this.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxFileName);
            this.Controls.Add(this.btnFileSelect);
            this.Enabled = ((bool) (resources.GetObject("$this.Enabled")));
            this.Font = ((System.Drawing.Font) (resources.GetObject("$this.Font")));
            this.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("$this.ImeMode")));
            this.Location = ((System.Drawing.Point) (resources.GetObject("$this.Location")));
            this.Name = "UserControlLoggingSpot";
            this.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("$this.RightToLeft")));
            this.Size = ((System.Drawing.Size) (resources.GetObject("$this.Size")));
            this.ResumeLayout(false);
        }

        #endregion

        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.CheckFileExists = false;
            dlg.OverwritePrompt = true;
            dlg.Filter = "Spot files (*.spot)|*.spot|All files (*.*)|*.*";
            dlg.FileName = tbxFileName.Text;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            tbxFileName.Text = dlg.FileName;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbxFileName.Enabled = btnFileSelect.Enabled = checkBox1.Checked;
            if (TreeUpdate != null) TreeUpdate(this, new EventArgs());
        }

        public bool DlgParamLoggingSpotOn
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        public string DlgParamLoggingSpotFileName
        {
            get { return tbxFileName.Text; }
            set { tbxFileName.Text = value; }
        }
    }
}