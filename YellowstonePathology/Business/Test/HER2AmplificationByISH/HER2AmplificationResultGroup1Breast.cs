using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Specimen.Model;

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
            if (this.m_HER2AmplificationByISHTestOrder.Indicator == HER2AmplificationByISHIndicatorCollection.BreastIndication &&
                this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble.HasValue && 
                this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble >= 2.0 && 
                this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal.HasValue && 
                this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal >= 4.0)
            {
                result = true;
                this.m_Result = HER2AmplificationResultEnum.Positive;
            }
            return result;
        }

        public override void SetResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_Interpretation = InterpretiveComment;
            this.m_Interpretation = this.m_Interpretation.Replace("*RATIO*", this.m_HER2AmplificationByISHTestOrder.Her2Chr17Ratio.Value.ToString());
            this.m_Interpretation = this.m_Interpretation.Replace("*CELLSCOUNTED*", this.m_HER2AmplificationByISHTestOrder.CellsCounted.ToString());
            this.m_Interpretation = this.m_Interpretation.Replace("*HER2STATUS*", this.m_Result.ToString());
            if (this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.m_Interpretation = this.m_InterpretiveComment.Replace("*HER2COPY*", this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal.Value.ToString());
            }

            base.SetResults(specimenOrder);
        }
    }
}
