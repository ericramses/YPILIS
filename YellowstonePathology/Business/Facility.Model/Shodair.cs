using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class Showdair : Facility
    {
        public Showdair()
        {
            this.m_FacilityId = "SHDR";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Shodair Children's Hospital - Department of Medical Genetics";
            this.m_Address1 = "2755 Colonial Drive";
            this.m_Address2 = null;
            this.m_City = "Helena";
            this.m_State = "MT";
            this.m_ZipCode = "59601";
            this.m_PhoneNumber = "(406)444-7532";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);
		}
    }
}
