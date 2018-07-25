using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.CalreticulinMutationAnalysis
{
	[PersistentClass("tblCalreticulinMutationAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class CalreticulinMutationAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
        private string m_Result;
        private string m_Percentage;
        private string m_Mutations;
        private string m_Interpretation;
        private string m_Method;
        private string m_ASR;

        public CalreticulinMutationAnalysisTestOrder()
        {

        }

		public CalreticulinMutationAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_ASR = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has not been approved by the FDA. The FDA has " +
                "determined such clearance or approval is not necessary.This laboratory is CLIA certified to perform high complexity clinical testing.";
            this.m_Method = "Total nucleic acid was extracted from patient’s plasma or PB/BM cells. Primer pairs were designed to encompass the JAK2 exons 12, 13, 14 and the V617F point mutation as well " +
                "as CALR exon 9 and MPL exon 10, which includes W515 and S505 point mutations.The PCR products were then purified and sequenced in both forward and reverse directions. " +
                "The JAK2 V617F utilizes a high sensitivity sequencing assay which can detect mutations at a 1 % level.The remaining sequencing tests have a sensitivity of 10 - 15 % for detecting " +
                "mutations in the wild - type background.Fragment analysis is also performed for CALR.Fragment length analysis is performed to further determine very low levels of heterozygous " +
                "insertions / deletions, which may be missed by sequencing.Fragment analysis has a sensitivity of 5 % for detecting heterozygous insertion / deletions in the wild - type background. " +
                "Various factors including quantity and quality of nucleic acid, sample preparation and sample age can affect assay performance.";     
            this.m_ReportReferences = "Nangalia J, et al. Somatic CALR mutations in myeloproliferative neoplasms with nonmutated JAK2, " + Environment.NewLine +
                "N Engl J Med. 2013 Dec 19; 369(25):2391 - 405. " + Environment.NewLine +
                "Klampfl T, et al. Somatic mutations of calreticulin in myeloproliferative neoplasms. N Engl J Med. 2013 Dec 19; 369(25):2379 - 90.";
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Percentage
        {
            get { return this.m_Percentage; }
            set
            {
                if (this.m_Percentage != value)
                {
                    this.m_Percentage = value;
                    this.NotifyPropertyChanged("Percentage");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Mutations
        {
            get { return this.m_Mutations; }
            set
            {
                if (this.m_Mutations != value)
                {
                    this.m_Mutations = value;
                    this.NotifyPropertyChanged("Mutations");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Method
        {
            get { return this.m_Method; }
            set
            {
                if (this.m_Method != value)
                {
                    this.m_Method = value;
                    this.NotifyPropertyChanged("Method");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string ASR
        {
            get { return this.m_ASR; }
            set
            {
                if (this.m_ASR != value)
                {
                    this.m_ASR = value;
                    this.NotifyPropertyChanged("ASR");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}
	}
}
