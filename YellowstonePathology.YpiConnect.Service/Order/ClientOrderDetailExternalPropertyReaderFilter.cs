using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service.Order
{
	public class ClientOrderDetailExternalPropertyReaderFilter : YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter
	{
		public ClientOrderDetailExternalPropertyReaderFilter()
        {
			this.m_PropertiesToRead.Add("ClientOrderDetailId");
			this.m_PropertiesToRead.Add("ClientOrderId");
			this.m_PropertiesToRead.Add("ContainerId");
			this.m_PropertiesToRead.Add("SpecimenNumber");
			this.m_PropertiesToRead.Add("Submitted");
			this.m_PropertiesToRead.Add("Description");
			this.m_PropertiesToRead.Add("OrderDate");
			this.m_PropertiesToRead.Add("OrderTime");
			this.m_PropertiesToRead.Add("OrderedBy");
			this.m_PropertiesToRead.Add("OrderType");
			this.m_PropertiesToRead.Add("SpecialInstructions");
			this.m_PropertiesToRead.Add("Inactive");
			this.m_PropertiesToRead.Add("CollectionDate");
			this.m_PropertiesToRead.Add("CallbackNumber");
			this.m_PropertiesToRead.Add("Shipped");
			this.m_PropertiesToRead.Add("ShipDate");
			this.m_PropertiesToRead.Add("ShipmentId");
			this.m_PropertiesToRead.Add("ClientFixation");
			this.m_PropertiesToRead.Add("FixationStartTime");
		}
	}
}
