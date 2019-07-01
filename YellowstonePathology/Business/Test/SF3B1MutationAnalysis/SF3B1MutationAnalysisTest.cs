using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.SF3B1MutationAnalysis
{
    public class SF3B1MutationAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public SF3B1MutationAnalysisTest()
        {
            this.m_PanelSetId = 348;
            this.m_PanelSetName = "SF3B1 Mutation Analysis";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = new TimeSpan(10, 0, 0, 0);

            this.m_AllowMultiplePerAccession = true;

            YellowstonePathology.Business.Facility.Model.Facility facility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("GNMCHLTH");
            string taskDescription = "Gather materials and send to Genomic Health.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, facility));

            this.m_TechnicalComponentFacility = facility;
            this.m_ProfessionalComponentFacility = facility;

            this.m_TechnicalComponentBillingFacility = facility;
            this.m_ProfessionalComponentBillingFacility = facility;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());

            //Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88363", null), 1);
            //this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);
        }
    }
}
