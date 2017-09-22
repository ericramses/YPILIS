using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MyriadGeneticsLab : Facility
    {
        public MyriadGeneticsLab()
        {
            this.m_FacilityId = "MYRGTCSLb";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Myriad Genetics Lab";
            this.m_Address1 = "320 Wakara Way";
            this.m_Address2 = null;
            this.m_City = "Salt Lake City";
            this.m_State = "UT";
            this.m_ZipCode = "84108";
            this.m_PhoneNumber = "(844)877-3636";
            this.m_FedexAccountNo = "164998258";
            this.m_FedexPaymentType = "SENDER";            
            this.m_IsReferenceLab = false;                        
        }
    }
}
