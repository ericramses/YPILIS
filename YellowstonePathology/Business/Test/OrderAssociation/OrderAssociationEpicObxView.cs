using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.OrderAssociation
{
	public class OrderAssociationEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public OrderAssociationEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			OrderAssociationTestOrder panelSetOrder = (OrderAssociationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);			
			this.AddNextObxElement("", document, "F");
			string result = "This order has been completed.";
			this.AddNextObxElement(result, document, "F");			
		}
	}
}
