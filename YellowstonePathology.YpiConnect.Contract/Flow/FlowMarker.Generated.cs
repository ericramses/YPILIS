using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	[DataContract]
	public partial class FlowMarker
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in FlowMarker");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in FlowMarker");
		}
		#endregion

		#region Fields
		private string m_FlowMarkerId;
		private string m_ReportNo;
		private string m_Name;
		private double m_Percentage;
		private string m_Intensity;
		private string m_Interpretation;
		private bool m_Predictive;
		private int m_Expresses;
		#endregion

		#region Properties
		[DataMember]
		public string FlowMarkerId
		{
			get { return this.m_FlowMarkerId; }
			set
			{
				if (this.m_FlowMarkerId != value)
				{
					this.m_FlowMarkerId = value;
					this.NotifyPropertyChanged("FlowMarkerId");
					this.NotifyDBPropertyChanged("FlowMarkerId");
				}
			}
		}

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

		[DataMember]
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

		[DataMember]
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

		[DataMember]
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

		[DataMember]
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

		[DataMember]
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

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_FlowMarkerId = propertyWriter.WriteString("FlowMarkerId");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_Name = propertyWriter.WriteString("Name");
			this.m_Percentage = propertyWriter.WriteFloat("Percentage");
			this.m_Intensity = propertyWriter.WriteString("Intensity");
			this.m_Interpretation = propertyWriter.WriteString("Interpretation");
			this.m_Predictive = propertyWriter.WriteBoolean("Predictive");
			this.m_Expresses = propertyWriter.WriteInt("Expresses");
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
			propertyReader.ReadString("Interpretation", Interpretation);
			propertyReader.ReadBoolean("Predictive", Predictive);
			propertyReader.ReadInt("Expresses", Expresses);
		}
		#endregion
	}
}
