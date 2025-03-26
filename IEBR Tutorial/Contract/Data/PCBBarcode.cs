#region Copyright
// SIPLACE BoardGateKeeper - Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
using System;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
#endregion

namespace Siplace.ExternalBarcodeReader.Contract.Data
{
    /// <summary>
    /// A container for the barcode data. It contains the barcode value (incl. panel barcodes), lane, 
    /// origin, board side and the reader ID, which uniquely identifies the reader.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Constants.NamespaceUriData)]
    public class PCBBarcode
    {
        #region Members
        [XmlIgnore]
        private string m_strBarcode;
        [XmlIgnore]
        private string[] m_strSubBarcodes;
        [XmlIgnore]
        private Lane m_Lane;
        [XmlIgnore]
        private SubLane m_Sublane;
        [XmlIgnore]
        private ScannerSide m_ScannerSide;
        [XmlIgnore]
        private string m_strReaderID;
        [XmlIgnore]
        private bool m_bIsTimeSet;
        [XmlIgnore]
        private string m_strScanTime;

        [XmlIgnore] private const string s_strFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ";

        #endregion

        #region Constructors

        #endregion

        #region Overrides
        /// <summary>
        /// this method is used mainly for debugging. We just need to see what's inside of the actual object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //append the barcode
            sb.Append("Barcode: " + Barcode + " ");

            //append additional information
            sb.Append("Scanner side:" + ScannerSide.ToString() + " ");
            sb.Append("Lane:" + (Lane == Lane.Unknown ? "Unknown" : (Lane == Lane.Left ? "left" : "right")) + " ");
            sb.Append("Sublane:" + (Sublane == SubLane.None ? "None" : (Sublane == SubLane.Left ? "left" : "right")) + " ");
            sb.Append("ReaderID:" + ReaderID + " ");
            sb.Append("ScanDateTime:" + ScanTime + " ");
            sb.Append("IsTimeSet:" + IsTimeSet + " ");

            return sb.ToString();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the unique identification (GUID) of the barcode reader
        /// </summary>
        [DataMember]
        public string ReaderID
        {
            get { return m_strReaderID; }
            set { m_strReaderID = value; }
        }

        /// <summary>
        /// Gets the main barcode
        /// </summary>
        [DataMember]
        public string Barcode
        {
            get
            {
                return m_strBarcode;
            }
            set
            {
                m_strBarcode = value;
            }
        }

        /// <summary>
        /// Gets the panel barcodes
        /// </summary>
        [DataMember]
        public string[] SubBarcodes
        {
            get { return m_strSubBarcodes; }
            set { m_strSubBarcodes = value; }
        }

        /// <summary>
        /// Gets the lane
        /// </summary>
        [DataMember]
        public Lane Lane
        {
            get { return m_Lane; }
            set { m_Lane = value; }
        }

        /// <summary>
        /// Gets the sublane
        /// </summary>
        [DataMember]
        public SubLane Sublane
        {
            get { return m_Sublane; }
            set { m_Sublane = value; }
        }

        /// <summary>
        /// Gets/Sets the side 
        /// </summary>
        [DataMember]
        public ScannerSide ScannerSide
        {
            get { return m_ScannerSide; }
            set { m_ScannerSide = value; }
        }

        /// <summary>
        /// Checks if the scan time is set
        /// </summary>
        [DataMember]
        public bool IsTimeSet
        {
            get { return m_bIsTimeSet; }
            set { m_bIsTimeSet = value; }
        }

        /// <summary>
        /// Gets the time when the barcode has been scanned
        /// </summary>
        [DataMember]
        public string ScanTime
        {
            get { return m_strScanTime; }
            set { m_strScanTime = value; }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Converts a DateTime object into string using the specified format
        /// </summary>
        /// <param name="dt">a DateTime object</param>
        /// <returns></returns>
        public static string GetDateAsString(DateTime dt)
        {
            return dt.ToUniversalTime().ToString(s_strFormatString);
        }

        /// <summary>
        /// Converts the string into a DateTime object using the specified format
        /// </summary>
        /// <param name="dateStr">a string</param>
        /// <returns></returns>
        public static DateTime GetStringAsDate(string dateStr)
        {
            DateTime dt = DateTime.ParseExact(dateStr, s_strFormatString, System.Globalization.CultureInfo.InvariantCulture);
            return dt;
        }
        #endregion
    }  
}
