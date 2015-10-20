using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
    public class TechInitiatedPeripheralSmearWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
        {
            this.m_ReportNo = reportNo;
            this.m_ReportSaveEnum = reportSaveEnum;
            this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);

            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearTestOrder techInitiatedPeripheralSmearTestOrder = (YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\TechInitiatedPeripheralSmear.xml";
            base.OpenTemplate();

            base.SetDemographicsV2();

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(this.m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

            base.ReplaceText("tech_comment", techInitiatedPeripheralSmearTestOrder.TechComment);
            base.ReplaceText("pathologist_comment", techInitiatedPeripheralSmearTestOrder.PathologistComment);


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
