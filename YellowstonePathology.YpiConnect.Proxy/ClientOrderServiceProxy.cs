using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Proxy
{
    public class ClientOrderServiceProxy
    {        
        //public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/WebService/Version/3.1.0.0/ClientOrderService.svc";
        public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/Testing/Services/ClientOrderService.svc";

        BasicHttpBinding m_BasicHttpBinding;
        EndpointAddress m_EndpointAddress;   

        YellowstonePathology.YpiConnect.Contract.IClientOrderService m_ClientOrderChannel;
        ChannelFactory<YellowstonePathology.YpiConnect.Contract.IClientOrderService> m_ChannelFactory;        

        public ClientOrderServiceProxy()
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

            this.m_ChannelFactory = new ChannelFactory<Contract.IClientOrderService>(this.m_BasicHttpBinding, this.m_EndpointAddress);
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

            this.m_ClientOrderChannel = this.m_ChannelFactory.CreateChannel();            
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByClientOrderId(string clientOrderId)
        {
            return this.m_ClientOrderChannel.GetClientOrderByClientOrderId(clientOrderId);
        }

        public YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetRecentOrderBrowserListItemsByClientId(int clientId)
        {
            return this.m_ClientOrderChannel.GetRecentOrderBrowserListItemsByClientId(clientId);
        }

        public YellowstonePathology.Business.Client.Model.PhysicianCollection GetPhysiciansByClientId(int clientId)
        {
            YellowstonePathology.Business.Client.Model.PhysicianCollection physicianCollection = this.m_ClientOrderChannel.GetPhysiciansByClientId(clientId);            
            return physicianCollection;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByContainerId(string containerId)
		{
            return this.m_ClientOrderChannel.GetClientOrderByContainerId(containerId);
		}

		public YellowstonePathology.Business.ClientOrder.Model.ShipmentListItemCollection GetShipmentListItemCollection(int clientId)
		{			
			return this.m_ClientOrderChannel.GetShipmentListItemCollection(clientId);
		}

		public YellowstonePathology.Business.ClientOrder.Model.ShipmentReturnResult GetShipment(string shipmentId)
		{
			return this.m_ClientOrderChannel.GetShipment(shipmentId);
		}

		public System.Xml.Linq.XElement PackingSlipReport(string shipmentId)
		{
			return this.m_ClientOrderChannel.PackingSlipReport(shipmentId);
		}		

        public YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection GetAllOrderTypes()
		{
            YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection orderTypeCollection = this.m_ClientOrderChannel.GetAllOrderTypes();			
			return orderTypeCollection;
		}

        public YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetAllOrderCategories()
		{
            YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection orderCategoryCollection = this.m_ClientOrderChannel.GetAllOrderCategories();			
			return orderCategoryCollection;
		}

        public YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetOrderCategory(string orderCategoryId)
		{
            YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection orderCategoryCollection = this.m_ClientOrderChannel.GetOrderCategory(orderCategoryId);			
			return orderCategoryCollection;
		}                

		public YellowstonePathology.Business.ClientOrder.Model.ContainerIdLookupResponse DoesContainerIdExist(string containerId, string clientOrderDetailId)
		{
			return this.m_ClientOrderChannel.DoesContainerIdExist(containerId, clientOrderDetailId);
		}
	}
}
