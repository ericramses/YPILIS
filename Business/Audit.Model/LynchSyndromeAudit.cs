using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class LynchSyndromeAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private List<string> m_ColorectalDescriptionKeyWords;
        private List<string> m_ColorectalDiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_ColorectalCptCodeCollection;

        private List<string> m_EndometrialDescriptionKeyWords;
        private List<string> m_EndometrialDiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_EndometrialCptCodeCollection;

        private List<string> m_UterineDescriptionKeyWords;
        private List<string> m_UterineDiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_UterineCptCodeCollection;

        public LynchSyndromeAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ColorectalDescriptionKeyWords = new List<string> { "colon", "cecum", "appendix", "rectum" };
            this.m_ColorectalDiagnosisKeyWords = new List<string> { "carcinoma", "adenocarcinoma" };
            this.m_ColorectalCptCodeCollection = new Billing.Model.CptCodeCollection();
            this.m_ColorectalCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());

            this.m_EndometrialDescriptionKeyWords = new List<string> { "endometrium" };
            this.m_EndometrialDiagnosisKeyWords = new List<string> { "endometrioid carcinoma", "endometrioid adenocarcinoma" };
            this.m_EndometrialCptCodeCollection = new Billing.Model.CptCodeCollection();
            this.m_EndometrialCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305());

            this.m_UterineDescriptionKeyWords = new List<string> { "uterus" };
            this.m_UterineDiagnosisKeyWords = new List<string> { "endometrioid carcinoma", "endometrioid adenocarcinoma" };
            this.m_UterineCptCodeCollection = new Billing.Model.CptCodeCollection();
            this.m_UterineCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId) == false)
            {
                bool match = false;
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                    if (this.ColorectalIndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine("Lynch Syndrome is suggested.");
                        break;
                    }
                    else if (this.EndometrialIndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine("Lynch Syndrome is suggested.");
                        break;
                    }
                    else if ( this.UterineIndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine("Lynch Syndrome is suggested.");
                        break;
                    }
                }
            }
        }

        private bool ColorectalIndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.WordsExistIn(description, this.m_ColorectalDescriptionKeyWords) == true)
            {
                if (this.WordsExistIn(diagnosis, this.m_ColorectalDiagnosisKeyWords) == true)
                {
                    if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_ColorectalCptCodeCollection) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool EndometrialIndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.WordsExistIn(description, this.m_EndometrialDescriptionKeyWords) == true)
            {
                if (this.WordsExistIn(diagnosis, this.m_EndometrialDiagnosisKeyWords) == true)
                {
                    if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_EndometrialCptCodeCollection) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool UterineIndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.WordsExistIn(description, this.m_UterineDescriptionKeyWords) == true)
            {
                if (this.WordsExistIn(diagnosis, this.m_UterineDiagnosisKeyWords) == true)
                {
                    if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_UterineCptCodeCollection) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool WordsExistIn(string text, List<string> keyWords)
        {
            bool result = false;
            if (string.IsNullOrEmpty(text) == false)
            {
                foreach (string keyWord in keyWords)
                {
                    if (text.ToUpper().Contains(keyWord.ToUpper()) == true)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
