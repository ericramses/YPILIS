using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAge1 : HPVRuleAge
    {
        public HPVRuleAge1()
        {
            this.m_AgeDescription = "Any";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return true;
        }
    }
}
