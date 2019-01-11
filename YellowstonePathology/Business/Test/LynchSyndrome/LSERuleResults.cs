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
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation m_PanelSetOrderLynchSyndromeEvaluation;

        public LSERuleResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation, LSERuleCollection lseRuleCollection)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrderLynchSyndromeEvaluation = panelSetOrderLynchSyndromeEvaluation;
            this.m_LSERuleCollection = lseRuleCollection;
            this.m_AbleToContinue = true;
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

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation PanelSetOrderLynchSyndromeEvaluation
        {
            get { return this.m_PanelSetOrderLynchSyndromeEvaluation; }
        }
    }
}
