using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class PhysicianClientDistributionReturnEventArgs : System.EventArgs
    {
        private Business.Client.PhysicianClientDistribution m_PhysicianClientDistribution;

        public PhysicianClientDistributionReturnEventArgs(Business.Client.PhysicianClientDistribution physicianClientDistribution)
        {
            this.m_PhysicianClientDistribution = physicianClientDistribution;
        }

        public Business.Client.PhysicianClientDistribution PhysicianClientDistribution
        {
            get { return this.m_PhysicianClientDistribution; }
        }
    }
}
