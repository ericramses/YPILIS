using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business
{
	public class BatchTypeListItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_BatchTypeId;
		private string m_BatchTypeDescription;
		private int m_DisplaySequence;
		private string m_BatchIndicator;

		public BatchTypeListItem()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
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
		public string BatchTypeDescription
		{
			get { return this.m_BatchTypeDescription; }
			set
			{
				if (this.m_BatchTypeDescription != value)
				{
					this.m_BatchTypeDescription = value;
					NotifyPropertyChanged("BatchTypeDescription");
				}
			}
		}

		[PersistentProperty()]
		public int DisplaySequence
		{
			get { return this.m_DisplaySequence; }
			set
			{
				if (this.m_DisplaySequence != value)
				{
					this.m_DisplaySequence = value;
					NotifyPropertyChanged("DisplaySequence");
				}
			}
		}

		[PersistentProperty()]
		public string BatchIndicator
		{
			get { return this.m_BatchIndicator; }
			set
			{
				if (this.m_BatchIndicator != value)
				{
					this.m_BatchIndicator = value;
					NotifyPropertyChanged("BatchIndicator");
				}
			}
		}
	}
}
