using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.InformalConsult
{
    public class InformalConsultWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public InformalConsultWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\InformalConsult.xml";
            base.OpenTemplate();

            base.SetDemographicsV2();

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(this.m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

            InformalConsultTestOrder panelSetOrder = (InformalConsultTestOrder)this.m_PanelSetOrder;

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false) base.ReplaceText("report_comment", panelSetOrder.Comment);
            else base.DeleteRow("report_comment");

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);

            this.SetReportDistribution();
            this.SetCaseHistory();

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
