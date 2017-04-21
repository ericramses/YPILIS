using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class VAEasternColorado : Facility
    {
        public VAEasternColorado()
        {
            this.m_FacilityId = "VSTRNCLRD";            
            this.m_FacilityName = "VA Easter Colorado";
            this.m_Address1 = "1055 Clermont Street";
            this.m_Address2 = null;
            this.m_City = "Denver";
            this.m_State = "CO";
            this.m_ZipCode = "80220";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;            
            this.m_IsReferenceLab = true;                        
        }
    }
}
