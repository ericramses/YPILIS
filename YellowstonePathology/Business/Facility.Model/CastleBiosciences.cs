using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CastleBiosciences : Facility
    {
        public CastleBiosciences()
        {
            this.m_FacilityId = "CSTLBSCNCS";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Castle Biosciences";
            this.m_Address1 = "3737N 7th St.";
            this.m_Address2 = "Suite 160";
            this.m_City = "Phoenix";
            this.m_State = "AZ";
            this.m_ZipCode = "85014";
            this.m_PhoneNumber = "(866)788-9007";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "SENDER";            
            this.m_IsReferenceLab = false;                        
        }
    }
}
