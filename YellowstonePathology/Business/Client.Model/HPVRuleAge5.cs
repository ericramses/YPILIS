using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAge5 : HPVRuleAge
    {
        public HPVRuleAge5()
        {
            this.m_AgeDescription = "between 21 and 29 years old";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = accessionOrder.PBirthdate >= DateTime.Today.AddYears(-29) && accessionOrder.PBirthdate <= DateTime.Today.AddYears(-21) ? true : false;

            return result;
        }
    }
}
