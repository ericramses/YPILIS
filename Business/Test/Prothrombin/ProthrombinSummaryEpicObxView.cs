using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Domain.Test.Prothrombin
{
	public class ProthrombinSummaryEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
		public ProthrombinSummaryEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			ProthrombinTestOrder testOrder = (ProthrombinTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, testOrder, "Prothrombin 20210A Mutation Analysis (Factor II)");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Result: " + testOrder.Result, document, "F");
        }
    }
}
