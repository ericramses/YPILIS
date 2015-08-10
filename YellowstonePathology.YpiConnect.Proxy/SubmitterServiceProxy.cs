using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Proxy
{
    public class SubmitterServiceProxy 
    {        
        //public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/WebService/Version/3.1.0.0/SubmitterService.svc";
        public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/Testing/Services/SubmitterService.svc";

        BasicHttpBinding m_BasicHttpBinding;
        EndpointAddress m_EndpointAddress;   
        YellowstonePathology.YpiConnect.Contract.ISubmitterService m_Channel;
        ChannelFactory<YellowstonePathology.YpiConnect.Contract.ISubmitterService> m_ChannelFactory;        

        public SubmitterServiceProxy()
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

            this.m_ChannelFactory = new ChannelFactory<Contract.ISubmitterService>(this.m_BasicHttpBinding, this.m_EndpointAddress);
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

            this.m_Channel = this.m_ChannelFactory.CreateChannel();            
        }

		public void Submit(YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent)
        {            
			this.m_Channel.Submit(remoteObjectTransferAgent);            
        }

        public void InsertBaseClassOnly(object subclassObject)
        {
            this.m_Channel.InsertBaseClassOnly(subclassObject);
        }
	}
}
