using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupBreast : DisplayGroupIHC
    {
        public DisplayGroupBreast()
        {
            this.m_GroupName = "Breast";
            
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("98")); // EstrogenReceptor());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("144")); // ProgesteroneReceptor());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("267")); // HER2DISH());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("154")); // SmoothMuscleMyosin());
            this.m_List.Add((ImmunoHistochemistryTest)this.m_AllTests.GetTest("118")); // Mammaglobin());
            this.m_List.Add(new GCDFP15());                        
        }
    }
}
