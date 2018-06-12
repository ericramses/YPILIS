using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
    public class ERPRSemiQuantitativePanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public ERPRSemiQuantitativePanel()
        {        
            this.m_PanelId = 62;
            this.m_PanelName = "Estrogen/Progesterone Receptor, Semi-Quantitative";
            this.m_AcknowledgeOnOrder = true;

            YellowstonePathology.Business.Test.Model.Test er = YellowstonePathology.Business.Test.Model.TestCollection.Instance.GetTest("99"); // EstrogenReceptorSemiquant();
            this.m_TestCollection.Add(er);

            YellowstonePathology.Business.Test.Model.Test pr = YellowstonePathology.Business.Test.Model.TestCollection.Instance.GetTest("145"); // ProgesteroneReceptorSemiquant();
            this.m_TestCollection.Add(pr);
        }        
    }
}
