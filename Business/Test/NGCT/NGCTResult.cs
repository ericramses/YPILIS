using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTResult : YellowstonePathology.Business.Test.TestResult
	{
		protected string m_Method;
		protected string m_References;
		protected string m_TestDevelopment;

		public NGCTResult()
		{
			this.m_Method = "DNA was extracted from the patient’s specimen using an automated method.  Real time PCR amplification was " +
				"performed for organism detection and identification.";
			this.m_References = "Whiley DM.  Comparison of three in-house multiplex PCR assays for the detection of Neisseria gonorrhoeae and Chlamydia trachomatis " +
				"using real-time and conventional detection methodologies. Pathology (October 2005) 37(5), pp. 364–370.";
			this.m_TestDevelopment = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not " +
				"been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This " +
				"test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical " +
				"Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
		}

		public void SetResults(NGCTTestOrder testOrder)
		{
			testOrder.Method = this.m_Method;
			testOrder.References = this.m_References;
			testOrder.TestDevelopment = this.m_TestDevelopment;
		}

		public void AcceptResults(NGCTTestOrder testOrder, Business.User.SystemUser user)
		{
			foreach(YellowstonePathology.Business.Test.PanelOrder panelOrder in testOrder.PanelOrderCollection)
			{
				panelOrder.AcceptResults(user);
				panelOrder.NotifyPropertyChanged("AcceptedBy");
			}
			testOrder.Accept(user);
		}
	}
}
