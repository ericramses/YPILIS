using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCL1t1114
{
	public class BCL1t1114Test : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public BCL1t1114Test()
		{
			this.m_PanelSetId = 100;
			this.m_PanelSetName = "BCL1/IgH t(11;14)";
			this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114TestOrder).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials (Bone Marrow Aspirate: 1-2 mL sodium heparin tube. EDTA tube is acceptable. " +
                "Peripheral Blood: 2-5 mL sodium heparin tube. EDTA tube is acceptable." +
                "Fresh, Unfixed Tissue: Tissue in RPMI. Fluids: Equal parts RPMI to specimen volume) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81401(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
		}
	}
}
