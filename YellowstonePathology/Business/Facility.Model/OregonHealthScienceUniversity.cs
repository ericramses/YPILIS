using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class OregonHealthScienceUniversity : Facility
    {        
        public OregonHealthScienceUniversity()
        {
            this.m_FacilityId = "OHSU";            
            this.m_FacilityName = "Oregon Health and Science University";
            this.m_Address1 = "3181 SW Sam Jackson Park Road";
            this.m_Address2 = null;
            this.m_City = "Portland";
            this.m_State = "OR";
            this.m_ZipCode = "97239";
            this.m_PhoneNumber = "(503)494-6776";
            this.m_FedexAccountNo = "37105001";
            this.m_FedexPaymentType = "SENDER";            
			
			//this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
