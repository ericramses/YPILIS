using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ClientBillingFacilityCollection : ObservableCollection<ClientBillingFacility>
    {
        public ClientBillingFacilityCollection()
        {
            Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            Facility neogenomicsFlorida = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCINC");

            Facility ypii = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection clientGroupStVincent = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("1");
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection clientGroupAllClients = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollection();

            this.Add(new ClientBillingFacility(neogenomicsIrvine, ypii, clientGroupStVincent, "Technical"));
            this.Add(new ClientBillingFacility(neogenomicsFlorida, ypii, clientGroupStVincent, "Technical"));

            this.Add(new ClientBillingFacility(neogenomicsIrvine, neogenomicsIrvine, clientGroupAllClients, "Professional"));
            this.Add(new ClientBillingFacility(neogenomicsFlorida, neogenomicsFlorida, clientGroupAllClients, "Professional"));
            
            this.Add(new ClientBillingFacility(ypii, ypii, clientGroupAllClients, "Technical"));
            this.Add(new ClientBillingFacility(ypii, ypii, clientGroupAllClients, "Professional"));
        }        
    }
}
