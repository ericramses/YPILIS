using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Autopsy
{
	public class AutopsyEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
		public AutopsyEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			AutopsyTestOrder testOrder = (AutopsyTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, testOrder, "Autopsy");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("This is the result for Autopsy", document, "F");
        }
    }
}
