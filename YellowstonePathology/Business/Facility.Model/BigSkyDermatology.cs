using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
	public class BigSkyDermatology : Facility
	{
		public BigSkyDermatology()
		{
			this.m_FacilityId = "BGSKDRMTLGY";
			this.m_FacilityName = "Big Sky Dermatology";
            this.m_Address1 = "4515 Valley Commons";
            this.m_Address2 = "#202";
            this.m_City = "Bozeman";
            this.m_State = "MT";
            this.m_ZipCode = "59718";
            this.m_IsReferenceLab = false;
			
			this.m_CliaLicense = new CLIALicense(this, string.Empty);
		}
	}
}
