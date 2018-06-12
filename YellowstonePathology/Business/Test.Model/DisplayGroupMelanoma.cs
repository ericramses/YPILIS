using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupMelanoma : DisplayGroupIHC
    {
        public DisplayGroupMelanoma()
        {
            this.m_GroupName = "Melanoma";
            
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("152")); // S100());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("119")); // MelanA());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("111")); // HMB45());            
        }
    }
}
