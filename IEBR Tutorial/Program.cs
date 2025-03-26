#region Copyright
// Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
using System;
using Siplace.ExternalBarcodeReader.Contract.Data;
#endregion

namespace EBRExample
{
    class Program
    {
        #region DOC_ProgramMain
        static void Main()
        {
            // create the configuration structure
            EBRReaderConfigurationData config = new EBRReaderConfigurationData
            {
                SupportsConveyorStop = true,
                SupportsLaneNumber = true,
            };

            // create the web service
            IEBRWebService webService = new EBRWebService(config);

            // register all events
            webService.ErrorMessageEvent += OnErrorMessageEvent;
            webService.SubscriptionChangedEvent += OnSubsciptionChangedEvent;
        
            // start the ServiceHost...
            Console.WriteLine("Starting the IEBR web service ...");
            try
            {
                webService.StartService();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Start failed with exception: " + ex.Message);
                Console.ReadLine();
                return;
            }            

            // now wait for barcodes...
            Console.WriteLine("Type in Barcode and press ENTER to send. Type EXIT and ENTER to shut down!");

            while (true)
            {
                // wait for end
                string barcode = Console.ReadLine();

                if ((barcode == "EXIT") || (barcode == "exit"))
                    break;

                // Send a barcode to all registered subscribers
                BarcodeProcessingResult result = webService.SendBarcode(new PCBBarcode
                {
                    IsTimeSet = true,
                    ScanTime = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    Lane = Lane.Left,
                    SubBarcodes = null,
                    Barcode = barcode,
                    ScannerSide = ScannerSide.ScannedFromTop,
                    Sublane = SubLane.None,
                });

                Console.WriteLine(String.Format("SendBarcode returned: {0} / {1} / {2}", result.ErrorCode, result.Reason, result.ReleaseBoard));
            }

            Console.WriteLine("Shutting down the IEBR web service ...");
            webService.StopService();
        }
        #endregion

        static void OnErrorMessageEvent(object sender, ErrorMessageEventArgs args)
        {
            Console.WriteLine("Error occurred: "+args.Message);
        }

        static void OnSubsciptionChangedEvent(object sender, SubscriptionChangedArgs args)
        {
            if (args.Subscribed)
                Console.WriteLine("New Subscriber: "+args.SubscriberName);
            else
                Console.WriteLine("Subscriber removed: " + args.SubscriberName);
        }

    }
}
