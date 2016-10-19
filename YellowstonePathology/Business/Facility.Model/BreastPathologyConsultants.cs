using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class BreastPathologyConsultants : Facility
    {
        public BreastPathologyConsultants()
        {
            this.m_FacilityId = "BRSTPATHCNLT";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Breast Pathology Consultants";
            this.m_Address1 = "220 21st Avenue South";
            this.m_Address2 = "Suite 250";
            this.m_City = "Nashville";
            this.m_State = "TN";
            this.m_ZipCode = "37212";
            this.m_PhoneNumber = "(615)457-1837";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "SENDER";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
