#region Copyright
// Works OIB - Copyright (C) ASMPT GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASMPT and are supplied subject to license terms.
#endregion

#region using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows.Forms;
#region DOC_DECLARE_USING
using Asm.As.Oib.DisplayService.Contracts.Data;
using Asm.As.Oib.DisplayService.Contracts.Messages;
using Asm.As.Oib.DisplayService.Contracts.Data.Types;
using Asm.As.Oib.DisplayService.Proxy.Architecture.Objects;
#endregion

#endregion

namespace DSTestClient
{
    public partial class Form1 : Form
    {
        #region Structs

        public struct SubAnswerItem
        {
            public string Text;
            public bool Editable;

            public override string ToString() { return Text; }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Remember the registered client
        /// </summary>
        private DisplayServiceClient _client;
        
        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            _lbStation.Visible = _lbLine.Visible = _tbLine.Visible = _tbStation.Visible = _lbDbId.Visible = _tbDbId.Visible = false;
        }

        #endregion

        #region Event Handlers

        #region DOC_REGISTER_CLIENT
        /// <summary>
        /// Handles the Click event of the Connect and Register button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BConnectRegisterClick(object sender, EventArgs e)
        {
            try
            {
                if (_client != null)
                {
                    _client.Dispose();
                    _client = null;
                }
                if (string.IsNullOrEmpty(_tbClientRegId.Text))
                {
                    //either use the app config settings (if check box is checked) or use programmatically defined parameters
                    _client = !_cbAppConfig.Checked
                                  ? new DisplayServiceClient(_tbComputerName.Text, 1406, _tbClientName.Text, 5555,
                                                             Guid.Empty, false, 0, _cbUnregister.Checked)
                                  : new DisplayServiceClient();
                }
                else
                {
                    var id = new Guid(_tbClientRegId.Text);
                    //either use the app config settings (if check box is checked) or use programmatically defined parameters
                    _client = !_cbAppConfig.Checked
                                  ? new DisplayServiceClient(_tbComputerName.Text, 1406, _tbClientName.Text, 5555, id,
                                                             false, 0, _cbUnregister.Checked)
                                  : new DisplayServiceClient(id);
                }

                #region Use Client Authentication

                //Usage of Client Authentication
                if (!_cbAppConfig.Checked && _cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //DisplayServiceClient.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                InitializeDsClient();
                _statusLabel.Text = @"Registered client: " + _client.ClientRegistrationId;
                _tbClientRegId.Text = _client.ClientRegistrationId.ToString();
                BCheckAliveClick(null, null);
                //Clipboard.SetText(_tbClientRegId.Text);

                RefreshViewers();

            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(this, @"Endpoint not found.", @"Exception", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, @"Exception");
            }
        }
        #endregion

        /// <summary>
        /// Handles the display service connected event
        /// </summary>
        private void ClientServiceConnected(string strserviceendpoint, string strcomment)
        {
            b_CheckAlive.BackColor = Color.Green;
        }

        /// <summary>
        /// Handles the display service unreachable event
        /// </summary>
        private void ClientServiceUnreachable(string strserviceendpoint, string strcomment)
        {
            b_CheckAlive.BackColor = Color.Red;
        }

        #region DOC_CONF_REC
        /// <summary>
        /// Handles the confirmation received event
        /// </summary>
        private void ClientConfirmationReceived(ConfirmationReceivedRequest confirmation)
        {
            if (confirmation.ThisViewerDeleted)
            {
                _rbConfirmation.Text = string.Format("The viewer just got unregistered: {0}, MessageId: {1}", confirmation.Viewer.ComputerName, confirmation.OriginalMessage.MessageGUID); 
            }
            else
            {
                if (confirmation.SubAnswer != null)
                {
                    _rbConfirmation.Text =
                        string.Format(
                            "Viewer: {0} sent Answer: {1}(AnswerID: {2}), SubAnswer: {3}(SubAnswerID:{4}), MessageId: {5}",
                            confirmation.Viewer.ComputerName, confirmation.Answer.AnswerText,
                            confirmation.Answer.AnswerID, confirmation.SubAnswer.SubAnswerText, confirmation.SubAnswer.SubAnswerID,
                            confirmation.OriginalMessage.MessageGUID);
                }
                else
                {
                    _rbConfirmation.Text =
                        string.Format(
                            "Viewer: {0} sent Answer: {1}(AnswerID: {2}), MessageId: {3}",
                            confirmation.Viewer.ComputerName, confirmation.Answer.AnswerText,
                            confirmation.Answer.AnswerID, confirmation.OriginalMessage.MessageGUID);
                }
            }

            if (confirmation.MessageDeleted)
            {
                _cbMessages.Items.Remove(confirmation.OriginalMessage.MessageGUID);
            }

            if (_cbMessages.Items.Count > 0)
            {
                _cbMessages.SelectedIndex = 0;
            }
            else
            {
                _cbMessages.Text = "";
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the Force Garbage Collection button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        // ReSharper disable MemberCanBeMadeStatic.Local
        private void BForceGcClick(object sender, EventArgs e)
        // ReSharper restore MemberCanBeMadeStatic.Local
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        /// Handles the Click event of the Send Message button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BSendMessageClick(object sender, EventArgs e)
        {
            try
            {
                _rbErrorMessage.Text = string.Empty;
                if (_client != null)
                {
                    var answers = new List<Answer>();

                    if (_cbUseAnswers.Checked)
                    {
                        foreach (string answer1 in _lbAnswers.Items)
                        {
                            var aw = new Answer(answer1, Guid.NewGuid());
                            answers.Add(aw);
                            if (_cbUseSubAnswers.Checked)
                            {
                                foreach (SubAnswerItem item in _lbSubAnswers.Items)
                                {
                                    var subanswer1 = new SubAnswer(item.Text, Guid.NewGuid()) { TextEditable = item.Editable };
                                    aw.SubAnswersNew.Add(subanswer1);
                                }
                            }
                        }
                    }

                    _rbConfirmation.Text = "";
                    #region DOC_ACK_TYPE
                    //set AcknowledgementType
                    AcknowledgementType ack;
                    if (_rbAckAllViewers.Checked)
                    {
                        ack = AcknowledgementType.AllReceivers;
                    }
                    else if (_rbAckOneViewer.Checked)
                    {
                        ack = AcknowledgementType.OneReceiver;
                    }
                    else
                    {
                        ack = AcknowledgementType.Originator;
                    }
                    #endregion
                    //set SeverityLevel
                    SeverityLevel level;
                    if (_rbSevError.Checked)
                    {
                        level = SeverityLevel.Error;
                    }
                    else if (_rbSevInfo.Checked)
                    {
                        level = SeverityLevel.Info;
                    }
                    else if (_rbSevNone.Checked)
                    {
                        level = SeverityLevel.None;
                    }
                    else
                    {
                        level = SeverityLevel.Warning;
                    }

                    SendMessageResponse messageResponse;
                    #region DOC_SENDMSG_EXPLICIT
                    // send the message explicit
                    if (_rbExplicit.Checked)
                    {

                        List<ViewerRegistration> list = GetSelectedViewers();

                        if (list.Count == 0)
                        {
                            MessageBox.Show(@"There are no viewers registered currently, canceling the send!");
                            return;
                        }
                        messageResponse = _client.SendMessageExplicit(list, _rbMessage.Text,
                                                                                          _rbExtDescription.Text,
                                                                                          ack,
                                                                                          _cbCallbackRequested.Checked,
                                                                                          (int)_nudPriority.Value,
                                                                                          level, answers, (int)_nudDefaultAnswerIndex.Value, 
                                                                                          _cbExpandExtendedDescription.Checked);

                     
                    }
                    #endregion  
                    // send the message by line path to all stations
                    else if (_rbLineAll.Checked)
                    {
                        if (string.IsNullOrEmpty(_tbLine.Text))
                        {
                            MessageBox.Show(@"You need to specify a valid line path, canceling the send!");
                            return;
                        }
                        messageResponse = _client.SendMessageByLinePathAllStations(_tbLine.Text, _rbMessage.Text,
                                                                                          _rbExtDescription.Text,
                                                                                          ack,
                                                                                          _cbCallbackRequested.Checked,
                                                                                          (int)_nudPriority.Value,
                                                                                          level, answers, null, (int)_nudDefaultAnswerIndex.Value,
                                                                                          _cbExpandExtendedDescription.Checked, _cbNonStationViewers.Checked);
                    }
                    // send the message by line path to the non-station viewer that is registered for that line
                    else if (_rbLineNo.Checked)
                    {
                        if (string.IsNullOrEmpty(_tbLine.Text))
                        {
                            MessageBox.Show(@"You need to specify a valid line path, canceling the send!");
                            return;
                        }
                        messageResponse = _client.SendMessageByLinePathNoStations(_tbLine.Text, _rbMessage.Text,
                                                                                          _rbExtDescription.Text,
                                                                                          ack,
                                                                                          _cbCallbackRequested.Checked,
                                                                                          (int)_nudPriority.Value,
                                                                                          level, answers, null, (int)_nudDefaultAnswerIndex.Value,
                                                                                          _cbExpandExtendedDescription.Checked);
                    }
                    #region DOC_SENDMSG_FIRSTSTATION
                    // send the message by line path to the first station
                    else if (_rbLineFirst.Checked)
                    {
                        if (string.IsNullOrEmpty(_tbLine.Text))
                        {
                            MessageBox.Show(@"You need to specify a valid line path, canceling the send!");
                            return;
                        }
                        messageResponse = _client.SendMessageByLinePathFirstStation(_tbLine.Text, _rbMessage.Text,
                                                                                          _rbExtDescription.Text,
                                                                                          ack,
                                                                                          _cbCallbackRequested.Checked,
                                                                                          (int)_nudPriority.Value,
                                                                                          level, answers, null, (int)_nudDefaultAnswerIndex.Value,
                                                                                          _cbExpandExtendedDescription.Checked, _cbNonStationViewers.Checked);
                    }
                    #endregion 
                    // send the message by station path
                    else
                    {
                        if (string.IsNullOrEmpty(_tbStation.Text))
                        {
                            MessageBox.Show(@"You need to specify a valid station path, canceling the send!");
                            return;
                        }
                        messageResponse = _client.SendMessageByStationPath(_tbStation.Text, _rbMessage.Text,
                                                                                      _rbExtDescription.Text,
                                                                                      ack,
                                                                                      _cbCallbackRequested.Checked,
                                                                                      (int)_nudPriority.Value,
                                                                                      level, answers, null, (int)_nudDefaultAnswerIndex.Value,
                                                                                          _cbExpandExtendedDescription.Checked);
                    }

                    int index = _cbMessages.Items.Add(messageResponse.Message.MessageGUID);
                    _cbMessages.SelectedIndex = index;

                    string errorMessage = string.Empty;
                    foreach (var detail in messageResponse.DeliveryDetails)
                    {
                        if (!detail.DeliveredSuccessfully)
                        {
                            errorMessage += string.Format("Viewer {0} could not be reached: {1}", detail.ViewerRegistration, detail.ExecptionMessage);
                        }
                    }
                    if (errorMessage != string.Empty)
                    {
                        _rbErrorMessage.Text = errorMessage;
                    }
                    else
                    {
                        _statusLabel.Text = @"Sent message " + messageResponse.Message.MessageGUID + @" successfully.";
                    }
                }
                else
                {
                    MessageBox.Show(this, @"No client registration available.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, @"Exception");
            }
        }

        /// <summary>
        /// Handles the Click event of the Update Message button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BUpdateMessageClick(object sender, EventArgs e)
        {
            try
            {
                _rbErrorMessage.Text = string.Empty;
                if (_client != null && !string.IsNullOrEmpty(_cbMessages.Text))
                {
                    //set SeverityLevel
                    SeverityLevel level;
                    if (_rbSevError.Checked)
                    {
                        level = SeverityLevel.Error;
                    }
                    else if (_rbSevInfo.Checked)
                    {
                        level = SeverityLevel.Info;
                    }
                    else if (_rbSevNone.Checked)
                    {
                        level = SeverityLevel.None;
                    }
                    else
                    {
                        level = SeverityLevel.Warning;
                    }

                    #region DOC_UPDATE_MSG
                    string msg = _cbMessages.Text;
                    List<MessageDeliveryDetails> updateMessageResponse = _client.UpdateMessageEx(msg, _rbMessage.Text, _rbExtDescription.Text, _cbExpandExtendedDescription.Checked, (int)_nudPriority.Value, level);

                    string errorMessage = string.Empty;
                    foreach (var detail in updateMessageResponse)
                    {
                        if (!detail.DeliveredSuccessfully)
                        {
                            errorMessage += string.Format("Viewer {0} could not be reached: {1}", detail.ViewerRegistration, detail.ExecptionMessage);
                        }
                    }
                    if (errorMessage != string.Empty)
                    {
                        _rbErrorMessage.Text = errorMessage;
                    }
                    else
                    {
                        _statusLabel.Text = @"Updated message " + msg + @" successfully.";
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, @"Exception");
            }
        }

        /// <summary>
        /// Handles the Click event of the Revoke Message button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BRevokeMessageClick(object sender, EventArgs e)
        {
            try
            {
                _rbErrorMessage.Text = string.Empty;
                if (_client != null && _cbMessages.SelectedItem != null)
                {
                    #region DOC_REVOKE_MSG
                    var msg = (string)_cbMessages.SelectedItem;
                    bool revoked = _client.RevokeMessage(msg);
                    if (!revoked)
                    {
                        _cbMessages.Items.Remove(msg);
                        if (_cbMessages.Items.Count > 0)
                        {
                            _cbMessages.SelectedIndex = 0;
                        }
                        else
                        {
                            _cbMessages.Text = "";
                        }
                        _rbErrorMessage.Text = @"Message could not be revoked";
                    }
                    else
                    {
                        _cbMessages.Items.Remove(msg);
                        if (_cbMessages.Items.Count > 0)
                        {
                            _cbMessages.SelectedIndex = 0;
                        }
                        else
                        {
                            _cbMessages.Text = "";
                        }
                        _statusLabel.Text = @"Revoked message " + msg + @" successfully.";
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, @"Exception");
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Use Sub Answer check box
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void _cbUseSubAnswers_CheckedChanged(object sender, EventArgs e)
        {
            _lbSubAnswers.Enabled = _btnAddSubAnswer.Enabled = _btnRemoveSubAnswer.Enabled = _cbUseSubAnswers.Checked;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the Sub Answer list box.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void _lbSubAnswers_SelectedIndexChanged(Object sender, EventArgs e)
        {
            int i = _lbSubAnswers.SelectedIndex;
            if ((i >= 0) && (i < _lbSubAnswers.Items.Count))
            {
                var item = (SubAnswerItem)_lbSubAnswers.Items[i];
                _cbxEditableSubAnswer.Checked = item.Editable;
            }
        }

        /// <summary>
        /// Handles the Click event of the Add Sub Answer button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void _btnAddSubAnswer_Click(object sender, EventArgs e)
        {
            var item = new SubAnswerItem { Text = _tbSubAnswer.Text, Editable = _cbxEditableSubAnswer.Checked };
            _lbSubAnswers.Items.Add(item);
        }

        /// <summary>
        /// Handles the Click event of the Remove Sub Answer button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void _btnRemoveSubAnswer_Click(object sender, EventArgs e)
        {
            int index = _lbSubAnswers.SelectedIndex;
            if (index != -1)
            {
                _lbSubAnswers.Items.RemoveAt(index);
            }
        }

        /// <summary>
        /// Handles the click event of the 'editable sub-answer' check box
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void _cbxEditableSubAnswer_CheckedChanged(Object sender, EventArgs e)
        {
            int i = _lbSubAnswers.SelectedIndex;
            if ((i >= 0) && (i < _lbSubAnswers.Items.Count))
            {
                var item = (SubAnswerItem) _lbSubAnswers.Items[i];
                item.Editable = _cbxEditableSubAnswer.Checked;
                _lbSubAnswers.BeginUpdate();  
                _lbSubAnswers.Items[i] = item;
                _lbSubAnswers.EndUpdate();
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Use Answer check box
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CbUseAnswerCheckedChanged(object sender, EventArgs e)
        {
            _lbAnswers.Enabled = _btnAddAnswer.Enabled = _btnRemoveAnswer.Enabled = _cbUseAnswers.Checked;
        }

        /// <summary>
        /// Handles the Click event of the Add Answer button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BAddAnswerClick(object sender, EventArgs e)
        {
            _lbAnswers.Items.Add(_tbAnswer.Text);
        }

        /// <summary>
        /// Handles the Click event of the Remove Answer button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BRemoveAnswerClick(object sender, EventArgs e)
        {
            int index = _lbAnswers.SelectedIndex;
            if (index != -1)
            {
                _lbAnswers.Items.RemoveAt(index);
            }
        }

        /// <summary>
        /// Handles the Click event of the Check Alive button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BCheckAliveClick(object sender, EventArgs e)
        {
            if (_client != null)
            {
                b_CheckAlive.BackColor = _client.IsServiceAlive ? Color.Green : Color.Red;
            }
        }

        /// <summary>
        /// Handles the Click event of the Refresh button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BRefreshClick(object sender, EventArgs e)
        {
            if (_client == null)
            {
                MessageBox.Show(@"No client registered at the moment.", @"No registration", MessageBoxButtons.OK);
            }
            else
            {
                RefreshViewers();
            }
        }


        /// <summary>
        /// Handles the Click event of the Viewer Identification check boxes
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RbViewerIdentificationCheckedChanged(object sender, EventArgs e)
        {
            if (_rbExplicit.Checked)
            {
                _lbStation.Visible = _lbLine.Visible = _tbLine.Visible = _tbStation.Visible = _lbDbId.Visible = _tbDbId.Visible = false;
                _cbNonStationViewers.Visible = _lbNonStation.Visible = false;
                _cbNonStationViewers.Checked = false;
            }
            else if (_rbLineAll.Checked || _rbLineFirst.Checked || _rbLineNo.Checked)
            {
                _lbLine.Visible = _tbLine.Visible = _lbDbId.Visible = _tbDbId.Visible = true;
                _lbStation.Visible = _tbStation.Visible = false;
                _cbNonStationViewers.Visible = _lbNonStation.Visible = true;
                _cbNonStationViewers.Checked = true;
                if (_rbLineNo.Checked)
                {
                    _cbNonStationViewers.Enabled = false;
                    _cbNonStationViewers.Checked = true;
                }
                else
                {
                    _cbNonStationViewers.Enabled = true;
                }
            }
            else
            {
                _lbLine.Visible = _tbLine.Visible = false;
                _lbStation.Visible = _tbStation.Visible = _lbDbId.Visible = _tbDbId.Visible = true;
                _cbNonStationViewers.Visible = _lbNonStation.Visible = false;
                _cbNonStationViewers.Checked = false;
            }
        }

        /// <summary>
        /// Handles the Selection event of the viewer list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvAllViewersSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lvAllViewers.SelectedIndices.Count > 0)
            {
                var viewer = _lvAllViewers.SelectedItems[0].Tag as ViewerRegistration;
                if (viewer != null)
                {
                    _tbLine.Text = viewer.SIPLACEProLinePath;
                    _tbStation.Text = viewer.SIPLACEProStationPath;
                    _tbDbId.Text = viewer.SIPLACEProDatabaseId;
                }
            }
        }

        /// <summary>
        /// Handles the Form Closing event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Form1FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_client != null)
                {
                    // We should ALWAYS call Dispose when we are done with a Disposable object!
                    _client.Dispose();
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }
       
        #endregion

        #region private Methods

        /// <summary>
        /// Refreshes the viewers list
        /// </summary>
        private void RefreshViewers()
        {
            try
            {
                #region DOC_GET_VIEWER
                List<ViewerRegistration> viewerRegistrations = _client.GetAllViewerRegistrations();
                #endregion
                _lvAllViewers.BeginUpdate();
                _lvAllViewers.Focus();
                _lvAllViewers.Items.Clear();

                foreach (ViewerRegistration viewer in viewerRegistrations)
                {
                    var item = new ListViewItem(new[]
                                                                 {
                                                                     viewer.ComputerName,  
                                                                     viewer.SIPLACEProLinePath,
                                                                     viewer.SIPLACEProStationPath,
                                                                     viewer.SIPLACEProStationIndexInLine.ToString(
                                                                         CultureInfo.InvariantCulture),
                                                                     viewer.ViewerGUID
                                                                 }) { Tag = viewer };

                    item = _lvAllViewers.Items.Add(item);
                    item.Selected = true;
                }
                _lvAllViewers.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, @"Exception");
            }
        }

        /// <summary>
        /// Gets the selected viewers
        /// </summary>
        /// <returns></returns>
        private List<ViewerRegistration> GetSelectedViewers()
        {
            var ret = new List<ViewerRegistration>();
            foreach (ListViewItem selectedItem in _lvAllViewers.SelectedItems)
            {
                var viewer = selectedItem.Tag as ViewerRegistration;
                if (viewer != null)
                {
                    ret.Add(viewer);
                }
            }
            return ret;
        }

        #region DOC_SUBSCRIBE
        /// <summary>
        /// Subscribe for events
        /// </summary>
        private void InitializeDsClient()
        {
            _client.ServiceConnected += ClientServiceConnected;
            _client.ServiceUnreachable += ClientServiceUnreachable;
            _client.ConfirmationReceived += ClientConfirmationReceived;
        }
        #endregion

       


        #endregion



    }
}
