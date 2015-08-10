using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	public partial class FlowComment : YellowstonePathology.Business.Domain.Persistence.ITrackable, INotifyPropertyChanged,
		YellowstonePathology.Business.Domain.Persistence.INotifyDBPropertyChanged, YellowstonePathology.Business.Domain.Persistence.IPropertyWritable,
		YellowstonePathology.Business.Domain.Persistence.IPropertyReadable, YellowstonePathology.Business.Domain.Persistence.IPersistable
    {
		protected delegate void PropertyChangedNotificationHandler(String info);
		protected delegate void DBPropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;
		public event YellowstonePathology.Business.Domain.Persistence.DBPropertyChangedEventHandler DBPropertyChanged;

		private YellowstonePathology.Business.Domain.Persistence.TrackingStateEnum m_TrackingState;

		public FlowComment()
        {
        }

		[DataMember]
		public YellowstonePathology.Business.Domain.Persistence.TrackingStateEnum TrackingState
		{
			get { return this.m_TrackingState; }
			set { this.m_TrackingState = value; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public void NotifyDBPropertyChanged(String info)
		{
			if (DBPropertyChanged != null)
			{
				DBPropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
				
