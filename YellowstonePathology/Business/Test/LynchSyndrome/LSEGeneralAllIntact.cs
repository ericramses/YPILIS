using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGeneralAllIntact : LSERule
    {

		public LSEGeneralAllIntact()
		{
            this.m_ResultName = "All Intact";
            this.m_Indication = LSEType.GENERAL;

            this.m_Result = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";
            this.m_Interpretation = "Mismatch repair protein expression is intact, indicating that the tumor is unlikely to respond to PD-1 blockade therapy.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;
		}

        public override bool IsIHCMatch(IHCResult ihcResult)
        {
            bool result = false;
            if (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.MSH2Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.MSH6Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.PMS2Result.LSEResult == LSEResultEnum.Intact)
            {
                result = true;
            }
            return result;
        }
    }
}
