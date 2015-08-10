using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetFNAAdequacyAssessment : PanelSet
	{
        public PanelSetFNAAdequacyAssessment()
		{
			this.m_PanelSetId = 81;
			this.m_PanelSetName = "FNA Immediate Assessment";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FNA;
			this.m_HasTechnicalComponent = true;            
            this.m_HasProfessionalComponent = true;            
            this.m_ResultDocumentSource = ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.FNAAdequacyAssessmentResult).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;
            this.m_ShowResultPageOnOrder = true;
            this.m_AcceptOnFinal = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
