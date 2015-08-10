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
	public class BillingGateway
	{
        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetRecentBillingAccessions(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetRecentBillingAccessions_2";
            cmd.CommandType = CommandType.StoredProcedure;
            BillingBuilder billingBuilder = new BillingBuilder(cmd);
            return billingBuilder.Build();
        }

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetRecentBillingAccessionsClient(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetRecentBillingAccessionsClient_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = webServiceAccount.WebServiceAccountClientCollection.ToIdString();
            BillingBuilder billingBuilder = new BillingBuilder(cmd);
            return billingBuilder.Build();
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByPostDate(DateTime postDate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetBillingAccessionsByPostDate_2";
			cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PostDate", SqlDbType.DateTime).Value = postDate;
			BillingBuilder billingBuilder = new BillingBuilder(cmd);
			return billingBuilder.Build();
		}

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByPostDateClient(DateTime postDate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetBillingAccessionsByPostDateClient_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PostDate", SqlDbType.DateTime).Value = postDate;
            cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = webServiceAccount.WebServiceAccountClientCollection.ToIdString();
            BillingBuilder billingBuilder = new BillingBuilder(cmd);
            return billingBuilder.Build();
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastName(string lastName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetBillingAccessionsByLastName_2";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = lastName;
			BillingBuilder billingBuilder = new BillingBuilder(cmd);
			return billingBuilder.Build();
		}

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastNameClient(string lastName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetBillingAccessionsByLastNameClient_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = lastName;
            cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = webServiceAccount.WebServiceAccountClientCollection.ToIdString();
            BillingBuilder billingBuilder = new BillingBuilder(cmd);
            return billingBuilder.Build();
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastNameAndFirstName(string lastName, string firstName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetBillingAccessionsByLastNameAndFirstName_2";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = lastName;
			cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = firstName;
			BillingBuilder billingBuilder = new BillingBuilder(cmd);
			return billingBuilder.Build();
		}

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByLastNameAndFirstNameClient(string lastName, string firstName, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetBillingAccessionsByLastNameAndFirstNameClient_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = lastName;
            cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = firstName;
            cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = webServiceAccount.WebServiceAccountClientCollection.ToIdString();
            BillingBuilder billingBuilder = new BillingBuilder(cmd);
            return billingBuilder.Build();
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByReportNo(string reportNo, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetBillingAccessionsByReportNo_2";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
			BillingBuilder billingBuilder = new BillingBuilder(cmd);
			return billingBuilder.Build();
		}

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByReportNoClient(string reportNo, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetBillingAccessionsByReportNoClient_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
            cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = webServiceAccount.WebServiceAccountClientCollection.ToIdString();
            BillingBuilder billingBuilder = new BillingBuilder(cmd);
            return billingBuilder.Build();
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByBirthdate(DateTime birthdate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetBillingAccessionsByBirthdate_2";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@Birthdate", SqlDbType.DateTime).Value = birthdate;
			BillingBuilder billingBuilder = new BillingBuilder(cmd);
			return billingBuilder.Build();
		}

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsByBirthdateClient(DateTime birthdate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetBillingAccessionsByBirthdateClient_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Birthdate", SqlDbType.DateTime).Value = birthdate;
            cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = webServiceAccount.WebServiceAccountClientCollection.ToIdString();
            BillingBuilder billingBuilder = new BillingBuilder(cmd);
            return billingBuilder.Build();
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsBySsn(string ssn, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetBillingAccessionsBySsn_2";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PSSN", SqlDbType.VarChar).Value = ssn;
			BillingBuilder billingBuilder = new BillingBuilder(cmd);
			return billingBuilder.Build();
		}

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection GetBillingAccessionsBySsnClient(string ssn, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetBillingAccessionsBySsnClient_2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PSSN", SqlDbType.VarChar).Value = ssn;
            cmd.Parameters.Add("@ClientIdString", SqlDbType.VarChar).Value = webServiceAccount.WebServiceAccountClientCollection.ToIdString();
            BillingBuilder billingBuilder = new BillingBuilder(cmd);
            return billingBuilder.Build();
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail GetBillingDetail(string reportNo, bool includeMemoryStream, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetBillingDetailByReportNo_3";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;			
			BillingDetailBuilder billingDetailBuilder = new BillingDetailBuilder(cmd, reportNo, includeMemoryStream, webServiceAccount);
			return billingDetailBuilder.Build();
		}        
	}
}
