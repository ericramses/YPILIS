﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ABL1KinaseDomainMutation
{
	public class ABL1KinaseDomainMutationEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
	{
		public ABL1KinaseDomainMutationEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			ABL1KinaseDomainMutationTestOrder testOrder = (ABL1KinaseDomainMutationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, testOrder, "ABL1 Kinase Domain Mutation");

			this.AddNextObxElement("", document, "F");
			string result = "Result: " + testOrder.Result;
			this.AddNextObxElement(result, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Pathologist: " + testOrder.Signature, document, "F");
			if (testOrder.FinalTime.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}

			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
			this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(testOrder.Interpretation, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(testOrder.Method, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(testOrder.References, document, "F");

			this.AddNextObxElement("", document, "F");
			this.HandleLongString(testOrder.ASRComment, document, "F");

			this.AddNextObxElement("", document, "F");
			string locationPerformed = testOrder.GetLocationPerformedComment();
			this.AddNextObxElement(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
