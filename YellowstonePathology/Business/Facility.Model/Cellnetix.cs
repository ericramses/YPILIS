using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class Cellnetix : Facility
    {
        public Cellnetix()
        {
            this.m_FacilityId = "CLLNTX";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Cellnetix";
            this.m_Address1 = "1124 Columbia Street";
            this.m_City = "Seattle";
            this.m_State = "WA";
            this.m_ZipCode = "98104";
            this.m_PhoneNumber = "8443444209";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
