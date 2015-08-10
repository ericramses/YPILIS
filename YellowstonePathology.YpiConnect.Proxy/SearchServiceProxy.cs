using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Proxy
{
    public class SearchServiceProxy 
    {        
        //public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/WebService/Version/3.1.0.0/SearchService.svc";
        public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/Testing/Services/SearchService.svc";

        BasicHttpBinding m_BasicHttpBinding;
        EndpointAddress m_EndpointAddress;   
        YellowstonePathology.YpiConnect.Contract.Search.ISearchService m_Channel;
        ChannelFactory<YellowstonePathology.YpiConnect.Contract.Search.ISearchService> m_ChannelFactory;        

        public SearchServiceProxy()
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

            this.m_ChannelFactory = new ChannelFactory<Contract.Search.ISearchService>(this.m_BasicHttpBinding, this.m_EndpointAddress);
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

        public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection ExecuteClientSearch(YellowstonePathology.YpiConnect.Contract.Search.Search search)
        {
            return this.m_Channel.ExecuteClientSearch(search);
        }

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection ExecutePathologistSearch(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			return this.m_Channel.ExecutePathologistSearch(search);
		}

        public void AcknowledgeDistributions(string reportDistributionLogIdStringList)
        {
            this.m_Channel.AcknowledgeDistributions(reportDistributionLogIdStringList);
        }        

        public bool Ping()
        {
            return this.m_Channel.Ping();
        }        
    }
}
