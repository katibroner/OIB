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
using Siplace.ExternalBarcodeReader.Contract.Data;

#endregion

namespace EBRExample
{
    /// <summary>
    /// A structure to define the SubsciptionChangedEvent arguments
    /// </summary>
    public struct SubscriptionChangedArgs
    {
        /// <summary>
        /// The name of the subscriber
        /// </summary>
        public string SubscriberName;
        /// <summary>
        /// The current subscription state: true = subscribed, false = unsubscripted
        /// </summary>
        public bool Subscribed;
    }

    /// <summary>
    /// A structure to define the ErrorMessageEvent arguments
    /// </summary>
    public struct ErrorMessageEventArgs
    {
        /// <summary>
        /// The Exception occurred
        /// </summary>
        public Exception InnerException;
        /// <summary>
        /// An error message
        /// </summary>
        public string Message;
    }

    /// <summary>
    /// the delegate for the subscription change
    /// </summary>
    /// <param name="sender">sender of event</param>
    /// <param name="args">event data</param>
    public delegate void SubsciptionChangedEventHandler(object sender, SubscriptionChangedArgs args);

    /// <summary>
    /// the delegate for the error message event
    /// </summary>
    /// <param name="sender">sender of event</param>
    /// <param name="args">event data</param>
    public delegate void ErrorMessageEventHandler(object sender, ErrorMessageEventArgs args);


    #region DOC_IEBRWebService
    /// <summary>
    /// The interface of our WebService
    /// </summary>
    public interface IEBRWebService
    {
        /// <summary>
        /// An event which is fired if someone subscribed or unsubscribed
        /// </summary>
        event SubsciptionChangedEventHandler SubscriptionChangedEvent;

        /// <summary>
        /// An event which is used to retrieve error messages from the WebService
        /// </summary>
        event ErrorMessageEventHandler ErrorMessageEvent;

        /// <summary>
        /// Starts the ServiceHost for the IEBR instance
        /// </summary>
        void StartService();

        /// <summary>
        /// Stops the ServiceHost for the IEBR instance
        /// </summary>
        void StopService();

        /// <summary>
        /// The count of subscribers
        /// </summary>
        int SubscriberCount { get; }

        /// <summary>
        /// Send a barcode to all registered barcode consumers
        /// </summary>
        /// <param name="pcbBarcode"></param>
        /// <returns></returns>
        BarcodeProcessingResult SendBarcode(PCBBarcode pcbBarcode);
    }
    #endregion

}
