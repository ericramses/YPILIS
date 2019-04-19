using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2
{
    public class ThrombocytopeniaProfileV2WordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public ThrombocytopeniaProfileV2WordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ThombocytopeniaProfileV2.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            ThrombocytopeniaProfileV2TestOrder testOrder = (ThrombocytopeniaProfileV2TestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(testOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
            amendment.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            /*this.SetXmlNodeData("hbf_percent", testOrder.HbFPercent);
            this.SetXmlNodeData("hbfreference_range", testOrder.HbFReferenceRange);
            this.SetXmlNodeData("fetal_maternal_bleed", testOrder.FetalBleed);
            this.SetXmlNodeData("reference_range", testOrder.FetalBleedReferenceRange);
            this.SetXmlNodeData("rh_immune_globulin", testOrder.RhImmuneGlobulin);
            this.SetXmlNodeData("test_sensitivity", "Sensitivity for Hb-F is " + testOrder.HbFPercent);
            this.SetXmlNodeData("report_comment", testOrder.ReportComment);
            this.SetXmlNodeData("asr_comment", testOrder.ASRComment);
            */
            this.SaveReport();
        }
    }
}
