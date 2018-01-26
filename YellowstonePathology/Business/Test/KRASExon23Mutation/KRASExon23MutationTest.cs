﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASExon23Mutation
{
	public class KRASExon23MutationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public KRASExon23MutationTest()
		{
			this.m_PanelSetId = 217;
			this.m_PanelSetName = "KRAS Exon 2,3 Mutation Analysis";
            this.m_Abbreviation = "KRASX23";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
			this.m_Active = true;


			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationWordDocument).AssemblyQualifiedName;
			
			this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

			string taskDescription = "Collect paraffin block from Histology and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
			this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

			YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics taskSendBlockToNeogenomics = new YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics();
			this.m_TaskCollection.Add(taskSendBlockToNeogenomics);
            
			this.m_PanelSetCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81275", null), 1));
            this.m_PanelSetCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81276", null), 1));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
