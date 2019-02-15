using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HoldForFlow
{
    public class HoldForFlowCMMCView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public HoldForFlowCMMCView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            HoldForFlowTestOrder panelSetOrder = (HoldForFlowTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            CultureAndHoldForCytogeneticsTest cultureAndHoldForCytogeneticsTest = new HoldForFlow.CultureAndHoldForCytogeneticsTest();
            DirectHarvestForFISHTest directHarvestForFISHTest = new HoldForFlow.DirectHarvestForFISHTest();
            ExtractAndHoldForMolecular.ExtractAndHoldForMolecularTest extractAndHoldForMolecularTest = new ExtractAndHoldForMolecular.ExtractAndHoldForMolecularTest();
            string title = "Hold for Flow";
            if (panelSetOrder.PanelSetName == cultureAndHoldForCytogeneticsTest.PanelSetName) title = "Hold For Cytogenetics";
            else if (panelSetOrder.PanelSetName == directHarvestForFISHTest.PanelSetName) title = "Hold For FISH";
            else if (panelSetOrder.PanelSetName == extractAndHoldForMolecularTest.PanelSetName) title = "Hold For Molecular";

            this.AddNextNteElement(title, document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Comment: " + panelSetOrder.Comment, document);
            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder);
            this.AddBlankNteElement(document);
        }
    }
}
