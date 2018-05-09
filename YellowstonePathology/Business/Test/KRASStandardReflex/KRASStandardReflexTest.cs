﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
	public class KRASStandardReflexTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public KRASStandardReflexTest()
		{
			this.m_PanelSetId = 30;
			this.m_PanelSetName = "KRAS Standard Reflex";
            this.m_Abbreviation = "KRASSR";
			this.m_CaseType = YellowstonePathology.Business.CaseType.ReflexTesting;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;			
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
            this.m_Active = true;
            
			this.m_SurgicalAmendmentRequired = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
			this.m_AcceptOnFinal = true;
            this.m_ExpectedDuration = new TimeSpan(120, 0, 0);
            this.m_IsBillable = false;
            this.m_EpicDistributionIsImplemented = true;
            //updated 12.14.16 by MS

            string taskDescription = "Collect paraffin block from Histology and send to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceKRASBRAF());
		}
	}
}
