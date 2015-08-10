using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Service
{
    public class SubmitterService : YellowstonePathology.YpiConnect.Contract.ISubmitterService
    {
		public void Submit(YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent)
        {            
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Business.Persistence.ObjectTracker();
            objectTracker.SubmitChanges(remoteObjectTransferAgent);            
		}

        public void InsertBaseClassOnly(object subclassObject)
        {
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Business.Persistence.ObjectTracker();
            objectTracker.InsertSubclassOnly(subclassObject);            
        }
	}
}
