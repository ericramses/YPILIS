using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace ClientWebServices
{
    // NOTE: If you change the class name "Services" here, you must also update the reference to "Services" in Web.config.
    public class Services : IServices
    {        
        public bool IsAuthorized()
        {
            return true;
        }
        
        public void AcknowledgeDistributions(string reportDistributionLogIdStringList)
        {
            DataContext.YpiData dataContext = new ClientWebServices.DataContext.YpiData();
            Repository.SearchRepository repository = new Repository.SearchRepository(dataContext);
            repository.AcknowledgeDistributions(reportDistributionLogIdStringList);
        }

        public SearchResults ExecuteSearch(Search search)
        {
            DataContext.YpiData dataContext = new ClientWebServices.DataContext.YpiData();
            Repository.SearchRepository repository = new Repository.SearchRepository(dataContext);

            SearchResults searchResults = new SearchResults();
            searchResults.Items = new List<SearchResult>();
         
            switch (search.SearchType)
            {                    
                case SearchTypeEnum.PatientLastNameSearch:
                    if (search.ClientUser.ClientSearchMode == ClientWebServices.ClientSearchModeEnum.UseClientList)
                    {
                        searchResults.Items = repository.GetClientCasesByPatientLastName(search);
                    }
                    else
                    {
                        searchResults.Items = repository.GetPhysicianCasesByPatientLastName(search);
                    }
                    break;                
                case SearchTypeEnum.PatientLastAndFirstNameSearch:
                    if (search.ClientUser.ClientSearchMode == ClientWebServices.ClientSearchModeEnum.UseClientList)
                    {
                        searchResults.Items = repository.GetClientCasesByPatientLastNameAndFirstName(search);
                    }
                    else
                    {
                        searchResults.Items = repository.GetPhysicianCasesByPatientLastNameAndFirstName(search);
                    }
                    break;                
                case SearchTypeEnum.RecentCases:
                    if (search.ClientUser.ClientSearchMode == ClientWebServices.ClientSearchModeEnum.UseClientList)
                    {
                        searchResults.Items = repository.GetClientRecentCases(search);
                    }
                    else
                    {
                        searchResults.Items = repository.GetPhysicianRecentCases(search);
                    }                    
                    break;                                    
                case SearchTypeEnum.NotDownloaded: //Not Downloaded is Depricated SH 5/17/2010
                case SearchTypeEnum.NotAcknowledged:
                    if (search.ClientUser.ClientSearchMode == ClientWebServices.ClientSearchModeEnum.UseClientList)
                    {
                        searchResults.Items = repository.GetClientCasesNotAcknowledged(search);
                    }
                    else
                    {
                        searchResults.Items = repository.GetPhysicianCasesNotAcknowledged(search);
                    }
                    break;                
                case SearchTypeEnum.DateOfBirth:
                    if (search.ClientUser.ClientSearchMode == ClientWebServices.ClientSearchModeEnum.UseClientList)
                    {
                        searchResults.Items = repository.GetClientCasesByPBirthDate(search);
                    }
                    else
                    {
                        searchResults.Items = repository.GetPhysicianCasesByPBirthDate(search);
                    }
                    break;                
                case SearchTypeEnum.SocialSecurityNumber:
                    if (search.ClientUser.ClientSearchMode == ClientWebServices.ClientSearchModeEnum.UseClientList)
                    {
                        searchResults.Items = repository.GetClientCasesByPSSN(search);
                    }
                    else
                    {
                        searchResults.Items = repository.GetPhysicianCasesByPSSN(search);
                    }
                    break;                
                case SearchTypeEnum.PhysicianId:
                    searchResults.Items = repository.GetClientCasesByPhysicianId(search);
                    break;                
            }

            return searchResults;
        }

        public ClientWebServices.ClientUser GetClientUser()
        {
            ClientWebServices.ClientUserList clientuserList = new ClientWebServices.ClientUserList();
            clientuserList.FromCode();
            ClientWebServices.ClientUser clientUser = clientuserList.GetCurrentUser();
            return clientUser;            
        }        

        public void SynchronizeRemoteDirectories(List<RemoteDirectory> remoteDirectories)
        {
            foreach (RemoteDirectory remoteDirectory in remoteDirectories)
            {
                if (remoteDirectory.ExistsLocally == true)
                {
                    if (System.IO.Directory.Exists(remoteDirectory.Uri.LocalPath) == true)
                    {
                        System.IO.Directory.Delete(remoteDirectory.Uri.LocalPath, true);
                    }
                }
            }
        }        

        public System.IO.Stream DownloadFile(RemoteFile remoteFile)
        {
            try
            {
                FileStream fileStream = File.OpenRead(remoteFile.Uri.LocalPath);
                return fileStream;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public System.IO.Stream DownloadCaseDocument(string reportNo)
        {
            string fileName = FileHelper.GetCasePath(reportNo) + reportNo + ".xps";

            FileStream fileStream = null;
            if (System.IO.File.Exists(fileName) == true)
            {
                fileStream = File.OpenRead(fileName);
            }
            return fileStream;            
        }

        public System.IO.Stream Download(string fileName)
        {            
            FileStream fileStream = null;
            if (System.IO.File.Exists(fileName) == true)
            {
                fileStream = File.OpenRead(fileName);
            }
            return fileStream;
        }

        public RemoteDirectories GetLMDDirectories()
        {
			string path = @"\\Ypiibldoc1\Documents\FlowCytometry\Analysis\BillingsClinic\LMD";
            RemoteDirectories lmdDirectories = new RemoteDirectories();
            string[] directories = System.IO.Directory.GetDirectories(path);
            foreach (string directoryName in directories)
            {
                RemoteDirectory lmdDirectory = new RemoteDirectory(directoryName);
                lmdDirectories.Items.Add(lmdDirectory);

                string[] files = System.IO.Directory.GetFiles(directoryName);
                foreach (string file in files)
                {
                    RemoteFile lmdFile = new RemoteFile(file);
                    lmdDirectory.Files.Items.Add(lmdFile);
                }
            }
            return lmdDirectories;
        }
                     
        public bool UploadDocument(UploadDocument uploadDocument)
        {
            bool result = true;

            FileStream fileStream = null;
            BinaryWriter writer = null;

            try
            {
                string saveFilePath = @"\\cfileserver\Documents\DocumentUpload\" + uploadDocument.FileName;
                fileStream = File.Open(saveFilePath, FileMode.Create);
                writer = new BinaryWriter(fileStream);
                writer.Write(uploadDocument.FileStream);
            }
            catch (Exception)
            {
                result = false;
            }
            finally 
            {
                if (fileStream != null)
                    fileStream.Close(); 
                if (writer != null)
                        writer.Close(); 
            }
             
            return result;             
        }         
    }
}
