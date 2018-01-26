﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.COL1A1PDGFB
{
    public class COL1A1PDGFBTest : PanelSet.Model.PanelSetMolecularTest
    {
        public COL1A1PDGFBTest()
        {
            this.m_PanelSetId = 285;
            this.m_PanelSetName = "COL1A1-PDGFB";
            this.m_Abbreviation = "COL1A1-PDGFB";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_AttemptOrderTargetLookup = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(7, 0, 0, 0);

            string taskDescription = "Gather materials and send out to Mayo Clinic.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, new Facility.Model.MayoClinic()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.MayoClinic();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.MayoClinic();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.MayoClinic();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.MayoClinic();

            this.m_HasSplitCPTCode = false;
            //this.m_RequiresAssignment = true;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}