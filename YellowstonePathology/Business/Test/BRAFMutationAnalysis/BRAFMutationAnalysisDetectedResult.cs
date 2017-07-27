using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisDetectedResult : BRAFMutationAnalysisResult
    {
        public BRAFMutationAnalysisDetectedResult()
        {
            this.m_ResultCode = "BRAFMTTNANLDTCTD";
            this.m_Result = "Detected";
        }
    }
}