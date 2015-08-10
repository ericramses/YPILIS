using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.Domain.Persistence
{
    [DataContract]
    public class SubmitChangesResult
    {
        private bool m_Tracking;
        private int m_ObjectsProcessed;

        public SubmitChangesResult()
        {

        }

        [DataMember]
        public bool Tracking
        {
            get { return this.m_Tracking; }
            set { this.m_Tracking = value; }
        }

        [DataMember]
        public int OjbectsProcessed
        {
            get { return this.m_ObjectsProcessed; }
            set { this.m_ObjectsProcessed = value; }
        }
    }
}
