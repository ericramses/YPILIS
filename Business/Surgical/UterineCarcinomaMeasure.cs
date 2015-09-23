using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class UterineCarcinomaMeasure : CarcinomaMeasure
    {
        public UterineCarcinomaMeasure()
        {
            this.m_Header = "Uterine Endometrioid Carcinoma Testing Suggested";
            this.m_DescriptionKeyWordCollection.Add("uterus");
            this.m_DiagnosisKeyWordCollection.Add("endometrioid carcinoma");
            this.m_DiagnosisKeyWordCollection.Add("endometrioid adenocarcinoma");
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());
        }
    }
}
