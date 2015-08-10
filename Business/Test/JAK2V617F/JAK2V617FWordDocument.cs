using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
            this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;

			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);

			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			JAK2V617FTestOrder panelSetOrderJAK2V617F = (JAK2V617FTestOrder)this.m_PanelSetOrder;
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\JAK2V617F.2.xml";
			base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			string reportResult = panelSetOrderJAK2V617F.Result;
			if (string.IsNullOrEmpty(reportResult))
			{
				reportResult = string.Empty;
			}

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("report_result", reportResult);
			this.ReplaceText("result_comment", panelSetOrderJAK2V617F.Comment);
			this.ReplaceText("report_interpretation", panelSetOrderJAK2V617F.Interpretation);
			this.ReplaceText("report_method", panelSetOrderJAK2V617F.Method);
			this.ReplaceText("report_reference", panelSetOrderJAK2V617F.Reference);

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
