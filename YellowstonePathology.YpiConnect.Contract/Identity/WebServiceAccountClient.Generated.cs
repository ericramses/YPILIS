using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{
	public partial class WebServiceAccountClient
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			if (xml.Element("WebServiceAccountClientId") != null) m_WebServiceAccountClientId = Convert.ToInt32(xml.Element("WebServiceAccountClientId").Value);
			if (xml.Element("WebServiceAccountId") != null) m_WebServiceAccountId = Convert.ToInt32(xml.Element("WebServiceAccountId").Value);
			if (xml.Element("ClientId") != null) m_ClientId = Convert.ToInt32(xml.Element("ClientId").Value);
		}

		public XElement ToXml()
		{
			XElement result = new XElement("WebServiceAccountClient");
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "WebServiceAccountClientId", WebServiceAccountClientId);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "WebServiceAccountId", WebServiceAccountId);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "ClientId", ClientId);
			return result;
		}
		#endregion

		#region Fields
		private int m_WebServiceAccountClientId;
		private int m_WebServiceAccountId;
		private int m_ClientId;
		#endregion

		#region Properties
		[DataMember]
		public int WebServiceAccountClientId
		{
			get { return this.m_WebServiceAccountClientId; }
			set
			{
				if(this.m_WebServiceAccountClientId != value)
				{
					this.m_WebServiceAccountClientId = value;
					this.NotifyPropertyChanged("WebServiceAccountClientId");
					this.NotifyDBPropertyChanged("WebServiceAccountClientId");
				}
			}
		}

		[DataMember]
		public int WebServiceAccountId
		{
			get { return this.m_WebServiceAccountId; }
			set
			{
				if(this.m_WebServiceAccountId != value)
				{
					this.m_WebServiceAccountId = value;
					this.NotifyPropertyChanged("WebServiceAccountId");
					this.NotifyDBPropertyChanged("WebServiceAccountId");
				}
			}
		}

		[DataMember]
		public int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if(this.m_ClientId != value)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
					this.NotifyDBPropertyChanged("ClientId");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_WebServiceAccountClientId = propertyWriter.WriteInt("WebServiceAccountClientId");
			this.m_WebServiceAccountId = propertyWriter.WriteInt("WebServiceAccountId");
			this.m_ClientId = propertyWriter.WriteInt("ClientId");
		}
		#endregion

		#region ReadPropertiesMethod
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadInt("WebServiceAccountClientId", WebServiceAccountClientId);
			propertyReader.ReadInt("WebServiceAccountId", WebServiceAccountId);
			propertyReader.ReadInt("ClientId", ClientId);
		}
		#endregion
	}
}
