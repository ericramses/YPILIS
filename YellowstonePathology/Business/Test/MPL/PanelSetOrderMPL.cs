using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.MPL
{
	[PersistentClass("tblPanelSetOrderMPL", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderMPL : PanelSetOrder
	{
		private string m_Result;
		private string m_Interpretation;
		private string m_Method;
        private string m_ASR;

		public PanelSetOrderMPL()
        {
            this.m_Method = "The total nucleic acid was extracted from patient�s plasma or PB/BM cells. Primer pairs were designed to encompass the MPL exon " +
                "10, which includes W515 and S505 point mutations.The PCR product was then purified and sequenced in both forward and reverse " +
                "directions.The resulting sequence is compared to Genebank Acce# NM_005373 reference sequence. This is a sequencing-based assay " +
                "which has a typical sensitivity of 10 - 15 % for detecting mutated MPL in the wild - type background.Various factors including quantity and " +
                "quality of nucleic acid, sample preparation and sample age can affect assay performance.";

            this.m_ASR = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has not been approved by the FDA. " +
                "The FDA has determined such clearance or approval is not necessary.This laboratory is CLIA certified to perform high complexity clinical testing.";

            this.m_ReportReferences = "1. Pikman Y, et al. MPL W515L is a novel somatic activating mutation in myelofibrosis with myeloid metaplasia. PLoS Med. " +
                "2006; 3:1140 - 1151.";
        }

		public PanelSetOrderMPL(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{

		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
