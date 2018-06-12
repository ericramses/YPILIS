using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupSiteSpecificTumorMarkers : DisplayGroupIHC
    {
        public DisplayGroupSiteSpecificTumorMarkers()
        {
            this.m_GroupName = "Site Specific Tumor Markers";

            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("270")); // NapsinA());            
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("161")); // TTF1());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("59")); // CA199());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("58")); // CA125());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("60")); // Calretinin());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("82")); // CEA());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("142")); // PlacentalAlkalinePhosphatase());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("149")); // RCC());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("197")); // HepatocyteSpecificAntigen());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("253")); // PAX8());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("276")); // Glypican3());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("346")); // GATA3());
        }
    }
}
