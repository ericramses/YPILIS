using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
    public class ClientServicesUI: INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private System.Windows.Visibility m_SignOutButtonVisibility;
        private string m_DisplayName;

        public ClientServicesUI()
        {
            this.m_SignOutButtonVisibility = System.Windows.Visibility.Collapsed;
			this.m_DisplayName = string.Empty;            
        }

        public string DisplayName
        {
            get { return this.m_DisplayName; }
            set
            {
                if (this.m_DisplayName != value)
                {
                    this.m_DisplayName = value;
                    this.NotifyPropertyChanged("DisplayName");
                }
            }
        }

        public System.Windows.Visibility SignOutButtonVisibility
        {
            get { return this.m_SignOutButtonVisibility; }
            set
            {
                if (this.m_SignOutButtonVisibility != value)
                {
                    this.m_SignOutButtonVisibility = value;
                    this.NotifyPropertyChanged("SignOutButtonVisibility");
                }
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
