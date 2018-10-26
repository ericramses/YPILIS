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

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.HER2CEP17Ratio >= 2.0 && this.AverageHER2CopyNo >= 4.0)
            {
                result = true;
                this.m_Result = HER2AmplificationResultEnum.Positive;
            }
            return result;
        }
    }
}
