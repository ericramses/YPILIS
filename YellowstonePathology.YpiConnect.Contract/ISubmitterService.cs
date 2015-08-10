using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;

namespace YellowstonePathology.YpiConnect.Contract
{
	[ServiceContract]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrder))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.Shipment))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail))]
    [ServiceKnownType(typeof(YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail))]    
	public interface ISubmitterService
    {				
        [OperationContract]
		void Submit(YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent);

        [OperationContract]
        void InsertBaseClassOnly(object subclassObject);        
	}
}
