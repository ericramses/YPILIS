﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianClientDistributionList : ObservableCollection<PhysicianClientDistributionListItem>
    {
        public PhysicianClientDistributionList()
        {

        }

        public void SetDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.HandleReferringProvider(accessionOrder);
            this.HandlePathGroup(accessionOrder);            
            foreach (PhysicianClientDistributionListItem physicianClientDistribution in this)
            {
                physicianClientDistribution.SetDistribution(panelSetOrder, accessionOrder);
            }            
        }    

        public void HandlePathGroup(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);
            if(client.PathologyGroupId != "YPBLGS")
            {
                Business.Facility.Model.Facility pathFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(client.PathologyGroupId);
                Business.Client.Model.Client pathClient = Business.Gateway.PhysicianClientGateway.GetClientByClientId(pathFacility.ClientId);

                PhysicianClientDistributionListItem physicianClientDistribution = YellowstonePathology.Business.Client.Model.PhysicianClientDistributionFactory.GetPhysicianClientDistribution(client.DistributionType);
                physicianClientDistribution.ClientId = pathClient.ClientId;
                physicianClientDistribution.ClientName = pathClient.ClientName;
                physicianClientDistribution.PhysicianId = 728;
                physicianClientDistribution.PhysicianName = "Staff Pathologist";
                physicianClientDistribution.DistributionType = pathClient.DistributionType;
                physicianClientDistribution.FaxNumber = pathClient.Fax;
                this.Add(physicianClientDistribution);                
            }
        }
        
        private void HandleReferringProvider(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);
            if(client.HasReferringProvider == true)
            {
                PhysicianClientDistributionListItem physicianClientDistribution = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(client.ReferringProviderClientId);
                this.Add(physicianClientDistribution);
            }
        }

        public bool DoesEpicDistributionExist()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistributionListItem in this)
            {
                if (physicianClientDistributionListItem.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC ||
                    physicianClientDistributionListItem.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
