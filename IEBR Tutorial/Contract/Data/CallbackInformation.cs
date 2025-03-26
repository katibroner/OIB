#region Copyright
// SIPLACE EBR Contract - Copyright (C) ASM Assembly Systems 2011
// All rights reserved.
//
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied
// subject to license terms.
#endregion

#region Using
using System.Text;
using System.Runtime.Serialization;
#endregion

namespace Siplace.ExternalBarcodeReader.Contract.Data
{
    /// <summary>
    /// A container for callback information which are send to the barcode adapter
    /// According to this information the barcode adapter can identify the callback address of the BoardGateKeeper
    /// </summary>
    [DataContract(Namespace = Constants.NamespaceUriData)]
    public class CallbackInformation
    {
        #region Members
        private string m_HostName = string.Empty;
        private string m_serviceName = string.Empty;
        private string m_port = string.Empty;
        private string[] m_endAddresses;
        #endregion

        #region Constructors
        public CallbackInformation()
        {
        }
        #endregion

        #region Overrides
        /// <summary>
        /// used mainly for debugging. We just see what's inside of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Hostname:" + Hostname + "\t");
            sb.Append("ServiceName:" + ServiceName + "\t");
            sb.Append("Port:" + Port + "\t");
            if (m_endAddresses == null)
                sb.Append("IP address: There are no IP addresses registered");
            else
            {
                for (int i = 0; i < m_endAddresses.Length; i++)
                    sb.Append("IP address: " + m_endAddresses[i] + "\t");
            }
            return sb.ToString();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the port number
        /// </summary>
        [DataMember]
        public string Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        /// <summary>
        /// Gets the name of the host
        /// </summary>
        [DataMember]
        public string Hostname
        {
            get { return m_HostName; }
            set { m_HostName = value; }
        }

        /// <summary>
        /// Gets the list of the callback IP addresses
        /// </summary>
        [DataMember]
        public string[] EndAddresses
        {
            get { return m_endAddresses; }
            set { m_endAddresses = value; }
        }

        /// <summary>
        /// Gets the name of the WCF service
        /// </summary>
        [DataMember]
        public string ServiceName
        {
            get { return m_serviceName; }
            set { m_serviceName = value; }
        }
        #endregion
    }
}