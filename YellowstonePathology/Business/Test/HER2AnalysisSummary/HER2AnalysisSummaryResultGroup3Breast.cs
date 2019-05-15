using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AnalysisSummary
{
    public class HER2AnalysisSummaryResultGroup3Breast : HER2AnalysisSummaryResult
    {
        public HER2AnalysisSummaryResultGroup3Breast(PanelSetOrderCollection panelSetOrderCollection, string reportNo) : base(panelSetOrderCollection, reportNo)
        {
            this.m_InterpretiveComment = "There are insufficient data on the efficacy of human epidermal growth factor receptor 2 (HER2)-targeted " +
                "therapy in cases with a HER2 ratio of < 2.0 in the absence of protein overexpression because such patients were not eligible " +
                "for the first generation of adjuvant trastuzumab clinical trials.  When concurrent immunohistochemistry (IHC) results are " +
                "negative (0 or 1+), it is recommended that the specimen be considered HER2 negative.";
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_HER2AnalysisSummaryTestOrder.Indicator == HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection.BreastIndication &&
                this.m_HER2AnalysisSummaryTestOrder.AverageHer2Chr17SignalAsDouble.HasValue &&
                this.m_HER2AnalysisSummaryTestOrder.AverageHer2Chr17SignalAsDouble < 2.0 &&
                this.m_HER2AnalysisSummaryTestOrder.AverageHer2NeuSignal.HasValue &&
                this.m_HER2AnalysisSummaryTestOrder.AverageHer2NeuSignal >= 6.0)
            {
                result = true;
            }
            return result;
        }

        public override void SetResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.HandleIHC();

            if (this.m_HER2AmplificationRecountTestOrder != null && this.m_HER2AmplificationRecountTestOrder.Accepted == true &&
                this.m_PanelSetOrderHer2AmplificationByIHC != null && this.m_PanelSetOrderHer2AmplificationByIHC.Accepted == true)
            {
                if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("2+"))
                {
                    this.m_Result = HER2AmplificationByISH.HER2AmplificationResultEnum.Positive;
                }
            }

            base.SetResults(specimenOrder);
        }
    }
}