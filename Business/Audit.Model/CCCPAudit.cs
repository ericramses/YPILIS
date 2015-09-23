using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CCCPAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private List<string> m_DescriptionKeyWords;
        private List<string> m_DiagnosisKeyWords;
        private YellowstonePathology.Business.Billing.Model.CptCodeCollection m_CptCodeCollection;

        public CCCPAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_DescriptionKeyWords = new List<string> { "colon", "cecum", "appendix", "rectum" };
            this.m_DiagnosisKeyWords = new List<string> { "carcinoma", "adenocarcinoma" };
            this.m_CptCodeCollection = new Billing.Model.CptCodeCollection();
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest comprehensiveColonCancerProfileTest = new Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(comprehensiveColonCancerProfileTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                    if (this.IndicatorExists(surgicalSpecimen.SpecimenOrder.Description, surgicalSpecimen.Diagnosis, panelSetOrderCPTCodeCollectionForThisSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine("Comprehensive Colon Cancer Profile is suggested.");
                        break;
                    }
                }
            }
        }

        private bool IndicatorExists(string description, string diagnosis, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;

            if (this.WordsExistIn(description, this.m_DescriptionKeyWords) == true)
            {
                if (this.WordsExistIn(diagnosis, this.m_DiagnosisKeyWords) == true)
                {
                    if (panelSetOrderCPTCodeCollection.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
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
