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

            base.ReplaceText("technologist_question", techInitiatedPeripheralSmearTestOrder.TechnologistsQuestion);
            base.ReplaceText("pathologist_feedback", techInitiatedPeripheralSmearTestOrder.PathologistFeedback);
            base.ReplaceText("cbc_comment", techInitiatedPeripheralSmearTestOrder.CBCComment);

            this.SetReportDistribution();            

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
