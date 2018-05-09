﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetHighGradeLargeBCellLymphomaPanelRetired : PanelSet
	{
        public PanelSetHighGradeLargeBCellLymphomaPanelRetired()
		{
			this.m_PanelSetId = 121;
			this.m_PanelSetName = "High Grade/Large B-Cell Lymphoma Panel - Retired";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;


            this.m_AllowMultiplePerAccession = true;
            this.m_Active = false;

            string taskDescription = "Gather materials (Bone marrow aspirate: 1-2 mL sodium heparin tube. EDTA tube is acceptable. " +
                "Peripheral blood: 2-5 mL sodium heparin tube. EDTA tube is acceptable. Fresh, unfixed tissue: Tissue in RPMI. " +
                "Fluids: Equal parts RPMI to specimen volume. Paraffin block: H&E slide (required) plus paraffin block. Circle H&E for tech-only." +
                "Cut slides: H&E slide (required) plus 6 unstained slides cut at 4 microns. Circle H&E for tech-only.) and send out to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88368", null), 6);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
		}
	}
}
