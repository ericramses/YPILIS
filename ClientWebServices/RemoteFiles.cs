using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClientWebServices
{
    [DataContract]
    public class RemoteFiles
    {
        List<RemoteFile> m_Items;

        public RemoteFiles()
        {
            this.m_Items = new List<RemoteFile>();
        }

        [DataMember]
        public List<RemoteFile> Items
        {
            get { return this.m_Items; }
            set { this.m_Items = value; }
        }
    }    
}
