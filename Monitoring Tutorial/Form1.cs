#region Copyright

// ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using Asm.As.Oib.Common.Utilities;
using Asm.As.Oib.Monitoring.Proxy.Architecture.Objects;
using Asm.As.Oib.Monitoring.Proxy.Business.EventArgs;
using Asm.As.Oib.Monitoring.Proxy.Business.Objects;
using Asm.As.Oib.WS.Eventing.Contracts.Data;
using Asm.As.Oib.WS.Eventing.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    /// Demo App to Monitoring Tutorial
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Form1 : Form
    { 
        #region Fields
        private Subscriber _subscriber;
        private ReliableReceiver _receiver;
        private Subscription _currentSubscription;
        private readonly System.Timers.Timer _timer = new System.Timers.Timer(TimeSpan.FromHours(1).TotalMilliseconds);
        #endregion

        #region Properties
        private DateTime NewExpiryDate
        {
            get { return DateTime.UtcNow + TimeSpan.FromDays(1); }
        }

        private string LastSubscriptionId
        {
            get { return UserSettings.Default.LastSubscriptionID; }
            set
            {
                _labelCurrentSubscriptionId.Text = UserSettings.Default.LastSubscriptionID = value;
                UserSettings.Default.Save();
            }
        }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <param name="uriKey">The URI key.</param>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="doReplaceHostName">if set to <c>true</c> [do replace host name].</param>
        /// <returns></returns>
        private static string GetUri(string uriKey, string hostName, bool doReplaceHostName)
        {
            string uri = EndpointHelper.GetAppSettingString(uriKey);
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(uriKey);
            }

            if (doReplaceHostName)
            {
                // look for a host name
                var match = Regex.Match(uri, @"\/\/([\w]+):", RegexOptions.IgnoreCase);

                if (match.Groups.Count > 1)
                {
                    // a host name found, do replace it
                    uri = uri.Replace(match.Groups[1].Value, hostName);
                }
            }

            return uri;
        }

        private string CallbackEndpoint
        {
            get
            {
                var uri = GetUri("Asm.As.Oib.Monitoring.Proxy.ReceiverReliable", EndpointHelper.MachineName, !_checkBoxUseAppConfig.Checked);

                // change a port number
                if (!_checkBoxUseAppConfig.Checked)
                    uri = Regex.Replace(uri, @":[\d]+\/", ":" + _numericUpDownPort.Value + "/", RegexOptions.IgnoreCase);

                return uri;
            }
        }

        private string SubscriptionManagerEndpoint
        {
            get
            {
                return GetUri("Asm.As.Oib.Monitoring.Proxy.SubscriptionManagerEndpoint", _textBoxCoreName.Text, !_checkBoxUseAppConfig.Checked);
            }
        }

        private string ServiceLocatorEndpoint
        {
            get
            {
                return GetUri("Asm.As.Oib.Proxy.Servicelocator", _textBoxCoreName.Text, !_checkBoxUseAppConfig.Checked);
            }
        }

        private int LastPortNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(UserSettings.Default.LastCallbackUri))
                {
                    Uri uri = new Uri(UserSettings.Default.LastCallbackUri);
                    return uri.Port;
                }
                return -1;
            }
        }
        #endregion

        #region Construction
        public Form1()
        {
            InitializeComponent();
            _textBoxCoreName.Text = EndpointHelper.MachineName;
            _timer.Elapsed += TimerOnElapsed;
            _buttonErrorCodes.Enabled = _buttonSubscribe.Enabled = _buttonUnsubscribe.Enabled = false;
            string executingDirectory = Assembly.GetExecutingAssembly().Location;
            foreach (string subDir in Directory.GetDirectories(Path.GetDirectoryName(executingDirectory)))
            {
                try
                {
                    string subDirName = new DirectoryInfo(subDir).Name;
                    var test = new CultureInfo(subDirName);
                    _comboBoxLanguage.Items.Add(subDirName);
                }
                catch 
                {
                    //
                }
            }
        }
        #endregion

        #region Helper Methods

        private List<string> GetMonitoringAdapters()
        {
            List<string> monitoringAdapters = new List<string>();
            try
            {
                // Add endpoint identity to provide information about service identity and open secure communication to service
                var address = new EndpointAddress(new Uri(ServiceLocatorEndpoint), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

                #region Client Authentication parameters

                // Parameters helper class that contains all the necessary information for binding and security settings

                // Option 1: This parameters indicates to use app.config central client authentication parameters
                var bindingParameters = new BindingParameters(address.ToString(), true);

                // Option 2: This parameters indicates to use other certificate for client authentication
                //var bindingParameters = new BindingParameters(address.ToString(), false)
                //{
                //    ClientCertificateParameters = new ServiceSecurityParameters
                //    {
                //        CertificateFindType = X509FindType.FindBySubjectName,
                //        CertificateStoreLocation = StoreLocation.CurrentUser,
                //        CertificateStoreName = StoreName.My,
                //        CertificateValue = "[Certificate Value used for OIB installation]"
                //    }
                //};

                #endregion

                ServiceLocatorClient slClient = new ServiceLocatorClient(EndpointHelper.CreateBindingFromParameters(bindingParameters), address);

                // Revocation list not used
                slClient.ChannelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    // Option 1: Use app.config central client authentication parameters
                    slClient.ChannelFactory.Credentials.ClientCertificate.SetCertificate(
                        bindingParameters.ClientCertificateParameters.CertificateStoreLocation,
                        bindingParameters.ClientCertificateParameters.CertificateStoreName,
                        bindingParameters.ClientCertificateParameters.CertificateFindType,
                        bindingParameters.ClientCertificateParameters.CertificateValue);

                    // Option 2: use other client authentication parameters
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //slClient.ChannelFactory.Credentials.ClientCertificate.SetCertificate(
                    //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
                }

                #endregion

                ServiceMatchCriteria criteria = new ServiceMatchCriteria{ServiceName = "SIPLACE.Monitoring"};
                ServiceDescription[] monitoringDescriptions = slClient.FindServices(criteria);
                foreach (ServiceDescription description in monitoringDescriptions)
                {
                    // This is a quick and dirty shortcut since we only use the computer name here and then 
                    // use a hard-coded endpoint pattern for the reliable tcp ASM endpoint
                    if (description.MetadataEndpoints.Length > 0)
                    {
                        Uri metadataEndpoint = description.MetadataEndpoints[0];
                        UriBuilder adapter = new UriBuilder(GetUri("Asm.As.Oib.Monitoring.Proxy.MonitoringEndpointReliable", _textBoxCoreName.Text, !_checkBoxUseAppConfig.Checked));
                        adapter.Host = metadataEndpoint.Host;
                        monitoringAdapters.Add(adapter.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                AddMessage("ERROR: While getting registered MonitoringAdapters from ServiceLocator: \n" + ex.Message);
            }
            return monitoringAdapters;
        }

        private void AddMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddMessage), message);
            }
            else
            {
                _listViewMessages.Items.Add(new ListViewItem(new [] {DateTime.Now.ToShortTimeString(), message}));
                _listViewMessages.EnsureVisible(_listViewMessages.Items.Count-1);
            }
        }

        #region DOC_MONITORING_START_RECEIVER
        private void StartReceiver()
        {
            if (_receiver == null)
            {
                if (_checkBoxUseAppConfig.Checked)
                {
                    // Passing in false here will force to use the same port again (important in case the 
                    // client process (here: this app) has died and wants to reconnect to an existing subscription
                    // Then the endpoint of the receiver must be exactly the same (including port number)
                    _receiver = new ReliableReceiver(false);
                }
                else
                {
                    // Create the tcp binding depending on port sharing enabled setting in GUI.
                    // Tcp port sharing only works in case the process is elevated or the current windows user is allowed to use port sharing (see SDK)
                    var binding = EndpointHelper.CreateBindingFromEndpointString(CallbackEndpoint, true, _cbPortSharing.Checked);
                    _receiver = new ReliableReceiver(new EndpointAddress(CallbackEndpoint), binding, false);
                }

                UserSettings.Default.LastCallbackUri = _labelCallbackUri.Text = _receiver.CallbackEndpointString;
                UserSettings.Default.Save();

                // Subscribe to events
                _receiver.BoardProcessedEventReceived += ReceiverOnBoardProcessedEventReceived;
                _receiver.ConfigurationChangedEventReceived += ReceiverOnConfigurationChangedEventReceived;
                _receiver.PassmodeEventReceived += ReceiverOnPassmodeEventReceived;
                _receiver.RecipeActivationChangeEventReceived += ReceiverOnRecipeActivationChangeEventReceived;
                _receiver.RecipeChangeEventReceived += ReceiverOnRecipeChangeEventReceived;
                _receiver.RecipeDownloadEventReceived += ReceiverOnRecipeDownloadEventReceived;
                _receiver.SetupUpdateEventReceived += ReceiverOnSetupUpdateEventReceived;
                _receiver.StationEventReceived += ReceiverOnStationEventReceived;
                _receiver.PickupErrorEventReceived += ReceiverOnPickupErrorEventReceived;
                _receiver.HeartBeatEventReceived += ReceiverOnHeartBeatEventReceived;
                _receiver.StationAvailablilityEventReceived += ReceiverOnStationAvailabilityEventReceived;
                _receiver.FeederErrorEventReceived += ReceiverOnFeederErrorEventReceived;
                _checkBoxUseAppConfig.Enabled = false;
            }
        }

   

        #endregion

        #region DOC_MONITORING_STOP_RECEIVER
        private void StopReceiver()
        {
            if (_receiver != null)
            {
                // Unsubscribe from events
                _receiver.BoardProcessedEventReceived -= ReceiverOnBoardProcessedEventReceived;
                _receiver.ConfigurationChangedEventReceived -= ReceiverOnConfigurationChangedEventReceived;
                _receiver.PassmodeEventReceived -= ReceiverOnPassmodeEventReceived;
                _receiver.RecipeActivationChangeEventReceived -= ReceiverOnRecipeActivationChangeEventReceived;
                _receiver.RecipeChangeEventReceived -= ReceiverOnRecipeChangeEventReceived;
                _receiver.RecipeDownloadEventReceived -= ReceiverOnRecipeDownloadEventReceived;
                _receiver.SetupUpdateEventReceived -= ReceiverOnSetupUpdateEventReceived;
                _receiver.StationEventReceived -= ReceiverOnStationEventReceived;
                _receiver.PickupErrorEventReceived -= ReceiverOnPickupErrorEventReceived;
                _receiver.HeartBeatEventReceived -= ReceiverOnHeartBeatEventReceived;
                _receiver.StationAvailablilityEventReceived -= ReceiverOnStationAvailabilityEventReceived;
                _receiver.FeederErrorEventReceived -= ReceiverOnFeederErrorEventReceived;

                _receiver.Dispose();
                _receiver = null;
                UserSettings.Default.LastCallbackUri = _labelCallbackUri.Text = string.Empty;
                UserSettings.Default.Save();
                CheckBoxUseAppConfigCheckedChanged(null, null);
            }
        }
        #endregion

        #region DOC_MONITORING_CREATE_SUBSCRIPTION
        private void CreateSubscription()
        {
            if (_currentSubscription == null)
            {
                SubscribeResult result;
                // We need to create a new subscription
                if (_checkBoxFilterLine.Checked)
                {
                    XPathFilterAdapter filterAdapter = new XPathFilterAdapter(XPathFilterDataType.LineFullPath, _textBoxLineFullPath.Text);
                    result = _subscriber.Subscribe(filterAdapter, _receiver.CallbackEndpointString, NewExpiryDate, DeliveryModeType.PushWithAck);
                }
                else
                {
                    result = _subscriber.Subscribe(_receiver.CallbackEndpointString, NewExpiryDate, DeliveryModeType.PushWithAck);
                }

                LastSubscriptionId = result.SubscriptionManager.Identifier.ToString();
                SubscriptionDescriptor search = new SubscriptionDescriptor();
                search.Id = result.SubscriptionManager.Identifier.ToString();
                Subscription[] foundSubscriptions = _subscriber.GetSubscriptions(search);
                if (foundSubscriptions != null && foundSubscriptions.GetLength(0) > 0)
                {
                    _currentSubscription = foundSubscriptions[0];
                }
            }
        }
        #endregion

        private void RenewSubscription()
        {
            if (_currentSubscription != null)
            {
                // We should renew the subscription (make it expire 1 day up to now)
                DateTime expiresDate = NewExpiryDate;
                RenewRequest renew = new RenewRequest(_currentSubscription.Manager.Identifier, new Renew(new Expires(expiresDate)));
                _subscriber.Renew(renew);
                _currentSubscription.Expires = expiresDate;
                LastSubscriptionId = _currentSubscription.Manager.Identifier.ToString();
            }
        }

        private void DeleteSubscription()
        {
            if (_currentSubscription != null)
            {
                try
                {
                    _subscriber.Unsubscribe(_currentSubscription.Manager.Identifier, _subscriber.DefaultTopic);
                }
                catch (Exception ex)
                {
                    // Maybe the subscription was deleted?
                    AddMessage("Unsubscribe got exception: " +ex);
                }
                _currentSubscription = null;
                LastSubscriptionId = string.Empty;
            }
        }

        #endregion

        #region Event Handler
        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                _timer.Enabled = false;

                // Renew the subscription
                RenewSubscription();
            }
            catch (Exception ex)
            {
                AddMessage("Got exception when renewing the subscription: " + ex);
            }
            finally
            {
                _timer.Enabled = true;
            }
        }


