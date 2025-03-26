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
    /// An interface defining the implemented contract on the channel from a BarcodeAdapter to the BoardGateKeeper
    /// </summary>
    [ServiceContract(Namespace = Constants.NamespaceUriService)]
    public interface IEBRReceive
    {
        /// <summary>
        /// Sends a barcode structure to the BoardGateKeeper
        /// </summary>
        /// <param name="pcbBarcode">a structure with barcode data</param>
        /// <returns>a structure containing the information about the processing</returns>
        [OperationContract]
        [FaultContract(typeof(EndpointNotFoundException))]
        BarcodeProcessingResult SendBarcode(PCBBarcode pcbBarcode);

        /// <summary>
        /// Gets the current connection state
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(EndpointNotFoundException))]
        void Ping();          
    }           
}
