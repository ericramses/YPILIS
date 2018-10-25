﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultGroup2 : HER2AmplificationResult
    {
        private bool m_RequiresBlindedObserver; 

        public HER2AmplificationResultGroup2(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        {
            this.m_Interpretation = "Evidence is limited on the efficacy of human epidermal growth factor 2 (HER2)-targeted therapy in the " +
            "small subset of cases with a HER2/chromosome enumeration probe 17 (CEP17) ratio ≥ 2.0 and an average HER2 copy number " +
            "of <4.0 per cell. In the first generation of adjuvant trastuzumab trials, patients in this subgroup who were randomly " +
            "assigned to the trastuzumab arm did not seem to derive an improvement in disease-free or overall survival, but there were " +
            "too few such cases to draw definitive conclusions. Immunohistochemistry (IHC) expression for HER2 should be used to " +
            "complement in situ hybridization (ISH) and define HER2 status. If the IHC result is not 3+ positive, it is recommended " +
            "that the specimen be considered HER2 negative because of the low HER2 copy number by ISH and the lack of protein expression.";
        }

        public override void IsAMatch(HER2AmplificationResultMatch her2AmplificationResultMatch)
        {
            if (this.HER2CEP17Ratio >= 2 && this.AverageHER2CopyNo < 4.0)
            {
                her2AmplificationResultMatch.IsAMatch = true;

                if (this.m_HER2ByIHCIsOrdered == true && this.m_HER2ByIHCIsAccepted == true)
                {
                    if(this.m_HER2ByIHCScore.Contains("0") || this.m_HER2ByIHCScore.Contains("1+"))
                    {
                        her2AmplificationResultMatch.Result = HER2AmplificationResultEnum.Negative;
                    }
                }
                else if (this.m_HER2ByIHCScore.Contains("2+"))
                {
                    this.m_RequiresBlindedObserver = true;
                }
                else if(this.m_HER2ByIHCScore.Contains("3+"))
                {
                    her2AmplificationResultMatch.Result = HER2AmplificationResultEnum.Positive;
                }
            }
        }
    }
}
