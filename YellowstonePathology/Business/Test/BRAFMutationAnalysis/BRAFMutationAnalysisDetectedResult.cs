using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisDetectedResult : BRAFMutationAnalysisResult
    {
        public BRAFMutationAnalysisDetectedResult()
        {
            this.m_ResultCode = "BRAFMTTNANLDTCTD";
            this.m_Result = "Detected";

            this.m_Interpretation = "BRAF mutations are frequently found in human cancers. They are found most frequently in melanoma (50-70%), papillary thyroid cancer " +
                "(36 - 40 %) and most all hairy cell leukemias. BRAF mutations are also found with low frequency in colorectal cancer (5 - 12 %), non - small cell " +
                "lung cancer(NSCLC), acute myeloid leukemia(AML), glioma, sarcomas, breast cancer, hepatoma, and ovarian cancer.The presence of " +
                "BRAF mutation is believed to be mutually exclusive to the diagnosis of Lynch syndrome(3)." + Environment.NewLine + Environment.NewLine +
                "In patients with metastatic colorectal cancer(CRC), the therapeutic significance of BRAF mutation remains controversial.Some studies " +
                "suggest that it is similar to KRAS mutation, associated with resistance to anti - EGFR therapy.However, recent meta - analysis suggested " +
                "that patients with BRAF mutation, when treated with anti - EGFR, show similar response to patients without any mutation. " + Environment.NewLine + Environment.NewLine +
                "Patients with BRAF mutation may respond to therapy including BRAF and MEK inhibitors or anti - VEGF antibodies";

            this.m_Method = "DNA was isolated from cells or microdissection-enriched FFPE tissue. Tumor in FFPE must be present in at least 20% of the tissue. BRAF " +
                "mutations were evaluated in the entire coding region of BRAF exon 15 by high-sensitivity Sanger sequencing which improves the lower " +
                "detection limit in mutation hotspot regions to approximately 1 % abnormal DNA.This includes V600 mutations and mutations in adjacent " +
                "codons 598, 599, and 601.Mutation detection outside these hotspot regions has a typical lower detection limit of 10 - 15 % mutated BRAF " +
                "in a wild-type background.The patient’s sequence is compared to the NCBI database: NM_004333.Various factors including quantity and " +
                "quality of nucleic acid, sample preparation and sample age can affect assay performance.";

            this.m_References = "1. Flaherty KT, et al., Inhibition of mutated, activated BRAF in metastatic melanoma. N Engl J Med. 2010; 363:809-819. " + Environment.NewLine +
                "2.Halaban R, et al., PLX4032, a selective BRAF(V600E)kinase inhibitor, activates the ERK pathway and enhances cell migration and " + Environment.NewLine +
                "proliferation of BRAF melanoma cells.Pigment Cell Melanoma Res. 2010; 23:190 - 200. " + Environment.NewLine +
                "3.Sharma SG, Gulley ML. BRAF mutation testing in colorectal cancer. Arch Pathol Lab Med. 2010 Aug; 134(8):1225 - 8. " + Environment.NewLine +
                "4.Flaherty KT.Next generation therapies change the landscape in melanoma.F1000 Medicine Reports. 2011; 3:8. " + Environment.NewLine +
                "5.Xing M.BRAF mutation in thyroid cancer. Endocr Relat Cancer. 2005 Jun; 12(2):245 - 62.";
        }
    }
}