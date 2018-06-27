using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.WebService
{
    [PersistentClass("tblWebServiceAccountClient", "YPIDATA")]
    public class WebServiceAccountClient : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int m_WebServiceAccountClientId;
        private int m_WebServiceAccountId;
        private int m_ClientId;
        private string m_ObjectId;

        private string m_ClientName;
        private WebServiceAccount m_WebServiceAccount;

        public WebServiceAccountClient()
        { }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentPrimaryKeyProperty(false)]
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

        [PersistentProperty()]
        public string ObjectId
        {
            get { return this.m_ObjectId; }
            set
            {
                if (this.m_ObjectId != value)
                {
                    this.m_ObjectId = value;
                    this.NotifyPropertyChanged("ObjectId");
                }
            }
        }

        [PersistentProperty()]
        public WebServiceAccount WebServiceAccount
        {
            get { return this.m_WebServiceAccount; }
            set
            {
                if (this.m_WebServiceAccount != value)
                {
                    this.m_WebServiceAccount = value;
                    this.NotifyPropertyChanged("WebServiceAccount");
                }
            }
        }

        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (this.m_ClientName != value)
                {
                    this.m_ClientName = value;
                    this.NotifyPropertyChanged("ClientName");
                }
            }
        }
    }
}
