#region Copyright

// ASM Device Manager - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

#region using

using System;
using System.ServiceModel;
using www.asm.com.MaterialManager_2018_11_Contracts_Service;

#endregion using

/// <summary>
/// The Material Manager Tutorial Application
/// </summary>
namespace MaterialManagerEventsTutorial
{
    #region DOC_SiMM_EVENTSRECEIVER_CLASS

    /// <summary>
    /// The receiver of the material manager events
    /// </summary>
    /// <seealso cref="www.asm.com.MaterialManager_2018_11_Contracts_Service.IMaterialManagerEventsDuplex" />
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventsReceiver : IMaterialManagerEventsDuplex
    {
        /// <summary>
        /// Occurs when [event received].
        /// </summary>
        public event EventHandler<MaterialManagerEventReceivedEventArgs> EventReceived;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static EventsReceiver Instance { get; } = new EventsReceiver();

        /// <summary>
        /// Publishes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public PublishResponse Publish(MaterialManagerMessageRequest request)
        {
            OnEventReceived(request);
            return new PublishResponse();
        }

        /// <summary>
        /// Called when [event received].
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void OnEventReceived(MaterialManagerMessageRequest data)
        {
            EventReceived?.Invoke(this, new MaterialManagerEventReceivedEventArgs(data));
        }
    }

    #endregion DOC_SiMM_EVENTSRECEIVER_CLASS
}