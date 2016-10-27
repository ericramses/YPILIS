using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniversityOfMiami : Facility
    {
        public UniversityOfMiami()
        {
            this.m_FacilityId = "UMMSM";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "University Of Miami Miller School of Medicine";
            this.m_Address1 = "1400 NW 12th Avenue";
            this.m_Address2 = "Room 4060";
            this.m_City = "Miammi";
            this.m_State = "FL";
            this.m_ZipCode = "33136";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
