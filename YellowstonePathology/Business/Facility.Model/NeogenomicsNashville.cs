
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NeogenomicsNashville : Facility
    {        
        public NeogenomicsNashville()
        {
            this.m_FacilityId = "NEOGNMCNSHVLL";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Neogenomics Nashville";
            this.m_Address1 = "618 Grassmere Park";
            this.m_Address2 = null;
            this.m_City = "Nashville";
            this.m_State = "TN";
            this.m_ZipCode = "37211";
            this.m_PhoneNumber = "(866)776-5907";
            this.m_FedexAccountNo = "245909101";
            this.m_FedexPaymentType = "THIRD_PARTY";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, "05D1065194");            
        }
    }
}
