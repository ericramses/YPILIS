using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Proxy
{
    public class WebServiceAccountServiceProxy 
    {        
        //public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/WebService/Version/3.1.0.0/WebServiceAccountService.svc";
        public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/Testing/Services/WebServiceAccountService.svc";        
        
        private BasicHttpBinding m_BasicHttpBinding;
        private EndpointAddress m_EndpointAddress;
        private YellowstonePathology.YpiConnect.Contract.Identity.IWebServiceAccountService m_WebAccountServiceChannel;
        private ChannelFactory<YellowstonePathology.YpiConnect.Contract.Identity.IWebServiceAccountService> m_ChannelFactory;        

        public WebServiceAccountServiceProxy()
        {            
            this.m_BasicHttpBinding = new BasicHttpBinding();            
            this.m_EndpointAddress = new EndpointAddress(EndpointAddressUrl);
            
            this.m_BasicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
            this.m_BasicHttpBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            this.m_BasicHttpBinding.MaxReceivedMessageSize = 2147483647;

            XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
            readerQuotas.MaxArrayLength = 25 * 208000;
            readerQuotas.MaxStringContentLength = 25 * 208000;
            this.m_BasicHttpBinding.ReaderQuotas = readerQuotas;            
            
            this.m_ChannelFactory = new ChannelFactory<Contract.Identity.IWebServiceAccountService>(this.m_BasicHttpBinding, this.m_EndpointAddress);
            this.m_ChannelFactory.Credentials.UserName.UserName = YellowstonePathology.YpiConnect.Contract.Identity.GuestWebServiceAccount.UserName;
            this.m_ChannelFactory.Credentials.UserName.Password = YellowstonePathology.YpiConnect.Contract.Identity.GuestWebServiceAccount.Password;

            foreach (System.ServiceModel.Description.OperationDescription op in this.m_ChannelFactory.Endpoint.Contract.Operations)
            {
                var dataContractBehavior = op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>();
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }

            this.m_WebAccountServiceChannel = this.m_ChannelFactory.CreateChannel();            
        }                

        public bool Ping()
        {            
            try
            {                
                return this.m_WebAccountServiceChannel.Ping();
            }
            catch (Exception)
            {                
                return false;
            }         
        }

        public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount GetWebServiceAccount(string userName, string password)
        {            
            return this.m_WebAccountServiceChannel.GetWebServiceAccount(userName, password);
        }

		public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountCollection GetWebServiceAccountCollectionByFacilityId(string facilityId)
		{
			return this.m_WebAccountServiceChannel.GetWebServiceAccountCollectionByFacilityId(facilityId);
		}

		public YellowstonePathology.Business.Client.Model.ClientCollection GetClientCollectionForContextSelection(string userName)
        {
            return this.m_WebAccountServiceChannel.GetClientCollectionForContextSelection(userName);
        }        
	}
}
