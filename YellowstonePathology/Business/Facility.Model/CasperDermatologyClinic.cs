using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CasperDermatologyClinic : Facility
    {
        public CasperDermatologyClinic()
        {
            this.m_FacilityId = "CSPRDRMCLNC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Casper Dermatology Clinic";
            this.m_Address1 = "1119 e. 3rd Street";
            this.m_Address2 = null;
            this.m_City = "Casper";
            this.m_State = "WY";
            this.m_ZipCode = "82601";
            this.m_PhoneNumber = "(307)266-2772";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "SENDER";            
            this.m_IsReferenceLab = false;                        
        }
    }
}
