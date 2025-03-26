#region Copyright

// // ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// // All rights reserved.
// //
// // The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using System.ComponentModel;
using System.Windows.Forms;
using Asm.As.Oib.SiplacePro.Optimizer.Contracts.Data;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    ///     Summary description for UserControlLogging.
    /// </summary>
    public class UserControlLogging : UserControl
    {
        private GroupBox groupBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private GroupBox groupBox2;
        private RadioButton radioButton4;
        private RadioButton radioButton5;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public UserControlLogging()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof (UserControlLogging));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButton3
            // 
            resources.ApplyResources(this.radioButton3, "radioButton3");
            this.radioButton3.Name = "radioButton3";
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton4);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // radioButton5
            // 
            resources.ApplyResources(this.radioButton5, "radioButton5");
            this.radioButton5.Name = "radioButton5";
            // 
            // radioButton4
            // 
            this.radioButton4.Checked = true;
            resources.ApplyResources(this.radioButton4, "radioButton4");
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.TabStop = true;
            // 
            // UserControlLogging
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UserControlLogging";
            resources.ApplyResources(this, "$this");
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        public MsgLevel DlgParamLoggingMessages
        {
            get
            {
                if (radioButton1.Checked) return MsgLevel.ERR_MSGS;
                if (radioButton2.Checked) return MsgLevel.ERR_WARNING_MSGS;
                if (radioButton3.Checked) return MsgLevel.ALL_MSGS;
                return MsgLevel.LEVEL_UNDEFINED;
            }
            set
            {
                if (value == MsgLevel.ERR_MSGS) radioButton1.Checked = true;
                else if (value == MsgLevel.ERR_WARNING_MSGS) radioButton2.Checked = true;
                else if (value == MsgLevel.ALL_MSGS) radioButton3.Checked = true;
            }
        }

        public LogLevel DlgParamLoggingNTMessages
        {
            get
            {
                if (radioButton4.Checked) return LogLevel.ERROR_ONLY;
                if (radioButton5.Checked) return LogLevel.ALL;
                return LogLevel.ALL;
            }
            set
            {
                if (value == LogLevel.ERROR_ONLY) radioButton4.Checked = true;
                else if (value == LogLevel.ALL) radioButton5.Checked = true;
            }
        }
    }
}