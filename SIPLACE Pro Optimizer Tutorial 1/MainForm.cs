#region Copyright

// ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region Using

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows.Forms;

#region DOC_NAMESPACES

using Asm.As.Oib.SiplacePro.Optimizer.Contracts.Data;
using Asm.As.Oib.SiplacePro.Optimizer.Contracts.Data.Events;
using Asm.As.Oib.SiplacePro.Optimizer.Contracts.Faults;
using Asm.As.Oib.SiplacePro.Optimizer.Proxy.Business.Objects;

#endregion

#endregion

namespace OIB.Tutorial
{
    public partial class MainForm : Form
    {
        #region Fields

        private Optimizer _OptimizerProxy;
        private bool _Disposed;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Disposed += MainForm_Disposed;
        }

        #endregion

        #region Form Events

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ReleaseOptimizerProxy();
        }

        private void MainForm_Disposed(object sender, EventArgs e)
        {
            _Disposed = true;
        }

        #endregion

        #region Start Optimization

        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _ListBox.Items.Clear();

                SetButtonStates(true);

                MsgOut(string.Format("Start of optimization for job '{0}'", _TextBoxJob.Text));
                Update();

                InitializeOptimizerPoxy();

                #region DOC_START_OPTIMIZER

                AsyncCallback callback = StartOptCallback;

                _OptimizerProxy.BeginStart(
                    _TextBoxJob.Text, // Use the Job full path from the GUI
                    null, // EndpointAddress == null -> use default from App.config
                    null, // Binding == null         -> use default from App.config
                    callback, // Callback to receive the result of this asynchronous call 
                    null);

                #endregion
            }
            catch (FaultException<SiplaceProOptimizerFault> fault)
            {
                MsgOut(string.Format(
                    "SERVICE ERRROR: Failed to start optimization for job '{0}': '{1}'",
                    _TextBoxJob.Text, fault.Message));
                SetButtonStates(false, false);
            }
            catch (CommunicationException ex)
            {
                MsgOut(string.Format(
                    "COMMUNICATION ERRROR: Failed to start optimization for job '{0}': '{1}'",
                    _TextBoxJob.Text, ex.Message));
                SetButtonStates(false, false);
            }
            catch (Exception ex)
            {
                MsgOut(string.Format(
                    "ERRROR: Failed to start optimization for job '{0}': '{1}'",
                    _TextBoxJob.Text, ex.Message));
                SetButtonStates(false, false);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SetButtonStates(bool currentlyOptimizing, bool? stopPossible = null)
        {
            _ButtonCancel.Enabled = currentlyOptimizing;
            _ButtonStart.Enabled = _ButtonMessages.Enabled = _ButtonOptimizationParameters.Enabled = !currentlyOptimizing;
            if (stopPossible != null)
            {
                _ButtonStop.Enabled = stopPossible.Value;
            }
        }


        private delegate void StartOptCallbackDelegate(IAsyncResult result);

        /// <summary>
        ///     Handling of the asychnron incomming event indicating the
        ///     asynchon call to start the optimization as completed yet.
        /// </summary>
        /// <param name="result"></param>
        private void StartOptCallback(IAsyncResult result)
        {
            if (_Disposed)
                return;

            #region DOC_START_OPTIMIZER_END

            if (InvokeRequired)
            {
                Invoke(
                    new StartOptCallbackDelegate(StartOptCallback),
                    new object[] {result});
            }
            else
            {
                try
                {
                    // Call EndStart() to see if there are exceptions
                    _OptimizerProxy.EndStart(result);
                }
           #endregion

                catch (FaultException<SiplaceProOptimizerFault> ex)
                {
                    MsgOut(string.Format(
                        "SERVICE ERRROR: Error receiving the async result of StartJob: '{0}'",
                        ex.Message));
                    SetButtonStates(false, false);
                }
                catch (CommunicationException ex)
                {
                    MsgOut(string.Format(
                        "COMMUNICATION ERRROR: Error receiving the async result of StartJob: '{0}'",
                        ex.Message));
                    SetButtonStates(false, false);
                }
                catch (Exception ex)
                {
                    MsgOut(string.Format(
                        "ERRROR: Error receiving the async result of StartJob: '{0}'",
                        ex.Message));
                    SetButtonStates(false, false);
                }
            }
        }

        #endregion

        #region Stop Optimization

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (_OptimizerProxy != null)
                {
                    _ButtonStop.Enabled = false;
                    _ButtonCancel.Enabled = false;
                    Update();
                    _OptimizerProxy.Stop();
                }
            }
            catch (FaultException<SiplaceProOptimizerFault> ex)
            {
                MsgOut(string.Format("SERVICE ERRROR: Error stopping optimizer: '{0}'", ex.Message));
                SetButtonStates(false, false);
            }
            catch (CommunicationException ex)
            {
                MsgOut(string.Format("COMMUNICATION ERRROR: Error stopping optimizer: '{0}'", ex.Message));
                SetButtonStates(false, false);
            }
            catch (Exception ex)
            {
                MsgOut(string.Format("ERRROR: Error stopping optimizer: '{0}'", ex.Message));
                SetButtonStates(false, false);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Cancel Optimization

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (_OptimizerProxy != null)
                {
                    _ButtonStop.Enabled = false;
                    _ButtonCancel.Enabled = false;
                    Update();
                    _OptimizerProxy.Cancel();
                }
            }
            catch (FaultException<SiplaceProOptimizerFault> ex)
            {
                MsgOut(string.Format("SERVICE ERRROR: Error canceling optimizer: '{0}'", ex.Message));
                SetButtonStates(false, false);
            }
            catch (CommunicationException ex)
            {
                MsgOut(string.Format("COMMUNICATION ERRROR: Error canceling optimizer: '{0}'", ex.Message));
                SetButtonStates(false, false);
            }
            catch (Exception ex)
            {
                MsgOut(string.Format("ERRROR: Error canceling optimizer: '{0}'", ex.Message));
                SetButtonStates(false, false);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Change Job Optimization Configuration

        private void ButtonOptimizationParameters_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                InitializeOptimizerPoxy();

                OptimizationParameters optParams = _OptimizerProxy.GetConfiguration(_TextBoxJob.Text, ObjectType.Job);

                Cursor = Cursors.Default;

                OptimizerParameterDialog dialog = new OptimizerParameterDialog(optParams, false);
                dialog.Text = string.Format("Optimization Parameters for Job '{0}'", _TextBoxJob.Text);
                dialog.ShowDialog(this);

                if (dialog.DialogResult == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    _OptimizerProxy.SaveConfiguration(dialog.m_InitialParams, _TextBoxJob.Text, ObjectType.Job);
                }
            }
            catch (FaultException<SiplaceProOptimizerFault> ex)
            {
                MsgOut(string.Format("SERVICE ERRROR: Failed to get/save the configuration for job '{0}': '{1}'", _TextBoxJob.Text, ex.Message));
            }
            catch (CommunicationException ex)
            {
                MsgOut(string.Format("COMMUNICATION ERRROR: Failed to get/save the configuration for job '{0}': '{1}'", _TextBoxJob.Text, ex.Message));
            }
            catch (Exception ex)
            {
                MsgOut(string.Format("ERRROR: Failed to get/save the configuration for job '{0}': '{1}'", _TextBoxJob.Text, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Show Job Messages

        private void ButtonMessages_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                InitializeOptimizerPoxy();

                #region DOC_SHOW_MESSAGES

                List<OptimizerMessage> messages = _OptimizerProxy.GetOptimizerMessages(
                    _TextBoxJob.Text,
                    ObjectType.Job);

                _ListBox.Items.Clear();
                MsgOut(string.Format("Messages for job '{0}'", _TextBoxJob.Text));
                foreach (OptimizerMessage message in messages)
                {
                    MsgOut(string.Format("Type: {0} \tCode: {1} \tText: {2}",
                        message.MessageType,
                        message.MessageCode,
                        message.Message));
                }

                #endregion
            }
            catch (FaultException<SiplaceProOptimizerFault> ex)
            {
                MsgOut(string.Format("SERVICE ERRROR: Failed to get the messages for job '{0}': '{1}'", _TextBoxJob.Text, ex.Message));
            }
            catch (CommunicationException ex)
            {
                MsgOut(string.Format("COMMUNICATION ERRROR: Failed to get the messages for job '{0}': '{1}'", _TextBoxJob.Text, ex.Message));
            }
            catch (Exception ex)
            {
                MsgOut(string.Format("ERRROR: Failed to get the messages for job '{0}': '{1}'", _TextBoxJob.Text, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Optimize Proxy Related

        #region DOC_INITIALZE_PROXY

        private void InitializeOptimizerPoxy()
        {
            if (_OptimizerProxy == null)
            {
                #region DOC_CREATE_PROXY

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //Optimizer.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                // Use App.config file endpoint settings.
                _OptimizerProxy = new Optimizer();

                _OptimizerProxy.StateChangedExt += OptimizerProxy_StateChangedExt;
                _OptimizerProxy.ProgressChangedExt += OptimizerProxy_ProgressChangedExt;
                _OptimizerProxy.ResultChangedExt += OptimizerProxy_ResultChangedExt;

                #endregion
            }
        }

        #endregion

        private void ReleaseOptimizerProxy()
        {
            try
            {
                if (_OptimizerProxy != null)
                {
                    _OptimizerProxy.StateChangedExt -= OptimizerProxy_StateChangedExt;
                    _OptimizerProxy.ProgressChangedExt -= OptimizerProxy_ProgressChangedExt;
                    _OptimizerProxy.ResultChangedExt -= OptimizerProxy_ResultChangedExt;
                    _OptimizerProxy.Dispose();
                }
            }
            catch (Exception ex)
            {
                MsgOut("ERROR: failed to release optimizer proxy: " + ex.Message);
            }
            finally
            {
                _OptimizerProxy = null;
            }
        }

        #region Optimizer Events

        private void OptimizerProxy_StateChangedExt(OptStateArgs args)
        {
            if (_Disposed)
                return;

            if (InvokeRequired)
            {
                Invoke(
                    new StateEventExt(OptimizerProxy_StateChangedExt),
                    new object[] {args});
            }
            else
            {
                _ButtonStop.Enabled = args.IsStopPossible;

                MsgOut(string.Format(
                    "OPTIMIZER: State changed: {0}, {1}, {2}",
                    args.StateCode,
                    args.StateComment,
                    args.IsStopPossible));
            }
        }

        private void OptimizerProxy_ProgressChangedExt(OptProgressArgs args)
        {
            if (_Disposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new ProgressEventExt(OptimizerProxy_ProgressChangedExt), new object[] {args});
            }
            else
            {
                MsgOut(string.Format("OPTIMIZER: Progress Changed: {0}, {1}, {2}, {3}, {4}, {5}",
                    args.OptProgressType,
                    args.RunTime,
                    args.Percent,
                    args.ProductionTime,
                    args.AverageUtilization,
                    args.PlacementRate));
            }
        }

        #region DOC_OPTIMIZER_RESULT_CHANGED_EVENT

        private void OptimizerProxy_ResultChangedExt(OptResultArgs args)
        {
            if (_Disposed)
                return;

            if (InvokeRequired)
            {
                Invoke(
                    new ResultEventExt(OptimizerProxy_ResultChangedExt),
                    new object[] {args});
            }
            else
            {
                if (args.OptimizationSuccessful)
                {
                    MsgOut(string.Format("OPTIMIZER: Result Changed (Success): {2} (errors: {0} warings: {1}).",
                        args.ErrorCount, args.WarningCount, args.ResultComment));
                    MsgOut(string.Format("OPTIMIZER: Result Changed (PlacementRate: {0} ProductionTime: {1} Utilization: {2}).",
                        args.PlacementRate, args.ProductionTime, args.AverageUtilization));
                }
                else
                    MsgOut(string.Format("OPTIMIZER: Result Changed (Failure): {2} (errors: {0} warings: {1}).",
                        args.ErrorCount, args.WarningCount, args.ResultComment));

                SetButtonStates(false, false);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Messages (message view)

        public delegate void MsgOutDelegate(string message);

        public void MsgOut(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MsgOutDelegate(MsgOut), new object[] {message});
            }
            else
            {
                _ListBox.Items.Add(message);

                // Scroll to the last entry
                _ListBox.SelectedIndex = _ListBox.Items.Count - 1;
            }
        }

        #endregion
    }
}