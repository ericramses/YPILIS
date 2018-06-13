using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupInfectiousDiseases : DisplayGroupIHC
    {
        public DisplayGroupInfectiousDiseases()
        {
            this.m_GroupName = "Infectious Diseases";
            
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetTest("107")); // HelicobacterPylori());
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetTest("350")); // CytomegaloVirus());            
        }
    }
}
