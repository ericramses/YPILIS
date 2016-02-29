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

        public LockReleaseResponseMessageBody(string masterAccessionNo, string respondingUserName, string respondingComputerName, DateTime timeLockAquired) 
            : base (masterAccessionNo, respondingUserName, respondingComputerName, timeLockAquired)
        {
            this.m_Message = this.m_UserName + " released the lock on " + this.m_MasterAccessionNo;
        }
    }
}
