#region Copyright

// ASM OIB - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASMand are supplied subject to license terms.

#endregion

#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using System.Text;
using schemas.xmlsoap.org.ws._2004._08.eventing;
using Asm.As.Oib.Common.Utilities;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using www.siplace.com.OIB._2019._11.MaintenanceData.Contracts.Service;
using www.siplace.com.OIB._2019_11.MaintenanceData.Contracts.Data;
using ServiceBinding = System.ServiceModel.Channels.Binding;
using Asm.As.Oib.SiplacePro.Proxy;
#endregion

namespace MaintenanceDataTutorial
{
    /// <summary>
    /// Demo App to Monitoring Tutorial
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainForm : Form
    {
        private const string DefaultTopic = "ASM:OIB:MaintenanceData:Notify";

        #region Fields

        private Subscription _currentSubscription;
        private MaintenanceDataEventingNotifyDuplex _receiver;
        private readonly System.Timers.Timer _timer = new System.Timers.Timer(TimeSpan.FromHours(1).TotalMilliseconds);
        private ServiceHost _mdiServiceHost;

        #endregion

        #region Properties

        private DateTime NewExpiryDate => DateTime.UtcNow + TimeSpan.FromDays(1);

        private string LastSubscriptionId
        {
            get => UserSettings.Default.LastSubscriptionID;
            set
            {
                _labelCurrentSubscriptionId.Text = UserSettings.Default.LastSubscriptionID = value;
                UserSettings.Default.Save();
            }
        }

        private string CallbackEndpoint
        {
            get
            {
                var uri = Helper.GetUri("Asm.As.Oib.MaintenanceData.Proxy.ReceiverReliable", EndpointHelper.MachineName,
                    !_checkBoxUseAppConfig.Checked);

                // change a port number
                if (!_checkBoxUseAppConfig.Checked)
                    uri = Regex.Replace(uri, @":[\d]+\/", ":" + _numericUpDownPort.Value + "/",
                        RegexOptions.IgnoreCase);

                return uri;
            }
        }

        private string SubscriptionManagerEndpoint => Helper.GetUri(
            "Asm.As.Oib.MaintenanceData.Proxy.SubscriptionManagerEndpoint", _textBoxCoreName.Text,
            !_checkBoxUseAppConfig.Checked);

        private string ServiceLocatorEndpoint => Helper.GetUri("Asm.As.Oib.Proxy.Servicelocator", _textBoxCoreName.Text,
            !_checkBoxUseAppConfig.Checked);

        private int LastPortNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(UserSettings.Default.LastCallbackUri))
                {
                    var uri = new Uri(UserSettings.Default.LastCallbackUri);
                    return uri.Port;
                }

