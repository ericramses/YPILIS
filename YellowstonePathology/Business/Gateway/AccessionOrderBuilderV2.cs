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
        Test.AccessionOrder m_AccessionOrder;
        PanelSet.Model.PanelSetCollection m_PanelSetCollection;

        public AccessionOrderBuilderV2()
        {
            this.m_PanelSetCollection = PanelSet.Model.PanelSetCollection.GetAll();
            this.m_SQL = new StringBuilder();
            this.m_SQL.AppendLine("declare @MasterAccessionNo varchar(20)");
            this.m_SQL.AppendLine("Select @MasterAccessionNo = '16-20207'");

            this.m_SQL.AppendLine("Select 'tblAccessionOrder' as tablename, * from tblAccessionOrder where masterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("Select 'tblSpecimenOrder' as tablename, * from tblSpecimenOrder where masterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("Select 'tblAliquotOrder' as tablename, ao.* from tblAliquotOrder ao");
            this.m_SQL.AppendLine("join tblSpecimenOrder so on ao.SpecimenOrderId = so.specimenOrderId");
            this.m_SQL.AppendLine("where so.MasterAccessionNo = @MasterAccessionNo");            

            this.m_SQL.AppendLine("Select 'tblSlideOrder' as tablename, so.* from tblSlideOrder so");
            this.m_SQL.AppendLine("join tblAliquotOrder ao on so.AliquotOrderId = ao.AliquotOrderId");
            this.m_SQL.AppendLine("join tblSpecimenOrder s on ao.SpecimenOrderId = s.SpecimenOrderId");
            this.m_SQL.AppendLine("where s.MasterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("Select pso.ReportNo, ps.ResultTableName");
            this.m_SQL.AppendLine("into #TestOrders");
            this.m_SQL.AppendLine("from tblPanelSetOrder pso join tblPanelSet ps on pso.PanelSetId = ps.PanelSetId where pso.MasterAccessionNo = @MasterAccessionNo");
            this.m_SQL.AppendLine("declare @ReportNo varchar(20)");
            this.m_SQL.AppendLine("declare @ResultTableName varchar(100)");
            this.m_SQL.AppendLine("DECLARE PanelSets CURSOR FOR SELECT* FROM #TestOrders;");
            this.m_SQL.AppendLine("OPEN PanelSets;");
            this.m_SQL.AppendLine("WHILE(1 = 1)");
            this.m_SQL.AppendLine("BEGIN;");
            this.m_SQL.AppendLine("FETCH NEXT");
            this.m_SQL.AppendLine("FROM PanelSets");
            this.m_SQL.AppendLine("INTO @ReportNo, @ResultTableName;");
            this.m_SQL.AppendLine("IF @@FETCH_STATUS < 0 BREAK;");
            this.m_SQL.AppendLine("declare @ResultTableSQL as varchar(max)");
            this.m_SQL.AppendLine("select @ResultTableSQL = 'Select ''tblPanelSetOrder'' as tablename, pso.*, rt.* from ' + @ResultTableName + ' rt join tblPanelSetOrder pso on rt.ReportNo = pso.ReportNo ' +");
            this.m_SQL.AppendLine("'join tblPanelSet ps on ps.PanelSetId = pso.panelSetId ' +");
            this.m_SQL.AppendLine("'where pso.ReportNo = ''' + @ReportNo + ''''");
            this.m_SQL.AppendLine("exec(@ResultTableSQL)");
            this.m_SQL.AppendLine("END;");
            this.m_SQL.AppendLine("CLOSE PanelSets;");
            this.m_SQL.AppendLine("DEALLOCATE PanelSets;");
            this.m_SQL.AppendLine("drop table #TestOrders");

            this.m_SQL.AppendLine("Select 'tblPanelOrder' as tablename, po.* from tblPanelOrder po");
            this.m_SQL.AppendLine("join tblPanelSetOrder pso on po.ReportNo = pso.ReportNo");
            this.m_SQL.AppendLine("where pso.MasterAccessionNo = @MasterAccessionNo");

            this.m_SQL.AppendLine("Select 'tblTestOrder' as tablename, t.* from tblTestOrder t");
            this.m_SQL.AppendLine("join tblPanelOrder po on t.PanelOrderId = po.PanelOrderId");
            this.m_SQL.AppendLine("join tblPanelSetOrder pso on po.ReportNo = pso.ReportNo");
            this.m_SQL.AppendLine("where pso.MasterAccessionNo = @MasterAccessionNo");
        }

        public void Build(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;

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
                    case "tblAccessionOrder":
                        this.HandleAccessionOrder(dr);
                        break;
                    case "tblSpecimenOrder":
                        this.HandleSpecimenOrder(dr);
                        break;
                    case "tblAliquotOrder":
                        this.HandleAliquotOrder(dr);
                        break;
                    case "tblSlideOrder":
                        this.HandleSlideOrder(dr);
                        break;
                    case "tblPanelSetOrder":
                        this.HandlePanelSetOrder(dr);
                        break;
                    case "tblPanelOrder":
                        this.HandlePanelOrder(dr);
                        break;
                    case "tblTestOrder":
                        this.HandleTestOrder(dr);
                        break;
                }
            }

            if (dr.NextResult() == true)
            {
                HandleDataSets(dr);
            }
        } 
        
        private void HandleAccessionOrder(SqlDataReader dr)
        {                        
            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(this.m_AccessionOrder, dr);
            sqlDataReaderPropertyWriter.WriteProperties();
        }

        private void HandleSpecimenOrder(SqlDataReader dr)
        {
            Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
            sqlDataReaderPropertyWriter.WriteProperties();
            this.m_AccessionOrder.SpecimenOrderCollection.Add(specimenOrder);
        }

        private void HandleAliquotOrder(SqlDataReader dr)
        {
            Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(aliquotOrder, dr);
            sqlDataReaderPropertyWriter.WriteProperties();
            this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(aliquotOrder.SpecimenOrderId).AliquotOrderCollection.Add(aliquotOrder);
        }

        private void HandleSlideOrder(SqlDataReader dr)
        {
            Slide.Model.SlideOrder slideOrder = new Slide.Model.SlideOrder();
            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(slideOrder, dr);
            sqlDataReaderPropertyWriter.WriteProperties();
            this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(slideOrder.AliquotOrderId).SlideOrderCollection.Add(slideOrder);
        }

        private void HandlePanelSetOrder(SqlDataReader dr)
        {
            int panelSetId = (int)dr["PanelSetId"];
            PanelSet.Model.PanelSet panelSet = this.m_PanelSetCollection.GetPanelSet(panelSetId);
            Test.PanelSetOrder panelSetOrder = Test.PanelSetOrderFactory.CreatePanelSetOrder(panelSet);
            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrder, dr);
            sqlDataReaderPropertyWriter.WriteProperties();
            this.m_AccessionOrder.PanelSetOrderCollection.Add(panelSetOrder);
        }

        private void HandlePanelOrder(SqlDataReader dr)
        {
            int panelId = (int)dr["PanelId"];
            Panel.Model.Panel panel = Panel.Model.PanelCollection.GetAll().GetPanel(panelId);
            Test.PanelOrder panelOrder = Test.PanelOrderFactory.GetPanelOrder(panel);
            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelOrder, dr);
            sqlDataReaderPropertyWriter.WriteProperties();
            this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelOrder.ReportNo).PanelOrderCollection.Add(panelOrder);
        }

        private void HandleTestOrder(SqlDataReader dr)
        {
            int testId = (int)dr["TestId"];
            Test.Model.TestOrder testOrder = new Test.Model.TestOrder();
            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrder, dr);
            sqlDataReaderPropertyWriter.WriteProperties();
            this.m_AccessionOrder.PanelSetOrderCollection.GetPanelOrder(testOrder.PanelOrderId).TestOrderCollection.Add(testOrder);

            Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(testOrder.AliquotOrderId);
            aliquotOrder.TestOrderCollection.Add(testOrder);
            testOrder.AliquotOrder = aliquotOrder;

            Slide.Model.SlideOrder slideOrder = aliquotOrder.SlideOrderCollection.GetSlideOrderByTestOrderId(testOrder.TestOrderId);
            slideOrder.TestOrder = testOrder;
            testOrder.SlideOrderCollection.Add(slideOrder);
        }
    }
}
