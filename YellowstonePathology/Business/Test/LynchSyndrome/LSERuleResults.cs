using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSERuleResults
    {
        private LSERuleCollection m_LSERuleCollection;
        private bool m_AbleToContinue;

        public LSERuleResults()
        { }

        public LSERuleResults(LSERuleCollection lseRuleCollection, bool ableToContinue)
        {
            this.m_LSERuleCollection = lseRuleCollection;
            this.m_AbleToContinue = ableToContinue;
        }

        public LSERuleCollection LSERuleCollection
        {
            get { return this.m_LSERuleCollection; }
            set { this.m_LSERuleCollection = value; }
        }

        public bool AbleToContinue
        {
            get { return this.m_AbleToContinue; }
            set { this.m_AbleToContinue = value; }
        }
    }
}
