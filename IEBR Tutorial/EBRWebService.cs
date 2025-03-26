#region Copyright

// Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.

#endregion

#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Siplace.ExternalBarcodeReader.Contract.Service;
using Siplace.ExternalBarcodeReader.Contract.Data;
using System.ServiceModel;

#endregion

namespace EBRExample
{     
    /// <summary>
    /// EBRWebService implements the IEBR interface and all work flows needed to communicate with SIPLACE
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]    
    public class EBRWebService : IEBR, IEBRWebService
    {
        #region structs
        private struct SubscriberData
        {
            public string SubscriberName;
            public string CallbackUri;
            public string ReaderId;
        }
        #endregion

        #region Members
        private readonly EBRReaderConfigurationData m_readerConfiguration;
        private readonly Dictionary<string, SubscriberData> m_subscribers = new Dictionary<string, SubscriberData>();
        /// <summary>
        /// A WCF Server object
        /// </summary>
        private ServiceHost m_serviceHost;
        #endregion

        #region Constructors
        public EBRWebService(EBRReaderConfigurationData config)
        {
            // define the configuration structure
            m_readerConfiguration = config;
        }        
        #endregion

        #region IEBR Methods
        /// <summary>
        /// A description for the adapter
        /// </summary>
        public string AdapterDescription
        {
            get { return "Example EBR Adapter"; }
        }
        /// <summary>
        /// BoardGateKeeper subscribes for the Barcode events on the BarcodeAdapter
        /// </summary>
        /// <param name="subscriberName">the name of the subscriber</param>
        /// <param name="callbackInfo">information about callback service (BoardGateKeeper)</param>
        /// <param name="uniqueKey">An unique identification of the BarcodeAdapter to recognize the source of the barcode events</param>
        /// <returns>error code</returns>
        public EBRErrorCode SubscribePCBEvents(string subscriberName, CallbackInformation callbackInfo, string uniqueKey)
        {
            lock (m_subscribers)
            {
                // first check if we already have this subscriber
                if (m_subscribers.ContainsKey(subscriberName))
                {
                    SendError("SubscribePCBEvents failed due to Subscriber is already subscribed", null);
                    return EBRErrorCode.EC_ALREADY_SUBSCRIBED;
                }

                // now create a new subscriber info
                SubscriberData subscriber = new SubscriberData
                                                {
                        SubscriberName = subscriberName,
                        CallbackUri = String.Empty,// has to be determined
                        ReaderId = uniqueKey,
                    };

                // determine the callback
                string succeededUri = String.Empty;
                List<string> ipList = new List<string> {callbackInfo.Hostname};
                ipList.AddRange(callbackInfo.EndAddresses);
                Exception lastException = null;
                foreach (string ipAddress in ipList)
                {
                    string uriString = String.Format("http://{0}:{1}/{2}", ipAddress, callbackInfo.Port, callbackInfo.ServiceName);

                    // ping every Uri
                    try
                    {
                        PingCallback(uriString);
                        succeededUri = uriString;
                        break;
                    }
                    catch (Exception ex)
                    {
                        lastException = ex;
                    }
                }

                // No Uri found? Cancel the subscription
                if (succeededUri == String.Empty)
                {                    
                    SendError("SubscribePCBEvents failed due to Callback failed. Last Exception was: "+ (lastException != null ? lastException.Message : "No exception"), lastException);
                    return EBRErrorCode.EC_CALLBACK_FAILED;
                }

                subscriber.CallbackUri = succeededUri;
                m_subscribers.Add(subscriberName, subscriber);
                // Inform the caller about the subscription change
                if (SubscriptionChangedEvent != null)
                {
                    SubscriptionChangedEvent(this, new SubscriptionChangedArgs
                                                       {
                                                           SubscriberName = subscriberName,
                                                           Subscribed = true,
                                                       });
                }
                return EBRErrorCode.EC_SUCCESS;
            }
        }

        /// <summary>
        /// Unsubscribe from the BarcodeAdapter
        /// </summary>
        /// <param name="subscriberName">the name of the subscriber</param>
        /// <returns>error code</returns>
        public EBRErrorCode UnSubscribePCBEvents(string subscriberName)
        {
            lock (m_subscribers)
            {
                if (!m_subscribers.ContainsKey(subscriberName))
                {
                    return EBRErrorCode.EC_UNKNOWN_SUBSCRIBER;
                }
                m_subscribers.Remove(subscriberName);
                if (SubscriptionChangedEvent != null)
                {
                    SubscriptionChangedEvent(this, new SubscriptionChangedArgs
                                                       {
                                                           SubscriberName = subscriberName,
                                                           Subscribed = false,
                                                       });
                }
                return EBRErrorCode.EC_SUCCESS;
            }
        }

        /// <summary>
        /// Gets the current state of the connection to the BarcodeAdapter
        /// </summary>
        /// <param name="subscriberName">the name of the subscriber</param>
        /// <returns>error code</returns>
        public EBRErrorCode GetState(string subscriberName)
        {
            lock (m_subscribers)
            {
                SubscriberData subscriber;
            
                if (!m_subscribers.TryGetValue(subscriberName, out subscriber))
                {
                    return EBRErrorCode.EC_UNKNOWN_SUBSCRIBER;
                }
                try
                {
                    // Ping the callback
                    PingCallback(subscriber.CallbackUri);
                }
                catch (Exception)
                {
                    return EBRErrorCode.EC_TARGET_NOT_AVAILABLE;
                }
                    
                return EBRErrorCode.EC_SUCCESS;
            }            
        }

        /// <summary>
        /// Gets the current configuration of the BarcodeAdapter
        /// </summary>
        /// <returns>a structure with the configuration settings</returns>
        public EBRReaderConfigurationData GetReaderConfiguration()
        {
            return m_readerConfiguration;
        }

        /// <summary>
        /// Gets all subscribers subscribed at the BarcodeAdapter
        /// </summary>
        /// <returns></returns>
        public Subscriber[] GetSubscribers()
        {
            List<Subscriber> allSubscribers = new List<Subscriber>();
            foreach (SubscriberData subscriber in m_subscribers.Values)
                allSubscribers.Add(new Subscriber
                                       {
                    SubscriberName = subscriber.SubscriberName,
                    Uri = subscriber.CallbackUri,
                });
            return allSubscribers.ToArray();
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Create a proxy to the receiver interface of a subscriber
        /// </summary>
        /// <param name="uriString">the IEBRReceive endpoint of subscriber</param>
        /// <returns></returns>
        private static IEBRReceive CreateReceiverProxy(string uriString)
        {
            ChannelFactory<IEBRReceive> factory = new ChannelFactory<IEBRReceive>(new BasicHttpBinding());
            EndpointAddress endpoint = new EndpointAddress(new Uri(uriString));
            IEBRReceive ebrProxy = factory.CreateChannel(endpoint);
            return ebrProxy;
        }
        /// <summary>
        /// Close a receiver proxy
        /// </summary>
        /// <param name="proxy">the proxy object</param>
        private static void CloseReceiverProxy(IEBRReceive proxy)
        {
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
        /// <summary>
        /// Method to ping a callback address which is a IEBRReceive web service
        /// </summary>
        /// <param name="uriString">the Uri of the IEBRReceive web service</param>
        private static void PingCallback(string uriString)
        {
            IEBRReceive proxy = CreateReceiverProxy(uriString);
            proxy.Ping();
            CloseReceiverProxy(proxy);
        }
        /// <summary>
        /// Sends an error message to all subscribers of ErrorMessageEvent queue
        /// </summary>
        /// <param name="message">the message text</param>
        /// <param name="ex">the exception thrown</param>
        private void SendError(string message, Exception ex)
        {
            if (ErrorMessageEvent != null)
            {
                ErrorMessageEvent(this, new ErrorMessageEventArgs
                                            {
                                                Message = message,
                                                InnerException = ex,
                                            });
            }
        }
        #endregion

        #region IEBRWebService Methods
        public event SubsciptionChangedEventHandler SubscriptionChangedEvent;
        public event ErrorMessageEventHandler ErrorMessageEvent;

        void IEBRWebService.StartService()
        {
            //don't try to start the service more than once
            if (m_serviceHost != null)
                return;

            m_serviceHost = new ServiceHost(this);
               
            m_serviceHost.Open();
        }
        /// <summary>
        /// aborts the thread with the EBRWebService
        /// </summary>
        /// <returns>success</returns>
        void IEBRWebService.StopService()
        {
            if (m_serviceHost != null && m_serviceHost.State == CommunicationState.Opened)
            {
                m_serviceHost.Close();
                m_serviceHost = null;
            }
        }

        int IEBRWebService.SubscriberCount
        {
            get { return m_subscribers.Count; }
        }

        BarcodeProcessingResult IEBRWebService.SendBarcode(PCBBarcode pcbBarcode)
        {
            lock (m_subscribers)
            {
                if (m_subscribers.Count == 0)
                {
                    return new BarcodeProcessingResult
                               {
                                   ErrorCode = EBRErrorCode.EC_MISC_ERROR,
                                   Reason = "No subscriber!!",
                                   ReleaseBoard = false,
                    };
                }

                BarcodeProcessingResult combinedResult = new BarcodeProcessingResult
                {
                    ReleaseBoard = true,
                    ErrorCode = EBRErrorCode.EC_SUCCESS,
                    Reason = String.Empty,
                };
                foreach (SubscriberData subscriber in m_subscribers.Values)
                {
                    try
                    {
                        IEBRReceive proxy = CreateReceiverProxy(subscriber.CallbackUri);
                        pcbBarcode.ReaderID = subscriber.ReaderId;
                        BarcodeProcessingResult lastResult = proxy.SendBarcode(pcbBarcode);

                        if ((combinedResult.ReleaseBoard) && (!lastResult.ReleaseBoard))
                            combinedResult.ReleaseBoard = false;
                        if ((combinedResult.ErrorCode == EBRErrorCode.EC_SUCCESS) && (lastResult.ErrorCode != EBRErrorCode.EC_SUCCESS))
                        {
                            // we always keep the last result
                            combinedResult.ErrorCode = lastResult.ErrorCode;
                            combinedResult.Reason = lastResult.Reason;
                        }
                        CloseReceiverProxy(proxy);
                    }
                    catch (EndpointNotFoundException ex)
                    {
                        SendError(ex.Message, ex);
                        // the endpoint was not found
                        return new BarcodeProcessingResult
                                   {
                                       ErrorCode = EBRErrorCode.EC_ENDPOINT_NOT_FOUND,
                                       Reason = ex.Message,
                                       ReleaseBoard = false,
                        };
                    }
                    catch (CommunicationException ex)
                    {
                        SendError(ex.Message, ex);
                        // any other communication exception
                        return new BarcodeProcessingResult
                                   {
                            ErrorCode = EBRErrorCode.EC_TARGET_NOT_AVAILABLE,
                            Reason = ex.Message,
                            ReleaseBoard = false,
                        };
                    }
                    catch (Exception ex)
                    {
                        SendError(ex.Message, ex);
                        // any other exception (not expected)
                        return new BarcodeProcessingResult
                                   {
                            ErrorCode = EBRErrorCode.EC_MISC_ERROR,
                            Reason = ex.Message,
                            ReleaseBoard = false,
                        };
                    }
                }
                // return the result of the subscribers
                return combinedResult;
            }            
        }
        #endregion
    
    }
}
