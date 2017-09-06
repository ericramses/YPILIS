using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Model
{
    public class KappaByISHLambdaByISH : ImmunoHistochemistryTest
    {
        public KappaByISHLambdaByISH()
        {
            this.m_TestId = 360;
            this.m_TestName = "Kappa by ISH/Lambda by ISH";
            this.m_TestAbbreviation = "Kappa/LambdaByISH";
            this.m_Active = true;
            this.m_NeedsAcknowledgement = true;
        }
    }
}
