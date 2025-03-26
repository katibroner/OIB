//-----------------------------------------------------------------------
// <copyright file="OibSerivceLocatorHelper.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region using

using System;

using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using ServiceDescription = www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data.ServiceDescription;

#endregion

namespace OIB.Tutorial
{
    /// <summary>
    /// Service locator helper managing the service locator proxy using the http protocol. Use the pattern:
    /// 
    /// using (SerivceLocatorHelper serivceLocatorHelper = new SerivceLocatorHelper(serviceLocatorMexUri))
    /// {
    ///     ...
    /// } // end of using closes connection (or aborts it if they are faulted).
    /// </summary>
    public class OibServiceLocatorHelper : ServiceHelper<IServiceLocator>
    {
        #region Constructor

        public OibServiceLocatorHelper(Uri mexUri, bool useCLientAuthentication)
            : base(mexUri, useCLientAuthentication)
        {
        }

        #endregion

        /// <summary>
        /// Returns the MEX address for a service name. If the service is installed more than one time, the first service is returned.
        /// </summary>
        /// <param name="serviceName">Name of the service, e.g. "ConfigurationManager"</param>
        /// <returns>The mex address, e.g. http://localhost:1405/Asm.As.Oib.ConfigurationManager/mex or null, if the service is not known by OIB.</returns>
        public Uri GetMexUriForServiceName(string serviceName)
        {
            if (Disposed)
                throw new ObjectDisposedException(GetType().Name);


            ServiceMatchCriteria criteria = new ServiceMatchCriteria();
            criteria.ServiceName = serviceName;
            ServiceDescription[] serviceDescriptions = Proxy.FindServices(criteria);

            if (serviceDescriptions.Length == 0 || serviceDescriptions[0].MetadataEndpoints.Length == 0)
                return null;

            foreach (Uri mexUri in serviceDescriptions[0].MetadataEndpoints)
            {
                if (mexUri.Scheme == Uri.UriSchemeHttp)
                {
                    return mexUri;
                }
            }

            return serviceDescriptions[0].MetadataEndpoints[0];
        }

        public ServiceDescription[] GetAllServiceDescriptionFroServiceName(string serviceName)
        {
            if (Disposed)
                throw new ObjectDisposedException(GetType().Name);

            ServiceMatchCriteria criteria = new ServiceMatchCriteria();
            criteria.ServiceName = serviceName;
            return Proxy.FindServices(criteria);
        }

        /// <summary>
        /// Resets the service - factory layout element assignments.
        /// </summary>
        public void ResetConfiguration()
        {
            foreach (ServiceDescription serviceDescription in Proxy.GetAllServices())
            {
                if (serviceDescription.FactoryElementId != 0)
                {
                    // need to clear both properties (legacy Configuration as well as newer FactoryElementId)
                    // the change is valid from R18-1, OIB 5.3
                    serviceDescription.Configuration = null;
                    serviceDescription.FactoryElementId = 0;

                    // implement changes
                    Proxy.SaveServiceDescription(serviceDescription);
                }
            }
        }
    }
}