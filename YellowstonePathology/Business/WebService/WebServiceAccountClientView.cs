using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.WebService
{
    public class WebServiceAccountClientView
    {
        private string m_ClientName;

        private WebServiceAccountClient m_WebServiceAccountClient;

        public WebServiceAccountClientView()
        { }

        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        public WebServiceAccountClient WebServiceAccountClient
        {
            get { return this.m_WebServiceAccountClient; }
            set { this.m_WebServiceAccountClient = value; }
        }
    }
}
