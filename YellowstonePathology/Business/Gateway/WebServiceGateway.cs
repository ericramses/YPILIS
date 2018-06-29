using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class WebServiceGateway
    {
        public static Business.WebService.WebServiceAccountCollection GetWebServiceAccounts()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblWebServiceAccount order by DisplayName;";
            cmd.CommandType = CommandType.Text;

            WebService.WebServiceAccountCollection result = BuildWebServiceAccountCollection(cmd);
            return result;
        }

        public static Business.WebService.WebServiceAccountCollection GetWebServiceAccountsByClientId(int clientId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select wsa.* from tblWebServiceAccount wsa join tblWebServiceAccountClient wsac on " +
                "wsa.WebServiceAccountId = wsac.WebServiceAccountId where wsac.ClientId = @ClientId order by DisplayName;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ClientId", clientId);

            WebService.WebServiceAccountCollection result = BuildWebServiceAccountCollection(cmd);
            return result;
        }

        public static Business.WebService.WebServiceAccountCollection GetWebServiceAccountsByDisplayName(string displayName)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblWebServiceAccount where DisplayName like '" + displayName + "%' order by DisplayName;";
            cmd.CommandType = CommandType.Text;

            WebService.WebServiceAccountCollection result = BuildWebServiceAccountCollection(cmd);
            return result;
        }

        private static WebService.WebServiceAccountCollection BuildWebServiceAccountCollection(MySqlCommand cmd)
        {
            WebService.WebServiceAccountCollection result = new WebService.WebServiceAccountCollection();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        WebService.WebServiceAccount webServiceAccount = new WebService.WebServiceAccount();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlServerDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(webServiceAccount, dr);
                        sqlServerDataReaderPropertyWriter.WriteProperties();
                        result.Add(webServiceAccount);
                    }
                }
            }

            return result;
        }

        public static List<WebService.WebServiceAccountView> GetWebServiceAccountViewList()
        {
            List<WebService.WebServiceAccountView> result = new List<WebService.WebServiceAccountView>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select c.ClientName, w.WebServiceAccountId, w.DisplayName, w.InitialPage from tblClient c join tblWebServiceAccount w on c.ClientId = w.PrimaryClientId order by w.DisplayName;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        WebService.WebServiceAccountView webServiceAccountView = new WebService.WebServiceAccountView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlServerDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(webServiceAccountView, dr);
                        sqlServerDataReaderPropertyWriter.WriteProperties();
                        result.Add(webServiceAccountView);
                    }
                }
            }

            return result;
        }

        /*public static void UpdateWebServiceAccount(YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount)
        {
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append("Update tblWebServiceAccount set UserName = @UserName, ");
            cmdText.Append("Password =  @Password, ");
            cmdText.Append("DisplayName = @DisplayName, ");
            cmdText.Append("PrimaryClientId = @PrimaryClientId, ");
            cmdText.Append("DownloadFileType = @DownloadFileType, ");
            cmdText.Append("InitialPage = @InitialPage, ");
            cmdText.Append("ApplicationTimeoutMinutes = @ApplicationTimeoutMinutes, ");
            cmdText.Append("RemoteFileDownloadDirectory = @RemoteFileDownloadDirectory, ");
            cmdText.Append("RemoteFileUploadDirectory = @RemoteFileUploadDirectory, ");
            cmdText.Append("AlertEmailAddress = @AlertEmailAddress ,");
            cmdText.Append("SaveUserNameLocal = @SaveUserNameLocal, ");
            cmdText.Append("SavePasswordLocal = @SavePasswordLocal, ");
            cmdText.Append("EnableApplicationTimeout = @EnableApplicationTimeout, ");
            cmdText.Append("EnableSaveSettings = @EnableSaveSettings, ");
            cmdText.Append("EnableFileUpload = @EnableFileUpload, ");
            cmdText.Append("EnableFileDownload = @EnableFileDownload, ");
            cmdText.Append("EnableOrderEntry = @EnableOrderEntry, ");
            cmdText.Append("EnableReportBrowser = @EnableReportBrowser, ");
            cmdText.Append("EnableBillingBrowser = @EnableBillingBrowser, ");
            cmdText.Append("EnableEmailAlert = @EnableEmailAlert, ");
            cmdText.Append("VersionCurrentlyUsing = @VersionCurrentlyUsing, ");
            cmdText.Append("SystemUserId = @SystemUserId, ");
            cmdText.Append("Signature = @Signature, ");
            cmdText.Append("FacilityId = @FacilityId, ");
            cmdText.Append("ObjectId = @ObjectId ");
            cmdText.Append("where WebServiceAccountId = @WebServiceAccountId;");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value=webServiceAccount.UserName;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = webServiceAccount.Password;
            cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = webServiceAccount.DisplayName;
            cmd.Parameters.Add("@PrimaryClientId", SqlDbType.Int).Value = webServiceAccount.PrimaryClientId;
            cmd.Parameters.Add("@DownloadFileType", SqlDbType.VarChar).Value = webServiceAccount.DownloadFileType;
            cmd.Parameters.Add("@InitialPage", SqlDbType.VarChar).Value = webServiceAccount.InitialPage;
            cmd.Parameters.Add("@ApplicationTimeoutMinutes", SqlDbType.Int).Value = webServiceAccount.ApplicationTimeoutMinutes;
            cmd.Parameters.Add("@RemoteFileDownloadDirectory", SqlDbType.VarChar).Value = webServiceAccount.RemoteFileDownloadDirectory;
            cmd.Parameters.Add("@RemoteFileUploadDirectory", SqlDbType.VarChar).Value = webServiceAccount.RemoteFileUploadDirectory;
            cmd.Parameters.Add("@AlertEmailAddress", SqlDbType.VarChar).Value = webServiceAccount.AlertEmailAddress;
            cmd.Parameters.Add("@SaveUserNameLocal", SqlDbType.Bit).Value = webServiceAccount.SaveUserNameLocal;
            cmd.Parameters.Add("@SavePasswordLocal", SqlDbType.Bit).Value = webServiceAccount.SavePasswordLocal;
            cmd.Parameters.Add("@EnableApplicationTimeout", SqlDbType.Bit).Value = webServiceAccount.EnableApplicationTimeout;
            cmd.Parameters.Add("@EnableSaveSettings", SqlDbType.Bit).Value = webServiceAccount.EnableSaveSettings;
            cmd.Parameters.Add("@EnableFileUpload", SqlDbType.Bit).Value = webServiceAccount.EnableFileUpload;
            cmd.Parameters.Add("@EnableFileDownload", SqlDbType.Bit).Value = webServiceAccount.EnableFileDownload;
            cmd.Parameters.Add("@EnableOrderEntry", SqlDbType.Bit).Value = webServiceAccount.EnableOrderEntry;
            cmd.Parameters.Add("@EnableReportBrowser", SqlDbType.Bit).Value = webServiceAccount.EnableReportBrowser;
            cmd.Parameters.Add("@EnableBillingBrowser", SqlDbType.Bit).Value = webServiceAccount.EnableBillingBrowser;
            cmd.Parameters.Add("@EnableEmailAlert", SqlDbType.Bit).Value = webServiceAccount.EnableEmailAlert;
            cmd.Parameters.Add("@VersionCurrentlyUsing", SqlDbType.VarChar).Value = webServiceAccount.VersionCurrentlyUsing;
            cmd.Parameters.Add("@SystemUserId", SqlDbType.Int).Value = webServiceAccount.SystemUserId;
            cmd.Parameters.Add("@Signature", SqlDbType.VarChar).Value = webServiceAccount.Signature;
            cmd.Parameters.Add("@FacilityId", SqlDbType.VarChar).Value = webServiceAccount.FacilityId;
            cmd.Parameters.Add("@ObjectId", SqlDbType.VarChar).Value = webServiceAccount.ObjectId;
            cmd.Parameters.Add("@WebServiceAccountId", SqlDbType.Int).Value = webServiceAccount.WebServiceAccountId;
            foreach(SqlParameter parameter in cmd.Parameters)
            {
                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }
            }
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }*/

        public static int GetNextWebServiceAccountId()
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select max(WebServiceAccountId) + 1 id from tblWebServiceAccount;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (int)((Int64)dr[0]);
                    }
                }
            }
            return result;
        }

        public static void UpDateSqlServerFromMySQL()
        {
            SqlParameter tableNameParameter = new SqlParameter("@TableName", SqlDbType.VarChar);
            tableNameParameter.Value = "tblWebServiceAccount";
            SqlParameter keyFieldParameter = new SqlParameter("@KeyField", SqlDbType.VarChar);
            keyFieldParameter.Value = "WebServiceAccountId";
            SqlParameter lastUpdateParameter = new SqlParameter("@LastUpdate", SqlDbType.DateTime);
            lastUpdateParameter.Value = DateTime.Now.AddDays(-1);
            SqlParameter currentUpdateParameter = new SqlParameter("@CurrentUpdate", SqlDbType.DateTime);
            currentUpdateParameter.Value = DateTime.Now;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "mysqlupdatesstable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(tableNameParameter);
            cmd.Parameters.Add(keyFieldParameter);
            cmd.Parameters.Add(lastUpdateParameter);
            cmd.Parameters.Add(currentUpdateParameter);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "mysqlinsertsstable";

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "mysqldeletesstable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Remove(lastUpdateParameter);
            cmd.Parameters.Remove(currentUpdateParameter);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.Parameters.Remove(tableNameParameter);
            cmd.Parameters.Remove(keyFieldParameter);
            tableNameParameter.Value = "tblWebServiceAccountClient";
            keyFieldParameter.Value = "WebServiceAccountClientId";

            cmd = new SqlCommand();
            cmd.CommandText = "mysqlupdatesstable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(tableNameParameter);
            cmd.Parameters.Add(keyFieldParameter);
            cmd.Parameters.Add(lastUpdateParameter);
            cmd.Parameters.Add(currentUpdateParameter);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "mysqlinsertsstable";

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "mysqldeletesstable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Remove(lastUpdateParameter);
            cmd.Parameters.Remove(currentUpdateParameter);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
