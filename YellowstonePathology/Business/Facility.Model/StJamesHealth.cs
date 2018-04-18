using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class StJamesHealth : Facility
    {
        public StJamesHealth()
        {
            this.m_FacilityId = "STJMSHLTH";
            this.m_FacilityIdOLD = null;         
            this.m_FacilityName = "St. James Health - Lab";
            this.m_Address1 = "400 South Clark";
            this.m_Address2 = null;
            this.m_City = "Butte";
            this.m_State = "MT";
            this.m_ZipCode = "59701";
            this.m_PhoneNumber = "(406)723-2600";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;

            //this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
