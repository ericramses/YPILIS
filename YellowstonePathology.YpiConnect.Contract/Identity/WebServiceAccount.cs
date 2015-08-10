using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{
    [DataContract]
	[PersistentClass("tblWebServiceAccount", "YPIDATA")]
	public class WebServiceAccount : INotifyPropertyChanged
    {
		protected delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		private bool m_IsKnown;
        private string m_LocalFileDownloadDirectory;
		private string m_LocalFileUploadDirectory;

		//Persistent data
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

        public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountClientCollection m_WebServiceAccountClientCollection;
		public YellowstonePathology.Business.Client.Model.Client m_Client;

        public WebServiceAccount()
        {            
            this.m_WebServiceAccountClientCollection = new WebServiceAccountClientCollection();
			this.m_IsKnown = true;
        }

        public WebServiceAccount(SavedSettings savedSettings)
        {
            this.m_UserName = savedSettings.UserName;
            this.m_Password = savedSettings.Password;
			this.m_LocalFileDownloadDirectory = savedSettings.LocalFileDownloadDirectory;
			this.m_LocalFileUploadDirectory = savedSettings.LocalFileUploadDirectory;
			this.m_IsKnown = false;
            this.m_EnableApplicationTimeout = false;
            this.m_ApplicationTimeoutMinutes = 0;
        }        

        [DataMember]
        public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountClientCollection WebServiceAccountClientCollection
        {
            get { return this.m_WebServiceAccountClientCollection; }
            set { this.m_WebServiceAccountClientCollection = value; }
        }

		[DataMember]
		public YellowstonePathology.Business.Client.Model.Client Client
		{
			get { return this.m_Client; }
			set { this.m_Client = value; }
		}        

		public string GetUserNamePasswordString()
        {            
            return "Username: " + this.m_UserName + " - Password: " + this.m_Password;
        }

		[DataMember]
		public bool IsKnown
		{
			get { return this.m_IsKnown; }
			set { this.m_IsKnown = value; }
		}

        public string LocalFileDownloadDirectory
        {
			get { return this.m_LocalFileDownloadDirectory; }
			set { this.m_LocalFileDownloadDirectory = value; }
        }

		public string LocalFileUploadDirectory
		{
			get { return this.m_LocalFileUploadDirectory; }
			set { this.m_LocalFileUploadDirectory = value; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[DataMember]
		[PersistentPrimaryKeyProperty(true)]
		public int WebServiceAccountId
		{
			get { return this.m_WebServiceAccountId; }
			set
			{
				if (this.m_WebServiceAccountId != value)
				{
					this.m_WebServiceAccountId = value;
					this.NotifyPropertyChanged("WebServiceAccountId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string UserName
		{
			get { return this.m_UserName; }
			set
			{
				if (this.m_UserName != value)
				{
					this.m_UserName = value;
					this.NotifyPropertyChanged("UserName");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string Password
		{
			get { return this.m_Password; }
			set
			{
				if (this.m_Password != value)
				{
					this.m_Password = value;
					this.NotifyPropertyChanged("Password");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string DisplayName
		{
			get { return this.m_DisplayName; }
			set
			{
				if (this.m_DisplayName != value)
				{
					this.m_DisplayName = value;
					this.NotifyPropertyChanged("DisplayName");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int PrimaryClientId
		{
			get { return this.m_PrimaryClientId; }
			set
			{
				if (this.m_PrimaryClientId != value)
				{
					this.m_PrimaryClientId = value;
					this.NotifyPropertyChanged("PrimaryClientId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string DownloadFileType
		{
			get { return this.m_DownloadFileType; }
			set
			{
				if (this.m_DownloadFileType != value)
				{
					this.m_DownloadFileType = value;
					this.NotifyPropertyChanged("DownloadFileType");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string InitialPage
		{
			get { return this.m_InitialPage; }
			set
			{
				if (this.m_InitialPage != value)
				{
					this.m_InitialPage = value;
					this.NotifyPropertyChanged("InitialPage");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int ApplicationTimeoutMinutes
		{
			get { return this.m_ApplicationTimeoutMinutes; }
			set
			{
				if (this.m_ApplicationTimeoutMinutes != value)
				{
					this.m_ApplicationTimeoutMinutes = value;
					this.NotifyPropertyChanged("ApplicationTimeoutMinutes");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string RemoteFileDownloadDirectory
		{
			get { return this.m_RemoteFileDownloadDirectory; }
			set
			{
				if (this.m_RemoteFileDownloadDirectory != value)
				{
					this.m_RemoteFileDownloadDirectory = value;
					this.NotifyPropertyChanged("RemoteFileDownloadDirectory");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string RemoteFileUploadDirectory
		{
			get { return this.m_RemoteFileUploadDirectory; }
			set
			{
				if (this.m_RemoteFileUploadDirectory != value)
				{
					this.m_RemoteFileUploadDirectory = value;
					this.NotifyPropertyChanged("RemoteFileUploadDirectory");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string AlertEmailAddress
		{
			get { return this.m_AlertEmailAddress; }
			set
			{
				if (this.m_AlertEmailAddress != value)
				{
					this.m_AlertEmailAddress = value;
					this.NotifyPropertyChanged("AlertEmailAddress");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool SaveUserNameLocal
		{
			get { return this.m_SaveUserNameLocal; }
			set
			{
				if (this.m_SaveUserNameLocal != value)
				{
					this.m_SaveUserNameLocal = value;
					this.NotifyPropertyChanged("SaveUserNameLocal");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool SavePasswordLocal
		{
			get { return this.m_SavePasswordLocal; }
			set
			{
				if (this.m_SavePasswordLocal != value)
				{
					this.m_SavePasswordLocal = value;
					this.NotifyPropertyChanged("SavePasswordLocal");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool EnableApplicationTimeout
		{
			get { return this.m_EnableApplicationTimeout; }
			set
			{
				if (this.m_EnableApplicationTimeout != value)
				{
					this.m_EnableApplicationTimeout = value;
					this.NotifyPropertyChanged("EnableApplicationTimeout");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool EnableSaveSettings
		{
			get { return this.m_EnableSaveSettings; }
			set
			{
				if (this.m_EnableSaveSettings != value)
				{
					this.m_EnableSaveSettings = value;
					this.NotifyPropertyChanged("EnableSaveSettings");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool EnableFileUpload
		{
			get { return this.m_EnableFileUpload; }
			set
			{
				if (this.m_EnableFileUpload != value)
				{
					this.m_EnableFileUpload = value;
					this.NotifyPropertyChanged("EnableFileUpload");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool EnableFileDownload
		{
			get { return this.m_EnableFileDownload; }
			set
			{
				if (this.m_EnableFileDownload != value)
				{
					this.m_EnableFileDownload = value;
					this.NotifyPropertyChanged("EnableFileDownload");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool EnableOrderEntry
		{
			get { return this.m_EnableOrderEntry; }
			set
			{
				if (this.m_EnableOrderEntry != value)
				{
					this.m_EnableOrderEntry = value;
					this.NotifyPropertyChanged("EnableOrderEntry");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool EnableReportBrowser
		{
			get { return this.m_EnableReportBrowser; }
			set
			{
				if (this.m_EnableReportBrowser != value)
				{
					this.m_EnableReportBrowser = value;
					this.NotifyPropertyChanged("EnableReportBrowser");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool EnableBillingBrowser
		{
			get { return this.m_EnableBillingBrowser; }
			set
			{
				if (this.m_EnableBillingBrowser != value)
				{
					this.m_EnableBillingBrowser = value;
					this.NotifyPropertyChanged("EnableBillingBrowser");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool EnableEmailAlert
		{
			get { return this.m_EnableEmailAlert; }
			set
			{
				if (this.m_EnableEmailAlert != value)
				{
					this.m_EnableEmailAlert = value;
					this.NotifyPropertyChanged("EnableEmailAlert");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int SystemUserId
		{
			get { return this.m_SystemUserId; }
			set
			{
				if (this.m_SystemUserId != value)
				{
					this.m_SystemUserId = value;
					this.NotifyPropertyChanged("SystemUserId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string Signature
		{
			get { return this.m_Signature; }
			set
			{
				if (this.m_Signature != value)
				{
					this.m_Signature = value;
					this.NotifyPropertyChanged("Signature");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string FacilityId
		{
			get { return this.m_FacilityId; }
			set
			{
				if (this.m_FacilityId != value)
				{
					this.m_FacilityId = value;
					this.NotifyPropertyChanged("FacilityId");
				}
			}
		}
	}
}
