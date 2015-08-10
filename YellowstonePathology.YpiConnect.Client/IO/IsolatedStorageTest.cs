using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;

namespace YellowstonePathology.YpiConnect.Client.IO
{
    public class IsolatedStorageTest 
    {        
        public void WriteIt()
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);                        
            IsolatedStorageFileStream isoStream1 =  new IsolatedStorageFileStream("InTheRoot.txt", FileMode.Create, isoStore);          
            isoStream1.Close();
        }

        public void DeleteIt()
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
            isoStore.DeleteFile("InTheRoot.txt");
        }

        public void ReadIt()
        {

        }
    }
}