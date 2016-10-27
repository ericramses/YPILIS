using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class SeattleCancerCenterAlliance : Facility
    {
        public SeattleCancerCenterAlliance()
        {
            this.m_FacilityId = "SCCA";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Seattle Cancer Center Alliance";
            this.m_Address1 = "825 Eastlake Ave E.";
            this.m_Address2 = null;
            this.m_City = "Seattle";
            this.m_State = "WA";
            this.m_ZipCode = "98109";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;
			
			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
