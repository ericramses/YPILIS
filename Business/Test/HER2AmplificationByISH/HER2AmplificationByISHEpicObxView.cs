using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
		public HER2AmplificationByISHEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			HER2AmplificationByISHTestOrder panelSetOrder = (HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);                        
            if (panelSetOrder.Indicator == "Breast")
            {
                this.BreastToXml(document, panelSetOrder);
            }
            else if (panelSetOrder.Indicator == "Gastric")
            {
                this.GastricToXml(document, panelSetOrder);
            }            
        }

		public void BreastToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            string referenceRange = "Based on 2013 CAP/ASCO guidelines, a case is considered POSITIVE when the HER2 to Chr17 ratio is >=2.0 with any average " +
                "HER2 copy number or when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number >=6.0 signals/nucleus, " +
                "EQUIVOCAL when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number >=4.0 and <6.0 signals/cell, and " +
                "NEGATIVE when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number < 4.0 signals/cell.";

            this.AddHeader(document, panelSetOrder, "HER2 Gene Amplification");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("HER2: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("Ratio: " + panelSetOrder.Her2Chr17Ratio, document, "F");
            this.AddNextObxElement("Average HER2 Copy Number = " + panelSetOrder.AverageHer2NeuSignal, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddNextObxElement("Reference Range: " + referenceRange, document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
				this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextObxElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document, "F");
            this.AddNextObxElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal, document, "F");
            this.AddNextObxElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextObxElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.Her2Chr17Ratio, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string specimenDescriptionString = specimenOrder.Description + " - " + aliquotOrder.Label;

            this.AddNextObxElement("Specimen Description: " + specimenDescriptionString, document, "F");            

            this.AddNextObxElement("Fixation Type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");

            this.AddNextObxElement("Fixation Duration: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Method: ", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");            

            this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(panelSetOrder.ReportReference, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
            {
                this.AddNextObxElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
                this.HandleLongString(amendment.Text, document, "C");
                if (amendment.RequirePathologistSignature == true)
                {
                    this.AddNextObxElement("Signature: " + amendment.PathologistSignature, document, "C");
                }
                this.AddNextObxElement("", document, "C");
            }

			this.HandleLongString(panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }

		public void GastricToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            this.AddHeader(document, panelSetOrder, "HER2 Gene Amplification");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("HER2: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("Ratio: " + panelSetOrder.Her2Chr17Ratio, document, "F");
            this.AddNextObxElement("Reference Range: Negative < 2, Positive >= 2", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
				this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextObxElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document, "F");
            this.AddNextObxElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal, document, "F");
            this.AddNextObxElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextObxElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.Her2Chr17Ratio, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string specimenDescriptionString = specimenOrder.Description + " - " + aliquotOrder.Label;

            this.AddNextObxElement("Specimen Description: " + specimenDescriptionString, document, "F");            

            this.AddNextObxElement("Fixation Type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");            
            this.AddNextObxElement("Fixation Duration: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Method: ", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(panelSetOrder.ReportReference, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
            {
                this.AddNextObxElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
                this.HandleLongString(amendment.Text, document, "C");
                if (amendment.RequirePathologistSignature == true)
                {
                    this.AddNextObxElement("Signature: " + amendment.PathologistSignature, document, "C");
                }
                this.AddNextObxElement("", document, "C");
            }

            this.AddNextObxElement(panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }       
    }
}
