//-----------------------------------------------------------------------
// <copyright file="ILog.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    /// Simple interface for a message logger. 
    /// The main form implements this interface
    /// and writes messages to its message view.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Info message
        /// </summary>
        /// <param name="message">The message as string</param>
        void Info(string message);

        /// <summary>
        /// Error message
        /// </summary>
        /// <param name="message">The error message as string</param>
        void Error(string message);

        /// <summary>
        /// Error message with exception
        /// </summary>
        /// <param name="message">The message as string</param>
        /// <param name="ex">The exception</param>
        void Error(string message, Exception ex);

        /// <summary>
        /// Logs a Setup Center message.
        /// </summary>
        /// <param name="message">The message as string</param>
        void InfoMessage(string message);

        /// <summary>
        /// Logs a error which has occured while processing a Setup Center message.
        /// </summary>
        /// <param name="message">The message as string</param>
        /// <param name="ex">The exception</param>
        void ErrorMessage(string message, Exception ex);
    }
}