using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.InformalConsult
{
	public class InformalConsultTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public InformalConsultTest()
		{
			this.m_PanelSetId = 216;
			this.m_PanelSetName = "Informal Consult";
            this.m_Abbreviation = "Consult";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Technical;
			this.m_HasTechnicalComponent = false;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;            
			                    
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.InformalConsult.InformalConsultTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.InformalConsult.InformalConsultWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = false;            
			this.m_AcceptOnFinal = true;
            this.m_HasNoOrderTarget = true;
            this.m_IsBillable = false;
            this.m_ShowResultPageOnOrder = true;

            YellowstonePathology.Business.Facility.Model.Facility ypi = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
