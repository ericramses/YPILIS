using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class AccessionOrderBuilderV2
    {
        StringBuilder m_SQL;        

        public AccessionOrderBuilderV2()
        {            
            this.m_SQL = new StringBuilder();
            this.m_SQL.AppendLine("declare @MasterAccessionNo varchar(20)");
            this.m_SQL.AppendLine("Select @MasterAccessionNo = '16-20207'");

            this.m_SQL.AppendLine("Select 'tblAccessionOrder' [tablename], * from tblAccessionOrder where masterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("Select 'tblSpecimenOrder' [tablename], * from tblSpecimenOrder where masterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("Select 'tblAliquotOrder' [tablename], ao.* from tblAliquotOrder ao");
            this.m_SQL.AppendLine("join tblSpecimenOrder so on ao.SpecimenOrderId = so.specimenOrderId");
            this.m_SQL.AppendLine("where so.MasterAccessionNo = @MasterAccessionNo");            

            this.m_SQL.AppendLine("Select 'tblSlideOrder' [tablename], so.* from tblSlideOrder so");
            this.m_SQL.AppendLine("join tblAliquotOrder ao on so.AliquotOrderId = ao.AliquotOrderId");
            this.m_SQL.AppendLine("join tblSpecimenOrder s on ao.SpecimenOrderId = s.SpecimenOrderId");
            this.m_SQL.AppendLine("where s.MasterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("Select 'tblPanelSetOrder' [tablename], * from tblPanelSetOrder where masterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("declare @ResultTableSQL as varchar(max)");
            this.m_SQL.AppendLine("Select @ResultTableSQL = 'Select ''' + ps.ResultTableName + ''' [tablename], * from ' + ps.ResultTableName + ' where ReportNo = ''' + pso.ReportNo + '''' from tblPanelSet ps");
            this.m_SQL.AppendLine("join tblPanelSetOrder pso on ps.PanelSetId = pso.panelSetId");
            this.m_SQL.AppendLine("where pso.MasterAccessionNo = @MasterAccessionNo");
            this.m_SQL.AppendLine("exec(@ResultTableSQL)");


            this.m_SQL.AppendLine("Select 'tblPanelOrder' [tablename], po.* from tblPanelOrder po");
            this.m_SQL.AppendLine("join tblPanelSetOrder pso on po.ReportNo = pso.ReportNo");
            this.m_SQL.AppendLine("where pso.MasterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("Select 'tblTestOrder' [tablename], t.* from tblTestOrder t");
            this.m_SQL.AppendLine("join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId");
            this.m_SQL.AppendLine("join tblPanelSetOrder pso on po.ReportNo = pso.ReportNo");
            this.m_SQL.AppendLine("where pso.MasterAccessionNo = @MasterAccessionNo");
        }

        public void Build(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = this.m_SQL.ToString();
            cmd.CommandType = CommandType.Text;            

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    HandleDataSets(dr);  
                }
            }            
        }
        
        private void HandleDataSets(SqlDataReader dr)
        {
            while (dr.Read())
            {
                string tablename = dr.GetString(0);
                switch (tablename)
                {

                }                
            }

            if (dr.NextResult() == true)
            {
                HandleDataSets(dr);
            }
        } 
        
        private void HandleAccessionOrder(SqlDataAdapter dr)
        {

        }

        private void HandleSpecimenOrder(SqlDataAdapter dr)
        {

        }        
    }
}
