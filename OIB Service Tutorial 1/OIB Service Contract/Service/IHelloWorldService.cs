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
	#region DOC_SERVICE_CONTRACT

	/// <summary>
	/// The test service with the only method SayHello()
	/// </summary>
	[ServiceContract(Namespace = Constants.NamespaceUriService)]
	public interface IHelloWorldService
	{
		[OperationContract]
		string SayHello();
	}

	#endregion
}