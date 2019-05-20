using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class BillingGateway
	{
        public static void UpdateMRNACCT()
        {
            MySqlCommand cmd = new MySqlCommand("prcUpdateMRNACCT");
            cmd.CommandType = CommandType.StoredProcedure;            

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateAccessionBillingInformationFromSVHBillingData(DateTime fileDate)
        {
            MySqlCommand cmd = new MySqlCommand("pUpdateAccessionBillingInformationFromSVHBillingData");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("FileDate", fileDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void CreateBillingEODProcess(DateTime processDate)
        {
            MySqlCommand cmd = new MySqlCommand("Insert Ignore tblBillingEODProcess (ProcessDate) Values(@ProcessDate);");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ProcessDate", processDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateBillingEODProcess(DateTime processDate, string processName)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE tblBillingEODProcess set `" + processName + "` = @CurrTime where ProcessDate = @ProcessDate");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CurrTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ProcessDate", processDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static YellowstonePathology.Business.Billing.Model.EODProcessStatus GetBillingEODProcessStatus(DateTime processDate)
        {
            YellowstonePathology.Business.Billing.Model.EODProcessStatus result = new Billing.Model.EODProcessStatus();
            MySqlCommand cmd = new MySqlCommand("Select * from tblBillingEODProcess where ProcessDate = @ProcessDate;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ProcessDate", processDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
            return result;
        }
    }
}
