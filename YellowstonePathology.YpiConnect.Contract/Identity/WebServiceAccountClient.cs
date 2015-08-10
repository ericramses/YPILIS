using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{
    [DataContract]
	[PersistentClass("tblWebServiceAccountClient", "YPIDATA")]
	public class WebServiceAccountClient : INotifyPropertyChanged
	{
		protected delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_WebServiceAccountClientId;
		private int m_WebServiceAccountId;
		private int m_ClientId;

		public WebServiceAccountClient()
		{

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
		public int WebServiceAccountClientId
		{
			get { return this.m_WebServiceAccountClientId; }
			set
			{
				if (this.m_WebServiceAccountClientId != value)
				{
					this.m_WebServiceAccountClientId = value;
					this.NotifyPropertyChanged("WebServiceAccountClientId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
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
		public int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if (this.m_ClientId != value)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
				}
			}
		}
	}
}
