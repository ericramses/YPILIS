using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColonMLH1Loss : LSERule
	{
		public LSEColonMLH1Loss()
		{
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_AdditionalTesting = LSERule.AdditionalTestingReflexBRAFMeth;			
			this.m_Interpretation = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline PMS2 or MLH1 mutations.  " +
                "Recommend genetic counseling and further evaluation.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