#region DOC_MONITORING_LOGGING_STATE_TRANSTIONS

        private void ReceiverOnStationEventReceived(object sender, StationEventArgs args)
        {
            AddMessage("ReceiverOnStationEventReceived: "+ args.StationEventComposite);

            if (args.StationEventComposite != null)
            {
                if (args.StationEventComposite.StateTransitions != null)
                {
                    foreach (var transition in args.StationEventComposite.StateTransitions)
                    {
                        AddMessage("  State Transition: Machine: " + args.StationEventComposite.Station.Name + 
                            " Processing area: " + transition.ProcessingArea + " into state " + transition.StationState);
                    }
                    
                }
            }
        }

#endregion 

        private void ReceiverOnSetupUpdateEventReceived(object sender, SetupCollectionEventArgs args)
        {
            AddMessage("ReceiverOnSetupUpdateEventReceived: " + args.SetupCollection);
        }

        private void ReceiverOnRecipeDownloadEventReceived(object sender, DownloadRecipeEventArgs args)
        {
            AddMessage("ReceiverOnRecipeDownloadEventReceived: " + args.DownloadRecipe);
        }

        private void ReceiverOnRecipeChangeEventReceived(object sender, RecipeChangeEventArgs args)
        {
            AddMessage("ReceiverOnRecipeChangeEventReceived: " + args.RecipeChange);
        }

        private void ReceiverOnRecipeActivationChangeEventReceived(object sender, RecipeActivationChangeEventArgs args)
        {
            AddMessage("ReceiverOnRecipeActivationChangeEventReceived: " + args.RecipeCollection);
        }

        private void ReceiverOnPassmodeEventReceived(object sender, PassmodeEventArgs args)
        {
            AddMessage("ReceiverOnPassmodeEventReceived: " + args.PassModeCollection);
        }

        private void ReceiverOnConfigurationChangedEventReceived(object sender, ConfigurationChangedEventArgs args)
        {
            AddMessage("ReceiverOnConfigurationChangedEventReceived: " + args.Configuration);
        }

        private void ReceiverOnBoardProcessedEventReceived(object sender, BoardProcessedEventArgs args)
        {
            AddMessage("ReceiverOnBoardProcessedEventReceived: " + args.BoardProcessedData);
        }

        private void ReceiverOnPickupErrorEventReceived(object sender, PickupErrorEventArgs args)
        {
            AddMessage("ReceiverOnPickupErrorEventReceived: " + args.PickupErrorEventData);
        }


        private void ReceiverOnHeartBeatEventReceived(object sender, HeartBeatEventArgs heartBeatEventArgs)
        {
            AddMessage("ReceiverOnHeartBeatEventReceived: " + heartBeatEventArgs.HeartBeatEventData + " with Stations: " + String.Join(", ", heartBeatEventArgs.HeartBeatEventData.Stations));
        }

        private void ReceiverOnFeederErrorEventReceived(object sender, FeederErrorEventArgs args)
        {
            AddMessage("ReceiverOnFeederErrorEventReceived: " + args.FeederErrorEventData);
        }

