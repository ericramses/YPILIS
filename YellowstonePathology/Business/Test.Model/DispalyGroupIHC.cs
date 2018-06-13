using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupIHC
    {
        protected string m_GroupName;
        protected List<ImmunoHistochemistryTest> m_List;
        protected YellowstonePathology.Business.Test.Model.TestCollection m_AllTests;

        public DisplayGroupIHC()
        {
            this.m_List = new List<ImmunoHistochemistryTest>();
            this.m_AllTests = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();
        }

        public string GroupName
        {
            get { return this.m_GroupName; }
        }

        public List<ImmunoHistochemistryTest> List
        {
            get { return this.m_List; }
        }
    }
}
