using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Builder
{
	public class PanelSetOrderBuilderLeukemiaLymphoma : PanelSetOrderBuilder
	{
		public override void Build(Test.PanelSetOrder panelSetOrder, System.Xml.Linq.XElement panelSetOrderElement)
		{			
			List<XElement> markerElements = (from item in panelSetOrderElement.Elements("FlowMarkerCollection")
											 select item).ToList<XElement>();
			foreach (XElement markerElement in markerElements.Elements("FlowMarker"))
			{
				Flow.FlowMarkerItem flowMarkerItem = new Flow.FlowMarkerItem();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(markerElement, flowMarkerItem);
				xmlPropertyWriter.Write();
				((Test.LLP.PanelSetOrderLeukemiaLymphoma)panelSetOrder).FlowMarkerCollection.Add(flowMarkerItem);
			}
		}
	}
}
