using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FLT3
{
	public class FLT3Test : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public FLT3Test()
		{
			this.m_PanelSetId = 153;
			this.m_PanelSetName = "FLT3 Mutation Analysis";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;


			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.FLT3.PanelSetOrderFLT3).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.FLT3.FLT3WordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;            
            this.m_EpicDistributionIsImplemented = true;

            string task2Description = "Collect (Peripheral blood: 5 mL in EDTA tube ONLY; Bone marrow: 2 mL in EDTA tube ONLY) and send to Neogenomics.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, task2Description, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            
            this.m_PanelSetCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:81245"), 1));
            this.m_PanelSetCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:81246"), 1));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
