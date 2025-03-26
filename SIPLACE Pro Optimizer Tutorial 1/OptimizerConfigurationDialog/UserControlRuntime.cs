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
    ///     Summary description for UserControlRuntime.
    /// </summary>
    public class UserControlRuntime : UserControl
    {
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox tbxRestartsMax;
        private TextBox tbxHours;
        private TextBox tbxMin;
        private Label label6;
        private TextBox tbxSec;
        private ErrorProvider errorProvider;
        private IContainer components;

        public delegate void EventHandler(object sender, EventArgs e);

        public event EventHandler TreeUpdate;
        public event EventHandler SetInputError;
        public event EventHandler ClearInputError;


        public UserControlRuntime()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            EnableRuntime();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof (UserControlRuntime));
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tbxRestartsMax = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxHours = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxSec = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize) (this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            // 
            // tbxRestartsMax
            // 
            resources.ApplyResources(this.tbxRestartsMax, "tbxRestartsMax");
            this.tbxRestartsMax.Name = "tbxRestartsMax";
            this.tbxRestartsMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxRestartsMax_KeyPress);
            this.tbxRestartsMax.Validated += new System.EventHandler(this.tbxRestartsMax_Validated);
            this.tbxRestartsMax.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
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
            // tbxHours
            // 
            resources.ApplyResources(this.tbxHours, "tbxHours");
            this.tbxHours.Name = "tbxHours";
            this.tbxHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxHours_KeyPress);
            this.tbxHours.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tbxMin
            // 
            resources.ApplyResources(this.tbxMin, "tbxMin");
            this.tbxMin.Name = "tbxMin";
            this.tbxMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxMin_KeyPress);
            this.tbxMin.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tbxSec
            // 
            resources.ApplyResources(this.tbxSec, "tbxSec");
            this.tbxSec.Name = "tbxSec";
            this.tbxSec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxSec_KeyPress);
            this.tbxSec.TextChanged += new System.EventHandler(this.tbxSec_TextChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // UserControlRuntime
            // 
            this.Controls.Add(this.tbxSec);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxHours);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxRestartsMax);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            resources.ApplyResources(this, "$this");
            this.Name = "UserControlRuntime";
            ((System.ComponentModel.ISupportInitialize) (this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            EnableRuntime();
            if (TreeUpdate != null) TreeUpdate(this, new EventArgs());
        }

        private void EnableRuntime()
        {
            tbxRestartsMax.Enabled = radioButton1.Checked;
            tbxHours.Enabled = tbxMin.Enabled = tbxSec.Enabled = radioButton2.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(tbxRestartsMax.Text);
                if (TreeUpdate != null) TreeUpdate(this, new EventArgs());
            }
            catch
            {
                tbxRestartsMax.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(tbxHours.Text);
                if (TreeUpdate != null) TreeUpdate(this, new EventArgs());
            }
            catch
            {
                tbxHours.Focus();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(tbxMin.Text);
                if (TreeUpdate != null) TreeUpdate(this, new EventArgs());
            }
            catch
            {
                tbxMin.Focus();
            }
        }

        private void tbxSec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(tbxSec.Text);
                if (TreeUpdate != null) TreeUpdate(this, new EventArgs());
            }
            catch
            {
                tbxSec.Focus();
            }
        }

        private void tbxRestartsMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            int KeyCode = e.KeyChar;
            if (!IsNumeric(KeyCode))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void tbxHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            int KeyCode = e.KeyChar;
            if (!IsNumeric(KeyCode))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void tbxMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            int KeyCode = e.KeyChar;
            if (!IsNumeric(KeyCode))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void tbxSec_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tbxRestartsMax_Validated(object sender, EventArgs e)
        {
            long l = 0;
            bool bError = true;
            try
            {
                l = Convert.ToInt64(tbxRestartsMax.Text);
                bError = false;
            }
            catch
            {
            }

            if (bError || l > int.MaxValue || l <= 0)
            {
                errorProvider.SetError(tbxRestartsMax, "QuantityError");
                if (SetInputError != null) SetInputError(this, new EventArgs());
            }
            else
            {
                errorProvider.SetError(tbxRestartsMax, "");
                if (ClearInputError != null) ClearInputError(this, new EventArgs());
            }
        }

        public long DlgParamNumberRestarts
        {
            get
            {
                try
                {
                    long l = Convert.ToInt64(tbxRestartsMax.Text);
                    if (l < 0) tbxRestartsMax.Text = "1";
                }
                catch
                {
                    tbxRestartsMax.Text = "1";
                }
                return Convert.ToInt64(tbxRestartsMax.Text);
            }
            set { tbxRestartsMax.Text = value.ToString(CultureInfo.InvariantCulture); }
        }

        public bool DlgParamUseRestarts
        {
            get { return radioButton1.Checked; }
            set { radioButton1.Checked = value; }
        }

        public int DlgParamRuntimeHours
        {
            get { return Convert.ToInt32(tbxHours.Text); }
            set { tbxHours.Text = value.ToString(CultureInfo.InvariantCulture); }
        }

        public int DlgParamRuntimeMinutes
        {
            get
            {
                try
                {
                    int n = Convert.ToInt32(tbxMin.Text);
                    if (n < 0) tbxMin.Text = "00";
                    else if (n > 60) tbxMin.Text = "59";
                }
                catch
                {
                    tbxMin.Text = "00";
                }
                return Convert.ToInt32(tbxMin.Text);
            }
            set { tbxMin.Text = value.ToString("0#"); }
        }

        public int DlgParamRuntimeSec
        {
            get
            {
                try
                {
                    int n = Convert.ToInt32(tbxSec.Text);
                    if (n < 0) tbxSec.Text = "00";
                    else if (n > 60) tbxSec.Text = "59";
                }
                catch
                {
                    tbxSec.Text = "00";
                }
                return Convert.ToInt32(tbxSec.Text);
            }
            set { tbxSec.Text = value.ToString("0#"); }
        }

        public bool DlgParamUseRuntime
        {
            get { return radioButton2.Checked; }
            set { radioButton2.Checked = value; }
        }
    }
}