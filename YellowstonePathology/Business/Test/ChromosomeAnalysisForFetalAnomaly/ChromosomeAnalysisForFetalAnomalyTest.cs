using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly
{
	public class ChromosomeAnalysisForFetalAnomalyTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public ChromosomeAnalysisForFetalAnomalyTest()
		{
			this.m_PanelSetId = 169;
            this.m_PanelSetName = "Chromosome Analysis For Fetal Anomaly";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Cytogenetics;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = new TimeSpan(14, 0, 0, 0);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyWordDocument).AssemblyQualifiedName;

            this.m_SurgicalAmendmentRequired = true;

            this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            YellowstonePathology.Business.Facility.Model.Facility facility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCNSHVLL");
            string taskDescription = "Collect fresh tissue in RPMI and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription, facility));

            this.m_TechnicalComponentFacility = facility;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = facility;
            this.m_ProfessionalComponentBillingFacility = facility;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
