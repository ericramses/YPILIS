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
        }
    }
}