                return -1;
            }
        }

        #endregion

        #region Construction

        public MainForm()
        {
            InitializeComponent();
            _textBoxCoreName.Text = EndpointHelper.MachineName;
            _timer.Elapsed += TimerOnElapsed;
            _buttonSubscribe.Enabled = false;
            _buttonUnsubscribe.Enabled = false;
        }

        #endregion

        #region Helper Methods

        private List<string> GetMaintenanceDataServiceEndpoints()
        {
            var mdiEndpoints = new List<string>();
            try
            {
                #region Client Authentication parameters

                // Parameters helper class that contains all the necessary information for binding and security settings

                // Option 1. This parameters indicates to use app.config central client authentication parameters
                var bindingParameters = new BindingParameters(ServiceLocatorEndpoint, true);

                // Option 2. This parameters indicates to use other certificate for client authentication
                //var bindingParameters = new BindingParameters(ServiceLocatorEndpoint, false)
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

                // Add endpoint identity to provide information about service identity and open secure communication to service
                var slClient = new ServiceLocatorClient(
                    EndpointHelper.CreateBindingFromParameters(bindingParameters), new EndpointAddress(new Uri(ServiceLocatorEndpoint), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service")));

                // Revocation list not used
                slClient.ChannelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    
                    // Option 1. Use app.config central client authentication parameters
                    slClient.ChannelFactory.Credentials.ClientCertificate.SetCertificate(
                        bindingParameters.ClientCertificateParameters.CertificateStoreLocation,
                        bindingParameters.ClientCertificateParameters.CertificateStoreName,
                        bindingParameters.ClientCertificateParameters.CertificateFindType,
                        bindingParameters.ClientCertificateParameters.CertificateValue);

                    // Option 2. use other client authentication parameters
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //slClient.ChannelFactory.Credentials.ClientCertificate.SetCertificate(
                    //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation] ");
                }

                #endregion

                var criteria = new ServiceMatchCriteria {ServiceName = "SIPLACE.Monitoring"};
                var monitoringDescriptions = slClient.FindServices(criteria);
                foreach (var description in monitoringDescriptions)
                {
                    foreach (var metadataEndpoint in description.MetadataEndpoints)
                    {
                        if (!metadataEndpoint.AbsoluteUri.Contains(
                            "Asm.As.Oib.Monitoring.Services/MaintenanceDataService"))
                        {
                            // Only select maintenance data service endpoint!!
                            continue;
                        }

                        var endpoint = new UriBuilder(metadataEndpoint.AbsoluteUri.Replace("/mex", ""));
                        mdiEndpoints.Add(endpoint.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                AddMessage($"ERROR: While getting registered MonitoringAdapters from ServiceLocator: \n{ex.Message}");
            }

            return mdiEndpoints;
        }

        private void AddMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddMessage), message);
            }
            else
            {
                _listViewMessages.Items.Add(new ListViewItem(new[] {DateTime.Now.ToShortTimeString(), message}));
                _listViewMessages.EnsureVisible(_listViewMessages.Items.Count - 1);
            }
        }

        #endregion

        #region DOC_MAINTENANCE_START_RECEIVER_AND_ADD_EVENT_HANDLER

        private void StartReceiver()
        {
            if (_mdiServiceHost != null)
            {
                AddMessage("The MDI callback service is already running.");
                return;
            }

            ServiceBinding binding;
            if (_checkBoxUseAppConfig.Checked)
            {
                // Passing in false here will force to use the same port again (important in case the 
                // client process (here: this app) has died and wants to reconnect to an existing subscription
                // Then the endpoint of the receiver must be exactly the same (including port number)
                binding = EndpointHelper.CreateBindingFromEndpointString(CallbackEndpoint, true,
                    EndpointHelper.TcpPortSharingEnabled);
            }
            else
            {
                // Create the TCP binding depending on port sharing enabled setting in GUI.
                // TCP port sharing only works in case the process is elevated or the current windows user is allowed to use port sharing (see SDK)
                binding =
                    EndpointHelper.CreateBindingFromEndpointString(CallbackEndpoint, true, _cbPortSharing.Checked);
            }

            _receiver = new MaintenanceDataEventingNotifyDuplex();
            _receiver.NewMaintenanceDataEventReceived += OnNewMaintenanceDataEventReceived;
            _mdiServiceHost = new ServiceHost(_receiver, new[] {new Uri(CallbackEndpoint)});
            _mdiServiceHost.AddServiceEndpoint(typeof(IMaintenanceDataNotifyDuplex), binding, CallbackEndpoint);
            var behaviour = _mdiServiceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behaviour.InstanceContextMode = InstanceContextMode.Single;
            _mdiServiceHost.Open();

            UserSettings.Default.LastCallbackUri = _labelCallbackUri.Text = CallbackEndpoint;
            UserSettings.Default.Save();

            _checkBoxUseAppConfig.Enabled = false;
        }

        private void OnNewMaintenanceDataEventReceived(object sender, MaintenanceDataEventArgs args)
        {
            AddMessage("************* MaintenanceDataEvent Received Begin ********************\n");
            HandleEquipmentConfiguration(
                new List<EquipmentConfigurationData> {args?.EquipmentConfigurationEventData?.EquipmentConfiguration},
                args?.EquipmentConfigurationEventData?.StationIdentification);
            HandleServiceAndMaintenanceData(
                new List<StationServiceAndMaintenanceData>
                    {args?.StationServiceAndMaintenanceEventData?.StationServiceAndMaintenanceData},
                args?.EquipmentConfigurationEventData?.StationIdentification);
            HandleSetupConfiguration(
                new List<SetupConfigurationData> {args?.SetupConfigurationEventData?.SetupConfiguration},
                args?.EquipmentConfigurationEventData?.StationIdentification);
            AddMessage("************* MaintenanceDataEvent Received End ********************\n");
        }

        #endregion

        #region DOC_MAINTENANCE_STOP_RECEIVER

        private void StopReceiver()
        {
            if (_mdiServiceHost != null && (_mdiServiceHost.State == CommunicationState.Opened ||
                                            _mdiServiceHost.State == CommunicationState.Opening))
            {
                _mdiServiceHost.Close();
                var retryCount = 3;
                //wait until the host is closed
                while (_mdiServiceHost.State != CommunicationState.Closed)
                {
                    System.Threading.Thread.Sleep(2000);
                    retryCount--;
                    if (retryCount == 0)
                        break;
                }

                if (_mdiServiceHost.State != CommunicationState.Closed)
                {
                    AddMessage(
                        $"Failed to stop the MDI callback service, the state is still '{_mdiServiceHost.State}'");
                }

                _mdiServiceHost.Abort();
                _mdiServiceHost = null;
                _receiver = null;

                AddMessage("The MDI Service was stopped.");
            }
            else
            {
                AddMessage("The MDI callback Service does not run, please start first the service.");
            }

            UserSettings.Default.LastCallbackUri = _labelCallbackUri.Text = string.Empty;
            UserSettings.Default.Save();
            CheckBoxUseAppConfigCheckedChanged(null, null);
        }

        #endregion

        #region DOC_MAINTENANCE_CREATE_SUBSCRIPTION

        private void CreateSubscription()
        {
            if (_currentSubscription != null) return;

            // We need to create a new subscription
            // Prepare the OIB Eventing subscription            
            var subscriptionData = new OIBSubscriptionData
            {
                CallbackUri = new Uri(UserSettings.Default.LastCallbackUri),
                Topic = DefaultTopic,
            };
            OIBSubscriptionHelper.UseClientAuthentication = _cbUseClientAuthentication.Checked;
            OIBSubscriptionHelper.SubscriptionManagerEndpoint = SubscriptionManagerEndpoint;
            _currentSubscription = OIBSubscriptionHelper.Subscribe(subscriptionData, _textBoxLineFullPath.Text);
            LastSubscriptionId = _currentSubscription.Manager.Identifier.Value;
        }

        #endregion

        #region DOC_MAINTENANCE_RENEW_DELETE_SUBSCRIPTION

        private void RenewSubscription()
        {
            if (_currentSubscription != null)
            {
                // We should renew the subscription (make it expire 1 day up to now)
                OIBSubscriptionHelper.UseClientAuthentication = _cbUseClientAuthentication.Checked;
                OIBSubscriptionHelper.SubscriptionManagerEndpoint = SubscriptionManagerEndpoint;
                LastSubscriptionId = OIBSubscriptionHelper.RenewSubscription(_currentSubscription, NewExpiryDate);
            }
        }

        private void DeleteSubscription()
        {
            if (_currentSubscription == null) return;
            try
            {
                OIBSubscriptionHelper.UseClientAuthentication = _cbUseClientAuthentication.Checked;
                OIBSubscriptionHelper.SubscriptionManagerEndpoint = SubscriptionManagerEndpoint;
                OIBSubscriptionHelper.Unsubscribe(_currentSubscription);
            }
            catch (Exception ex)
            {
                // Maybe the subscription was deleted?
                AddMessage("Unsubscribe got exception: " + ex);
            }

            _currentSubscription = null;
            LastSubscriptionId = string.Empty;
        }

        #endregion

        #region DOC_HANDLE_MAINTENANCE_DATA

        private void HandleEquipmentConfiguration(List<EquipmentConfigurationData> configEquipmentList,
            StationIdentification stationIdentification )
        {
            if (configEquipmentList == null) return;
            AddMessage($"MachineId: {stationIdentification?.MachineId}\n");
            AddMessage($"LineFullPath: {stationIdentification?.LineFullPath}\n");
            AddMessage($"StationFullPath: {stationIdentification?.StationFullPath}\n");
            AddMessage($"StationFullPath: {stationIdentification?.TypeId}\n");
            AddMessage("*************************************************\n");
            AddMessage("Equipment Configuration Begin:\n");
            foreach (var value in configEquipmentList)
            {
                if (value == null) continue;

                AddMessage("Equipment Configuration:\n");
                AddMessage($"SW-Version:   {value.StationSoftwareVersion}\n");
                AddMessage($"Station Type: {value.StationTypeName}\n");
                AddMessage("--> Placement Machine Details:\n");
                AddMessage($"    UniqueId: {value.PlacementMachineDetails.UniqueId}\n");
                AddMessage("--> Cameras:\n");
                foreach (var details in value.PlacementMachineDetails.Cameras)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name:     {details.GeneralData.Name}\n");
                    AddMessage($"    Type:     {details.GeneralData.Type}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                }

                AddMessage("--> Conveyors:\n");
                foreach (var details in value.PlacementMachineDetails.Conveyors)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name:     {details.GeneralData.Name}\n");
                    AddMessage($"    Type:     {details.GeneralData.Type}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                }

                AddMessage("--> Gantries:\n");
                foreach (var details in value.PlacementMachineDetails.Gantries)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name:     {details.GeneralData.Name}\n");
                    AddMessage($"    Type:     {details.GeneralData.Type}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                }

                AddMessage("--> Nozzle Changer Carrier:\n");
                foreach (var details in value.PlacementMachineDetails.NozzleChangerCarrier)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name:     {details.GeneralData.Name}\n");
                    AddMessage($"    Type:     {details.GeneralData.Type}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                }

                AddMessage("--> Place Heads:\n");
                foreach (var details in value.PlacementMachineDetails.PlaceHeads)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name:     {details.GeneralData.Name}\n");
                    AddMessage($"    Type:     {details.GeneralData.Type}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                }

                AddMessage("--> Tape Cutters:\n");
                foreach (var details in value.PlacementMachineDetails.TapeCutters)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name:     {details.GeneralData.Name}\n");
                    AddMessage($"    Type:     {details.GeneralData.Type}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                }

                AddMessage("--> Vacuum Pumps:\n");
                foreach (var details in value.PlacementMachineDetails.VacuumPumps)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name:     {details.GeneralData.Name}\n");
                    AddMessage($"    Type:     {details.GeneralData.Type}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                }
            }

            AddMessage("Equipment Configuration End\n");
        }

        private void HandleServiceAndMaintenanceData(List<StationServiceAndMaintenanceData> serviceAndMaintenanceList,
            StationIdentification stationIdentification)
        {
            if (serviceAndMaintenanceList == null) return;
            AddMessage($"MachineId: {stationIdentification?.MachineId}\n");
            AddMessage($"LineFullPath: {stationIdentification?.LineFullPath}\n");
            AddMessage($"StationFullPath: {stationIdentification?.StationFullPath}\n");
            AddMessage("*************************************************\n");
            AddMessage("Service And Maintenance Begin:\n");
            foreach (var value in serviceAndMaintenanceList)
            {
                if (value == null) continue;

                AddMessage("--> Equipment Service And Maintenance Data:\n");
                foreach (var data in value.EquipmentServiceAndMaintenanceData)
                {
                    AddMessage($"  **UniqueId: {data.UniqueId}\n");
                    AddMessage("--> Calibration Details:\n");
                    foreach (var details in data.CalibrationDetails)
                    {
                        AddMessage($"   *Name: {details.Name}\n");
                        AddMessage($"    Type: {details.Type}\n");
                        AddMessage($"    LastRunTime: {details.LastRunTime}\n");
                        AddMessage($"    Status: {details.Status}\n");
                    }

                    AddMessage("--> Error Details:\n");
                    foreach (var details in data.ErrorDetails)
                    {
                        AddMessage($"   *Name: {details.Name}\n");
                        AddMessage($"    OccurenceTime: {details.OccurenceTime}\n");
                    }

                    AddMessage("--> Maintenance Details:\n");
                    foreach (var details in data.MaintenanceDetails)
                    {
                        AddMessage($"   *Name: {details.Name}\n");
                        AddMessage($"    CounterType: {details.CounterType}\n");
                        AddMessage($"    CurrentCounterValue: {details.CurrentCounterValue}\n");
                        AddMessage($"    CurrentRatio: {details.CurrentRatio}\n");
                        AddMessage($"    CurrentTimeStamp: {details.CurrentTimeStamp}\n");
                        AddMessage($"    LastMaintenanceValid: {details.LastMaintenanceValid}\n");
                        AddMessage($"    LastMaintenanceCounterValue: {details.LastMaintenanceCounterValue}\n");
                        AddMessage($"    LastMaintenanceCounterValue: {details.LastMaintenanceCounterValue}\n");
                    }

                    AddMessage("--> Sensor Details:\n");
                    foreach (var details in data.SensorDetails)
                    {
                        AddMessage($"   *PhysicalUnitType: {details.PhysicalUnitType}\n");
                        AddMessage($"    SampleValue: {details.SampleValue}\n");
                        AddMessage($"    SampleTimeStamp: {details.SampleTimeStamp}\n");
                    }

                    AddMessage("--> Verification Details:\n");
                    foreach (var details in data.VerificationDetails)
                    {
                        AddMessage($"   *Name: {details.Name}\n");
                        AddMessage($"    Value: {details.Value}\n");
                        AddMessage($"    Type: {details.Type}\n");
                        AddMessage($"    Status: {details.Status}\n");
                        AddMessage($"    LastRunTime: {details.LastRunTime}\n");
                    }
                }
            }

            AddMessage("Service And Maintenance End\n");
        }

        private void HandleSetupConfiguration(List<SetupConfigurationData> configSetupList,
            StationIdentification stationIdentification)
        {
            if (configSetupList == null) return;
            AddMessage($"MachineId: {stationIdentification?.MachineId}\n");
            AddMessage($"LineFullPath: {stationIdentification?.LineFullPath}\n");
            AddMessage($"StationFullPath: {stationIdentification?.StationFullPath}\n");
            AddMessage("*************************************************\n");
            AddMessage("Setup Configuration Begin:\n");
            foreach (var value in configSetupList)
            {
                if (value == null) continue;

                AddMessage("--> Placement Machine Details:\n");
                AddMessage("--> Garages:\n");
                foreach (var details in value.PlacementMachineDetails.Garages)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name: {details.GeneralData.Name}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                    AddMessage($"    Type: {details.GeneralData.Type}\n");
                }

                AddMessage("--> Table Devices:\n");
                foreach (var details in value.PlacementMachineDetails.TableDevices)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name: {details.GeneralData.Name}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                    AddMessage($"    Type: {details.GeneralData.Type}\n");
                }

                AddMessage("--> Tape Feeders:\n");
                foreach (var details in value.PlacementMachineDetails.TapeFeeders)
                {
                    AddMessage($"   *UniqueId: {details.GeneralData.UniqueId}\n");
                    AddMessage($"    Name: {details.GeneralData.Name}\n");
                    AddMessage($"    Location: {details.GeneralData.Location}\n");
                    AddMessage($"    Type: {details.GeneralData.Type}\n");
                }
            }

            AddMessage("Setup Configuration End\n");
        }

        #endregion

        #region Event Handler

        private void _buttonClearMessages_Click(object sender, EventArgs e)
        {
            _listViewMessages.Items.Clear();
        }

        private void _buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            StringBuilder buffer = new StringBuilder();
            for (int i = 0; i < _listViewMessages.Columns.Count; i++)
            {
                buffer.Append(_listViewMessages.Columns[i].Text);
                buffer.Append("\t");
            }

            buffer.Append(Environment.NewLine);

            for (int i = 0; i < _listViewMessages.Items.Count; i++)
            {
                for (int j = 0; j < _listViewMessages.Columns.Count; j++)
                {
                    buffer.Append(_listViewMessages.Items[i].SubItems[j].Text);
                    buffer.Append("\t");
                }

                buffer.Append(Environment.NewLine);
            }

            Clipboard.SetText(buffer.ToString());
        }

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
                AddMessage($"Got exception when renewing the subscription: {ex}");
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

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
                _buttonSubscribe.Enabled = true;
                _buttonUnsubscribe.Enabled = true;

                #region DOC_MAINTENANCE_LAST_SUBSCRIPTION_ID

                OIBSubscriptionHelper.UseClientAuthentication = _cbUseClientAuthentication.Checked;
                OIBSubscriptionHelper.Init(SubscriptionManagerEndpoint);

                var lastSubscriptionId = LastSubscriptionId;
                if (!string.IsNullOrEmpty(lastSubscriptionId))
                {
                    // See if there is still a subscription with this ID.
                    // If yes, then renew it...
                    var search = new SubscriptionDescriptor {Id = lastSubscriptionId};
                    var foundSubscriptions = OIBSubscriptionHelper.GetSubscriptions(search);
                    if (foundSubscriptions != null && foundSubscriptions.GetLength(0) > 0)
                    {
                        // Check that the subscription that we found is using the same callback
                        var foundCallbackUri = foundSubscriptions[0].Delivery.NotifyTo.ToEndpointAddress().Uri;
                        if (foundCallbackUri.ToString() != CallbackEndpoint)
                        {
                            MessageBox.Show(
                                $@"A subscription with Identifier:{lastSubscriptionId} was found in Eventing which uses a different callback than specified! Please manually delete the old subscription!.");
                        }
                        else
                        {
                            _currentSubscription = foundSubscriptions[0];
                        }
                    }
                }

                #endregion

                #region DOC_MAINTENANCE_LAST_SUBSCRIPTION_CALLBACK

                else
                {
                    // Alternative 2, use the callback URI and the topic to get the existing subscription
                    var search = new SubscriptionDescriptor
                    {
                        Topic = DefaultTopic,
                        Endpoint = EndpointAddressAugust2004.FromEndpointAddress(new EndpointAddress(CallbackEndpoint))
                    };
                    Subscription[] foundSubscriptions = OIBSubscriptionHelper.GetSubscriptions(search);
                    if (foundSubscriptions != null && foundSubscriptions.GetLength(0) > 0)
                    {
                        _currentSubscription = foundSubscriptions[0];
                    }
                }

                #endregion

                var lastPortNumber = LastPortNumber;
                if (lastPortNumber != -1)
                {
                    _numericUpDownPort.Value = lastPortNumber;
                }

                #region DOC_MAINTENANCE_SUBSCRIPTION_FOUND

                if (_currentSubscription != null)
                {
                    StartReceiver();
                    RenewSubscription();
                    _labelCurrentSubscriptionId.Text = _currentSubscription.Manager.Identifier.Value;
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

                _listBoxEndpoints.Items.Clear();
                foreach (var epMdi in GetMaintenanceDataServiceEndpoints())
                {
                    _listBoxEndpoints.Items.Add(epMdi);
                }

                if (_listBoxEndpoints.Items.Count != 0) _buttonGetConfiguration.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error happened when connecting to core: {ex}");
            }
        }

        private void Form1Closing(object sender, FormClosingEventArgs e)
        {
            if (_checkBoxUnsubscribe.Checked)
            {
                DeleteSubscription();
            }

            if (_receiver != null) _receiver.NewMaintenanceDataEventReceived -= OnNewMaintenanceDataEventReceived;

            _timer.Enabled = false;
            StopReceiver();
        }

        private void CheckBoxUseAppConfigCheckedChanged(object sender, EventArgs e)
        {
            _numericUpDownPort.Enabled = !_checkBoxUseAppConfig.Checked;
        }

        private void ListBoxAdaptersSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void _checkBoxFilterLine_CheckedChanged(object sender, EventArgs e)
        {
            _textBoxLineFullPath.Enabled = _checkBoxFilterLine.Checked;
        }

        private void ButtonGetAllMtcDataClick(object sender, EventArgs e)
        {
            #region  DOC_MAINTENANCE_SYNCHRONOUS_ACCESS

            AddMessage("************* Get All Maintenance Data Begin ********************\n");

            ChannelFactory<IMaintenanceDataSource> factoryMaintenanceData = null;
            try
            {
                if (_listBoxEndpoints.SelectedItem == null) return;

                var mdiEndpoint = _listBoxEndpoints.SelectedItem.ToString();
                var epAddress = new EndpointAddress(mdiEndpoint);
                var binding = EndpointHelper.CreateBindingFromEndpointString(epAddress.ToString(), false, false);
                factoryMaintenanceData = new ChannelFactory<IMaintenanceDataSource>(binding, epAddress);
                var maintenanceData = factoryMaintenanceData.CreateChannel();

                var configEquipment =
                    maintenanceData.GetEquipmentConfiguration(new GetEquipmentConfigurationRequest
                        {MachineIds = new string[] { }});
                foreach (var response in configEquipment?.EquipmentConfigurations?.Values?.ToList())
                {
                    HandleEquipmentConfiguration(new List<EquipmentConfigurationData> {response.EquipmentConfiguration},
                        response.StationIdentification);
                }

                var serviceAndMaintenance =
                    maintenanceData.GetServiceAndMaintenanceData(new GetServiceAndMaintenanceDataRequest
                        {MachineIds = new string[] { }});
                foreach (var response in serviceAndMaintenance?.StationServiceAndMaintenanceData?.Values?.ToList())
                {
                    HandleServiceAndMaintenanceData(
                        new List<StationServiceAndMaintenanceData> {response.StationServiceAndMaintenanceData},
                        response.StationIdentification);
                }

                var configSetup = maintenanceData.GetSetupConfiguration(new GetSetupConfigurationRequest
                    {MachineIds = new string[] { }});
                foreach (var response in configSetup?.SetupConfigurations?.Values?.ToList())
                {
                    HandleSetupConfiguration(new List<SetupConfigurationData> {response.SetupConfiguration},
                        response.StationIdentification);
                }
            }
            catch (FaultException faultEx)
            {
                AddMessage($"ERROR: Got fault exception while calling GetConfiguration(): {faultEx.Message}");
            }
            catch (Exception ex)
            {
                AddMessage($"ERROR: Got exception while calling GetConfiguration(): {ex.Message}");
            }
            finally
            {
                EndpointHelper.CloseCommunicationObject(factoryMaintenanceData);
            }

            AddMessage("************* Get All Maintenance Data End ********************\n");

            #endregion
        }

        #endregion
    }
}