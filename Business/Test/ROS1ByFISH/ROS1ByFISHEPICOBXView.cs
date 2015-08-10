using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
	public class ROS1ByFISHEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
	{
        public ROS1ByFISHEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder panelSetOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, panelSetOrder, "ROS1 by Fish Analysis");			
		}
	}
}
