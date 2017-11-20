using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    public class EmbeddingAutopsyItem
    {
        private string m_AliquotOrderId;
        private Nullable<DateTime> m_ProcessorStartTime;
        private Nullable<TimeSpan> m_ProcessorFixationDuration;
        private DateTime? m_DateScanned;
        private string m_ScannedBy;
        private int? m_ScannedById;
        private bool m_Updated;

        public EmbeddingAutopsyItem()
        {

        }

        public EmbeddingAutopsyItem(string aliquotOrderId)
        {
            this.m_AliquotOrderId = aliquotOrderId;
        }

        public EmbeddingAutopsyItem(string aliquotOrderId,
            Nullable<DateTime> processorStartTime,
            Nullable<TimeSpan> processorFixationDuration,
            DateTime? dateScanned,
            string scannedBy,
            int? scannedById,
            bool updated)
        {
            this.m_AliquotOrderId = aliquotOrderId;
            this.m_ProcessorStartTime = processorStartTime;
            this.m_ProcessorFixationDuration = processorFixationDuration;
            this.m_DateScanned = dateScanned;
            this.m_ScannedBy = scannedBy;
            this.m_ScannedById = scannedById;
            this.m_Updated = updated;
        }

        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
        }

        public string ScannedBy
        {
            get { return this.m_ScannedBy; }
        }

        public int? ScannedById
        {
            get { return this.m_ScannedById; }
        }

        public DateTime? DateScanned
        {
            get { return this.m_DateScanned; }
        }

        public Nullable<DateTime> ProcessorStartTime
        {
            get { return this.m_ProcessorStartTime; }
        }

        public Nullable<TimeSpan> ProcessorFixationDuration
        {
            get { return this.m_ProcessorFixationDuration; }
        }

        public bool Updated
        {
            get { return this.m_Updated; }
            set
            {
                if (this.m_Updated != value)
                {
                    this.m_Updated = true;
                }
            }
        }
    }
}