#region DOC_MONITORING_LOGGING_NEW_STATE_TRANSTIONS

        private void ReceiverOnStationAvailabilityEventReceived(object sender, StationAvailabilityEventArgs args)
        {
            AddMessage("ReceiverOnStationAvailabilityEventReceived. " + args.StationAvailabilityCollection);

            if (args.StationAvailabilityCollection != null)
            {
                {
                    foreach (var state in args.StationAvailabilityCollection.ProcessingLocationAvailabilityStates)
                    {
                        AddMessage("New State Transitions : Machine: " + args.StationAvailabilityCollection.Station.Name +
                                   " Processing area: " + state.ProcessingLocationId + " from state " + state.PreviousStateCodeMes + " into state " + state.CurrentStateCodeMes
                                   + " at " + state.CurrentStateBegin.ToLocalTime());
                        foreach (StateChangeTrigger trigger in state.StateChangeTriggers)
                        {
                            AddMessage("Trigger at " + trigger.TimeStamp.ToLocalTime() + " with "  + trigger.StateChangeReasons.Count + " reasons:");
                            foreach (StateChangeReason reason in trigger.StateChangeReasons)
                            {
                                AddMessage("AssistId: " + reason.AssistId + " ErrorNo: " + reason.ErrorNo);
                            }
                        }
                    }
                }
            }
        }

