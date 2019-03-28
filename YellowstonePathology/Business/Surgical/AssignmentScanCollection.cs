using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Surgical
{
    public class AssignmentScanCollection : ObservableCollection<AssignmentScan>
    {
        public AssignmentScanCollection()
        { }

        public AssignmentScan GetAssignmentScan(string slideOrderId)
        {
            AssignmentScan result = this.FirstOrDefault(t => t.SlideOrderId == slideOrderId);
            return result;
        }

        public bool Exists(string slideOrderId)
        {
            AssignmentScan test = this.FirstOrDefault(t => t.SlideOrderId == slideOrderId);
            return test == null ? false : true;
        }
    }
}
