using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	public class ComprehensiveColonCancerProfileEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public ComprehensiveColonCancerProfileEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {			            
			ComprehensiveColonCancerProfile comprehensiveColonCancerProfile = (ComprehensiveColonCancerProfile)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult = new ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, comprehensiveColonCancerProfile);

            this.AddHeader(document, comprehensiveColonCancerProfile, "Comprehensive Colon Cancer Profile");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(comprehensiveColonCancerProfile.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + comprehensiveColonCancerProfile.Signature, document, "F");
            if (comprehensiveColonCancerProfile.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + comprehensiveColonCancerProfile.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            string specimenDescription = comprehensiveColonCancerProfileResult.SpecimenOrder.GetSpecimenDescriptionString();
			this.HandleLongString("Specimen:", document, "F");
            this.HandleLongString(specimenDescription, document, "F");
            this.AddNextObxElement("", document, "F");

			this.HandleLongString("Diagnosis:", document, "F");
            this.HandleLongString(comprehensiveColonCancerProfileResult.SurgicalSpecimen.Diagnosis, document, "F");
            this.AddNextObxElement("", document, "F");

			this.HandleLongString("AJCC Pathologic Staging:", document, "F");
            this.HandleLongString(comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.AJCCStage, document, "F");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.ReportNo, document, "F");
			this.AddNextObxElement("", document, "F");            

            this.AddNextObxElement("Mismatch Repair Protein Expression by Immunohistochemistry: ", document, "F");
			this.AddNextObxElement("MLH1: " + comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHC.MLH1Result, document, "F");
			this.AddNextObxElement("MSH2: " + comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHC.MSH2Result, document, "F");
			this.AddNextObxElement("MSH6: " + comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHC.MSH6Result, document, "F");
			this.AddNextObxElement("PMS2: " + comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHC.PMS2Result, document, "F");
			this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHC.ReportNo, document, "F");
			this.AddNextObxElement("", document, "F");

            DateTime testChangeDate = DateTime.Parse("11/23/2015");
            if (comprehensiveColonCancerProfile.OrderDate.Value > testChangeDate)
            {
                if (comprehensiveColonCancerProfileResult.RASRAFIsOrdered == true)
                {
                    this.AddNextObxElement("Molecular Analysis", document, "F");
                    this.AddNextObxElement("KRAS Mutation Analysis:  " + comprehensiveColonCancerProfileResult.RASRAFTestOrder.KRASResult, document, "F");
                    this.AddNextObxElement("BRAF V600E/K Mutation Analysis:  " + comprehensiveColonCancerProfileResult.RASRAFTestOrder.BRAFResult, document, "F");
                    this.AddNextObxElement("NRAS Mutation Analysis:  " + comprehensiveColonCancerProfileResult.RASRAFTestOrder.NRASResult, document, "F");
                    this.AddNextObxElement("HRAS Mutation Analysis:  " + comprehensiveColonCancerProfileResult.RASRAFTestOrder.HRASResult, document, "F");
                    this.AddNextObxElement("Reference Report:  " + comprehensiveColonCancerProfileResult.RASRAFTestOrder.ReportNo, document, "F");
                    this.AddNextObxElement("", document, "F");
                }
            }
            else
            {
                if (comprehensiveColonCancerProfileResult.BRAFV600EKIsOrdered == true ||
                    comprehensiveColonCancerProfileResult.KRASStandardIsOrderd == true ||
                    comprehensiveColonCancerProfileResult.KRASExon23MutationIsOrdered ||
                    comprehensiveColonCancerProfileResult.KRASExon4MutationIsOrdered ||
                    comprehensiveColonCancerProfileResult.KRASExon4MutationIsOrdered ||
                    comprehensiveColonCancerProfileResult.NRASMutationAnalysisIsOrdered ||
                    comprehensiveColonCancerProfileResult.MLHIsOrdered == true)
                {
                    this.AddNextObxElement("Molecular Analysis", document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis != null)
                {
                    this.AddNextObxElement("MLH1 Promoter Methylation Analysis:  " + comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.Result, document, "F");
                    this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.ReportNo, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (comprehensiveColonCancerProfileResult.KRASStandardIsOrderd == true)
                {
                    this.AddNextObxElement("KRAS Mutation Analysis:  " + comprehensiveColonCancerProfileResult.KRASStandardTestOrder.Result, document, "F");
                    this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.KRASStandardTestOrder.ReportNo, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (comprehensiveColonCancerProfileResult.KRASExon23MutationIsOrdered == true)
                {
                    this.AddNextObxElement("KRAS Exon 23 Mutation Analysis:  " + comprehensiveColonCancerProfileResult.KRASExon23MutationTestOrder.Result, document, "F");
                    this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.KRASExon23MutationTestOrder.ReportNo, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (comprehensiveColonCancerProfileResult.KRASExon4MutationIsOrdered == true)
                {
                    this.AddNextObxElement("KRAS Exon 4 Mutation Analysis: " + comprehensiveColonCancerProfileResult.KRASExon4MutationTestOrder.Result, document, "F");
                    this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.KRASExon4MutationTestOrder.ReportNo, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (comprehensiveColonCancerProfileResult.BRAFV600EKIsOrdered == true)
                {
                    this.AddNextObxElement("BRAF V600E/K Mutation Analysis: " + comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.Result, document, "F");
                    this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.ReportNo, document, "F");
                    this.AddNextObxElement("", document, "F");
                }

                if (comprehensiveColonCancerProfileResult.NRASMutationAnalysisIsOrdered == true)
                {
                    this.AddNextObxElement("NRAS Mutation Analysis: " + comprehensiveColonCancerProfileResult.NRASMutationAnalysisTestOrder.Result, document, "F");
                    this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.NRASMutationAnalysisTestOrder.ReportNo, document, "F");
                    this.AddNextObxElement("", document, "F");
                }
            }
        }
    }
}
