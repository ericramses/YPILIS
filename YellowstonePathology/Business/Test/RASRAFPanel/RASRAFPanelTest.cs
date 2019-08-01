﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class RASRAFPanelTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public RASRAFPanelTest()
        {
            this.m_PanelSetId = 218;
            this.m_PanelSetName = "RAS/RAF Panel";
            this.m_Abbreviation = "RAS/RAF";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelWordDocument).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(14);

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            string taskDescription = "Collect paraffin block from Histology and send to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics taskSendBlockToNeogenomics = new YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics();
            this.m_TaskCollection.Add(taskSendBlockToNeogenomics);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81479", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
