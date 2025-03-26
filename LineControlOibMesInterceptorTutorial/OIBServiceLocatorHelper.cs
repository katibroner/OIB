#region Copyright

// OIB - Copyright (C) ASM Assembly Systems 2016
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM and are supplied subject to license terms.

#endregion

#region using

using System;
using System.ServiceModel;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;

#endregion

namespace LineControlOibMesInterceptorTutorial
{
    public static class OIBServiceLocatorHelper
    {
        #region DOC_RegisterAtOIB
        /// <summary>
        /// Registering a service at OIB
        /// </summary>
        /// <param name="serviceName">Name of the service to be registered</param>
        /// <param name="productName">Name of the calling software product</param>
        /// <param name="productDesc">A description of the product</param>
        /// <param name="uri">URI of the service</param>
        /// <param name="coreComputerName">Name of the OIB core computer.</param>
        /// <returns>
        /// true: success
        /// </returns>
        public static bool RegisterServiceAtOIB(string serviceName, string productName, string productDesc, Uri uri, string coreComputerName)
        {
            ServiceLocatorClient proxy = null;
            try
            {
                proxy = GetProxy(coreComputerName);

                www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription serviceDescription =
                    new www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription();
                serviceDescription.Description = productDesc;
                serviceDescription.Product = productName;
                serviceDescription.ServiceName = serviceName;
                serviceDescription.ServiceVersion = "V1.0";
                serviceDescription.MetadataEndpoints = new Uri[1];
                serviceDescription.MetadataEndpoints[0] = uri;

                www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription registerdServiceDescription =
                    proxy.RegisterService(serviceDescription);

                proxy.Close();
            }
            catch(CommunicationException communicationException)
            {
                if (proxy != null)
                {
                    MesSimForm.AddTrace("RegisterServiceAtOIB can not register service at host " + proxy.Endpoint.Address + ", Exception=" +
                                      communicationException.Message);
                }

                return false;
            }
            catch(Exception ex)
            {
                MesSimForm.AddTrace("RegisterServiceAtOIB failed with " + ex.Message);
                return false;
            }
            return true;
        }
        #endregion
        /// <summary>
        /// Unregistering a service at OIB
        /// </summary>
        /// <param name="serviceName">Name of the service to be registered</param>
        /// <param name="productName">Name of the calling software product</param>
        /// <param name="productDesc">A description of the product</param>
        /// <param name="uri">URI of the service</param>
        /// <param name="coreComputerName">Name of the OIB core computer.</param>
        /// <returns>true: success</returns>
        public static bool UnregisterServiceAtOIB(string serviceName, string productName, string productDesc, Uri uri, string coreComputerName)
        {
            ServiceLocatorClient proxy = null;
            try
            {
                proxy = GetProxy(coreComputerName);

                www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription serviceDescription =
                    new www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription();
                serviceDescription.Description = productDesc;
                serviceDescription.Product = productName;
                serviceDescription.ServiceName = serviceName;
                serviceDescription.ServiceVersion = "V1.0";
                serviceDescription.MetadataEndpoints = new Uri[1];
                serviceDescription.MetadataEndpoints[0] = uri;

                proxy.UnregisterService(serviceDescription);

                proxy.Close();
            }
            catch (CommunicationException communicationException)
            {
                if(proxy != null)
                {
                    MesSimForm.AddTrace("UnregisterServiceAtOIB can not register service at host " + proxy.Endpoint.Address + ", Exception=" +
                                      communicationException.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                MesSimForm.AddTrace("UnregisterServiceAtOIB failed with " + ex.Message);
                return false;
            }
            return true;
        }

        private static ServiceLocatorClient GetProxy(string coreComputerName)
        {
            WSHttpBinding bind = new WSHttpBinding(SecurityMode.None)
            {
                AllowCookies = false,
                BypassProxyOnLocal = true,
                UseDefaultWebProxy = false,
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                MaxBufferPoolSize = 524288,
                MaxReceivedMessageSize = 2147483647,
                ReceiveTimeout = TimeSpan.MaxValue, // Max
                OpenTimeout = new TimeSpan(0, 1, 0, 0), // 1 Hour
                CloseTimeout = new TimeSpan(0, 1, 0, 0), // 1 Hour
                SendTimeout = new TimeSpan(0, 1, 0, 0), // 1 Hour
                ReaderQuotas =
                    {
                        MaxArrayLength = 2147483647,
                        MaxBytesPerRead = 2147483647,
                        MaxDepth = 2147483647,
                        MaxNameTableCharCount = 2147483647,
                        MaxStringContentLength = 2147483647
                    },
            };
            UriBuilder builder = new UriBuilder("http://localhost:1405/Asm.As.oib.servicelocator");
            builder.Host = coreComputerName;
            return new ServiceLocatorClient(bind, new EndpointAddress(builder.Uri));
        }

    }
}