#region Copyright
// SIPLACE BoardGateKeeper - Copyright (C) ASM Assembly Systems 2011
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
    /// A container holding the information about the results of the SendBarcode method.
    /// It contains besides the error code a boolean value saying if we can process the board and 
    /// a text variable containing the cause of the possible error
    /// </summary>
    [DataContract(Namespace = Constants.NamespaceUriData)]    
    public class BarcodeProcessingResult
    {
        #region Members
        private EBRErrorCode m_ErrorCode;
        private bool m_bReleaseBoard;
        private string m_strReason;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the error code
        /// </summary>
        [DataMember]
        public EBRErrorCode ErrorCode
        {
            get { return m_ErrorCode; }
            set { m_ErrorCode = value; }
        }

        /// <summary>
        /// Gets the information wheather we can release the board and continue processing
        /// </summary>
        [DataMember]
        public bool ReleaseBoard
        {
            get { return m_bReleaseBoard; }
            set { m_bReleaseBoard = value; }
        }

        /// <summary>
        /// Gets the reason for the error code
        /// </summary>
        [DataMember]
        public string Reason
        {
            get { return m_strReason; }
            set { m_strReason = value; }
        }
        #endregion
    }   
}
