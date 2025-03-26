//-----------------------------------------------------------------------
// <copyright file="EventHelloWorldReport.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace
using System.Runtime.Serialization;

#endregion

namespace OIB.ServiceTutorial.Contract.Data
{
	#region DOC_MESSAGE_OBJECT

	/// <summary>
	/// This class is the content of the "HelloWorld" event.
	/// </summary>
	[DataContract(Name = "HelloWorldReport", Namespace = Constants.NamespaceUriData)]
	public class EventHelloWorldReport
	{
		[DataMember(Name = "Message")]
		public string Message
		{
			get;
			set;
		}
	}

	#endregion
}