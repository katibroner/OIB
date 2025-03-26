#region Copyright
// ASM Device Manager - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Windows;
using Asm.As.Oib.Common.Utilities;
using Asm.As.Oib.WS.Eventing.Contracts.Data;
using Asm.As.Oib.WS.Eventing.Contracts.Messages;
using Asm.As.Oib.WS.Eventing.Contracts.Service;
using MaterialManagerEventsTutorial.ViewModels;
using www.asm.com.MaterialManager_2018_11_Contracts_Service;

namespace MaterialManagerEventsTutorial
{
    /// <summary>
    /// The EventsController
    /// </summary>
    public static class EventsController
    {
        #region constant fields

        private const string _topic = "MaterialManager.Events";

        private const string _localhost = "localhost";

        /// <summary>
        /// This is an unique Uri to events receiver.
        /// In case a receiver is an UI client please use Guid part in the Uri to allow to use many of client's instances.
        /// Other way, when a receiver is a single service (i.e. Windows service) please do not use any Guid part in the Uri.
        /// </summary>
        private static readonly string s_siMMTestClient = "SiMMTestClient/" + Guid.NewGuid().ToString();

        #endregion constant fields

        #region connection info

        /// <summary>
        /// Gets or sets the name of the core host.
        /// </summary>
        /// <value>
        /// The name of the core host.
        /// </value>
        public static string CoreHostName { get; set; } = _localhost;

        /// <summary>
        /// Gets or sets the core HTTP port.
        /// </summary>
        /// <value>
        /// The core HTTP port.
        /// </value>
        public static int CoreHttpPort { get; set; } = 1405;

        /// <summary>
        /// Gets or sets if client authentication should be used
        /// </summary>
        public static bool UseClientAuthentication { get; set; }

        /// <summary>
        /// The callback URI
        /// </summary>
        private static Uri _callbackUri;

        /// <summary>
        /// Gets or sets the callback URI.
        /// </summary>
        /// <value>
        /// The callback URI.
        /// </value>
        public static Uri CallbackUri
        {
            get
            {
                if (_callbackUri == null)
                {
                    _callbackUri = new Uri($"net.tcp://{Environment.MachineName}:{54561}/{s_siMMTestClient}");
                }
                return _callbackUri;

            }
            set { _callbackUri = value; }
        }

        #endregion connection info

        /// <summary>
        /// The subscription identifier
        /// </summary>
        private static Identifier _subscriptionIdentifier;

        /// <summary>
        /// Gets a value indicating whether this instance is subscribed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is subscribed; otherwise, <c>false</c>.
        /// </value>
        public static bool IsSubscribed => _subscriptionIdentifier != null;

        private static Identifier GetSubscriptionIdentifier(ISubscriptionManager subscriptionManager)
        {
            List<Identifier> subscriptionIdentifierCollection = new List<Identifier>();

            // !!! Very important:
            // to find out a subscription on a server-site correctly the SubscriptionDescriptor should have 
            // SubscriptionDescriptor.Id or SubscriptionDescriptor.Endpoint and SubscriptionDescriptor.Topic
            // or at least must have either both
            // SubscriptionDescriptor.Id       or
            // SubscriptionDescriptor.Endpoint and SubscriptionDescriptor.Topic
            // defined.
            // For example:
            // 1. if SubscriptionDescriptor has only Id defined, on a server site it will be found all subscriptions with this Id regardless the topic i.e. the subscription with a topic for a certain client is unambiguous identified by Id; 
            // 2. if SubscriptionDescriptor has only Topic defined, on a server site it will be found all subscriptions with this Topic regardless the Id and Endpoint;
            // 3. if SubscriptionDescriptor has only Endpoint defined, on a server site it will be found all subscriptions with this Endpoint regardless the Id and Topic;
            // !!! Very important.
            var subscriptionDescriptor = new SubscriptionDescriptor
            {
                Topic = _topic,
                Endpoint = EndpointAddressAugust2004.FromEndpointAddress(new EndpointAddress(CallbackUri))
            };

            // Get all subscriptions which are currently registered for this SubscriptionDescriptor
            List<Subscription> subscriptions = subscriptionManager.GetSubscriptions(subscriptionDescriptor);

            foreach (Subscription subscription in subscriptions)
            {
                if (subscription.Delivery.NotifyTo.Uri.AbsoluteUri.Contains(s_siMMTestClient))
                {
                    subscriptionIdentifierCollection.Add(subscription.Manager.Identifier);
                }
            }

            if (subscriptionIdentifierCollection.Count > 1)
                throw new Exception("There are more than one subscriptions on this endpoint!");

            return subscriptionIdentifierCollection.FirstOrDefault();
        }


        #region DOC_SiMM_UNSUBSCRIBE

