#region Copyright

// ASM Device Manager - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

#region using

using Microsoft.Win32;
using MaterialManagerEventsTutorial.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

#endregion using

namespace MaterialManagerEventsTutorial.ViewModels
{
    /// <summary>
    /// ViewModel of M-V-VM pattern
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MainPanelViewModel : INotifyPropertyChanged
    {
        #region fields

        private readonly object _eventDataLock = new object();
        private readonly ObservableCollection<EventData> _eventDataCollection = new ObservableCollection<EventData>();
        private bool _connectButtonEnabled;
        private bool _disconnectButtonEnabled;
        private ICommand _connectCommand;
        private ICommand _disconnectCommand;
        private ICommand _saveMessagesCommand;
        private string _filter;
        private ICommand _filterMessagesCommand;
        private string _oibHost;
        private int _oibPort;
        private ICommand _clearMessagesCommand;
        private bool _useClientAuthentication;

        #endregion fields

        #region DOC_SiMM_MAINPANELVIEWMODEL_CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPanelViewModel"/> class.
        /// </summary>
        public MainPanelViewModel()
        {
            // get connection info from userSettings of app.config
            var settings = ((System.Configuration.ClientSettingsSection) ConfigurationManager.GetSection(
                "userSettings/MaterialManagerEventsTutorial.Properties.Settings")).Settings;
            int.TryParse(settings.Get("OibCorePort").Value.ValueXml.InnerText, out int port);
            OibPort = port;
            OibHost = settings.Get("OibCoreHost").Value.ValueXml.InnerText;

            // Material Manager event handler registration
            EventsReceiver.Instance.EventReceived += EventsReceiver_EventReceived;

            Filter = string.Empty;
            ConnectButtonEnabled = true;

            // Material Manager events collection view
            EventDataCollectionView = CollectionViewSource.GetDefaultView(_eventDataCollection);
            EventDataCollectionView.SortDescriptions.Add(new SortDescription(nameof(EventData.ReceiveDateTime), ListSortDirection.Descending));
        }

        #endregion DOC_SiMM_MAINPANELVIEWMODEL_CONSTRUCTOR

        #region commands

        /// <summary>
        /// Gets the save messages command.
        /// </summary>
        /// <value>
        /// The save messages command.
        /// </value>
        public ICommand SaveMessagesCommand => _saveMessagesCommand ?? (_saveMessagesCommand = new TransmitCommand(SaveMessages));

        /// <summary>
        /// Gets the connect command.
        /// </summary>
        /// <value>
        /// The connect command.
        /// </value>
        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new TransmitCommand(Connect));

        /// <summary>
        /// Gets the disconnect command.
        /// </summary>
        /// <value>
        /// The disconnect command.
        /// </value>
        public ICommand DisconnectCommand => _disconnectCommand ?? (_disconnectCommand = new TransmitCommand(Disconnect));

        /// <summary>
        /// Gets the clear messages command.
        /// </summary>
        /// <value>
        /// The clear messages command.
        /// </value>
        public ICommand ClearMessagesCommand => _clearMessagesCommand ?? (_clearMessagesCommand = new TransmitCommand(ClearMessages));

        /// <summary>
        /// Gets the filter messages command.
        /// </summary>
        /// <value>
        /// The filter messages command.
        /// </value>
        public ICommand FilterMessagesCommand => _filterMessagesCommand ?? (_filterMessagesCommand = new TransmitCommand(FilterMessages));

        #endregion commands

        /// <summary>
        /// Gets the event data collection view.
        /// </summary>
        /// <value>
        /// The event data collection view.
        /// </value>
        public ICollectionView EventDataCollectionView { get; }

        /// <summary>
        /// Gets or sets the OIB host.
        /// </summary>
        /// <value>
        /// The OIB host.
        /// </value>
        public string OibHost
        {
            get { return _oibHost; }
            set
            {
                if(_oibHost == value)
                    return;

                _oibHost = value;
            }
        }

        /// <summary>
        /// Gets or sets the oib port.
        /// </summary>
        /// <value>
        /// The oib port.
        /// </value>
        public int OibPort
        {
            get { return _oibPort; }
            set
            {
                if(_oibPort == value)
                    return;

                _oibPort = value;
            }
        }

