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
    ///     Summary description for UserControlComponentSetup.
    /// </summary>
    public class UserControlComponentSetup : UserControl
    {
        private Label label1;
        private Label label2;
        private TextBox tbxResCount;
        private ErrorProvider errorProvider;
        private IContainer components;
        public event EventHandler SetInputError;
        public event EventHandler ClearInputError;


        public UserControlComponentSetup()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof (UserControlComponentSetup));
            this.tbxResCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize) (this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxResCount
            // 
            resources.ApplyResources(this.tbxResCount, "tbxResCount");
            this.tbxResCount.Name = "tbxResCount";
            this.tbxResCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxResCount_KeyPress);
            this.tbxResCount.Validated += new System.EventHandler(this.tbxResCount_Validated);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // UserControlComponentSetup
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxResCount);
            resources.ApplyResources(this, "$this");
            this.Name = "UserControlComponentSetup";
            ((System.ComponentModel.ISupportInitialize) (this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private void tbxResCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            int KeyCode = e.KeyChar;
            if (!IsNumeric(KeyCode))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private bool IsNumeric(int Val)
        {
            return ((Val >= 48 && Val <= 57) || (Val == 8) || (Val == 27));
        }

        private void tbxResCount_Validated(object sender, EventArgs e)
        {
            long l = 0;
            bool bError = true;
            try
            {
                l = Convert.ToInt64(tbxResCount.Text);
                bError = false;
            }
            catch
            {
            }

            if (bError || l <= 0 || l > 999)
            {
                errorProvider.SetError(tbxResCount, "QuantityError");
                if (SetInputError != null) SetInputError(this, new EventArgs());
            }
            else
            {
                errorProvider.SetError(tbxResCount, "");
                if (ClearInputError != null) ClearInputError(this, new EventArgs());
            }
        }


        public long DlgParamSetupComponentRestriction
        {
            get { return Convert.ToInt64(tbxResCount.Text); }
            set { tbxResCount.Text = value.ToString(CultureInfo.InvariantCulture); }
        }
    }
}