using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Domain.Test.FactorVLeiden
{
	public class FactorVLeidenInsufficientResult : FactorVLeidenResult
	{
		public FactorVLeidenInsufficientResult()
		{
			this.m_Result = "NO RESULT";
			this.m_ResultCode = "FCTVLDNQNS";
			this.m_ResultDescription = "Insufficient DNA for analysis (see interpretation).";
			this.m_Interpretation = "Hypercoagulability typically results from a combination of acquired and heritable risk factors.  Among the many " +
				"well documented heritable risk factors, mutation of the Factor V gene is the most common, occurring in up to 3% of the general " +
				"population and in up to 50% of patients with recurrent thromboembolic events.  The mutation (commonly referred to as Factor V Leiden), " +
				"which is characterized by a substitution of glutamine for arginine at position 506 (R506Q), alters a site on the factor V protein that " +
				"is normally cleaved by activated protein C, thereby causing impaired clot degradation.  Studies have demonstrated that individuals who " +
				"harbor heterozygous Factor V Leiden mutations are 5 to 10 times more likely to experience a thromboembolic event when compared to the " +
				"general population, while homozygous Factor V Leiden carriers are at significantly higher risk (50 to 80-fold).  Knowledge of a patient’s " +
				"Factor V Leiden mutation status is valuable, as it may influence treatment regimens as well as allow for timely pre-operative and genetic " +
				"counseling.  Molecular analysis for the Factor V Leiden (R506Q) mutation could not be performed due to insufficient DNA in the patient’s " +
				"sample.  Submission of a new sample is recommended.";
		}
	}
}
