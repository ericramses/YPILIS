using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class IncompatibleDistributionType
    {
        private string m_PrimaryDistributionType;
        private string m_SecondaryDistributionType;

        public IncompatibleDistributionType(string primaryDistributionType, string secondaryDistributionType)
        {
            this.m_PrimaryDistributionType = primaryDistributionType;
            this.m_SecondaryDistributionType = secondaryDistributionType;
        }

        public string PrimaryDistributionType
        {
            get { return this.m_PrimaryDistributionType; }
        }

        public string SecondaryDistributionType
        {
            get { return this.m_SecondaryDistributionType; }
        }
    }
}
