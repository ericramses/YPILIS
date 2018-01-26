using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisInsufficientResult : BRAFMutationAnalysisResult
    {
        public BRAFMutationAnalysisInsufficientResult()
        {
            this.m_ResultCode = "BRAFMTTNANLINS";
            this.m_Result = "Insufficient DNA to perform analysis";
        }
    }
}
