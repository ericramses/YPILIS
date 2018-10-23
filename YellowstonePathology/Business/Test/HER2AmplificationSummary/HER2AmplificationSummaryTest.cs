﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationSummaryTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
    {
        public HER2AmplificationSummaryTest()
        {
            this.m_PanelSetId = 313;
            this.m_PanelSetName = "HER2 Amplification Summary";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
            this.m_Active = true;
            this.m_IsBillable = false;

            //this.m_SurgicalAmendmentRequired = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryWordDocument).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(4, 0, 0, 0);

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_HasSplitCPTCode = false;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88377", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            string taskDescription = "Figure this out.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
