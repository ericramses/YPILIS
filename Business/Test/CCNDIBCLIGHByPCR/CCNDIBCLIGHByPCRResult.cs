using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR
{
    public class CCNDIBCLIGHByPCRResult
    {
        public static string NucleiScored = "200";
        protected string m_Result;
        protected string m_Interpretation;
        protected string m_ProbeSetDetail;
        protected string m_References;

        public CCNDIBCLIGHByPCRResult()
        {
        }

        public void SetResults(CCNDIBCLIGHByPCRTestOrder panelSetOrder)
        {
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.ProbeSetDetail = this.m_ProbeSetDetail;
            panelSetOrder.References = this.m_References;
            panelSetOrder.NucleiScored = CCNDIBCLIGHByPCRResult.NucleiScored;
        }
    }
}
