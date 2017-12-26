using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1SP142
{
	public class PDL1SP142Test : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public PDL1SP142Test()
		{
			this.m_PanelSetId = 215;
            this.m_PanelSetName = "PD-L1 (SP142)";
            this.m_Abbreviation = "PD-L1 (SP142)";
            this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = false;

            this.m_SurgicalAmendmentRequired = true;
            this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142TestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142WordDocument).AssemblyQualifiedName;			            

            string taskDescription = "Collect paraffin block from Histology and send to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
