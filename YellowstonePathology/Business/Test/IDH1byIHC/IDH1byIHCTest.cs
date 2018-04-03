using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.IDH1byIHC
{
    public class IDH1byIHCTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public IDH1byIHCTest()
        {
            this.m_PanelSetId = 273;
            this.m_PanelSetName = "IDH1 by IHC";
            this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_IsBillable = true;            
            this.m_HasNoOrderTarget = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(10);            

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            string taskDescription = "Collect paraffin block from Histology and send to Neo.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, new Facility.Model.NeogenomicsIrvine()));
        }
    }
}
