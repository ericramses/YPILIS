using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Billing
{
	public class CptCodeGroup : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;		

        private string m_CptCodeGroupId;
        private string m_GroupName;		

		private CptCodeGroupCodeCollection m_CptCodeGroupCodeCollection;

		public CptCodeGroup()
		{
			this.m_CptCodeGroupCodeCollection = new CptCodeGroupCodeCollection();
		}

        public string CptCodeGroupId
        {
            get { return this.m_CptCodeGroupId; }
            set
            {
                if (this.m_CptCodeGroupId != value)
                {
                    this.m_CptCodeGroupId = value;
                    this.NotifyPropertyChanged("CptCodeGroupId");
                }
            }
        }

        public string GroupName
        {
            get { return this.m_GroupName; }
            set
            {
                if (this.m_GroupName != value)
                {
                    this.m_GroupName = value;
                    this.NotifyPropertyChanged("GroupName");
                }
            }
        }							

		public CptCodeGroupCodeCollection CptCodeGroupCodeCollection
		{
			get { return this.m_CptCodeGroupCodeCollection; }
			set
			{
				this.m_CptCodeGroupCodeCollection = value;
				NotifyPropertyChanged("CptCodeGroupCodeCollection");
			}
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }		
	}
}
