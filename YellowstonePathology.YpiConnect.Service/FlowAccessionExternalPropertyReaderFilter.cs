using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service
{
	public class FlowAccessionExternalPropertyReaderFilter : YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter
	{
		public FlowAccessionExternalPropertyReaderFilter()
        {
			this.m_PropertiesToRead.Add("MasterAccessionNo");
			//this.m_PropertiesToRead.Add("ReportNo");
			this.m_PropertiesToRead.Add("CollectionDate");
			this.m_PropertiesToRead.Add("PFirstName");
			this.m_PropertiesToRead.Add("PLastName");
			this.m_PropertiesToRead.Add("PMiddleInitial");
			this.m_PropertiesToRead.Add("PBirthdate");
			//this.m_PropertiesToRead.Add("SpecimenType");
			//this.m_PropertiesToRead.Add("Final");
			//this.m_PropertiesToRead.Add("FinalDate");
			//this.m_PropertiesToRead.Add("FinalTime");
			//this.m_PropertiesToRead.Add("PathologistSignature");
			//this.m_PropertiesToRead.Add("PathologistId");
		}
	}
}
