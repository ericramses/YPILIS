using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    class LSEIHCResultAnyLossOfExpression : LSEIHCResult
    {
        public LSEIHCResultAnyLossOfExpression()
        {
            this.m_Description = "Loss of nuclear expression";
            this.m_LSEResult = LSEResultEnum.AnyLoss;
        }
    }
}
