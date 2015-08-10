using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Client.IO
{
    public interface IFileIO
    {
        void CreateApplicationRoot();
        FileStream GetFileStream(string fileName, System.IO.FileMode fileMode);
        bool FileExists(string fileName);
        bool DirectoryExists(string directoryName);
        void CreateDirectory(string directoryName);
        void DeleteFile(string fileName);
        void SaveLocal(XmlDocument xmlDocument);
        YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetSavedOrders();
    }
}
