using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	public interface IFlowAccessionGateway
	{
		void SubmitChanges(Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession> flowAccessionCollection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum);
		void SubmitChanges(Collection<YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrder> panelSetOrderCollection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum);
		void SubmitChanges(Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowLeukemia> flowLeukemiaCollection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum);
		void SubmitChanges(Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowMarker> flowMarkerCollection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum);
	}
}
