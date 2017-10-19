using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class BoneMarrowSummaryTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public BoneMarrowSummaryTest()
        {
            this.m_PanelSetId = 268;
            this.m_PanelSetName = "Bone Marrow Summary";
            this.m_Abbreviation = "Bone Marrow Summary";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Summary;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
            this.m_Active = true;
            this.m_HasNoOrderTarget = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.BoneMarrowSummary.BoneMarrowSummaryWordDocument).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = false;
            this.m_EpicDistributionIsImplemented = true;

            this.m_ExpectedDuration = new TimeSpan(14, 0, 0, 0);

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            //YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81270(), 1);
            //this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            //string taskDescription = "Gather materials and get to work.";
            //this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
        }
    }
}
