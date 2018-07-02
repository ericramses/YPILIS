using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.WebService
{
    public class WebServiceAccountClientView
    {
        private int m_WebServiceAccountClientId;
        private int m_ClientId;
        private int m_WebServiceAccountId;
        private string m_ClientName;

        public WebServiceAccountClientView()
        { }

        [PersistentProperty()]
        public int WebServiceAccountClientId
        {
            get { return this.m_WebServiceAccountClientId; }
            set { this.m_WebServiceAccountClientId = value; }
        }

        [PersistentProperty()]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }

        [PersistentProperty()]
        public int WebServiceAccountId
        {
            get { return this.m_WebServiceAccountId; }
            set { this.m_WebServiceAccountId = value; }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }
    }
}
