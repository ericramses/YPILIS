using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniversityOfUtahHealthCare : Facility
    {
        public UniversityOfUtahHealthCare()
        {
            this.m_FacilityId = "UUHCare";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "University Of Utah Health Care";
            this.m_Address1 = "30 North 1900 East";
            this.m_City = "Salt Lake City";
            this.m_State = "UT";
            this.m_ZipCode = "84132";
            this.m_PhoneNumber = "8015850221";
            this.m_FedexAccountNo = "575922228";
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
