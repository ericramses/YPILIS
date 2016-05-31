using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client
{
    public class FaxPhysicianClientDistribution : PhysicianClientDistribution
    {
        public override void From(PhysicianClientDistribution physicianClientDistribution)
        {
            base.From(physicianClientDistribution);
            this.m_DistributionType = YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX;
        }        
    }
}
