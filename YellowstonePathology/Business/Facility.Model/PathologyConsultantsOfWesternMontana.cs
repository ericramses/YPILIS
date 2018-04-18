using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class PathologyConsultantsOfWesternMontana : Facility
    {
        public PathologyConsultantsOfWesternMontana()
        {
            this.m_FacilityId = "PCOWM";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Pathology Consultants Of Western Montana";
            this.m_Address1 = "500 W Broadway St";
            this.m_Address2 = null;
            this.m_City = "Missoula";
            this.m_State = "MT";
            this.m_ZipCode = "59802";
            this.m_PhoneNumber = null;            
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = false;
            this.m_ClientId = 1282;
	
            //this.m_CliaLicense = new CLIALicense(this, string.Empty);            
        }
    }
}
