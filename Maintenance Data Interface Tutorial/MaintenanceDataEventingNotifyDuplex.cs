#region Copyright

// ASM MaintenanceData OIB Tutorial - Copyright (C) ASM Assembly Systems 2017
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.

#endregion


using System.ServiceModel;
using System.Threading;
using www.siplace.com.OIB._2019._11.MaintenanceData.Contracts.Service;

#region DOC_MaintenanceDataEventingWebService

namespace MaintenanceDataTutorial
{

    /// <summary>
    /// Occurs when a board is completed within a station.
    /// (when the board is leaving the station).
    /// Comprises all events, state transitions, pickup errors and consumption data for a board in a given station
    /// </summary>
    /// <param name="sender">The sender of the event (this object)</param>
    /// <param name="args">The Event args describing the contents.</param>
    internal delegate void NewMaintenanceDataEvent(object sender, MaintenanceDataEventArgs args);

    /// <summary>
    /// The implementation if the IMaintenanceDataNotifyDuplex service callback interface.
    /// Since the InstanceContextMode is single and the calls are all synchronized by default
    /// (ConcurrencyMode.Single), the NewProcessData() method is always run by one thread at a time.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class MaintenanceDataEventingNotifyDuplex : IMaintenanceDataNotifyDuplex
    {
        #region fields
        private readonly SynchronizationContext _sync = SynchronizationContext.Current;
        #endregion

        #region Events

        /// <summary>
        /// Fired whenever a board was processed by one station (when the board is leaving the station).
        /// Comprises all events, state transitions, pickup errors and consumption data for a board in a given station
        /// </summary>
        public event NewMaintenanceDataEvent NewMaintenanceDataEventReceived;

        #endregion

        #region Constructors

        public MaintenanceDataEventingNotifyDuplex()
        {
        }

        #endregion

        #region DOC_NewProcessData - implement IMaintenanceDataNotifyDuplex

        public NewMaintenanceDataResponse NewMaintenanceData(MaintenanceDataRequest request)
        {
            if (NewMaintenanceDataEventReceived != null)
            {
                var args = new MaintenanceDataEventArgs(request);
                if (_sync != null)
                {
                    void SendOrPostCallback(object state)
                    {
                        NewMaintenanceDataEventReceived(this, args);
                    }

                    _sync.Send(SendOrPostCallback, null);
                }
                else
                {
                    NewMaintenanceDataEventReceived(this, args);
                }
            }

            return new NewMaintenanceDataResponse();
        }

        #endregion
    }
}

#endregion
