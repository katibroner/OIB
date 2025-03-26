#region Copyright
// SIPLACE EBR Contract - Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
using System.ServiceModel;
using Siplace.ExternalBarcodeReader.Contract.Data;
#endregion

namespace Siplace.ExternalBarcodeReader.Contract.Service
{
    /// <summary>
    /// An interface defining the implemented contract on the channel from the BoardGateKeeper to a BarcodeAdapter
    /// </summary>
    [ServiceContract(Namespace = Constants.NamespaceUriService)]
    public interface IEBR
    {
        /// <summary>
        /// Retrieves a description from the connected EBR adapter
        /// </summary>        
        string AdapterDescription 
        {
            [OperationContract]
            get;
        }

        /// <summary>
        /// Subscribers at the BarcodeAdapter to recive the barcode events
        /// </summary>
        /// <param name="subscriberName">the name of the subscriber</param>
        /// <param name="callbackInfo">the callback information about the BoardGateKeeper</param>
        /// <param name="uniqueKey">The unique identification of the BarcodeAdapter</param>
        /// <returns>the error code</returns>
        [OperationContract]
        [FaultContract(typeof(EndpointNotFoundException))]
        EBRErrorCode SubscribePCBEvents(string subscriberName, CallbackInformation callbackInfo, string uniqueKey);

        /// <summary>
        /// Unsubscribes from the BarcodeAdapter to stop receiveing the barcode events
        /// </summary>
        /// <param name="subscriberName">the name of the subscriber</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(EndpointNotFoundException))]
        EBRErrorCode UnSubscribePCBEvents(string subscriberName);

        /// <summary>
        /// Gets the current connection state of the BarcodeAdapter
        /// </summary>
        /// <param name="subscriberName"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(EndpointNotFoundException))]
        EBRErrorCode GetState(string subscriberName);

        /// <summary>
        /// Gets the current configuration of the BarcodeAdapter
        /// </summary>
        /// <returns>A structure containing the current configuration of the BarcodeAdapter</returns>
        [OperationContract]
        [FaultContract(typeof(EndpointNotFoundException))]
        EBRReaderConfigurationData GetReaderConfiguration();

        /// <summary>
        /// Gets the list of all subscribed hosts
        /// </summary>
        /// <returns>the list of all subscribers</returns>
        [OperationContract]
        [FaultContract(typeof(EndpointNotFoundException))]
        Subscriber[] GetSubscribers();
    }   
}
