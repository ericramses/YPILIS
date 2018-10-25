using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultGroup5 : HER2AmplificationResult
    {

        public HER2AmplificationResultGroup5(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        {
        }

        public override void IsAMatch(HER2AmplificationResultMatch her2AmplificationResultMatch)
        {
            if (this.HER2CEP17Ratio < 2.0)
            {
                her2AmplificationResultMatch.IsAMatch = true;
                her2AmplificationResultMatch.Result = HER2AmplificationResultEnum.Negative;
            }
        }
    }
}
