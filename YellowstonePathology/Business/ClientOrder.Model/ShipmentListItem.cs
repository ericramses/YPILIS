using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public class ShipmentListItem
	{
		#region Fields
		private string m_ShipmentId;
		private string m_ShipmentFrom;
		private string m_ShipmentTo;
		private Nullable<DateTime> m_ShipDate;
		private bool m_Shipped;
		private bool m_Received;
		private Nullable<DateTime> m_ReceivedDate;
		private int m_ReceivedById;
		private string m_ShipmentPreparedBy;
		#endregion

		public ShipmentListItem() {}

		#region Properties
		[DataMember]
        [PersistentPrimaryKeyProperty(false)]
		public string ShipmentId
		{
			get { return this.m_ShipmentId; }
			set { this.m_ShipmentId = value; }
		}

		[DataMember]
        [PersistentProperty()]
		public string ShipmentFrom
		{
			get { return this.m_ShipmentFrom; }
			set { this.m_ShipmentFrom = value; }
		}

		[DataMember]
        [PersistentProperty()]
        public string ShipmentTo
		{
			get { return this.m_ShipmentTo; }
			set { this.m_ShipmentTo = value; }
		}

		[DataMember]
        [PersistentProperty()]
        public Nullable<DateTime> ShipDate
		{
			get { return this.m_ShipDate; }
			set { this.m_ShipDate = value; }
		}

		[DataMember]
        [PersistentProperty()]
        public bool Shipped
		{
			get { return this.m_Shipped; }
			set { this.m_Shipped = value; }
		}

		[DataMember]
        [PersistentProperty()]
        public bool Received
		{
			get { return this.m_Received; }
			set { this.m_Received = value; }
		}

		[DataMember]
        [PersistentProperty()]
        public Nullable<DateTime> ReceivedDate
		{
			get { return this.m_ReceivedDate; }
			set { this.m_ReceivedDate = value; }
		}

		[DataMember]
        [PersistentProperty()]
        public int ReceivedById
		{
			get { return this.m_ReceivedById; }
			set { this.m_ReceivedById = value; }
		}

		[DataMember]
        [PersistentProperty()]
        public string ShipmentPreparedBy
		{
			get { return this.m_ShipmentPreparedBy; }
			set { this.m_ShipmentPreparedBy = value; }
		}
		#endregion
	}
}
