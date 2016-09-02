using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class FoundationOne : Facility
    {
        public FoundationOne()
        {
            this.m_FacilityId = "FNDONE";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Foundation One";
            this.m_Address1 = "7010 Kit Creek Rd";
            this.m_City = "Billings";
            this.m_State = "MT";
            this.m_ZipCode = "27560";
            this.m_IsReferenceLab = true;                        
        }
    }
}
