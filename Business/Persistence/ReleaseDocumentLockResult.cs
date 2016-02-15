using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class ReleaseDocumentLockResult
    {
        private bool m_LockWasReleased;
        private string m_Message;

        public ReleaseDocumentLockResult()
        {
            
        }

        public bool LockWasReleased
        {
            get { return this.m_LockWasReleased; }
            set { this.LockWasReleased = value; }
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.Message = value; }
        }

    }
}
