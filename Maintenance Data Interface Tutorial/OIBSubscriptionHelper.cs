#region Copyright

// ASM MaintenanceData OIB Tutorial - Copyright (C) ASM Assembly Systems 2013
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.

#endregion

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using schemas.xmlsoap.org.ws._2004._08.eventing;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Asm.As.Oib.Common.Utilities;
using Asm.As.Oib.Monitoring.Proxy.Architecture.Objects;

#endregion

namespace MaintenanceDataTutorial
{
    /// <summary>
    /// the context data for OIB eventing subscription
    /// </summary>
    public struct OIBSubscriptionData
    {
        /// <summary>
        /// The topic of subscription
        /// </summary>
        public string Topic;

        /// <summary>
        /// The URI of our web service which has to be registered as event consumer
        /// </summary>
        public Uri CallbackUri;
    }

    /// <summary>
    /// A static class which is used to subscribe and unsubscribe an service uri at OIB Eventing
    /// </summary>
    static class OIBSubscriptionHelper
    {
        #region fields

        private static ISubscriptionManager _subscriptionManager;
        private static ChannelFactory<ISubscriptionManager> _factorySubscriptionManager;

        #endregion

        public static string SubscriptionManagerEndpoint { get; set; }

        public static bool UseClientAuthentication{ get; set; }

        #region Public methods

        /// <summary>
        /// Subscribe for eventing
        /// </summary>
        /// <param name="data">the subscription data</param>
        /// <param name="filterLine">The filter option for subscription queue</param>
        public static Subscription Subscribe(OIBSubscriptionData data, string filterLine = null)
        {
            #region DOC_RENEW

            // First, we check, if there is already an existing subscription (if so, we only renew it).
            Subscription currentSubscription = null;

            var subscriptionManager = GetSubscriptionManager();
            using (subscriptionManager as IDisposable)
            {
                // Query all subscriptions for my endpoint URI
                var subscriptionDescriptor = new SubscriptionDescriptor
                {
                    Endpoint = EndpointAddressAugust2004.FromEndpointAddress(new EndpointAddress(data.CallbackUri))
                };

                var subscriptions = subscriptionManager.GetSubscriptions(subscriptionDescriptor);
                foreach (var subscription in subscriptions)
                {
                    if (subscription.Delivery.NotifyTo.ToEndpointAddress().Uri.AbsoluteUri ==
                        data.CallbackUri.AbsoluteUri)
                    {
                        // We found the subscription: we renew it (causing dead queues immediately to reactivate).
                        var renewRequest = new RenewRequest
                        {
                            Identifier = subscription.Manager.Identifier,
                            SubscriptionTopic = data.Topic,
                            Renew = new Renew {Expires = new Expires {Value = DateTime.MaxValue.ToUniversalTime()}}
                        };

                        // Expires never
                        subscriptionManager.Renew(renewRequest); // Throws on error

                        // remember the subscription identifier for unsubscribe
                        currentSubscription = subscription;
                        break;
                    }
                }
            }

            #endregion

            #region DOC_SUBSCRIBE

            if (currentSubscription != null) return currentSubscription;

            var subscribe = new Subscribe {Delivery = new Delivery()};

            #region DOC_SUBSCRIBE_FILTER

            if (!string.IsNullOrEmpty(filterLine))
                subscribe.Filter = CreateXPathMessageFilter(XPathFilterDataType.LineFullPath, new List<string>{filterLine}, false);

            #endregion

            // Setting the Delivery Mode
            // Here set it to the Push with Acknowledge
            subscribe.Delivery.DeliveryMode =
                "http://schemas.xmlsoap.org/ws/2004/08/eventing/DeliveryModes/PushWithAck";
            var notifyTo = new EndpointAddress(data.CallbackUri);
            subscribe.Delivery.NotifyTo = EndpointAddressAugust2004.FromEndpointAddress(notifyTo);

            // lifetime of the subscription
            // Here: Forever
            var exp = new Expires {Value = DateTime.UtcNow.Add(TimeSpan.FromHours(6))};
            subscribe.Expires = exp;

            // action - Subscribe to an topic here the Setup Center events.
            var subscribeRequest = new SubscribeRequest {SubscriptionTopic = data.Topic, Subscribe = subscribe};

            subscriptionManager = GetSubscriptionManager();
            using (subscriptionManager as IDisposable)
            {
                var subscribeResponse = subscriptionManager.Subscribe(subscribeRequest);
                // remember the subscription identifier for unsubscribe:
                currentSubscription = subscribeResponse.SubscribeResponse1.Subscription;
            }

            #endregion

            return currentSubscription;
        }

        /// <summary>
        /// Unsubscribe from eventing
        /// </summary>
        /// <param name="subscription">The subscription to unsubscribe</param>
        public static void Unsubscribe(Subscription subscription)
        {
            #region DOC_UNSUBSCRIBE
            // Unsubscribe for events
            if (subscription == null) return;
            var subscriptionManagerProxy = GetSubscriptionManager();
            using (subscriptionManagerProxy as IDisposable)
            {
                var unsubscribeRequest = new UnsubscribeRequest(
                    subscription.Manager.EndpointAddress,
                    subscription.Manager.Identifier,
                    subscription.Topic,
                    new Unsubscribe());

                subscriptionManagerProxy.Unsubscribe(unsubscribeRequest);
            }
            #endregion
        }

