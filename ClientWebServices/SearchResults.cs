using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClientWebServices
{
    [DataContract]
    public class SearchResults
    {
        [DataMember]
        public List<SearchResult> Items;

        public void Populate(List<SearchResult> searchResultList)
        {
            foreach (SearchResult searchResult in searchResultList)
            {
                this.Items.Add(new SearchResult()
                {
                    MasterAccessionNo = searchResult.MasterAccessionNo,
                    AccessionDate = searchResult.AccessionDate,
                    PatientName = searchResult.PatientName
                });
            }
        }
    }
}
