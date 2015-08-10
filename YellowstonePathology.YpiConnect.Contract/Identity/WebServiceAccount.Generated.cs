using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{    
	public partial class WebServiceAccount
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			if (xml.Element("WebServiceAccountId") != null) m_WebServiceAccountId = Convert.ToInt32(xml.Element("WebServiceAccountId").Value);
			if (xml.Element("UserName") != null) m_UserName = xml.Element("UserName").Value;
			if (xml.Element("Password") != null) m_Password = xml.Element("Password").Value;
			if (xml.Element("DisplayName") != null) m_DisplayName = xml.Element("DisplayName").Value;
			if (xml.Element("PrimaryClientId") != null) m_PrimaryClientId = Convert.ToInt32(xml.Element("PrimaryClientId").Value);
			if (xml.Element("DownloadFileType") != null) m_DownloadFileType = xml.Element("DownloadFileType").Value;
			if (xml.Element("InitialPage") != null) m_InitialPage = xml.Element("InitialPage").Value;
		}

		public XElement ToXml()
		{
			XElement result = new XElement("ClientOrder");
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "WebServiceAccountId", WebServiceAccountId);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "UserName", UserName);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "Password", Password);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "DisplayName", DisplayName);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "PrimaryClientId", PrimaryClientId);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "DownloadFileType", DownloadFileType);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "InitialPage", InitialPage);
			return result;
		}
		#endregion

		#region Fields
		private int m_WebServiceAccountId;
		private string m_UserName;
		private string m_Password;
		private string m_DisplayName;
		private int m_PrimaryClientId;
		private string m_DownloadFileType;
		private string m_InitialPage;
		private int m_ApplicationTimeoutMinutes;
		private string m_RemoteFileDownloadDirectory;
		private string m_RemoteFileUploadDirectory;
		private string m_AlertEmailAddress;
		private bool m_SaveUserNameLocal;
		private bool m_SavePasswordLocal;
		private bool m_EnableApplicationTimeout;
		private bool m_EnableSaveSettings;
		private bool m_EnableFileUpload;
		private bool m_EnableFileDownload;
		private bool m_EnableOrderEntry;
		private bool m_EnableReportBrowser;
		private bool m_EnableBillingBrowser;
		private bool m_EnableEmailAlert;
		private int m_SystemUserId;
		private string m_Signature;
		private string m_FacilityId;
		#endregion

		#region Properties
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
		public string UserName
		{
			get { return this.m_UserName; }
			set
			{
				if(this.m_UserName != value)
				{
					this.m_UserName = value;
					this.NotifyPropertyChanged("UserName");
					this.NotifyDBPropertyChanged("UserName");
				}
			}
		}

		[DataMember]
		public string Password
		{
			get { return this.m_Password; }
			set
			{
				if(this.m_Password != value)
				{
					this.m_Password = value;
					this.NotifyPropertyChanged("Password");
					this.NotifyDBPropertyChanged("Password");
				}
			}
		}

		[DataMember]
		public string DisplayName
		{
			get { return this.m_DisplayName; }
			set
			{
				if(this.m_DisplayName != value)
				{
					this.m_DisplayName = value;
					this.NotifyPropertyChanged("DisplayName");
					this.NotifyDBPropertyChanged("DisplayName");
				}
			}
		}

		[DataMember]
		public int PrimaryClientId
		{
			get { return this.m_PrimaryClientId; }
			set
			{
				if(this.m_PrimaryClientId != value)
				{
					this.m_PrimaryClientId = value;
					this.NotifyPropertyChanged("PrimaryClientId");
					this.NotifyDBPropertyChanged("PrimaryClientId");
				}
			}
		}

		[DataMember]
		public string DownloadFileType
		{
			get { return this.m_DownloadFileType; }
			set
			{
				if(this.m_DownloadFileType != value)
				{
					this.m_DownloadFileType = value;
					this.NotifyPropertyChanged("DownloadFileType");
					this.NotifyDBPropertyChanged("DownloadFileType");
				}
			}
		}

		[DataMember]
		public string InitialPage
		{
			get { return this.m_InitialPage; }
			set
			{
				if(this.m_InitialPage != value)
				{
					this.m_InitialPage = value;
					this.NotifyPropertyChanged("InitialPage");
					this.NotifyDBPropertyChanged("InitialPage");
				}
			}
		}

		[DataMember]
		public int ApplicationTimeoutMinutes
		{
			get { return this.m_ApplicationTimeoutMinutes; }
			set
			{
				if(this.m_ApplicationTimeoutMinutes != value)
				{
					this.m_ApplicationTimeoutMinutes = value;
					this.NotifyPropertyChanged("ApplicationTimeoutMinutes");
					this.NotifyDBPropertyChanged("ApplicationTimeoutMinutes");
				}
			}
		}

		[DataMember]
		public string RemoteFileDownloadDirectory
		{
			get { return this.m_RemoteFileDownloadDirectory; }
			set
			{
				if(this.m_RemoteFileDownloadDirectory != value)
				{
					this.m_RemoteFileDownloadDirectory = value;
					this.NotifyPropertyChanged("RemoteFileDownloadDirectory");
					this.NotifyDBPropertyChanged("RemoteFileDownloadDirectory");
				}
			}
		}

		[DataMember]
		public string RemoteFileUploadDirectory
		{
			get { return this.m_RemoteFileUploadDirectory; }
			set
			{
				if(this.m_RemoteFileUploadDirectory != value)
				{
					this.m_RemoteFileUploadDirectory = value;
					this.NotifyPropertyChanged("RemoteFileUploadDirectory");
					this.NotifyDBPropertyChanged("RemoteFileUploadDirectory");
				}
			}
		}

		[DataMember]
		public string AlertEmailAddress
		{
			get { return this.m_AlertEmailAddress; }
			set
			{
				if(this.m_AlertEmailAddress != value)
				{
					this.m_AlertEmailAddress = value;
					this.NotifyPropertyChanged("AlertEmailAddress");
					this.NotifyDBPropertyChanged("AlertEmailAddress");
				}
			}
		}

		[DataMember]
		public bool SaveUserNameLocal
		{
			get { return this.m_SaveUserNameLocal; }
			set
			{
				if(this.m_SaveUserNameLocal != value)
				{
					this.m_SaveUserNameLocal = value;
					this.NotifyPropertyChanged("SaveUserNameLocal");
					this.NotifyDBPropertyChanged("SaveUserNameLocal");
				}
			}
		}

		[DataMember]
		public bool SavePasswordLocal
		{
			get { return this.m_SavePasswordLocal; }
			set
			{
				if(this.m_SavePasswordLocal != value)
				{
					this.m_SavePasswordLocal = value;
					this.NotifyPropertyChanged("SavePasswordLocal");
					this.NotifyDBPropertyChanged("SavePasswordLocal");
				}
			}
		}

		[DataMember]
		public bool EnableApplicationTimeout
		{
			get { return this.m_EnableApplicationTimeout; }
			set
			{
				if(this.m_EnableApplicationTimeout != value)
				{
					this.m_EnableApplicationTimeout = value;
					this.NotifyPropertyChanged("EnableApplicationTimeout");
					this.NotifyDBPropertyChanged("EnableApplicationTimeout");
				}
			}
		}

		[DataMember]
		public bool EnableSaveSettings
		{
			get { return this.m_EnableSaveSettings; }
			set
			{
				if(this.m_EnableSaveSettings != value)
				{
					this.m_EnableSaveSettings = value;
					this.NotifyPropertyChanged("EnableSaveSettings");
					this.NotifyDBPropertyChanged("EnableSaveSettings");
				}
			}
		}

		[DataMember]
		public bool EnableFileUpload
		{
			get { return this.m_EnableFileUpload; }
			set
			{
				if(this.m_EnableFileUpload != value)
				{
					this.m_EnableFileUpload = value;
					this.NotifyPropertyChanged("EnableFileUpload");
					this.NotifyDBPropertyChanged("EnableFileUpload");
				}
			}
		}

		[DataMember]
		public bool EnableFileDownload
		{
			get { return this.m_EnableFileDownload; }
			set
			{
				if(this.m_EnableFileDownload != value)
				{
					this.m_EnableFileDownload = value;
					this.NotifyPropertyChanged("EnableFileDownload");
					this.NotifyDBPropertyChanged("EnableFileDownload");
				}
			}
		}

		[DataMember]
		public bool EnableOrderEntry
		{
			get { return this.m_EnableOrderEntry; }
			set
			{
				if(this.m_EnableOrderEntry != value)
				{
					this.m_EnableOrderEntry = value;
					this.NotifyPropertyChanged("EnableOrderEntry");
					this.NotifyDBPropertyChanged("EnableOrderEntry");
				}
			}
		}

		[DataMember]
		public bool EnableReportBrowser
		{
			get { return this.m_EnableReportBrowser; }
			set
			{
				if(this.m_EnableReportBrowser != value)
				{
					this.m_EnableReportBrowser = value;
					this.NotifyPropertyChanged("EnableReportBrowser");
					this.NotifyDBPropertyChanged("EnableReportBrowser");
				}
			}
		}

		[DataMember]
		public bool EnableBillingBrowser
		{
			get { return this.m_EnableBillingBrowser; }
			set
			{
				if(this.m_EnableBillingBrowser != value)
				{
					this.m_EnableBillingBrowser = value;
					this.NotifyPropertyChanged("EnableBillingBrowser");
					this.NotifyDBPropertyChanged("EnableBillingBrowser");
				}
			}
		}

		[DataMember]
		public bool EnableEmailAlert
		{
			get { return this.m_EnableEmailAlert; }
			set
			{
				if(this.m_EnableEmailAlert != value)
				{
					this.m_EnableEmailAlert = value;
					this.NotifyPropertyChanged("EnableEmailAlert");
					this.NotifyDBPropertyChanged("EnableEmailAlert");
				}
			}
		}

		[DataMember]
		public int SystemUserId
		{
			get { return this.m_SystemUserId; }
			set
			{
				if(this.m_SystemUserId != value)
				{
					this.m_SystemUserId = value;
					this.NotifyPropertyChanged("SystemUserId");
					this.NotifyDBPropertyChanged("SystemUserId");
				}
			}
		}

		[DataMember]
		public string Signature
		{
			get { return this.m_Signature; }
			set
			{
				if(this.m_Signature != value)
				{
					this.m_Signature = value;
					this.NotifyPropertyChanged("Signature");
					this.NotifyDBPropertyChanged("Signature");
				}
			}
		}

		[DataMember]
		public string FacilityId
		{
			get { return this.m_FacilityId; }
			set
			{
				if(this.m_FacilityId != value)
				{
					this.m_FacilityId = value;
					this.NotifyPropertyChanged("FacilityId");
					this.NotifyDBPropertyChanged("FacilityId");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_WebServiceAccountId = propertyWriter.WriteInt("WebServiceAccountId");
			this.m_UserName = propertyWriter.WriteString("UserName");
			this.m_Password = propertyWriter.WriteString("Password");
			this.m_DisplayName = propertyWriter.WriteString("DisplayName");
			this.m_PrimaryClientId = propertyWriter.WriteInt("PrimaryClientId");
			this.m_DownloadFileType = propertyWriter.WriteString("DownloadFileType");
			this.m_InitialPage = propertyWriter.WriteString("InitialPage");
			this.m_ApplicationTimeoutMinutes = propertyWriter.WriteInt("ApplicationTimeoutMinutes");
			this.m_RemoteFileDownloadDirectory = propertyWriter.WriteString("RemoteFileDownloadDirectory");
			this.m_RemoteFileUploadDirectory = propertyWriter.WriteString("RemoteFileUploadDirectory");
			this.m_AlertEmailAddress = propertyWriter.WriteString("AlertEmailAddress");
			this.m_SaveUserNameLocal = propertyWriter.WriteBoolean("SaveUserNameLocal");
			this.m_SavePasswordLocal = propertyWriter.WriteBoolean("SavePasswordLocal");
			this.m_EnableApplicationTimeout = propertyWriter.WriteBoolean("EnableApplicationTimeout");
			this.m_EnableSaveSettings = propertyWriter.WriteBoolean("EnableSaveSettings");
			this.m_EnableFileUpload = propertyWriter.WriteBoolean("EnableFileUpload");
			this.m_EnableFileDownload = propertyWriter.WriteBoolean("EnableFileDownload");
			this.m_EnableOrderEntry = propertyWriter.WriteBoolean("EnableOrderEntry");
			this.m_EnableReportBrowser = propertyWriter.WriteBoolean("EnableReportBrowser");
			this.m_EnableBillingBrowser = propertyWriter.WriteBoolean("EnableBillingBrowser");
			this.m_EnableEmailAlert = propertyWriter.WriteBoolean("EnableEmailAlert");
			this.m_SystemUserId = propertyWriter.WriteInt("SystemUserId");
			this.m_Signature = propertyWriter.WriteString("Signature");
			this.m_FacilityId = propertyWriter.WriteString("FacilityId");
		}
		#endregion

		#region ReadPropertiesMethod
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadInt("WebServiceAccountId", WebServiceAccountId);
			propertyReader.ReadString("UserName", UserName);
			propertyReader.ReadString("Password", Password);
			propertyReader.ReadString("DisplayName", DisplayName);
			propertyReader.ReadInt("PrimaryClientId", PrimaryClientId);
			propertyReader.ReadString("DownloadFileType", DownloadFileType);
			propertyReader.ReadString("InitialPage", InitialPage);
			propertyReader.ReadInt("ApplicationTimeoutMinutes", ApplicationTimeoutMinutes);
			propertyReader.ReadString("RemoteFileDownloadDirectory", RemoteFileDownloadDirectory);
			propertyReader.ReadString("RemoteFileUploadDirectory", RemoteFileUploadDirectory);
			propertyReader.ReadString("AlertEmailAddress", AlertEmailAddress);
			propertyReader.ReadBoolean("SaveUserNameLocal", SaveUserNameLocal);
			propertyReader.ReadBoolean("SavePasswordLocal", SavePasswordLocal);
			propertyReader.ReadBoolean("EnableApplicationTimeout", EnableApplicationTimeout);
			propertyReader.ReadBoolean("EnableSaveSettings", EnableSaveSettings);
			propertyReader.ReadBoolean("EnableFileUpload", EnableFileUpload);
			propertyReader.ReadBoolean("EnableFileDownload", EnableFileDownload);
			propertyReader.ReadBoolean("EnableOrderEntry", EnableOrderEntry);
			propertyReader.ReadBoolean("EnableReportBrowser", EnableReportBrowser);
			propertyReader.ReadBoolean("EnableBillingBrowser", EnableBillingBrowser);
			propertyReader.ReadBoolean("EnableEmailAlert", EnableEmailAlert);
			propertyReader.ReadInt("SystemUserId", SystemUserId);
			propertyReader.ReadString("Signature", Signature);
			propertyReader.ReadString("FacilityId", FacilityId);
		}
		#endregion
	}
}
