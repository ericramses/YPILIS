using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CanSetDistributionAudit : AccessionOrderAudit
    {
        public CanSetDistributionAudit(Test.AccessionOrder accessionOrder) : base(accessionOrder)
        { }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.ClientId);
            foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistributionListItem in physicianClientDistributionCollection)
            {
                if (physicianClientDistributionListItem.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC ||
                    physicianClientDistributionListItem.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX)
                {
                    if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == true || string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append("Unable to set distribution as the Account Number or the Medical Record Number is missing.");
                        break;
                    }
                }
            }
        }
    }
}
