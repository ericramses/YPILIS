using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class WebServiceGateway
    {
        public static Business.WebService.WebServiceAccountCollection GetWebServiceAccountCollection()
        {
            WebService.WebServiceAccountCollection result = new WebService.WebServiceAccountCollection();

           SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblWebServiceAccount order by UserName;";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        WebService.WebServiceAccount webServiceAccount = new WebService.WebServiceAccount();
                        YellowstonePathology.Business.Persistence.SqlServerDataReaderPropertyWriter sqlServerDataReaderPropertyWriter = new Persistence.SqlServerDataReaderPropertyWriter(webServiceAccount, dr);
                        sqlServerDataReaderPropertyWriter.WriteProperties();
                        result.Add(webServiceAccount);
                    }
                }
            }

            return result;
        }
    }
}
