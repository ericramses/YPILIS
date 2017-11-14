using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CarisLifeSciences : Facility
    {
        public CarisLifeSciences()
        {
            this.m_FacilityId = "CRSLFS";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Caris Life Sciences";
            this.m_Address1 = "4610 south 44th Place, Suite 100";
            this.m_City = "Phoenix";
            this.m_State = "AZ";
            this.m_ZipCode = "85040";
            this.m_PhoneNumber = "888-979- 8669";
            this.m_FedexAccountNo = "310178438";
            this.m_FedexPaymentType = "RECIPIENT";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
