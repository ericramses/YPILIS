using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWIWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
            this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
            this.m_PanelSetOrder = m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HPVThirdWave.7.xml";
			base.OpenTemplate();

			base.SetDemographicsV2();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(this.m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI)this.m_PanelSetOrder;
			
			if(string.IsNullOrEmpty(panelSetOrder.Result) == false) base.ReplaceText("test_result", panelSetOrder.Result);            

			if (string.IsNullOrEmpty(panelSetOrder.Comment) == false) base.ReplaceText("report_comment", panelSetOrder.Comment);
			else base.DeleteRow("report_comment");

            bool hpvHasBeenOrdered = this.m_AccessionOrder.PanelSetOrderCollection.Exists(62);

            string additionalTestingComment = string.Empty;
            if (hpvHasBeenOrdered == true)
            {
				additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.HPV1618HasBeenOrderedComment;
            }
            else
            {
				additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.NoAdditionalTestingOrderedComment;
            }

            this.ReplaceText("additional_testing", additionalTestingComment);    

			base.ReplaceText("test_information", panelSetOrder.TestInformation);
			base.ReplaceText("report_references", panelSetOrder.References);
            			
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
