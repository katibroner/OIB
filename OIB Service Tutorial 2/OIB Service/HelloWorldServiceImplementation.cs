//-----------------------------------------------------------------------
// <copyright file="HelloWorldServiceImplementation.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

using System.ServiceModel;

using OIB.ServiceTutorial.Contract;

namespace OIB.Tutorial.Service
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class HelloWorldServiceImplementation : IHelloWorldService
	{
		#region IService Members

		public string SayHello()
		{
			return "Hello World";
		}

		#endregion
	}
}