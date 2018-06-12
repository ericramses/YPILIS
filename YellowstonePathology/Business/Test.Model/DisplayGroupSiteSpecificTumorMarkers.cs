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

            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("270")); // NapsinA());            
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("161")); // TTF1());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("59")); // CA199());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("58")); // CA125());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("60")); // Calretinin());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("82")); // CEA());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("142")); // PlacentalAlkalinePhosphatase());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("149")); // RCC());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("197")); // HepatocyteSpecificAntigen());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("253")); // PAX8());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("276")); // Glypican3());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("346")); // GATA3());
        }
    }
}
