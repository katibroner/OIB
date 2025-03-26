#region Copyright
//-----------------------------------------------------------------------
// <copyright file="ILog.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------
#endregion

#region Namespace

using System;

#endregion

namespace OIB.Tutorial.Common.Logging
{
    /// <summary>
    /// Simple interface for a message logger. 
    /// The main form implements this interface
    /// and writes messages to its message view.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The message as string</param>
        void InfoMessage(string message);

        /// <summary>
        /// Logs a error
        /// </summary>
        /// <param name="message">The message as string</param>
        /// <param name="ex">The exception</param>
        void ErrorMessage(string message, Exception ex = null);

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">The message as string</param>
        /// <param name="ex">The exception</param>
        void WarnMessage(string message, Exception ex = null);

        /// <summary>
        /// Logs a debug warning
        /// </summary>
        /// <param name="message">The message as string</param>
        /// <param name="ex">The exception</param>
        void DebugMessage(string message, Exception ex = null);
    }
}