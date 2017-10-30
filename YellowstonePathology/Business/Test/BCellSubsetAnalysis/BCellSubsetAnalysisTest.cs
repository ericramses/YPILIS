using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BCellSubsetAnalysis
{
    public class BCellSubsetAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public BCellSubsetAnalysisTest()
        {
            this.m_PanelSetId = 263;
            this.m_PanelSetName = "B-Cell Subset Analysis";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.BCellSubsetAnalysis.BCellSubsetAnalysisTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.BCellSubsetAnalysis.BCellSubsetAnalysisWordDocument).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86359 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("CPT86359"), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86360 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("CPT86360"), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt88184 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("CPT88184"), 1);
            this.m_PanelSetCptCodeCollection.Add(cpt86359);
            this.m_PanelSetCptCodeCollection.Add(cpt86360);
            this.m_PanelSetCptCodeCollection.Add(cpt88184);

            string taskDescription = "Gather materials and perform test.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
        }
    }
}