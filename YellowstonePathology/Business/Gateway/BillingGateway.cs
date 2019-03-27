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
        
        /*public static YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection GetNoCodeTypeItems()
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection result = new Test.PanelSetOrderCPTCodeBillCollection();
            MySqlCommand cmd = new MySqlCommand("select * from tblPanelSetOrderCPTCodeBill where CodeType is null and PostDate is not null " +
                "order by PostDate desc limit 1000;");
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill cptBill = new Test.PanelSetOrderCPTCodeBill();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(cptBill, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(cptBill);
                    }
                }
            }

            return result;
        }
        
        public static void SetNoCodeTypeItems(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill cptBill)
        {
            MySqlCommand cmd = new MySqlCommand("Update tblPanelSetOrderCPTCodeBill b set b.CodeType = @CodeType where b.PanelSetOrderCPTCodeBillId = @PanelSetOrderCPTCodeBillId;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PanelSetOrderCPTCodeBillId", cptBill.PanelSetOrderCPTCodeBillId);
            cmd.Parameters.AddWithValue("@CodeType", cptBill.CodeType);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }*/

    }
}
