using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service
{
	public class FlowMarkerExternalPropertyReaderFilter : YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter
	{
		public FlowMarkerExternalPropertyReaderFilter()
        {
			this.m_PropertiesToRead.Add("FlowMarkerId");
			this.m_PropertiesToRead.Add("ReportNo");
			this.m_PropertiesToRead.Add("Name");
			this.m_PropertiesToRead.Add("Percentage");
			this.m_PropertiesToRead.Add("Intensity");
			this.m_PropertiesToRead.Add("Interpretation");
			this.m_PropertiesToRead.Add("Predictive");
			this.m_PropertiesToRead.Add("Expresses");
		}
	}
}
