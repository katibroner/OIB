#region Copyright
// Siplace Traceability OIB Tutorial - Copyright (C) ASM Assembly Systems 2013
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region DOC_MainProgram
using System;
using System.ServiceModel;
using schemas.xmlsoap.org.ws._2004._08.eventing;

namespace TraceOIBTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the service host...");

            // create the service class which implements the Traceability OIB Contract
            TraceOIBEventingWebService service = new TraceOIBEventingWebService();

            // create a service host for our contract implementation
            ServiceHost serviceHost = new ServiceHost(service);

            // open the web service
            serviceHost.Open();
            Console.WriteLine(string.Format("Service host started with endpoint {0}", serviceHost.Description.Endpoints[0].Address.Uri));

            // Prepare the OIB Eventing subscription            
            OIBSubscriptionData subscriptionData = new OIBSubscriptionData
            {
                OIBHost = "localhost",
                OIBHTTPPort = 1405,
                OIBTCPPort = 1406,
                ServiceUri = serviceHost.ChannelDispatchers[0].Listener.Uri,
                Topic = "SIPLACE:OIB:Traceability:Notify",
                LineFilter = null, // "MyLine",
            };

            // Activate / Deactivate client authentication
            OIBSubscriptionHelper.UseClientAuthentication = false;

            // subscribe for events..
            Identifier subscriptionId = OIBSubscriptionHelper.Subscribe(subscriptionData);
            Console.WriteLine("Subscribed for topic " + subscriptionData.Topic);

            // wait for end...
            Console.WriteLine("Press <enter> to terminate the service and unsubscribe from events.");
            Console.ReadLine();

            // Now unsubscribe. If messages have to be spooled during application is not running
            // comment this line out.
            OIBSubscriptionHelper.Unsubscribe(subscriptionId, subscriptionData);

            // shut down the service host
            serviceHost.Close();
        }
    }
}
#endregion
