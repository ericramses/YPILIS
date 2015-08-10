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

namespace YellowstonePathology.YpiConnect.Service.Identity
{
    public class WebServiceAccountGateway 
    {
        public WebServiceAccountGateway()
        {

        }

        public YellowstonePathology.Business.Client.Model.ClientLocation GetClientLocation(int clientLocationId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblClientLocation where ClientLocationId = @ClientLocationId";

            SqlParameter clientLocationIdParameter = new SqlParameter("@ClientLocationId", SqlDbType.Int);
            clientLocationIdParameter.Value = clientLocationId;
            cmd.Parameters.Add(clientLocationIdParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = null;
            using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        clientLocation = new Business.Client.Model.ClientLocation();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(clientLocation, dr);
						propertyWriter.WriteProperties();
                    }
                }
            }
            
            return clientLocation;
        }

		public YellowstonePathology.Business.Client.Model.ClientCollection GetClientCollectionForContextSelection(string userName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select c.* " +
                "from tblClient c " +
                "join tblWebServiceAccountClient wsac on c.ClientId = wsac.ClientId " +
                "join tblWebServiceAccount wsa on wsac.WebServiceAccountId = wsa.WebServiceAccountId " +
                "where wsa.UserName = @UserName";            
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;            
            return this.BuildClientCollection(cmd);
        }

        public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount GetAccount(string userName, string password)
        {
            YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount result = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ws_GetWebServiceAccount";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;            
			
			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					result = this.BuildWebServiceAccount(dr);
				}
			}
			return result;
		}

		private YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount BuildWebServiceAccount(SqlDataReader dr)
		{
            YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount = null;
			while (dr.Read())
			{
                webServiceAccount = new Contract.Identity.WebServiceAccount();
				YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(webServiceAccount, dr);
				propertyWriter.WriteProperties();
			}
			if (webServiceAccount != null)
			{
				dr.NextResult();

				while (dr.Read())
                {
                    YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountClient webServiceAccountClient = new Contract.Identity.WebServiceAccountClient();
                    YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(webServiceAccountClient, dr);
					propertyWriter.WriteProperties();
                    webServiceAccount.WebServiceAccountClientCollection.Add(webServiceAccountClient);
                }

				dr.NextResult();

				while (dr.Read())
				{
                    webServiceAccount.Client = new Business.Client.Model.Client();
					int s = (int)dr["ClientId"];
					//TODO this is here becuse Client.Zip should be a nullable int but is not.
					if (s == 0)
					{
						webServiceAccount.Client.ClientName = "Not Provided";
					}
					else
					{
                        webServiceAccount.Client = new Business.Client.Model.Client();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(webServiceAccount.Client, dr);
						propertyWriter.WriteProperties();
					}
				}
			}

			if (webServiceAccount == null)
			{
                webServiceAccount = new Contract.Identity.WebServiceAccount();
                webServiceAccount.UserName = "Unknown";
                webServiceAccount.Password = "unknown";
                webServiceAccount.IsKnown = false;
			}
			return webServiceAccount;
		}

		public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountCollection GetWebServiceAccountCollectionByFacilityId(string facilityId)
		{
			YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountCollection webServiceAccountCollection = new Contract.Identity.WebServiceAccountCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetWebServiceAccountCollectionByFacilityId_New";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@FacilityId", SqlDbType.VarChar).Value = facilityId;

			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						int accountCount = (int)dr["AccountCount"];
						for(int cnt = 0; cnt < accountCount; cnt++)
						{
							dr.NextResult();
							YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount = this.BuildWebServiceAccount(dr);
							webServiceAccountCollection.Add(webServiceAccount);
						}
					}
				}
			}
			return webServiceAccountCollection;
		}

		private YellowstonePathology.Business.Client.Model.ClientCollection BuildClientCollection(SqlCommand cmd)
        {
            YellowstonePathology.Business.Client.Model.ClientCollection clientCollection = new Business.Client.Model.ClientCollection();
            using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.Client client = new Business.Client.Model.Client();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(client, dr);
						propertyWriter.WriteProperties();
                    }                    
                }
            }            
            return clientCollection;
        }
    }
}
