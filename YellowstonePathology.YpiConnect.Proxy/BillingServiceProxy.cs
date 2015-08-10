using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Proxy
{
    public class BillingServiceProxy
    {        
        //public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/WebService/Version/3.1.0.0/BillingService.svc";
        public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/Testing/Services/BillingService.svc";

        BasicHttpBinding m_BasicHttpBinding;
        EndpointAddress m_EndpointAddress;   

        YellowstonePathology.YpiConnect.Contract.Billing.IBillingService m_BillingChannel;
        ChannelFactory<YellowstonePathology.YpiConnect.Contract.Billing.IBillingService> m_ChannelFactory;        

        public BillingServiceProxy()
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

            this.m_ChannelFactory = new ChannelFactory<Contract.Billing.IBillingService>(this.m_BasicHttpBinding, this.m_EndpointAddress);
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

            this.m_BillingChannel = this.m_ChannelFactory.CreateChannel();            
        }

        public bool Ping()
        {
            return this.m_BillingChannel.Ping();
        }

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByPostDate(DateTime postDate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = this.m_BillingChannel.GetBillingAccessionsByPostDate(postDate, webServiceAccount);            
            return billingAccessionCollection;
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastName(string lastName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = this.m_BillingChannel.GetBillingAccessionsByLastName(lastName, webServiceAccount);
			return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastNameAndFirstName(string lastName, string firstName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = this.m_BillingChannel.GetBillingAccessionsByLastNameAndFirstName(lastName, firstName, webServiceAccount);
			return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByReportNo(string reportNo, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = this.m_BillingChannel.GetBillingAccessionsByReportNo(reportNo, webServiceAccount);
			return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByBirthdate(DateTime birthdate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = this.m_BillingChannel.GetBillingAccessionsByBirthdate(birthdate, webServiceAccount);
			return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsBySsn(string ssn, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = this.m_BillingChannel.GetBillingAccessionsBySsn(ssn, webServiceAccount);
			return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail GetBillingDetail(string reportNo, bool includeMemoryStream, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail billingDetail = this.m_BillingChannel.GetBillingDetail(reportNo, includeMemoryStream, webServiceAccount);
			return billingDetail;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetRecentBillingAccessions(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = this.m_BillingChannel.GetRecentBillingAccessions(webServiceAccount);
			return billingAccessionCollection;
		}
	}
}