        /// <summary>
        /// Unsubscribe from SiMM events.
        /// Do not need to unsubscribe an expired subscription 
        /// </summary>
        public static void Unsubscribe()
        {
            // first check if subscription exists.
            if(IsSubscribed == false)
                return;

            ISubscriptionManager subscriptionManager = GetSubscriptionManager(out var address);

            try
            {
                MessageBox.Show(
                    $"Deleting subscription(s) to SiMM events provider \nat: {address.Uri.AbsoluteUri}",
                    MainPanelViewModel.GetAppName);

                // Deleting subscription
                // Create a request on OIB to delete subscriptions with Identifier or SubscriptionTopic and Endpoint
                var request = new UnsubscribeRequest
                {
                    // !!! Very important:
                    // to remove subscription on server-site correctly the UnsubscribeRequest should have 
                    // UnsubscribeRequest.Identifier or UnsubscribeRequest.Endpoint and UnsubscribeRequest.SubscriptionTopic
                    // or at least must have either defined
                    // UnsubscribeRequest.Identifier or
                    // UnsubscribeRequest.Endpoint   and UnsubscribeRequest.SubscriptionTopic
                    // for example:
                    // 1. if UnsubscribeRequest has only Identifier defined, on a server site it will be removed all subscriptions with this Identifier i.e. the subscription with a topic for a certain client is unambiguous identified by Identifier; 
                    // 2. if UnsubscribeRequest has only SubscriptionTopic defined, on a server site it will be removed all subscriptions with this SubscriptionTopic regardless the Identifier and Endpoint;
                    // 3. if UnsubscribeRequest has only Endpoint defined, on a server site it will be removed all subscriptions with this Endpoint regardless the Identifier and SubscriptionTopic;
                    // !!! Very important:
                    Identifier = _subscriptionIdentifier,
                    SubscriptionTopic = _topic,
                    Endpoint = EndpointAddressAugust2004.FromEndpointAddress(new EndpointAddress(CallbackUri)),
                    Body = new Unsubscribe()
                };

                // Send the request to OIB
                subscriptionManager.Unsubscribe(request);
            }
            finally
            {
                ResetSubscription();

                if (subscriptionManager != null)
                    EndpointHelper.CloseChannel(subscriptionManager);
            }
        }

        #endregion DOC_SiMM_UNSUBSCRIBE


        #region DOC_SiMM_CREATE_SUBSCRIPTION

        /// <summary>
        /// Renews the or create subscription to SiMM events.
        /// In the common case for renew subscription by timer do call subscriptionManager.Renew(RenewRequest request).
        /// Other way, the subscriptionManager.UpdateSubscription(UpdateSubscriptionRequest request) is used then the subscription parameters are changed:
        /// for instance EndpointAddress or Filter or both
        /// </summary>
        public static void RenewOrCreateSubscription()
        {
            ISubscriptionManager subscriptionManager = GetSubscriptionManager(out var address);

            try
            {
                // Check if we have a lost subscription (e.g. on application crash)
                // this case applicable only for non unique Uri (Windows service)
                if (_subscriptionIdentifier == null)
                {
                    _subscriptionIdentifier = GetSubscriptionIdentifier(subscriptionManager);
                }

                // Create an expiry date (time) for subscriptions.
                // !!! Very important:
                // Please never use an "infinity" subscription expiry time like DateTime expiry = DateTime.MaxValue.ToUniversalTime();
                // The endless subscription can lead a pretty dance the OIB Eventing Service. In case the subscribed application runs out
                // the OIB Eventing Service's data base could be overcome with unreceived messages. 
                // General recommendation is:
                // 1. for an UI application use a subscription expiry time not a more than 1 hour and do a subscription renew half the expiry time.
                // 2. for a service application, for example Windows service, use a subscription expiry time doubled for possible outcome
                //    and do a subscription renew for one fourth the expiry time.
                // Please note: messages to subscriber, coming after a subscription time expiry will be lost and not delivered after the next subscription renew.
                // !!! Very important.
                DateTime expiry = DateTime.Now.AddHours(1).ToUniversalTime();

                // renew already existing one subscription
                if (_subscriptionIdentifier != null)
                {
                    try
                    {
                        // In the common case for renew subscription by timer do call subscriptionManager.Renew(RenewRequest request).
                        // Other way, the subscriptionManager.UpdateSubscription(UpdateSubscriptionRequest request) is used then the subscription parameters are changed:
                        // for instance EndpointAddress or Filter

                        // Create a request to OIB to renew the current subscription
                        var request = new RenewRequest
                        {
                            Identifier = _subscriptionIdentifier,
                            SubscriptionTopic = _topic,
                            Body = new Renew { Expires = new Expires(expiry) }
                        };

                        // Send the renew request to OIB
                        subscriptionManager.Renew(request);

                        MessageBox.Show(
                            $"Renewing subscription for SiMM events provider\nat: {address.Uri.AbsoluteUri}\ndone with {_subscriptionIdentifier}",
                            "EventsController");
                    }
                    catch
                    {
                        ResetSubscription();
                    }
                }

                // at here could be two cases: there is no subscription in the beginning (e.g. on system startup) or
                // exception on subscriptionManager.Renew(request) due e.g. subscription expire
                if (_subscriptionIdentifier == null)
                {
                    // Create a new subscription.
                    // Creating a subscription descriptor makes an application instance a receiver of material manager events.
                    // The subscription exists on the server side until a expiry time is not gone. After that the subscription will be deleted and client belongs to it becomes no one event any more.
                    var subscribe = new Subscribe
                    {
                        Delivery = new Delivery(DeliveryModeType.PushWithAck)
                        {
                            NotifyTo = new EndpointAddress(CallbackUri)
                        },
                        Expires = new Expires(expiry),

                        // The name of the client application. 
                        // Use this to specify a human-readable application name. The application name can be seen in Subscription Administration Studio Plugin.
                        ClientAppName = Process.GetCurrentProcess().ProcessName
                    };

                    var request = new SubscribeRequest()
                    {
                        SubscriptionTopic = _topic,
                        Subscribe = subscribe
                    };

                    SubscribeResult result = subscriptionManager.Subscribe(request).Body;
                    _subscriptionIdentifier = result.SubscriptionManager.Identifier;

                    // subscription done - log the success message
                    MessageBox.Show(
                        $"Subscribing for SiMM events provider at: {address.Uri.AbsoluteUri}\ndone with {_subscriptionIdentifier}",
                        MainPanelViewModel.GetAppName);
                }
            }
            finally
            {
                if (subscriptionManager != null)
                    EndpointHelper.CloseChannel(subscriptionManager);
            }
        }

