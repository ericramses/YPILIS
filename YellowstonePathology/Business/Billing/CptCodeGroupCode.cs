using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Billing
{
	public class CptCodeGroupCode : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;		

        private string m_CptCodeGroupCodeId;
        private string m_CptCodeGroupId;
        private string m_CptCode;	

		public CptCodeGroupCode()
		{
		}

        public string CptCodeGroupCodeId
        {
            get { return this.m_CptCodeGroupCodeId; }
            set
            {
                if (this.m_CptCodeGroupCodeId != value)
                {
                    this.m_CptCodeGroupCodeId = value;
                    this.NotifyPropertyChanged("CptCodeGroupCodeId");
                }
            }
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

        public string CptCode
        {
            get { return this.m_CptCode; }
            set
            {
                if (this.m_CptCode != value)
                {
                    this.m_CptCode = value;
                    this.NotifyPropertyChanged("CptCode");                    
                }
            }
        }		

		public CptCodeGroupCode(string cptCodeGroupId, string cptCode)
		{
			this.CptCodeGroupCodeId = Guid.NewGuid().ToString();
			this.CptCode = cptCode;
			this.CptCodeGroupId = cptCodeGroupId;
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
