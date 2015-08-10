using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
    public class HPVTWIPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public HPVTWIPanel()
        {        
            this.m_PanelId = 22;
            this.m_PanelName = "High Risk HPV TWI";
            this.m_AcknowledgeOnOrder = true;
            this.m_PanelOrderClassName = typeof(PanelOrderHPVTWI).AssemblyQualifiedName;
        }
    }
}
