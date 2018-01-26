using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class WilliamsPorterDayLaw : Facility
    {
        public WilliamsPorterDayLaw()
        {
            this.m_FacilityId = "WPDNLaw";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Williams, Porter, Day and Neville, PC";
            this.m_Address1 = "159 N. Wolcott St., #400";
            this.m_Address2 = null;
            this.m_City = "Casper";
            this.m_State = "WY";
            this.m_ZipCode = "82601";
            this.m_PhoneNumber = "3072650700";
            this.m_FedexAccountNo = "100388332";
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = false;                        
        }
    }
}
