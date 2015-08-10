using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace YellowstonePathology.YpiConnect.Contract.Search
{
    [ServiceContract] 
    public interface IClientSearchService
    {
        [OperationContract]
        bool Ping();

        [OperationContract]
        void AcknowledgeDistributions(string reportDistributionLogIdStringList);

        [OperationContract]
        SearchResultCollection ExecuteSearch(Search search);          
    }
}
