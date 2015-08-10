using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ClientWebServices
{
    [DataContract]
    public class ClientUser
    {                
        List<Domain.Client> m_Clients;
        List<Domain.Physician> m_Physicians;

        string m_UserName;
        string m_DisplayName;
        string m_ClientKey;
        string m_Password;
        bool m_ReportFinderPageIsAvailable;
        bool m_SynchronizationPageIsAvailable;
        string m_DownloadFileType;
        string m_InitialPage;
        ClientWebServices.ClientSearchModeEnum m_ClientSearchMode;        

        public ClientUser()
        {
            this.m_Clients = new List<ClientWebServices.Domain.Client>();
            this.m_Physicians = new List<ClientWebServices.Domain.Physician>();

            this.m_DownloadFileType = "xps";
            this.m_InitialPage = "ReportBrowser";
        }        
        
        public string ClientIdStringList
        {
            get
            {
                string result = string.Empty;
                for (int i = 0; i < this.m_Clients.Count; i++ )
                {
                    result = result + (this.m_Clients[i].ClientId);
                    if (i != this.m_Clients.Count - 1)
                    {
                        result = result + (", ");
                    }
                }
                return result;
            }
        }

        public string PhysicianIdStringList
        {
            get
            {
                string result = string.Empty;
                for (int i = 0; i < this.m_Physicians.Count; i++)
                {
                    result = result + (this.m_Physicians[i].PhysicianID);
                    if (i != this.m_Physicians.Count - 1)
                    {
                        result = result + (", ");
                    }
                }
                return result;
            }
        }

        public List<int> PhysicianIds
        {
            get
            {
                var query = from item in this.Physicians
                            select item.PhysicianID;
                return query.ToList<int>();                
            }
        }

        public List<int> ClientIds
        {
            get
            {
                var query = from item in this.Clients
                            select item.ClientId;
                return query.ToList<int>();
            }
        }

        [DataMember]
        public ClientWebServices.ClientSearchModeEnum ClientSearchMode
        {
            get { return this.m_ClientSearchMode; }
            set { this.m_ClientSearchMode = value; }
        }

        [DataMember]
        public List<Domain.Client> Clients
        {
            get { return this.m_Clients; }
            set { this.m_Clients = value; }
        }         

        [DataMember]
        public List<Domain.Physician> Physicians
        {
            get { return this.m_Physicians; }
            set { this.m_Physicians = value; }
        }         

        [DataMember]
        public string UserName
        {
            get { return this.m_UserName; }
            set { this.m_UserName = value; }
        }

        [DataMember]
        public string ClientKey
        {
            get { return this.m_ClientKey; }
            set { this.m_ClientKey = value; }
        }

        [DataMember]
        public string DisplayName
        {
            get { return this.m_DisplayName; }
            set { this.m_DisplayName = value; }
        }

        [DataMember]
        public bool ReportFinderPageIsAvailable
        {
            get { return this.m_ReportFinderPageIsAvailable; }
            set { this.m_ReportFinderPageIsAvailable = value; }
        }

        [DataMember]
        public bool SynchronizationPageIsAvailable
        {
            get { return this.m_SynchronizationPageIsAvailable; }
            set { this.m_SynchronizationPageIsAvailable = value; }
        }

        [DataMember]
        public string DownloadFileType
        {
            get { return this.m_DownloadFileType; }
            set { this.m_DownloadFileType = value; }
        }

        [DataMember]
        public string InitialPage
        {
            get { return this.m_InitialPage; }
            set { this.m_InitialPage = value; }
        }        
    }
}
