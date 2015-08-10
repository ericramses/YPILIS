using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class AccessionOrderWithTrackerReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        public AccessionOrderWithTrackerReturnEventArgs(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Persistence.ObjectTracker ObjectTracker
        {
            get { return this.m_ObjectTracker; }
        }
    }
}
