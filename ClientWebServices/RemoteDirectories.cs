using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClientWebServices
{
    [DataContract]
    public class RemoteDirectories
    {
        List<RemoteDirectory> m_Items;

        public RemoteDirectories()
        {
            this.m_Items = new List<RemoteDirectory>();
        }

        [DataMember]
        public List<RemoteDirectory> Items
        {
            get { return this.m_Items; }
            set { this.m_Items = value; }
        }
    }
}
