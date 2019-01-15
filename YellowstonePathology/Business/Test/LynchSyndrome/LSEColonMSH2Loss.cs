using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColonMSH2Loss : LSERule
	{
		public LSEColonMSH2Loss()
		{            
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Intact;
			this.m_MSH2Result = LSEResultEnum.Loss;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_BRAFRequired = false;
            this.m_MethRequired = false;
            this.m_Interpretation = "This staining pattern is extremely rare and may be due to an MSH2 gene mutation.  Recommend genetic counseling and further evaluation to exclude Lynch Syndrome. ";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
