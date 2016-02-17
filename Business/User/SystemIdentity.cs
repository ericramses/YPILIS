using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.User
{
    public class SystemIdentity : INotifyPropertyChanged
    {
        private static volatile SystemIdentity instance;
        private static object syncRoot = new Object();        

        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        public event UserChangedHandler UserChanged;
        public delegate void UserChangedHandler();        

        private SystemUser m_User;
        private bool m_IsKnown;        

		YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        static SystemIdentity()
        {
            
        }

        private SystemIdentity()
        {            
            this.m_User = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserByUserName(System.Windows.Forms.SystemInformation.UserName);            
            this.m_IsKnown = true;
        }

        public static SystemIdentity Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SystemIdentity();
                    }
                }

                return instance;
            }
        }       

		public YellowstonePathology.Business.User.SystemUser User
        {
            get { return this.m_User; }            
        }        

        public bool IsKnown
        {
            get { return this.m_IsKnown; }
        }        	
        
        public void Clear()        
        {
            this.m_IsKnown = false;
            this.m_User = this.GetBlankUser();
            this.NotifyPropertyChanged("");
        }
        
        public void SetUser(int userId)
        {
			this.m_User = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(userId);
            this.m_IsKnown = true;
            this.NotifyPropertyChanged("");

            if (this.UserChanged != null)
            {
                this.UserChanged();
            }
		} 
        
        private SystemUser GetBlankUser()
        {
            SystemUser blankUser = new SystemUser();
            blankUser.UserName = "None";
            blankUser.DisplayName = "None";
            blankUser.UserId = 0;
            return blankUser;
        }

		public void SecurityBadgeScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            int scannedUserId = Convert.ToInt32(barcode.ID);
            if (this.m_User.UserId == scannedUserId)
            {
                this.m_User = this.GetBlankUser();
                this.m_IsKnown = false;							
			}
            else
            {
                this.SetUser(scannedUserId);
            } 
           
            if (this.UserChanged != null) this.UserChanged();
            this.NotifyPropertyChanged("");
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
