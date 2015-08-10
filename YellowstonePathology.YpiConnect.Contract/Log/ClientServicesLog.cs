using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract.Log
{
    public class ClientServicesLog
    {
        int m_ClientServicesLogId;
        int m_EventId;
        DateTime m_LogDate;
        string m_LoggedBy;
        string m_Description;
        string m_Details;
        string m_IpAddress;

        public ClientServicesLog()
        {
        
        }

        public void SetDefaults()
        {
            this.m_LogDate = DateTime.Now;
        }

        public int ClientServicesLogId
        {
            get { return this.m_ClientServicesLogId; }
            set { this.m_ClientServicesLogId = value; }
        }

        public int EventId
        {
            get { return this.m_EventId; }
            set { this.m_EventId = value; }
        }

        public DateTime LogDate
        {
            get { return this.m_LogDate; }
            set { this.m_LogDate = value; }
        }

        public string LoggedBy
        {
            get { return this.m_LoggedBy; }
            set { this.m_LoggedBy = value; }
        }              

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public string Details
        {
            get { return this.m_Details; }
            set { this.m_Details = value; }
        }

        public string IpAddress
        {
            get { return this.m_IpAddress; }
            set { this.m_IpAddress = value; }
        }
    }
}
