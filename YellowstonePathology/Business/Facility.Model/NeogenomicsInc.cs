using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NeogenomicsInc : Facility
    {        
        public NeogenomicsInc()
        {
            this.m_FacilityId = "NEOGNMCINC";
            this.m_FacilityIdOLD = "null";
            this.m_FacilityName = "Neogenomics Inc.";
            this.m_Address1 = "618 Grassmere Street";
            this.m_Address2 = null;
            this.m_City = "Nashville";
            this.m_State = "TN";
            this.m_ZipCode = "37211";
            this.m_PhoneNumber = "(239)768-0600";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
