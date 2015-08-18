

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;












namespace YellowstonePathology.Business.Test.LLP
{
/*    public class LeukemiaLymphomaWordDocument : YellowstonePathology.Business.Document.Old.BaseReport, YellowstonePathology.Business.Interface.ICaseDocument
    {        
        const string m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\LeukemiaLymphoma.6.xml";
        private YellowstonePathology.Business.Document.NativeDocumentFormatEnum m_NativeDocumentFormat;

		public LeukemiaLymphomaWordDocument()
        {
			this.m_NativeDocumentFormat = YellowstonePathology.Business.Document.NativeDocumentFormatEnum.Word;
        }

        public YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat
        {
            get { return this.m_NativeDocumentFormat; }
            set { this.m_NativeDocumentFormat = value; }
        }

		public YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
		{
			return YellowstonePathology.Business.Document.CaseDocument.DeleteCaseFiles(orderIdParser); //reportNo);
        }

		public void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
		{
            if (string.IsNullOrEmpty(masterAccessionNo) == true) throw new Exception("The master accession cannot be 0");

            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_ReportSaveMode = reportSaveMode;
			this.m_ReportNo = reportNo;
			base.OpenTemplate(m_TemplateName);

			this.m_SqlStatements.Add("select ao.*, su.signature, su.initials, pso.FinalDate, pso.FinalTime, pso.Final " +
				"from tblAccessionOrder ao " +
				"join tblPanelSetOrder pso on pso.MasterAccessionNo = ao.MasterAccessionNo " +
				"join tblSystemUser su on pso.AssignedToId = su.userid " +
				"where ReportNo = '" + reportNo + "'");            

			this.m_SqlStatements.Add("select * from tblFlowLeukemia where ReportNo = '" + this.m_ReportNo + "'");
			this.m_SqlStatements.Add("select fm.* " +
				"from tblFlowMarkers fm " +
				"join tblMarkers m on fm.Name = m.MarkerName " +
				"where ReportNo = '" + this.m_ReportNo + "' order by m.OrderFlag, m.MarkerName ");

			this.m_SqlStatements.Add("select top 4 * from tblFlowMarkers where ReportNo = '" + this.m_ReportNo + "' and markerused = 1");

			this.m_SqlStatements.Add("Select * from tblAmendment where ReportNo = '" + this.m_ReportNo + "'");

			this.m_SqlStatements.Add("Select so.* from tblSpecimenOrder so join tblPanelSetOrder pso on so.MasterAccessionNo = pso.MasterAccessionNo " +
				"where pso.ReportNo = '" + reportNo + "'");


            this.m_SqlStatements.Add("Select * from tblPanelSetOrder where ReportNo = '" + reportNo + "'");

			this.m_TableNames.Add("tblAccessionOrder");            
            this.m_TableNames.Add("tblFlowLeukemia");
            this.m_TableNames.Add("tblFlowMarkers");
            this.m_TableNames.Add("tblPredictiveMarkers");
            this.m_TableNames.Add("tblAmendment");
			this.m_TableNames.Add("tblSpecimenOrder");
            this.m_TableNames.Add("tblPanelSetOrder");

            this.GetDataSet();

            string collectionDate = DateTime.Parse(this.m_ReportData.Tables["tblAccessionOrder"].Rows[0]["collectiondate"].ToString()).ToString("MM/dd/yyyy");
            this.ReplaceText("date_of_service", collectionDate);

            this.SetDemographicsV2("tblAccessionOrder");            

            this.SetReportDistribution();
            this.SetCaseHistory();


            if (this.m_ReportData.Tables["tblAmendment"].Rows.Count != 0)
            {
                string amendmentTitle = this.m_ReportData.Tables["tblAmendment"].Rows[0]["AmendmentType"].ToString();
                if (amendmentTitle == "Correction") amendmentTitle = "Corrected";

                if (this.m_ReportData.Tables["tblAmendment"].Rows[0]["RevisedDiagnosis"].ToString() == "True")
                {
                    this.SetXmlNodeData("amendment_title", "Revised");                    
                }
                else
                {
                    this.SetXmlNodeData("amendment_title", amendmentTitle);
                }
            }
            else
            {
                this.SetXmlNodeData("amendment_title", "");
            }

			YellowstonePathology.Business.Document.Old.Amendment amendment = new YellowstonePathology.Business.Document.Old.Amendment();
            amendment.SetAmendment(this.m_ReportData.Tables["tblAmendment"], this.m_ReportXml, this.m_NameSpaceManager);
                       
           DataRow rowLeukemia = this.m_ReportData.Tables["tblFlowLeukemia"].Rows[0];
           DataRow rowPanelSetOrder = this.m_ReportData.Tables[6].Rows[0];
           DataRow rowFlow = this.m_ReportData.Tables["tblAccessionOrder"].Rows[0];
           
           DataRow rowSpecimen = null;
           foreach (DataRow dr in this.m_ReportData.Tables["tblSpecimenOrder"].Rows)
           {
               if (dr["SpecimenOrderId"].ToString() == rowPanelSetOrder["OrderedOnId"].ToString())
               {
                   rowSpecimen = dr;
               }
           }

		   DateTime date;
			string dateStr = string.Empty;
		   bool result = DateTime.TryParse(rowFlow["finaldate"].ToString(), out date);
		   if (result == true) dateStr = date.ToShortDateString();
           string finalDate = "Final Date: " + dateStr;
           this.SetXmlNodeData("final_date", finalDate);            

           DateTime accessionTimeDate;
           bool accessionTimeResult = DateTime.TryParse(rowFlow["accessiontime"].ToString(), out accessionTimeDate);
           if (accessionTimeResult == true)
           {
               this.SetXmlNodeData("accession_time", accessionTimeDate.ToString("HH:mm"));
           }
           
           XmlNode tableNodeDelete = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_impression']", this.m_NameSpaceManager);
           XmlNode tableNodeDelete2 = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='lymphocyte_%']", this.m_NameSpaceManager);

           XmlNode deleteNode3;
		   if (rowFlow["Final"].ToString() == "True" && rowLeukemia["TestCancelled"].ToString() == "False")
           {
               deleteNode3 = tableNodeDelete.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='pathologist_signature']", this.m_NameSpaceManager);
               string signature = rowFlow["signature"].ToString();
               this.SetXmlNodeData("pathologist_signature", rowFlow["Signature"].ToString());
           }
           else if (rowFlow["Final"].ToString() == "False")
           {
               deleteNode3 = tableNodeDelete.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='This Case Is Not Final']", this.m_NameSpaceManager);
               string signature = rowFlow["signature"].ToString();
               this.SetXmlNodeData("pathologist_signature", "This Case Is Not Final");
           }
		   
            string reportImpression = rowLeukemia["Impression"].ToString();               
            this.SetXMLNodeParagraphData("report_impression", reportImpression);

            string interpretiveComment = rowLeukemia["InterpretiveComment"].ToString();
            this.SetXmlNodeData("interpretive_comment", interpretiveComment);

            string population = rowLeukemia["CellPopulationOfInterest"].ToString();
            this.SetXmlNodeData("cell_population_of_interest", population);

            double LymphCnt = Convert.ToDouble(rowLeukemia["LymphocyteCount"].ToString());
            double MonoCnt = Convert.ToDouble(rowLeukemia["MonocyteCount"].ToString());
            double MyeCnt = Convert.ToDouble(rowLeukemia["MyeloidCount"].ToString());
            double DimCnt = Convert.ToDouble(rowLeukemia["DimCD45ModSSCount"].ToString());
            double OtherCnt = Convert.ToDouble(rowLeukemia["OtherCount"].ToString());

            string blastCD34Percent = rowLeukemia["EGateCD34Percent"].ToString();
            string blastCD117Percent = rowLeukemia["EGateCD117Percent"].ToString();

            double TotalCnt = LymphCnt + MonoCnt + MyeCnt + DimCnt;
            double LymphPcnt = LymphCnt / TotalCnt;
            double MonoPcnt = MonoCnt / TotalCnt;
            double MyePcnt = MyeCnt / TotalCnt;
            double DimPcnt = DimCnt / TotalCnt;
            double OtherPcnt = OtherCnt / TotalCnt;

            this.SetXmlNodeData("lymphocyte_%", this.GetGatingPercent(LymphPcnt));
            this.SetXmlNodeData("monocyte_%", this.GetGatingPercent(MonoPcnt));
            this.SetXmlNodeData("myeloid_%", this.GetGatingPercent(MyePcnt));
            this.SetXmlNodeData("dimcd45modss_%", this.GetGatingPercent(DimPcnt));               

            if (string.IsNullOrEmpty(blastCD34Percent) == true && string.IsNullOrEmpty(blastCD117Percent) == true)
            {
                this.SetXmlNodeData("CD_34", "");
                this.SetXmlNodeData("CD_117", "");
                this.SetXmlNodeData("Blast_Header", "");
                this.SetXmlNodeData("blast_cd34_percent", "");
                this.SetXmlNodeData("blast_cd117_percent", "");
            }
            else
            {
                this.SetXmlNodeData("CD_34", "CD34");
                this.SetXmlNodeData("CD_117", "CD117");
                this.SetXmlNodeData("Blast_Header", "Blast Marker Percentages (as % of nucleated cells):");
                this.SetXmlNodeData("blast_cd34_percent", blastCD34Percent);
                this.SetXmlNodeData("blast_cd117_percent", blastCD117Percent);
            }

            XmlNode tableNode1 = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='marker_name1']", this.m_NameSpaceManager);
            XmlNode MarkerNode1 = tableNode1.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='marker_name1']", this.m_NameSpaceManager);
            XmlNode nodeToInsertAfter1 = MarkerNode1;

            int rowCount = this.m_ReportData.Tables["tblFlowMarkers"].Rows.Count;
            int FirstHalfCount = rowCount / 2;
            int LastHalfCount = rowCount - FirstHalfCount;

            for (int i = 0; i < FirstHalfCount; i++)
            {
                DataRow dr = this.m_ReportData.Tables["tblFlowMarkers"].Rows[i];
                string marker = dr["name"].ToString();
                string interpretation = dr["interpretation"].ToString();
                string intensity = dr["intensity"].ToString();

                XmlNode NewMarkerNode1 = MarkerNode1.Clone();
                NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_name1']/w:t", this.m_NameSpaceManager).InnerText = marker;
                NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_interpretation1']/w:t", this.m_NameSpaceManager).InnerText = interpretation;
                NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_intensity1']/w:t", this.m_NameSpaceManager).InnerText = intensity;
                tableNode1.InsertAfter(NewMarkerNode1, nodeToInsertAfter1);
                nodeToInsertAfter1 = NewMarkerNode1;
            }
            tableNode1.RemoveChild(MarkerNode1);

            XmlNode tableNode2 = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='marker_name2']", this.m_NameSpaceManager);
            XmlNode MarkerNode2 = tableNode2.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='marker_name2']", this.m_NameSpaceManager);
            XmlNode nodeToInsertAfter2 = MarkerNode2;

            for (int i = FirstHalfCount; i < rowCount; i++)
            {
                DataRow dr = this.m_ReportData.Tables["tblFlowMarkers"].Rows[i];
                string marker = dr["name"].ToString();
                string interpretation = dr["Interpretation"].ToString();
                string intensity = dr["intensity"].ToString();

                XmlNode NewMarkerNode2 = MarkerNode2.Clone();
                NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_name2']/w:t", this.m_NameSpaceManager).InnerText = marker;
                NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_interpretation2']/w:t", this.m_NameSpaceManager).InnerText = interpretation;
                NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_intensity2']/w:t", this.m_NameSpaceManager).InnerText = intensity;
                tableNode2.InsertAfter(NewMarkerNode2, nodeToInsertAfter2);
                nodeToInsertAfter2 = NewMarkerNode2;
            }
            tableNode2.RemoveChild(MarkerNode2);           

            string ClinicalHistory = rowFlow["ClinicalHistory"].ToString();
            this.SetXmlNodeData("clinical_history", ClinicalHistory);

            string specimenDescription = rowSpecimen["Description"].ToString();

            string dateTimeCollected = string.Empty;

            if (string.IsNullOrEmpty(rowSpecimen["CollectionTime"].ToString()) == false)
            {
                dateTimeCollected = DateTime.Parse(rowSpecimen["CollectionTime"].ToString()).ToString("MM/dd/yyyy HH:mm");
            }
            else
            {
                dateTimeCollected = DateTime.Parse(rowSpecimen["CollectionDate"].ToString()).ToString("MM/dd/yyyy");
            }

            this.SetXmlNodeData("specimen_description", specimenDescription);
            this.SetXmlNodeData("date_time_collected", dateTimeCollected);

			string SpecimenAdequacy = rowSpecimen["SpecimenAdequacy"].ToString();
            this.SetXmlNodeData("specimen_adequacy", SpecimenAdequacy);

            this.SetXmlNodeData("location_performed", "Technical and professional component(s) performed at Yellowstone Pathology Institute Billings, 2900 12th Avenue North, Suite 295W, Billings, MT 59101 (CLIA:27D0946844).");

            this.SaveReport();
        }

        public void Publish()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_ReportNo);
			YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(orderIdParser);
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_ReportNo);
        }

        public string GetGatingPercent(double gatingPercent)
        {
            string result = string.Empty;
            switch (gatingPercent.ToString())
            {
                case "":
                case "0":
                    result = "< 1%";
                    break;
                case "1":
                    result = "~100%";
                    break;
                default:
                    result = gatingPercent.ToString("###.##%");
                    break;
            }
            if (result == "NaN")
            {
                result = "0";
            }
            return result;
        }
    }
*/
	public class LeukemiaLymphomaWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
			this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;

			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportNo);

			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

			PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (PanelSetOrderLeukemiaLymphoma)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\LeukemiaLymphoma.7.xml";

			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
			this.ReplaceText("specimen_description", specimenOrder.Description);

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			if (m_PanelSetOrder.AmendmentCollection.Count > 0)
			{
				string amendmentTitle = m_PanelSetOrder.AmendmentCollection[0].AmendmentType;
				if (amendmentTitle == "Correction") amendmentTitle = "Corrected";

				if (m_PanelSetOrder.AmendmentCollection[0].RevisedDiagnosis == true)
				{
					this.SetXmlNodeData("amendment_title", "Revised");
				}
				else
				{
					this.SetXmlNodeData("amendment_title", amendmentTitle);
				}
			}
			else
			{
				this.SetXmlNodeData("amendment_title", "");
			}

			XmlNode tableNodeDelete = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_impression']", this.m_NameSpaceManager);
			XmlNode tableNodeDelete2 = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='lymphocyte_%']", this.m_NameSpaceManager);

			string signature = string.Empty;
			if (this.m_PanelSetOrder.Final == true && this.m_PanelSetOrder.PanelSetId != 66)
			{
				signature = this.m_PanelSetOrder.Signature;
			}
			else if (this.m_PanelSetOrder.Final == false)
			{
				signature = "This Case Is Not Final";
			}
			this.SetXmlNodeData("pathologist_signature", signature);

			this.SetXMLNodeParagraphData("report_impression", panelSetOrderLeukemiaLymphoma.Impression);
			this.SetXmlNodeData("interpretive_comment", panelSetOrderLeukemiaLymphoma.InterpretiveComment);
			this.SetXmlNodeData("cell_population_of_interest", panelSetOrderLeukemiaLymphoma.CellPopulationOfInterest);

			double LymphCnt = Convert.ToDouble(panelSetOrderLeukemiaLymphoma.LymphocyteCount);
			double MonoCnt = Convert.ToDouble(panelSetOrderLeukemiaLymphoma.MonocyteCount);
			double MyeCnt = Convert.ToDouble(panelSetOrderLeukemiaLymphoma.MyeloidCount);
			double DimCnt = Convert.ToDouble(panelSetOrderLeukemiaLymphoma.DimCD45ModSSCount);
			double OtherCnt = Convert.ToDouble(panelSetOrderLeukemiaLymphoma.OtherCount);

			double TotalCnt = LymphCnt + MonoCnt + MyeCnt + DimCnt;
			double LymphPcnt = LymphCnt / TotalCnt;
			double MonoPcnt = MonoCnt / TotalCnt;
			double MyePcnt = MyeCnt / TotalCnt;
			double DimPcnt = DimCnt / TotalCnt;
			double OtherPcnt = OtherCnt / TotalCnt;

			this.SetXmlNodeData("lymphocyte_%", this.GetGatingPercent(LymphPcnt));
			this.SetXmlNodeData("monocyte_%", this.GetGatingPercent(MonoPcnt));
			this.SetXmlNodeData("myeloid_%", this.GetGatingPercent(MyePcnt));
			this.SetXmlNodeData("dimcd45modss_%", this.GetGatingPercent(DimPcnt));

			if (string.IsNullOrEmpty(panelSetOrderLeukemiaLymphoma.EGateCD34Percent) == true && string.IsNullOrEmpty(panelSetOrderLeukemiaLymphoma.EGateCD117Percent) == true)
			{
				this.SetXmlNodeData("CD_34", "");
				this.SetXmlNodeData("CD_117", "");
				this.SetXmlNodeData("Blast_Header", "");
				this.SetXmlNodeData("blast_cd34_percent", "");
				this.SetXmlNodeData("blast_cd117_percent", "");
			}
			else
			{
				this.SetXmlNodeData("CD_34", "CD34");
				this.SetXmlNodeData("CD_117", "CD117");
				this.SetXmlNodeData("Blast_Header", "Blast Marker Percentages (as % of nucleated cells):");
				this.SetXmlNodeData("blast_cd34_percent", panelSetOrderLeukemiaLymphoma.EGateCD34Percent);
				this.SetXmlNodeData("blast_cd117_percent", panelSetOrderLeukemiaLymphoma.EGateCD117Percent);
			}

			XmlNode tableNode1 = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='marker_name1']", this.m_NameSpaceManager);
			XmlNode MarkerNode1 = tableNode1.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='marker_name1']", this.m_NameSpaceManager);
			XmlNode nodeToInsertAfter1 = MarkerNode1;

			int rowCount = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Count;
			int FirstHalfCount = rowCount / 2;
			int LastHalfCount = rowCount - FirstHalfCount;

			for (int i = 0; i < FirstHalfCount; i++)
			{
				XmlNode NewMarkerNode1 = MarkerNode1.Clone();
				NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_name1']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection[i].Name;
				NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_interpretation1']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection[i].Interpretation;
				NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_intensity1']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection[i].Intensity;
				tableNode1.InsertAfter(NewMarkerNode1, nodeToInsertAfter1);
				nodeToInsertAfter1 = NewMarkerNode1;
			}
			tableNode1.RemoveChild(MarkerNode1);

			XmlNode tableNode2 = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='marker_name2']", this.m_NameSpaceManager);
			XmlNode MarkerNode2 = tableNode2.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='marker_name2']", this.m_NameSpaceManager);
			XmlNode nodeToInsertAfter2 = MarkerNode2;

			for (int i = FirstHalfCount; i < rowCount; i++)
			{
				XmlNode NewMarkerNode2 = MarkerNode2.Clone();
				NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_name2']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection[i].Name;
				NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_interpretation2']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection[i].Interpretation;
				NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_intensity2']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection[i].Intensity;
				tableNode2.InsertAfter(NewMarkerNode2, nodeToInsertAfter2);
				nodeToInsertAfter2 = NewMarkerNode2;
			}
			tableNode2.RemoveChild(MarkerNode2);

			this.SetXmlNodeData("clinical_history", this.m_AccessionOrder.ClinicalHistory);
			this.SetXmlNodeData("specimen_description", specimenOrder.Description);

			string dateTimeCollected = string.Empty;
			if (specimenOrder.CollectionTime.HasValue == true)
			{
				dateTimeCollected = specimenOrder.CollectionTime.Value.ToString("MM/dd/yyyy HH:mm");
			}
			else if (specimenOrder.CollectionDate.HasValue == true)
			{
				dateTimeCollected = specimenOrder.CollectionDate.Value.ToString("MM/dd/yyyy");
			}
			this.SetXmlNodeData("date_time_collected", dateTimeCollected);

			this.SetXmlNodeData("specimen_adequacy", specimenOrder.SpecimenAdequacy);
			this.SetXmlNodeData("location_performed", "Technical and professional component(s) performed at Yellowstone Pathology Institute Billings, 2900 12th Avenue North, Suite 295W, Billings, MT 59101 (CLIA:27D0946844).");

			this.SaveReport();
		}

		public string GetGatingPercent(double gatingPercent)
		{
			string result = string.Empty;
			switch (gatingPercent.ToString())
			{
				case "":
				case "0":
					result = "< 1%";
					break;
				case "1":
					result = "~100%";
					break;
				default:
					result = gatingPercent.ToString("###.##%");
					break;
			}
			if (result == "NaN")
			{
				result = "0";
			}
			return result;
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
