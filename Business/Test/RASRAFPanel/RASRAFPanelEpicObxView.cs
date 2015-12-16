using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class RASRAFPanelEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
        public RASRAFPanelEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            RASRAFPanelTestOrder panelSetOrder = (RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "RAS/RAF Panel");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("BRAF Result: " + panelSetOrder.BRAFResult, document, "F");
            this.AddNextObxElement("KRAS Result: " + panelSetOrder.KRASResult, document, "F");
            this.AddNextObxElement("NRAS Result: " + panelSetOrder.NRASResult, document, "F");
            this.AddNextObxElement("HRAS Result: " + panelSetOrder.HRASResult, document, "F");
            
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString(panelSetOrder.References, document, "F");

            this.AddNextObxElement("", document, "F");
            this.HandleLongString(panelSetOrder.ReportDisclaimer, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
