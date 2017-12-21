using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CytochemicalForMicroorganisms : Test
    {
		public CytochemicalForMicroorganisms()
		{
			this.m_IsBillable = true;
			this.m_HasGCode = false;
			this.m_HasCptCodeLevels = false;            
		}

		public CytochemicalForMicroorganisms(string testId, string testName)  
            : base(testId, testName)
        {
            this.m_IsBillable = true;
            this.m_HasGCode = false;
            this.m_HasCptCodeLevels = false;
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode result = Business.Billing.Model.CptCodeCollection.Instance.GetClone("88312", null);
            if (isTechnicalOnly == true)
            {
                result = Business.Billing.Model.CptCodeCollection.Instance.GetClone("88312", "TC");
            }
            return result;   
        }

        public override string GetCodeableType(bool orderedAsDual)
        {
            return YellowstonePathology.Business.Billing.Model.CodeableType.CYTOCHMCLMO;
        }
    }
}
