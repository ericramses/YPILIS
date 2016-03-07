using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.AppMessaging
{
    public class LockReleaseResponseMessageBody : MessageBody
    {
        public LockReleaseResponseMessageBody()
        {

        }

        public LockReleaseResponseMessageBody(MessageBody receivedMessageBody, bool lockWasReleased) 
            : base (receivedMessageBody.MasterAccessionNo, receivedMessageBody.LockAquiredByUserName, receivedMessageBody.LockAquiredByHostName, receivedMessageBody.TimeLockAquired)
        {
            this.m_LockWasReleased = LockWasReleased;

            if(lockWasReleased == true)
            {
                this.m_Message = this.m_RequestingUserName + " has released the lock on " + this.m_MasterAccessionNo;
            }
            else
            {
                this.m_Message = this.m_RequestingUserName + " says hold your horses. I'm working with " + this.m_MasterAccessionNo + ".";
            }
            
        }
    }
}
