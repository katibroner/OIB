//-----------------------------------------------------------------------
// <copyright file="Form1.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;
using OIB.ServiceTutorial.Contract;
using OIB.Tutorial.Client;
using schemas.xmlsoap.org.ws._2004._08.eventing;
using OIB.ServiceTutorial.Contract.Service;

#endregion

namespace OIB_Client
{
	public partial class Form1 : Form
	{
		#region Fields

		private Identifier _SubscriptionId;
		private static readonly string _BaseUri = "http://" + Environment.MachineName + ":34554/";
		private readonly Uri _CallbackUri = 	new Uri(_BaseUri + "ServiceClient");

		private HelloWorldServiceEventsDuplexImplementation _HelloWorldServiceEventsDuplexImplementation;
		private ServiceHost _CallbackServiceHost;
		
        #endregion

        public Form1()
		{
			InitializeComponent();
		}


	    private void AdjustBindingParameters(WSHttpBinding bind, bool useSecurity)
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
	            bind.ReliableSession.Enabled = false;
	            bind.TransactionFlow = false;
	            bind.UseDefaultWebProxy = false;

                // Use message security
                bind.Security.Mode = useSecurity ? SecurityMode.Message : SecurityMode.None;

                // Usage of client authentication binding properties
                bind.Security.Message.ClientCredentialType = _cbUseClientAuthentication.Checked ? MessageCredentialType.Certificate : MessageCredentialType.None;
	        }
        }

        /// <summary>
        /// Creates a proxy to access the service locator.
        /// </summary>
        /// <returns></returns>
        private ISubscriptionManager GetSubscriptionManagerProxy()
		{
		    WSHttpBinding httpBinding = new WSHttpBinding();
		    AdjustBindingParameters(httpBinding, true);
            httpBinding.ReliableSession.Enabled = _TextBoxSubscriptionManager.Text.Contains("/Reliable");

            // Add endpoint identity to provide information about service identity and open secure communication to service
            EndpointAddress address = new EndpointAddress(new Uri(_TextBoxSubscriptionManager.Text), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

            var factory = new ChannelFactory<ISubscriptionManager>(httpBinding, address);

            // Revocation list not used
            factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            #region Use Client Authentication

            // Usage of client authentication
            if (_cbUseClientAuthentication.Checked)
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

		private void ButtonSubscribe_Click(object sender, EventArgs e)
		{
			#region DOC_OPEN_CALLBACK_HOST

			if (_CallbackServiceHost != null)
			{
				_CallbackServiceHost.Close();
				_CallbackServiceHost = null;
			}

			_HelloWorldServiceEventsDuplexImplementation = new HelloWorldServiceEventsDuplexImplementation();
			_CallbackServiceHost = new ServiceHost(_HelloWorldServiceEventsDuplexImplementation, new[] { new Uri(_BaseUri) });
		    WSHttpBinding httpBinding = new WSHttpBinding();
		    AdjustBindingParameters(httpBinding, false);

            _CallbackServiceHost.AddServiceEndpoint(typeof(IHelloWorldServiceEventsDuplex), httpBinding, _CallbackUri);
			_CallbackServiceHost.Open();

			#endregion

            #region DOC_RENEW

            // First, we check, if there is already an existing subscription (if so, we only renew it).
            try
            {
                _SubscriptionId = null;

                ISubscriptionManager subscriptionManagerProxy = GetSubscriptionManagerProxy();
                try
                {
                    // Query all subscriptions for my endpoint URI
                    SubscriptionDescriptor subscriptionDescriptor = new SubscriptionDescriptor();
                    subscriptionDescriptor.Endpoint = 
                        EndpointAddressAugust2004.FromEndpointAddress(new EndpointAddress(_CallbackUri));

                    Subscription[] subscriptions = subscriptionManagerProxy.GetSubscriptions(subscriptionDescriptor);
                    foreach (Subscription subscription in subscriptions)
                    {
                        if (subscription.Delivery.NotifyTo.ToEndpointAddress().Uri.AbsoluteUri == _CallbackUri.AbsoluteUri)
                        {
                            // We found the subscription: we renew it (causing dead queues immediately to reactivate).
                            RenewRequest renewRequest = new RenewRequest();
                            renewRequest.Identifier = subscription.Manager.Identifier;
                            renewRequest.SubscriptionTopic = Constants.MyApplicationNotifyTopic;
                            renewRequest.Renew = new Renew();
                            renewRequest.Renew.Expires = new Expires();

                            // Expires never
                            renewRequest.Renew.Expires.Value = DateTime.MaxValue.ToUniversalTime(); 

                            subscriptionManagerProxy.Renew(renewRequest);   // Throws on error
                            
                            // remember the subscription identifier for unsubscribe
                            _SubscriptionId = subscription.Manager.Identifier;
                            break;
                        }
                    }
                }
                finally
                {
                    CloseChannel(subscriptionManagerProxy);     
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to renew subscription: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
            #endregion

            #region DOC_SUBSCRIBE

            if (_SubscriptionId == null)    // In case, the subscription does not yet exist: subscribe
            {
                try
                {
                    Subscribe subscribe = new Subscribe();
                    subscribe.Delivery = new Delivery();

                    // Setting the Deliverymode
                    // Here set it to the Push with Acknowledge
                    subscribe.Delivery.DeliveryMode =
                        "http://schemas.xmlsoap.org/ws/2004/08/eventing/DeliveryModes/PushWithAck";
                    EndpointAddress notifyTo = new EndpointAddress(_CallbackUri);
                    subscribe.Delivery.NotifyTo = EndpointAddressAugust2004.FromEndpointAddress(notifyTo);

                    // lifetime of the subscription
                    // Here: Forever
                    Expires exp = new Expires();
                    exp.Value = DateTime.MaxValue.ToUniversalTime();
                    subscribe.Expires = exp;

                    // action - Subscribe to a topic 
                    SubscribeRequest subscribeRequest = new SubscribeRequest {SubscriptionTopic = Constants.MyApplicationNotifyTopic, Subscribe = subscribe};

                    ISubscriptionManager subscriptionManagerProxy = GetSubscriptionManagerProxy();
                    try
                    {
                        SubscribeResponse subscribeResponse = subscriptionManagerProxy.Subscribe(subscribeRequest);
                        // remember the subscription identifier for unsubscribe:
                        _SubscriptionId = subscribeResponse.SubscribeResponse1.SubscriptionManager.Identifier;
                    }
                    finally
                    {
                        CloseChannel(subscriptionManagerProxy);                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to subscribe for messages: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

			#endregion
		}

		private void _ButtonUnSubscribe_Click(object sender, EventArgs e)
		{
			#region DOC_UNSUBSCRIBE

			// Unsubscribe for events
            if (_SubscriptionId != null && _CallbackServiceHost != null)
			{
				ISubscriptionManager subscriptionManagerProxy = GetSubscriptionManagerProxy();
                try
                {
                    EndpointAddress notifyTo = new EndpointAddress(
                        _CallbackServiceHost.ChannelDispatchers[0].Listener.Uri);

                    UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest(
                        EndpointAddressAugust2004.FromEndpointAddress(notifyTo),
                        _SubscriptionId,
                        "SIPLACE:OIB:TOTORIAL:SUBSCRIPTION",
                        new Unsubscribe());

                    subscriptionManagerProxy.Unsubscribe(unsubscribeRequest);
                    _SubscriptionId = null;
                }
                finally
                {
                    CloseChannel(subscriptionManagerProxy);                    
                }
			}

			// Close the Web service
			if (_CallbackServiceHost != null)
			{
				_CallbackServiceHost.Close();
				_CallbackServiceHost = null;
			}

			#endregion
		}

        private void CloseChannel(object toClose)
        {
            ICommunicationObject channel = toClose as ICommunicationObject;
            
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
    }
}