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
            this.HandleReferringProvider(accessionOrder);
            foreach (PhysicianClientDistribution physicianClientDistribution in this)
            {
                physicianClientDistribution.SetDistribution(panelSetOrder, accessionOrder);
            }            
        }    
        
        private void HandleReferringProvider(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);
            if(client.HasReferringProvider == true)
            {
                PhysicianClientDistribution physicianClientDistribution = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(client.ReferringProviderClientId);
                this.Add(physicianClientDistribution);
            }
        }    
    }
}
