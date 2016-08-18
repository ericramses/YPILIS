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
        Test.AccessionOrder m_AccessionOrder;
        PanelSet.Model.PanelSetCollection m_PanelSetCollection;

        public AccessionOrderBuilderV2()
        {
            this.m_PanelSetCollection = PanelSet.Model.PanelSetCollection.GetAll();
            this.m_SQL = new StringBuilder();
            this.m_SQL.AppendLine("declare @MasterAccessionNo varchar(20)");
            this.m_SQL.AppendLine("Select @MasterAccessionNo = '16-20207'");

        }

        public void Build(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "whctest";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = accessionOrder.MasterAccessionNo;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    HandleDataSets(dr);  
                }
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                this.BuildSurgicalObjects();
            }
        }

        private void HandleDataSets(SqlDataReader dr)
        {
            DataTable dataTable = new DataTable();
            dataTable.Load(dr, LoadOption.OverwriteChanges);
            if(dataTable.Rows.Count > 0)
            {
                string tablename = dataTable.Rows[0][0].ToString();
                switch (tablename)
                {
                    case "tblAccessionOrder":
                        this.HandleAccessionOrder(dataTable);
                        break;
                    case "tblSpecimenOrder":
                        this.HandleSpecimenOrder(dataTable);
                        break;
                    case "tblAliquotOrder":
                        this.HandleAliquotOrder(dataTable);
                        break;
                    case "tblSlideOrder":
                        this.HandleSlideOrder(dataTable);
                        break;
                    case "tblPanelSetOrder":
                        this.HandlePanelSetOrder(dataTable);
                        break;
                    case "tblPanelOrder":
                        this.HandlePanelOrder(dataTable);
                        break;
                    case "tblTestOrder":
                        this.HandleTestOrder(dataTable);
                        break;
                    case "tblTaskOrder":
                        this.HandleTaskOrder(dataTable);
                        break;
                    case "tblTaskOrderDetail":
                        this.HandleTaskOrderDetail(dataTable);
                        break;
                    case "tblICD9BillingCode":
                        this.HandleICD9BillingCode(dataTable);
                        break;
                    case "tblAmendment":
                        this.HandleAmendment(dataTable);
                        break;
                    case "tblPanelSetOrderCPTCode":
                        this.HandlePanelSetOrderCPTCode(dataTable);
                        break;
                    case "tblPanelSetOrderCPTCodeBill":
                        this.HandlePanelSetOrderCPTCodeBill(dataTable);
                        break;
                    case "tblTestOrderReportDistribution":
                        this.HandleTestOrderReportDistribution(dataTable);
                        break;
                    case "tblTestOrderReportDistributionLog":
                        this.HandleTestOrderReportDistributionLog(dataTable);
                        break;
                    case "tblSurgicalSpecimen":
                        this.HandleSurgicalSpecimen(dataTable);
                        break;
                    case "tblIcd9Code":
                        this.HandleICD9Code(dataTable);
                        break;
                    case "tblIntraoperativeConsultationResult":
                        this.HandleIntraoperativeConsultationResult(dataTable);
                        break;
                    case "tblStainResult":
                        this.HandleStainResult(dataTable);
                        break;
                    case "tblSurgicalAudit":
                        this.HandleSurgicalAudit(dataTable);
                        break;
                    case "tblSurgicalSpecimenAudit":
                        this.HandleSurgicalSpecimenAudit(dataTable);
                        break;
                    case "tblFlowMarkers":
                        this.HandleFlowMarker(dataTable);
                        break;
                }
            }

            if (dr.IsClosed == false)
            {
                HandleDataSets(dr);
            }
        } 
        
        private void HandleAccessionOrder(SqlDataReader dr)
        {                        
            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(this.m_AccessionOrder, dr);
            sqlDataReaderPropertyWriter.WriteProperties();
        }

        private void HandleSlideOrder(DataTable dataTable)
        {
            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                foreach(Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    aliquotOrder.SlideOrderCollection.Sync(dataTable, aliquotOrder.AliquotOrderId);
                }
            }
        }

        private void HandlePanelSetOrder(DataTable dataTable)
        {
            this.m_AccessionOrder.PanelSetOrderCollection.Sync(dataTable);
        }

        private void HandlePanelOrder(DataTable dataTable)
        {
            foreach(Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PanelOrderCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }

        private void HandleTestOrder(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                foreach(Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    panelOrder.TestOrderCollection.Sync(dataTable, panelOrder.PanelOrderId);
                }
            }

            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                foreach (Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    aliquotOrder.TestOrderCollection.Sync(dataTable, aliquotOrder.AliquotOrderId);
                }
            }

            this.m_AccessionOrder.SyncTestOrders(dataTable);

            /*-int testId = (int)dr["TestId"];
            -Test.Model.TestOrder poTestOrder = new Test.Model.TestOrder();
            -Persistence.SqlDataReaderPropertyWriter poSqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(poTestOrder, dr);
            -poSqlDataReaderPropertyWriter.WriteProperties();
            -this.m_AccessionOrder.PanelSetOrderCollection.GetPanelOrder(poTestOrder.PanelOrderId).TestOrderCollection.Add(poTestOrder);

            -Test.Model.TestOrder aoTestOrder = new Test.Model.TestOrder();
            -Persistence.SqlDataReaderPropertyWriter aoSqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(aoTestOrder, dr);
            -aoSqlDataReaderPropertyWriter.WriteProperties();
            -Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aoTestOrder.AliquotOrderId);
            -aliquotOrder.TestOrderCollection.Add(aoTestOrder);
            -this.BuildTestOrderAliquotOrder(poTestOrder, aliquotOrder);


            -Test.Model.TestOrder soTestOrder = new Test.Model.TestOrder();
            -Persistence.SqlDataReaderPropertyWriter soSqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(soTestOrder, dr);
            -soSqlDataReaderPropertyWriter.WriteProperties();
            Slide.Model.SlideOrder slideOrder = aliquotOrder.SlideOrderCollection.GetSlideOrderByTestOrderId(soTestOrder.TestOrderId);
            if (slideOrder != null)
            {
                slideOrder.TestOrder = soTestOrder;
            }
            this.BuildTestOrderSlideOrderCollection(poTestOrder);*/
        }
        private void HandleTaskOrder(DataTable dataTable)
        {
            this.m_AccessionOrder.TaskOrderCollection.Sync(dataTable);
        }

        private void HandleTaskOrderDetail(DataTable dataTable)
        {
            foreach (Task.Model.TaskOrder taskOrder in this.m_AccessionOrder.TaskOrderCollection)
            {
                taskOrder.TaskOrderDetailCollection.Sync(dataTable, taskOrder.TaskOrderId);
            }
        }

        private void HandleICD9BillingCode(DataTable dataTable)
        {
            this.m_AccessionOrder.ICD9BillingCodeCollection.Sync(dataTable);
        }

        private void HandleAmendment(DataTable dataTable)
        {
            foreach(Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.AmendmentCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }

        private void HandlePanelSetOrderCPTCode(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PanelSetOrderCPTCodeCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }
        private void HandlePanelSetOrderCPTCodeBill(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PanelSetOrderCPTCodeBillCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }
        private void HandleTestOrderReportDistribution(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.TestOrderReportDistributionCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }
        private void HandleTestOrderReportDistributionLog(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.TestOrderReportDistributionLogCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }

        private void HandleSurgicalSpecimen(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            surgicalTestOrder.SurgicalSpecimenCollection.Sync(dataTable, surgicalTestOrder.ReportNo);
        }

        private void HandleICD9Code(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach(Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                surgicalSpecimen.ICD9BillingCodeCollection.Sync(dataTable, surgicalSpecimen.SurgicalSpecimenId);
            }
        }
        private void HandleIntraoperativeConsultationResult(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach (Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                surgicalSpecimen.IntraoperativeConsultationResultCollection.Sync(dataTable, surgicalSpecimen.SurgicalSpecimenId);
            }
        }

        private void HandleStainResult(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach (Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                surgicalSpecimen.StainResultItemCollection.Sync(dataTable, surgicalSpecimen.SurgicalSpecimenId);
            }
        }

        private void HandleSurgicalAudit(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            surgicalTestOrder.SurgicalAuditCollection.Sync(dataTable);
        }

        private void HandleSurgicalSpecimenAudit(DataTable dataTable)
        {
            Test.Surgical.SurgicalTestOrder surgicalTestOrder = (Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach (Test.Surgical.SurgicalAudit surgicalAudit in surgicalTestOrder.SurgicalAuditCollection)
            {
                surgicalAudit.SurgicalSpecimenAuditCollection.Sync(dataTable, surgicalAudit.SurgicalAuditId);
            }
        }

        private void HandleFlowMarker(DataTable dataTable)
        {
            foreach(Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                if(panelSetOrder is Test.LLP.PanelSetOrderLeukemiaLymphoma)
                {
                    Test.LLP.PanelSetOrderLeukemiaLymphoma llpPanelSetOrder = (Test.LLP.PanelSetOrderLeukemiaLymphoma)panelSetOrder;
                    llpPanelSetOrder.FlowMarkerCollection.Sync(dataTable, llpPanelSetOrder.ReportNo);
                }
            }
        }

        private void BuildSurgicalObjects()
        {

        }
    }
}
