using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain
{
	[XmlType("LockItem")]
	public class LockItem
	{
		private string m_KeyString;
		private DateTime m_LockDate;
		private string m_LockedBy;
        private string m_ComputerName;

		public LockItem()
		{
		}

		public string KeyString
		{
			get { return this.m_KeyString; }
			set { this.m_KeyString = value; }
		}

		public DateTime LockDate
		{
			get { return this.m_LockDate; }
			set { this.m_LockDate = value; }
		}

		public string LockedBy
		{
			get { return this.m_LockedBy; }
			set { this.m_LockedBy = value; }
		}

        public string ComputerName
        {
            get { return this.m_ComputerName; }
            set { this.m_ComputerName = value; }
        }
	}
}
