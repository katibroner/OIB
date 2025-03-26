#region Copyright
// SIPLACE BoardGateKeeper - Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Siplace.ExternalBarcodeReader.Contract.Data
{
    /// <summary>
    /// Lane number
    /// </summary>
    public enum Lane
    {
        /// <summary>
        /// Lane is not set
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// the right lane
        /// </summary>
        Right = 1,

        /// <summary>
        /// the left lane
        /// </summary>
        Left = 2
    }
    /// <summary>
    /// Sublane number
    /// </summary>
    public enum SubLane
    {
        /// <summary>
        /// sublane is not set
        /// </summary>
        None = 0,

        /// <summary>
        /// the right lane
        /// </summary>
        Right = 1,

        /// <summary>
        /// the left sublane
        /// </summary>
        Left = 2,
    }

    /// <summary>
    /// Defines all possible scanner' sides
    /// </summary>
    public enum ScannerSide
    {
        /// <summary>
        /// an unknown (unspecified) scanner side
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// barcode was scanned from the top
        /// </summary>
        ScannedFromTop = 1,
        /// <summary>
        /// barcode was scanned from the bottom
        /// </summary>
        ScannedFromBottom = 2,
    }
}
