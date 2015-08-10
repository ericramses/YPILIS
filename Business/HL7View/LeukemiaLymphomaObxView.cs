using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View
{
	public class LeukemiaLymphomaObxView
	{
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Amendment.Model.AmendmentItemCollection m_AmendmentItemCollection;
		YellowstonePathology.Business.Test.PanelSetOrderLeukemiaLymphoma m_PanelSetOrderLeukemiaLymphoma;
		string m_DateFormat = "yyyyMMddHHmm";
		string m_ReportNo;
		int m_ObxCount;

        public LeukemiaLymphomaObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Amendment.Model.AmendmentItemCollection amendmentItemCollection, string reportNo, int obxCount)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_AmendmentItemCollection = amendmentItemCollection;
			this.m_PanelSetOrderLeukemiaLymphoma = (YellowstonePathology.Business.Test.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection[0];
			this.m_ReportNo = reportNo;
			this.m_ObxCount = obxCount;
		}

		public int ObxCount
		{
			get { return this.m_ObxCount; }
		}

		public void ToXml(XElement document)
		{
            this.AddNextObxElement("~", document, "F");
            this.AddNextObxElement("Yellowstone Pathology", document, "F");
			this.AddNextObxElement("Leukemia/Lymphoma Phenotyping Report", document, "F");
            this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Patient Name: " + this.m_AccessionOrder.PatientDisplayName, document, "F");
			this.AddNextObxElement("Master Accession: " + this.m_AccessionOrder.MasterAccessionNo.ToString(), document, "F");
            this.AddNextObxElement("Report No: " + this.m_ReportNo, document, "F");
			this.AddNextObxElement("MRN: " + this.m_AccessionOrder.SvhMedicalRecord, document, "F");
			this.AddNextObxElement("Encounter: " + this.m_AccessionOrder.SvhAccount, document, "F");
			this.AddNextObxElement("DOB: " + this.m_AccessionOrder.PBirthdate.Value.ToString(this.m_DateFormat), document, "F");
			this.AddNextObxElement("Provider: " + this.m_AccessionOrder.PhysicianName, document, "F");
			this.AddNextObxElement("Pathologist: " + m_PanelSetOrderLeukemiaLymphoma.Signature, document, "F");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Impression: " + this.m_PanelSetOrderLeukemiaLymphoma.Impression, document, "F");
			this.HandleLongString("Interperative Comment: " + this.m_PanelSetOrderLeukemiaLymphoma.InterpretiveComment, document, "F");
            this.AddNextObxElement("", document, "F");

            foreach (YellowstonePathology.Business.Amendment.Model.AmendmentItem amendmentItem in this.m_AmendmentItemCollection)
			{
				this.AddNextObxElement(amendmentItem.AmendmentType + ": " + amendmentItem.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
				this.HandleLongString(amendmentItem.Amendment, document, "C");
				this.AddNextObxElement("Signature: " + amendmentItem.PathologistSignature, document, "C");
				this.AddNextObxElement("", document, "C");
			}

			this.AddNextObxElement("Cell Polulation Of Interest: " + this.m_PanelSetOrderLeukemiaLymphoma.CellPopulationOfInterest, document, "F");

			this.AddNextObxElement("Marker; Interpretation; Intensity", document, "C");
			foreach (Flow.FlowMarkerItem flowMarkerItem in this.m_PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection)
			{
				this.AddNextObxElement(flowMarkerItem.Name + "; " + flowMarkerItem.Interpretation + "; " + flowMarkerItem.Intensity, document, "F");
			}
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Cell Distribution: ", document, "F");
			this.AddNextObxElement("Lymphocytes: " + this.m_PanelSetOrderLeukemiaLymphoma.LymphocyteCountPercent.ToString("p"), document, "F");
			this.AddNextObxElement("Monocytes: " + this.m_PanelSetOrderLeukemiaLymphoma.MonocyteCountPercent.ToString("p"), document, "F");
			this.AddNextObxElement("Myeloid: " + this.m_PanelSetOrderLeukemiaLymphoma.MyeloidCountPercent.ToString("p"), document, "F");
			this.AddNextObxElement("Dim Cd45/Mod SS: " + this.m_PanelSetOrderLeukemiaLymphoma.DimCD45ModSSCountPercent.ToString("p"), document, "F");
			this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Blast Marker Percentages (as % of nucleated cells):", document, "F");
			this.AddNextObxElement("CD34: " + this.m_PanelSetOrderLeukemiaLymphoma.EGateCD34Percent, document, "F");
			this.AddNextObxElement("CD117: " + this.m_PanelSetOrderLeukemiaLymphoma.EGateCD117Percent, document, "F");
			this.AddNextObxElement("", document, "F");

            string clinicalHistory = "None";
            if (!string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory))
            {
				clinicalHistory = this.m_AccessionOrder.ClinicalHistory;
            }
			this.HandleLongString("Clinical History: " + clinicalHistory, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Specimen type: " + this.m_AccessionOrder.SpecimenOrderCollection[0].Description, document, "F");
			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Specimen Adequacy: " + this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenAdequacy, document, "F");
			this.AddNextObxElement("", document, "F");

            //this.AddPdfObx(panelSetOrder, document);
		}

        /*private void AddPdfObx(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, XElement document)
        {
            YellowstonePathology.Business.Document.SurgicalReport surgicalReport = new Document.SurgicalReport();
            surgicalReport.Publish(this.m_AccessionOrder.MasterAccessionNo, panelSetOrder.ReportNo, Document.ReportSaveModeEnum.Normal);
            YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(panelSetOrder.ReportNo);

            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNamePDF(panelSetOrder.ReportNo);
            byte[] bytes = System.IO.File.ReadAllBytes(fileName);
            string fileString = Convert.ToBase64String(bytes);

            XElement obxPdfElement = new XElement("OBX",
                new XElement("OBX.1",
                    new XElement("OBX.1.1", this.m_ObxCount.ToString())),
                new XElement("OBX.2",
                    new XElement("OBX.2.1", "ED")),
                new XElement("OBX.3",
                    new XElement("OBX.3.1", "REPORT"),
                    new XElement("OBX.3.2", "PATHOLOGY REPORT")),
                new XElement("OBX.4", 
                    new XElement("OBX.4.1", "1")),
                new XElement("OBX.5",
                    new XElement("OBX.5.1", ""),
                    new XElement("OBX.5.2", ""),
                    new XElement("OBX.5.3", "PDF"),
                    new XElement("OBX.5.4", "Base64"),
                    new XElement("OBX.5.5", fileString)));
            document.Add(obxPdfElement);            
        }*/

		private void AddNextObxElement(string value, XElement document, string observationResultStatus)
		{
			XElement obxElement = new XElement("OBX",
				new XElement("OBX.1",
					new XElement("OBX.1.1", this.m_ObxCount.ToString())),
				new XElement("OBX.2",
					new XElement("OBX.2.1", "TX")),
				new XElement("OBX.3",
                    new XElement("OBX.3.1", "&GDT")),
				new XElement("OBX.5",
					new XElement("OBX.5.1", value)),
				new XElement("OBX.11",
					new XElement("OBX.11.1", observationResultStatus)));

			this.m_ObxCount++;
            document.Add(obxElement);
		}

        private void HandleLongString(string value, XElement document, string observationResultStatus)
        {
            string[] textSplit = value.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries); 
            foreach (string text in textSplit)
            {
                this.AddNextObxElement(text, document, observationResultStatus);
            }
        }
	}
}
