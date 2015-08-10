using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Domain
{
	[DataContract]
	public class PanelSetOrderLeukemiaLymphoma : PanelSetOrder, YellowstonePathology.Shared.Interface.ILeukemiaLymphomaResult
	{
		private Flow.FlowMarkerCollection m_FlowMarkerCollection;

		public PanelSetOrderLeukemiaLymphoma()
		{
			this.m_FlowMarkerCollection = new Flow.FlowMarkerCollection();
		}		

		public Flow.FlowMarkerCollection FlowMarkerCollection
		{
			get { return this.m_FlowMarkerCollection; }
			set
			{
				this.m_FlowMarkerCollection = value;
				NotifyPropertyChanged("FlowMarkerCollection");
			}
		}

		public void RefreshGatingPercent()
		{
			this.NotifyPropertyChanged("LymphocyteCountPercent");
			this.NotifyPropertyChanged("MonocyteCountPercent");
			this.NotifyPropertyChanged("MyeloidCountPercent");
			this.NotifyPropertyChanged("DimCD45ModSSCountPercent");
		}

		public double GatingCountTotal
		{
			get
			{
				return this.m_LymphocyteCount + this.m_MonocyteCount + this.m_MyeloidCount + this.m_DimCD45ModSSCount + this.m_OtherCount;
			}
		}

		public double LymphocyteCountPercent
		{
			get
			{
				double percent = this.m_LymphocyteCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}

		public double MonocyteCountPercent
		{
			get
			{
				double percent = this.m_MonocyteCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}

		public double MyeloidCountPercent
		{
			get
			{
				double percent = this.m_MyeloidCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}

		public double DimCD45ModSSCountPercent
		{
			get
			{
				double percent = this.m_DimCD45ModSSCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}


		public double OtherCountPercent
		{
			get
			{
				double percent = this.m_OtherCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}

		#region Fields
		private string m_ReportNo;
		private int m_GatingPopulation;
		private string m_GatingPopulationV2;
		private int m_LymphocyteCount;
		private double m_GPLymphocytePrc;
		private int m_MonocyteCount;
		private int m_MyeloidCount;
		private int m_DimCD45ModSSCount;
		private int m_OtherCount;
		private string m_OtherName;
		private string m_LightScatter;
		private int? m_LightScatterV2;
		private double m_BCellPercent;
		private double m_TCellPercent;
		private double m_NKCellPercent;
		private double m_MyeloidCellPercent;
		private string m_InterpretiveComment;
		private string m_Impression;
		private int m_SpecimenViability;
		private double m_SpecimenViabilityPercent;
		private string m_CellPopulationOfInterest;
		private string m_TestResult;
		private string m_CellDescription;
		private string m_BTCellSelection;
		private string m_KappaLambda;
		private string m_EGateCD34Percent;
		private string m_EGateCD117Percent;
		private bool m_TechFinal;
		private Nullable<DateTime> m_TechFinalDate;
		private Nullable<DateTime> m_TechFinalTime;
		private int m_TechFinaledById;
		private int m_TestId;
		private string m_ReportComment;
		private string m_TestCancelledComment;
		private bool m_TestCancelled;
		private bool m_NoCharge;
		#endregion

		#region Properties		
		[DataMember]
		public int GatingPopulation
		{
			get { return this.m_GatingPopulation; }
			set
			{
				if (this.m_GatingPopulation != value)
				{
					this.m_GatingPopulation = value;
					this.NotifyPropertyChanged("GatingPopulation");
				}
			}
		}

		[DataMember]
		public string GatingPopulationV2
		{
			get { return this.m_GatingPopulationV2; }
			set
			{
				if (this.m_GatingPopulationV2 != value)
				{
					this.m_GatingPopulationV2 = value;
					this.NotifyPropertyChanged("GatingPopulationV2");
				}
			}
		}

		[DataMember]
		public int LymphocyteCount
		{
			get { return this.m_LymphocyteCount; }
			set
			{
				if (this.m_LymphocyteCount != value)
				{
					this.m_LymphocyteCount = value;
					this.NotifyPropertyChanged("LymphocyteCount");
					this.RefreshGatingPercent();
				}
			}
		}


		[DataMember]
		public double GPLymphocytePrc
		{
			get { return this.m_GPLymphocytePrc; }
			set
			{
				if (this.m_GPLymphocytePrc != value)
				{
					this.m_GPLymphocytePrc = value;
					this.NotifyPropertyChanged("GPLymphocytePrc");
				}
			}
		}

		[DataMember]
		public int MonocyteCount
		{
			get { return this.m_MonocyteCount; }
			set
			{
				if (this.m_MonocyteCount != value)
				{
					this.m_MonocyteCount = value;
					this.NotifyPropertyChanged("MonocyteCount");
					this.RefreshGatingPercent();
				}
			}
		}

		[DataMember]
		public int MyeloidCount
		{
			get { return this.m_MyeloidCount; }
			set
			{
				if (this.m_MyeloidCount != value)
				{
					this.m_MyeloidCount = value;
					this.NotifyPropertyChanged("MyeloidCount");
					this.RefreshGatingPercent();
				}
			}
		}

		[DataMember]
		public int DimCD45ModSSCount
		{
			get { return this.m_DimCD45ModSSCount; }
			set
			{
				if (this.m_DimCD45ModSSCount != value)
				{
					this.m_DimCD45ModSSCount = value;
					this.NotifyPropertyChanged("DimCD45ModSSCount");
					this.RefreshGatingPercent();
				}
			}
		}

		[DataMember]
		public int OtherCount
		{
			get { return this.m_OtherCount; }
			set
			{
				if (this.m_OtherCount != value)
				{
					this.m_OtherCount = value;
					this.NotifyPropertyChanged("OtherCount");
					this.RefreshGatingPercent();
				}
			}
		}

		[DataMember]
		public string OtherName
		{
			get { return this.m_OtherName; }
			set
			{
				if (this.m_OtherName != value)
				{
					this.m_OtherName = value;
					this.NotifyPropertyChanged("OtherName");
				}
			}
		}

		[DataMember]
		public string LightScatter
		{
			get { return this.m_LightScatter; }
			set
			{
				if (this.m_LightScatter != value)
				{
					this.m_LightScatter = value;
					this.NotifyPropertyChanged("LightScatter");
				}
			}
		}

		[DataMember]
		public int? LightScatterV2
		{
			get { return this.m_LightScatterV2; }
			set
			{
				if (this.m_LightScatterV2 != value)
				{
					this.m_LightScatterV2 = value;
					this.NotifyPropertyChanged("LightScatterV2");
				}
			}
		}

		[DataMember]
		public double BCellPercent
		{
			get { return this.m_BCellPercent; }
			set
			{
				if (this.m_BCellPercent != value)
				{
					this.m_BCellPercent = value;
					this.NotifyPropertyChanged("BCellPercent");
				}
			}
		}

		[DataMember]
		public double TCellPercent
		{
			get { return this.m_TCellPercent; }
			set
			{
				if (this.m_TCellPercent != value)
				{
					this.m_TCellPercent = value;
					this.NotifyPropertyChanged("TCellPercent");
				}
			}
		}

		[DataMember]
		public double NKCellPercent
		{
			get { return this.m_NKCellPercent; }
			set
			{
				if (this.m_NKCellPercent != value)
				{
					this.m_NKCellPercent = value;
					this.NotifyPropertyChanged("NKCellPercent");
				}
			}
		}

		[DataMember]
		public double MyeloidCellPercent
		{
			get { return this.m_MyeloidCellPercent; }
			set
			{
				if (this.m_MyeloidCellPercent != value)
				{
					this.m_MyeloidCellPercent = value;
					this.NotifyPropertyChanged("MyeloidCellPercent");
				}
			}
		}

		[DataMember]
		public string InterpretiveComment
		{
			get { return this.m_InterpretiveComment; }
			set
			{
				if (this.m_InterpretiveComment != value)
				{
					this.m_InterpretiveComment = value;
					this.NotifyPropertyChanged("InterpretiveComment");
				}
			}
		}

		[DataMember]
		public string Impression
		{
			get { return this.m_Impression; }
			set
			{
				if (this.m_Impression != value)
				{
					this.m_Impression = value;
					this.NotifyPropertyChanged("Impression");
				}
			}
		}

		[DataMember]
		public int SpecimenViability
		{
			get { return this.m_SpecimenViability; }
			set
			{
				if (this.m_SpecimenViability != value)
				{
					this.m_SpecimenViability = value;
					this.NotifyPropertyChanged("SpecimenViability");
				}
			}
		}

		[DataMember]
		public double SpecimenViabilityPercent
		{
			get { return this.m_SpecimenViabilityPercent; }
			set
			{
				if (this.m_SpecimenViabilityPercent != value)
				{
					this.m_SpecimenViabilityPercent = value;
					this.NotifyPropertyChanged("SpecimenViabilityPercent");
				}
			}
		}

		[DataMember]
		public string CellPopulationOfInterest
		{
			get { return this.m_CellPopulationOfInterest; }
			set
			{
				if (this.m_CellPopulationOfInterest != value)
				{
					this.m_CellPopulationOfInterest = value;
					this.NotifyPropertyChanged("CellPopulationOfInterest");
				}
			}
		}

		[DataMember]
		public string TestResult
		{
			get { return this.m_TestResult; }
			set
			{
				if (this.m_TestResult != value)
				{
					this.m_TestResult = value;
					this.NotifyPropertyChanged("TestResult");
				}
			}
		}

		[DataMember]
		public string CellDescription
		{
			get { return this.m_CellDescription; }
			set
			{
				if (this.m_CellDescription != value)
				{
					this.m_CellDescription = value;
					this.NotifyPropertyChanged("CellDescription");
				}
			}
		}

		[DataMember]
		public string BTCellSelection
		{
			get { return this.m_BTCellSelection; }
			set
			{
				if (this.m_BTCellSelection != value)
				{
					this.m_BTCellSelection = value;
					this.NotifyPropertyChanged("BTCellSelection");
				}
			}
		}

		[DataMember]
		public string KappaLambda
		{
			get { return this.m_KappaLambda; }
			set
			{
				if (this.m_KappaLambda != value)
				{
					this.m_KappaLambda = value;
					this.NotifyPropertyChanged("KappaLambda");
				}
			}
		}

		[DataMember]
		public string EGateCD34Percent
		{
			get { return this.m_EGateCD34Percent; }
			set
			{
				if (this.m_EGateCD34Percent != value)
				{
					this.m_EGateCD34Percent = value;
					this.NotifyPropertyChanged("EGateCD34Percent");
				}
			}
		}

		[DataMember]
		public string EGateCD117Percent
		{
			get { return this.m_EGateCD117Percent; }
			set
			{
				if (this.m_EGateCD117Percent != value)
				{
					this.m_EGateCD117Percent = value;
					this.NotifyPropertyChanged("EGateCD117Percent");
				}
			}
		}

		[DataMember]
		public bool TechFinal
		{
			get { return this.m_TechFinal; }
			set
			{
				if (this.m_TechFinal != value)
				{
					this.m_TechFinal = value;
					this.NotifyPropertyChanged("TechFinal");
				}
			}
		}

		[DataMember]
		public Nullable<DateTime> TechFinalDate
		{
			get { return this.m_TechFinalDate; }
			set
			{
				if (this.m_TechFinalDate != value)
				{
					this.m_TechFinalDate = value;
					this.NotifyPropertyChanged("TechFinalDate");
				}
			}
		}

		[DataMember]
		public Nullable<DateTime> TechFinalTime
		{
			get { return this.m_TechFinalTime; }
			set
			{
				if (this.m_TechFinalTime != value)
				{
					this.m_TechFinalTime = value;
					this.NotifyPropertyChanged("TechFinalTime");
				}
			}
		}

		[DataMember]
		public int TechFinaledById
		{
			get { return this.m_TechFinaledById; }
			set
			{
				if (this.m_TechFinaledById != value)
				{
					this.m_TechFinaledById = value;
					this.NotifyPropertyChanged("TechFinaledById");
				}
			}
		}

		[DataMember]
		public int TestId
		{
			get { return this.m_TestId; }
			set
			{
				if (this.m_TestId != value)
				{
					this.m_TestId = value;
					this.NotifyPropertyChanged("TestId");
				}
			}
		}

		[DataMember]
		public string ReportComment
		{
			get { return this.m_ReportComment; }
			set
			{
				if (this.m_ReportComment != value)
				{
					this.m_ReportComment = value;
					this.NotifyPropertyChanged("ReportComment");
				}
			}
		}

		[DataMember]
		public string TestCancelledComment
		{
			get { return this.m_TestCancelledComment; }
			set
			{
				if (this.m_TestCancelledComment != value)
				{
					this.m_TestCancelledComment = value;
					this.NotifyPropertyChanged("TestCancelledComment");
				}
			}
		}

		[DataMember]
		public bool TestCancelled
		{
			get { return this.m_TestCancelled; }
			set
			{
				if (this.m_TestCancelled != value)
				{
					this.m_TestCancelled = value;
					this.NotifyPropertyChanged("TestCancelled");
				}
			}
		}

		[DataMember]
		public bool NoCharge
		{
			get { return this.m_NoCharge; }
			set
			{
				if (this.m_NoCharge != value)
				{
					this.m_NoCharge = value;
					this.NotifyPropertyChanged("NoCharge");
				}
			}
		}
		#endregion

		#region WritePropertiesMethod
		public override void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			base.WriteProperties(propertyWriter);
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_GatingPopulation = propertyWriter.WriteInt("GatingPopulation");
			this.m_GatingPopulationV2 = propertyWriter.WriteString("GatingPopulationV2");
			this.m_LymphocyteCount = propertyWriter.WriteInt("LymphocyteCount");
			this.m_GPLymphocytePrc = propertyWriter.WriteFloat("GPLymphocytePrc");
			this.m_MonocyteCount = propertyWriter.WriteInt("MonocyteCount");
			this.m_MyeloidCount = propertyWriter.WriteInt("MyeloidCount");
			this.m_DimCD45ModSSCount = propertyWriter.WriteInt("DimCD45ModSSCount");
			this.m_OtherCount = propertyWriter.WriteInt("OtherCount");
			this.m_OtherName = propertyWriter.WriteString("OtherName");
			this.m_LightScatter = propertyWriter.WriteString("LightScatter");
			this.m_LightScatterV2 = propertyWriter.WriteNullableInt("LightScatterV2");
			this.m_BCellPercent = propertyWriter.WriteFloat("BCellPercent");
			this.m_TCellPercent = propertyWriter.WriteFloat("TCellPercent");
			this.m_NKCellPercent = propertyWriter.WriteFloat("NKCellPercent");
			this.m_MyeloidCellPercent = propertyWriter.WriteFloat("MyeloidCellPercent");
			this.m_InterpretiveComment = propertyWriter.WriteString("InterpretiveComment");
			this.m_Impression = propertyWriter.WriteString("Impression");
			this.m_SpecimenViability = propertyWriter.WriteInt("SpecimenViability");
			this.m_SpecimenViabilityPercent = propertyWriter.WriteFloat("SpecimenViabilityPercent");
			this.m_CellPopulationOfInterest = propertyWriter.WriteString("CellPopulationOfInterest");
			this.m_TestResult = propertyWriter.WriteString("TestResult");
			this.m_CellDescription = propertyWriter.WriteString("CellDescription");
			this.m_BTCellSelection = propertyWriter.WriteString("BTCellSelection");
			this.m_KappaLambda = propertyWriter.WriteString("KappaLambda");
			this.m_EGateCD34Percent = propertyWriter.WriteString("EGateCD34Percent");
			this.m_EGateCD117Percent = propertyWriter.WriteString("EGateCD117Percent");
			this.m_TechFinal = propertyWriter.WriteBoolean("TechFinal");
			this.m_TechFinalDate = propertyWriter.WriteNullableDateTime("TechFinalDate");
			this.m_TechFinalTime = propertyWriter.WriteNullableDateTime("TechFinalTime");
			this.m_TechFinaledById = propertyWriter.WriteInt("TechFinaledById");
			this.m_TestId = propertyWriter.WriteInt("TestId");
			this.m_ReportComment = propertyWriter.WriteString("ReportComment");
			this.m_TestCancelledComment = propertyWriter.WriteString("TestCancelledComment");
			this.m_TestCancelled = propertyWriter.WriteBoolean("TestCancelled");
			this.m_NoCharge = propertyWriter.WriteBoolean("NoCharge");
		}
		#endregion
	}
}
