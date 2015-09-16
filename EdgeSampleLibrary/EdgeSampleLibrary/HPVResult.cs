using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdgeSampleLibrary
{
    public class HPVResult
    {
        public static string OveralResultCodePositive = "HPVTWPSTV";

        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string IndeterminateResult = "Indeterminate";
        public static string QnsResult = "QNS";
        public static string LowDnaResult = "Low gDNA";
        public static string HighCVResult = "High %CV";
        public static string LowFamFozResult = "LowFamFoz";
        public static string Unsatisfactory = "Unsatisfactory";
        public static string InsuficientDNA = "Insufficient DNA to perform analysis";

        public static string CytologyReportNoPlaceHolder = "cytology_accessionno";

        public static string TestInformation = "Testing for high-risk HPV was performed using the Invader technology from Hologic after automated DNA extraction from the " +
            "ThinPrep sample.  The Invader chemistry is a proprietary signal amplification method capable of detecting low levels of target DNA.  Using analyte specific reagents, " +
            "the assay is capable of detecting genotypes 16, 18, 31, 33, 35, 39, 45, 51, 52, 56, 58, 59, 66 and 68.  The assay also evaluates specimen adequacy by measuring the " +
            "amount of normal human DNA present in the sample.  HPV types 16 & 18 are frequently associated with high risk for development of high grade dysplasia and anogenital " +
            "carcinoma.  HPV types 31/33/35/39/45/51/52/56/58/59/68 have also been classified as high-risk for the development of high grade dysplasia and anogenital carcinoma.  " +
            "HPV type 66 has been classified as probable high-risk.  A negative test result does not necessarily imply the absence of HPV infection as this assay targets only the " +
            "most common high-risk genotypes and insufficient sampling can affect results.  These results should be correlated with cytology and clinical exam results.";

        public static string References = "Darragh TM, Colgan TJ, Cox JT et al. The Lower Anogenital Squamous Terminology (LAST) Standardization Project for HPV-Associated Lesions: " +
            "Background and Consensus Recommendations from the College of American Pathologists and the American Society for Colposcopy and Cervical Pathology. Arch Pathol " +
            "Lab Med 2012 Oct; 136(10): 1266-97.";

        public static string InsufficientComment = "The quantity of genomic DNA present in the sample is insufficient to perform the analysis, even after an attempt to " +
            "increase DNA content by using more specimen volume.  There is no charge for this specimen.  Consider repeat testing, if clinically indicated.";

        public static string IndeterminateComment = "Results are indeterminate due to technical issues with this specific specimen, which may be related to specimen DNA " +
            "quality or interfering substances.  Consider repeat testing, if clinically indicated.";
        
        protected string m_ResultCode;
        protected string m_Result;
        protected string m_Comment;

        public HPVResult()
        {

        } 
        
        public string ResultCode
        {
            get { return this.m_ResultCode; }
        }              

        public string Result
        {
            get { return this.m_Result; }
        }
    }
}
