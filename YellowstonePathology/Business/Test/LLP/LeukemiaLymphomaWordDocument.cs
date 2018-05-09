using System;
using System.Xml;

namespace YellowstonePathology.Business.Test.LLP
{
	public class LeukemiaLymphomaWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public LeukemiaLymphomaWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (PanelSetOrderLeukemiaLymphoma)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\LeukemiaLymphoma.12.xml";

			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
			this.ReplaceText("specimen_description", specimenOrder.Description);

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

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


            if (string.IsNullOrEmpty(panelSetOrderLeukemiaLymphoma.SpecimenViabilityPercent) == true || panelSetOrderLeukemiaLymphoma.SpecimenViabilityPercent == "0")
            {
                this.SetXmlNodeData("llp_specimen_viability", "");
                this.SetXmlNodeData("llp_specimen_viability_percent", "");
            }
            else
            {
                this.SetXmlNodeData("llp_specimen_viability", "Specimen Viability Percent");
                this.SetXmlNodeData("llp_specimen_viability_percent", panelSetOrderLeukemiaLymphoma.SpecimenViabilityPercent);
            }

            this.HandleMarkers(panelSetOrderLeukemiaLymphoma);
			this.SetXmlNodeData("clinical_history", this.m_AccessionOrder.ClinicalHistory);
			this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            this.SetXMLNodeParagraphData("additional_testing", this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(this.m_PanelSetOrder.ReportNo));

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

			this.SaveReport(false);
		}

        private void HandleMarkers(PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma)
        {            
            panelSetOrderLeukemiaLymphoma.FlowMarkerCollection.SetFirstMarkerPanelIfExists();

            XmlNode tableNode1 = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='marker_name1']", this.m_NameSpaceManager);
            XmlNode MarkerNode1 = tableNode1.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='marker_name1']", this.m_NameSpaceManager);
            XmlNode cellPopulationNode = tableNode1.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='cell_population_of_interest']", this.m_NameSpaceManager);
            XmlNode blankRowNode1 = tableNode1.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='blank_row']", this.m_NameSpaceManager);

            XmlNode tableNode2 = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='marker_name2']", this.m_NameSpaceManager);
            XmlNode MarkerNode2 = tableNode2.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='marker_name2']", this.m_NameSpaceManager);
            XmlNode blankRowNode2 = tableNode2.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='blank_row']", this.m_NameSpaceManager);

            foreach (Business.Flow.CellPopulationOfInterest cellPopulationOfInterest in panelSetOrderLeukemiaLymphoma.FlowMarkerCollection.CellPopulationsOfInterest)
            {
                Business.Flow.FlowMarkerCollection flowMarkerCollection = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection.GetMarkerPanel(cellPopulationOfInterest.Id);
                
                XmlNode nodeToInsertAfter1 = MarkerNode1;

                int rowCount = flowMarkerCollection.Count;
                int FirstHalfCount = rowCount / 2;
                int LastHalfCount = rowCount - FirstHalfCount;

                XmlNode newCellPopulationNode = cellPopulationNode.Clone();
                newCellPopulationNode.SelectSingleNode("descendant::w:r[w:t='cell_population_of_interest']/w:t", this.m_NameSpaceManager).InnerText = cellPopulationOfInterest.Description;
                tableNode1.InsertAfter(newCellPopulationNode, nodeToInsertAfter1);
                nodeToInsertAfter1 = newCellPopulationNode;

                for (int i = 0; i < FirstHalfCount; i++)
                {
                    XmlNode NewMarkerNode1 = MarkerNode1.Clone();
                    NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_name1']/w:t", this.m_NameSpaceManager).InnerText = flowMarkerCollection[i].Name;
                    NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_interpretation1']/w:t", this.m_NameSpaceManager).InnerText = flowMarkerCollection[i].Interpretation;
                    NewMarkerNode1.SelectSingleNode("descendant::w:r[w:t='marker_intensity1']/w:t", this.m_NameSpaceManager).InnerText = flowMarkerCollection[i].Intensity;
                    tableNode1.InsertAfter(NewMarkerNode1, nodeToInsertAfter1);
                    nodeToInsertAfter1 = NewMarkerNode1;
                }

                XmlNode newBlankRowNode3 = blankRowNode1.Clone();
                newBlankRowNode3.SelectSingleNode("descendant::w:r[w:t='blank_row']/w:t", this.m_NameSpaceManager).InnerText = null;
                tableNode1.InsertAfter(newBlankRowNode3, nodeToInsertAfter1);                

                XmlNode nodeToInsertAfter2 = MarkerNode2;

                XmlNode newBlankRowNode2 = blankRowNode2.Clone();
                newBlankRowNode2.SelectSingleNode("descendant::w:r[w:t='blank_row']/w:t", this.m_NameSpaceManager).InnerText = null;
                tableNode2.InsertAfter(newBlankRowNode2, nodeToInsertAfter2);
                nodeToInsertAfter2 = newBlankRowNode2;

                for (int i = FirstHalfCount; i < rowCount; i++)
                {
                    XmlNode NewMarkerNode2 = MarkerNode2.Clone();
                    NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_name2']/w:t", this.m_NameSpaceManager).InnerText = flowMarkerCollection[i].Name;
                    NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_interpretation2']/w:t", this.m_NameSpaceManager).InnerText = flowMarkerCollection[i].Interpretation;
                    NewMarkerNode2.SelectSingleNode("descendant::w:r[w:t='marker_intensity2']/w:t", this.m_NameSpaceManager).InnerText = flowMarkerCollection[i].Intensity;
                    tableNode2.InsertAfter(NewMarkerNode2, nodeToInsertAfter2);
                    nodeToInsertAfter2 = NewMarkerNode2;
                }                

                XmlNode newBlankRowNode4 = blankRowNode2.Clone();
                newBlankRowNode4.SelectSingleNode("descendant::w:r[w:t='blank_row']/w:t", this.m_NameSpaceManager).InnerText = null;
                tableNode2.InsertAfter(newBlankRowNode4, nodeToInsertAfter2);
                nodeToInsertAfter2 = newBlankRowNode4;
            }

            tableNode1.RemoveChild(cellPopulationNode);
            tableNode1.RemoveChild(MarkerNode1);
            tableNode1.RemoveChild(blankRowNode1);

            tableNode2.RemoveChild(blankRowNode2);
            tableNode2.RemoveChild(MarkerNode2);            
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

		public override void Publish(bool notify)
		{
			base.Publish(notify);
		}
	}
}
