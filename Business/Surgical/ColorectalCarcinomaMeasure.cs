using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class ColorectalCarcinomaMeasure : CarcinomaMeasure
    {
        public ColorectalCarcinomaMeasure()
        {
            this.m_Header = "Colorectal Carcinoma Testing Suggested";
            this.m_DescriptionKeyWordCollection.Add("colon");
            this.m_DescriptionKeyWordCollection.Add("cecum");
            this.m_DescriptionKeyWordCollection.Add("appendix");
            this.m_DescriptionKeyWordCollection.Add("rectum");
            this.m_DiagnosisKeyWordCollection.Add("carcinoma");
            this.m_DiagnosisKeyWordCollection.Add("adenocarcinoma");
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());
        }
    }
}
