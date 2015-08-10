using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClientWebServices
{
    [DataContract]
    public class Search
    {        
        public SearchTypeEnum m_SearchType;
        List<object> m_SearchParameters;        
        ClientWebServices.ClientUser m_ClientUser;

        public Search()
        {     
            this.m_SearchParameters = new List<object>();
        }        

        [DataMember]
        public SearchTypeEnum SearchType
        {
            get { return this.m_SearchType; }
            set { this.m_SearchType = value; }
        }

        [DataMember]
        public List<object> SearchParameters
        {
            get { return this.m_SearchParameters; }
            set { this.m_SearchParameters = value; }
        }

        [DataMember]
        public ClientWebServices.ClientUser ClientUser
        {
            get { return this.m_ClientUser; }
            set { this.m_ClientUser = value; }
        }
    }
}
