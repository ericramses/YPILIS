using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.OrderAssociation
{
    public class OrderAssociationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public OrderAssociationTest()
        {
            this.m_PanelSetId = 316;
            this.m_PanelSetName = "Order Association";
            this.m_Abbreviation = "Order Association";
            this.m_CaseType = YellowstonePathology.Business.CaseType.ALLCaseTypes;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
            this.m_Active = true;
            this.m_AttemptOrderTargetLookup = false;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.OrderAssociation.OrderAssociationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.DoNotPublishReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;
            this.m_EnforceOrderTarget = false;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
