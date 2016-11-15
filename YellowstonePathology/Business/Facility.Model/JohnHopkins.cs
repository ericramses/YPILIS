using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class JohnHopkins : Facility
    {
        public JohnHopkins()
        {
            this.m_FacilityId = "JHNHLKNS";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "John Hopkins Reference Laboratories";
            this.m_Address1 = "1620 McElderry Street";
            this.m_Address2 = "Reed Hall, room 315";
            this.m_City = "Baltimore";
            this.m_State = "MD";
            this.m_ZipCode = "21231-240";
            this.m_PhoneNumber = "(410)516-8000";
            this.m_FedexAccountNo = "245909101";
            this.m_FedexPaymentType = "SENDER";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