        public static string RenewSubscription(Subscription subscription, DateTime expiryDate)
        {
            if (subscription == null) return null;

            // We should renew the subscription (make it expire 1 day up to now)
            var renew = new RenewRequest
            {
                Identifier = subscription.Manager.Identifier,
                SubscriptionTopic = subscription.Topic,
                Renew = new Renew {Expires = new Expires {Value = expiryDate}}
            };
            GetSubscriptionManager().Renew(renew);
            subscription.Expires = expiryDate;

            return subscription.Manager.Identifier.ToString();
        }

        public static Subscription[] GetSubscriptions(SubscriptionDescriptor subscriptionDescriptor)
        {
            Subscription[] subscriptions;
            var subscriptionManager = GetSubscriptionManager();
            using (subscriptionManager as IDisposable)
            {
                subscriptions = subscriptionManager.GetSubscriptions(subscriptionDescriptor);
            }

            return subscriptions;
        }

        public static void Init(string subscriptionEndpoint)
        {
            if (_factorySubscriptionManager != null)
            {
                EndpointHelper.CloseChannel(_factorySubscriptionManager);
                _factorySubscriptionManager = null;
            }

            SubscriptionManagerEndpoint = subscriptionEndpoint;
        }

        #region DOC_CREATE_FILTER

        /// <summary>
        /// 1.) Case sensitive:
        /// //d:Line[. = "Linie 1 AbC IR"]
        /// 
        /// 2.) Case insensitive:
        /// //d:Line[translate(., 'LINE1ABCR ','line1abcr ')  = "linie 1 abc ir"]
        /// 
        /// Create an XPath filter object for the maintenance data queue
        /// </summary>
        /// <param name="xPathFilterType">XPath 1.0 expression</param>
        /// <param name="listValues">The Values associated with Data Type (i.e.ISA 95 Line Path)</param>
        /// <param name="caseInvariant">if set to <c>true</c> [case invariant].</param>
        /// <returns>a XPathMessageFilter object representing a query on an XML document defined by xpathFilter parameter</returns>
        private static XPathMessageFilter CreateXPathMessageFilter(XPathFilterDataType xPathFilterType, List<string> listValues, bool caseInvariant)
        {
            // Add a filter
            const string strXml = "<MaintenanceData xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                                  "<completionStatusType>MessageComplete</completionStatusType>" +
                                  "</MaintenanceData>";
            var reader = new XmlTextReader(strXml, XmlNodeType.Document, null);
            var xmlDoc = new XPathDocument(reader);
            var nav = xmlDoc.CreateNavigator();

            var xmlManager = new XmlNamespaceManager(nav.NameTable);
            xmlManager.AddNamespace("a", "http://www.w3.org/2005/08/addressing");
            xmlManager.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
            xmlManager.AddNamespace("i", "http://www.w3.org/2001/XMLSchema-instance");
            xmlManager.AddNamespace("h", "http://www.Siemens.com/Oib/SubscriptionTopic");
            xmlManager.AddNamespace("d", "http://www.siplace.com/OIB/2019/11/MaintenanceData/Contracts/Data");
            xmlManager.AddNamespace("m", "http://www.siplace.com/OIB/2019/11/MaintenanceData/Contracts/Message");
            var filter = CreateFilterExpression(listValues, caseInvariant);
            var xPathFilterString = $"//d:{xPathFilterType}[{filter}]";
            return new XPathMessageFilter(xPathFilterString, xmlManager);
        }

        private static string CreateFilterExpression(List<string> filters, bool caseInvariant)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < filters.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(" or ");
                }
                var strValue = filters[i];
                if (caseInvariant)
                {
                    var distinctLower = new string(strValue.ToLower().Distinct().ToArray());
                    var distinctUpper = distinctLower.ToUpper();
                    builder.AppendFormat("translate(., '{0}','{1}')  = \"{2}\"", distinctUpper, distinctLower, strValue.ToLower());
                }
                else
                {
                    builder.AppendFormat(". = \"{0}\"", strValue);
                }
            }
            return builder.ToString();
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a proxy to access the service locator.
        /// </summary>
        /// <returns>a proxy to the subscription manager</returns>
        private static ISubscriptionManager GetSubscriptionManager()
        {
            if (_factorySubscriptionManager == null)
            {
                // Add endpoint identity to provide information about service identity and open secure communication to service
                EndpointAddress address = new EndpointAddress(new Uri(SubscriptionManagerEndpoint), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

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

                var binding = EndpointHelper.CreateBindingFromParameters(bindingParameters);

                _factorySubscriptionManager = new ChannelFactory<ISubscriptionManager>(binding, address);

                // Revocation list not used
                _factorySubscriptionManager.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                #region Use Client Authentication

                // Usage of client authentication
                if (UseClientAuthentication)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****

                    // Option 1: Use app.config central client authentication parameters
                    _factorySubscriptionManager.Credentials.ClientCertificate.SetCertificate(
                        bindingParameters.ClientCertificateParameters.CertificateStoreLocation,
                        bindingParameters.ClientCertificateParameters.CertificateStoreName,
                        bindingParameters.ClientCertificateParameters.CertificateFindType,
                        bindingParameters.ClientCertificateParameters.CertificateValue);

                    // Option 2: use other client authentication parameters
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //_factorySubscriptionManager.Credentials.ClientCertificate.SetCertificate(
                    //    StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
                }

                #endregion

                EndpointHelper.ApplyDefaultBehaviors(_factorySubscriptionManager);
            }

            return EndpointHelper.GetProxyToService(_factorySubscriptionManager, ref _subscriptionManager);
        }

        #endregion
    }
}