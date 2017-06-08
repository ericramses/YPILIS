using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HematopathologySummary
{
    public class HematopathologySummaryWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public HematopathologySummaryWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            HematopathologySummaryTestOrder panelSetOrderHematopathologySummary = (HematopathologySummaryTestOrder)this.m_PanelSetOrder;
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HematopathologySummary.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();            

            //YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            //amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            string reportResult = panelSetOrderHematopathologySummary.Result;
            if (string.IsNullOrEmpty(reportResult))
            {
                reportResult = string.Empty;
            }
                        
            this.ReplaceText("summary_interpretation", panelSetOrderHematopathologySummary.Interpretation);

            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            XmlNode testTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='test_name']", this.m_NameSpaceManager);            
            XmlNode rowTestNode = testTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='test_name']", this.m_NameSpaceManager);

            List<Business.Test.PanelSetOrder> testingSummaryList = this.GetTestingSummaryList(this.m_AccessionOrder.PanelSetOrderCollection);
            foreach (Business.Test.PanelSetOrder pso in testingSummaryList)
            {
                if(pso.PanelSetId != 197 && pso.PanelSetId != 268)
                {
                    XmlNode rowTestNodeClone = rowTestNode.Clone();
                    rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = pso.PanelSetName;
                    rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_report_no']/w:t", this.m_NameSpaceManager).InnerText = pso.ReportNo;

                    this.SetXMLNodeParagraphDataNode(rowTestNodeClone, "test_result", pso.ToResultString(this.m_AccessionOrder));
                    //rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_result']/w:t", this.m_NameSpaceManager).InnerText = pso.ToResultString(this.m_AccessionOrder);

                    testTableNode.InsertAfter(rowTestNodeClone, rowTestNode);                    
                }                
            }

            testTableNode.RemoveChild(rowTestNode);
            this.ReplaceText("disclosure_statement", string.Empty);

            this.SaveReport();
        }

        private List<Business.Test.PanelSetOrder> GetTestingSummaryList(Business.Test.PanelSetOrderCollection panelSetOrderCollection)
        {
            List<Business.Test.PanelSetOrder> result = new List<PanelSetOrder>();            

            List<int> exclusionList = new List<int>();
            exclusionList.Add(13);
            exclusionList.Add(20);
            exclusionList.Add(145);
            exclusionList.Add(197);
            exclusionList.Add(262);

            foreach (Business.Test.PanelSetOrder pso in panelSetOrderCollection)
            {
                if(exclusionList.IndexOf(pso.PanelSetId) == -1)
                {
                    result.Add(pso);
                }    
            }

            if (panelSetOrderCollection.Exists(145)) result.Add(panelSetOrderCollection.GetPanelSetOrder(145));
            if (panelSetOrderCollection.Exists(20)) result.Add(panelSetOrderCollection.GetPanelSetOrder(20));            

            return result;
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
