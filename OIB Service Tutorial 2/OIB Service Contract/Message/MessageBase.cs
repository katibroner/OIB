//-----------------------------------------------------------------------
// <copyright file="MessageBase.cs" company="ASM Assembly Systems GmbH & Co. KG">
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

namespace OIB.ServiceTutorial.Contract.Message
{
	/// <summary>
	/// Base class for OIB messages implementing the member SubscriptionTopic
	/// </summary>
	[MessageContract(WrapperNamespace = Constants.OibNamespaceUriMessages)]
	public class MessageBase
	{
	    [MessageHeader(Name = Constants.OibSubscriptionTopic, Namespace = Constants.OibNamespaceUriMessages)]
	    public string SubscriptionTopic { get; set; }
	}
}