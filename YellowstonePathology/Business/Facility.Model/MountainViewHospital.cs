using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MountainViewHospital : Facility
    {        
        public MountainViewHospital()
        {
            this.m_FacilityId = "MVH";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "MountainViewHospital";
            this.m_Address1 = "2325 Coranado Street";
            this.m_Address2 = null;
            this.m_City = "Idaho Falls";
            this.m_State = "ID";
            this.m_ZipCode = "83404";
            this.m_PhoneNumber = "(208)557-2576";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = false;


			
			//this.m_CliaLicense = new CLIALicense(this, "05D1065194");            
        }
    }
}
