using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
    [DataContract]
	public class BillingDetail : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeCollection m_PanelSetOrderCPTCodeCollection;
		private YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeBillCollection m_PanelSetOrderCPTCodeBillCollection;
		private YellowstonePathology.YpiConnect.Contract.Billing.ICD9BillingCodeCollection m_ICD9BillingCodeCollection;
		private RemoteFileList m_RemoteFileList;

		public BillingDetail()
		{
			this.m_PanelSetOrderCPTCodeCollection = new YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeCollection();
			this.m_PanelSetOrderCPTCodeBillCollection = new YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeBillCollection();
            this.m_ICD9BillingCodeCollection = new YellowstonePathology.YpiConnect.Contract.Billing.ICD9BillingCodeCollection();
            this.m_RemoteFileList = new RemoteFileList();
		}

		[DataMember]
		public YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeCollection PanelSetOrderCPTCodeCollection
		{
			get { return this.m_PanelSetOrderCPTCodeCollection; }
			set { this.m_PanelSetOrderCPTCodeCollection = value; }
		}

		[DataMember]
		public YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeBillCollection PanelSetOrderCPTCodeBillCollection
		{
			get { return this.m_PanelSetOrderCPTCodeBillCollection; }
			set { this.m_PanelSetOrderCPTCodeBillCollection = value; }
		}

		[DataMember]
		public YellowstonePathology.YpiConnect.Contract.Billing.ICD9BillingCodeCollection ICD9BillingCodeCollection
		{
			get { return this.m_ICD9BillingCodeCollection; }
			set { this.m_ICD9BillingCodeCollection = value; }
		}

		[DataMember]
		public YellowstonePathology.YpiConnect.Contract.RemoteFileList RemoteFileList
		{
			get { return this.m_RemoteFileList; }
			set { this.m_RemoteFileList = value; }
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
