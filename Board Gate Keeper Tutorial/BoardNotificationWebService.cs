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
using System.IO;
using System.ServiceModel;
using System.Xml.Serialization;
using www.siplace.com.OIB._2011._07.BoardGateKeeper.Contracts.Service;

#endregion

namespace BGKOibTutorial
{
    /// <summary>
    /// The implementation of IBoardNotification interface
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class BoardNotificationWebService : IBoardNotification
    {
        public bool Ping()
        {
            Console.WriteLine("Ping called");
            return true;
        }

        public BoardRequestResult BoardRequest(BoardRequestData data)
        {
            Console.WriteLine(string.Format("BoardRequest called for barcode {0}", data.Board.Barcode));

            //create the virtual inkspot handling data to be added into the BoardRequestResult
            var vihData = CreateVirtualInkspotHandlingData();
           
            return new BoardRequestResult
                {
                    RequestResult = BoardNotificationResultValues.Confirmed.ToString(),
                    Reason = "Confirmed by OIB Tutorial",
                    BoardPath = "Here comes the complete path where the board in Siplace Pro.",
                    BoardSide = "Top",
                    VIHResult = vihData,
                    // In case you want to set another barcode (instead of the scanned data.Board.Barcode), you can override that here
                    OverridingBarcode = "JHGF"
            };
        }

      

        public void BoardReleased(BoardReleasedData data)
        {
            Console.WriteLine(string.Format("BoardReleased called for barcode {0}", data.Board.Barcode));
        }

        public void BoardFailed(BoardFailedData data)
        {
            Console.WriteLine(string.Format("BoardFailed called for barcode {0}", data.Board.Barcode));
        }

        public void ScannerConnectionStateChanged(ScannerConnectionChangedData data)
        {
            Console.WriteLine(string.Format("ScannerConnectionStateChanged called for scanner {0}", data.ScannerId));
        }

        public void LineConfigurationChanged(LineConfiguration lineConfiguration)
        {
            Console.WriteLine(string.Format("LineConfigurationChanged: Line:{0}, Active:{1}, LineConfigurationChangeType:{2}, OperationMode:{3}, TimeOfChange:{4}", 
                lineConfiguration.LineName, lineConfiguration.Active, lineConfiguration.LineConfigurationChangeType, lineConfiguration.OperationMode, lineConfiguration.TimeOfChange));
         }

        #region Helper Methods
        /// <summary>
        /// create a structure demo of the virtual inkspot handling data
        /// </summary>
        /// <returns></returns>
        private static VirtualInkspotHandlingData CreateVirtualInkspotHandlingData()
        {
            
            var vihData = new VirtualInkspotHandlingData();
            //get the xml file and deserialize the subPanels from there.
            //In real environment this part should come from the MES system
            string xmlFilePath =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"XmlData\\VIHData.xml");
            if (File.Exists(xmlFilePath))
            {
                vihData = (VirtualInkspotHandlingData)DeSerialize<VirtualInkspotHandlingData>(xmlFilePath);
            }
            
            return vihData;
        }

        public static string Serialize<T>(T data, string path)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter sw = new StreamWriter(path);
            xmlSerializer.Serialize(sw, data);
            return sw.ToString();
        }

        public static object DeSerialize<T>(string path)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            TextReader rdr = new StreamReader(path);
            var result = (T)xmlSerializer.Deserialize(rdr);
            return result;
        }
        #endregion
    }
}
