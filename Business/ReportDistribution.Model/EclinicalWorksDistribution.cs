using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class EclinicalWorksDistribution : Distribution
    {
        public const string DistributionType = "Eclinical Works";

        public EclinicalWorksDistribution()
            : base(DistributionType)
        {

        }

        public override YellowstonePathology.Business.Rules.MethodResult Distribute(string reportNo, object writer)
        {                                    
            Business.HL7View.ECW.ECWResultView ecwResultView = new HL7View.ECW.ECWResultView(reportNo, false, writer);
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            ecwResultView.CanSend(result);                        
            return result;
        }
    }
}
