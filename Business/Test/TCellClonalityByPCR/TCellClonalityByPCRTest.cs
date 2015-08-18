using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TCellClonalityByPCR
{
	public class TCellClonalityByPCRTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public TCellClonalityByPCRTest()
		{
			this.m_PanelSetId = 152;
			this.m_PanelSetName = "T-Cell Gene Rearrangement";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.TCellClonalityByPCR.PanelSetOrderTCellClonalityByPCR).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;            

            string taskDescription = "Gather materials (Peripheral blood: 5 mL in EDTA tube. Bone marrow: 2 mL in EDTA tube. " +
                "FFPE tissue: Paraffin block is preferred. Alternatively, send 1 H&E slide plus 5-10 unstained slides cut at 5 " +
                "or more microns. Fresh tissue: Two pieces) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81342(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
		}
	}
}
