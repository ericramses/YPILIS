using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CMMC : Facility
    {
        public CMMC()
        {
            this.m_FacilityId = "CMMC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "CMMC";
            this.m_Address1 = "408 Wendell Avenue";
            this.m_Address2 = null;
            this.m_City = "Lewistown";
            this.m_State = "MT";
            this.m_ZipCode = "59457";
            this.m_PhoneNumber = "(406)535-6282";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
