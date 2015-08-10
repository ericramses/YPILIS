using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
    public class CysticFibrosisPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public CysticFibrosisPanel()
        {
            this.m_PanelId = 2;
            this.m_PanelName = "Cystic Fibrosis";
            this.m_AcknowledgeOnOrder = true;            
        }
    }
}
