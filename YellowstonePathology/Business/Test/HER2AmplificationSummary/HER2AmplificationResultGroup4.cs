using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultGroup4 : HER2AmplificationResult
    {
        public HER2AmplificationResultGroup4(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        {
            this.m_Interpretation = "It is uncertain whether patients with an average of ≥ 4.0 and < 6.0 human epidermal growth factor 2 " +
                "receptor(HER2) signals per cell and HER2 / chromosome enumeration probe 17(CEP17) ratio of < 2.0 benefit from HER2 - " +
                "targeted therapy in the absence of protein overexpression(immunohistochemistry[IHC] 3 +).  If the specimen test result is " +
                "close to the in situ hybridization (ISH)ratio threshold for positive, there is a higher likelihood that repeat testing will " +
                "result in different results by chance alone.  Therefore, when IHC results are not 3 + positive, it is recommended that the " +
                "sample be considered HER2 negative without additional testing on the same specimen.";
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.HER2CEP17Ratio >= 4.0 && this.AverageHER2CopyNo < 6.0)
            {
                result = true;

                this.HandleIHC();
            }
            return result;
        }
    }
}
