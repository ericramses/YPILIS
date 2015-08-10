using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
    public class JAK2V617FPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public JAK2V617FPanel()
        {
            this.m_PanelId = 1;
            this.m_PanelName = "JAK2V617F";
            this.m_AcknowledgeOnOrder = true;            
        }
    }
}
