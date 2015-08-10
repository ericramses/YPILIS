using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public NGCTPanel()
        {
            this.m_PanelId = 3;
            this.m_PanelName = "NG-CT";
            this.m_AcknowledgeOnOrder = true;
        }
    }
}
