using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ComprehensiveColonCancerProfileFinalAudit : Audit
    {
        private YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileResult m_ComprehensiveColonCancerProfileResult;

        public ComprehensiveColonCancerProfileFinalAudit(YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            this.m_ComprehensiveColonCancerProfileResult = comprehensiveColonCancerProfileResult;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();
            if(this.m_ComprehensiveColonCancerProfileResult.LSEIHCIsOrdered == true &&
                this.m_ComprehensiveColonCancerProfileResult.BRAFV600EKIsOrdered == false &&
                this.m_ComprehensiveColonCancerProfileResult.KRASExon23MutationIsOrdered == false &&
                this.m_ComprehensiveColonCancerProfileResult.KRASExon4MutationIsOrdered == false &&
                this.m_ComprehensiveColonCancerProfileResult.KRASStandardIsOrderd == false &&
                this.m_ComprehensiveColonCancerProfileResult.MLHIsOrdered == false &&
                this.m_ComprehensiveColonCancerProfileResult.NRASMutationAnalysisIsOrdered == false &&
                this.m_ComprehensiveColonCancerProfileResult.RASRAFIsOrdered == false)
            {
                this.m_Status = AuditStatusEnum.Warning;
                this.m_Message.Append("This Comprehensive Colon Cancer Profile Test is not necessary. Should it be removed?");
            }
        }
    }
}
