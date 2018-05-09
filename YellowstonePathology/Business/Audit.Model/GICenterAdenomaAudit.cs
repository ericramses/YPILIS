using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class GICenterAdenomaAudit : AccessionOrderAudit
    {
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_DiagnosisKeyWords;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_SpecimenKeyWords;

        public GICenterAdenomaAudit(Test.AccessionOrder accessionOrder) : base(accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_DiagnosisKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "adeno", "sessile" };
            this.m_SpecimenKeyWords = new YellowstonePathology.Business.Surgical.KeyWordCollection { "colon", "cecum", "rectum" };
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            //If SVH GI Center
            if(AccessionOrder.ClientId == 1456)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                    if (panelSetOrderCPTCodeCollectionForThisSpecimen.Exists("88305", 1))
                    {
                        if(this.m_DiagnosisKeyWords.WordsExistIn(surgicalSpecimen.Diagnosis) == true)
                        {
                            if(this.m_SpecimenKeyWords.WordsExistIn(surgicalSpecimen.SpecimenOrder.Description) == true)
                            {
                                if (surgicalTestOrder.TestOrderReportDistributionCollection.Exists(this.m_AccessionOrder.PhysicianId, 1630, YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX) == false)
                                {
                                    string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                                    surgicalTestOrder.TestOrderReportDistributionCollection.AddNext(testOrderReportDistributionId, testOrderReportDistributionId, surgicalTestOrder.ReportNo, this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.PhysicianName,
                                        1630, "SVH - GI Adenoma Reporting", YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX);
                                }
                            }                            
                        }
                    }
                }
            }
            
        }
    }
}
