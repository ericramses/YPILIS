using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	[PersistentClass("tblChromosomeAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class ChromosomeAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Karyotype;
		private string m_Interpretation;
		private string m_Comment;
		private string m_MetaphasesCounted;
		private string m_MetaphasesAnalyzed;
		private string m_MetaphasesKaryotyped;
		private string m_CultureType;
		private string m_BandingTechnique;
		private string m_BandingResolution;
        private string m_ASR;
		
		public ChromosomeAnalysisTestOrder()
        {
        }

		public ChromosomeAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_ASR = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has not been approved by the FDA. The FDA has determined such clearance or approval is not necessary. This laboratory is CLIA certified to perform high complexity clinical testing.";
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Karyotype
		{
			get { return this.m_Karyotype; }
			set
			{
				if (this.m_Karyotype != value)
				{
					this.m_Karyotype = value;
					this.NotifyPropertyChanged("Karyotype");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string MetaphasesCounted
		{
			get { return this.m_MetaphasesCounted; }
			set
			{
				if (this.m_MetaphasesCounted != value)
				{
					this.m_MetaphasesCounted = value;
					this.NotifyPropertyChanged("MetaphasesCounted");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string MetaphasesAnalyzed
		{
			get { return this.m_MetaphasesAnalyzed; }
			set
			{
				if (this.m_MetaphasesAnalyzed != value)
				{
					this.m_MetaphasesAnalyzed = value;
					this.NotifyPropertyChanged("MetaphasesAnalyzed");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string MetaphasesKaryotyped
		{
			get { return this.m_MetaphasesKaryotyped; }
			set
			{
				if (this.m_MetaphasesKaryotyped != value)
				{
					this.m_MetaphasesKaryotyped = value;
					this.NotifyPropertyChanged("MetaphasesKaryotyped");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string CultureType
		{
			get { return this.m_CultureType; }
			set
			{
				if (this.m_CultureType != value)
				{
					this.m_CultureType = value;
					this.NotifyPropertyChanged("CultureType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string BandingTechnique
		{
			get { return this.m_BandingTechnique; }
			set
			{
				if (this.m_BandingTechnique != value)
				{
					this.m_BandingTechnique = value;
					this.NotifyPropertyChanged("BandingTechnique");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string BandingResolution
		{
			get { return this.m_BandingResolution; }
			set
			{
				if (this.m_BandingResolution != value)
				{
					this.m_BandingResolution = value;
					this.NotifyPropertyChanged("BandingResolution");
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

            result.AppendLine("Karyotype: " + this.m_Karyotype);
            result.AppendLine();

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            result.AppendLine("Comment: " + this.m_Comment);
            result.AppendLine();            
            
            return result.ToString().Trim();
        }
	}
}
