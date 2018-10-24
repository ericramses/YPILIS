using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultGroup1 : HER2AmplificationResult
    {

        public HER2AmplificationResultGroup1(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        {
        }

        public override void IsAMatch( HER2AmplificationResultMatch her2AmplificationResultMatch)
        {
            if (this.AverageHER2CopyNo >= 4.0 && this.HER2CEP17Ratio >= 2)
            {
                her2AmplificationResultMatch.IsAMatch = true;
                her2AmplificationResultMatch.Result = HER2AmplificationResultEnum.Positive;
            }
        }
    }
}
