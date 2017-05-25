using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NeogenomicsIrvine : Facility
    {        
        public NeogenomicsIrvine()
        {
            this.m_FacilityId = "NEOGNMCIRVN";
            this.m_FacilityIdOLD = "CC56EEAA-C223-4591-BD44-7B6C56C80382";
            this.m_FacilityName = "Neogenomics";
            this.m_Address1 = "31 Columbia";
            this.m_Address2 = null;
            this.m_City = "Aliso Viejo";
            this.m_State = "CA";
            this.m_ZipCode = "92618";
            this.m_PhoneNumber = "(888)443-3310";
            this.m_FedexAccountNo = "245909101";
            this.m_FedexPaymentType = "THIRD_PARTY";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, "05D1065194");            
        }
    }
}
