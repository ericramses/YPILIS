using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
	public class MontanaDermatology : Facility
	{        
		public MontanaDermatology()
		{
            this.m_FacilityId = "MTDRMTLGY";
            this.m_IsReferenceLab = true;
            this.m_FacilityName = "Montana Dermatology and Skin Cancer Center - SV Clinic";
            this.Address1 = "2900 12th Avenue North";
            this.m_Address2 = "Suite 265W";
            this.m_City = "Billings";
            this.m_State = "MT";
            this.m_ZipCode = "59101";
            this.m_PhoneNumber = "(406)237-7999";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;
            this.m_IsReferenceLab = false;

			//this.m_CliaLicense = new CLIALicense(this, null);
		}
	}
}
