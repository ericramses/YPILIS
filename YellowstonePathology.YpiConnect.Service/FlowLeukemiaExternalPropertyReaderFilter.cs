using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service
{
	public class FlowLeukemiaExternalPropertyReaderFilter : YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter
	{
		public FlowLeukemiaExternalPropertyReaderFilter()
        {
			this.m_PropertiesToRead.Add("ReportNo");
			this.m_PropertiesToRead.Add("GatingPopulationV2");
			this.m_PropertiesToRead.Add("LymphocyteCount");
			this.m_PropertiesToRead.Add("MonocyteCount");
			this.m_PropertiesToRead.Add("MyeloidCount");
			this.m_PropertiesToRead.Add("DimCD45ModSSCount");
			this.m_PropertiesToRead.Add("OtherCount");
			this.m_PropertiesToRead.Add("OtherName");
			this.m_PropertiesToRead.Add("InterpretiveComment");
			this.m_PropertiesToRead.Add("Impression");
			this.m_PropertiesToRead.Add("CellPopulationOfInterest");
			this.m_PropertiesToRead.Add("TestResult");
			this.m_PropertiesToRead.Add("CellDescription");
			this.m_PropertiesToRead.Add("BTCellSelection");
			this.m_PropertiesToRead.Add("EGateCD34Percent");
			this.m_PropertiesToRead.Add("EGateCD117Percent");
		}
	}
}
