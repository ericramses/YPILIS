﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly
{
    public class ChromosomeAnalysisForFetalAnomalCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public ChromosomeAnalysisForFetalAnomalCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            ChromosomeAnalysisForFetalAnomalyTestOrder panelSetOrder = (ChromosomeAnalysisForFetalAnomalyTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("HPV-16/18 Genotyping By PCR", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);

            this.AddBlankNteElement(document);
            string result = "Result: " + panelSetOrder.Result;
            this.AddNextNteElement(result, document);
            result = "  Karyotype : " + panelSetOrder.Karyotype;
            this.AddNextNteElement(result, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Pathologist: " + panelSetOrder.Signature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder);

            this.AddNextNteElement("Specimen Information:", document);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Identification: " + specimenOrder.Description, document);
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Interpretation:", document);
            this.HandleLongString(panelSetOrder.Interpretation, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Test Details:", document);
            this.HandleLongString(panelSetOrder.TestDetails, document);

            this.AddBlankNteElement(document);
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
