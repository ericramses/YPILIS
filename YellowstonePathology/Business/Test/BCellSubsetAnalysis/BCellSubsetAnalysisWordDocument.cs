using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BCellSubsetAnalysis
{
    public class BCellSubsetAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public BCellSubsetAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            BCellSubsetAnalysisTestOrder testOrder = (BCellSubsetAnalysisTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\BCellSubsetAnalysis.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("report_mature", testOrder.MatureBCellsPlusPercent);
            this.ReplaceText("report_mm", testOrder.MatureBCellsMinusPercent);
            this.ReplaceText("report_memory", testOrder.MemoryBCellPercent);
            this.ReplaceText("report_nonswitched", testOrder.NonSwitchedMemoryBCellPercent);
            this.ReplaceText("report_marginal_zone", testOrder.MarginalZoneBCellPercent);
            this.ReplaceText("report_class_switched", testOrder.ClassSwitchedMemoryBCellPercent);
            this.ReplaceText("report_naive", testOrder.NaiveBCellPercent);
            this.ReplaceText("report_transitional", testOrder.TransitionalBCellPercent);
            this.ReplaceText("report_plasmablasts", testOrder.PlasmaBlastsPercent);
            this.ReplaceText("report_cd24_mfi", testOrder.MFIPercent);
            this.ReplaceText("report_total_nucleated", testOrder.TotalNucleatedPercent);
            this.ReplaceText("report_total_lymphocytes", testOrder.TotalLymphocytesPercent);

            this.ReplaceText("report_interpretation", testOrder.Interpretation);
            this.ReplaceText("report_method", testOrder.Method);
            this.ReplaceText("report_references", testOrder.ReportReferences);
            this.ReplaceText("asr_comment", testOrder.ASRComment);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
