using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ChristianaCare : Facility
    {
        public ChristianaCare()
        {
            this.m_FacilityId = "CHRSTNCR";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Christiana Care Health System";
            this.m_Address1 = "4701 Ogletown Stanton Rd";
            this.m_Address2 = "Multidisciplinary Center Ste#1200";
            this.m_City = "Newark";
            this.m_State = "DE";
            this.m_ZipCode = "19713";
            this.m_PhoneNumber = "(302)623-4720";
            this.m_FedexAccountNo = "241991024";
            this.m_FedexPaymentType = "RECIPIENT";            
            this.m_IsReferenceLab = true;                        
        }
    }
}
