using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class EndometrialCarcinomaMeasure : CarcinomaMeasure
    {
        public EndometrialCarcinomaMeasure()
        {
            this.m_Header = "Endometrial Carcinoma Testing Suggested";
            this.m_DescriptionKeyWordCollection.Add("endometrium");
            this.m_DiagnosisKeyWordCollection.Add("endometrioid carcinoma");
            this.m_DiagnosisKeyWordCollection.Add("endometrioid adenocarcinoma");
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305());
        }
    }
}
