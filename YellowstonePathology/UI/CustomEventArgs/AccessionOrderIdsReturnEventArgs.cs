using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class AccessionOrderIdsReturnEventArgs : EventArgs
    {
        string m_AccessionOrderIds;

        public AccessionOrderIdsReturnEventArgs(string accessionOrderIds)
        {
            this.m_AccessionOrderIds = accessionOrderIds;
        }

        public string AccessionOrderIds
        {
            get { return this.m_AccessionOrderIds; }
        }
    }
}
