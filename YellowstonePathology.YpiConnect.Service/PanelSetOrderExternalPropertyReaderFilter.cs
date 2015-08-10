using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Service
{
	public class PanelSetOrderExternalPropertyReaderFilter : YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter
	{
		public PanelSetOrderExternalPropertyReaderFilter()
        {
			this.m_PropertiesToRead.Add("ReportNo");
			//this.m_PropertiesToRead.Add("PanelSetId");
			//this.m_PropertiesToRead.Add("PanelSetName");
			this.m_PropertiesToRead.Add("FinaledById");
			this.m_PropertiesToRead.Add("Final");
			this.m_PropertiesToRead.Add("FinalDate");
			this.m_PropertiesToRead.Add("FinalTime");
			//this.m_PropertiesToRead.Add("OrderedById");
			//this.m_PropertiesToRead.Add("OrderDate");
			//this.m_PropertiesToRead.Add("OrderTime");
			this.m_PropertiesToRead.Add("Signature");
			this.m_PropertiesToRead.Add("AssignedToId");
			//this.m_PropertiesToRead.Add("TemplateId");
			//this.m_PropertiesToRead.Add("MasterAccessionNo");
			//this.m_PropertiesToRead.Add("OriginatingLocation");
			//this.m_PropertiesToRead.Add("Audit");
			//this.m_PropertiesToRead.Add("BillingAudit");
			//this.m_PropertiesToRead.Add("SignatureAudit");
			//this.m_PropertiesToRead.Add("ResultDocumentSource");
			//this.m_PropertiesToRead.Add("ResultDocumentPath");
			//this.m_PropertiesToRead.Add("Published");
			//this.m_PropertiesToRead.Add("DateLastPublished");
			//this.m_PropertiesToRead.Add("TechnicalComponentFacilityId");
			//this.m_PropertiesToRead.Add("HasTechnicalComponent");
			//this.m_PropertiesToRead.Add("ProfessionalComponentFacilityId");
			//this.m_PropertiesToRead.Add("HasProfessionalComponent");
			//this.m_PropertiesToRead.Add("AuditedById");
			//this.m_PropertiesToRead.Add("AuditDate");
		}
	}
}
