using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Panel.Model
{
	[PersistentClass("tblPanelOrderBatch", "YPIDATA")]
	public class PanelOrderBatch : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private int m_PanelOrderBatchId;
		private System.Nullable<DateTime> m_BatchDate;
		private System.Nullable<DateTime> m_RunDate;
		private string m_Description;
		private int m_BatchTypeId;

		public PanelOrderBatch()
        {
		}

		public PanelOrderBatch(string objectId)
		{
			this.m_ObjectId = objectId;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

		[PersistentPrimaryKeyProperty(true)]
		public int PanelOrderBatchId
		{
			get { return this.m_PanelOrderBatchId; }
			set
			{
				if (this.m_PanelOrderBatchId != value)
				{
					this.m_PanelOrderBatchId = value;
					NotifyPropertyChanged("PanelOrderBatchId");
				}
			}
		}

		[PersistentProperty()]
		public System.Nullable<DateTime> BatchDate
		{
			get { return this.m_BatchDate; }
			set
			{
				if (this.m_BatchDate != value)
				{
					this.m_BatchDate = value;
					NotifyPropertyChanged("BatchDate");
				}
			}
		}

		[PersistentProperty()]
		public System.Nullable<DateTime> RunDate
		{
			get { return this.m_RunDate; }
			set
			{
				if (this.m_RunDate != value)
				{
					this.m_RunDate = value;
					NotifyPropertyChanged("RunDate");
				}
			}
		}

		[PersistentProperty()]
		public int BatchTypeId
		{
			get { return this.m_BatchTypeId; }
			set
			{
				if (this.m_BatchTypeId != value)
				{
					this.m_BatchTypeId = value;
					NotifyPropertyChanged("BatchTypeId");
				}
			}
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
				{
					this.m_Description = value;
					NotifyPropertyChanged("Description");
				}
			}
		}

	}
}
