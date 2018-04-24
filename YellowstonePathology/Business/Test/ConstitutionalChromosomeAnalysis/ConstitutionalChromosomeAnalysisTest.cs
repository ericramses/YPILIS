using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ConstitutionalChromosomeAnalysis
{
    public class ConstitutionalChromosomeAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public ConstitutionalChromosomeAnalysisTest()
        {
            this.m_PanelSetId = 278;
            this.m_PanelSetName = "Constitutional Chromosome Analysis";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Cytogenetics;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(14);
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.NothingToPublishReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = false;

            this.m_SurgicalAmendmentRequired = true;

            string taskDescription = "Collect (Peripheral blood: 2-5 mL in Sodium Heparin tube ONLY; " +
            "Bone marrow: 2 mL in Sodium Heparin tube ONLY; Fresh unfixed tissue in RPMI) and send to Neogenomics.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsNashville()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsNashville();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsNashville();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsNashville();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
