using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class TallmanDermatology : Facility
    {
        public TallmanDermatology()
        {
            this.m_FacilityId = "TLLMNDRM";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Tallman Dermatology";
            this.m_Address1 = "2294 Grant Road";
            this.m_Address2 = null;
            this.m_City = "Billings";
            this.m_State = "MT";
            this.m_ZipCode = "59102";
            this.m_PhoneNumber = "(406)294-9515";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;

            //this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
