using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    class HER2AmplificationResultGroup3 : HER2AmplificationResult
    {
        public HER2AmplificationResultGroup3(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        {
            this.m_Interpretation = "There are insufficient data on the efficacy of human epidermal growth factor receptor 2 (HER2)-targeted " +
                "therapy in cases with a HER2 ratio of <2.0 in the absence of protein overexpression because such patients were not eligible " +
                "for the first generation of adjuvant trastuzumab clinical trials. When concurrent immunohistochemistry (IHC) results are " +
                "negative (0 or 1+), it is recommended that the specimen be considered HER2 negative.";
        }

        public override void IsAMatch(HER2AmplificationResultMatch her2AmplificationResultMatch)
        {
            if (this.HER2CEP17Ratio < 2 && this.AverageHER2CopyNo >= 6.0)
            {
                her2AmplificationResultMatch.IsAMatch = true;

                this.HandleIHC(her2AmplificationResultMatch);
            }
        }
    }
}
