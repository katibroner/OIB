//-----------------------------------------------------------------------
// <copyright file="SetupCenterNotifyReceiver.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System;
using System.ServiceModel;

using schemas.xmlsoap.org.ws._2004._08.eventing;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Data;
using www.siplace.com.OIB._2008._05.SetupCenter.Contracts.Service;

#endregion

namespace OIB.Tutorial
{

    #region DOC_SetupCenterNotifyReceiver_CLASS

    /// <summary>
    /// This class receives Setup Center events. For this purpose it acts as a Web service
    /// by publishing itself as a Web service based on the .exe.config settings.
    /// If the events are to be used by forms, this class needs to be instantiated
    /// in the form's thread (otherwise the calls need to be synchronized using 
    /// InvokeRequired/Invoke).
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SetupCenterNotifyReceiver : ISiplaceSetupCenterNotifyNewDuplex, IDisposable
    {
        #region Fields

        private readonly Uri _SubscriptionManagerMexUri;
        private readonly ILog _Log;
        private ServiceHost _ServiceHost;
        private readonly Identifier _SubscriptionId;
        private bool _useClientAuthentication;

        #endregion

        #region Constructor

        #region DOC_SetupCenterNotifyReceiver_CONSTRUCTOR

        /// <summary>
        /// Class to subscribe and receive Setup Center events.
        /// </summary>
        /// <param name="log">Log messages consumer</param>
        /// <param name="subscriptionManagerMexUri">MEX uri of the subscription manager.</param>
        /// <param name="useClientAuthentication">If true use client authentication</param>
        public SetupCenterNotifyReceiver(ILog log, Uri subscriptionManagerMexUri, bool useClientAuthentication)
        {
            _Log = log;
            _SubscriptionManagerMexUri = subscriptionManagerMexUri;
            _useClientAuthentication = useClientAuthentication;

            // Publish my own object instance as a Web service 
            // to receive Setup Center events using
            // the settings from the app.config file
            _ServiceHost = new ServiceHost(this);
            _ServiceHost.Open();

            // Subscribe for Setup Center events at OIB subscription manager:
            using (ServiceHelper<ISubscriptionManager> subscriptionManagerHelper =
                new ServiceHelper<ISubscriptionManager>(_SubscriptionManagerMexUri, useClientAuthentication))
            {
                Subscribe subscribe = new Subscribe();
                subscribe.Delivery = new Delivery();

                // Setting the delivery mode to: Push with Acknowledge
                subscribe.Delivery.DeliveryMode =
                    "http://schemas.xmlsoap.org/ws/2004/08/eventing/DeliveryModes/PushWithAck";
                
                // Set the receiver of the Setup Center events: my own Web service
                EndpointAddress notifyTo = new EndpointAddress(_ServiceHost.ChannelDispatchers[0].Listener.Uri);
                subscribe.Delivery.NotifyTo = EndpointAddressAugust2004.FromEndpointAddress(notifyTo);

                // Set life time of the subscription: Forever
                Expires exp = new Expires();
                exp.Value = DateTime.MaxValue.ToUniversalTime();
                subscribe.Expires = exp;

                // Set subscription topic: the Setup Center events.
                SubscribeRequest subscribeRequest = new SubscribeRequest{SubscriptionTopic = "SIPLACE:OIB:SetupCenter:NotifyNew", Subscribe = subscribe};

                // !Do the subscription!
                SubscribeResponse subscribeResult = 
                    subscriptionManagerHelper.Proxy.Subscribe(subscribeRequest);

                // Remember the subscription identifier for unsubscribe (see Dispose()).
                _SubscriptionId = subscribeResult.SubscribeResponse1.SubscriptionManager.Identifier;
            }
        }

        #endregion

        #endregion

        #region IDisposable Members

        #region DOC_SetupCenterNotifyReceiver_DISPOSE
        public void Dispose()
        {
            if (_ServiceHost != null)
            {
                // Unsubscribe for setup center events
                if (_SubscriptionId != null)
                {
                    using (ServiceHelper<ISubscriptionManager> subscriptionManagerHelper =
                        new ServiceHelper<ISubscriptionManager>(_SubscriptionManagerMexUri, _useClientAuthentication))
                    {
                        EndpointAddress notifyTo = new EndpointAddress(
                            _ServiceHost.ChannelDispatchers[0].Listener.Uri);
                        
                        UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest(
                            EndpointAddressAugust2004.FromEndpointAddress(notifyTo),
                            _SubscriptionId,
                            "SIPLACE:OIB:SetupCenter:NotifyNew",
                            new Unsubscribe());

                        subscriptionManagerHelper.Proxy.Unsubscribe(unsubscribeRequest);
                    }
                }

                // Close own Web service to stop receiving events
                _ServiceHost.Close();
                _ServiceHost = null;
            }
        }
        #endregion

        #endregion

        #region ISiplaceSetupCenterNotifyNewDuplex Helper Methods

        #region DOC_PACKAGING_UNIT_CREATED

        private void PackagingUnitCreated(PackagingUnitCreatedReport report)
        {
            try
            {
                foreach (var data in report.PackagingUnitManagementDataList)
                {
                    _Log.InfoMessage(string.Format("EVENT: Packaging unit created (id: '{0}' type: '{1}').", 
                        data.PackagingUnit.UID,
                        data.PackagingUnit.ComponentName));
                }
            }
            catch (Exception ex)
            {
                _Log.ErrorMessage("Processing of event PackagingUnitCreated failed.", ex);
            }
        }

        #endregion

        private void PackagingUnitUpdated(PackagingUnitUpdatedReport report)
        {
            try
            {
                foreach (var data in report.PackagingUnitManagementDataList)
                {
                    _Log.InfoMessage(string.Format("EVENT: Packaging unit updated (id: '{0}', type: '{1}')", 
                        data.PackagingUnit.UID, 
                        data.PackagingUnit.ComponentName));
                }
            }
            catch (Exception ex)
            {
                _Log.ErrorMessage("Processing of event PackagingUnitUpdated failed.", ex);
            }
        }

        private void PackagingUnitDeleted(PackagingUnitDeletedReport report)
        {
            try
            {
                foreach (var data in report.PackagingUnitManagementDataList)
                {
                    _Log.InfoMessage(string.Format("EVENT: Packaging unit deleted (id: '{0}', type: '{1}')",
                        data.PackagingUnit.UID,
                        data.PackagingUnit.ComponentName));
                }
            }
            catch (Exception ex)
            {
                _Log.ErrorMessage("Processing of event PackagingUnitDeleted failed.", ex);
            }
        }

        private void PackagingQuantityChanged(PackagingQuantityChangedReport report)
        {
            try
            {
                foreach (var data in report.PackagingUnitQuantities)
                {
                    _Log.InfoMessage(string.Format("EVENT: Packaging unit quantity changed (id: '{0}', quantity: '{1}', change: '{2}')",
                        data.PackagingUnit.UID,
                        data.PackagingUnit.Quantity,
                        data.QuanityCorrection ));
                }
            }
            catch (Exception ex)
            {
                _Log.ErrorMessage("Processing of event PackagingQuantityChanged failed.", ex);
            }
        }

        private void LockStateChanged(LockStateChangedReport report)
        {
            try
            {
                foreach (var packagingUnit in report.PackagingUnits)
                {
                    var locked = packagingUnit.LockInfos.Length > 0;
                    _Log.InfoMessage(string.Format("EVENT: Lock state changed (id: '{0}', locked: '{1}')",
                        packagingUnit.UID, locked));
                }
            }
            catch (Exception ex)
            {
                _Log.ErrorMessage("Processing of event LockStateChanged failed.", ex);
            }
        }

        #region Messages to ignore

        private void MessagesReceived(MessagesReceivedReport report)
        {
            // Ignore notification
        }

        private void SetupChanged(SetupChangedReport report)
        {
            // Ignore notification
        }

        private void NewSetupActive(NewSetupActiveReport report)
        {
            // Ignore notification
        }

        private void TablePlaced(TablePlacedReport report)
        {
            // Ignore notification
        }

        private void TableRemoved(TableRemovedReport report)
        {
            // Ignore notification
        }

        private void FeederPlaced(FeederPlacedReport report)
        {
            // Ignore notification
        }

        private void FeederRemoved(FeederRemovedReport report)
        {
            // Ignore notification
        }

        private void MaterialMoved(MaterialMovedReport report)
        {
            // Ignore notification
        }

        private void MaterialReordered(MaterialReorderReport report)
        {
            // Ignore notification
        }

        private void PackagingUnitConsumed(PackagingUnitConsumedReport report)
        {
            // Ignore notification
        }

        private void SpliceChainSplit(SpliceChainSplitReport report)
        {
            // Ignore notification
        }

        private void PrinterToolVerified(PrinterToolVerifiedReport report)
        {
            // Ignore notification
        }

        private void PrinterToolUnverified(PrinterToolUnverifiedReport report)
        {
            // Ignore notification
        }

        private void PrinterRecipeVerified(PrinterRecipeVerifiedReport report)
        {
            // Ignore notification
        }

        private void PrinterMaterialVerified(PrinterMaterialVerifiedReport report)
        {
            // Ignore notification
        }

        private void PrinterMaterialUnverified(PrinterMaterialUnverifiedReport report)
        {
            // Ignore notification
        }

        private void PrinterDeviceStateChanged(PrinterDeviceStateChangedReport report)
        {
            // Ignore notification
        }

        private void PrinterCoverOpened(PrinterCoverOpenedReport report)
        {
            // Ignore notification
        }

        private void PrinterCoverClosed(PrinterCoverClosedReport report)
        {
            // Ignore notification
        }

        private void PrinterConsumablesUpdated(PrinterConsumablesUpdatedReport report)
        {
            // Ignore notification
        }

        private void PrinterCapabilitiesChanged(PrinterCapabilitiesChangedReport report)
        {
            // Ignore notification
        }

        private void NewSetupControlDataRequested(NewSetupControlDataRequestedReport report)
        {
            // Ignore notification
        }

        private void ConfigurationChanged(ConfigurationChangedReport report)
        {
            // Ignore notification
        }

        private void ComponentLevelIndicatorThresholdChanged(ComponentLevelIndicatorThresholdChangedReport report)
        {
            // Ignore notification
        }


        #endregion

        #endregion

        #region ISiplaceSetupCenterNotifyNewDuplex Members

        public SetupCenterEventResponse SetupCenterEvent(SetupCenterEventDataRequest request)
        {
            if (request != null)
            {
                if (request.FeederPlacedReport != null)
                {
                    FeederPlaced(request.FeederPlacedReport);
                }
                else if (request.InstantFeederPlacedReport != null)
                {
                    FeederPlaced(request.InstantFeederPlacedReport);
                }
                else if (request.FeederRemovedReport != null)
                {
                    FeederRemoved(request.FeederRemovedReport);
                }
                else if (request.InstantFeederRemovedReport != null)
                {
                    FeederRemoved(request.InstantFeederRemovedReport);
                }
                else if (request.MessagesReceivedReport != null)
                {
                    MessagesReceived(request.MessagesReceivedReport);
                }
                else if (request.NewSetupActiveReport != null)
                {
                    NewSetupActive(request.NewSetupActiveReport);
                }
                else if (request.PackagingQuantityChangedReport != null)
                {
                    PackagingQuantityChanged(request.PackagingQuantityChangedReport);
                }
                else if (request.SetupChangedReport != null)
                {
                    SetupChanged(request.SetupChangedReport);
                }
                else if (request.MaterialReorderReport != null)
                {
                    MaterialReordered(request.MaterialReorderReport);
                }
                else if (request.PackagingUnitConsumedReport != null)
                {
                    PackagingUnitConsumed(request.PackagingUnitConsumedReport);
                }
                else if (request.MaterialMovedReport != null)
                {
                    MaterialMoved(request.MaterialMovedReport);
                }
                else if (request.TablePlacedReport != null)
                {
                    TablePlaced(request.TablePlacedReport);
                }
                else if (request.TableRemovedReport != null)
                {
                    TableRemoved(request.TableRemovedReport);
                }
                else if (request.PackagingUnitCreatedReport != null)
                {
                    PackagingUnitCreated(request.PackagingUnitCreatedReport);
                }
                else if (request.PackagingUnitUpdatedReport != null)
                {
                    PackagingUnitUpdated(request.PackagingUnitUpdatedReport);
                }
                else if (request.PackagingUnitDeletedReport != null)
                {
                    PackagingUnitDeleted(request.PackagingUnitDeletedReport);
                }
                else if (request.LockStateChangedReport != null)
                {
                    LockStateChanged(request.LockStateChangedReport);
                }
                else if (request.SpliceChainSplitReport != null)
                {
                    SpliceChainSplit(request.SpliceChainSplitReport);
                }
                else if (request.ComponentLevelIndicatorThresholdChangedReport != null)
                {
                    ComponentLevelIndicatorThresholdChanged(request.ComponentLevelIndicatorThresholdChangedReport);
                }
                else if (request.ConfigurationChangedReport != null)
                {
                    ConfigurationChanged(request.ConfigurationChangedReport);
                }
                else if (request.NewSetupControlDataRequestedReport != null)
                {
                    NewSetupControlDataRequested(request.NewSetupControlDataRequestedReport);
                }
                else if (request.PrinterCapabilitiesChangedReport != null)
                {
                    PrinterCapabilitiesChanged(request.PrinterCapabilitiesChangedReport);
                }
                else if (request.PrinterConsumablesUpdatedReport != null)
                {
                    PrinterConsumablesUpdated(request.PrinterConsumablesUpdatedReport);
                }
                else if (request.PrinterCoverClosedReport != null)
                {
                    PrinterCoverClosed(request.PrinterCoverClosedReport);
                }
                else if (request.PrinterCoverOpenedReport != null)
                {
                    PrinterCoverOpened(request.PrinterCoverOpenedReport);
                }
                else if (request.PrinterDeviceStateChangedReport != null)
                {
                    PrinterDeviceStateChanged(request.PrinterDeviceStateChangedReport);
                }
                else if (request.PrinterMaterialUnverifiedReport != null)
                {
                    PrinterMaterialUnverified(request.PrinterMaterialUnverifiedReport);
                }
                else if (request.PrinterMaterialVerifiedReport != null)
                {
                    PrinterMaterialVerified(request.PrinterMaterialVerifiedReport);
                }
                else if (request.PrinterRecipeVerifiedReport != null)
                {
                    PrinterRecipeVerified(request.PrinterRecipeVerifiedReport);
                }
                else if (request.PrinterToolUnverifiedReport != null)
                {
                    PrinterToolUnverified(request.PrinterToolUnverifiedReport);
                }
                else if (request.PrinterToolVerifiedReport != null)
                {
                    PrinterToolVerified(request.PrinterToolVerifiedReport);
                }
            }

            return new SetupCenterEventResponse();
        }

     
        #endregion
    }

    #endregion
}