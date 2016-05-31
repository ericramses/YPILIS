using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Billing
{
    public class BillingChangeResult
    {
        bool m_CompletedSuccessfully;
        string m_Message;

        public BillingChangeResult()
        {

        }

        public bool CompletedSuccessfully
        {
            get { return this.m_CompletedSuccessfully; }
            set { this.m_CompletedSuccessfully = value; }
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }
    }
}
