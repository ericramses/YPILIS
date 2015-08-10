using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClientWebServices
{
    [DataContract]
    public class RemoteFile
    {
        Uri m_Uri;
        Uri m_LocalUri;
        string m_DisplayName;        
        bool m_ExistsLocally;

        public RemoteFile(string fullName)
        {
            this.m_Uri = new Uri(fullName);
            this.m_DisplayName = this.m_Uri.SetDisplayName();
        }

        [DataMember]
        public Uri Uri
        {
            get { return this.m_Uri; }
            set { this.m_Uri = value; }
        }

        [DataMember]
        public Uri LocalUri
        {
            get { return this.m_LocalUri; }
            set { this.m_LocalUri = value; }
        }

        [DataMember]
        public string DisplayName
        {
            get { return this.m_DisplayName; }
            set { this.m_DisplayName = value; }
        }

        [DataMember]
        public bool ExistsLocally
        {
            get { return this.m_ExistsLocally; }
            set { this.m_ExistsLocally = value; }
        }
    }
}
