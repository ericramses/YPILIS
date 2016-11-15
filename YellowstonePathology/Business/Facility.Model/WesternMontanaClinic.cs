using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class WesternMontanaClinic : Facility
    {
        public WesternMontanaClinic()
        {
            this.m_FacilityId = "WSTMTCLNC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Western Montana Clinic - Dermatology Department";
            this.m_Address1 = "510 West Front Street";
            this.m_Address2 = null;
            this.m_City = "Missoula";
            this.m_State = "Mt";
            this.m_ZipCode = "59802";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
