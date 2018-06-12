using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupColon : DisplayGroupIHC
    {
        public DisplayGroupColon()
        {
            this.m_GroupName = "Colon";
            
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("81")); // CDX2());
            this.m_List.Add(new Villin());            
        }
    }
}
