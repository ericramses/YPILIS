using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	[DataContract]
	public partial class FlowLeukemia
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in FlowLeukemia");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in FlowLeukemia");
		}
		#endregion

		#region Fields
		private string m_ReportNo;
		private string m_GatingPopulationV2;
		private int m_LymphocyteCount;
		private int m_MonocyteCount;
		private int m_MyeloidCount;
		private int m_DimCD45ModSSCount;
		private int m_OtherCount;
		private string m_OtherName;
		private string m_InterpretiveComment;
		private string m_Impression;
		private string m_CellPopulationOfInterest;
		private string m_TestResult;
		private string m_CellDescription;
		private string m_BTCellSelection;
		private string m_EGateCD34Percent;
		private string m_EGateCD117Percent;
		#endregion

		#region Properties
		[DataMember]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if(this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
					this.NotifyDBPropertyChanged("ReportNo");
				}
			}
		}

		[DataMember]
		public string GatingPopulationV2
		{
			get { return this.m_GatingPopulationV2; }
			set
			{
				if(this.m_GatingPopulationV2 != value)
				{
					this.m_GatingPopulationV2 = value;
					this.NotifyPropertyChanged("GatingPopulationV2");
					this.NotifyDBPropertyChanged("GatingPopulationV2");
				}
			}
		}

		[DataMember]
		public int LymphocyteCount
		{
			get { return this.m_LymphocyteCount; }
			set
			{
				if(this.m_LymphocyteCount != value)
				{
					this.m_LymphocyteCount = value;
					this.NotifyPropertyChanged("LymphocyteCount");
					this.NotifyDBPropertyChanged("LymphocyteCount");
				}
			}
		}

		[DataMember]
		public int MonocyteCount
		{
			get { return this.m_MonocyteCount; }
			set
			{
				if(this.m_MonocyteCount != value)
				{
					this.m_MonocyteCount = value;
					this.NotifyPropertyChanged("MonocyteCount");
					this.NotifyDBPropertyChanged("MonocyteCount");
				}
			}
		}

		[DataMember]
		public int MyeloidCount
		{
			get { return this.m_MyeloidCount; }
			set
			{
				if(this.m_MyeloidCount != value)
				{
					this.m_MyeloidCount = value;
					this.NotifyPropertyChanged("MyeloidCount");
					this.NotifyDBPropertyChanged("MyeloidCount");
				}
			}
		}

		[DataMember]
		public int DimCD45ModSSCount
		{
			get { return this.m_DimCD45ModSSCount; }
			set
			{
				if(this.m_DimCD45ModSSCount != value)
				{
					this.m_DimCD45ModSSCount = value;
					this.NotifyPropertyChanged("DimCD45ModSSCount");
					this.NotifyDBPropertyChanged("DimCD45ModSSCount");
				}
			}
		}

		[DataMember]
		public int OtherCount
		{
			get { return this.m_OtherCount; }
			set
			{
				if(this.m_OtherCount != value)
				{
					this.m_OtherCount = value;
					this.NotifyPropertyChanged("OtherCount");
					this.NotifyDBPropertyChanged("OtherCount");
				}
			}
		}

		[DataMember]
		public string OtherName
		{
			get { return this.m_OtherName; }
			set
			{
				if(this.m_OtherName != value)
				{
					this.m_OtherName = value;
					this.NotifyPropertyChanged("OtherName");
					this.NotifyDBPropertyChanged("OtherName");
				}
			}
		}

		[DataMember]
		public string InterpretiveComment
		{
			get { return this.m_InterpretiveComment; }
			set
			{
				if(this.m_InterpretiveComment != value)
				{
					this.m_InterpretiveComment = value;
					this.NotifyPropertyChanged("InterpretiveComment");
					this.NotifyDBPropertyChanged("InterpretiveComment");
				}
			}
		}

		[DataMember]
		public string Impression
		{
			get { return this.m_Impression; }
			set
			{
				if(this.m_Impression != value)
				{
					this.m_Impression = value;
					this.NotifyPropertyChanged("Impression");
					this.NotifyDBPropertyChanged("Impression");
				}
			}
		}

		[DataMember]
		public string CellPopulationOfInterest
		{
			get { return this.m_CellPopulationOfInterest; }
			set
			{
				if(this.m_CellPopulationOfInterest != value)
				{
					this.m_CellPopulationOfInterest = value;
					this.NotifyPropertyChanged("CellPopulationOfInterest");
					this.NotifyDBPropertyChanged("CellPopulationOfInterest");
				}
			}
		}

		[DataMember]
		public string TestResult
		{
			get { return this.m_TestResult; }
			set
			{
				if(this.m_TestResult != value)
				{
					this.m_TestResult = value;
					this.NotifyPropertyChanged("TestResult");
					this.NotifyDBPropertyChanged("TestResult");
				}
			}
		}

		[DataMember]
		public string CellDescription
		{
			get { return this.m_CellDescription; }
			set
			{
				if(this.m_CellDescription != value)
				{
					this.m_CellDescription = value;
					this.NotifyPropertyChanged("CellDescription");
					this.NotifyDBPropertyChanged("CellDescription");
				}
			}
		}

		[DataMember]
		public string BTCellSelection
		{
			get { return this.m_BTCellSelection; }
			set
			{
				if(this.m_BTCellSelection != value)
				{
					this.m_BTCellSelection = value;
					this.NotifyPropertyChanged("BTCellSelection");
					this.NotifyDBPropertyChanged("BTCellSelection");
				}
			}
		}

		[DataMember]
		public string EGateCD34Percent
		{
			get { return this.m_EGateCD34Percent; }
			set
			{
				if(this.m_EGateCD34Percent != value)
				{
					this.m_EGateCD34Percent = value;
					this.NotifyPropertyChanged("EGateCD34Percent");
					this.NotifyDBPropertyChanged("EGateCD34Percent");
				}
			}
		}

		[DataMember]
		public string EGateCD117Percent
		{
			get { return this.m_EGateCD117Percent; }
			set
			{
				if(this.m_EGateCD117Percent != value)
				{
					this.m_EGateCD117Percent = value;
					this.NotifyPropertyChanged("EGateCD117Percent");
					this.NotifyDBPropertyChanged("EGateCD117Percent");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_GatingPopulationV2 = propertyWriter.WriteString("GatingPopulationV2");
			this.m_LymphocyteCount = propertyWriter.WriteInt("LymphocyteCount");
			this.m_MonocyteCount = propertyWriter.WriteInt("MonocyteCount");
			this.m_MyeloidCount = propertyWriter.WriteInt("MyeloidCount");
			this.m_DimCD45ModSSCount = propertyWriter.WriteInt("DimCD45ModSSCount");
			this.m_OtherCount = propertyWriter.WriteInt("OtherCount");
			this.m_OtherName = propertyWriter.WriteString("OtherName");
			this.m_InterpretiveComment = propertyWriter.WriteString("InterpretiveComment");
			this.m_Impression = propertyWriter.WriteString("Impression");
			this.m_CellPopulationOfInterest = propertyWriter.WriteString("CellPopulationOfInterest");
			this.m_TestResult = propertyWriter.WriteString("TestResult");
			this.m_CellDescription = propertyWriter.WriteString("CellDescription");
			this.m_BTCellSelection = propertyWriter.WriteString("BTCellSelection");
			this.m_EGateCD34Percent = propertyWriter.WriteString("EGateCD34Percent");
			this.m_EGateCD117Percent = propertyWriter.WriteString("EGateCD117Percent");
		}
		#endregion

		#region ReadPropertiesMethod
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadString("ReportNo", ReportNo);
			propertyReader.ReadString("GatingPopulationV2", GatingPopulationV2);
			propertyReader.ReadInt("LymphocyteCount", LymphocyteCount);
			propertyReader.ReadInt("MonocyteCount", MonocyteCount);
			propertyReader.ReadInt("MyeloidCount", MyeloidCount);
			propertyReader.ReadInt("DimCD45ModSSCount", DimCD45ModSSCount);
			propertyReader.ReadInt("OtherCount", OtherCount);
			propertyReader.ReadString("OtherName", OtherName);
			propertyReader.ReadString("InterpretiveComment", InterpretiveComment);
			propertyReader.ReadString("Impression", Impression);
			propertyReader.ReadString("CellPopulationOfInterest", CellPopulationOfInterest);
			propertyReader.ReadString("TestResult", TestResult);
			propertyReader.ReadString("CellDescription", CellDescription);
			propertyReader.ReadString("BTCellSelection", BTCellSelection);
			propertyReader.ReadString("EGateCD34Percent", EGateCD34Percent);
			propertyReader.ReadString("EGateCD117Percent", EGateCD117Percent);
		}
		#endregion
	}
}
