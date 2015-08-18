using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeIHCPanelWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
            this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;

			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);

			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);            

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\LynchSyndromeIHCPanel.5.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);       
			
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTestOrder panelSetOrder = (YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(panelSetOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);

            string specimenDescription = specimenOrder.Description + ", Block " + aliquotOrder.Label;
            this.ReplaceText("specimen_description", specimenDescription);            

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXmlNodeData("mlh1_result", panelSetOrder.MLH1Result);
            this.SetXmlNodeData("msh2_result", panelSetOrder.MSH2Result);
            this.SetXmlNodeData("msh6_result", panelSetOrder.MSH6Result);
            this.SetXmlNodeData("pms2_result", panelSetOrder.PMS2Result);            
			
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
