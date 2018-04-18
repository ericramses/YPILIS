using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NationalInstituteOfHealth : Facility
    {
        public NationalInstituteOfHealth()
        {
            this.m_FacilityId = "NIHNCI";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "National Institute Of Health";
            this.m_Address1 = "10 center Drive";
            this.m_Address2 = "Bldg. 10/Room 2S235";
            this.m_City = "Bethesda";
            this.m_State = "MD";
            this.m_ZipCode = "20892";
            this.m_PhoneNumber = "(301)496-2132";
            this.m_FedexAccountNo = "138537129";
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = true;
            //this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
