using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MarshfieldClinic : Facility
    {
        public MarshfieldClinic()
        {
            this.m_FacilityId = "MRSHFLDCLNC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Marshfield Clinic";
            this.m_Address1 = "1000 N. Oak Ave";
            this.m_City = "Marshfield";
            this.m_State = "WI";
            this.m_ZipCode = "54449";
            this.m_PhoneNumber = "715-389-3091";
            this.m_FedexAccountNo = "337966969";
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
