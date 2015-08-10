using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{
    [ServiceContract]    
    public interface IWebServiceAccountService
    {
        [OperationContract]
        bool Ping();

        [OperationContract]
        WebServiceAccount GetWebServiceAccount(string userName, string password);

		[OperationContract]
		WebServiceAccountCollection GetWebServiceAccountCollectionByFacilityId(string facilityId);

        [OperationContract]
		YellowstonePathology.Business.Client.Model.ClientCollection GetClientCollectionForContextSelection(string userName);

        [OperationContract]
        YellowstonePathology.Business.Client.Model.ClientLocation GetClientLocation(int clientLocationId);
	}
}
