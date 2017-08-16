using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniversityOfArizonaCancerCenter : Facility
    {
        public UniversityOfArizonaCancerCenter()
        {
            this.m_FacilityId = "UACC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "University Of Arizona Cancer Center";
            this.m_Address1 = "3838 North Campbell Avenue";
            this.m_Address2 = null;
            this.m_City = "Tucson";
            this.m_State = "AZ";
            this.m_ZipCode = "85719";
            this.m_PhoneNumber = "520-694-1856";
            this.m_FedexAccountNo = "321679277";
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
