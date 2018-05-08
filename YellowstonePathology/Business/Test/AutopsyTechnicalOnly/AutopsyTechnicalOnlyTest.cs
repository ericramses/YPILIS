using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AutopsyTechnicalOnly
{
    public class AutopsyTechnicalOnlyTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public AutopsyTechnicalOnlyTest()
        {
            this.m_PanelSetId = 289;
            this.m_PanelSetName = "Autopsy Technical Only";
            this.m_Abbreviation = "ATPSYTCHONLY";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Technical;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.None;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyWordDocument).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;
            this.m_AcceptOnFinal = true;
            this.m_HasNoOrderTarget = true;

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
