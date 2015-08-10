using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Xml.Linq;
using YellowstonePathology.Persistence;

namespace YellowstonePathology.Business.Flow
{
	[PersistentClass("tblFlowMarkers", "YPIDATA")]
	public partial class FlowMarkerItem
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in FlowMarkerItem");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in FlowMarkerItem");
		}
		#endregion

		#region Fields
		private string m_FlowMarkerId;
		private string m_ReportNo;
		private string m_Name;
		private double m_Percentage;
		private string m_Intensity;
		private bool m_Report;
		private bool m_MarkerUsed;
		private string m_MarkerType;
		private string m_Interpretation;
		private bool m_Predictive;
		private int m_Expresses;
		private int m_OrderFlag;
		private string m_Result;
		#endregion

		#region Properties
        [PersistentPrimaryKeyProperty(false)]
		public string FlowMarkerId
		{
			get { return this.m_FlowMarkerId; }
			set
			{
				if(this.m_FlowMarkerId != value)
				{
					this.m_FlowMarkerId = value;
					this.NotifyPropertyChanged("FlowMarkerId");
					this.NotifyDBPropertyChanged("FlowMarkerId");
				}
			}
		}

        [PersistentProperty()]
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

        [PersistentProperty()]
		public string Name
		{
			get { return this.m_Name; }
			set
			{
				if(this.m_Name != value)
				{
					this.m_Name = value;
					this.NotifyPropertyChanged("Name");
					this.NotifyDBPropertyChanged("Name");
				}
			}
		}

        [PersistentProperty()]
		public double Percentage
		{
			get { return this.m_Percentage; }
			set
			{
				if(this.m_Percentage != value)
				{
					this.m_Percentage = value;
					this.NotifyPropertyChanged("Percentage");
					this.NotifyDBPropertyChanged("Percentage");
				}
			}
		}

        [PersistentProperty()]
		public string Intensity
		{
			get { return this.m_Intensity; }
			set
			{
				if(this.m_Intensity != value)
				{
					this.m_Intensity = value;
					this.NotifyPropertyChanged("Intensity");
					this.NotifyDBPropertyChanged("Intensity");
				}
			}
		}

        [PersistentProperty()]
		public bool Report
		{
			get { return this.m_Report; }
			set
			{
				if(this.m_Report != value)
				{
					this.m_Report = value;
					this.NotifyPropertyChanged("Report");
					this.NotifyDBPropertyChanged("Report");
				}
			}
		}

        [PersistentProperty()]
		public bool MarkerUsed
		{
			get { return this.m_MarkerUsed; }
			set
			{
				if(this.m_MarkerUsed != value)
				{
					this.m_MarkerUsed = value;
					this.NotifyPropertyChanged("MarkerUsed");
					this.NotifyDBPropertyChanged("MarkerUsed");
				}
			}
		}

        [PersistentProperty()]
		public string MarkerType
		{
			get { return this.m_MarkerType; }
			set
			{
				if(this.m_MarkerType != value)
				{
					this.m_MarkerType = value;
					this.NotifyPropertyChanged("MarkerType");
					this.NotifyDBPropertyChanged("MarkerType");
				}
			}
		}

        [PersistentProperty()]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				if(this.m_Interpretation != value)
				{
					this.m_Interpretation = value;
					this.NotifyPropertyChanged("Interpretation");
					this.NotifyDBPropertyChanged("Interpretation");
				}
			}
		}

        [PersistentProperty()]
		public bool Predictive
		{
			get { return this.m_Predictive; }
			set
			{
				if(this.m_Predictive != value)
				{
					this.m_Predictive = value;
					this.NotifyPropertyChanged("Predictive");
					this.NotifyDBPropertyChanged("Predictive");
				}
			}
		}

        [PersistentProperty()]
		public int Expresses
		{
			get { return this.m_Expresses; }
			set
			{
				if(this.m_Expresses != value)
				{
					this.m_Expresses = value;
					this.NotifyPropertyChanged("Expresses");
					this.NotifyDBPropertyChanged("Expresses");
				}
			}
		}

        [PersistentProperty()]
		public int OrderFlag
		{
			get { return this.m_OrderFlag; }
			set
			{
				if(this.m_OrderFlag != value)
				{
					this.m_OrderFlag = value;
					this.NotifyPropertyChanged("OrderFlag");
					this.NotifyDBPropertyChanged("OrderFlag");
				}
			}
		}

        [PersistentProperty()]
		public string Result
		{
			get { return this.m_Result; }
			set
			{
				if(this.m_Result != value)
				{
					this.m_Result = value;
					this.NotifyPropertyChanged("Result");
					this.NotifyDBPropertyChanged("Result");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_FlowMarkerId = propertyWriter.WriteString("FlowMarkerId");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_Name = propertyWriter.WriteString("Name");
			this.m_Percentage = propertyWriter.WriteFloat("Percentage");
			this.m_Intensity = propertyWriter.WriteString("Intensity");
			this.m_Report = propertyWriter.WriteBoolean("Report");
			this.m_MarkerUsed = propertyWriter.WriteBoolean("MarkerUsed");
			this.m_MarkerType = propertyWriter.WriteString("MarkerType");
			this.m_Interpretation = propertyWriter.WriteString("Interpretation");
			this.m_Predictive = propertyWriter.WriteBoolean("Predictive");
			this.m_Expresses = propertyWriter.WriteInt("Expresses");
			this.m_OrderFlag = propertyWriter.WriteInt("OrderFlag");
			this.m_Result = propertyWriter.WriteString("Result");
		}
		#endregion

		#region ReadPropertiesMethod
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadString("FlowMarkerId", FlowMarkerId);
			propertyReader.ReadString("ReportNo", ReportNo);
			propertyReader.ReadString("Name", Name);
			propertyReader.ReadDouble("Percentage", Percentage);
			propertyReader.ReadString("Intensity", Intensity);
			propertyReader.ReadBoolean("Report", Report);
			propertyReader.ReadBoolean("MarkerUsed", MarkerUsed);
			propertyReader.ReadString("MarkerType", MarkerType);
			propertyReader.ReadString("Interpretation", Interpretation);
			propertyReader.ReadBoolean("Predictive", Predictive);
			propertyReader.ReadInt("Expresses", Expresses);
			propertyReader.ReadInt("OrderFlag", OrderFlag);
			propertyReader.ReadString("Result", Result);
		}
		#endregion
	}
}
