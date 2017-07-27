using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisIndeterminateResult : BRAFMutationAnalysisResult
    {
        public BRAFMutationAnalysisIndeterminateResult()
        {
            this.m_ResultCode = "BRAFMTTNANLNDTRMNT";
            this.m_Result = "Indeterminate";
        }
    }
}