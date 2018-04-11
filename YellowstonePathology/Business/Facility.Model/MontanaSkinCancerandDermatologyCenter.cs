using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MontanaSkinCancerandDermatologyCenter : Facility
    {        
        public MontanaSkinCancerandDermatologyCenter()
        {
            this.m_FacilityId = "MTSCDC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Montana Skin Cancer and Dermatology Center";
            this.m_Address1 = "1727 West College Street";
            this.m_Address2 = null;
            this.m_City = "Bozeman";
            this.m_State = "MT";
            this.m_ZipCode = "59715";
            this.m_PhoneNumber = "4065874432";
            this.m_FedexAccountNo = "174761051";
            this.m_FedexPaymentType = "RECIPIENT";					
        }
    }
}
