using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class StVincentHealthcare : Facility
    {
        public StVincentHealthcare()
        {
            this.m_FacilityId = "STVNCNT";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "St. Vincent Healthcare";
            this.m_Address1 = "1233 N. 30th St.";
            this.m_Address2 = null;     
            this.m_City = "Billings";
            this.m_State = "MT";
            this.m_ZipCode = "59102";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);  
        }
    }
}
