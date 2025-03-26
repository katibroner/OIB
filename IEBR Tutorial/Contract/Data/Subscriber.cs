#region Copyright
// SIPLACE EBR Contract - Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
using System.Runtime.Serialization;
#endregion

namespace Siplace.ExternalBarcodeReader.Contract.Data
{
    /// <summary>
    /// A container holding the information about hosts which have subscribed for the barcode events on a
    /// Barcode Adapter. It contains the URI and subscriber name. There can be possibly more subscribers on 
    /// one Barcode Adapter.
    /// </summary>
    [DataContract(Namespace = Constants.NamespaceUriData)]
    public class Subscriber
    {
        #region Members

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the subscriber
        /// </summary>
        [DataMember]
        public string SubscriberName { get; set; }

        /// <summary>
        /// Gets the URI of the subscriber
        /// </summary>
        [DataMember]
        public string Uri { get; set; }

        #endregion
    }
}