﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ABL1KinaseDomainMutation
{
	public class ABL1KinaseDomainMutationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public ABL1KinaseDomainMutationTest()
        {
            this.m_PanelSetId = 135;
            this.m_PanelSetName = "ABL1 Kinase Domain Mutation";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationWordDocument).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;
            
            string taskDescription = "Collect (Peripheral blood: 5 mL in EDTA tube ONLY; Bone marrow: 2 mL in EDTA tube ONLY) and send to Neogenomics.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81170", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
    }
}
