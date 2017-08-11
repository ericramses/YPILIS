using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class BoneMarrowSummaryWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public BoneMarrowSummaryWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\BoneMarrowSummary.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();            
                        
            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            string surgicalResult = string.Empty;
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            string reportNo = surgicalTestOrder.ReportNo;

            XmlNode surgicalTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='surgical_description']", this.m_NameSpaceManager);
            XmlNode descriptionRowNode = surgicalTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='surgical_description']", this.m_NameSpaceManager);
            XmlNode diagnosisRowNode = surgicalTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='surgical_diagnosis']", this.m_NameSpaceManager);
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                string description = surgicalSpecimen.DiagnosisIdFormatted + "  " +surgicalSpecimen.SpecimenOrder.Description;

                if (surgicalSpecimen.DiagnosisId > 1) reportNo = string.Empty;

                XmlNode descriptionRowClone = descriptionRowNode.Clone();
                descriptionRowClone.SelectSingleNode("descendant::w:r[w:t='surgical_description']/w:t", this.m_NameSpaceManager).InnerText = description;
                descriptionRowClone.SelectSingleNode("descendant::w:r[w:t='surgical_report_no']/w:t", this.m_NameSpaceManager).InnerText = reportNo;

                XmlNode diagnosisRowClone = diagnosisRowNode.Clone();
                this.SetXMLNodeParagraphDataNode(diagnosisRowClone, "surgical_diagnosis", surgicalSpecimen.Diagnosis);

                surgicalTableNode.InsertBefore(descriptionRowClone, descriptionRowNode);
                surgicalTableNode.InsertBefore(diagnosisRowClone, descriptionRowNode);
            }

            surgicalTableNode.RemoveChild(descriptionRowNode);
            surgicalTableNode.RemoveChild(diagnosisRowNode);


            XmlNode testTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='test_name']", this.m_NameSpaceManager);            
            XmlNode rowTestNode = testTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='test_name']", this.m_NameSpaceManager);

            List<Business.Test.PanelSetOrder> testingSummaryList = this.GetTestingSummaryList(this.m_AccessionOrder.PanelSetOrderCollection);
            foreach (Business.Test.PanelSetOrder pso in testingSummaryList)
            {
                XmlNode rowTestNodeClone = rowTestNode.Clone();
                rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = pso.PanelSetName;
                rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_report_no']/w:t", this.m_NameSpaceManager).InnerText = pso.ReportNo;

                this.SetXMLNodeParagraphDataNode(rowTestNodeClone, "test_result", pso.ToResultString(this.m_AccessionOrder));

                testTableNode.InsertAfter(rowTestNodeClone, rowTestNode);                    
            }

            testTableNode.RemoveChild(rowTestNode);
            this.ReplaceText("disclosure_statement", string.Empty);

            this.SaveReport();
        }

        private List<Business.Test.PanelSetOrder> GetTestingSummaryList(Business.Test.PanelSetOrderCollection panelSetOrderCollection)
        {
            List<Business.Test.PanelSetOrder> result = new List<PanelSetOrder>();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();

            Business.Test.PanelSetOrderCollection flow = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection cyto = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection fish = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection molecular = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection other = new PanelSetOrderCollection();

            List<int> exclusionList = new List<int>();
            exclusionList.Add(13);
            exclusionList.Add(197);
            exclusionList.Add(262);
            exclusionList.Add(268);

            foreach (Business.Test.PanelSetOrder pso in panelSetOrderCollection)
            {
                if(exclusionList.IndexOf(pso.PanelSetId) == -1)
                {
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSets.GetPanelSet(pso.PanelSetId);
                    if (panelSet.CaseType == YellowstonePathology.Business.CaseType.FlowCytometry) flow.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.Cytogenetics) cyto.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.FISH) fish.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.Molecular) molecular.Insert(0, pso);
                    else other.Insert(0, pso);
                }
            }

            result.AddRange(other);
            result.AddRange(molecular);
            result.AddRange(fish);
            result.AddRange(cyto);
            result.AddRange(flow);
            return result;
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
