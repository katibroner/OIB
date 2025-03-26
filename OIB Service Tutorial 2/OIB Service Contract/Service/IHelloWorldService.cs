//-----------------------------------------------------------------------
// <copyright file="IHelloWorldService.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System.ServiceModel;

#endregion

namespace OIB.ServiceTutorial.Contract
{
	/// <summary>
	/// Interface of the service to be called by the clinent.
	/// </summary>
	[ServiceContract(Namespace = Constants.NamespaceUriService)]
	public interface IHelloWorldService
	{
		[OperationContract]
		string SayHello();
	}
}