using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI.Billing
{
	public class AutomationListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private string m_ReportNo;
		private Nullable<DateTime> m_PostDate;
		private int m_PanelSetId;
		private string m_ClientName;
        private int m_ClientId;        
		private string m_PanelSetName;        

        public AutomationListItem()
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
        public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if (value != this.m_ReportNo)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

        [PersistentProperty()]
        public Nullable<DateTime> PostDate
		{
			get { return this.m_PostDate; }
			set
			{
				if (value != this.m_PostDate)
				{
					this.m_PostDate = value;
					this.NotifyPropertyChanged("PostDate");
				}
			}
		}

        [PersistentProperty()]
        public int PanelSetId
		{
			get { return this.m_PanelSetId; }
			set
			{
				if (value != this.m_PanelSetId)
				{
					this.m_PanelSetId = value;
					this.NotifyPropertyChanged("PanelSetId");
				}
			}
		}        

        [PersistentProperty()]
        public string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if (value != this.m_ClientName)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
				}
			}
		}
        
        [PersistentProperty()]
        public string PanelSetName
		{
			get { return this.m_PanelSetName; }
			set
			{
				if (value != this.m_PanelSetName)
				{
					this.m_PanelSetName = value;
					this.NotifyPropertyChanged("PanelSetName");
				}
			}
		}
    }
}
