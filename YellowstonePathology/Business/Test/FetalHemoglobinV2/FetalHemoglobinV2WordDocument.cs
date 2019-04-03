using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.FetalHemoglobinV2
{
    public class FetalHemoglobinV2WordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        //YellowstonePathology.Business.Flow.FlowMarkerPanelList m_PanelList;

        public FetalHemoglobinV2WordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\FetalHemoglobinV2.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Test.FetalHemoglobinV2.FetalHemoglobinV2TestOrder testOrder = (FetalHemoglobinV2TestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
            amendment.SetAmendment(testOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXmlNodeData("test_result", testOrder.HbFResult);
            this.SetXmlNodeData("reference_range", testOrder.ReferenceRange);
            this.SetXmlNodeData("test_sensitivity", "Sensitivity for Hb-F is " + testOrder.HbFResult);
            this.SetXmlNodeData("report_comment", testOrder.ReportComment);
            this.SetXmlNodeData("asr_comment", testOrder.ASRComment);

            this.SaveReport();
        }
    }
}
