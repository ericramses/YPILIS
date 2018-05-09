﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FlowCytometryAnalysis
{
	public class FlowCytometryAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public FlowCytometryAnalysisTest()
		{
			this.m_PanelSetId = 248;
			this.m_PanelSetName = "Flow Cytometry Analysis";
            this.m_Abbreviation = "Flow Cytometry Analysis";
			this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;            
            
			this.m_AllowMultiplePerAccession = true;            
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            string taskDescription = "Collect specimen and send to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = neogenomicsIrvine;
            this.m_ProfessionalComponentBillingFacility = neogenomicsIrvine;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
		}
	}
}
