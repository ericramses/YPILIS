using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	public class ComprehensiveColonCancerProfileEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
	{
		public ComprehensiveColonCancerProfileEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
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

			if (comprehensiveColonCancerProfileResult.BRAFV600EKIsOrdered == true ||
				comprehensiveColonCancerProfileResult.KRASStandardIsOrderd == true ||
				comprehensiveColonCancerProfileResult.MLHIsOrdered == true)
			{
				this.AddNextObxElement("Molecular Analysis", document, "F");
				this.AddNextObxElement("", document, "F");
			}

			if (comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis != null)
			{
				this.AddNextObxElement("MLH1 Promoter Methylation Analysis:  " + comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.ReportNo, document, "F");
				this.AddNextObxElement("Result: " + comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.Result, document, "F");
				this.AddNextObxElement("", document, "F");
			}

			if (comprehensiveColonCancerProfileResult.KRASStandardIsOrderd == true)
            {
				this.AddNextObxElement("KRAS Standard Mutation Analysis: " + comprehensiveColonCancerProfileResult.KRASStandardTestOrder.ReportNo, document, "F");
                this.AddNextObxElement("Result: " + comprehensiveColonCancerProfileResult.KRASStandardTestOrder.Result, document, "F");                
                this.AddNextObxElement("", document, "F");
			}

			if (comprehensiveColonCancerProfileResult.BRAFV600EKIsOrdered == true)
            {
				this.AddNextObxElement("BRAF V600E/K Mutation by PCR: " + comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.ReportNo, document, "F");
				this.AddNextObxElement("Result: " + comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.Result, document, "F");
				this.AddNextObxElement("", document, "F");
            }
        }
	}
}
