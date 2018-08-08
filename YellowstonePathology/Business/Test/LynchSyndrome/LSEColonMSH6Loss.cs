using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColonMSH6Loss : LSEResult
	{
		public LSEColonMSH6Loss()
		{
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Intact;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Loss;
			this.m_PMS2Result = LSEResultEnum.Intact;
			this.m_BrafResult = LSEResultEnum.NotPerformed;
			this.m_MethResult = LSEResultEnum.NotPerformed;
            this.m_BRAFIsIndicated = false;
			
			this.m_Comment = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MSH6 or MSH2 mutations.  Recommend genetic counseling and further evaluation.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
