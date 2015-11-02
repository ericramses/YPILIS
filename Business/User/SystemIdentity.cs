using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.User
{
    public class SystemIdentity : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        public event UserChangedHandler UserChanged;
        public delegate void UserChangedHandler();        

        private SystemUser m_User;
        private bool m_IsKnown;
        private string m_StationName;

		YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        public SystemIdentity(SystemIdentityTypeEnum systemIdentityType)
        {                        			
			switch (systemIdentityType)
            {
                case SystemIdentityTypeEnum.CurrentlyLoggedIn:
                    string userName = System.Windows.Forms.SystemInformation.UserName;
					this.m_User = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserByUserName(userName);
                    this.m_IsKnown = true;
                    break;
                case SystemIdentityTypeEnum.CurrentlyScannedIn:
                    this.m_User = this.GetBlankUser();
                    this.m_IsKnown = false;
					this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
                    this.m_BarcodeScanPort.SecurityBadgeScanReceived += SecurityBadgeScanReceived;
                    break;
				case SystemIdentityTypeEnum.Administrator:
					this.m_User = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserByUserName("Administrator");
					this.m_IsKnown = true;
					break;
				case SystemIdentityTypeEnum.CurrentlySelected:                    
                case SystemIdentityTypeEnum.Blank:
                    this.m_User = this.GetBlankUser();
                    this.m_IsKnown = false;
                    break;
            }
            this.SetStationName();
        }

		public YellowstonePathology.Business.User.SystemUser User
        {
            get { return this.m_User; }            
        }

		public void SetSelectedUser(YellowstonePathology.Business.User.SystemUser systemUser)
        {
            this.m_User = systemUser;
            this.m_IsKnown = true;
			
            if (this.UserChanged != null)
            {
                this.UserChanged();
            }
        }

        public bool IsKnown
        {
            get { return this.m_IsKnown; }
        }

        public string StationName
        {
            get { return this.m_StationName; }
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

        private void SetStationName()
        {
            string result = System.Windows.Forms.SystemInformation.ComputerName.ToUpper();
            switch (result)
            {                
                case "CUTTINGA":
                case "CUTTING1":
                    result = "CAPTAIN";
                    break;
                case "CUTTINGB":
                case "CUTTING2":
                    result = "TENNILE";
                    break;                
            }
            this.m_StationName = result;
        }		

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		public static bool DoesLoggedInUserNeedToScanId()
		{
            bool result = false;

            string userName = System.Windows.Forms.SystemInformation.UserName.ToUpper();
			switch (userName)
			{
				case "NOT ASSIGNED": //0
				case "HISTOLOGY":    //5094
				case "HISTOLOGYA":   //5096
				case "HISTOLOGYB":   //5097
				case "HISTOLOGYC":   //5117
				case "CODYHISTOLOGY": //5077
                    result = true;
					break;
				default:
					result = false;
					break;
			}
            return result;
		}
    }
}
