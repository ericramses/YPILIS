using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class CarcinomaMeasure
    {
        protected KeyWordCollection m_DescriptionKeyWordCollection;
        protected KeyWordCollection m_DiagnosisKeyWordCollection;
        protected string m_Header;
        protected YellowstonePathology.Business.Billing.Model.CptCodeCollection m_CptCodeCollection;

        public CarcinomaMeasure()
        {
            this.m_DescriptionKeyWordCollection = new KeyWordCollection();
            this.m_DiagnosisKeyWordCollection = new KeyWordCollection();
            this.m_CptCodeCollection = new Business.Billing.Model.CptCodeCollection();
        }

        public virtual bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
            bool result = false;
            if (this.m_DescriptionKeyWordCollection.WordsExistIn(surgicalSpecimen.SpecimenOrder.Description) == true)
            {
                if (this.m_DiagnosisKeyWordCollection.WordsExistIn(surgicalSpecimen.Diagnosis) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                    if (panelSetOrderCPTCodeCollectionForThisSpecimen.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Billing.Model.CptCodeCollection CptCodeCollection
        {
            get { return this.m_CptCodeCollection; }
        }

        public KeyWordCollection DescriptionKeyWordCollection
        {
            get { return this.m_DescriptionKeyWordCollection; }
        }

        public KeyWordCollection DiagnosisKeyWordCollection
        {
            get { return this.m_DiagnosisKeyWordCollection; }
        }

        public string Header
        {
            get { return this.m_Header; }
        }
    }
}
