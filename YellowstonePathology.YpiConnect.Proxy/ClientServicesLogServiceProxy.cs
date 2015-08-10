using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Proxy
{
    public class ClientServicesLogServiceProxy 
    {        
        //public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/WebService/Version/3.1.0.0/ClientServicesLogService.svc";
        public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/Testing/Services/ClientServicesLogService.svc";

        private BasicHttpBinding m_BasicHttpBinding;
        private EndpointAddress m_EndpointAddress;
		private YellowstonePathology.YpiConnect.Contract.Log.IClientServicesLogService m_ClientLogServiceChannel;
        private ChannelFactory<YellowstonePathology.YpiConnect.Contract.Log.IClientServicesLogService> m_ChannelFactory;


        public ClientServicesLogServiceProxy()
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

            this.m_ChannelFactory = new ChannelFactory<Contract.Log.IClientServicesLogService>(this.m_BasicHttpBinding, this.m_EndpointAddress);
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

			this.m_ClientLogServiceChannel = this.m_ChannelFactory.CreateChannel();            
        }

        public string GetCallingUser()
        {
			return this.m_ClientLogServiceChannel.GetCallingUser();
        }

        public void LogEvent(int eventId, string details)
        {
			this.m_ClientLogServiceChannel.LogEvent(eventId, details);
        }
    }
}
