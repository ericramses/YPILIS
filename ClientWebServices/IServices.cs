using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ClientWebServices
{    
    [ServiceContract]    
    public interface IServices
    {        
        [OperationContract]
        bool IsAuthorized();

        [OperationContract]
        void AcknowledgeDistributions(string reportDistributionLogIdStringList);

        [OperationContract]
        SearchResults ExecuteSearch(Search search);

        [OperationContract]
        ClientWebServices.ClientUser GetClientUser();        

        [OperationContract]
        RemoteDirectories GetLMDDirectories();

        //TODO: this is being depricated
        [OperationContract]
        System.IO.Stream DownloadFile(RemoteFile remoteFile);

        //TODO: this is being depricated
        [OperationContract]
        System.IO.Stream DownloadCaseDocument(string reportNo);

        [OperationContract]
        System.IO.Stream Download(string fileName);

        [OperationContract]
        void SynchronizeRemoteDirectories(List<RemoteDirectory> remoteDirectories);
                      
        [OperationContract]
        bool UploadDocument(UploadDocument uploadDocument);                         
    }
}
