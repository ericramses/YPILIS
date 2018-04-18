using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class FortHarrisonVA : Facility
    {
        public FortHarrisonVA()
        {
            this.m_FacilityId = "FTHVA";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Fort Harrison VA Medical Center";
            this.m_Address1 = "3687 Veterans Drive";
            this.m_Address2 = null;
            this.m_City = "Fort Harrison";
            this.m_State = "MT";
            this.m_ZipCode = "59636";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;

            //this.m_CliaLicense = new CLIALicense(this, "27D0410539");
        }
    }
}
