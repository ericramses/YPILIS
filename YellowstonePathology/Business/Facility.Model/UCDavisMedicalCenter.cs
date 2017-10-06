using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UCDavisMedicalCenter : Facility
    {
        public UCDavisMedicalCenter()
        {
            this.m_FacilityId = "UCDAVIS";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "UC Davis Medical Center";
            this.m_Address1 = "2315 Stockton Boulevard";
            this.m_Address2 = null;
            this.m_City = "Sacrementao";
            this.m_State = "UCA";
            this.m_ZipCode = "95817";
            this.m_PhoneNumber = "(916)734-0500";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
