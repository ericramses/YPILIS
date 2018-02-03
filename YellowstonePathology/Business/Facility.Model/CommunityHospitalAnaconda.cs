using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CommunityHospitalAnaconda : Facility
    {
        public CommunityHospitalAnaconda()
        {
            this.m_FacilityId = "CMMHSPTLCND";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Community Hospital of Anaconda";
            this.m_Address1 = "305 West Pennsylvania";
            this.m_Address2 = null;
            this.m_City = "Anaconda";
            this.m_State = "MT";
            this.m_ZipCode = "59711";
            this.m_PhoneNumber = "406-563-8606";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
