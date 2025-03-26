#region Copyright
// Siplace Traceability OIB Tutorial - Copyright (C) ASM Assembly Systems 2013
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using schemas.xmlsoap.org.ws._2004._08.eventing;
using System.ServiceModel;
using System.Xml;
using System.Xml.XPath;
using System.ServiceModel.Dispatcher;
#endregion

namespace TraceOIBTutorial
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
        public Uri ServiceUri;
        /// <summary>
        /// The host name or IP address of the OIB core computer
        /// </summary>
        public string OIBHost;
        /// <summary>
        /// The http port of OIB Core
        /// </summary>
        public int OIBHTTPPort;
        /// <summary>
        /// The tcp port of OIB Core
        /// </summary>
        public int OIBTCPPort;
        /// <summary>
        /// A SiplacePro line name if a filtered subscription is needed (null otherwise)
        /// </summary>
        public string LineFilter;    
    }

    /// <summary>
    /// A static class which is used to subscribe and unsubscribe an service uri at OIB Eventing
    /// </summary>
    static class OIBSubscriptionHelper
    {
        #region Properties

        /// <summary>
        /// If true use client authentication
        /// </summary>
        public static bool UseClientAuthentication = true;

        #endregion

        #region Public methods
        /// <summary>
        /// Subscribe for eventing
        /// </summary>
        /// <param name="data">the subscription data</param>
        public static Identifier Subscribe(OIBSubscriptionData data)
        {
            #region DOC_RENEW

            // First, we check, if there is already an existing subscription (if so, we only renew it).
            Identifier subscriptionId = null;

            ISubscriptionManager subscriptionManagerProxy = GetSubscriptionManagerProxy(data.OIBHost, data.OIBHTTPPort);
            try
            {
                // Query all subscriptions for my endpoint URI
                SubscriptionDescriptor subscriptionDescriptor = new SubscriptionDescriptor();
                subscriptionDescriptor.Endpoint =
                    EndpointAddressAugust2004.FromEndpointAddress(new EndpointAddress(data.ServiceUri));

                Subscription[] subscriptions = subscriptionManagerProxy.GetSubscriptions(subscriptionDescriptor);
                foreach (Subscription subscription in subscriptions)
                {
                    if (subscription.Delivery.NotifyTo.ToEndpointAddress().Uri.AbsoluteUri ==
                        data.ServiceUri.AbsoluteUri)
                    {
                        // We found the subscription: we renew it (causing dead queues immediately to reactivate).
                        RenewRequest renewRequest = new RenewRequest();
                        renewRequest.Identifier = subscription.Manager.Identifier;
                        renewRequest.SubscriptionTopic = data.Topic;
                        renewRequest.Renew = new Renew();
                        renewRequest.Renew.Expires = new Expires();

                        // Expires never
                        renewRequest.Renew.Expires.Value = DateTime.MaxValue.ToUniversalTime();

                        subscriptionManagerProxy.Renew(renewRequest); // Throws on error

                        // remember the subscription identifier for unsubscribe
                        subscriptionId = subscription.Manager.Identifier;
                        break;
                    }
                }
            }
            finally
            {
                CloseChannel(subscriptionManagerProxy);
            }
            #endregion

            #region DOC_SUBSCRIBE
            if (subscriptionId == null)    // In case, the subscription does not yet exist: subscribe
            {
                Subscribe subscribe = new Subscribe();
                subscribe.Delivery = new Delivery();
                if (!string.IsNullOrEmpty(data.LineFilter))
                {
                    string xPathFilter = string.Format("Line[.=\"{0}\"]", data.LineFilter);
                    subscribe.Filter = CreateTraceabilityFilter(xPathFilter);
                }

                // Setting the Delivery mode
                // Here set it to the Push with Acknowledge
                subscribe.Delivery.DeliveryMode =
                    "http://schemas.xmlsoap.org/ws/2004/08/eventing/DeliveryModes/PushWithAck";
                EndpointAddress notifyTo = new EndpointAddress(data.ServiceUri);
                subscribe.Delivery.NotifyTo = EndpointAddressAugust2004.FromEndpointAddress(notifyTo);

                // lifetime of the subscription
                // Here: Forever
                Expires exp = new Expires();
                exp.Value = DateTime.MaxValue.ToUniversalTime();
                subscribe.Expires = exp;

                // action - Subscribe to an topic here the Setup Center events.
                SubscribeRequest subscribeRequest = new SubscribeRequest{SubscriptionTopic = data.Topic, Subscribe = subscribe};

                subscriptionManagerProxy = GetSubscriptionManagerProxy(data.OIBHost, data.OIBHTTPPort);
                try
                {
                    SubscribeResponse subscribeResponse = subscriptionManagerProxy.Subscribe(subscribeRequest);
                    // remember the subscription identifier for unsubscribe:
                    subscriptionId = subscribeResponse.SubscribeResponse1.SubscriptionManager.Identifier;
                }
                finally
                {
                    CloseChannel(subscriptionManagerProxy);
                }
            }
            #endregion

            return subscriptionId;
        }

        /// <summary>
        /// Unsubscribe from eventing
        /// </summary>
        /// <param name="subscriptionId">the subscription id to unsubscribe</param>
        /// <param name="data">the subscription data to unsubscribe</param>
        public static void Unsubscribe(Identifier subscriptionId, OIBSubscriptionData data)
        {
            #region DOC_UNSUBSCRIBE

            // Unsubscribe for events
            if (subscriptionId != null)
            {
                ISubscriptionManager subscriptionManagerProxy = GetSubscriptionManagerProxy(data.OIBHost, data.OIBHTTPPort);
                try
                {
                    EndpointAddress notifyTo = new EndpointAddress(data.ServiceUri);

                    UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest(
                        EndpointAddressAugust2004.FromEndpointAddress(notifyTo),
                        subscriptionId,
                        data.Topic,
                        new Unsubscribe());

                    subscriptionManagerProxy.Unsubscribe(unsubscribeRequest);
                }
                finally
                {
                    CloseChannel(subscriptionManagerProxy);                    
                }
            }
            #endregion
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates a proxy to access the service locator.
        /// </summary>
        /// <param name="oibHost">the host name of OIB Core computer</param>
        /// <param name="oibPortHttp">the http port of subscription manager</param>
        /// <returns>a proxy to the subscription manager</returns>
        private static ISubscriptionManager GetSubscriptionManagerProxy(string oibHost, int oibPortHttp)
        {
            WSHttpBinding httpBinding = new WSHttpBinding();
            AdjustBindingParametersReliable(httpBinding);
            var strAddress = string.Format(
                    "http://{0}:{1}/ASM.AS.Oib.WS.Eventing.Services/SubscriptionManager/ReliableSecure",
                    oibHost, oibPortHttp);

            // Add endpoint identity to provide information about service identity and open secure communication to service
            EndpointAddress address = new EndpointAddress(new Uri(strAddress), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

            var factory = new ChannelFactory<ISubscriptionManager>(httpBinding, address);

            // Revocation list not used
            factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            #region Use Client Authentication

            // Usage of client authentication
            if (UseClientAuthentication)
            {
                // Add client certificate to authenticate to service

                // **** IMPORTANT ****
                //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                //- uncomment to use client authentication
                //factory.Credentials.ClientCertificate.SetCertificate(
                //    StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
            }

            #endregion

            return factory.CreateChannel();
        }

        private static void CloseChannel(ISubscriptionManager proxy)
        {
            // Close the proxy. 
            // In case the connection is faulted or an exception has occurred: abort the proxy.
            IClientChannel channel = proxy as IClientChannel;
            if (channel == null)
                return;

            if (channel.State != CommunicationState.Faulted)
            {
                try
                {
                    channel.Close();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    try
                    {
                        channel.Abort();
                    }
                    catch
                    {
                        //
                    }
                }
            }
            else
            {
                try
                {
                    channel.Abort();
                }
                catch
                {
                    //
                }
            }
        }

        private static void AdjustBindingParametersReliable(WSHttpBinding bind)
        {
            if (bind != null)
            {
                bind.AllowCookies = false;
                bind.BypassProxyOnLocal = true;
                bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
                bind.OpenTimeout = new TimeSpan(0, 0, 10);
                bind.CloseTimeout = new TimeSpan(0, 0, 10);
                bind.SendTimeout = new TimeSpan(0, 0, 10);
                bind.MaxBufferPoolSize = 524288;
                bind.MaxReceivedMessageSize = 2147483647;
                bind.ReaderQuotas.MaxArrayLength = 2147483647;
                bind.ReaderQuotas.MaxBytesPerRead = 2147483647;
                bind.ReaderQuotas.MaxDepth = 2147483647;
                bind.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                bind.ReaderQuotas.MaxStringContentLength = 2147483647;
                bind.ReliableSession.Enabled = true;
                bind.ReliableSession.Ordered = true;
                bind.TransactionFlow = false;
                bind.UseDefaultWebProxy = false;

                // Use message security for secure endpoint
                bind.Security.Mode = SecurityMode.Message;

                // Usage of client authentication  binding properties
                bind.Security.Message.ClientCredentialType = UseClientAuthentication ? MessageCredentialType.Certificate : MessageCredentialType.None;
            }
        }

        /// <summary>
        /// Create an XPath filter object for the traceabilityData queue
        /// </summary>
        /// <param name="xpathFilter">XPath 1.0 expression</param>
        /// <returns>a XPathMessageFilter object representing a query on an XML document defined by xpathFilter parameter</returns>
        private static XPathMessageFilter CreateTraceabilityFilter(string xpathFilter)
        {
            // Add a filter
            const string strXml = "<TraceabilityData xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                                    "<completionStatusType>MessageComplete</completionStatusType>" +
                                    "</TraceabilityData>";
            XmlTextReader reader = new XmlTextReader(strXml, XmlNodeType.Document, null);
            XPathDocument xmlDoc = new XPathDocument(reader);
            XPathNavigator nav = xmlDoc.CreateNavigator();

            if (nav.NameTable == null)
                return null;
            
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nav.NameTable);
            nsmgr.AddNamespace("a", "http://www.w3.org/2005/08/addressing");
            nsmgr.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
            nsmgr.AddNamespace("i", "http://www.w3.org/2001/XMLSchema-instance");
            nsmgr.AddNamespace("h", "http://www.Siemens.com/Oib/SubscriptionTopic");
            nsmgr.AddNamespace("d", "http://www.siplace.com/OIB/2012/03/Traceability/Contracts/Data");
            nsmgr.AddNamespace("m", "http://www.siplace.com/OIB/2012/03/Traceability/Contracts/Message");

            return new XPathMessageFilter("//d:" + xpathFilter, nsmgr);                                
        }
        #endregion
    }
}
