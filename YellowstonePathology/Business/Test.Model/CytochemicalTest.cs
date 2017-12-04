using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CytochemicalTest : Test
    {        

		public CytochemicalTest()
		{
			this.m_IsBillable = true;            
		}
		
		public CytochemicalTest(string testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = true;            
        }        

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode result = Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88313");
            if (isTechnicalOnly == true)
            {
                result = Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88313tc");
            }
            return result;            
        }        

        public override string GetCodeableType(bool orderedAsDual)
        {
            return YellowstonePathology.Business.Billing.Model.CodeableType.CYTOCHMCL;
        }       
    }
}
