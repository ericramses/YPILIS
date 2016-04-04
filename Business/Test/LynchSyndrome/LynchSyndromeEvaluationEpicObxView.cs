using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeEvaluationEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public LynchSyndromeEvaluationEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrderLynchSyndromeEvaluation, "Lynch Syndrome Evaluation");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(panelSetOrderLynchSyndromeEvaluation.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString("Comment:", document, "F");
            this.AddNextObxElement(panelSetOrderLynchSyndromeEvaluation.Comment, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrderLynchSyndromeEvaluation.Signature, document, "F");
            if (panelSetOrderLynchSyndromeEvaluation.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrderLynchSyndromeEvaluation.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(102, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
            if (panelSetOrderLynchSyndromeIHC != null)
            {
                this.AddNextObxElement("Mismatch Repair Protein Expression by Immunohistochemistry: ", document, "F");
                this.AddNextObxElement("MLH1: " + panelSetOrderLynchSyndromeIHC.MLH1Result, document, "F");
                this.AddNextObxElement("MSH2: " + panelSetOrderLynchSyndromeIHC.MSH2Result, document, "F");
                this.AddNextObxElement("MSH6: " + panelSetOrderLynchSyndromeIHC.MSH6Result, document, "F");
                this.AddNextObxElement("PMS2: " + panelSetOrderLynchSyndromeIHC.PMS2Result, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (panelSetOrderLynchSyndromeEvaluation.BRAFIsIndicated == true)
            {
                this.AddNextObxElement("Molecular Analysis", document, "F");
                this.AddNextObxElement("", document, "F");

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(18) == true) //BRAF
                {
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);

					this.AddNextObxElement("BRAF V600E Mutation by PCR: " + panelSetOrderBraf.ReportNo, document, "F");
					this.AddNextObxElement("Result: " + panelSetOrderBraf.Result, document, "F");
                    this.AddNextObxElement("", document, "F");   
                }
            }

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderLynchSyndromeEvaluation.OrderedOn, panelSetOrderLynchSyndromeEvaluation.OrderedOnId);
            this.AddNextObxElement("Specimem: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(panelSetOrderLynchSyndromeEvaluation.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(panelSetOrderLynchSyndromeEvaluation.References, document, "F");
            this.AddNextObxElement("", document, "F");
        }
    }
}
