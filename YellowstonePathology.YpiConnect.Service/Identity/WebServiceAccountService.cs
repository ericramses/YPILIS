using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service.Identity
{
    public class WebServiceAccountService : YellowstonePathology.YpiConnect.Contract.Identity.IWebServiceAccountService
    {
        public bool Ping()
        {            
            return true;
        }

        public YellowstonePathology.Business.Client.Model.ClientLocation GetClientLocation(int clientLocationId)
        {
            YellowstonePathology.YpiConnect.Service.Identity.WebServiceAccountGateway gateway = new WebServiceAccountGateway();
            return gateway.GetClientLocation(clientLocationId);
        }

        public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount GetWebServiceAccount(string userName, string password)
        {
            YellowstonePathology.YpiConnect.Service.Log.ClientServicesLogService clientServicesLog = new Log.ClientServicesLogService();
            clientServicesLog.LogEvent(1055, userName);

            YellowstonePathology.YpiConnect.Service.Identity.WebServiceAccountGateway webServiceAccountGateway = new WebServiceAccountGateway();            
            YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount = webServiceAccountGateway.GetAccount(userName, password);

            return webServiceAccount;
        }       

		public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountCollection GetWebServiceAccountCollectionByFacilityId(string facilityId)
		{
			YellowstonePathology.YpiConnect.Service.Identity.WebServiceAccountGateway webServiceAccountGateway = new WebServiceAccountGateway();
			YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountCollection webServiceAccountCollection = webServiceAccountGateway.GetWebServiceAccountCollectionByFacilityId(facilityId);

			return webServiceAccountCollection;
		}

		public YellowstonePathology.Business.Client.Model.ClientCollection GetClientCollectionForContextSelection(string userName)
        {
            YellowstonePathology.YpiConnect.Service.Identity.WebServiceAccountGateway webServiceAccountGateway = new WebServiceAccountGateway();
            return webServiceAccountGateway.GetClientCollectionForContextSelection(userName);           
        }
    }
}
