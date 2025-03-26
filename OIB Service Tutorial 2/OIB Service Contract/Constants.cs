//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
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

namespace OIB.ServiceTutorial.Contract
{
	public class Constants
	{
		public const string NamespaceUriService = "http://www.mycompany.com/Test/2009/01/MyOibTest/Contracts/Service";
		public const string NamespaceUriData = "http://www.mycompany.com/Test/2009/01/MyOibTest/Contracts/Data";
		public const string NamespaceUriMessages = "http://www.mycompany.com/Test/2009/01/MyOibTest/Contracts/Message";
		public const string MyApplicationNotifyTopic = "SIPLACE:OIB:TOTORIAL:SUBSCRIPTION";

		public const string OibNamespaceUriMessages = "http://www.siplace.com/OIB/2008/05/WS/Eventing/Contracts/Message";
		public const string OibSubscriptionTopic = "SubscriptionTopic";
	}
}