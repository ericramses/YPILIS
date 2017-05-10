using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class StanfordUniversityMedicalCenter : Facility
    {
        public StanfordUniversityMedicalCenter()
        {
            this.m_FacilityId = "STUMC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Stanford University Medical Center";
            this.m_Address1 = "300 Pasteur Drive";
            this.m_Address2 = "Room L235";
            this.m_City = "Stanford";
            this.m_State = "CA";
            this.m_ZipCode = "94305";
            this.m_PhoneNumber = "(650)723-7211";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "SENDER";            
            this.m_IsReferenceLab = true;                        
        }
    }
}
