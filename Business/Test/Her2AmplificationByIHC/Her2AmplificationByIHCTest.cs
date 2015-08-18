using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
	public class Her2AmplificationByIHCTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public Her2AmplificationByIHCTest()
		{
			this.m_PanelSetId = 171;
			this.m_PanelSetName = "HER2 Amplification by IHC";
            this.m_Abbreviation = "HER2IHC";
            this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;


			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials (Cut Slides: 3 cut slides. Cut sections at 3-4 microns, and place tissue at the center bottom of the slide" +
                "or Paraffin block: (Preferred for image analysis.) Formalin-fixed paraffin-embedded tissue.) and give to Molecular for send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            string taskDescription2 = "Receive materials from histology and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription2));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
