using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Model
{
    public class LambdaByISH : ImmunoHistochemistryTest
    {
        public LambdaByISH()
        {
            this.m_TestId = "361";
            this.m_TestName = "Lambda by ISH";
            this.m_TestAbbreviation = "Lambda";
            this.m_Active = true;
            this.m_NeedsAcknowledgement = true;
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode code =  Business.Billing.Model.CptCodeCollection.GetCptCode("88364");
            return code;
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(CptCodeLevelEnum cptCodeLevel, bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode code = Business.Billing.Model.CptCodeCollection.GetCptCode("88364");
            return code;
        }
    }
}
