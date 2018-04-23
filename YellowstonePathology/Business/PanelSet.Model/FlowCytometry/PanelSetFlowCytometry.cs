using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model.FlowCytometry
{
    public class PanelSetFlowCytometry : PanelSet
    {               
        public PanelSetFlowCytometry()
        {
            this.m_CaseType = "Flow Cytometry";
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
			this.m_CaseType = this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;           

            this.m_HasTechnicalComponent = true;            
            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
        }       
    }
}
