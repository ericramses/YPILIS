/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 10:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.TCellSubsetAnalysis
{
	/// <summary>
	/// Description of TCellSubsetAnalysisTestOrder.
	/// </summary>
	[PersistentClass("tblTCellSubsetAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class TCellSubsetAnalysisTestOrder : PanelSetOrder
	{
		public static string s_ASR = "Tests utilizing Analytic Specific Reagents (ASR’s) were developed and performance characteristics determined by " +
			"Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has " +
			"determined that such clearance or approval is not necessary.  ASR’s may be used for clinical purposes and should not be regarded as " +
			"investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) " +
			"as qualified to perform high complexity clinical laboratory testing.";
		public static string s_Method = "Not Set Yet";
		public static string s_References = "Not Set Yet";
		
		private string m_Result;
		private string m_Interpretation;
		private string m_Method;
		private string m_References;
		private string m_ASRComment;
		private string m_CD3Percent;
		private string m_CD4Percent;
		private string m_CD8Percent;
		private string m_CD4CD8Ratio;
		
		public TCellSubsetAnalysisTestOrder()
		{
		}

		public TCellSubsetAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute, systemIdentity)
		{
			this.m_ASRComment = s_ASR;
			this.m_Method = s_Method;
			this.m_References = s_References;
		}

		[PersistentProperty()]
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
		public string References
		{
			get { return this.m_References; }
			set
			{
				if (this.m_References != value)
				{
					this.m_References = value;
					this.NotifyPropertyChanged("References");
				}
			}
		}

		[PersistentProperty()]
		public string ASRComment
		{
			get { return this.m_ASRComment; }
			set
			{
				if (this.m_ASRComment != value)
				{
					this.m_ASRComment = value;
					this.NotifyPropertyChanged("ASRComment");
				}
			}
		}

		[PersistentProperty()]
		public string CD3Percent
		{
			get { return this.m_CD3Percent; }
			set
			{
				if (this.m_CD3Percent != value)
				{
					this.m_CD3Percent = value;
					this.NotifyPropertyChanged("CD3Percent");
				}
			}
		}

		[PersistentProperty()]
		public string CD4Percent
		{
			get { return this.m_CD4Percent; }
			set
			{
				if (this.m_CD4Percent != value)
				{
					this.m_CD4Percent = value;
					this.NotifyPropertyChanged("CD4Percent");
					this.SetCD4CD8Ratio();
				}
			}
		}

		[PersistentProperty()]
		public string CD8Percent
		{
			get { return this.m_CD8Percent; }
			set
			{
				if (this.m_CD8Percent != value)
				{
					this.m_CD8Percent = value;
					this.NotifyPropertyChanged("CD8Percent");
					this.SetCD4CD8Ratio();
				}
			}
		}

		[PersistentProperty()]
		public string CD4CD8Ratio
		{
			get { return this.m_CD4CD8Ratio; }
			set
			{
				if (this.m_CD4CD8Ratio != value)
				{
					this.m_CD4CD8Ratio = value;
					this.NotifyPropertyChanged("CD4CD8Ratio");
				}
			}
		}
		
		private void SetCD4CD8Ratio()
		{
			string result = string.Empty;
			int cd4;
			int cd8;
			bool cd4HasValue = Int32.TryParse(this.m_CD4Percent, out cd4);
			bool cd8HasValue = Int32.TryParse(this.m_CD8Percent, out cd8);
			if(cd4HasValue && cd8HasValue)
			{
				double resultValue = cd4 / cd8;
				result = Math.Round(resultValue, 2).ToString();
			}
			this.CD4CD8Ratio = result;
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
