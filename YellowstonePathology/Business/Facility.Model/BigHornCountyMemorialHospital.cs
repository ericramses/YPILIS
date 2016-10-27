using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class BigHornCountyMemorialHospital : Facility
    {        
        public BigHornCountyMemorialHospital()
        {
            this.m_FacilityId = "BHCMH";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "BigHornCountyMemorialHospital";
            this.m_Address1 = "17 North Miles Avenue";
            this.m_Address2 = null;
            this.m_City = "Hardin";
            this.m_State = "MT";
            this.m_ZipCode = "59034";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = false; 
			
			this.m_CliaLicense = new CLIALicense(this, "05D1065194");            
        }
    }
}
