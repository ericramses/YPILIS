using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NorthernMontanaHealthcare : Facility
    {
        public NorthernMontanaHealthcare()
        {
            this.m_FacilityId = "NMTHC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Northern Montana Healthcare";
            this.m_Address1 = "P.O. Box 1231";
            this.m_Address2 = "ATTB: Lab";
            this.m_City = "Havre";
            this.m_State = "MT";
            this.m_ZipCode = "59501";
            this.m_PhoneNumber = "4062652211";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "RECIPIENT";            
            this.m_IsReferenceLab = true;                        
        }
    }
}
