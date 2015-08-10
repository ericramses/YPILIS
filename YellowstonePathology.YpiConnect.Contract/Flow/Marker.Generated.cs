using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	[DataContract]
	public partial class Marker
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in Marker");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in Marker");
		}
		#endregion

		#region Fields
		private int m_MarkerId;
		private string m_MarkerName;
		private string m_CPTCode;
		private int m_QTYBill;
		private int m_OrderFlag;
		private string m_Type;
		private bool m_Predictive;
		private bool m_IsNormalMarker;
		private bool m_IsMyelodysplastic;
		#endregion

		#region Properties
		[DataMember]
		public int MarkerId
		{
			get { return this.m_MarkerId; }
			set
			{
				if(this.m_MarkerId != value)
				{
					this.m_MarkerId = value;
					this.NotifyPropertyChanged("MarkerId");
					this.NotifyDBPropertyChanged("MarkerId");
				}
			}
		}

		[DataMember]
		public string MarkerName
		{
			get { return this.m_MarkerName; }
			set
			{
				if(this.m_MarkerName != value)
				{
					this.m_MarkerName = value;
					this.NotifyPropertyChanged("MarkerName");
					this.NotifyDBPropertyChanged("MarkerName");
				}
			}
		}

		[DataMember]
		public string CPTCode
		{
			get { return this.m_CPTCode; }
			set
			{
				if(this.m_CPTCode != value)
				{
					this.m_CPTCode = value;
					this.NotifyPropertyChanged("CPTCode");
					this.NotifyDBPropertyChanged("CPTCode");
				}
			}
		}

		[DataMember]
		public int QTYBill
		{
			get { return this.m_QTYBill; }
			set
			{
				if(this.m_QTYBill != value)
				{
					this.m_QTYBill = value;
					this.NotifyPropertyChanged("QTYBill");
					this.NotifyDBPropertyChanged("QTYBill");
				}
			}
		}

		[DataMember]
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

		[DataMember]
		public string Type
		{
			get { return this.m_Type; }
			set
			{
				if(this.m_Type != value)
				{
					this.m_Type = value;
					this.NotifyPropertyChanged("Type");
					this.NotifyDBPropertyChanged("Type");
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
		public bool IsNormalMarker
		{
			get { return this.m_IsNormalMarker; }
			set
			{
				if(this.m_IsNormalMarker != value)
				{
					this.m_IsNormalMarker = value;
					this.NotifyPropertyChanged("IsNormalMarker");
					this.NotifyDBPropertyChanged("IsNormalMarker");
				}
			}
		}

		[DataMember]
		public bool IsMyelodysplastic
		{
			get { return this.m_IsMyelodysplastic; }
			set
			{
				if(this.m_IsMyelodysplastic != value)
				{
					this.m_IsMyelodysplastic = value;
					this.NotifyPropertyChanged("IsMyelodysplastic");
					this.NotifyDBPropertyChanged("IsMyelodysplastic");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_MarkerId = propertyWriter.WriteInt("MarkerId");
			this.m_MarkerName = propertyWriter.WriteString("MarkerName");
			this.m_CPTCode = propertyWriter.WriteString("CPTCode");
			this.m_QTYBill = propertyWriter.WriteInt("QTYBill");
			this.m_OrderFlag = propertyWriter.WriteInt("OrderFlag");
			this.m_Type = propertyWriter.WriteString("Type");
			this.m_Predictive = propertyWriter.WriteBoolean("Predictive");
			this.m_IsNormalMarker = propertyWriter.WriteBoolean("IsNormalMarker");
			this.m_IsMyelodysplastic = propertyWriter.WriteBoolean("IsMyelodysplastic");
		}
		#endregion

		#region ReadPropertiesMethod
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadInt("MarkerId", MarkerId);
			propertyReader.ReadString("MarkerName", MarkerName);
			propertyReader.ReadString("CPTCode", CPTCode);
			propertyReader.ReadInt("QTYBill", QTYBill);
			propertyReader.ReadInt("OrderFlag", OrderFlag);
			propertyReader.ReadString("Type", Type);
			propertyReader.ReadBoolean("Predictive", Predictive);
			propertyReader.ReadBoolean("IsNormalMarker", IsNormalMarker);
			propertyReader.ReadBoolean("IsMyelodysplastic", IsMyelodysplastic);
		}
		#endregion
	}
}
