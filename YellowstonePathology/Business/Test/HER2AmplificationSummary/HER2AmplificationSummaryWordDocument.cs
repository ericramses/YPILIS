using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationSummaryWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public HER2AmplificationSummaryWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            HER2AmplificationSummaryTestOrder her2AmplificationSummaryTestOrder = (HER2AmplificationSummaryTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationSummary.xml";

            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);

            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

            this.SetXmlNodeData("report_interpretation", her2AmplificationSummaryTestOrder.Interpretation);

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            SetXmlNodeData("specimen_type", specimenOrder.Description + blockDescription);
            SetXmlNodeData("specimen_fixation", specimenOrder.LabFixation);
            SetXmlNodeData("time_to_fixation", specimenOrder.TimeToFixationHourString);

            SetXmlNodeData("date_time_collected", collectionDateTimeString);
            SetXmlNodeData("report_distribution", "No Distribution Selected");

            this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
