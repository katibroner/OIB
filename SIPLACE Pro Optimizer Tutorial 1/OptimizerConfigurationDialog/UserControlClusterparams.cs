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
    ///     Summary description for UserControlClusterparams.
    /// </summary>
    public class UserControlClusterparams : UserControl
    {
        private Label label1;
        private Label label2;
        private CheckBox cbxBalancing;
        private TextBox tbxSetupFillLevel;
        private ErrorProvider errorProvider;
        private Label label3;
        private IContainer components;

        public event EventHandler SetInputError;
        public event EventHandler ClearInputError;


        public UserControlClusterparams()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof (UserControlClusterparams));
            this.cbxBalancing = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSetupFillLevel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxBalancing
            // 
            resources.ApplyResources(this.cbxBalancing, "cbxBalancing");
            this.cbxBalancing.Name = "cbxBalancing";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbxSetupFillLevel
            // 
            resources.ApplyResources(this.tbxSetupFillLevel, "tbxSetupFillLevel");
            this.tbxSetupFillLevel.Name = "tbxSetupFillLevel";
            this.tbxSetupFillLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxSetupFillLevel_KeyPress);
            this.tbxSetupFillLevel.Validated += new System.EventHandler(this.tbxSetupFillLevel_Validated);
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // UserControlClusterparams
            // 
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxSetupFillLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxBalancing);
            resources.ApplyResources(this, "$this");
            this.Name = "UserControlClusterparams";
            ((System.ComponentModel.ISupportInitialize) (this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private void tbxSetupFillLevel_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tbxSetupFillLevel_Validated(object sender, EventArgs e)
        {
            long l = 0;
            bool bError = true;
            try
            {
                l = Convert.ToInt64(tbxSetupFillLevel.Text);
                bError = false;
            }
            catch
            {
            }

            if (bError || l <= 0 || l > 100)
            {
                errorProvider.SetError(tbxSetupFillLevel, "QuantityError");
                if (SetInputError != null) SetInputError(this, new EventArgs());
            }
            else
            {
                errorProvider.SetError(tbxSetupFillLevel, "");
                if (ClearInputError != null) ClearInputError(this, new EventArgs());
            }
        }


        public bool DlgParamClusterBalancing
        {
            get { return cbxBalancing.Checked; }
            set { cbxBalancing.Checked = value; }
        }

        public int DlgParamClusterSetupFillLevel
        {
            get { return Convert.ToInt32(tbxSetupFillLevel.Text); }
            set { tbxSetupFillLevel.Text = value.ToString(CultureInfo.InvariantCulture); }
        }
    }
}