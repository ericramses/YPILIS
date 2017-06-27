using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniversityOfMississippi : Facility
    {
        public UniversityOfMississippi()
        {
            this.m_FacilityId = "UOFMSSPP";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "UniversityOfMississippi";
            this.m_Address1 = "2500 N. State St.";
            this.m_Address2 = null;
            this.m_City = "Jackson";
            this.m_State = "MS";
            this.m_ZipCode = "39216";
            this.m_PhoneNumber = "6629157211";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "RECIPIENT";            
            this.m_IsReferenceLab = true;                        
        }
    }
}
