using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule17 : ReflexOrder
    {
        public HPVReflexOrderRule17()
        {
            this.m_RuleNumber = 17;
            this.m_ReflexOrderCode = "RFLXHPVRL17";
            this.m_Description = "Perform reflex HPV testing on patients who have a PAP result of ASCUS or LSIL and have no positive PAP result in the last year.";
            this.m_PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            if (accessionOrder.PanelSetOrderCollection.Exists(15) == true)
            {
                YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
                if (panelSetOrderCytology.Final == true)
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
                            List<string> priorResults = patientHistory.GetPriorHPVResult(accessionOrder.MasterAccessionNo, DateTime.Today.AddDays(-330));
                            foreach(string papResult in priorResults)
                            {
                                if(YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisThreeOrBetter(papResult) == true)
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
