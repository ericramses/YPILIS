using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupNeural : DisplayGroupIHC
    {
        public DisplayGroupNeural()
        {
            this.m_GroupName = "Neural";
            
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetTest("77")); // CD56());
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetTest("152")); // S100());            
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetTest("83")); // Chromogranin());
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetTest("156")); // Synaptophysin());
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetTest("104")); // GFAP());            
        }
    }
}
