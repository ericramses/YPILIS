using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ButtePathology : Facility
    {
        public ButtePathology()
        {
            this.m_FacilityId = "BTTPTHLGY";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Butte Pathology, LLC";
            this.m_Address1 = "400 S. Clark";
            this.m_Address2 = null;
            this.m_City = "Butte";
            this.m_State = "MT";
            this.m_ZipCode = "59701";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;
            this.m_ClientId = 278;

            this.m_CliaLicense = new CLIALicense(this, "27D0410539");
        }
    }
}