#endregion

        private void ButtonSubscribeClick(object sender, EventArgs e)
        {
            StartReceiver();
            CreateSubscription();
            _numericUpDownPort.Enabled = _buttonSubscribe.Enabled = false;
            _buttonUnsubscribe.Enabled = true;
            _timer.Enabled = true;
        }

        private void ButtonUnsubscribeClick(object sender, EventArgs e)
        {
            DeleteSubscription();
            _numericUpDownPort.Enabled = _buttonSubscribe.Enabled = true;
            _buttonUnsubscribe.Enabled = false;
            _timer.Enabled = false;
            StopReceiver();
        }

        private void ButtonInitializeClick(object sender, EventArgs e)
        {
            try
            {
                #region DOC_MONITORING_CREATE_SUBSCRIBER

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //- used with Option 2 (see below)
                    //Subscriber.SetCertificateForClientAuthentication(
                    //    StoreLocation.CurrentUser, X509FindType.FindBySubjectName, StoreName.My, "[Certificate Value used for OIB installation]");
                }

                #endregion

                if (_checkBoxUseAppConfig.Checked)
                {
                    _subscriber = new Subscriber();
                }
                else
                {
                    string spSM = SubscriptionManagerEndpoint;

                    #region Client Authentication parameters

                    // Parameters helper class that contains all the necessary information for binding and security settings

                    // Option 1: This parameters indicates to use app.config central client authentication parameters
                    var bindingParameters = new BindingParameters(spSM, true);

                    // Option 2: This parameters indicates to use other certificate for client authentication
                    //var bindingParameters = new BindingParameters(address.ToString(), false)
                    //{
                    //    ClientCertificateParameters = new ServiceSecurityParameters
                    //    {
                    //        CertificateFindType = X509FindType.FindBySubjectName,
                    //        CertificateStoreLocation = StoreLocation.CurrentUser,
                    //        CertificateStoreName = StoreName.My,
                    //        CertificateValue = "[Certificate Value used for OIB installation]"
                    //    }
                    //};

                    #endregion

                    _subscriber = new Subscriber(
                        new EndpointAddress(new Uri(spSM), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service")), EndpointHelper.CreateBindingFromParameters(bindingParameters));
                }

                #endregion

                #region DOC_MONITORING_LAST_SUBSCRIPTION_ID

                string lastSubsciptionId = LastSubscriptionId;
                if (!string.IsNullOrEmpty(lastSubsciptionId))
                {
                    // See if there is still a subscription with this ID.
                    // If yes, then renew it...
                    SubscriptionDescriptor search = new SubscriptionDescriptor();
                    search.Id = lastSubsciptionId;
                    Subscription[] foundSubscriptions = _subscriber.GetSubscriptions(search);
                    if (foundSubscriptions != null && foundSubscriptions.GetLength(0) > 0)
                    {
                        // Check that the subscription that we found is using the same callback
                        Uri foundCallbackUri = foundSubscriptions[0].Delivery.EndpointSerialiazable.ToEndpointAddress().Uri;
                        if (foundCallbackUri.ToString() != CallbackEndpoint)
                        {
                            MessageBox.Show(
                                $@"A subscription with Identifier:{lastSubsciptionId} was found in Eventing which uses a different callback than specified! Please manually delete the old subscription!.");
                        }
                        else
                        {
                            _currentSubscription = foundSubscriptions[0];
                        }
                    }
                }
                #endregion

                #region DOC_MONITORING_LAST_SUBSCRIPTION_CALLBACK
                else
                {
                    // Alternative 2, use the callback URI and the topic to get the existing subscription
                    SubscriptionDescriptor search = new SubscriptionDescriptor();
                    search.Topic = _subscriber.DefaultTopic;
                    search.Endpoint = EndpointAddressAugust2004.FromEndpointAddress(new EndpointAddress(CallbackEndpoint));
                    Subscription[] foundSubscriptions = _subscriber.GetSubscriptions(search);
                    if (foundSubscriptions != null && foundSubscriptions.GetLength(0) > 0)
                    {
                        _currentSubscription = foundSubscriptions[0];
                    }
                }
                #endregion

                int lastPortNumber = LastPortNumber;
                if (lastPortNumber != -1)
                {
                    _numericUpDownPort.Value = lastPortNumber;
                }

                #region DOC_MONITORING_SUBSCRIPTION_FOUND
                if (_currentSubscription != null)
                {
                    StartReceiver();
                    RenewSubscription();
                    _labelCurrentSubscriptionId.Text = _currentSubscription.Manager.Identifier.ToString();
                    _numericUpDownPort.Enabled = _buttonSubscribe.Enabled = false;
                    _buttonUnsubscribe.Enabled = true;
                    _timer.Enabled = true;
                }
                else
                {
                    _numericUpDownPort.Enabled = _buttonSubscribe.Enabled = true;
                    _buttonUnsubscribe.Enabled = false;
                }
                #endregion

                _textBoxCoreName.Enabled = _buttonInitialize.Enabled = false;

                _listBoxAdapters.Items.Clear();
                foreach (string monitoringAdapterEP in GetMonitoringAdapters())
                {
                    _listBoxAdapters.Items.Add(monitoringAdapterEP);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error happened when connecting to core: " + ex);
            }
        }

        private void Form1Closing(object sender, FormClosingEventArgs e)
        {
            if (_checkBoxUnsubscribe.Checked)
            {
                DeleteSubscription();
            }
            _timer.Enabled = false;
            StopReceiver();
        }

        private void CheckBoxUseAppConfigCheckedChanged(object sender, EventArgs e)
        {
            _numericUpDownPort.Enabled = !_checkBoxUseAppConfig.Checked;
        }

        private void CheckBoxFilterLineCheckedChanged(object sender, EventArgs e)
        {
            _textBoxLineFullPath.Enabled = _checkBoxFilterLine.Checked;
        }

        private void ListBoxAdaptersSelectedIndexChanged(object sender, EventArgs e)
        {
            _buttonErrorCodes.Enabled = _buttonGetConfiguration.Enabled = _listBoxAdapters.SelectedItem != null;
        }

        private void ButtonGetConfigurationClick(object sender, EventArgs e)
        {
            try
            {
                string monitoringAdapterEndpoint = _listBoxAdapters.SelectedItem.ToString();
                EndpointAddress epa = new EndpointAddress(monitoringAdapterEndpoint);
                Monitoring monitoringInstance = MonitoringManager.GetOrCreateInstance(epa, EndpointHelper.CreateBindingFromEndpointString(monitoringAdapterEndpoint, true, true));
                Configuration config = monitoringInstance.GetConfiguration();
                AddMessage("Configuration: " + config);
            }
            catch (Exception ex)
            {
                AddMessage("ERROR: Got exception while calling GetConfiguration(): " + ex.Message);
            }
        }

        private void ButtonErrorCodesClick(object sender, EventArgs e)
        {

            #region DOC_MONITORING_READ_MACHINE_ERROR_STRINGS
            var saveFileDialog1 = new SaveFileDialog();

            try
            {
                string monitoringAdapterEndpoint = _listBoxAdapters.SelectedItem.ToString();
                EndpointAddress epa = new EndpointAddress(monitoringAdapterEndpoint);
                Monitoring monitoringInstance = MonitoringManager.GetOrCreateInstance(epa, EndpointHelper.CreateBindingFromEndpointString(monitoringAdapterEndpoint, true, true));

                var builder = new StringBuilder("MachineErrorCodes:\n");
                foreach (var kvp in monitoringInstance.MachineErrorStrings)
                {
                    builder.AppendFormatSafeLine("{0}: \t\t\tDescription: '{1}'", kvp.Key.ToString("D5"), kvp.Value);
                }
                saveFileDialog1.Filter = @"txt files (*.txt)|*.txt";
                saveFileDialog1.FileName = "ErrorStrings-" + _comboBoxLanguage.SelectedItem + ".txt";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, builder.ToString());
                }

            }
            catch (Exception ex)
            {
                AddMessage("ERROR: Got exception while saving File '" + saveFileDialog1.FileName + "', exception: " + ex.Message);
            }
            #endregion
        }

        private void ComboBoxLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            // Switch to whatever language you want. If the satellite assembly is installed in the sub-folder of where  the Monitoring Proxy is located, 
            // then you get translated descriptions.
            // Default language is English

            // You can only set this once, somehow... apparently the satellite assembly will not be loaded again once it is set.
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_comboBoxLanguage.SelectedItem.ToString());
            _comboBoxLanguage.Enabled = false;
        }

        #endregion
        
    }
}