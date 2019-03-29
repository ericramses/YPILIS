using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAgeCollection : ObservableCollection<HPVRuleAge>
    {
        public HPVRuleAgeCollection()
        { }

        public static HPVRuleAgeCollection GetAll()
        {
            HPVRuleAgeCollection result = new Model.HPVRuleAgeCollection();
            result.Add(new Model.HPVRuleAge1());
            result.Add(new Model.HPVRuleAge2());
            result.Add(new Model.HPVRuleAge3());
            result.Add(new Model.HPVRuleAge4());
            result.Add(new Model.HPVRuleAge5());
            return result;
        }

        public bool ExixtsByAgeDescription(string ageDescription)
        {
            HPVRuleAge result = this.FirstOrDefault(x => x.AgeDescription == ageDescription);
            return result == null ? false : true;
        }

        public HPVRuleAge GetByAgeDescription(string ageDescription)
        {
            HPVRuleAge result = this.FirstOrDefault(x => x.AgeDescription == ageDescription);
            return result;
        }

        public HPVRuleAgeCollection SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            HPVRuleAgeCollection result = new Model.HPVRuleAgeCollection();
            foreach (HPVRuleAge rule in this)
            {
                if (rule.SatisfiesCondition(accessionOrder) == true)
                {
                    result.Add(rule);
                }
            }
            return result;
        }
    }
}
