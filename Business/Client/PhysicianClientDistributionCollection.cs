using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client
{
    public class PhysicianClientDistributionCollection : ObservableCollection<PhysicianClientDistribution>
    {
        public PhysicianClientDistributionCollection()
        {

        }

        public void SetDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            foreach (PhysicianClientDistribution physicianClientDistribution in this)
            {
                physicianClientDistribution.SetDistribution(panelSetOrder, accessionOrder);
            }
        }        
    }
}
