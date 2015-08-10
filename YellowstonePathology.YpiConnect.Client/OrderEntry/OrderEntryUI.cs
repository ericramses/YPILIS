using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Controls;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    public class OrderEntryUI : INotifyPropertyChanged
    {        
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy m_ClientOrderServiceProxy;        
        private YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection m_OrderBrowserListItemCollection;

        public OrderEntryUI()
        {                        
            this.m_ClientOrderServiceProxy = new YpiConnect.Proxy.ClientOrderServiceProxy();
            this.m_OrderBrowserListItemCollection = this.m_ClientOrderServiceProxy.GetRecentOrderBrowserListItemsByClientId(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.PrimaryClientId);            
        }

        public void RefreshSearch()
        {
            this.m_OrderBrowserListItemCollection = this.m_ClientOrderServiceProxy.GetRecentOrderBrowserListItemsByClientId(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.PrimaryClientId);            
		}

        public YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection OrderBrowserListItemCollection
        {
            get { return this.m_OrderBrowserListItemCollection; }
            set
            {
                if (this.m_OrderBrowserListItemCollection != value)
                {
                    this.m_OrderBrowserListItemCollection = value;
                    this.NotifyPropertyChanged("OrderBrowserListItemCollection");
                }
            }
        }               

		public YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy ClientOrderServiceProxy
		{
			get { return this.m_ClientOrderServiceProxy; }
		}

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
