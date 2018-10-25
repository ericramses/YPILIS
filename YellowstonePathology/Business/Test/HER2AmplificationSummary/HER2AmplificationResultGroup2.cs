using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultGroup2 : HER2AmplificationResult
    {

        public HER2AmplificationResultGroup2(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        { }

        public override void IsAMatch(HER2AmplificationResultMatch her2AmplificationResultMatch)
        {
            if (this.HER2CEP17Ratio >= 2 && this.AverageHER2CopyNo < 4.0)
            {
                her2AmplificationResultMatch.IsAMatch = true;
                if(this.m_HER2ByIHCIsOrdered == true)
                {
                    if(this.m_HER2ByIHCIsAccepted == true && (this.m_HER2ByIHCScore == "0" || this.m_HER2ByIHCScore == "1+"))
                    {
                        her2AmplificationResultMatch.Result = HER2AmplificationResultEnum.Negative;
                    }
                }
                else
                {
                    this.m_HER2ByIHCRequired = true;
                }
            }
        }
    }
}
