//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------


namespace BGKOibTutorial
{
    #region DOC_BoardNotificationResultValues
	/// <summary>
    /// The result values 
    /// </summary>
    public enum BoardNotificationResultValues
    {
        /// <summary>
        /// The MES confirmed the board for next processing step
        /// </summary>
        Confirmed = 1,

        /// <summary>
        /// The MES rejected the board. The board has to be locked in the conveyor and must not enter the next processing step.
        /// </summary>
        Rejected = 2,

        /// <summary>
        /// An internal error (exception etc.) occurred in MES.
        /// The conveyor will be stopped and the board must be removed from the line. The attribute "reason" may contain an error
        /// message.
        /// </summary>
        Internal_Error = 3,

        /// <summary>
        /// The MES rejected the board. The board has to be passed without producing it
        /// </summary>
        PassThrough = 4
    }
	#endregion
}
