using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile
{
	[PersistentClass("tblNeoARRAYSNPCytogeneticProfileTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class NeoARRAYSNPCytogeneticProfileTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_CopyNumberVariant;
		private string m_UniparentalDisomy;
		private string m_MicroarrayResults;
		private string m_Interpretation;
		private string m_Method;
		private string m_TestDevelopment;        
		
		public NeoARRAYSNPCytogeneticProfileTestOrder()
        {

        }

		public NeoARRAYSNPCytogeneticProfileTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentStringProperty(500)]
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

		[PersistentStringProperty(50)]
		public string CopyNumberVariant
		{
			get { return this.m_CopyNumberVariant; }
			set
			{
				if (this.m_CopyNumberVariant != value)
				{
					this.m_CopyNumberVariant = value;
					this.NotifyPropertyChanged("CopyNumberVariant");
				}
			}
		}

		[PersistentStringProperty(50)]
		public string UniparentalDisomy
		{
			get { return this.m_UniparentalDisomy; }
			set
			{
				if (this.m_UniparentalDisomy != value)
				{
					this.m_UniparentalDisomy = value;
					this.NotifyPropertyChanged("UniparentalDisomy");
				}
			}
		}

		[PersistentStringProperty(500)]
		public string MicroarrayResults
		{
			get { return this.m_MicroarrayResults; }
			set
			{
				if (this.m_MicroarrayResults != value)
				{
					this.m_MicroarrayResults = value;
					this.NotifyPropertyChanged("MicroarrayResults");
				}
			}
		}

		[PersistentStringProperty(5000)]
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

		[PersistentStringProperty(5000)]
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

		[PersistentStringProperty(500)]
		public string TestDevelopment
		{
			get { return this.m_TestDevelopment; }
			set
			{
				if (this.m_TestDevelopment != value)
				{
					this.m_TestDevelopment = value;
					this.NotifyPropertyChanged("TestDevelopment");
				}
			}
		}        

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Copy Number Variant: " + this.m_CopyNumberVariant);
			result.AppendLine("Uniparental Disomy: " + this.m_UniparentalDisomy);
			result.AppendLine("Microarray Results: " + this.m_MicroarrayResults);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}
	}
}
