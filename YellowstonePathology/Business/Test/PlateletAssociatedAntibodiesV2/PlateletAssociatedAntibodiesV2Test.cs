﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.PlateletAssociatedAntibodiesV2
{
    public class PlateletAssociatedAntibodiesV2Test : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public PlateletAssociatedAntibodiesV2Test()
        {
            this.m_PanelSetId = 340;
            this.m_PanelSetName = "Platelet Associated Antibodies";
            this.m_AllowMultiplePerAccession = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_CaseType = this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_Active = true;
            this.m_ExpectedDuration = new TimeSpan(24, 0, 0);
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.PlateletAssociatedAntibodiesV2.PlateletAssociatedAntibodiesV2WordDocument).AssemblyQualifiedName;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PlateletAssociatedAntibodiesV2.PlateletAssociatedAntibodiesV2TestOrder).AssemblyQualifiedName;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            string taskDescription = "Perform PAA testing.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Facility.Model.Facility ypi = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86023", null), 2);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
        }
    }
}
