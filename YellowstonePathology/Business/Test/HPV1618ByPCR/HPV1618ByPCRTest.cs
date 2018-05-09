﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV1618ByPCRTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public HPV1618ByPCRTest()
		{
			this.m_PanelSetId = 213;
			this.m_PanelSetName = "HPV Genotypes 16 and 18 By PCR";
            this.m_Abbreviation = "HPV1618PCR";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = false;            

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRWordDocument).AssemblyQualifiedName;
            this.m_RequiresPathologistSignature = false;
            this.m_AcceptOnFinal = false;
			this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;
            this.m_CMMCDistributionIsImplemented = true;

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_HasSplitCPTCode = false;            

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("87625", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            string task1Description = "Cut curls and an after H&E. Give to molecular";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description));

            string task2Description = "Receive materials from histology and run test.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceHPV1618GEN());

            HPV1618ByPCRPanel hpv1618ByPCRPanel = new HPV1618ByPCRPanel();
            this.m_PanelCollection.Add(hpv1618ByPCRPanel);
        }
	}
}
