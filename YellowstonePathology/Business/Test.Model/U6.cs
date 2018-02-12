using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Model
{
    public class U6 : ImmunoHistochemistryTest
    {
        public U6()
        {
            this.m_TestId = "383";
            this.m_TestName = "U6";
            this.m_TestAbbreviation = "U6";
            this.m_Active = true;
            this.m_NeedsAcknowledgement = true;
        }
    }
}
