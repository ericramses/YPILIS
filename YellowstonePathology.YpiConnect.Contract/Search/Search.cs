using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract.Search
{
    [DataContract]
    public class Search
    {        
        public SearchTypeEnum m_SearchType;
        List<object> m_SearchParameters;
        YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount m_WebServiceAccount;

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
        public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount WebServiceAccount
        {
            get { return this.m_WebServiceAccount; }
            set { this.m_WebServiceAccount = value; }
        }
    }
}
