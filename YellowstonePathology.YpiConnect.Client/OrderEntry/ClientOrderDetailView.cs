using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    public class ClientOrderDetailView : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;
        private System.Windows.Media.SolidColorBrush m_BackgroundColor;
        
        public ClientOrderDetailView(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
        {
            this.m_ClientOrderDetail = clientOrderDetail;
            this.m_BackgroundColor = System.Windows.Media.Brushes.Aqua;
        }        

        System.Windows.Media.SolidColorBrush BackgroundColor
        {
            get { return this.m_BackgroundColor; }
            set
            {
                if (this.m_BackgroundColor != value)
                {
                    this.m_BackgroundColor = value;
                    this.NotifyPropertyChanged("BackgroundColor");
                }
            }
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetail
        {
            get { return this.m_ClientOrderDetail; }
            set 
            { 
                this.m_ClientOrderDetail = value;
                this.NotifyPropertyChanged("ClientOrderDetail");
            }
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
