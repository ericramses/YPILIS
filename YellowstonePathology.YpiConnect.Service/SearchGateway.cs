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

namespace YellowstonePathology.YpiConnect.Service
{
	public class SearchGateway
	{
		public YellowstonePathology.Business.Client.Model.Client GetClient(int clientId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT c.*, (SELECT * from tblClientLocation where ClientId = c.ClientId for xml path('ClientLocation'), type) ClientLocationCollection " +
				" FROM tblClient c where c.ClientId = @ClientId for xml Path('Client'), type";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
			Search.ClientBuilder builder = new Search.ClientBuilder();
			XElement resultElement = YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteXmlReaderCommand(cmd, YellowstonePathology.Business.Domain.Persistence.DataLocationEnum.ProductionData);
			builder.Build(resultElement);
			return builder.Client;
		}

		public void AcknowledgeDistributions(string reportDistributionLogIdStringList)
		{
			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
				cmd.CommandText = "dbo.ws_AcknowledgeDistributions";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@ReportDistributionLogIdStringList", SqlDbType.VarChar).Value = reportDistributionLogIdStringList;
				cmd.ExecuteNonQuery();
			}
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPhysicianId(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetClientCasesByPhysicianId";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPSSN(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetClientCasesByPSSN";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = search.WebServiceAccount.WebServiceAccountClientCollection.ToIdString();
			cmd.Parameters.Add("@PSSN", SqlDbType.VarChar).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPBirthDate(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetClientCasesByPBirthDate";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = search.WebServiceAccount.WebServiceAccountClientCollection.ToIdString();
			cmd.Parameters.Add("@PBirthdate", SqlDbType.DateTime).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPatientLastName(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetClientCasesByPatientLastName";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = search.WebServiceAccount.WebServiceAccountClientCollection.ToIdString();
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesByPatientLastNameAndFirstName(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetClientCasesByPatientLastNameAndFirstName";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = search.WebServiceAccount.WebServiceAccountClientCollection.ToIdString();
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = search.SearchParameters[0];
			cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = search.SearchParameters[1];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientRecentCases(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetClientRecentCases";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = search.WebServiceAccount.WebServiceAccountClientCollection.ToIdString();
			cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetClientCasesNotAcknowledged(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetClientCasesNotAcknowledged";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = search.WebServiceAccount.WebServiceAccountClientCollection.ToIdString();
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetPathologistCasesByPatientLastName(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetPathologistCasesByPatientLastName";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PathologistId", SqlDbType.Int).Value = search.SearchParameters[1];
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetPathologistCasesByPatientLastNameAndFirstName(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetPathologistCasesByPatientLastNameAndFirstName";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PathologistId", SqlDbType.Int).Value = search.SearchParameters[2];
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = search.SearchParameters[0];
			cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = search.SearchParameters[1];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetPathologistRecentCases(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetPathologistRecentCases";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PathologistId", SqlDbType.Int).Value = search.SearchParameters[1];
			cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetPathologistCasesByPBirthDate(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetPathologistCasesByPBirthDate";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PathologistId", SqlDbType.Int).Value = search.SearchParameters[1];
			cmd.Parameters.Add("@PBirthdate", SqlDbType.DateTime).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetPathologistCasesByPSSN(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetPathologistCasesByPSSN";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PathologistId", SqlDbType.Int).Value = search.SearchParameters[1];
			cmd.Parameters.Add("@PSSN", SqlDbType.VarChar).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection GetRecentProfessionalCasesByFacilityId(YellowstonePathology.YpiConnect.Contract.Search.Search search)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetRecentProfessionalCasesByFacilityId";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@FacilityId", SqlDbType.VarChar).Value = search.SearchParameters[1];
			cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = search.SearchParameters[0];
			return this.BuildSearchResultCollection(cmd);
		}

		private YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection BuildSearchResultCollection(SqlCommand cmd)
		{
			YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection searchResultCollection = new Contract.Search.SearchResultCollection();
			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult = new Contract.Search.SearchResult();
						searchResult.WriteProperties(propertyWriter);
						searchResultCollection.Add(searchResult);
					}
				}
			}
			return searchResultCollection;
		}
	}
}
