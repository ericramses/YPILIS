using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class SheridanPathologyAssociates : Facility
    {        
        public SheridanPathologyAssociates()
        {
            this.m_FacilityId = "SHPTHASS";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Sheridan Pathology Associates";
            this.m_Address1 = "1401 W. 5th St.";
            this.m_Address2 = null;
            this.m_City = "Sheridan";
            this.m_State = "WY";
            this.m_ZipCode = "82801";
            this.m_PhoneNumber = "(307)672-1040";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;


            //this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
