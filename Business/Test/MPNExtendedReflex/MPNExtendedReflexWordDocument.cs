using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	public class MPNExtendedReflexWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
			this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;
			this.m_AccessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo(masterAccessionNo, true);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexResult mpnExtendedReflexResult = new Test.MPNExtendedReflex.MPNExtendedReflexResult(this.m_AccessionOrder);
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\MPNExtendedReflex.2.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("panelset_name", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.PanelSetName);
            this.ReplaceText("jak2v617_result", mpnExtendedReflexResult.JAK2V617FResult.Result);
            this.ReplaceText("calr_result", mpnExtendedReflexResult.CALRResult.Result);
            this.ReplaceText("mpl_result", mpnExtendedReflexResult.MPLResult.Result);
            this.ReplaceText("report_comment", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Comment);

            base.ReplaceText("specimen_description", mpnExtendedReflexResult.SpecimenOrder.Description);
            this.ReplaceText("report_interpretation", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Interpretation);
            this.ReplaceText("report_method", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Method);
            this.ReplaceText("report_reference", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.References);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(mpnExtendedReflexResult.SpecimenOrder.CollectionDate, mpnExtendedReflexResult.SpecimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Signature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
