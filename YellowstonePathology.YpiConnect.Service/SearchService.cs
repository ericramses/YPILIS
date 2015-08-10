using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YellowstonePathology.YpiConnect.Service
{
	public class SearchService : YellowstonePathology.YpiConnect.Contract.Search.ISearchService
	{
		public bool Ping()
		{
			return true;
		}

		public void AcknowledgeDistributions(string reportDistributionLogIdStringList)
		{
			YellowstonePathology.YpiConnect.Service.Search.ClientSearchGateway gateway = new YellowstonePathology.YpiConnect.Service.Search.ClientSearchGateway();
			gateway.AcknowledgeDistributions(reportDistributionLogIdStringList);
		}

		public Contract.Search.SearchResultCollection ExecuteClientSearch(Contract.Search.Search search)
		{
			YellowstonePathology.YpiConnect.Service.SearchGateway gateway = new YellowstonePathology.YpiConnect.Service.SearchGateway();
			YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection searchResults = null;

			switch (search.SearchType)
			{
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.PatientLastNameSearch:
					searchResults = gateway.GetClientCasesByPatientLastName(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.PatientLastAndFirstNameSearch:
					searchResults = gateway.GetClientCasesByPatientLastNameAndFirstName(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.RecentCases:
					searchResults = gateway.GetClientRecentCases(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.NotDownloaded: //Not Downloaded is Depricated SH 5/17/2010
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.NotAcknowledged:
					searchResults = gateway.GetClientCasesNotAcknowledged(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.DateOfBirth:
					searchResults = gateway.GetClientCasesByPBirthDate(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.SocialSecurityNumber:
					searchResults = gateway.GetClientCasesByPSSN(search);
					break;
			}

			if (searchResults == null) searchResults = new Contract.Search.SearchResultCollection();
			return searchResults;
		}

		public Contract.Search.SearchResultCollection ExecutePathologistSearch(Contract.Search.Search search)
		{
			YellowstonePathology.YpiConnect.Service.SearchGateway gateway = new YellowstonePathology.YpiConnect.Service.SearchGateway();
			YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection searchResults = null;

			switch (search.SearchType)
			{
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.PatientLastNameSearch:
					searchResults = gateway.GetPathologistCasesByPatientLastName(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.PatientLastAndFirstNameSearch:
					searchResults = gateway.GetPathologistCasesByPatientLastNameAndFirstName(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.RecentCases:
					searchResults = gateway.GetPathologistRecentCases(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.DateOfBirth:
					searchResults = gateway.GetPathologistCasesByPBirthDate(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.SocialSecurityNumber:
					searchResults = gateway.GetPathologistCasesByPSSN(search);
					break;
				case YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.RecentCasesForFacilityId:
					searchResults = gateway.GetRecentProfessionalCasesByFacilityId(search);
					break;
			}

			if (searchResults == null) searchResults = new Contract.Search.SearchResultCollection();
			return searchResults;
		}
	}
}
