using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service.Order
{
	public class ClientOrderDetailInternalPropertyReaderFilter : YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter
	{
		public ClientOrderDetailInternalPropertyReaderFilter()
        {
			this.m_PropertiesToRead.Add("ClientOrderDetailId");
			this.m_PropertiesToRead.Add("ClientOrderId");
			this.m_PropertiesToRead.Add("ContainerId");
			//this.m_PropertiesToRead.Add("SpecimenNumber");
			//this.m_PropertiesToRead.Add("Submitted");
			this.m_PropertiesToRead.Add("Accessioned");
			this.m_PropertiesToRead.Add("Received");
			this.m_PropertiesToRead.Add("DateReceived");
			this.m_PropertiesToRead.Add("Validated");
			this.m_PropertiesToRead.Add("Description");
			this.m_PropertiesToRead.Add("DescriptionToAccession");
			this.m_PropertiesToRead.Add("OrderDate");
			this.m_PropertiesToRead.Add("OrderTime");
			//this.m_PropertiesToRead.Add("OrderedBy");
			this.m_PropertiesToRead.Add("OrderType");
			//this.m_PropertiesToRead.Add("SpecialInstructions");
			//this.m_PropertiesToRead.Add("Inactive");
			this.m_PropertiesToRead.Add("CollectionDate");
			//this.m_PropertiesToRead.Add("CallbackNumber");
			this.m_PropertiesToRead.Add("ClientFixation");
			this.m_PropertiesToRead.Add("LabFixation");
			this.m_PropertiesToRead.Add("FixationStartTime");
			this.m_PropertiesToRead.Add("FixationEndTime");
		}
	}
}
