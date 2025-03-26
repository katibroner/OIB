#region Copyright
// Siplace Traceability OIB Tutorial - Copyright (C) ASM Assembly Systems 2013
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region DOC_TraceOIBEventingWebService
using System;
using System.ServiceModel;
using www.siplace.com.OIB._2012._03.Traceability.Contracts.Service;

namespace TraceOIBTutorial
{
    /// <summary>
    /// The implementation if the Traceability OIB Contract
    /// (duplex interface is used to inform OIB Eventing if the event was delivered successfully)
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class TraceOIBEventingWebService : ITraceabilityDataDuplex
    {

        public NewTraceabilityDataResponse NewTraceabilityData(TraceabilityDataRequest request)
        {
            Console.WriteLine("SUCCESS: Received traceability data for boardId: " + request.TraceabilityData.BoardID);

            return new NewTraceabilityDataResponse();
        }
    }
}
#endregion