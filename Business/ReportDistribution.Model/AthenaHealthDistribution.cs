using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class AthenaHealthDistribution : Distribution
    {
        public const string DistributionType = "Athena Health";

        public AthenaHealthDistribution()
            : base(DistributionType)
        {

        }

        public override YellowstonePathology.Business.Rules.MethodResult Distribute(string reportNo)
        {
            YellowstonePathology.Business.HL7View.CMMC.CMMCResultView cmmcResultView = new HL7View.CMMC.CMMCResultView(reportNo);            
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            cmmcResultView.Send(result);            
            return result;
        }
    }
}
