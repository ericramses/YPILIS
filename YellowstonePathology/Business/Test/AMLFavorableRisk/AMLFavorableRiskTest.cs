using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AMLFavorableRisk
{
    public class AMLFavorableRiskTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
    {
        public AMLFavorableRiskTest()
        {
            this.m_PanelSetId = 246;
            this.m_PanelSetName = "AML Favorable Risk";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(14);

            this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskWordDocument).AssemblyQualifiedName;

            string taskDescription = "Gather materials (Peripheral blood: 2-5 mL in sodium heparin tube, 2x5 mL in EDTA tube; " +
            "Bone marrow: 1-2 mL in sodium heparin tube or 2 mL in EDTA tube; Fresh unfixed tissue in RPMI) and send out to Neo.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
    }
}
