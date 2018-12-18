using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Policy
{
    public class DirectoryCollection : ObservableCollection<Directory>
    {
        public DirectoryCollection()
        {
            this.Add(new Directory(0, "YPII", 0));
        }
    }
}
