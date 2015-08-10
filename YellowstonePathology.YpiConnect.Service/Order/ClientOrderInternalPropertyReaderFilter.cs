using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service.Order
{
	public class ClientOrderInternalPropertyReaderFilter : YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter
	{
		public ClientOrderInternalPropertyReaderFilter()
        {
			this.m_PropertiesToRead.Add("ClientOrderId");
			this.m_PropertiesToRead.Add("Received");    									
			this.m_PropertiesToRead.Add("Accessioned");
			this.m_PropertiesToRead.Add("Validated");
			this.m_PropertiesToRead.Add("MasterAccessionNo");
			this.m_PropertiesToRead.Add("Hold");
			this.m_PropertiesToRead.Add("HoldMessage");
			this.m_PropertiesToRead.Add("HoldBy");
			this.m_PropertiesToRead.Add("Acknowledged");
			this.m_PropertiesToRead.Add("AcknowledgedById");
			this.m_PropertiesToRead.Add("AcknowledgedDate");
		}
	}
}
