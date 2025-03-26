//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Using

using System;
using System.ServiceModel;
using www.siplace.com.OIB._2011._07.BoardGateKeeper.Contracts.Service;

#endregion

namespace BGKOibTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            #region DOC_GetLineConfigurations

            try
            {
                // Please note: This method was introduced in R17-1 and only works for BGK 2.2 or higher
                Console.WriteLine("Starting to call into BGK for getting the current lines...");
                BoardNotificationServerClient bgkClient = new BoardNotificationServerClient();
                LineConfigurationRequestResult result = bgkClient.BgkLineConfigurationsRequest(new LineConfigurationRequestData());

                int numberOfLines = 0;
                if (result!= null && result.LineConfigurations != null)
                {
                    numberOfLines = result.LineConfigurations.Length;
                }
                Console.WriteLine("Got '{0}' configured lines.", numberOfLines);
                if (numberOfLines > 0)
                {
                    Console.WriteLine("Details: ");
                    if (result != null && result.LineConfigurations != null)
                    {
                        foreach (var line in result.LineConfigurations)
                        {
                            Console.WriteLine("LineName: '{0}', Active: '{1}', OperationMode: '{2}'", line.LineName,
                                line.Active, line.OperationMode == 1 ? "Interlocking" : "Notification");
                        }
                    }
                }
                Console.WriteLine("Finished calling into BGK for getting the current lines.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception while calling BgkLineConfigurationsRequest(): " + e);
            }
            #endregion
            #region DOC_Main

            Console.WriteLine("Starting the BoardNotificationWebService service host...");

            // create the service class which implements the BoardNotification OIB Contract
            BoardNotificationWebService service = new BoardNotificationWebService();

            // create a service host for our contract implementation
            ServiceHost serviceHost = new ServiceHost(service);

            // open the web service
            serviceHost.Open();
            Console.WriteLine(string.Format("BoardNotificationWebService Service host started with endpoint {0}",
                serviceHost.Description.Endpoints[0].Address.Uri));

            string coreComputerName = "localhost";

            if (args.Length > 0)
            {
                coreComputerName = args[0];
            }

            Console.WriteLine("Registering the BoardNotificationWebService service host at OIB core at computer: " + coreComputerName);


            // register at oib
            OIBServiceLocatorHelper.RegisterServiceAtOIB(
                "BoardNotification.Service", 
                "BoardNotification Tutorial",
                "A sample application implementing the IBoardNotification interface",
                serviceHost.Description.Endpoints[0].Address.Uri, coreComputerName);
            Console.WriteLine("Service successfully registered BoardNotificationWebService at OIB");
            
            // wait for end...
            Console.WriteLine("Press <enter> to terminate BoardNotificationWebService");
            Console.ReadLine();

            // shut down the servicehost
            serviceHost.Close();
            #endregion
        }
    }
}
