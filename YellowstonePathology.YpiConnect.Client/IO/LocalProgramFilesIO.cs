using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Client.IO
{
    public class LocalProgramFilesIO : IFileIO
    {
        public void CreateApplicationRoot()
        {            
            this.CreateDirectory("Program Files\\Yellowstone Pathology Institute");
            this.CreateDirectory("Program Files\\Yellowstone Pathology Institute\\Client Services");    
        }

        public void CreateDirectory(string directoryName)
        {
            if (Directory.Exists(directoryName) == false)
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        public bool DirectoryExists(string directoryName)
        {
            bool result = false;
            if (Directory.Exists(directoryName) == true)
            {
                result = true;
            }
            return result;
        }

        public bool FileExists(string fileName)
        {
            bool result = false;
            if (File.Exists(fileName) == true)
            {
                result = true;
            }
            return result;
        }

        public void SaveLocal(XmlDocument xmlDocument)
        {
            throw new NotImplementedException("Not implemented");
        }

        public FileStream GetFileStream(string fileName, System.IO.FileMode fileMode)
        {
            string applicationSettingsFile = fileName;
            return new FileStream(applicationSettingsFile, fileMode);
        }

        public void DeleteFile(string fileName)
        {
            throw new NotImplementedException("Not implemented");
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetSavedOrders()
        {
            throw new NotImplementedException("Not implemented");
        }
    }
}
