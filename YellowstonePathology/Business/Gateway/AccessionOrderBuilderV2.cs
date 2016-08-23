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
        private Test.AccessionOrder m_AccessionOrder;
        private PanelSet.Model.PanelSetCollection m_PanelSetCollection;
        private List<string> m_PanelSetOrderReportNumbers;
        private DataTable m_TestOrderDataTable;
        private DataTable m_AliquotOrderDataTable;    

        public AccessionOrderBuilderV2()
        {
            this.m_PanelSetCollection = PanelSet.Model.PanelSetCollection.GetAll();

        }

        public void Build(SqlCommand cmd, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_PanelSetOrderReportNumbers = new List<string>();
            this.m_AccessionOrder = accessionOrder;
                        
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    HandleDataSets(dr);
                }
            }

            this.m_AccessionOrder.AccessionLock.MasterAccessionNo = accessionOrder.MasterAccessionNo;
            this.m_AccessionOrder.PanelSetOrderCollection.RemoveDeleted(this.m_PanelSetOrderReportNumbers);
            if(this.m_TestOrderDataTable != null) this.HandleSlideOrderTestOrder(this.m_TestOrderDataTable);
            if(this.m_AliquotOrderDataTable != null) this.HandleTestOrderAliquotOrder(this.m_AliquotOrderDataTable);
        }

        private void HandleDataSets(SqlDataReader dr)
        {
            DataTable dataTable = new DataTable();
            dataTable.Load(dr, LoadOption.OverwriteChanges);
            if (dataTable.Rows.Count > 0)
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

        private void HandleAccessionOrder(DataTable dataTable)
        {
            this.m_AccessionOrder.Sync(dataTable);
        }

        private void HandleSpecimenOrder(DataTable dataTable)
        {
            this.m_AccessionOrder.SpecimenOrderCollection.Sync(dataTable);
        }

        private void HandleAliquotOrder(DataTable dataTable)
        {
            this.m_AliquotOrderDataTable = dataTable;
            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                specimenOrder.AliquotOrderCollection.Sync(dataTable, specimenOrder.SpecimenOrderId);
            }
        }

        private void HandleSlideOrder(DataTable dataTable)
        {
            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                foreach (Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    aliquotOrder.SlideOrderCollection.Sync(dataTable, aliquotOrder.AliquotOrderId);
                }
            }
        }

        private void HandlePanelSetOrder(DataTable dataTable)
        {
            string reportNo = dataTable.Rows[0]["ReportNo"].ToString();            
            this.m_PanelSetOrderReportNumbers.Add(reportNo);
            this.m_AccessionOrder.PanelSetOrderCollection.Sync(dataTable);
        }

        private void HandlePanelOrder(DataTable dataTable)
        {
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.PanelOrderCollection.Sync(dataTable, panelSetOrder.ReportNo);
            }
        }

        private void HandleTestOrder(DataTable dataTable)
        {
            this.m_TestOrderDataTable = dataTable;
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                foreach (Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    panelOrder.TestOrderCollection.Sync(dataTable, panelOrder.PanelOrderId);
                }
            }            
        }

        private void HandlAliquotOrderTestOrder(DataTable dataTable)
        {
            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                foreach(Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    aliquotOrder.TestOrderCollection.Sync(dataTable, aliquotOrder.AliquotOrderId);                    
                }
            }
        }

        private void HandlTestOrderSlideOrderCollection(DataTable dataTable)
        {
            foreach (Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                foreach (Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach (Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        //stOrder.SlideOrderCollection.sy
                    }
                }
            }
        }

        private void HandleSlideOrderTestOrder(DataTable dataTable)
        {
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                YellowstonePathology.Business.Test.Model.TestOrder_Base testOrder = null;
                foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    foreach (Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                    {
                        foreach (Business.Slide.Model.SlideOrder slideOrder in aliquotOrder.SlideOrderCollection)
                        {
                            if(slideOrder.TestOrder == null)
                            {
                                testOrder = new Test.Model.TestOrder();
                            }
                            else
                            {
                                testOrder = slideOrder.TestOrder;
                            }

                            YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(testOrder, dataTableReader);
                            sqlDataTableReaderPropertyWriter.WriteProperties();
                        }
                    }
                }
            }            
        }

        private void HandleTestOrderAliquotOrder(DataTable dataTable)
        {
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                YellowstonePathology.Business.Test.AliquotOrder_Base aliquotOrder = null;
                foreach(Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
                {
                    foreach(Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                    {
                        foreach(Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                        {
                            if(testOrder.AliquotOrder == null)
                            {
                                aliquotOrder = new Test.AliquotOrder_Base();
                            }
                            else
                            {
                                aliquotOrder = testOrder.AliquotOrder;
                            }

                            YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(aliquotOrder, dataTableReader);
                            sqlDataTableReaderPropertyWriter.WriteProperties();
                        }
                    }
                }
                
            }
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
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
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
            foreach (Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
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
            foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                if (panelSetOrder is Test.LLP.PanelSetOrderLeukemiaLymphoma)
                {
                    Test.LLP.PanelSetOrderLeukemiaLymphoma llpPanelSetOrder = (Test.LLP.PanelSetOrderLeukemiaLymphoma)panelSetOrder;
                    llpPanelSetOrder.FlowMarkerCollection.Sync(dataTable, llpPanelSetOrder.ReportNo);
                }
            }
        }       
    }
}