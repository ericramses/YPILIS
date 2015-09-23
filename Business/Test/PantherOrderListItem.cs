﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    public class PantherOrderListItem
    {
        private string m_ReportNo;
        private string m_PanelSetName;
        private DateTime m_OrderTime;
        private string m_PLastName;
        private string m_PFirstName;
        private string m_ResultCode;
        private Nullable<DateTime> m_AcceptedTime;
        private Nullable<DateTime> m_FinalTime;

        public PantherOrderListItem()
        {

        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        [PersistentProperty()]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set { this.m_PanelSetName = value; }
        }

        [PersistentProperty()]
        public DateTime OrderTime
        {
            get { return m_OrderTime; }
            set { this.m_OrderTime = value; }
        }

        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set { this.m_PLastName = value; }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set { this.m_PFirstName = value; }
        }

        [PersistentProperty()]
        public string ResultCode
        {
            get { return this.m_ResultCode; }
            set { this.m_ResultCode = value; }
        }

        [PersistentProperty()]
        public Nullable<DateTime> AcceptedTime
        {
            get { return m_AcceptedTime; }
            set { this.m_AcceptedTime = value; }
        }

        [PersistentProperty()]
        public Nullable<DateTime> FinalTime
        {
            get { return m_FinalTime; }
            set { this.m_FinalTime = value; }
        }
    }
}