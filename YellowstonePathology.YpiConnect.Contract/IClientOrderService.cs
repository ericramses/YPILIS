using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace YellowstonePathology.YpiConnect.Contract
{    
    [ServiceContract]    
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrder))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.Shipment))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection))]    
    public interface IClientOrderService
    {        
        [OperationContract]
        YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByClientOrderId(string clientOrderId);

        [OperationContract]
        YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection GetRecentOrderBrowserListItemsByClientId(int clientId);        

        [OperationContract]
        YellowstonePathology.Business.Client.Model.PhysicianCollection GetPhysiciansByClientId(int clientId);

		[OperationContract]
		YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByContainerId(string containerId);

		[OperationContract]
		YellowstonePathology.Business.ClientOrder.Model.ShipmentListItemCollection GetShipmentListItemCollection(int clientId);

		[OperationContract]
	    YellowstonePathology.Business.ClientOrder.Model.ShipmentReturnResult GetShipment(string shipmentId);		

		[OperationContract]
		System.Xml.Linq.XElement PackingSlipReport(string shipmentId);		

		[OperationContract]
		YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection GetAllOrderTypes();

		[OperationContract]
		YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetAllOrderCategories();

		[OperationContract]
		YellowstonePathology.Business.ClientOrder.Model.OrderCategoryCollection GetOrderCategory(string orderCategoryId);

		[OperationContract]
		YellowstonePathology.Business.ClientOrder.Model.ContainerIdLookupResponse DoesContainerIdExist(string containerId, string clientOrderDetailId);
	}
}
