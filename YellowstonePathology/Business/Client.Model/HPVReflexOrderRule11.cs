using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule11 : ReflexOrder
    {
        public HPVReflexOrderRule11()
        {
            this.m_RuleNumber = 11;
            this.m_ReflexOrderCode = "RFLXHPVRL11";
            this.m_Description = "Perform reflex HPV testing on patients who are 25 years and older, have a PAP result of ASCUS or LSIL and have not had an HPV in the last year.";
			this.m_PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
            this.m_PatientAge = HPVRuleValues.Age25andOlder;
            this.m_PAPResult = HPVRuleValues.PAPResultASCUSorLSIL;
            this.m_HPVResult = HPVRuleValues.NotUsed;
            this.m_HPVTesting = HPVRuleValues.HPVLastTestNotInPastYear;
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrep = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrep.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetThinPrep.PanelSetId);
                if (panelSetOrderCytology.Final == true)
                {
                    if (accessionOrder.PBirthdate <= DateTime.Today.AddYears(-25))
                    {
						if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisAscusLsil(panelSetOrderCytology.ResultCode) == true)
                        {							
							YellowstonePathology.Business.Domain.PatientHistory patientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(accessionOrder.PatientId);
                            Nullable<DateTime> dateOfLastHPV = patientHistory.GetDateOfPreviousHpv(accessionOrder.AccessionDate.Value);

                            if (dateOfLastHPV.HasValue == true)
                            {
                                if (dateOfLastHPV < DateTime.Today.AddDays(-330))
                                {
                                    result = true;
                                }
                            }
                            else
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            return result;
        }             
    }
}
