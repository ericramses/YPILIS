using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColonPMS2Loss : LSERule
    {
		public LSEColonPMS2Loss()
		{
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Intact;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Loss;

            this.m_AdditionalTesting = LSERule.AdditionalTestingNone;
			this.m_Interpretation = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MLH1 mutations.  Recommend genetic counseling and further evaluation.";
            this.m_Method = IHCBRAFMLHMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
