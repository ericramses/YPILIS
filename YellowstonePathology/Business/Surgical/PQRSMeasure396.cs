using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasure396 : PQRSMeasure
    {        
        public PQRSMeasure396()
        {            
			this.m_Header = "Lung Cancer Reporting #396";
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88309"));            
            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.Patients18To75;

            this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:g9422"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:g9423"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:g9424"));
            this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:g9425"));
        }

        public override bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;
            if (string.IsNullOrEmpty(surgicalSpecimen.SpecimenOrder.Description) == false)
            {
                string specimenKeyWord = "Lung";
                if (surgicalSpecimen.SpecimenOrder.Description.ToUpper().Contains(specimenKeyWord.ToUpper()) == true)
                {
                    string diagnosisKeyWord = "Carcinoma";
                    if (string.IsNullOrEmpty(surgicalSpecimen.Diagnosis) == false && surgicalSpecimen.Diagnosis.ToUpper().Contains(diagnosisKeyWord.ToUpper()) == true)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                        if (panelSetOrderCPTCodeCollectionForThisSpecimen.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }        
    }
}
