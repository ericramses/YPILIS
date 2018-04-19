using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH
{
	public class CCNDIBCLIGHByFISHTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public CCNDIBCLIGHByFISHTest()
		{
			this.m_PanelSetId = 148;
            this.m_PanelSetName = "CCND1/IgH t(11;14) By FISH";
			this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH.CCNDIBCLIGHByFISHTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH.CCNDIBCLIGHByFISHWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            this.m_SurgicalAmendmentRequired = true;
            string taskDescription = "Collect parafin block from Histology and send to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = neogenomicsIrvine;
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
