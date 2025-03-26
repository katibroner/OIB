#region Copyright

// // ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// // All rights reserved.
// //
// // The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Asm.As.Oib.SiplacePro.Optimizer.Contracts.Data;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    ///     Summary description for OptimizerParameterDialog.
    /// </summary>
    public class OptimizerParameterDialog : Form
    {
        private TreeView m_treeView;
        private Label m_lblCaption;
        private Panel m_panelEditor;
        protected Button m_buttonOk;
        protected Button m_buttonCancel;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        //private Biz.Job m_job = null;
        //private Biz.ClusterJob m_clusterJob = null;
        //private OptParamHelper m_optParamHelper = new OptParamHelper();
        public OptimizationParameters m_InitialParams = new OptimizationParameters();

        //all possible warnings which can be treated as errors
        private readonly int[] m_arrAllWarningsAsErrors = {3104, 3117, 3130};


        private readonly bool m_bClusterMode;
        private bool m_bInputError;



        //Controls in the panel Editor
        private readonly UserControlLevel m_userControlLevel = new UserControlLevel();
        private readonly UserControlComponentSetup m_userControlComponentSetup = new UserControlComponentSetup();
        private readonly UserControlRuntime m_userControlRuntime = new UserControlRuntime();
        private readonly UserControlClusterparams m_userControlClusterparams = new UserControlClusterparams();
        private readonly UserControlPerformance m_userControlPerformanceSettings = new UserControlPerformance();
        private readonly UserControlErrorAsWarning m_userControlErrorAsWarning; //Created in constructor as it needs data in its constructor
        private readonly UserControlLogging m_userControlLogging = new UserControlLogging();
        private readonly UserControlLoggingSpot m_userControlLoggingSpot = new UserControlLoggingSpot();
        private readonly UserControlDebug m_userControlDebug = new UserControlDebug();

        //For jobs
        public OptimizerParameterDialog(OptimizationParameters Optparams, bool bIsClusterJob)
        {
            InitializeComponent();
            m_bClusterMode = bIsClusterJob;

            m_InitialParams = Optparams;

            m_userControlErrorAsWarning = new UserControlErrorAsWarning(m_arrAllWarningsAsErrors);

            m_userControlLevel.TreeUpdate += m_userControlLevel_TreeUpdate;
         
            m_userControlComponentSetup.SetInputError += m_userControlComponentSetup_SetInputError;
            m_userControlComponentSetup.ClearInputError += m_userControlComponentSetup_ClearInputError;
            
            m_userControlRuntime.TreeUpdate += m_userControlRuntime_TreeUpdate;
            m_userControlRuntime.SetInputError += m_userControlRuntime_SetInputError;
            m_userControlRuntime.ClearInputError += m_userControlRuntime_ClearInputError;
            
            m_userControlLoggingSpot.TreeUpdate += m_userControlLoggingSpot_TreeUpdate;
            
            m_userControlErrorAsWarning.TreeUpdate += m_userControlErrorAsWarning_TreeUpdate;

            
            if (!m_bClusterMode)
            {

            }
            else
            {
                m_userControlClusterparams.SetInputError += m_userControlClusterparams_SetInputError;
                m_userControlClusterparams.ClearInputError += m_userControlClusterparams_ClearInputError;

                m_panelEditor.SizeChanged += m_panelEditor_SizeChanged;

                SetParameterValues();

                FillTreeView();
                if (m_treeView.Nodes.Count > 0 && m_treeView.Nodes[0] != null)
                    m_treeView.SelectedNode = m_treeView.Nodes[0];
                m_treeView.Select();

                m_userControlLevel.DlgParamLevelEnabled = false; //User cannot set the level in cluster job optimization
            }

            m_panelEditor.SizeChanged += m_panelEditor_SizeChanged;

            SetParameterValues();

            FillTreeView();
            if (m_treeView.Nodes.Count > 0 && m_treeView.Nodes[0] != null)
                m_treeView.SelectedNode = m_treeView.Nodes[0];
            m_treeView.Select();

        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!DesignMode)
            {
                if (disposing)
                {
                    if (components != null)
                    {
                        components.Dispose();
                    }
                    Owner = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof (OptimizerParameterDialog));
            this.m_treeView = new System.Windows.Forms.TreeView();
            this.m_panelEditor = new System.Windows.Forms.Panel();
            this.m_lblCaption = new System.Windows.Forms.Label();
            this.m_buttonOk = new System.Windows.Forms.Button();
            this.m_buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_treeView
            // 
            resources.ApplyResources(this.m_treeView, "m_treeView");
            this.m_treeView.ItemHeight = 18;
            this.m_treeView.Name = "m_treeView";
            this.m_treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_treeView_AfterSelect);
            this.m_treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_treeView_BeforeSelect);
            // 
            // m_panelEditor
            // 
            resources.ApplyResources(this.m_panelEditor, "m_panelEditor");
            this.m_panelEditor.Name = "m_panelEditor";
            // 
            // m_lblCaption
            // 
            resources.ApplyResources(this.m_lblCaption, "m_lblCaption");
            this.m_lblCaption.BackColor = System.Drawing.SystemColors.Info;
            this.m_lblCaption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblCaption.Name = "m_lblCaption";
            // 
            // m_buttonOk
            // 
            resources.ApplyResources(this.m_buttonOk, "m_buttonOk");
            this.m_buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_buttonOk.Name = "m_buttonOk";
            this.m_buttonOk.Click += new System.EventHandler(this.m_buttonOk_Click);
            // 
            // m_buttonCancel
            // 
            resources.ApplyResources(this.m_buttonCancel, "m_buttonCancel");
            this.m_buttonCancel.CausesValidation = false;
            this.m_buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_buttonCancel.Name = "m_buttonCancel";
            // 
            // OptimizerParameterDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.m_lblCaption);
            this.Controls.Add(this.m_panelEditor);
            this.Controls.Add(this.m_treeView);
            this.Controls.Add(this.m_buttonOk);
            this.Controls.Add(this.m_buttonCancel);
            this.Name = "OptimizerParameterDialog";
            this.ResumeLayout(false);
        }

        #endregion

        private void FillTreeView()
        {
            m_treeView.BeginUpdate();

            if (m_bClusterMode)
            {
                TreeNode tClusterParams = new TreeNode("ClusterParameters");
                tClusterParams.Tag = m_userControlClusterparams;
                m_treeView.Nodes.Add(tClusterParams);
            }

            TreeNode tRuntime = new TreeNode("LimitRuntime");
            ChangeTreeNodeRuntimeText(tRuntime);
            tRuntime.Tag = m_userControlRuntime;
            m_treeView.Nodes.Add(tRuntime);

            TreeNode tLevel = new TreeNode("OptimizerLevel");
            ChangeTreeNodeLevelText(tLevel);
            tLevel.Tag = m_userControlLevel;
            m_treeView.Nodes.Add(tLevel);

            TreeNode tComponentSetup = new TreeNode("SetupComponentRestriction");
            tComponentSetup.Tag = m_userControlComponentSetup;
            m_treeView.Nodes.Add(tComponentSetup);

            TreeNode tPerformanceSettings = new TreeNode("PerformanceSettings");
            tPerformanceSettings.Tag = m_userControlPerformanceSettings;
            m_treeView.Nodes.Add(tPerformanceSettings);

            TreeNode tWarningsAsErrors = new TreeNode("Warnings As Errors"); //"PerformanceSettings"));
            ChangeTreeNodeErrorAsWarningText(tWarningsAsErrors);
            tWarningsAsErrors.Tag = m_userControlErrorAsWarning;
            m_treeView.Nodes.Add(tWarningsAsErrors);

            TreeNode tLogging = new TreeNode("Logging");
            tLogging.Tag = m_userControlLogging;
            int nNodeLogging = m_treeView.Nodes.Add(tLogging);

            TreeNode tSpot = new TreeNode("SpotFile");
            ChangeTreeNodeSpotText(tSpot);
            tSpot.Tag = m_userControlLoggingSpot;
            m_treeView.Nodes[nNodeLogging].Nodes.Add(tSpot);

            m_treeView.ExpandAll();
            m_treeView.EndUpdate();
        }

        private void SetParameterValues()
        {
            //Set the params here

            m_userControlClusterparams.DlgParamClusterBalancing = false;
            if (m_InitialParams.ClusterSetupFillLevel > 100 || m_InitialParams.ClusterSetupFillLevel < 1)
                m_userControlClusterparams.DlgParamClusterSetupFillLevel = 100;
            else
                m_userControlClusterparams.DlgParamClusterSetupFillLevel = m_InitialParams.ClusterSetupFillLevel;

            m_userControlRuntime.DlgParamUseRuntime = !m_InitialParams.MaxRestartsLimit;
            long lHours, lMinutes, lSec, ldays, lmSec;
            ConvertTime(m_InitialParams.MaxRuntime, out ldays, out lHours, out lMinutes, out lSec, out lmSec);

            m_userControlRuntime.DlgParamRuntimeHours = (int) lHours + (int) ldays*24;
            m_userControlRuntime.DlgParamRuntimeMinutes = (int) lMinutes;
            m_userControlRuntime.DlgParamRuntimeSec = lmSec > 500 ? (int) lSec + 1 : (int) lSec;

            m_userControlRuntime.DlgParamUseRestarts = m_InitialParams.MaxRestartsLimit;
            m_userControlRuntime.DlgParamNumberRestarts = m_InitialParams.MaxRestarts;

            m_userControlLevel.DlgParamInClusterMode = m_bClusterMode;
            if (m_bClusterMode && OptLevel != 3) // in Cluster Mode only level 3 is permitted
                OptLevel = 3;
            m_userControlLevel.DlgParamLevel = OptLevel;
            m_userControlLevel.DlgParamOptimizeTables = m_InitialParams.OptimizeTables;
            m_userControlLevel.DlgParamOptimizeNozzleChanger = m_InitialParams.OptimizeNozzleChanger;
            m_userControlLevel.DlgParamOptimizeHeadSteps = m_InitialParams.OptimizeHeadSteps;
            m_userControlLevel.DlgParamOptimizeKeepFeederPos = m_InitialParams.DontChangeCompLoc;
            m_userControlLevel.DlgParamOptimizeModifyFeeders = m_InitialParams.DontChangeCompLocModifyFeeder;
            m_userControlLevel.DlgParamOptimizeReloadMTC = m_InitialParams.MTCIdenticalTowers;
            m_userControlLevel.DlgParamOptimizeMTCFillStrategy = m_InitialParams.FillWPC100Percent;
            m_userControlLevel.DlgParamOptimizeMTCFillStrategyMode = m_InitialParams.TowerFillUpStrategy;
            m_userControlLevel.DlgParamIdenticalNozzleSetups = m_InitialParams.IdenticalNozzleSetups;
            m_userControlLevel.DlgParamAssignPlacementHeadschedule = m_InitialParams.AssignPlacementPosFromHeadschedule;
            m_userControlLevel.DlgParamKeepPlacementSequence = m_InitialParams.KeepPlacementSequence;
            m_userControlLevel.DlgParamIgnoreFixedFlags = m_InitialParams.IgnoreFixedFlags;
            m_userControlLevel.DlgParamNoAdditionalSetupComps = m_InitialParams.NoAdditionalSetupComps;

            m_userControlComponentSetup.DlgParamSetupComponentRestriction = m_InitialParams.CompMaxSetupCount == 0 ? 999 : m_InitialParams.CompMaxSetupCount;

            m_userControlPerformanceSettings.DlgParamFastPerformanceLevel2 = m_InitialParams.PerformanceSettingLevel2Fast;

            m_userControlErrorAsWarning.DlgParamOptimizeWarningsAsErrors = new List<int>(m_InitialParams.WarningsAsErrors);

            m_userControlLogging.DlgParamLoggingMessages = m_InitialParams.OptMsgLevel;
            m_userControlLogging.DlgParamLoggingNTMessages = m_InitialParams.OptLogLevel;

            m_userControlLoggingSpot.DlgParamLoggingSpotOn = m_InitialParams.SaveResults;
            m_userControlLoggingSpot.DlgParamLoggingSpotFileName = m_InitialParams.LogFileName;

            //m_userControlDebug.DlgParamIterations = m_InitialParams.MaxIterCount;
        }


        private void GetParamsFromEditors()
        {
            //m_InitialParams.ClusterBalancing = m_userControlClusterparams.DlgParamClusterBalancing;
            m_InitialParams.ClusterSetupFillLevel = m_userControlClusterparams.DlgParamClusterSetupFillLevel;

            //m_InitialParams.MaxIterCount			= m_userControlDebug.DlgParamIterations;
            //if(m_InitialParams.MaxIterCount > int.MaxValue) m_InitialParams.lMaxIterCount = -1;

            m_InitialParams.MaxRestartsLimit = m_userControlRuntime.DlgParamUseRestarts;

            m_InitialParams.MaxRuntime = ((m_userControlRuntime.DlgParamRuntimeHours*60 + m_userControlRuntime.DlgParamRuntimeMinutes)*60 + m_userControlRuntime.DlgParamRuntimeSec)*
                                         1000; // msec
            if (m_InitialParams.MaxRuntime <= 0 || m_InitialParams.MaxRuntime > int.MaxValue) m_InitialParams.MaxRuntime = 60000; // at least a minute

            m_InitialParams.MaxRestarts = m_userControlRuntime.DlgParamNumberRestarts == 0 ? 5 : m_userControlRuntime.DlgParamNumberRestarts;
            if (m_InitialParams.MaxRestarts > int.MaxValue) m_InitialParams.MaxRestarts = 1;

            m_InitialParams.OptimizeTables = m_userControlLevel.DlgParamOptimizeTables;
            m_InitialParams.MTCIdenticalTowers = m_userControlLevel.DlgParamOptimizeReloadMTC;
            m_InitialParams.FillWPC100Percent = m_userControlLevel.DlgParamOptimizeMTCFillStrategy;
            m_InitialParams.TowerFillUpStrategy = m_userControlLevel.DlgParamOptimizeMTCFillStrategyMode;
            m_InitialParams.OptimizeNozzleChanger = m_userControlLevel.DlgParamOptimizeNozzleChanger;
            m_InitialParams.OptimizeHeadSteps = m_userControlLevel.DlgParamOptimizeHeadSteps;
            m_InitialParams.DontChangeCompLoc = m_userControlLevel.DlgParamOptimizeKeepFeederPos;
            m_InitialParams.DontChangeCompLocModifyFeeder = m_userControlLevel.DlgParamOptimizeModifyFeeders;
            m_InitialParams.IdenticalNozzleSetups = m_userControlLevel.DlgParamIdenticalNozzleSetups;
            m_InitialParams.AssignPlacementPosFromHeadschedule = m_userControlLevel.DlgParamAssignPlacementHeadschedule;
            m_InitialParams.KeepPlacementSequence = m_userControlLevel.DlgParamKeepPlacementSequence;
            m_InitialParams.IgnoreFixedFlags = m_userControlLevel.DlgParamIgnoreFixedFlags;
            m_InitialParams.NoAdditionalSetupComps = m_userControlLevel.DlgParamNoAdditionalSetupComps;

            m_InitialParams.PerformanceSettingLevel2Fast = m_userControlPerformanceSettings.DlgParamFastPerformanceLevel2;

            m_InitialParams.WarningsAsErrors = m_userControlErrorAsWarning.DlgParamOptimizeWarningsAsErrors.ToArray();

            m_InitialParams.OptMsgLevel = m_userControlLogging.DlgParamLoggingMessages;
            m_InitialParams.OptLogLevel = m_userControlLogging.DlgParamLoggingNTMessages;
            m_InitialParams.CompMaxSetupCount = m_userControlComponentSetup.DlgParamSetupComponentRestriction;
            if (m_InitialParams.CompMaxSetupCount > int.MaxValue || m_InitialParams.CompMaxSetupCount <= 0) m_InitialParams.CompMaxSetupCount = 999;

            m_InitialParams.SaveResults = m_userControlLoggingSpot.DlgParamLoggingSpotOn;
            m_InitialParams.LogFileName = m_userControlLoggingSpot.DlgParamLoggingSpotFileName;
        }


        private void m_treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (m_bInputError) return; // Do not change tabs user entered wrong value

            TreeNode t = e.Node;
            string str = t.Text;
            int nEnd = str.IndexOf("(", StringComparison.Ordinal);
            int nCount = str.Length - nEnd;
            if (nEnd > 1)
                m_lblCaption.Text = str.Remove(nEnd, nCount);
            else
                m_lblCaption.Text = t.Text;

            m_panelEditor.CausesValidation = true;
            m_panelEditor.Controls.Clear();
            if (t.Tag is Control)
            {
                ((Control) t.Tag).Size = m_panelEditor.Size;
                m_panelEditor.Controls.Add((Control) t.Tag);
            }
        }

        //Change the size of the user controls if the user changes the size of the main frame dialog 
        private void m_panelEditor_SizeChanged(object sender, EventArgs e)
        {
            m_userControlLevel.Size = m_panelEditor.Size;
            m_userControlErrorAsWarning.Size = m_panelEditor.Size;
        }

        //Tree updates
        private void m_userControlLevel_TreeUpdate(object sender, EventArgs e)
        {
            ChangeTreeNodeLevelText(m_treeView.SelectedNode);
        }

        private void ChangeTreeNodeLevelText(TreeNode treeNode)
        {
            if (m_userControlLevel.DlgParamLevel == 4)
                ChangeTreeNodeText(treeNode, "Custom");
            else
                ChangeTreeNodeText(treeNode, m_userControlLevel.DlgParamLevel.ToString(CultureInfo.InvariantCulture));
        }

        private void ChangeTreeNodeText(TreeNode n, string strText)
        {
            if (n == null) return;
            string str = n.Text;
            int nEnd = str.IndexOf("(", StringComparison.Ordinal);
            int nCount = str.Length - nEnd;
            string strNew = str;
            if (nEnd > 0)
            {
                strNew = str.Remove(nEnd, nCount);
            }
            n.Text = strNew.Trim() + " (" + strText + ")";
        }

        private void m_buttonOk_Click(object sender, EventArgs e)
        {
            OnOk();
        }

        private void m_userControlRuntime_TreeUpdate(object sender, EventArgs e)
        {
            ChangeTreeNodeRuntimeText(m_treeView.SelectedNode);
        }

        private void ChangeTreeNodeRuntimeText(TreeNode treeNode)
        {
            if (m_userControlRuntime.DlgParamUseRestarts)
                ChangeTreeNodeText(treeNode, "Restarts " + m_userControlRuntime.DlgParamNumberRestarts);
            else if (m_userControlRuntime.DlgParamUseRuntime)
            {
                long nTime = ((long) ((m_userControlRuntime.DlgParamRuntimeHours*60
                                       + m_userControlRuntime.DlgParamRuntimeMinutes)*60
                                      + m_userControlRuntime.DlgParamRuntimeSec))*1000;
                ChangeTreeNodeText(treeNode, "Runtime " + ConvertTimeToStrHHMMSS(nTime));
            }
        }

        private void m_userControlLoggingSpot_TreeUpdate(object sender, EventArgs e)
        {
            ChangeTreeNodeSpotText(m_treeView.SelectedNode);
        }

        private void ChangeTreeNodeSpotText(TreeNode treeNode)
        {
            if (m_userControlLoggingSpot.DlgParamLoggingSpotOn)
                ChangeTreeNodeText(treeNode, "On");
            else
                ChangeTreeNodeText(treeNode, "Off");
        }

        private void m_userControlErrorAsWarning_TreeUpdate(object sender, EventArgs e)
        {
            ChangeTreeNodeErrorAsWarningText(m_treeView.SelectedNode);
        }

        private void ChangeTreeNodeErrorAsWarningText(TreeNode treeNode)
        {
            ChangeTreeNodeText(treeNode, m_userControlErrorAsWarning.DlgParamOptimizeWarningsAsErrors.Count.ToString(CultureInfo.InvariantCulture));
        }


        protected void OnOk()
        {
            GetParamsFromEditors();
        }


        private void m_userControlClusterparams_SetInputError(object sender, EventArgs e)
        {
            m_bInputError = true;
        }

        private void m_userControlClusterparams_ClearInputError(object sender, EventArgs e)
        {
            m_bInputError = false;
        }

        private void m_treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (m_bInputError) e.Cancel = true;
        }

        private void m_userControlRuntime_SetInputError(object sender, EventArgs e)
        {
            m_bInputError = true;
        }

        private void m_userControlRuntime_ClearInputError(object sender, EventArgs e)
        {
            m_bInputError = false;
        }

        private void m_userControlComponentSetup_SetInputError(object sender, EventArgs e)
        {
            m_bInputError = true;
        }

        private void m_userControlComponentSetup_ClearInputError(object sender, EventArgs e)
        {
            m_bInputError = false;
        }

        private int OptLevel
        {
            get
            {
                int nLevel = 4;
                if (!m_InitialParams.OptimizeTables && !m_InitialParams.OptimizeNozzleChanger && !m_InitialParams.OptimizeHeadSteps && !m_InitialParams.IdenticalNozzleSetups)
                    nLevel = 0;
                else if (m_InitialParams.OptimizeTables && m_InitialParams.OptimizeNozzleChanger && !m_InitialParams.OptimizeHeadSteps)
                    nLevel = 1;
                else if (!m_InitialParams.OptimizeTables && !m_InitialParams.OptimizeNozzleChanger && m_InitialParams.OptimizeHeadSteps)
                    nLevel = 2;
                else if (m_InitialParams.OptimizeTables && m_InitialParams.OptimizeNozzleChanger && m_InitialParams.OptimizeHeadSteps)
                    nLevel = 3;
                return nLevel;
            }
            set
            {
                if (value == 3)
                    m_InitialParams.OptimizeTables = m_InitialParams.OptimizeNozzleChanger = m_InitialParams.OptimizeHeadSteps = true;
            }
        }

        public static void ConvertTime(double fTime, out long days, out long hours, out long minutes, out long seconds, out long milliseconds)
        {
            milliseconds = 0;

            //convert to seconds
            long count = Convert.ToInt64(fTime);

            //pull out days
            days = count/(24*3600*1000);

            if (days > 0)
            {
                count -= (24*3600*1000*days);
            }

            //pull out HH::MM::SS::MS
            hours = count/(3600*1000);
            if (hours > 0)
            {
                count -= (3600*1000*hours);
            }

            minutes = count/(60*1000);
            if (minutes > 0)
            {
                count -= (60*1000*minutes);
            }
            seconds = (int) count/(1000);

            if (seconds > 0)
            {
                count -= (1000*seconds);
            }

            if (count > 0)
            {
                milliseconds = count;
            }
        }

        public static string ConvertTimeToStrHHMMSS(double fTime)
        {
            try
            {
                long minutes;
                long days;
                long hours;
                long seconds;
                long milliseconds;
                ConvertTime(fTime, out days, out hours, out minutes, out seconds, out milliseconds);

                // convert days back to hours
                hours += days*24;

                // round up milliseconds if >=500
                if (milliseconds >= 500)
                {
                    seconds += 1;
                    if (seconds > 59)
                    {
                        minutes += 1;
                        seconds -= 60;
                        if (minutes > 59)
                        {
                            hours += 1;
                            minutes -= 60;
                        }
                    }
                }
                return (String.Format("{0:00}:{1:00}:{2:00}", new object[] {hours, minutes, seconds}));
            }
            catch
            {
                return "";
            }
        }
    }
}