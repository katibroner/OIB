//-----------------------------------------------------------------------
// <copyright file="IHelloWorldServiceEventsDuplex.cs" company="ASM Assembly Systems GmbH & Co. KG">
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
	#region DOC_MESSAGE_DUPLEX_CONTRACT

	/// <summary>
	/// Event interface of the service to be implemented by the client. OIB eventing calls the client on
	/// this interface on events posted by the service. The methods of this interface have to be 
	/// IsOneWay = false since a positive response is required to acknowledge the event.
	/// 
	/// Compare: The signature of this interface is equal to IHelloWorldServiceEventsOneWay!
	/// </summary>
	[ServiceContract(Namespace = Constants.NamespaceUriService)]
	public interface IHelloWorldServiceEventsDuplex
	{
		[OperationContract(Action = Constants.NamespaceUriService + @"/HelloWorldEvent", IsOneWay = false)]
		void HelloWorldEvent(EventHelloWorldReportRequest eventReportRequest);
	}

	#endregion
}