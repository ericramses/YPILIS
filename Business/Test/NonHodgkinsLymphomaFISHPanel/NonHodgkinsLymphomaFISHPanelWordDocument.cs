using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel
{
	public class NonHodgkinsLymphomaFISHPanelWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
			this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;

			this.m_AccessionOrder = YellowstonePathology.Business.Persistence.ObjectGateway.Instance.GetByMasterAccessionNo(masterAccessionNo);

			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\NonHodgkinsLymphomaFISHPanel.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelTestOrder nonHodgkinsLymphomaFISHPanelTestOrder = (YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelTestOrder)this.m_PanelSetOrder;

			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(nonHodgkinsLymphomaFISHPanelTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(nonHodgkinsLymphomaFISHPanelTestOrder.OrderedOn, nonHodgkinsLymphomaFISHPanelTestOrder.OrderedOnId);

			string specimenDescription = specimenOrder.Description;
			if (aliquotOrder != null) specimenDescription += ", Block " + aliquotOrder.Label;
			this.ReplaceText("specimen_description", specimenDescription);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.SetXMLNodeParagraphData("report_result", nonHodgkinsLymphomaFISHPanelTestOrder.Result);
			this.SetXMLNodeParagraphData("report_interpretation", nonHodgkinsLymphomaFISHPanelTestOrder.Interpretation);
			this.SetXMLNodeParagraphData("nuclei_scored", nonHodgkinsLymphomaFISHPanelTestOrder.NucleiScored);
			this.SetXMLNodeParagraphData("probeset_details", nonHodgkinsLymphomaFISHPanelTestOrder.ProbeSetDetail);
			this.SetXMLNodeParagraphData("report_reference", nonHodgkinsLymphomaFISHPanelTestOrder.References);
			this.ReplaceText("report_disclaimer", nonHodgkinsLymphomaFISHPanelTestOrder.ReportDisclaimer);

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
