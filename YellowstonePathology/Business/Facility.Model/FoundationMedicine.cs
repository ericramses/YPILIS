using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class FoundationMedicine : Facility
    {
        public FoundationMedicine()
        {
            this.m_FacilityId = "FNDMDCN";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Foundation Medicine";
            this.m_Address1 = "7010 Kit Creek Road";
            this.m_Address2 = null;
            this.m_City = "Morrisville";
            this.m_State = "NC";
            this.m_ZipCode = "27560";
            this.m_PhoneNumber = "(617)418-220";
            this.m_FedexAccountNo = "245909101";
            this.m_FedexPaymentType = "SENDER";
            this.m_IsReferenceLab = true;                        
        }
    }
}
