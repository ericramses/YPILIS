using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract.Search
{
    [CollectionDataContract]
    public class SearchResultCollection : List<SearchResult>
    {
        
    }
}
