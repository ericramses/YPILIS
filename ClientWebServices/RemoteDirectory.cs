using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClientWebServices
{
    [DataContract]
    public class RemoteDirectory
    {
        Uri m_Uri;
        string m_DisplayName;
        bool m_ExistsLocally;

        RemoteFiles m_Files;        
        
        public RemoteDirectory(string fullName)
        {
            this.m_Uri = new Uri(fullName);
            this.m_DisplayName = this.m_Uri.SetDisplayName();
            this.m_Files = new RemoteFiles();
        }

        [DataMember]
        public Uri Uri
        {
            get { return this.m_Uri; }
            set { this.m_Uri = value; }
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

        [DataMember]
        public RemoteFiles Files
        {
            get { return this.m_Files; }
            set { this.m_Files = value; }
        }
    }
}
