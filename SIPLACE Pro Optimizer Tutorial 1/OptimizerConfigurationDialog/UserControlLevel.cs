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
using Asm.As.Oib.SiplacePro.Optimizer.Contracts.Data;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    ///     Summary description for UserControlLevel.
    /// </summary>
    public class UserControlLevel : UserControl
    {
        private ComboBox comboBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private Panel panel1;
        private CheckBox cbxOptTable;
        private CheckBox cbxOptHeadStep;
        private CheckBox cbxOptNozzleChanger;
        private CheckBox cbxMTCStrategy;
        private CheckBox cbxOpttableFeederPos;
        private Label tbxOptLevelDescription;
        private CheckBox cbxReloadMTC;
        private CheckBox cbxModifyFeeders;
        private CheckBox m_chkIdenticalNozzleConfig;
        private CheckBox cbxAssignPlacementsHeadSchedule;
        private CheckBox cbxKeepPlacementSequence;
        private CheckBox cbxIgnoreFixedFlags;
        private CheckBox cbxNoAdditionalSetupComps;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        private GroupBox groupBox1;
        private GroupBox groupBox2;

        public delegate void EventHandler(object sender, EventArgs e);

        public event EventHandler TreeUpdate;
        private bool m_bInClusterMode;


        public UserControlLevel()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            comboBox1.SelectedIndex = 1;
            SetOptimizationParameters(comboBox1.SelectedIndex);
            EnableButtons();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof (UserControlLevel));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbxOptTable = new System.Windows.Forms.CheckBox();
            this.cbxOptNozzleChanger = new System.Windows.Forms.CheckBox();
            this.cbxOptHeadStep = new System.Windows.Forms.CheckBox();
            this.cbxOpttableFeederPos = new System.Windows.Forms.CheckBox();
            this.cbxMTCStrategy = new System.Windows.Forms.CheckBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbxOptLevelDescription = new System.Windows.Forms.Label();
            this.cbxReloadMTC = new System.Windows.Forms.CheckBox();
            this.cbxModifyFeeders = new System.Windows.Forms.CheckBox();
            this.m_chkIdenticalNozzleConfig = new System.Windows.Forms.CheckBox();
            this.cbxAssignPlacementsHeadSchedule = new System.Windows.Forms.CheckBox();
            this.cbxKeepPlacementSequence = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxNoAdditionalSetupComps = new System.Windows.Forms.CheckBox();
            this.cbxIgnoreFixedFlags = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Items.AddRange(new object[]
            {
                resources.GetString("comboBox1.Items"),
                resources.GetString("comboBox1.Items1"),
                resources.GetString("comboBox1.Items2"),
                resources.GetString("comboBox1.Items3"),
                resources.GetString("comboBox1.Items4")
            });
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // cbxOptTable
            // 
            resources.ApplyResources(this.cbxOptTable, "cbxOptTable");
            this.cbxOptTable.Name = "cbxOptTable";
            this.cbxOptTable.CheckedChanged += new System.EventHandler(this.cbxOptTable_CheckedChanged);
            // 
            // cbxOptNozzleChanger
            // 
            resources.ApplyResources(this.cbxOptNozzleChanger, "cbxOptNozzleChanger");
            this.cbxOptNozzleChanger.Name = "cbxOptNozzleChanger";
            this.cbxOptNozzleChanger.CheckedChanged += new System.EventHandler(this.cbxOptNozzleChanger_CheckedChanged);
            // 
            // cbxOptHeadStep
            // 
            resources.ApplyResources(this.cbxOptHeadStep, "cbxOptHeadStep");
            this.cbxOptHeadStep.Name = "cbxOptHeadStep";
            this.cbxOptHeadStep.CheckedChanged += new System.EventHandler(this.cbxOptHeadStep_CheckedChanged);
            // 
            // cbxOpttableFeederPos
            // 
            resources.ApplyResources(this.cbxOpttableFeederPos, "cbxOpttableFeederPos");
            this.cbxOpttableFeederPos.Name = "cbxOpttableFeederPos";
            this.cbxOpttableFeederPos.CheckedChanged += new System.EventHandler(this.cbxOpttableFeederPos_CheckedChanged);
            // 
            // cbxMTCStrategy
            // 
            resources.ApplyResources(this.cbxMTCStrategy, "cbxMTCStrategy");
            this.cbxMTCStrategy.Name = "cbxMTCStrategy";
            this.cbxMTCStrategy.CheckedChanged += new System.EventHandler(this.cbxMTCStrategy_CheckedChanged);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            // 
            // radioButton3
            // 
            resources.ApplyResources(this.radioButton3, "radioButton3");
            this.radioButton3.Name = "radioButton3";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Name = "panel1";
            // 
            // tbxOptLevelDescription
            // 
            resources.ApplyResources(this.tbxOptLevelDescription, "tbxOptLevelDescription");
            this.tbxOptLevelDescription.BackColor = System.Drawing.SystemColors.Info;
            this.tbxOptLevelDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbxOptLevelDescription.Name = "tbxOptLevelDescription";
            // 
            // cbxReloadMTC
            // 
            resources.ApplyResources(this.cbxReloadMTC, "cbxReloadMTC");
            this.cbxReloadMTC.Name = "cbxReloadMTC";
            // 
            // cbxModifyFeeders
            // 
            resources.ApplyResources(this.cbxModifyFeeders, "cbxModifyFeeders");
            this.cbxModifyFeeders.Name = "cbxModifyFeeders";
            // 
            // m_chkIdenticalNozzleConfig
            // 
            resources.ApplyResources(this.m_chkIdenticalNozzleConfig, "m_chkIdenticalNozzleConfig");
            this.m_chkIdenticalNozzleConfig.Name = "m_chkIdenticalNozzleConfig";
            this.m_chkIdenticalNozzleConfig.UseVisualStyleBackColor = true;
            // 
            // cbxAssignPlacementsHeadSchedule
            // 
            resources.ApplyResources(this.cbxAssignPlacementsHeadSchedule, "cbxAssignPlacementsHeadSchedule");
            this.cbxAssignPlacementsHeadSchedule.Name = "cbxAssignPlacementsHeadSchedule";
            this.cbxAssignPlacementsHeadSchedule.UseVisualStyleBackColor = true;
            this.cbxAssignPlacementsHeadSchedule.CheckedChanged += new System.EventHandler(this.cbxAssignPlacementsHeadSchedule_CheckedChanged);
            // 
            // cbxKeepPlacementSequence
            // 
            resources.ApplyResources(this.cbxKeepPlacementSequence, "cbxKeepPlacementSequence");
            this.cbxKeepPlacementSequence.Name = "cbxKeepPlacementSequence";
            this.cbxKeepPlacementSequence.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.cbxOptHeadStep);
            this.groupBox1.Controls.Add(this.cbxKeepPlacementSequence);
            this.groupBox1.Controls.Add(this.cbxAssignPlacementsHeadSchedule);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.cbxNoAdditionalSetupComps);
            this.groupBox2.Controls.Add(this.cbxIgnoreFixedFlags);
            this.groupBox2.Controls.Add(this.cbxOptTable);
            this.groupBox2.Controls.Add(this.cbxOpttableFeederPos);
            this.groupBox2.Controls.Add(this.m_chkIdenticalNozzleConfig);
            this.groupBox2.Controls.Add(this.cbxModifyFeeders);
            this.groupBox2.Controls.Add(this.cbxReloadMTC);
            this.groupBox2.Controls.Add(this.cbxOptNozzleChanger);
            this.groupBox2.Controls.Add(this.cbxMTCStrategy);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // cbxNoAdditionalSetupComps
            // 
            resources.ApplyResources(this.cbxNoAdditionalSetupComps, "cbxNoAdditionalSetupComps");
            this.cbxNoAdditionalSetupComps.Name = "cbxNoAdditionalSetupComps";
            this.cbxNoAdditionalSetupComps.UseVisualStyleBackColor = true;
            // 
            // cbxIgnoreFixedFlags
            // 
            resources.ApplyResources(this.cbxIgnoreFixedFlags, "cbxIgnoreFixedFlags");
            this.cbxIgnoreFixedFlags.Name = "cbxIgnoreFixedFlags";
            this.cbxIgnoreFixedFlags.UseVisualStyleBackColor = true;
            // 
            // UserControlLevel
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbxOptLevelDescription);
            this.Controls.Add(this.comboBox1);
            resources.ApplyResources(this, "$this");
            this.Name = "UserControlLevel";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        #region Initialization and enabling

        private void SetOptimizationParameters(int nOptLevel)
        {
            switch (nOptLevel)
            {
                case 0:
                    cbxOptTable.Checked = false;
                    cbxOptHeadStep.Checked = false;
                    cbxOptNozzleChanger.Checked = false;
                    m_chkIdenticalNozzleConfig.Checked = false;

                    cbxOptTable.Enabled = false;
                    cbxOptHeadStep.Enabled = false;
                    cbxOptNozzleChanger.Enabled = false;
                    m_chkIdenticalNozzleConfig.Enabled = false;
                    break;
                case 1:
                    cbxOptTable.Checked = true;
                    cbxOptHeadStep.Checked = false;
                    cbxOptNozzleChanger.Checked = true;
                    //m_chkIdenticalNozzleConfig.Checked = false;

                    cbxOptTable.Enabled = false;
                    cbxOptHeadStep.Enabled = false;
                    cbxOptNozzleChanger.Enabled = false;
                    if (!m_bInClusterMode) m_chkIdenticalNozzleConfig.Enabled = true;
                    break;
                case 2:
                    cbxOptTable.Checked = false;
                    cbxOptHeadStep.Checked = true;
                    cbxOptNozzleChanger.Checked = false;
                    //m_chkIdenticalNozzleConfig.Checked = false;

                    cbxOptTable.Enabled = false;
                    cbxOptHeadStep.Enabled = false;
                    cbxOptNozzleChanger.Enabled = false;
                    if (!m_bInClusterMode) m_chkIdenticalNozzleConfig.Enabled = true;
                    break;
                case 3:
                    cbxOptTable.Checked = true;
                    cbxOptHeadStep.Checked = true;
                    cbxOptNozzleChanger.Checked = true;

                    cbxOptTable.Enabled = false;
                    cbxOptHeadStep.Enabled = false;
                    cbxOptNozzleChanger.Enabled = false;
                    if (!m_bInClusterMode) m_chkIdenticalNozzleConfig.Enabled = true;
                    break;
                case 4:
                    cbxOptTable.Enabled = true;
                    cbxOptHeadStep.Enabled = true;
                    cbxOptNozzleChanger.Enabled = true;
                    if (!m_bInClusterMode) m_chkIdenticalNozzleConfig.Enabled = true;
                    break;
            }

            EnableButtons();
        }

        private void EnableButtons()
        {
            cbxOpttableFeederPos.Enabled = cbxMTCStrategy.Enabled =
                cbxReloadMTC.Enabled = cbxNoAdditionalSetupComps.Enabled = cbxOptTable.Checked;
            if (!cbxOptTable.Checked)
            {
                cbxOpttableFeederPos.Checked = false;
                cbxMTCStrategy.Checked = false;
                cbxReloadMTC.Checked = false;
                cbxNoAdditionalSetupComps.Checked = false;
            }

            cbxModifyFeeders.Enabled = cbxIgnoreFixedFlags.Enabled = cbxOpttableFeederPos.Checked;
            if (!cbxOpttableFeederPos.Checked)
            {
                cbxModifyFeeders.Checked = false;
                cbxIgnoreFixedFlags.Checked = false;
            }

            if (m_bInClusterMode)
            {
                m_chkIdenticalNozzleConfig.Checked = false;
                m_chkIdenticalNozzleConfig.Enabled = false;
            }

            if (!cbxOptHeadStep.Checked || cbxOptTable.Checked || cbxOptNozzleChanger.Checked)
                cbxAssignPlacementsHeadSchedule.Checked = false;

            cbxAssignPlacementsHeadSchedule.Enabled = cbxOptHeadStep.Checked && !cbxOptTable.Checked && !cbxOptNozzleChanger.Checked;

            if (!cbxAssignPlacementsHeadSchedule.Checked)
                cbxKeepPlacementSequence.Checked = false;

            cbxKeepPlacementSequence.Enabled = cbxAssignPlacementsHeadSchedule.Checked;

            panel1.Enabled = cbxMTCStrategy.Checked;
        }

        #endregion

        #region Event Handlers

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOptimizationParameters(comboBox1.SelectedIndex);


            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    tbxOptLevelDescription.Text = "LevelText0";
                    break;
                case 1:
                    tbxOptLevelDescription.Text = "LevelText1";
                    break;
                case 2:
                    tbxOptLevelDescription.Text = "LevelText2";
                    break;
                case 3:
                    tbxOptLevelDescription.Text = "LevelText3";
                    break;
                case 4:
                    tbxOptLevelDescription.Text = "LevelTextCustom";
                    break;
            }

            if (TreeUpdate != null) TreeUpdate(this, new EventArgs());
        }


        private void cbxOptTable_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void cbxMTCStrategy_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void cbxOptHeadStep_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }


        private void cbxAssignPlacementsHeadSchedule_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }


        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.DroppedDown = true;
        }

        private void cbxOpttableFeederPos_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void cbxOptNozzleChanger_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        #endregion

        #region Public properties

        public int DlgParamLevel
        {
            get { return comboBox1.SelectedIndex; }
            set { comboBox1.SelectedIndex = value; }
        }

        public bool DlgParamOptimizeTables
        {
            get { return cbxOptTable.Checked; }
            set { cbxOptTable.Checked = value; }
        }

        public bool DlgParamOptimizeNozzleChanger
        {
            get { return cbxOptNozzleChanger.Checked; }
            set { cbxOptNozzleChanger.Checked = value; }
        }

        public bool DlgParamOptimizeHeadSteps
        {
            get { return cbxOptHeadStep.Checked; }
            set { cbxOptHeadStep.Checked = value; }
        }

        public bool DlgParamOptimizeKeepFeederPos
        {
            get { return cbxOpttableFeederPos.Checked; }
            set { cbxOpttableFeederPos.Checked = value; }
        }

        public bool DlgParamOptimizeModifyFeeders
        {
            get { return cbxModifyFeeders.Checked; }
            set { cbxModifyFeeders.Checked = value; }
        }

        public bool DlgParamOptimizeReloadMTC
        {
            get { return cbxReloadMTC.Checked; }
            set { cbxReloadMTC.Checked = value; }
        }

        public bool DlgParamOptimizeMTCFillStrategy
        {
            get { return cbxMTCStrategy.Checked; }
            set { cbxMTCStrategy.Checked = value; }
        }

        public MTCFillStrategyMode DlgParamOptimizeMTCFillStrategyMode
        {
            get
            {
                if (radioButton1.Checked) return MTCFillStrategyMode.BestPerformance;
                if (radioButton2.Checked) return MTCFillStrategyMode.FillOneCompPerCass;
                if (radioButton3.Checked) return MTCFillStrategyMode.TowerSortedByComp;
                return MTCFillStrategyMode.BestPerformance;
            } // undefined (0) not ok for the optimizer
            set
            {
                if (value == MTCFillStrategyMode.BestPerformance) radioButton1.Checked = true;
                else if (value == MTCFillStrategyMode.FillOneCompPerCass) radioButton2.Checked = true;
                else if (value == MTCFillStrategyMode.TowerSortedByComp) radioButton3.Checked = true;
            }
        }

        public bool DlgParamLevelEnabled
        {
            get { return comboBox1.Enabled; }
            set { comboBox1.Enabled = value; }
        }

        public bool DlgParamIdenticalNozzleSetups
        {
            get { return m_chkIdenticalNozzleConfig.Checked; }
            set { m_chkIdenticalNozzleConfig.Checked = value; }
        }

        public bool DlgParamAssignPlacementHeadschedule
        {
            get { return cbxAssignPlacementsHeadSchedule.Checked; }
            set { cbxAssignPlacementsHeadSchedule.Checked = value; }
        }

        public bool DlgParamKeepPlacementSequence
        {
            get { return cbxKeepPlacementSequence.Checked; }
            set { cbxKeepPlacementSequence.Checked = value; }
        }

        public bool DlgParamIgnoreFixedFlags
        {
            get { return cbxIgnoreFixedFlags.Checked; }
            set { cbxIgnoreFixedFlags.Checked = value; }
        }

        public bool DlgParamNoAdditionalSetupComps
        {
            get { return cbxNoAdditionalSetupComps.Checked; }
            set { cbxNoAdditionalSetupComps.Checked = value; }
        }

        public bool DlgParamInClusterMode
        {
            set { m_bInClusterMode = value; }
        }

        #endregion
    }
}