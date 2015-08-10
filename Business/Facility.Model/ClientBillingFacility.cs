using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ClientBillingFacility
    {        
        private Facility m_PerformingFacility;
        private Facility m_DefaultBillingFacility;
        private Business.Client.Model.ClientGroup m_ClientGroup;
        private string m_FacilityComponent;

        public ClientBillingFacility(Facility performaingFacility, Facility defaultBillingFacility, Business.Client.Model.ClientGroup clientGroup, string facilityComponent)
        {
            this.m_PerformingFacility = performaingFacility;
            this.m_DefaultBillingFacility = defaultBillingFacility;
            this.m_ClientGroup = clientGroup;
            this.m_FacilityComponent = facilityComponent;
        }        

        public bool IsMatch(string performingFacilityId, int clientId, string facilityComponent)
        {
            bool result = false;
            if (this.m_PerformingFacility.FacilityId == performingFacilityId && this.m_FacilityComponent == facilityComponent)
            {
                if (this.m_ClientGroup.Exists(clientId) == true)
                {
                    result = true;
                }
            }
            return result;
        }

        public Business.Client.Model.ClientGroup ClientGroup
        {
            get { return this.m_ClientGroup; }
        }

        public Facility PerformingFacility
        {
            get { return this.m_PerformingFacility; }
        }

        public Facility DefaultBillingFacility
        {
            get { return this.m_DefaultBillingFacility; }
        }

        public string FacilityComponent
        {
            get { return this.m_FacilityComponent; }
        }
    }
}
