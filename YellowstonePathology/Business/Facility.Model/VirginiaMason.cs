using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class VirginiaMason : Facility
    {
        public VirginiaMason()
        {
            this.m_FacilityId = "VRGNMSON";            
            this.m_FacilityName = "Virginia Mason Medical Center";
            this.m_Address1 = "1100 Ninth Avenue";
            this.m_Address2 = null;
            this.m_City = "Seattle";
            this.m_State = "WA";
            this.m_ZipCode = "98101";
            this.m_PhoneNumber = "(206)223-6861";
            this.m_FedexAccountNo = "986497";
            this.m_FedexPaymentType = "RECIPIENT";
			//this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
