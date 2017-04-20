using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CBCI : Facility
    {
        public CBCI()
        {
            this.m_FacilityId = "CBCI";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "CBCI/Dr. Matous";
            this.m_Address1 = "1721 E. 19th Ave.";
            this.m_Address2 = "Suite 300";
            this.m_City = "Denver";
            this.m_State = "CO";
            this.m_ZipCode = "80218";
            this.m_PhoneNumber = "(720)754-4448";
            this.m_FedexAccountNo = "481734541";
            this.m_FedexPaymentType = "RECIPIENT";            
            this.m_IsReferenceLab = true;                        
        }
    }
}
