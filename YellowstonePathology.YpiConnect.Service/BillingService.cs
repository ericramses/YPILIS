using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service
{
    public class BillingService : YellowstonePathology.YpiConnect.Contract.Billing.IBillingService
    {
        public bool Ping()
        {
            return true;
        }

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetRecentBillingAccessions(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            BillingGateway gateway = new BillingGateway();
            YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = null;
            if (webServiceAccount.PrimaryClientId == 0)
            {
                billingAccessionCollection = gateway.GetRecentBillingAccessions(webServiceAccount);
            }
            else
            {
                billingAccessionCollection = gateway.GetRecentBillingAccessionsClient(webServiceAccount);
            }
            return billingAccessionCollection;
        }

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByPostDate(DateTime postDate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
			BillingGateway gateway = new BillingGateway();
            YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = null;
            if (webServiceAccount.PrimaryClientId == 0)
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByPostDate(postDate, webServiceAccount);
            }
            else
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByPostDateClient(postDate, webServiceAccount);
            }
            return billingAccessionCollection;
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastName(string lastName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			BillingGateway gateway = new BillingGateway();            
            YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = null;
            if (webServiceAccount.PrimaryClientId == 0)
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByLastName(lastName, webServiceAccount);
            }
            else
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByLastNameClient(lastName, webServiceAccount);
            }
            return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastNameAndFirstName(string lastName, string firstName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			BillingGateway gateway = new BillingGateway();
            YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = null;
            if (webServiceAccount.PrimaryClientId == 0)
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByLastNameAndFirstName(lastName, firstName, webServiceAccount);
            }
            else
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByLastNameAndFirstNameClient(lastName, firstName, webServiceAccount);
            }
            return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByReportNo(string reportNo, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			BillingGateway gateway = new BillingGateway();
            YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = null;
            if (webServiceAccount.PrimaryClientId == 0)
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByReportNo(reportNo, webServiceAccount);
            }
            else
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByReportNoClient(reportNo, webServiceAccount);
            }
            return billingAccessionCollection;
		}
		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByBirthdate(DateTime birthdate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			BillingGateway gateway = new BillingGateway();
            YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = null;
            if (webServiceAccount.PrimaryClientId == 0)
            {
			    billingAccessionCollection = gateway.GetBillingAccessionsByBirthdate(birthdate, webServiceAccount);
            }
            else
            {
                billingAccessionCollection = gateway.GetBillingAccessionsByBirthdateClient(birthdate, webServiceAccount);
            }
            return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsBySsn(string ssn, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			BillingGateway gateway = new BillingGateway();
            YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = null;
            if (webServiceAccount.PrimaryClientId == 0)
            {
                billingAccessionCollection = gateway.GetBillingAccessionsBySsn(ssn, webServiceAccount);
            }
            else
            {
                billingAccessionCollection = gateway.GetBillingAccessionsBySsnClient(ssn, webServiceAccount);
            }
            return billingAccessionCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail GetBillingDetail(string reportNo, bool getMemoryStream, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{        
			BillingGateway gateway = new BillingGateway();
            return gateway.GetBillingDetail(reportNo, getMemoryStream, webServiceAccount);            
		}
	}
}
