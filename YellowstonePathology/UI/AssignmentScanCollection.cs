using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI
{
    public class AssignmentScanCollection : ObservableCollection<AssignmentScan>
    {
        public AssignmentScanCollection()
        {

        }

        public bool Exists(string scanId)
        {
            bool result = false;
            foreach(AssignmentScan item in this)
            {
                if(item.ScanId == scanId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
