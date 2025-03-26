#region Copyright
// ASM Device Manager - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.
#endregion

using System;
using www.asm.com.MaterialManager_2018_11_Contracts_Service;

namespace MaterialManagerEventsTutorial
{
    /// <summary>
    /// The MaterialManager's Event arguments
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MaterialManagerEventReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public MaterialManagerMessageRequest Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialManagerEventReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="System.ArgumentNullException">data</exception>
        public MaterialManagerEventReceivedEventArgs(MaterialManagerMessageRequest data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Data = data;
        }
    }
}