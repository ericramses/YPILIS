using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultGroup2 : HER2AmplificationResult
    {
        private bool m_IHCIsOrdered;

        public HER2AmplificationResultGroup2(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        { }

        public override void IsAMatch(HER2AmplificationResultMatch her2AmplificationResultMatch)
        {
            if (this.AverageHER2CopyNo < 4.0 && this.HER2CEP17Ratio >= 2)
            {
                Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
                if(this.m_PanelSetOrderCollection.Exists(her2AmplificationByIHCTest.PanelSetId) == true)
                {
                    this.m_IHCIsOrdered = true;
                    Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC panelSetOrderHer2AmplificationByIHC = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_PanelSetOrderCollection.GetPanelSetOrder(her2AmplificationByIHCTest.PanelSetId);
                    if(panelSetOrderHer2AmplificationByIHC.Accepted == true)
                    {
                        this.m_HER2ByIHCScore = panelSetOrderHer2AmplificationByIHC.Score;
                    }
                }
                her2AmplificationResultMatch.IsAMatch = true;
                her2AmplificationResultMatch.Result = HER2AmplificationResultEnum.Positive;
            }
        }
    }
}
