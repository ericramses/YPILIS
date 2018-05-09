﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FNAAdequacyAssessment
{
	public class FNAAdequacyAssessmentTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public FNAAdequacyAssessmentTest()
		{
			this.m_PanelSetId = 81;
			this.m_PanelSetName = "FNA Immediate Assessment";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FNA;
			this.m_HasTechnicalComponent = true;            
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.NothingToPublishReport).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;
            this.m_ShowResultPageOnOrder = true;
            this.m_AcceptOnFinal = true;

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
