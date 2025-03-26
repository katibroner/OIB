#region Copyright
// SIPLACE EBR Contract - Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
using System.Text;
using System.Runtime.Serialization;
#endregion

namespace Siplace.ExternalBarcodeReader.Contract.Data
{
    /// <summary>
    /// Holds the basic information about the scanner and its parameters
    /// </summary>    
    [DataContract(Namespace = Constants.NamespaceUriData)]
    public class EBRReaderConfigurationData
    {
        #region Members

        #endregion

        #region Overrides
        /// <summary>
        /// Mainly for debugging here. Shows the current content of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("EBRLaneConfigurationData:" + " ");
            sb.Append("SupportsLineNumber:" + SupportsLaneNumber + " ");
            sb.Append("SupportsConveyorStop:" + SupportsConveyorStop);
            return sb.ToString();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the information wheeather the BarcodeAdapter supports the stop conveyor function
        /// </summary>
        [DataMember]
        public bool SupportsConveyorStop { get; set; }

        /// <summary>
        /// Gets the information wheather the BarcodeAdapter provides the information about the lane number
        /// </summary>
        [DataMember]
        public bool SupportsLaneNumber { get; set; }

        #endregion
    }
}