//-----------------------------------------------------------------------
// <copyright file="HelloWorldServiceImplementation.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System.ServiceModel;

using OIB.ServiceTutorial.Contract;

#endregion

namespace OIB.Tutorial.Service
{
	#region DOC_SERVICE_IMPLEMENTATION

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class HelloWorldServiceImplementation : IHelloWorldService
	{
		public string SayHello()
		{
			return "Hello World";
		}
	}

	#endregion
}