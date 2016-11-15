using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NationalCancerInstitute : Facility
    {
        public NationalCancerInstitute()
        {
            this.m_FacilityId = "NCI";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "National Cancer Institute";
            this.m_Address1 = "10 center Drive";
            this.m_Address2 = null;
            this.m_City = "Bethesda";
            this.m_State = "MD";
            this.m_ZipCode = "20892";
            this.m_PhoneNumber = "(301)827-0022";
            this.m_FedexAccountNo = "236812405";
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = true;                        
        }
    }
}
