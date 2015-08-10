using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing
{
	public class BillingRuleMatchItemList : List<BillingRuleMatch>
	{

		public BillingRuleMatchItemList(string patientType, string primaryInsurance, string secondaryInsurance, bool postDischarge)
		{
			BillingRuleMatch patientTypeMatchItem = new BillingRuleMatch("PatientTpye", patientType);
			this.Add(patientTypeMatchItem);

			BillingRuleMatch primaryInsuranceMatchItem = new BillingRuleMatch("PrimaryInsurance", primaryInsurance);
			this.Add(primaryInsuranceMatchItem);

			BillingRuleMatch secondaryInsuranceMatchItem = new BillingRuleMatch("SecondaryInsurance", secondaryInsurance);
			this.Add(secondaryInsuranceMatchItem);

			BillingRuleMatch postDischargeMatchItem = new BillingRuleMatch("PostDischarge", postDischarge.ToString());
			this.Add(postDischargeMatchItem);
		}

		public bool Matches()
		{
			bool result = true;
			foreach (BillingRuleMatch billingRuleMatch in this)
			{
				if (billingRuleMatch.Matches == false)
				{
					result = false;
					break;
				}
			}
			return result;
		}
	}
}
