#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region Using



#endregion

namespace www.siplace.com.OIB._2015._10.LineControlServer.Contracts.Data
{
    /// <summary>
    /// The conveyor lane 
    /// </summary>
    public enum Conyevor
    {
        /// <summary>
        /// Right lane.
        /// </summary>
        Right = 1,

        /// <summary>
        /// Left lane.
        /// </summary>
        Left = 2
    }

    /// <summary>
    /// The MES decision result values 
    /// </summary>
    public enum MesDecision
    {
        /// <summary>
        /// The operation shall continue.
        /// </summary>
        OK = 0,

        /// <summary>
        /// The operation is rejected.
        /// </summary>
        Rejected = 1,

        /// <summary>
        /// An error has been occurred.
        /// </summary>
        Error = 2
    }

    /// <summary>
    /// Barcode reader position.
    /// </summary>
    public enum BarcodeReadPosition
    {
        /// <summary>
        /// The PCB Barcode has been read from a position above the PCB.
        /// </summary>
        AbovePCB = 1,

        /// <summary>
        /// The PCB Barcode has been read from a position below the PCB.
        /// </summary>
        BelowPCB = 2,

        /// <summary>
        /// The position of the PCB Barcode reader is unknown.
        /// </summary>
        Unknown = 3
    }
}