        /// <summary>
        /// Gets or sets if client authentication should be used
        /// </summary>
        public bool UseClientAuthentication
        {
            get => _useClientAuthentication;
            set
            {
                _useClientAuthentication = value;
                EventsController.UseClientAuthentication = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [connect button enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [connect button enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool ConnectButtonEnabled
        {
            get { return _connectButtonEnabled; }
            set
            {
                _connectButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disconnect button enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disconnect button enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool DisconnectButtonEnabled
        {
            get { return _disconnectButtonEnabled; }
            set
            {
                _disconnectButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the name of the get application.
        /// </summary>
        /// <value>
        /// The name of the get application.
        /// </value>
        public static string GetAppName => "MaterialManagerEventsTutorial";

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void ClearMessages(object obj)
        {
            lock (_eventDataLock)
            {
                _eventDataCollection.Clear();
            }
        }

        private static bool CheckKeywords(string[] keywords, EventData toCheck)
        {
            var oneOf = new List<string>();

            foreach (var keyword in keywords)
            {
                // Check if it is a must have keyword
                if (keyword.StartsWith("+"))
                {
                    // At least one has to have it
                    string search = keyword.Substring(1);
                    if (!(toCheck.Key.ToLowerInvariant().Contains(search) ||
                          toCheck.Value.ToLowerInvariant().Contains(search) ||
                          toCheck.MessageId.ToLowerInvariant().Contains(search)))
                    {
                        return false;
                    }
                }
                else if (keyword.StartsWith("-"))
                {
                    // No one has to have it
                    string search = keyword.Substring(1);
                    if (toCheck.Key.ToLowerInvariant().Contains(search) ||
                        toCheck.Value.ToLowerInvariant().Contains(search) ||
                        toCheck.MessageId.ToLowerInvariant().Contains(search))
                    {
                        return false;
                    }
                }
                else
                {
                    oneOf.Add(keyword);
                }
            }

            foreach (var search in oneOf)
            {
                if (toCheck.Key.ToLowerInvariant().Contains(search) ||
                    toCheck.Value.ToLowerInvariant().Contains(search) ||
                    toCheck.MessageId.ToLowerInvariant().Contains(search))
                {
                    return true;
                }
            }

            // One-of-selection not found
            return oneOf.Count < 1;
        }

        private void FilterMessages(object obj)
        {
            string[] keywords = Filter.Trim().ToLowerInvariant().Split(' ');
            EventDataCollectionView.Filter = item => CheckKeywords(keywords, (EventData) item);
            EventDataCollectionView.Refresh();
        }

        #region DOC_SiMM_MAINPANELVIEWMODEL_EVENTRECEIVED

        /// <summary>
        /// Handles the EventReceived event of the EventsReceiver control.
        /// When a new event is here its content goes append into events collection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MaterialManagerEventReceivedEventArgs"/> instance containing the event data.</param>
        private void EventsReceiver_EventReceived(object sender, MaterialManagerEventReceivedEventArgs e)
        {
            try
            {
                var eventDataCollection = EventData.FromEvent(e);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    lock (_eventDataLock)
                    {
                        foreach (var eventData in eventDataCollection)
                        {
                            _eventDataCollection.Add(eventData);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MainPanelViewModel.GetAppName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion DOC_SiMM_MAINPANELVIEWMODEL_EVENTRECEIVED

        private void Connect(object obj)
        {
            try
            {
                EventsController.CoreHostName = OibHost;
                EventsController.CoreHttpPort = OibPort;
                EventsController.CreateServiceEndpointForEventing();
                EventsController.RenewOrCreateSubscription();

                ConnectButtonEnabled = false;
                DisconnectButtonEnabled = true;
            }
            catch (Exception ex)
            {
                EventsController.ResetSubscription();

                MessageBox.Show(
                    ex.Message,
                    MainPanelViewModel.GetAppName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Disconnect(object obj)
        {
            try
            {
                EventsController.Unsubscribe();

                ConnectButtonEnabled = true;
                DisconnectButtonEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MainPanelViewModel.GetAppName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void SaveMessages(object obj)
        {
            try
            {
                string text;
                lock (_eventDataLock)
                {
                    var ids = new List<string>();
                    var eds = new List<EventData>();

                    foreach (var data in _eventDataCollection)
                    {
                        if (!ids.Contains(data.MessageId))
                        {
                            eds.Add(data);
                        }
                    }
                    text = string.Join(Environment.NewLine, eds);
                }

                var dialog = new SaveFileDialog {Filter = "*.txt|*.txt", DefaultExt = "*.txt"};
                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllText(dialog.FileName, text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MainPanelViewModel.GetAppName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}