/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 9:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
	/// <summary>
	/// Description of BCellEnumerationTestOrder.
	/// </summary>
	/// 
	[PersistentClass("tblBCellEnumerationTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class BCellEnumerationTestOrder : PanelSetOrder
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
		private string m_WBC;
		private string m_LymphocytePercentage;
		private string m_CD19BCellPositiveCount;
		private string m_CD19BCellPositivePercent;
		private string m_CD20BCellPositiveCount;
		private string m_CD20BCellPositivePercent;
		private string m_CD19AbsoluteCount;
		private string m_CD20AbsoluteCount;
		
		public BCellEnumerationTestOrder()
		{
		}

		public BCellEnumerationTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
		public string WBC
		{
			get { return this.m_WBC; }
			set
			{
				if (this.m_WBC != value)
				{
					this.m_WBC = value;
					this.NotifyPropertyChanged("WBC");
					this.SetCD19AbsoluteCount();
					this.SetCD20AbsoluteCount();
				}
			}
		}

		[PersistentProperty()]
		public string LymphocytePercentage
		{
			get { return this.m_LymphocytePercentage; }
			set
			{
				if (this.m_LymphocytePercentage != value)
				{
					this.m_LymphocytePercentage = value;
					this.NotifyPropertyChanged("LymphocytePercentage");
					this.SetCD19AbsoluteCount();
					this.SetCD20AbsoluteCount();
				}
			}
		}

		[PersistentProperty()]
		public string CD19BCellPositiveCount
		{
			get { return this.m_CD19BCellPositiveCount; }
			set
			{
				if (this.m_CD19BCellPositiveCount != value)
				{
					this.m_CD19BCellPositiveCount = value;
					this.NotifyPropertyChanged("CD19BCellPositiveCount");
				}
			}
		}

		[PersistentProperty()]
		public string CD19BCellPositivePercent
		{
			get { return this.m_CD19BCellPositivePercent; }
			set
			{
				if (this.m_CD19BCellPositivePercent != value)
				{
					this.m_CD19BCellPositivePercent = value;
					this.NotifyPropertyChanged("CD19BCellPositivePercent");
					this.SetCD19AbsoluteCount();
				}
			}
		}

		[PersistentProperty()]
		public string CD20BCellPositiveCount
		{
			get { return this.m_CD20BCellPositiveCount; }
			set
			{
				if (this.m_CD20BCellPositiveCount != value)
				{
					this.m_CD20BCellPositiveCount = value;
					this.NotifyPropertyChanged("CD20BCellPositiveCount");
				}
			}
		}

		[PersistentProperty()]
		public string CD20BCellPositivePercent
		{
			get { return this.m_CD20BCellPositivePercent; }
			set
			{
				if (this.m_CD20BCellPositivePercent != value)
				{
					this.m_CD20BCellPositivePercent = value;
					this.NotifyPropertyChanged("CD20BCellPositivePercent");
					this.SetCD20AbsoluteCount();
				}
			}
		}
		
		[PersistentProperty()]
		public string CD19AbsoluteCount
		{
			get { return this.m_CD19AbsoluteCount; }
			set
			{
				if (this.m_CD19AbsoluteCount != value)
				{
					this.m_CD19AbsoluteCount = value;
					this.NotifyPropertyChanged("CD19AbsoluteCount");
				}
			}
		}
		
		[PersistentProperty()]
		public string CD20AbsoluteCount
		{
			get { return this.m_CD20AbsoluteCount; }
			set
			{
				if (this.m_CD20AbsoluteCount != value)
				{
					this.m_CD20AbsoluteCount = value;
					this.NotifyPropertyChanged("CD20AbsoluteCount");
				}
			}
		}
		
		private void SetCD19AbsoluteCount()
		{
			string result = string.Empty;
			double wbc;
			double lymphocytePercentage;
			double cd19BCellPositivePercent;
			bool wbcHasValue = double.TryParse(this.m_WBC, out wbc);
			bool lymphocytePercentageHasValue = double.TryParse(this.m_LymphocytePercentage, out lymphocytePercentage);
			bool cd19BCellPositivePercentHasValue = double.TryParse(this.m_CD19BCellPositivePercent, out cd19BCellPositivePercent);
			if(wbcHasValue && lymphocytePercentageHasValue && cd19BCellPositivePercentHasValue)
			{
				int lpDenominator = this.m_LymphocytePercentage.Length == 1 ? 10 : 100;
				int cdDenominator = this.m_CD19BCellPositivePercent.Length == 1 ? 10 : 100;
				double resultValue = wbc * lymphocytePercentage * cd19BCellPositivePercent / lpDenominator / cdDenominator;
				result = Math.Round(resultValue, 2).ToString();
			}
			this.CD19AbsoluteCount = result;
		}
		
		public void SetCD20AbsoluteCount()
		{
			string result = string.Empty;
			double wbc;
			double lymphocytePercentage;
			double cd20BCellPositivePercent;
			bool wbcHasValue = double.TryParse(this.m_WBC, out wbc);
			bool lymphocytePercentageHasValue = double.TryParse(this.m_LymphocytePercentage, out lymphocytePercentage);
			bool cd20BCellPositivePercentHasValue = double.TryParse(this.m_CD20BCellPositivePercent, out cd20BCellPositivePercent);
			if(wbcHasValue && lymphocytePercentageHasValue && cd20BCellPositivePercentHasValue)
			{
				int lpDenominator = this.m_LymphocytePercentage.Length == 1 ? 10 : 100;
				int cdDenominator = this.m_CD20BCellPositivePercent.Length == 1 ? 10 : 100;
				double resultValue = wbc * lymphocytePercentage * cd20BCellPositivePercent / lpDenominator / cdDenominator;
				result = Math.Round(resultValue, 2).ToString();
			}
			this.CD20AbsoluteCount = result;
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
