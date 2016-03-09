using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.AppMessaging
{
    public class LockReleaseRequestMessageBody : MessageBody
    {                        
        public LockReleaseRequestMessageBody()
        {

        }

        public LockReleaseRequestMessageBody(string masterAccessionNo, string lockAquiredbyUserName, string lockAquiredbyHostName, DateTime timeLockAquired) 
            : base (masterAccessionNo, lockAquiredbyUserName, lockAquiredbyHostName, timeLockAquired)
        {            
            this.m_Message = this.m_RequestingHostName + "\\" + this.m_RequestingUserName + " would like to take " + this.m_MasterAccessionNo;
        }        
    }
}
