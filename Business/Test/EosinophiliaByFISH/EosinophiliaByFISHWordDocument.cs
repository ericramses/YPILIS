using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EosinophiliaByFISH
{
	public class EosinophiliaByFISHWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum, object writer)
		{
			this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;

			this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo);

			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\EosinophiliaByFISH.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHTestOrder eosinophiliaByFISHTestOrder = (YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHTestOrder)this.m_PanelSetOrder;

			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(eosinophiliaByFISHTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(eosinophiliaByFISHTestOrder.OrderedOn, eosinophiliaByFISHTestOrder.OrderedOnId);

			string specimenDescription = specimenOrder.Description;
			if (aliquotOrder != null) specimenDescription += ", Block " + aliquotOrder.Label;
			this.ReplaceText("specimen_description", specimenDescription);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.SetXMLNodeParagraphData("report_result", eosinophiliaByFISHTestOrder.Result);
			this.SetXMLNodeParagraphData("report_interpretation", eosinophiliaByFISHTestOrder.Interpretation);
			this.SetXMLNodeParagraphData("nuclei_scored", eosinophiliaByFISHTestOrder.NucleiScored);
			this.SetXMLNodeParagraphData("probeset_details", eosinophiliaByFISHTestOrder.ProbeSetDetail);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

			this.ReplaceText("report_disclaimer", eosinophiliaByFISHTestOrder.ReportDisclaimer);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
