/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 9:49 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
	/// <summary>
	/// Description of BCellEnumerationEpic.
	/// </summary>
	public class BCellEnumerationEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
	{
		public BCellEnumerationEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			BCellEnumerationTestOrder panelSetOrder = (BCellEnumerationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, panelSetOrder, "B-Cell Enumeration");

			this.AddNextObxElement("", document, "F");
			string result = "Result: " + panelSetOrder.Result;
			this.AddNextObxElement(result, document, "F");
			result = "WBC: " + panelSetOrder.WBC;
			this.AddNextObxElement(result, document, "F");
			result = "Lymphocyte Percentage: " + panelSetOrder.LymphocytePercentage;
			this.AddNextObxElement(result, document, "F");
			result = "CD19 B-Cell Positive Count: " + panelSetOrder.CD19BCellPositiveCount;
			this.AddNextObxElement(result, document, "F");
			result = "CD19 B-Cell Positive Percent: " + panelSetOrder.CD19BCellPositivePercent;
			this.AddNextObxElement(result, document, "F");
			result = "CD20 B-Cell Positive Count: " + panelSetOrder.CD20BCellPositiveCount;
			this.AddNextObxElement(result, document, "F");
			result = "CD20 B-Cell Positive Percent: " + panelSetOrder.CD20BCellPositivePercent;
			this.AddNextObxElement(result, document, "F");
			result = "CD19 Absolute Count: " + panelSetOrder.CD19AbsoluteCount;
			this.AddNextObxElement(result, document, "F");
			result = "CD20 Absolute Count: " + panelSetOrder.CD20AbsoluteCount;
			this.AddNextObxElement(result, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
			if (panelSetOrder.FinalTime.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}

			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(panelSetOrder.Interpretation, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(panelSetOrder.References, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement(panelSetOrder.ASRComment, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement(panelSetOrder.GetLocationPerformedComment(), document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
