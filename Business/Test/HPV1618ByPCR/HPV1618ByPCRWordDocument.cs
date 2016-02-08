using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV1618ByPCRWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum, object writer)
		{
			this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;
			this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo);

			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder panelSetOrder = (YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HPV1618GenotypingByPCR.1.xml";
			base.OpenTemplate();

			base.SetDemographicsV2();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);            

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(this.m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

			base.ReplaceText("hpv16_result", panelSetOrder.HPV16Result);
			base.ReplaceText("hpv18_result", panelSetOrder.HPV18Result);

            if (panelSetOrder.Indication == YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                base.ReplaceText("report_interpretation_header", "Interpretation");
                base.ReplaceText("report_interpretation", panelSetOrder.Interpretation);
				base.ReplaceText("pathologist_signature", panelSetOrder.Signature);

                if (panelSetOrder.FinalTime.HasValue == true)
                {
                    string esignedHeader = "*** E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + "***";
                    base.ReplaceText("esigned_header", esignedHeader);
                }
            }
            else
            {
                base.DeleteRow("report_interpretation_header");
                base.DeleteRow("report_interpretation");
                base.DeleteRow("pathologist_signature");
                base.DeleteRow("esigned_header");
            }

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                base.ReplaceText("report_comment", panelSetOrder.Comment);
            }
            else
            {
                base.DeleteRow("report_comment");
            }
            
            base.ReplaceText("report_method", panelSetOrder.Method);

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
