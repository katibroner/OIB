//-----------------------------------------------------------------------
// <copyright file="Form1.cs" company="ASM Assembly Systems GmbH & Co. KG">
//     Copyright (c) ASM Assembly Systems GmbH & Co. KG. All rights reserved.
// </copyright>
// <summary>
//    This code is part of the OIB SDK. 
//    Use and redistribution is free without any warranty. 
// </summary>
//-----------------------------------------------------------------------

#region Namespace

using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.ServiceModel;
using OIB.ServiceTutorial.Contract;
using www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Service;
using www.siplace.com.OIB._2008._05.ConfigurationManager.Contracts.Data;
using System.ServiceModel.Description;
using ServiceLocatorData = www.siplace.com.OIB._2008._05.ServiceLocator.Contracts.Data;

#endregion

namespace OIB_Client
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Simply create and proxy from the know endpoint address an call its SayHello() method.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonDirectCall_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			try
			{
				WSHttpBinding binding = new WSHttpBinding();
			    AdjustBindingParameters(binding);

                EndpointAddress address = new EndpointAddress(_TextBoxDirectEndpointAddress.Text);
				IHelloWorldService helloWorldProxy = ChannelFactory<IHelloWorldService>.CreateChannel(binding, address);
                try
                {
                    MessageBox.Show("SUCCESS: Service returned: " + helloWorldProxy.SayHello(), "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                finally
                {
                    CloseChannel(helloWorldProxy);   
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error has occurred closing the Web service: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			Cursor = Cursors.Default;
		}

		/// <summary>
		/// 1. Connect to the service locator and query all registered 
		///    "Hello World Service" services
		/// 2. Find the MEX address for the service assigned to the factrory layout element 'Enterprise'
		/// 3. Get the http enpoint from the MEX enpoint of the service
		/// 4. Finally create the proxy and call SayHello()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonCallViaServiceLocator_Click(object sender, EventArgs e)
		{
			#region DOC_CALL_SERVICE

			Cursor = Cursors.WaitCursor;
			try
			{
				//
				// 1. Connect to the service locator and query all registered 
				//    "Hello World Service" services
				//
				ServiceLocatorData.ServiceDescription[] services;
				Uri mexUri = null;
				IServiceLocator serviceLocatorProxy = GetServiceLocatorProxy();
                try
                {
                    ServiceLocatorData.ServiceMatchCriteria serviceMatchCriteria = 
                        new ServiceLocatorData.ServiceMatchCriteria();
                    serviceMatchCriteria.ServiceName = "Hello World Service";
                    services = serviceLocatorProxy.FindServices(serviceMatchCriteria);
                }
                finally
                {
                    CloseChannel(serviceLocatorProxy);                    
                }

				if (services == null || services.Length == 0)
				{
					MessageBox.Show("There are no services with name 'Hello World Service' registered", 
						"OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				//
				// 2. Find the service assigned to the factory layout element "Enterprise"
				//
				foreach (ServiceLocatorData.ServiceDescription service in services)
				{
					foreach (Isa95Base factoryLayoutNode in service.Configurations)
					{
						if (factoryLayoutNode.Name == "Enterprise")
						{
							mexUri = service.MetadataEndpoints[0];
						}
					}
				}

				if (mexUri == null)
				{
					MessageBox.Show(@"Servces of name 'Hello World Service' are registered but no one was not assigned the factory layout element 'Enterprise'", 
						"OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				//
				// 3. Get the http enpoint from the MEX enpoint of the service.
				//
				WSHttpBinding mexBinding = new WSHttpBinding();
			    AdjustBindingParameters(mexBinding);

				// Get all endpoints from the web service with the given mex-address
				MetadataExchangeClient client = new MetadataExchangeClient(mexBinding);

				MetadataSet mds = client.GetMetadata(mexUri, MetadataExchangeClientMode.MetadataExchange);
				MetadataImporter mdi = new WsdlImporter(mds);

				EndpointAddress address = null;
                WSHttpBinding binding = null;
				// Select the endpoint with the required protocol
				foreach (ServiceEndpoint endpoint in mdi.ImportAllEndpoints())
				{
					if (endpoint.Address.Uri.Scheme.ToLower() == "http")
					{
						address = endpoint.Address;
						binding = endpoint.Binding as WSHttpBinding;
                        if (binding != null)
						    break;
					}
				}

				if (address == null)
				{
					MessageBox.Show("No http endpoint supported", "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			    AdjustBindingParameters(binding);

                //
                // 4. Finally create the proxy and call SayHello()
                //
                IHelloWorldService helloWorldServiceProxy = ChannelFactory<IHelloWorldService>.CreateChannel(binding, address);
                try
                {
                    MessageBox.Show("SUCCESS: Service returned: " + helloWorldServiceProxy .SayHello(), "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    CloseChannel(helloWorldServiceProxy );                    
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error has occurred registering the service: " + ex.Message, "OIB Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			Cursor = Cursors.Default;

			#endregion
		}

        private void CloseChannel(object toClose)
        {
            ICommunicationObject channel = toClose as ICommunicationObject;
            
            if (channel == null)
                return;

            if (channel.State != CommunicationState.Faulted)
            {
                try
                {
                    channel.Close();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    try
                    {
                        channel.Abort();
                    }
                    catch
                    {
                        //
                    }
                }
            }
            else
            {
                try
                {
                    channel.Abort();
                }
                catch
                {
                    //
                }
            }
        }

	    private void AdjustBindingParameters(WSHttpBinding bind)
	    {
	        if (bind != null)
	        {
	            bind.AllowCookies = false;
	            bind.BypassProxyOnLocal = true;
	            bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
	            bind.OpenTimeout = new TimeSpan(0, 0, 10);
	            bind.CloseTimeout = new TimeSpan(0, 0, 10);
	            bind.SendTimeout = new TimeSpan(0, 1, 00);
	            bind.MaxBufferPoolSize = 524288;
	            bind.MaxReceivedMessageSize = 2147483647;
	            bind.ReaderQuotas.MaxArrayLength = 2147483647;
	            bind.ReaderQuotas.MaxBytesPerRead = 2147483647;
	            bind.ReaderQuotas.MaxDepth = 2147483647;
	            bind.ReaderQuotas.MaxNameTableCharCount = 2147483647;
	            bind.ReaderQuotas.MaxStringContentLength = 2147483647;
	            bind.ReliableSession.Enabled = false;
	            bind.TransactionFlow = false;
	            bind.UseDefaultWebProxy = false;
	            bind.Security.Mode = SecurityMode.None;
	        }
	    }

        /// <summary>
        /// Creates a poxy to access the service locator.
        /// </summary>
        /// <returns></returns>
        private IServiceLocator GetServiceLocatorProxy()
		{
			WSHttpBinding httpBinding = new WSHttpBinding();
		    AdjustBindingParameters(httpBinding);
            httpBinding.ReliableSession.Enabled = true;
			EndpointAddress address = new EndpointAddress(_TextBoxServiceLocatorEndpoint.Text);
			return ChannelFactory<IServiceLocator>.CreateChannel(httpBinding, address);
		}
	}
}