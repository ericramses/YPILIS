using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class RASRAFPanelWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
        {
            this.m_ReportNo = reportNo;
            this.m_ReportSaveEnum = reportSaveEnum;
            this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            RASRAFPanelTestOrder testOrder = (RASRAFPanelTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\RASRAFPanel.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("braf_result", testOrder.BRAFResult);
            this.ReplaceText("kras_result", testOrder.KRASResult);
            this.ReplaceText("nras_result", testOrder.NRASResult);
            this.ReplaceText("hras_result", testOrder.HRASResult);
            
            this.ReplaceText("braf_mn", testOrder.BRAFMutationName);
            this.ReplaceText("kras_mn", testOrder.KRASMutationName);
            this.ReplaceText("nras_mn", testOrder.NRASMutationName);
            this.ReplaceText("hras_mn", testOrder.HRASMutationName);
            
            this.ReplaceText("braf_anmn", testOrder.BRAFAlternateNucleotideMutationName);
            this.ReplaceText("kras_anmn", testOrder.KRASAlternateNucleotideMutationName);
            this.ReplaceText("nras_anmn", testOrder.NRASAlternateNucleotideMutationName);
            this.ReplaceText("hras_anmn", testOrder.HRASAlternateNucleotideMutationName);
            
            this.ReplaceText("braf_cons", testOrder.BRAFConsequence);
            this.ReplaceText("kras_cons", testOrder.KRASConsequence);
            this.ReplaceText("nras_cons", testOrder.NRASConsequence);
            this.ReplaceText("hras_cons", testOrder.HRASConsequence);
            
            this.ReplaceText("braf_peop", testOrder.BRAFPredictedEffectOnProtein);
            this.ReplaceText("kras_peop", testOrder.KRASPredictedEffectOnProtein);
            this.ReplaceText("nras_peop", testOrder.NRASPredictedEffectOnProtein);
            this.ReplaceText("hras_peop", testOrder.HRASPredictedEffectOnProtein);
            
            this.ReplaceText("report_interpretation", testOrder.Interpretation);
            this.ReplaceText("report_comment", testOrder.Comment);
            this.ReplaceText("report_method", testOrder.Method);
            this.ReplaceText("report_references", testOrder.References);
            this.ReplaceText("report_disclaimer", testOrder.ReportDisclaimer);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
