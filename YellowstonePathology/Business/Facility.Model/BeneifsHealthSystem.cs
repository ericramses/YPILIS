using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Facility.Model
{
    public class BeneifsHealthSystem : Facility
    {
        public BeneifsHealthSystem()
        {
            this.m_FacilityId = "BNFSHLTHSSTM";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Benefis Hospitals Pathology Department";
            this.m_Address1 = "Attn: Kelly Oleson";
            this.m_Address2 = " 1101 26th St. South";
            this.m_City = "Great Falls";
            this.m_State = "MT";
            this.m_ZipCode = "59405";
            this.m_PhoneNumber = "(406)455-4450";
            this.m_FedexAccountNo = "191009622";
            this.m_FedexPaymentType = "RECIPIENT";

            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
