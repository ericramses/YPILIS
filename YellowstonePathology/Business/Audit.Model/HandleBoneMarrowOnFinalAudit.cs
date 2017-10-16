using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HandleBoneMarrowOnFinalAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        YellowstonePathology.Business.Test.BoneMarrowSummary.BoneMarrowSummaryTest m_BoneMarrowSummaryTest;
        private bool m_HasSummary;
        private bool m_IsLastFinalForSummary;

        public HandleBoneMarrowOnFinalAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            this.m_BoneMarrowSummaryTest = new Test.BoneMarrowSummary.BoneMarrowSummaryTest();
        }

        public override void Run()
        {
            this.HasSummary();
            this.IsLastReportFinalForSummary();
            this.IsSummaryMessageIncidated();
        }

        private void HasSummary()
        {
            m_HasSummary = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.DoesPanelSetExist(this.m_BoneMarrowSummaryTest.PanelSetId) == true)
            {
                m_HasSummary = true;
                this.m_Status = AuditStatusEnum.OK;
            }
        }

        private void IsLastReportFinalForSummary()
        {
            List<int> exclusionList = this.m_AccessionOrder.PanelSetOrderCollection.GetBoneMarrowSummaryExclusionList();
            this.m_IsLastFinalForSummary = this.m_AccessionOrder.PanelSetOrderCollection.IsLastReportInSummaryToFinal(exclusionList, this.m_PanelSetOrder.PanelSetId);
        }

        private void IsSummaryMessageIncidated()
        {
            if (this.m_HasSummary == false && this.m_IsLastFinalForSummary == true)
            {
                BoneMarrowSummaryAudit audit = new BoneMarrowSummaryAudit(this.m_AccessionOrder);
                audit.Run();
                if(audit.Status == AuditStatusEnum.Failure)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("A Bone Marrow Summary is suggested.");
                }
            }
        }
    }
}
