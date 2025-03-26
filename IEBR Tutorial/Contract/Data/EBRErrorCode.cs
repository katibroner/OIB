#region Copyright
// SIPLACE BoardGateKeeper - Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
#endregion

namespace Siplace.ExternalBarcodeReader.Contract.Data
{
    /// <summary>
    /// Defines all possible return codes occuring during the WCF communication
    /// </summary>
    public enum EBRErrorCode
    {
        /// <summary>
        /// a default error code (unknown error)
        /// </summary>
        EC_UNKNOWN = 0,
        /// <summary>
        /// everything went allright
        /// </summary>
        EC_SUCCESS = 1,
        /// <summary>
        /// the subscriber has been alredy subscribed
        /// </summary>
        EC_ALREADY_SUBSCRIBED = 2,
        /// <summary>
        /// callback failed
        /// </summary>
        EC_CALLBACK_FAILED = 3,
        /// <summary>
        /// this subscriber hasn't subscribed for the events on the server
        /// </summary>
        EC_UNKNOWN_SUBSCRIBER = 4,
        /// <summary>
        /// this reader isn't registered in the application
        /// </summary>
        EC_UNKNOWN_READER = 5,
        /// <summary>
        /// a hardware error occured
        /// </summary>
        EC_HARDWARE_ERROR = 6,
        /// <summary>
        /// a miscellaneous error occured
        /// </summary>
        EC_MISC_ERROR = 7,
        /// <summary>
        /// The received barcode is not allowed, e.g. the line isn’t configured for external barcode reader
        /// </summary>
        EC_BARCODE_NOT_ALLOWED = 8,
        /// <summary>
        /// Barcode processing failed due to an unexpected error. 
        /// </summary>
        EC_BARCODE_PROCESSING_FAILED = 9,
        /// <summary>
        /// The external MES system isn't available
        /// </summary>
        EC_TARGET_NOT_AVAILABLE = 10,
        /// <summary>
        /// The external MES system rejected the current board
        /// </summary>
        EC_BOARD_REJECTED_BY_TARGET = 11,
        /// <summary>
        /// A timeout occurred while processing the barcode. This may be happen if process interlocking is activated
        /// and the external (MES/TraceHost) System does not respond
        /// </summary>
        EC_TARGET_TIMEOUT = 12,
        /// <summary>
        /// A endpoint was not found!! Scanner may be offline
        /// </summary>
        EC_ENDPOINT_NOT_FOUND = 13,
    }
}
