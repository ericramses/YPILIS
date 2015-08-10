using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.YpiConnect.Service.Search
{
    public class ClientSearchGateway
    {
		public YellowstonePathology.Business.Client.Model.Client GetClient(int clientId)
		{
			SearchGateway gateway = new SearchGateway();
			return gateway.GetClient(clientId);			
		}

		public void AcknowledgeDistributions(string reportDistributionLogIdStringList)
		{
			SearchGateway gateway = new SearchGateway();
			gateway.AcknowledgeDistributions(reportDistributionLogIdStringList);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPhysicianId(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SearchGateway gateway = new SearchGateway();
			return gateway.GetClientCasesByPhysicianId(search);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPSSN(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SearchGateway gateway = new SearchGateway();
			return gateway.GetClientCasesByPSSN(search);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPBirthDate(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SearchGateway gateway = new SearchGateway();
			return gateway.GetClientCasesByPBirthDate(search);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPatientLastName(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SearchGateway gateway = new SearchGateway();
            return gateway.GetClientCasesByPatientLastName(search);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPatientLastNameAndFirstName(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SearchGateway gateway = new SearchGateway();
			return gateway.GetClientCasesByPatientLastNameAndFirstName(search);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientRecentCases(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SearchGateway gateway = new SearchGateway();
			return gateway.GetClientRecentCases(search);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesNotAcknowledged(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SearchGateway gateway = new SearchGateway();
			return gateway.GetClientCasesNotAcknowledged(search);
		}
	}
}
