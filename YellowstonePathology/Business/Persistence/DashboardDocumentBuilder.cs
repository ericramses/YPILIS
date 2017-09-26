using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class DashboardDocumentBuilder : DocumentBuilder
    {
        private MySqlCommand m_SQLCommand;

        public DashboardDocumentBuilder()
        {
            this.m_SQLCommand = new MySqlCommand();
            this.m_SQLCommand.CommandText = "prcGetDashboard";
            this.m_SQLCommand.CommandType = CommandType.StoredProcedure;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Monitor.Model.Dashboard dashboard = new Monitor.Model.Dashboard();
            this.BuildDashboard(dashboard);
            return dashboard;
        }

        private void BuildDashboard(YellowstonePathology.Business.Monitor.Model.Dashboard dashboard)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        string s = dr[0].ToString();
                        dashboard.YPIBlocks = Convert.ToInt32(dr[0].ToString());
                    }

                    dr.NextResult();

                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(dashboard, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }
    }
}
