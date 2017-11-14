using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasure395 : PQRSMeasure
    {        
        public PQRSMeasure395()
        {            
			this.m_Header = "Lung Cancer Reporting #395";

            this.m_PQRIKeyWordCollection.Add("Lung");
            this.m_PQRIKeyWordCollection.Add("Bronch");
            this.m_PQRIKeyWordCollection.Add("Pleural");

            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88104"));
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88108"));
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88112"));
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88173"));
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88305"));

            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.Patients18AndOlder;            

            this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:g9418"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:g9419"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:g9420"));
            this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:g9421"));
        }

        public override bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;
            if (string.IsNullOrEmpty(surgicalSpecimen.SpecimenOrder.Description) == false)
            {                                
                if (this.m_PQRIKeyWordCollection.WordsExistIn(surgicalSpecimen.SpecimenOrder.Description) == true)
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
