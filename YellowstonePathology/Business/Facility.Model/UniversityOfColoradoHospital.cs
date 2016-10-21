using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniversityOfColoradoHospital : Facility
    {
        public UniversityOfColoradoHospital()
        {
            this.m_FacilityId = "UOCH";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "University Of Colorado Hospital";
            this.m_Address1 = "1665 Aurora Court";
            this.m_Address2 = "Suite 3070";
            this.m_City = "Aurora";
            this.m_State = "CO";
            this.m_ZipCode = "80045";
            this.m_PhoneNumber = "(720)848-0590";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = "SENDER";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
