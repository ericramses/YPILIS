using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618Panel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public HPV1618Panel()
        {        
            this.m_PanelId = 65;
            this.m_PanelName = "HPV Genotypes 16 and 18";
            this.m_AcknowledgeOnOrder = true;
            this.m_PanelOrderClassName = typeof(YellowstonePathology.Business.Test.HPV1618.PanelOrderHPV1618).AssemblyQualifiedName;
        }
    }
}
