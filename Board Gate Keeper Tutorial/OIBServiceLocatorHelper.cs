//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
#endregion

namespace BGKOibTutorial
{
    public static class OIBServiceLocatorHelper
    {
        #region Fields

        // Field to activate Client Authentication
        private static bool UseClientAuthentication = false;

        #endregion

        #region DOC_RegisterServiceAtOIB
        /// <summary>
        /// Registering a service at OIB
        /// </summary>
        /// <param name="serviceName">Name of the service to be registered</param>
        /// <param name="productName">Name of the calling software product</param>
        /// <param name="productDesc">A description of the product</param>
        /// <param name="uri">URI of the service</param>
        /// <param name="oibCoreComputerName">Name of the OIB core computer.</param>
        /// <returns>
        /// true: success
        /// </returns>
        public static bool RegisterServiceAtOIB(string serviceName, string productName, string productDesc, Uri uri, string oibCoreComputerName)
        {
            string oibServiceLocatorUri = string.Format("http://{0}:1405/ASM.AS.OIB.servicelocatorSecure",oibCoreComputerName);
            ServiceLocatorClient proxy = null;
            try
            {
                WSHttpBinding wsBinding = new WSHttpBinding("wsHttpBindingConfig");

                // Adjust binding including security parameters
                AdjustBindingParameters(wsBinding, true);

                // Add endpoint identity to provide information about service identity and open secure communication to service
                EndpointAddress address = new EndpointAddress(new Uri(oibServiceLocatorUri), EndpointIdentity.CreateDnsIdentity("ASM.SW.Service"));
                proxy = new ServiceLocatorClient(wsBinding, address);

                // Revocation list not used
                proxy.ChannelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;


                #region Use Client Authentication

                // Usage of client authentication
                if (UseClientAuthentication)
                {
                    // Add client certificate to authenticate to service

                    // **** IMPORTANT ****
                    //- the certificate value [Certificate Value used for OIB installation] must be set to that one selected during OIB installation
                    //- uncomment to use client authentication, but consider these values overrides app.config settings
                    //proxy.ChannelFactory.Credentials.ClientCertificate.SetCertificate(
                    //  StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySubjectName, "[Certificate Value used for OIB installation]");
                }

                #endregion

                www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription serviceDescription =
                    new www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription();
                serviceDescription.Description = productDesc;
                serviceDescription.Product = productName;
                serviceDescription.ServiceName = serviceName;
                serviceDescription.ServiceVersion = "V1.0";
                serviceDescription.MetadataEndpoints = new Uri[1];
                serviceDescription.MetadataEndpoints[0] = uri;

                proxy.RegisterService(serviceDescription);

                proxy.Close();
                proxy = null;
            }
            catch (CommunicationException communicationException)
            {
                Console.WriteLine("RegisterServiceAtOIB can not register service at host " + oibServiceLocatorUri + ", Exception=" +
                                  communicationException.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RegisterServiceAtOIB failed with " + ex.Message);
                return false;
            }
            finally
            {
                if (proxy != null)
                {
                    proxy.Abort();
                }
            }
            return true;
        }

        private static void AdjustBindingParameters(WSHttpBinding binding, bool useSecurity)
        {
            if (binding != null)
            {
                binding.MaxBufferPoolSize = 524288;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.OpenTimeout = new TimeSpan(0, 0, 0, 10); // 10 seconds
                binding.CloseTimeout = new TimeSpan(0, 1, 0, 0); // 10 seconds
                binding.SendTimeout = new TimeSpan(0, 0, 1, 0); // 1 Minute
                binding.ReaderQuotas.MaxArrayLength = 2147483647;
                binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                binding.ReaderQuotas.MaxDepth = 2147483647;
                binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;

                // Use message security
                binding.Security.Mode = useSecurity ? SecurityMode.Message : SecurityMode.None;

                // Usage of client authentication binding properties
                binding.Security.Message.ClientCredentialType = UseClientAuthentication ? MessageCredentialType.Certificate : MessageCredentialType.None;
            }
        }
        #endregion
    }
}
