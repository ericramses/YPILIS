using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service.Order
{    
    public class ClientOrderService : YellowstonePathology.YpiConnect.Contract.IClientOrderService
    {        
        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByClientOrderId(string clientOrderId)
        {
            return YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrderByClientOrderId(clientOrderId);
        }

        public YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetRecentOrderBrowserListItemsByClientId(int clientId)
        {
            return YellowstonePathology.Business.Gateway.ClientOrderGateway.GetRecentOrderBrowserListItemsByClientId(clientId);
        }

        public YellowstonePathology.Business.Client.Model.PhysicianCollection GetPhysiciansByClientId(int clientId)
        {
            return YellowstonePathology.Business.Gateway.ClientOrderGateway.GetPhysiciansByClientId(clientId);
        }                        
        
		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByContainerId(string containerId)
		{
            return YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrderByContainerId(containerId);
		}               

		public YellowstonePathology.Business.ClientOrder.Model.ShipmentListItemCollection GetShipmentListItemCollection(int clientId)
		{
            return YellowstonePathology.Business.Gateway.ClientOrderGateway.GetShipmentListItemCollection(clientId);
		}

		public YellowstonePathology.Business.ClientOrder.Model.ShipmentReturnResult GetShipment(string shipmentId)
		{
            return YellowstonePathology.Business.Gateway.ClientOrderGateway.GetShipment(shipmentId);
		}

		public System.Xml.Linq.XElement PackingSlipReport(string shipmentId)
		{
            YellowstonePathology.Business.Gateway.ClientOrderGateway gateway = new YellowstonePathology.Business.Gateway.ClientOrderGateway();
			return gateway.PackingSlipReport(shipmentId);
		}		

		public YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection GetAllOrderTypes()
		{
            YellowstonePathology.Business.Gateway.ClientOrderGateway gateway = new YellowstonePathology.Business.Gateway.ClientOrderGateway();
			return gateway.GetAllOrderTypes();
		}

		public YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetAllOrderCategories()
		{
            YellowstonePathology.Business.Gateway.ClientOrderGateway gateway = new YellowstonePathology.Business.Gateway.ClientOrderGateway();
			return gateway.GetAllOrderCategories();
		}

		public YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetOrderCategory(string orderCategoryId)
		{
            YellowstonePathology.Business.Gateway.ClientOrderGateway gateway = new YellowstonePathology.Business.Gateway.ClientOrderGateway();
			return gateway.GetOrderCategory(orderCategoryId);
		}						
	        
		public YellowstonePathology.Business.ClientOrder.Model.ContainerIdLookupResponse DoesContainerIdExist(string containerId, string clientOrderDetailId)
		{
            return YellowstonePathology.Business.Gateway.ClientOrderGateway.DoesContainerIdExist(containerId, clientOrderDetailId);
		}
	}
}
