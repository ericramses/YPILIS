using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service.Order
{
    public class ClientOrderExternalPropertyReaderFilter : YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter
    {
		public ClientOrderExternalPropertyReaderFilter()
        {
			this.m_PropertiesToRead.Add("ClientOrderId");
			this.m_PropertiesToRead.Add("Submitted");
			this.m_PropertiesToRead.Add("OrderDate");
			this.m_PropertiesToRead.Add("OrderTime");
			this.m_PropertiesToRead.Add("OrderedBy");
			this.m_PropertiesToRead.Add("PFirstName");
			this.m_PropertiesToRead.Add("PLastName");
			this.m_PropertiesToRead.Add("PMiddleInitial");
			this.m_PropertiesToRead.Add("PBirthdate");
			this.m_PropertiesToRead.Add("PSex");
			this.m_PropertiesToRead.Add("PSSN");
			this.m_PropertiesToRead.Add("SvhMedicalRecord");
			this.m_PropertiesToRead.Add("SvhAccountNo");			
			this.m_PropertiesToRead.Add("ClientId");
			this.m_PropertiesToRead.Add("ClientName");
			this.m_PropertiesToRead.Add("ProviderId");
			this.m_PropertiesToRead.Add("ProviderName");
			this.m_PropertiesToRead.Add("PreOpDiagnosis");
			this.m_PropertiesToRead.Add("PostOpDiagnosis");
			this.m_PropertiesToRead.Add("ClinicalHistory");
			this.m_PropertiesToRead.Add("ReportCopyTo");
			this.m_PropertiesToRead.Add("CollectionDate");
			this.m_PropertiesToRead.Add("ExternalOrderId");      
			this.m_PropertiesToRead.Add("OrderedByFirstName");
			this.m_PropertiesToRead.Add("OrderedByLastName");
			this.m_PropertiesToRead.Add("OrderedById");
			this.m_PropertiesToRead.Add("ProviderFirstName");
			this.m_PropertiesToRead.Add("ProviderLastName");
			this.m_PropertiesToRead.Add("OrderType");
		}
    }
}
