﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class HospitalSplitSurgicalAndReferenceLabBillingRuleSet : BillingRuleSet
    {
        public HospitalSplitSurgicalAndReferenceLabBillingRuleSet()
        {            
            this.m_BillingRuleSetId = "HSSRGCLRL";
            this.m_BillingRuleSetIdOld = "ceb64127-9d7e-4f61-be5e-3fec67e00fb9";
            this.m_BillingRuleSetName = "Hospital Split Surgical And Reference Lab Rule Set";

            CptCodeCollection clinicalFeeScheduleCodes = Store.AppDataStore.Instance.CPTCodeCollection.GetCptCodeCollection(FeeScheduleEnum.Clinical);
            CptCodeCollection professionalFeeScheduleCodes = Store.AppDataStore.Instance.CPTCodeCollection.GetCptCodeCollection(FeeScheduleEnum.Physician);

            BillingRule billingRule0 = new BillingRule();
            billingRule0.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule0.Priority = 0;            
            billingRule0.PatientType = new RuleValueAny();
            billingRule0.PrimaryInsurance = new RuleValueAny();
            billingRule0.SecondaryInsurance = new RuleValueAny();
            billingRule0.PostDischarge = new RuleValueAny();
            billingRule0.BillingType = BillingTypeEnum.Split;
            billingRule0.ReferenceLab = new RuleValueAny();
            billingRule0.PanelSetIncludeOnlyList.Add(13);
            this.m_BillingRuleCollection.Add(billingRule0);            

            BillingRule billingRule01 = new BillingRule();
            billingRule01.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule01.Priority = 1;
            billingRule01.PatientType = new RuleValueAny();
            billingRule01.PrimaryInsurance = new RuleValueAny();
            billingRule01.SecondaryInsurance = new RuleValueAny();
            billingRule01.PostDischarge = new RuleValueAny();
            billingRule01.BillingType = BillingTypeEnum.Split;
            billingRule01.ReferenceLab = new RuleValueBoolean(true);            
            this.m_BillingRuleCollection.Add(billingRule01);

            BillingRule billingRule1 = new BillingRule();
            billingRule1.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule1.Priority = 2;            
            billingRule1.PatientType = new RuleValueAny();
            billingRule1.PrimaryInsurance = new RuleValueAny();
            billingRule1.SecondaryInsurance = new RuleValueAny();
            billingRule1.PostDischarge = new RuleValueAny();
            billingRule1.BillingType = BillingTypeEnum.Global;
            billingRule1.ReferenceLab = new RuleValueBoolean(false);
            billingRule1.PanelSetExcludeList.Add(13);
            this.m_BillingRuleCollection.Add(billingRule1);

            /*
            BillingRule billingRule10 = new BillingRule();
            billingRule10.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule10.Priority = 3;
            billingRule10.PatientType = new RuleValueAny();
            billingRule10.PrimaryInsurance = new RuleValueAny();
            billingRule10.SecondaryInsurance = new RuleValueAny();
            billingRule10.PostDischarge = new RuleValueAny();
            billingRule10.BillingType = BillingTypeEnum.Global;
            billingRule10.ReferenceLab = new RuleValueAny();
            billingRule10.PanelSetIncludeOnlyList.Add(248); //Flow Cytometry Analysis sent to NEO
            this.m_BillingRuleCollection.Add(billingRule10);
            */

            BillingRule billingRule2 = new BillingRule();
            billingRule2.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule2.Priority = 4;            
            billingRule2.PatientType = new RuleValueString("OP");
            billingRule2.PrimaryInsurance = new RuleValueString("Medicaid");
            billingRule2.SecondaryInsurance = new RuleValueAny();
            billingRule2.PostDischarge = new RuleValueAny();
            billingRule2.BillingType = BillingTypeEnum.Global;
            billingRule2.ReferenceLab = new RuleValueAny();
            this.m_BillingRuleCollection.Add(billingRule2);

            BillingRule billingRule3 = new BillingRule();
            billingRule3.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule3.Priority = 5;            
            billingRule3.PatientType = new RuleValueAny();
            billingRule3.PrimaryInsurance = new RuleValueString("Governmental");
            billingRule3.SecondaryInsurance = new RuleValueAny();
            billingRule3.PostDischarge = new RuleValueAny();
            billingRule3.BillingType = BillingTypeEnum.Split;
            billingRule3.ReferenceLab = new RuleValueAny();
            this.m_BillingRuleCollection.Add(billingRule3);

            BillingRule billingRule4 = new BillingRule();
            billingRule4.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule4.Priority = 6;            
            billingRule4.PatientType = new RuleValueAny();
            billingRule4.PrimaryInsurance = new RuleValueString("Medicare");
            billingRule4.SecondaryInsurance = new RuleValueAny();
            billingRule4.PostDischarge = new RuleValueAny();
            billingRule4.BillingType = BillingTypeEnum.Split;
            billingRule4.ReferenceLab = new RuleValueAny();
            this.m_BillingRuleCollection.Add(billingRule4);

            BillingRule billingRule5 = new BillingRule();
            billingRule5.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule5.Priority = 7;
            billingRule5.PatientType = new RuleValueString("IP");
            billingRule5.PrimaryInsurance = new RuleValueString("Medicaid");
            billingRule5.SecondaryInsurance = new RuleValueAny();
            billingRule5.PostDischarge = new RuleValueAny();
            billingRule5.BillingType = BillingTypeEnum.Split;
            billingRule5.ReferenceLab = new RuleValueAny();
            this.m_BillingRuleCollection.Add(billingRule5);
            
            BillingRule billingRule6 = new BillingRule();
            billingRule6.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule6.Priority = 8;
            billingRule6.PatientType = new RuleValueString("IP");
            billingRule6.PrimaryInsurance = new RuleValueAny();
            billingRule6.SecondaryInsurance = new RuleValueString("Medicaid");
            billingRule6.PostDischarge = new RuleValueAny();
            billingRule6.BillingType = BillingTypeEnum.Split;
            billingRule6.ReferenceLab = new RuleValueAny();
            this.m_BillingRuleCollection.Add(billingRule6);

            BillingRule billingRule9 = new BillingRule();
            billingRule9.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule9.Priority = 9;            
            billingRule9.PatientType = new RuleValueAny();
            billingRule9.PrimaryInsurance = new RuleValueAny();
            billingRule9.SecondaryInsurance = new RuleValueAny();
            billingRule9.PostDischarge = new RuleValueBoolean(true);
            billingRule9.BillingType = BillingTypeEnum.Global;
            billingRule9.ReferenceLab = new RuleValueAny();
            this.m_BillingRuleCollection.Add(billingRule9);                        
        }
    }
}
