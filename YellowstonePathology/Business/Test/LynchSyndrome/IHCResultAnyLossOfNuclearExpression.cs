using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class IHCResultAnyLossOfNuclearExpression :IHCResult
    {
        public IHCResultAnyLossOfNuclearExpression()
        {
            this.m_ResultCode = "LNCHIHC07";
            this.m_MLH1Result = new LSEIHCResultAnyLossOfExpression();
            this.m_MSH2Result = new LSEIHCResultAnyLossOfExpression();
            this.m_MSH6Result = new LSEIHCResultAnyLossOfExpression();
            this.m_PMS2Result = new LSEIHCResultAnyLossOfExpression();
        }
    }
}
