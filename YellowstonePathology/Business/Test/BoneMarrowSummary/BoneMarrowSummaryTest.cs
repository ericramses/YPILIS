﻿using System;
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

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
        }
    }
}
