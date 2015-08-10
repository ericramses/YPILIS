using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client
{
    public class PhysicianClientDistributionFactory
    {
        public static PhysicianClientDistribution GetPhysicianClientDistribution(string distributionType)
        {
            PhysicianClientDistribution physicianClientDistribution = null;
            switch (distributionType)
            {
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX:
                    physicianClientDistribution = new FaxPhysicianClientDistribution();
                    break;                                                       
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                    physicianClientDistribution = new ECWPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                    physicianClientDistribution = new AthenaPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                    physicianClientDistribution = new EPICPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                    physicianClientDistribution = new MediTechPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX:
                    physicianClientDistribution = new EPICAndFaxPhysicianClientDistribution();
                    break;                                                
                default:
                    physicianClientDistribution = new PhysicianClientDistribution();
                    break;
            }
            return physicianClientDistribution;
        }
    }
}
