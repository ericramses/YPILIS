using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
    [ServiceContract]    
    public interface IBillingService
    {
        [OperationContract]
        bool Ping();

        [OperationContract]
        YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetRecentBillingAccessions(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount);

        [OperationContract]
        YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByPostDate(DateTime postDate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount);

		[OperationContract]
		YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastName(string lastName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount);

		[OperationContract]
		YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastNameAndFirstName(string lastName, string firstName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount);

		[OperationContract]
		YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByReportNo(string reportNo, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount);

		[OperationContract]
		YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByBirthdate(DateTime birthdate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount);

		[OperationContract]
		YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsBySsn(string ssn, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount);

		[OperationContract]
		YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail GetBillingDetail(string reportNo, bool includeMemoryStream, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount);
	}
}