        private static ISubscriptionManager GetSubscriptionManager(out EndpointAddress address)
        {
            // Add endpoint identity to provide information about service identity and open secure communication to service
            address = new EndpointAddress(new Uri(GetEndpointAddress()), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

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

            // Create the connection objects
            var binding = EndpointHelper.CreateBindingFromParameters(bindingParameters);

            var factory = new ChannelFactory<ISubscriptionManager>(binding, address);

            // Revocation list not used
            factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            #region Use Client Authentication

            // Usage of client authentication
            if (UseClientAuthentication)
            {
                // Add client certificate to authenticate to service

                // **** IMPORTANT ****
                // Option 1: Use app.config central client authentication parameters
                factory.Credentials.ClientCertificate.SetCertificate(
                    bindingParameters.ClientCertificateParameters.CertificateStoreLocation,
                    bindingParameters.ClientCertificateParameters.CertificateStoreName,
                    bindingParameters.ClientCertificateParameters.CertificateFindType,
                    bindingParameters.ClientCertificateParameters.CertificateValue);

                // Option 2: use other client authentication parameters
                //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                //factory.Credentials.ClientCertificate.SetCertificate(
                //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
            }

            #endregion

            return factory.CreateChannel();
        }

        #endregion DOC_SiMM_CREATE_SUBSCRIPTION


        /// <summary>
        /// Resets the subscription.
        /// </summary>
        public static void ResetSubscription()
        {
            _subscriptionIdentifier = null;
        }


        #region DOC_SiMM_CREATE_SERVICEENDPOINT

        /// <summary>
        /// Creates the service endpoint for eventing.
        /// </summary>
        public static void CreateServiceEndpointForEventing()
        {
            try
            {
                // create WCF endpoint host
                ThreadedCallbackHost.StartCallback(CallbackUri.ToString(),
                    EventsReceiver.Instance,
                    typeof(IMaterialManagerEventsDuplex), false, out string newCallbackAddress);
                CallbackUri = new Uri(newCallbackAddress);
                IsServiceEndpointExists = true;

                MessageBox.Show(
                    $"Service endpoint with callback address {CallbackUri} \ncreated.",
                    MainPanelViewModel.GetAppName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Exception occurred when initializing callback service endpoint:\n {ex.Message}.",
                    MainPanelViewModel.GetAppName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                throw;
            }
        }

        #endregion DOC_SiMM_CREATE_SERVICEENDPOINT

        /// <summary>
        /// Gets the endpoint address.
        /// </summary>
        /// <returns></returns>
        private static string GetEndpointAddress()
        {
            return $"http://{CoreHostName}:{CoreHttpPort}/Asm.As.Oib.WS.Eventing.Services/SubscriptionManagerSecure";
        }

        /// <summary>
        /// Gets a value indicating whether this instance is service endpoint exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is service endpoint exists; otherwise, <c>false</c>.
        /// </value>
        public static bool IsServiceEndpointExists { get; private set; } = false;

    }
}