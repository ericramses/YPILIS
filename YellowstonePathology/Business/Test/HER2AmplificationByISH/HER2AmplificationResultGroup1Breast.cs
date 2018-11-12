using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGroup1Breast : HER2AmplificationResultBreast
    {

        public HER2AmplificationResultGroup1Breast(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        {
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble.HasValue && 
                this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble >= 2.0 && 
                this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal.HasValue && 
                this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal >= 4.0)
            {
                result = true;
                this.m_Result = HER2AmplificationResultEnum.Positive;
            }
            return result;
        }
    }
}
