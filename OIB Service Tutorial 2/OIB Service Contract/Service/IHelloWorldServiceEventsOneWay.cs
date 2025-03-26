//-----------------------------------------------------------------------
// <copyright file="IHelloWorldServiceEventsOneWay.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System.ServiceModel;
using OIB.ServiceTutorial.Contract.Message;

#endregion

namespace OIB.ServiceTutorial.Contract.Service
{
	#region DOC_MESSAGE_ONE_WAY_CONTRACT

	/// <summary>
	/// Event interface of the service to be used to send events to OIB eventing. It is a one
	/// way interface since the events are transferred via MSMQ.
	/// 
	/// Compare: The signature of this interface is equal to IHelloWorldServiceEventsDuplex!
	/// </summary>
	[ServiceContract(Namespace = Constants.NamespaceUriService)]
	public interface IHelloWorldServiceEventsOneWay
	{
		[OperationContract(Action = Constants.NamespaceUriService + @"/HelloWorldEvent", IsOneWay = true)]
		void HelloWorldEvent(EventHelloWorldReportRequest eventReportRequest);
	}

	#endregion
}