//-----------------------------------------------------------------------
// <copyright file="EventHelloWorldReportRequest.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System.ServiceModel;
using OIB.ServiceTutorial.Contract.Data;

#endregion

namespace OIB.ServiceTutorial.Contract.Message
{
	#region DOC_MESSAGE_RESPONSE_REQUEST

	/// <summary>
	/// This class wraps the event content EventHelloWorldReport in to a message
	/// (untyped object) and adds the subsciption topic.
	/// </summary>
	[MessageContract(WrapperNamespace = Constants.NamespaceUriMessages)]
	public class EventHelloWorldReportRequest : MessageBase
	{
	    public EventHelloWorldReportRequest()
		{
			SubscriptionTopic = Constants.MyApplicationNotifyTopic;
		}

		public EventHelloWorldReportRequest(EventHelloWorldReport eventHelloWorldReport)
		{
			SubscriptionTopic = Constants.MyApplicationNotifyTopic;
			Resource = eventHelloWorldReport;
		}

	    [MessageBodyMember(Name = "EventHelloWorldReport", Namespace = Constants.OibNamespaceUriMessages)]
	    public EventHelloWorldReport Resource { get; set; }
	}

	#endregion
}