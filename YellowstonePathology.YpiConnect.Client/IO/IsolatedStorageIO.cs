using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.IO.IsolatedStorage;

namespace YellowstonePathology.YpiConnect.Client.IO
{
    public class IsolatedStorageIO : IFileIO
    {
        public IsolatedStorageIO()
        {
            
        }

        public void CreateApplicationRoot()
        {
            this.CreateDirectory("SavedOrders");
            this.CreateDirectory("Program Files");
            this.CreateDirectory("Program Files\\Yellowstone Pathology");
            this.CreateDirectory("Program Files\\Yellowstone Pathology Institute\\Client Services");            
        }

        public void CreateDirectory(string directoryName)
        {
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);                 
            isoFile.CreateDirectory(directoryName);
        }

        public bool DirectoryExists(string directoryName)
        {
            bool result = false;
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            string[] directories = isoFile.GetDirectoryNames("*");
            foreach (string directory in directories)
            {
                if (directory.ToUpper() == directoryName.ToUpper())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void DeleteAllDirectories()
        {
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            isoFile.Remove();           
        }

        public bool FileExists(string fileName)
        {
            bool result = false;
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            string[] files = null;
            try
            {
                files = isoFile.GetFileNames(fileName);
            }
            catch
            {
                //Do nothing;   
            }

            if (files != null && files.Length != 0)
            {
                result = true;
            }
            return result;
        }

        public FileStream GetFileStream(string fileName, System.IO.FileMode fileMode)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(fileName, fileMode, isoStore);
            return isoStream;
        }

        public void DeleteFile(string fileName)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            isoStore.DeleteFile(fileName);
        }

        public void SaveLocal(XmlDocument xmlDocument)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            string fileName = "SavedOrders\\" + Guid.NewGuid().ToString() + ".xml";
            IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Create, isoStore);
            xmlDocument.Save(stream);
            stream.Close();
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetSavedOrders()
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection();
            /*
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            List<string> files = new List<string>(isoStore.GetFileNames("SavedOrders\\*"));

            foreach (string file in files)
            {                
                StreamReader reader = new StreamReader(new IsolatedStorageFileStream("SavedOrders\\" + file, FileMode.Open, isoStore));
                String result = reader.ReadToEnd();
                reader.Close();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(result);

                XmlEncryptor xmlEncryptor = new XmlEncryptor();
                xmlEncryptor.Decrypt(xmlDocument);

                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Domain.Persistence.SerializationHelper.DeserializeItem<YellowstonePathology.Business.ClientOrder.Model.ClientOrder>(xmlDocument.OuterXml);
                clientOrderCollection.Load(clientOrder);
            }
            */
            return clientOrderCollection;
        }
    }
}
