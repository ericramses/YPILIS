using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.UI.Billing
{	
	public class AutomationList : ObservableCollection<AutomationListItem>
    {        

		public AutomationList()
		{

		}        

        public static AutomationList GetList(DateTime postDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT distinct pso.MasterAccessionNo, pso.ReportNo, a.AccessionTime AccessionDate,  pso.PanelSetId, " +
                "concat(a.PFirstName, ' ', a.PLastName) AS PatientName, " +
                "a.PLastName, a.PFirstName, a.ClientName, a.PhysicianName, a.PBirthdate, pso.FinalDate, pso.PanelSetName, su.UserName as OrderedBy, " +
                "'' ForeignAccessionNo, pso.IsPosted " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrderCPTCodeBill psocpt on pso.ReportNo = psocpt.ReportNo " +
                "Left Outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE pso.IsPosted = 1 and a.SvhMedicalRecord like 'V%' and pso.PanelSetId = 13 and psocpt.PostDate = @PostDate Order By pso.FinalDate, pso.PanelSetId, a.AccessionTime;";

            cmd.Parameters.AddWithValue("@PostDate", postDate);
            return BuildList(cmd);            
        }

        private static AutomationList BuildList(MySqlCommand cmd)
        {
            AutomationList result = new AutomationList();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AutomationListItem item = new AutomationListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }
            return result;
        }
    }
}
