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
using System.Globalization;
using System.Windows.Forms;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    ///     Summary description for UserControlDebug.
    /// </summary>
    public class UserControlDebug : UserControl
    {
        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox m_tbxIterations;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public UserControlDebug()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (UserControlDebug));
            this.m_tbxIterations = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_tbxIterations
            // 
            this.m_tbxIterations.AccessibleDescription = resources.GetString("m_tbxIterations.AccessibleDescription");
            this.m_tbxIterations.AccessibleName = resources.GetString("m_tbxIterations.AccessibleName");
            this.m_tbxIterations.Anchor = ((System.Windows.Forms.AnchorStyles) (resources.GetObject("m_tbxIterations.Anchor")));
            this.m_tbxIterations.AutoSize = ((bool) (resources.GetObject("m_tbxIterations.AutoSize")));
            this.m_tbxIterations.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("m_tbxIterations.BackgroundImage")));
            this.m_tbxIterations.Dock = ((System.Windows.Forms.DockStyle) (resources.GetObject("m_tbxIterations.Dock")));
            this.m_tbxIterations.Enabled = ((bool) (resources.GetObject("m_tbxIterations.Enabled")));
            this.m_tbxIterations.Font = ((System.Drawing.Font) (resources.GetObject("m_tbxIterations.Font")));
            this.m_tbxIterations.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("m_tbxIterations.ImeMode")));
            this.m_tbxIterations.Location = ((System.Drawing.Point) (resources.GetObject("m_tbxIterations.Location")));
            this.m_tbxIterations.MaxLength = ((int) (resources.GetObject("m_tbxIterations.MaxLength")));
            this.m_tbxIterations.Multiline = ((bool) (resources.GetObject("m_tbxIterations.Multiline")));
            this.m_tbxIterations.Name = "m_tbxIterations";
            this.m_tbxIterations.PasswordChar = ((char) (resources.GetObject("m_tbxIterations.PasswordChar")));
            this.m_tbxIterations.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("m_tbxIterations.RightToLeft")));
            this.m_tbxIterations.ScrollBars = ((System.Windows.Forms.ScrollBars) (resources.GetObject("m_tbxIterations.ScrollBars")));
            this.m_tbxIterations.Size = ((System.Drawing.Size) (resources.GetObject("m_tbxIterations.Size")));
            this.m_tbxIterations.TabIndex = ((int) (resources.GetObject("m_tbxIterations.TabIndex")));
            this.m_tbxIterations.Text = resources.GetString("m_tbxIterations.Text");
            this.m_tbxIterations.TextAlign = ((System.Windows.Forms.HorizontalAlignment) (resources.GetObject("m_tbxIterations.TextAlign")));
            this.m_tbxIterations.Visible = ((bool) (resources.GetObject("m_tbxIterations.Visible")));
            this.m_tbxIterations.WordWrap = ((bool) (resources.GetObject("m_tbxIterations.WordWrap")));
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
            // label3
            // 
            this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
            this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles) (resources.GetObject("label3.Anchor")));
            this.label3.AutoSize = ((bool) (resources.GetObject("label3.AutoSize")));
            this.label3.Dock = ((System.Windows.Forms.DockStyle) (resources.GetObject("label3.Dock")));
            this.label3.Enabled = ((bool) (resources.GetObject("label3.Enabled")));
            this.label3.Font = ((System.Drawing.Font) (resources.GetObject("label3.Font")));
            this.label3.Image = ((System.Drawing.Image) (resources.GetObject("label3.Image")));
            this.label3.ImageAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("label3.ImageAlign")));
            this.label3.ImageIndex = ((int) (resources.GetObject("label3.ImageIndex")));
            this.label3.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("label3.ImeMode")));
            this.label3.Location = ((System.Drawing.Point) (resources.GetObject("label3.Location")));
            this.label3.Name = "label3";
            this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("label3.RightToLeft")));
            this.label3.Size = ((System.Drawing.Size) (resources.GetObject("label3.Size")));
            this.label3.TabIndex = ((int) (resources.GetObject("label3.TabIndex")));
            this.label3.Text = resources.GetString("label3.Text");
            this.label3.TextAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("label3.TextAlign")));
            this.label3.Visible = ((bool) (resources.GetObject("label3.Visible")));
            // 
            // label4
            // 
            this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
            this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles) (resources.GetObject("label4.Anchor")));
            this.label4.AutoSize = ((bool) (resources.GetObject("label4.AutoSize")));
            this.label4.Dock = ((System.Windows.Forms.DockStyle) (resources.GetObject("label4.Dock")));
            this.label4.Enabled = ((bool) (resources.GetObject("label4.Enabled")));
            this.label4.Font = ((System.Drawing.Font) (resources.GetObject("label4.Font")));
            this.label4.Image = ((System.Drawing.Image) (resources.GetObject("label4.Image")));
            this.label4.ImageAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("label4.ImageAlign")));
            this.label4.ImageIndex = ((int) (resources.GetObject("label4.ImageIndex")));
            this.label4.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("label4.ImeMode")));
            this.label4.Location = ((System.Drawing.Point) (resources.GetObject("label4.Location")));
            this.label4.Name = "label4";
            this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("label4.RightToLeft")));
            this.label4.Size = ((System.Drawing.Size) (resources.GetObject("label4.Size")));
            this.label4.TabIndex = ((int) (resources.GetObject("label4.TabIndex")));
            this.label4.Text = resources.GetString("label4.Text");
            this.label4.TextAlign = ((System.Drawing.ContentAlignment) (resources.GetObject("label4.TextAlign")));
            this.label4.Visible = ((bool) (resources.GetObject("label4.Visible")));
            // 
            // UserControlDebug
            // 
            this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
            this.AccessibleName = resources.GetString("$this.AccessibleName");
            this.AutoScroll = ((bool) (resources.GetObject("$this.AutoScroll")));
            this.AutoScrollMargin = ((System.Drawing.Size) (resources.GetObject("$this.AutoScrollMargin")));
            this.AutoScrollMinSize = ((System.Drawing.Size) (resources.GetObject("$this.AutoScrollMinSize")));
            this.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tbxIterations);
            this.Enabled = ((bool) (resources.GetObject("$this.Enabled")));
            this.Font = ((System.Drawing.Font) (resources.GetObject("$this.Font")));
            this.ImeMode = ((System.Windows.Forms.ImeMode) (resources.GetObject("$this.ImeMode")));
            this.Location = ((System.Drawing.Point) (resources.GetObject("$this.Location")));
            this.Name = "UserControlDebug";
            this.RightToLeft = ((System.Windows.Forms.RightToLeft) (resources.GetObject("$this.RightToLeft")));
            this.Size = ((System.Drawing.Size) (resources.GetObject("$this.Size")));
            this.ResumeLayout(false);
        }

        #endregion

        public long DlgParamIterations
        {
            get
            {
                try
                {
                    return Convert.ToInt64(m_tbxIterations.Text);
                }
                catch
                {
                    return -1;
                }
            }
            set { m_tbxIterations.Text = value.ToString(CultureInfo.InvariantCulture); }
        }
    }
}