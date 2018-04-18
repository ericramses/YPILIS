using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class AnschutzPathologyUCH : Facility
    {
        public AnschutzPathologyUCH()
        {
            this.m_FacilityId = "APUCH";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Anschutz Inpatient Pavilion - Pathology; University Of Colorado Hospital";
            this.m_Address1 = "12605 Aurora Court";
            this.m_Address2 = "Suite 3.003";
            this.m_City = "Aurora";
            this.m_State = "CO";
            this.m_ZipCode = "80045";
            this.m_PhoneNumber = "(720)848-4421";
            this.m_FedexAccountNo = "478927088";
            this.m_FedexPaymentType = "SENDER";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
