using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class PathologyAssociatesOfIdahoFalls : Facility
    {
        public PathologyAssociatesOfIdahoFalls()
        {
            this.m_FacilityId = "PAOIF";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Pathology Associates Of Idaho Falls";
            this.m_Address1 = "3100 Channing Way";
            this.m_Address2 = null;
            this.m_City = "Idaho Falls";
            this.m_State = "ID";
            this.m_ZipCode = "83404";
            this.m_PhoneNumber = null;
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = false;
            this.m_ClientId = 1201;
	
            //this.m_CliaLicense = new CLIALicense(this, string.Empty);            
        }
    }
}
