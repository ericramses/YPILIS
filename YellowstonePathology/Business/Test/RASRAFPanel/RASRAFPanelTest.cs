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
            this.m_EpicDistributionIsImplemented = true;            

            string taskDescription = "Collect paraffin block from Histology and send to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics taskSendBlockToNeogenomics = new YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics();
            this.m_TaskCollection.Add(taskSendBlockToNeogenomics);            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
