//-----------------------------------------------------------------------
// <copyright file="HelloWorldServiceEventsDuplexImplementation.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

using OIB.ServiceTutorial.Contract.Service;
using System.Windows.Forms;
using System.ServiceModel;

namespace OIB.Tutorial.Client
{
	#region DOC_CLIENT_IMPL

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class HelloWorldServiceEventsDuplexImplementation : IHelloWorldServiceEventsDuplex
	{
		public void HelloWorldEvent(ServiceTutorial.Contract.Message.EventHelloWorldReportRequest eventReportRequest)
		{
			MessageBox.Show("SUCCESS: Received service event: " + eventReportRequest.Resource.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}

	#endregion
}