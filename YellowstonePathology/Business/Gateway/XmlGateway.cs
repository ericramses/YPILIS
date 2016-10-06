using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class XmlGateway
    {
        public static XElement GetSpecimenOrder(string masterAccessionNo)
        {
            XElement result = new XElement("SpecimenOrders");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblSpecimenOrder where MasterAccessionno = '" + masterAccessionNo + "' order by SpecimenNumber";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
                        XElement specimenOrderElement = new XElement("SpecimenOrder");
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
						sqlDataReaderPropertyWriter.WriteProperties();

						YellowstonePathology.Business.Persistence.XmlPropertyReader xmlPropertyReader = new Persistence.XmlPropertyReader(specimenOrder, specimenOrderElement);
						xmlPropertyReader.Write();
						result.Add(specimenOrderElement);
                    }
                }               
            }

            return result;
        }

		public static XElement GetAccessionOrder(string masterAccessionNo)
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblAccessionOrder where MasterAccessionno = '" + masterAccessionNo + "'";
            cmd.CommandType = CommandType.Text;

            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = new Test.AccessionOrder();
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(accessionOrder, dr);
						propertyWriter.WriteProperties();
                    }
                }
            }

			XElement result = new XElement("AccessionOrder");
			YellowstonePathology.Business.Persistence.XmlPropertyReader xmlPropertyReader = new YellowstonePathology.Business.Persistence.XmlPropertyReader(accessionOrder, result);
			xmlPropertyReader.Write();
            return result;
        }

		public static XElement GetClientOrders(string masterAccessionNo)
		{			
			XElement result = new XElement("ClientOrders");
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(masterAccessionNo);
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in clientOrderCollection)
            {
                XElement clientOrderElement = new XElement("ClientOrder");
                YellowstonePathology.Business.Persistence.XmlPropertyReader clientOrderPropertyWriter = new Persistence.XmlPropertyReader(clientOrder, clientOrderElement);
                clientOrderPropertyWriter.Write();
                result.Add(clientOrderElement);
                
                foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrder.ClientOrderDetailCollection)
                {
                    XElement clientOrderDetailElement = new XElement("ClientOrderDetail");
                    YellowstonePathology.Business.Persistence.XmlPropertyReader clientOrderDetailPropertyWriter = new Persistence.XmlPropertyReader(clientOrderDetail, clientOrderDetailElement);
                    clientOrderDetailPropertyWriter.Write();
                    clientOrderElement.Add(clientOrderDetailElement);
                }
            }            
			return result;
		}

		public static XElement GetOrderComments(string masterAccessionNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select * from tblOrderCommentLog where MasterAccessionNo = @MasterAccessionNo; ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;

			XElement result = new XElement("OrderComments");
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog = new Domain.OrderCommentLog();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						orderCommentLog.WriteProperties(propertyWriter);

						YellowstonePathology.Business.Domain.Persistence.XmlPropertyReader xmlPropertyReader = new Domain.Persistence.XmlPropertyReader();
						xmlPropertyReader.Initialize("OrderComment");
						orderCommentLog.ReadProperties(xmlPropertyReader);

						result.Add(xmlPropertyReader.Document);
					}
				}
			}

			return result;
		}

		public static YellowstonePathology.Document.Result.Data.PlacentalPathologyQuestionnaireData GetPlacentalPathologyQuestionnaire(string clientOrderId, object writer)
		{
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(clientOrderId, writer);
			XElement clientOrderElement = new XElement("ClientOrder");
			YellowstonePathology.Business.Persistence.XmlPropertyReader clientOrderPropertyWriter = new Persistence.XmlPropertyReader(clientOrder, clientOrderElement);
			clientOrderPropertyWriter.Write();

			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrder.ClientOrderDetailCollection)
			{
				if (clientOrderDetail.OrderTypeCode == "PLCNT")
				{
					XElement clientOrderDetailElement = new XElement("ClientOrderDetail");
					YellowstonePathology.Business.Persistence.XmlPropertyReader clientOrderDetailPropertyWriter = new Persistence.XmlPropertyReader(clientOrderDetail, clientOrderDetailElement);
					clientOrderDetailPropertyWriter.Write();
					clientOrderElement.Add(clientOrderDetailElement);
				}
			}
			YellowstonePathology.Document.Result.Data.PlacentalPathologyQuestionnaireData result = new YellowstonePathology.Document.Result.Data.PlacentalPathologyQuestionnaireData(clientOrderElement);
			return result;
		}

		/*public static YellowstonePathology.Business.Domain.XElementFromSql GetXmlOrdersToAcknowledge(string panelOrderIds)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "pGetXmlOrdersToAcknowledge_A1";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@PanelOrderIdString", SqlDbType.VarChar).Value = panelOrderIds;

			XElement fromSqlElement = new XElement("Document");
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (XmlReader xmlReader = cmd.ExecuteXmlReader())
				{
					if (xmlReader.Read() == true)
					{
						fromSqlElement = XElement.Load(xmlReader);
					}
				}
			}
			YellowstonePathology.Business.Domain.XElementFromSql xElementFromSql = new Domain.XElementFromSql();
			xElementFromSql.Document = fromSqlElement;
			return xElementFromSql;
		}*/

        public static YellowstonePathology.Business.Reports.LabOrderSheetData GetOrdersToAcknowledge(string panelOrderIds)
        {
            Reports.LabOrderSheetData result = new Reports.LabOrderSheetData();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "pGetXmlOrdersToAcknowledge_A2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PanelOrderIdString", SqlDbType.VarChar).Value = panelOrderIds;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Reports.LabOrderSheetDataReport labOrderSheetDataReport = new Reports.LabOrderSheetDataReport();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(labOrderSheetDataReport, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(labOrderSheetDataReport);
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Reports.LabOrderSheetDataPanelOrder labOrderSheetDataPanelOrder = new Reports.LabOrderSheetDataPanelOrder();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(labOrderSheetDataPanelOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach(Reports.LabOrderSheetDataReport labOrderSheetDataReport in result)
                        {
                            if (labOrderSheetDataReport.ReportNo == labOrderSheetDataPanelOrder.ReportNo)
                            {
                                labOrderSheetDataReport.LabOrderSheetDataPanelOrders.Add(labOrderSheetDataPanelOrder);
                                break;
                            }
                        }
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Reports.LabOrderSheetDataTestOrder labOrderSheetDataTestOrder = new Reports.LabOrderSheetDataTestOrder();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(labOrderSheetDataTestOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (Reports.LabOrderSheetDataReport labOrderSheetDataReport in result)
                        {
                            bool added = false;
                            foreach(Reports.LabOrderSheetDataPanelOrder labOrderSheetDataPanelOrder in labOrderSheetDataReport.LabOrderSheetDataPanelOrders)
                            {
                                if (labOrderSheetDataPanelOrder.PanelOrderId == labOrderSheetDataTestOrder.PanelOrderId)
                                {
                                    labOrderSheetDataPanelOrder.LabOrderSheetDataTestOrders.Add(labOrderSheetDataTestOrder);
                                    added = true;
                                    break;
                                }
                            }
                            if (added == true) break;
                        }
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Document.Result.Data.AccessionOrderDataSheetData GetAccessionOrderDataSheetData(string masterAccessionNo)
		{
			XElement accessionOrderDocument = XmlGateway.GetAccessionOrder(masterAccessionNo);
			XElement specimenOrderDocument = XmlGateway.GetSpecimenOrder(masterAccessionNo);
			XElement clientOrderDocument = XmlGateway.GetClientOrders(masterAccessionNo);
			XElement caseNotesDocument = XmlGateway.GetOrderComments(masterAccessionNo);

			YellowstonePathology.Document.Result.Data.AccessionOrderDataSheetData accessionOrderDataSheetData = new YellowstonePathology.Document.Result.Data.AccessionOrderDataSheetData(accessionOrderDocument, specimenOrderDocument, clientOrderDocument, caseNotesDocument);
			return accessionOrderDataSheetData;
		}		
        
        /*public static XElement GetClientBillingDetailReport(DateTime postDateStart, DateTime postDateEnd, Nullable<int> clientGroupId)
        {
            XElement result = new XElement("Document");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prcGetClientBillingDetailReport";
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = postDateStart;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = postDateEnd;
            cmd.Parameters.Add("@ClientGroupId", SqlDbType.Int).Value = clientGroupId;                        

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        result = XElement.Load(xmlReader);
                    }
                }
            }

            return result;
        }*/

        public static XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData GetClientBillingDetailReport(DateTime postDateStart, DateTime postDateEnd, Nullable<int> clientGroupId)
        {
            XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData result = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prcGetClientBillingDetailReport_1";
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = postDateStart;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = postDateEnd;
            cmd.Parameters.Add("@ClientGroupId", SqlDbType.Int).Value = clientGroupId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientBillingDetailReportDataAccessionOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientBillingDetailReportDataAccessionOrder);
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientBillingDetailReportDataReport, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach(XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            if (clientBillingDetailReportDataReport.MasterAccessionNo == clientBillingDetailReportDataAccessionOrder.MasterAccessionNo)
                            {
                                clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports.Add(clientBillingDetailReportDataReport);
                                break;
                            }

                        }
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Test.PanelSetOrderCPTCode panelSetOrderCPTCode = new Test.PanelSetOrderCPTCode();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderCPTCode, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            bool added = false;
                            foreach(XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport in clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports)
                            if (panelSetOrderCPTCode.ReportNo == clientBillingDetailReportDataReport.ReportNo)
                            {
                                clientBillingDetailReportDataReport.PanelSetOrderCPTCodes.Add(panelSetOrderCPTCode);
                                added = true;
                                break;
                            }
                            if (added) break;
                        }
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = new Test.PanelSetOrderCPTCodeBill();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderCPTCodeBill, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            bool added = false;
                            foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport in clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports)
                                if (panelSetOrderCPTCodeBill.ReportNo == clientBillingDetailReportDataReport.ReportNo)
                                {
                                    clientBillingDetailReportDataReport.PanelSetOrderCPTCodeBills.Add(panelSetOrderCPTCodeBill);
                                    added = true;
                                    break;
                                }
                            if (added) break;
                        }
                    }
                }
            }

            return result;
        }

        public static XElement GetClientSupplyOrderReportData(string clientSupplyOrderId)
        {
            XElement result = new XElement("Document");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT c.*," +
                " (Select cd.* " +
                "  from tblClientSupplyOrderDetail cd where cd.clientSupplyOrderId = c.clientSupplyOrderId for xml path('ClientSupplyOrderDetail'), type) ClientSupplyOrderDetailCollection" +
                " from tblClientSupplyOrder c where c.ClientSupplyOrderId = @ClientSupplyOrderId for xml path('ClientSupplyOrder')";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientSupplyOrderId", SqlDbType.VarChar).Value = clientSupplyOrderId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        result = XElement.Load(xmlReader);
                    }
                }
            }

            return result;
        }
    }
}
