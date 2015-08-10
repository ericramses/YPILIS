using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract.Search
{
    public enum SearchTypeEnum
    {
        PatientLastNameSearch,        
        PatientLastAndFirstNameSearch,        
        RecentCases,        
        NotDownloaded,        
        DateOfBirth,        
        SocialSecurityNumber,        
        PhysicianId,
        NotAcknowledged,
		RecentCasesForFacilityId
    }
}
