using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class RASRAFPanelResult
    {
        protected const string m_References = "Not Set";
        protected const string m_BRAFReferences = "";
        protected const string m_TestDevelopment = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has " +
            "not been approved by the FDA. The FDA has determined such clearance or approval is not necessary. This laboratory is CLIA certified to perform high " +
            "complexity clinical testing.";

        public RASRAFPanelResult()
        {

        }

        public void SetResults(RASRAFPanelTestOrder testOrder)
        {
        	StringBuilder references = new StringBuilder();
        	if(testOrder.BRAFResult == "Detected")
        	{
        		references.AppendLine(m_BRAFReferences);
        	}
        	if(testOrder.KRASResult == "Detected")
        	{
        		references.AppendLine( KRASExon4Mutation.KRASExon4MutationResult.m_References);
        	}
        	if(testOrder.NRASResult == "Detected")
        	{
        		references.AppendLine(m_BRAFReferences);
        	}
        	if(testOrder.HRASResult == "Detected")
        	{
        		references.AppendLine(m_BRAFReferences);
        	}
        	testOrder.References = references.ToString().Trim();

            StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(testOrder.GetLocationPerformedComment());
            disclaimer.Append(m_TestDevelopment);
            testOrder.ReportDisclaimer = disclaimer.ToString();
        }
    }
}
