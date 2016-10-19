using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniversityOfArkansasMedicalSciences : Facility
    {
        public UniversityOfArkansasMedicalSciences()
        {
            this.m_FacilityId = "UAMS";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "University Of Arkansas Medical Sciences";
            this.m_Address1 = "4301 West Markham St.";
            this.m_Address2 = null;
            this.m_City = "Little Rock";
            this.m_State = "AR";
            this.m_ZipCode = "72205";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
