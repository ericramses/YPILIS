using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class SheridanMemorialHospital : Facility
    {        
        public SheridanMemorialHospital()
        {
            this.m_FacilityId = "SMH";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Sheridan Memorial Hospital - Pathology";
            this.m_Address1 = "1401 West Fifth Street";
            this.m_City = "Sheridan";
            this.m_State = "WY";
            this.m_ZipCode = "82801";
            this.m_PhoneNumber = "(307)672-1035";
            this.m_FedexAccountNo = "124675804";
            this.m_FedexPaymentType = "RECIPIENT";

            this.m_CliaLicense = new CLIALicense(this, "05D1065194");            
        }
    }
}
