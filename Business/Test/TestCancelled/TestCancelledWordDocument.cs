using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Test.TestCancelled
{
	public class TestCancelledWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
            this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;
			this.m_AccessionOrder = YellowstonePathology.Business.Persistence.ObjectGateway.Instance.GetByMasterAccessionNo(masterAccessionNo);
            
            YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder reportOrderTestCancelled = (YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_PanelSetOrder = reportOrderTestCancelled;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\TestCancelled.8.xml";
			base.OpenTemplate();

			base.SetDemographicsV2();

			string testName = "Test Canceled";
			if (string.IsNullOrEmpty(reportOrderTestCancelled.CancelledTestName) == false)
			{
				testName = reportOrderTestCancelled.CancelledTestName;
			}

			this.ReplaceText("test_canceled_name", testName);
            this.ReplaceText("test_cancelled_comment",  reportOrderTestCancelled.Comment);
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            string finalDate = YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate) + " - " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(this.m_PanelSetOrder.FinalTime);
            this.SetXmlNodeData("final_date", finalDate);

            this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}
