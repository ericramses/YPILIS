using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
    public class BCellEnumerationCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public BCellEnumerationCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            BCellEnumerationTestOrder panelSetOrder = (BCellEnumerationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("B-Cell Enumeration", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            StringBuilder result = new StringBuilder("WBC: " + panelSetOrder.WBC.ToString() + "/uL (from client)");
            this.AddNextNteElement(result.ToString(), document);
            result = new StringBuilder("Lymphocyte Percentage: " + panelSetOrder.LymphocytePercentage.ToString().StringAsPercent());
            this.AddNextNteElement(result.ToString(), document);
            result = new StringBuilder("CD19 B-Cell Positive Percent: " + panelSetOrder.CD19BCellPositivePercent.ToString().StringAsPercent());
            this.AddNextNteElement(result.ToString(), document);
            result = new StringBuilder("CD20 B-Cell Positive Percent: " + panelSetOrder.CD20BCellPositivePercent.ToString().StringAsPercent());
            this.AddNextNteElement(result.ToString(), document);
            result = new StringBuilder("CD19 Absolute Count: " + panelSetOrder.CD19AbsoluteCount.ToString() + "/uL");
            this.AddNextNteElement(result.ToString(), document);
            result = new StringBuilder("CD20 Absolute Count: " + panelSetOrder.CD20AbsoluteCount.ToString() + "/uL");
            this.AddNextNteElement(result.ToString(), document);

            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Description: " + specimenOrder.Description, document);
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Method:", document);
            this.HandleLongString(panelSetOrder.Method, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("References:", document);
            this.HandleLongString(panelSetOrder.ReportReferences, document);
            this.AddBlankNteElement(document);

            this.HandleLongString(panelSetOrder.ASRComment, document);
            this.AddBlankNteElement(document);

            this.HandleLongString(panelSetOrder.GetLocationPerformedComment(), document);
            this.AddBlankNteElement(document);
        }
    }
}
