using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Trichomonas
{
    public class TrichomonasEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public TrichomonasEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            TrichomonasTestOrder panelSetOrder = (TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "Trichomonas Vaginalis");

            this.AddNextObxElementBeaker("RESULT", "Result: " + panelSetOrder.Result, document, "F");

            this.AddNextObxElementBeaker("RESULTREFERENCE", "Reference Range: Negative", document, "F");

            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("SPECIMENDESCRIPTION", "Specimen Description: " + specimenOrder.Description, document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElementBeaker("COLLECTIONDATETIME", "Collection Date/Time: " + collectionDateTimeString, document, "F");

            this.AddNextObxElementBeaker("METHOD", "Method: " + panelSetOrder.Method, document, "F");

            this.AddNextObxElementBeaker("REFERENCES", "References: " + 
                "Jordan JA, Lowery D, Trucco M. Taqman-Based Detection of Trichomonas vaginalis DNA from female genitals specimens. " +
                "J Clin Micro 2001; 3819-22", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("LOCATIONPERFORMED", "Location Performed: " + locationPerformed, document, "F");
        }
    }
}
