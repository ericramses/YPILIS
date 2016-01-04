using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CalreticulinMutationAnalysis
{
	public class CalreticulinMutationAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
			this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			CalreticulinMutationAnalysisTestOrder reportOrderCalreticulinMutationAnalysis = (CalreticulinMutationAnalysisTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\CalreticulinMutationAnalysis.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			string reportResult = reportOrderCalreticulinMutationAnalysis.Result;
			if (string.IsNullOrEmpty(reportResult))
			{
				reportResult = string.Empty;
			}

			this.ReplaceText("report_result", reportResult);
            this.ReplaceText("calr_percentage", reportOrderCalreticulinMutationAnalysis.Percentage);
            this.ReplaceText("calr_mutations", reportOrderCalreticulinMutationAnalysis.Mutations);

            this.ReplaceText("report_interpretation", reportOrderCalreticulinMutationAnalysis.Interpretation);
            this.ReplaceText("report_method", reportOrderCalreticulinMutationAnalysis.Method);
            this.ReplaceText("report_references", reportOrderCalreticulinMutationAnalysis.References);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", reportOrderCalreticulinMutationAnalysis.ReferenceLabSignature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
