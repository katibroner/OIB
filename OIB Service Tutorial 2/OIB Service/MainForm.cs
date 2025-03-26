//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.ServiceModel;
using schemas.xmlsoap.org.ws._2004._08.eventing;
using OIB.ServiceTutorial.Contract.Message;
using OIB.ServiceTutorial.Contract.Data;
using OIB.ServiceTutorial.Contract.Service;
using System.ServiceModel.Channels;


#region DOC_NAMESPACES


#endregion

#endregion

namespace OIB.Tutorial.Service
{
    public partial class mainForm : Form
    {
        #region Constructor

        public mainForm()
        {
            InitializeComponent();
        }

        #endregion

        private void ButtonSendEvent_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                #region DOC_POST_MESSAGE

                // Create the message object:
                EventHelloWorldReport eventHelloWorldReport = new EventHelloWorldReport();
                eventHelloWorldReport.Message = "Hello World";

                // Wrap the message with the report request object:
                EventHelloWorldReportRequest eventHelloWorldReportRequest =
                    new EventHelloWorldReportRequest(eventHelloWorldReport);

                // Connect to the notification service ...
                NetTcpBinding binding = new NetTcpBinding();
                AdjustBindingParameters(binding);
                // Add endpoint identity to provide information about service identity and open secure communication to service
                EndpointAddress address = new EndpointAddress(new Uri(_TextBoxNotificationManager.Text), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));

                ChannelFactory<IHelloWorldServiceEventsOneWay> notificationManagerChannelFactory = new ChannelFactory<IHelloWorldServiceEventsOneWay>(binding, address);

                // Revocation list not used
                notificationManagerChannelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                #region Use Client Authentication

                // Usage of client authentication
                if (_cbUseClientAuthentication.Checked)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication
                    //notificationManagerChannelFactory.Credentials.ClientCertificate.SetCertificate(
                    //    StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
                }

                #endregion

                // ... and use it as endpoint for the notification channel ...
                IHelloWorldServiceEventsOneWay notificationChannel = notificationManagerChannelFactory.CreateChannel();

                // ... and finally post the message.
                notificationChannel.HelloWorldEvent(eventHelloWorldReportRequest);
                ((IChannel)notificationChannel).Close();
                
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred while posting the message: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }


        private void AdjustBindingParameters(NetTcpBinding bind)
        {
            if (bind != null)
            {
                bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
                bind.OpenTimeout = new TimeSpan(0, 0, 10);
                bind.CloseTimeout = new TimeSpan(0, 0, 10);
                bind.SendTimeout = new TimeSpan(0, 1, 0);
                bind.MaxBufferPoolSize = 524288;
                bind.MaxReceivedMessageSize = 2147483647;
                bind.ReaderQuotas.MaxArrayLength = 2147483647;
                bind.ReaderQuotas.MaxBytesPerRead = 2147483647;
                bind.ReaderQuotas.MaxDepth = 2147483647;
                bind.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                bind.ReaderQuotas.MaxStringContentLength = 2147483647;
                bind.ReliableSession.Enabled = false;
                bind.TransactionFlow = false;

                // Use message security
                bind.Security.Mode = SecurityMode.Transport;

                // Usage of client authentication binding properties
                bind.Security.Transport.ClientCredentialType = _cbUseClientAuthentication.Checked ? TcpClientCredentialType.Certificate : TcpClientCredentialType.None;
            }
        }
    }
}