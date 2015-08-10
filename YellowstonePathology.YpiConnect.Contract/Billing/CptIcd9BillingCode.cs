using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
	[DataContract]
	public class CptIcd9BillingCode : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		private int m_CptIcd9BillingCodeId;
		private string m_MasterAccessionNo;
		private string m_CptBillingCodeId;
		private string m_Icd9BillingCodeId;

        public CptIcd9BillingCode()
        {

        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		[DataMember]
		[PersistentProperty()]
		public int CptIcd9BillingCodeId
		{
			get { return this.m_CptIcd9BillingCodeId; }
			set
			{
				if (this.m_CptIcd9BillingCodeId != value)
				{
					this.m_CptIcd9BillingCodeId = value;
					this.NotifyPropertyChanged("CptIcd9BillingCodeId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if (this.m_MasterAccessionNo != value)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string CptBillingCodeId
		{
			get { return this.m_CptBillingCodeId; }
			set
			{
				if (this.m_CptBillingCodeId != value)
				{
					this.m_CptBillingCodeId = value;
					this.NotifyPropertyChanged("CptBillingCodeId");
				}
			}
		}

		[DataMember]
		public string Icd9BillingCodeId
		{
			get { return this.m_Icd9BillingCodeId; }
			set
			{
				if (this.m_Icd9BillingCodeId != value)
				{
					this.m_Icd9BillingCodeId = value;
					this.NotifyPropertyChanged("Icd9BillingCodeId");
				}
			}
		}
	}
}
