using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTConfirmatoryPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public NGCTConfirmatoryPanel()
        {
            this.m_PanelId = 6;
            this.m_PanelName = "NG-CT Retest";
            this.m_AcknowledgeOnOrder = true;
        }
    }
}
