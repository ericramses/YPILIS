using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.CXCR4MutationAnalysis
{
    public class CXCR4MutationAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public CXCR4MutationAnalysisTest()
        {
            this.m_PanelSetId = 253;
            this.m_PanelSetName = "CXCR4 Mutation Analysis";
            this.m_Abbreviation = "CXCR4 Mutation Analysis";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_IsBillable = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials and send to Neo.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();

            this.m_PanelSetCptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Billing.Model.CptCodeCollection.GetCPTCode("81479"), 1));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
    }
}
