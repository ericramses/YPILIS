using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWIEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
		public HPVTWIEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {            
            HPVTWITestOrder panelSetOrder = (HPVTWITestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "High Risk HPV Report");
            this.AddNextObxElement("", document, "F");            

            string resultText = "Result: " + panelSetOrder.Result;
            this.AddNextObxElement(resultText, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Specimen: ThinPrep fluid", document, "F");
            this.AddNextObxElement("", document, "F");

            bool hpvHasBeenOrdered = this.m_AccessionOrder.PanelSetOrderCollection.Exists(62);

            string additionalTestingComment = string.Empty;
            if (hpvHasBeenOrdered == true)
            {
				additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.HPV1618HasBeenOrderedComment;                
            }
            else
            {
				additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.NoAdditionalTestingOrderedComment;
            }

            this.AddNextObxElement("Additional Testing:", document, "F");
            this.AddNextObxElement(additionalTestingComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Test Information: ", document, "F");
            string method = "Testing for high-risk HPV was performed using the Invader technology from Hologic after automated DNA extraction from a ThinPrep sample.  The Invader chemistry is a proprietary signal amplification method capable of detecting low levels of target DNA.  Using analyte specific reagents, the assay is capable of detecting genotypes 16, 18, 31, 33, 35, 39, 45, 51, 52, 56, 58, 59, 66 and 68.  The assay also evaluates specimen adequacy by measuring the amount of normal human DNA present in the sample. HPV types 16 & 18 are frequently associated with high risk for development of high grade cervical dysplasia and cervical carcinoma.  HPV types 31/33/35/39/45/51/52/56/58/59/68 have also been classified as high-risk for the development of high grade cervical dysplasia and cervical carcinoma.  HPV type 66 has been classified as probable high-risk. A negative test result does not necessarily imply the absence of HPV infection as this assay targets only the most common high-risk genotypes and insufficient cervical sampling can affect results.  These results should be correlated with Pap smear and clinical exam results."; 
            this.AddNextObxElement(method, document, "F");            
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");
            string references = "Wright TC, Jr., Massad LS, Dunton CJ, Spitzer M, Wilkinson EJ, and Solomon D.2007. 2006 consensus guidelines for the management of women with abnormal cervical cancer screening tests. Am J Obstet Gynecol 197(4): 346-55.";
            this.AddNextObxElement(references, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("This test was performed using a US FDA approved DNA probe kit.  The FDA procedure was performed using a modified DNA extraction method for test optimization, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the responsibility for test performance.", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
