using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class BozemanDeaconess : Facility
    {
        public BozemanDeaconess()
        {
            this.m_FacilityId = "BZMNDCNSS";
            this.m_FacilityName = "Bozeman Deaconess";
            this.m_Address1 = "915 Highland Boulevard";
            this.m_City = "Bozeman";
            this.m_State = "MT";
            this.m_ZipCode = "59715";
            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
