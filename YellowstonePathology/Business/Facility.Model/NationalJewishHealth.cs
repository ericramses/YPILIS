using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NathionalJewishHealth : Facility
    {
        public NathionalJewishHealth()
        {
            this.m_FacilityId = "NJH";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Nathional Jewish Health";
            this.m_Address1 = "1400 Jackson St";
            this.m_Address2 = "Room F107";
            this.m_City = "Denver";
            this.m_State = "CO";
            this.m_ZipCode = "80206";
            this.m_PhoneNumber = "303-398-1355";
            this.m_IsReferenceLab = false;
            this.m_FedexAccountNo = "080226705";
            this.m_FedexPaymentType = "RECIPIENT";

            this.m_CliaLicense = new CLIALicense(this, string.Empty);            
        }
    }
}
