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

        public LockReleaseRequestMessageBody(string masterAccessionNo, string requestingUserName, string requestingComputerName, DateTime timeLockAquired) 
            : base (masterAccessionNo, requestingUserName, requestingComputerName, timeLockAquired)
        {            
            this.m_Message = this.m_UserName + " would like to take " + this.m_MasterAccessionNo;
        }        
    }
}
