//-----------------------------------------------------------------------
// <copyright file="EventHelloWorldResponse.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <email>oib-support.siplace@asmpt.com</email>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------


#region Namespace

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace OIB.ServiceTutorial.Contract.Message
{
	[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
	public partial class EventHelloWorldResponse
	{
		public EventHelloWorldResponse()
		{
		}
	}
}
