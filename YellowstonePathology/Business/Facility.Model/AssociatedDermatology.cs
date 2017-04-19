using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class AssociatedDermatology : Facility
    {
        public AssociatedDermatology()
        {
            this.m_FacilityId = "ASSCTEDDNTLGY";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Associated Dermatology";
            this.m_Address1 = "50. S Last Chance Gulch";
            this.m_Address2 = null;
            this.m_City = "Helena";
            this.m_State = "MT";
            this.m_ZipCode = "59601";
            this.m_PhoneNumber = "(406)443-8228";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;            
            this.m_IsReferenceLab = true;                        
        }
    }
}
