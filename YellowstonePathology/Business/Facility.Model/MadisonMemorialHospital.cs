using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MadisonMemorialHospital : Facility
    {
        public MadisonMemorialHospital()
        {
            this.m_FacilityId = "MMH";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Madison Memorial Hospital";
            this.m_Address1 = "P.O. Box 310";
            this.m_Address2 = null;
            this.m_City = "Rexburg";
            this.m_State = "ID";
            this.m_ZipCode = "83440";
            this.m_PhoneNumber = "(208) 359-6900";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;            
            this.m_IsReferenceLab = true;                        
        }
    }
}
