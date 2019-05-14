using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
    public class EODProcessStatus
    {
        private DateTime m_ProcessDate;
        private DateTime? m_MRNAcctUpdate;
        private DateTime? m_ADTMatch;
        private DateTime? m_ProcessSVHCDMFiles;
        private DateTime? m_TransferSVHFiles;
        private DateTime? m_SendSVHClinicEmail;
        private DateTime? m_ProcessPSAFiles;
        private DateTime? m_TransferPSAFiles;
        private DateTime? m_FaxTheReport;

        public EODProcessStatus()
        { }

        [PersistentProperty()]
        public DateTime ProcessDate
        {
            get { return this.m_ProcessDate; }
            set { this.m_ProcessDate = value; }
        }

        [PersistentProperty()]
        public DateTime? MRNAcctUpdate
        {
            get { return this.m_MRNAcctUpdate; }
            set { this.m_MRNAcctUpdate = value; }
        }

        [PersistentProperty()]
        public DateTime? ADTMatch
        {
            get { return this.m_ADTMatch; }
            set { this.m_ADTMatch = value; }
        }

        [PersistentProperty()]
        public DateTime? ProcessSVHCDMFiles
        {
            get { return this.m_ProcessSVHCDMFiles; }
            set { this.m_ProcessSVHCDMFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? TransferSVHFiles
        {
            get { return this.m_TransferSVHFiles; }
            set { this.m_TransferSVHFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? SendSVHClinicEmail
        {
            get { return this.m_SendSVHClinicEmail; }
            set { this.m_SendSVHClinicEmail = value; }
        }

        [PersistentProperty()]
        public DateTime? ProcessPSAFiles
        {
            get { return this.m_ProcessPSAFiles; }
            set { this.m_ProcessPSAFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? TransferPSAFiles
        {
            get { return this.m_TransferPSAFiles; }
            set { this.m_TransferPSAFiles = value; }
        }

        [PersistentProperty()]
        public DateTime? FaxTheReport
        {
            get { return this.m_FaxTheReport; }
            set { this.m_FaxTheReport = value; }
        }
    }
}
