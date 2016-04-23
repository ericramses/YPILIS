﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Interaction logic for LockedCaseDialog.xaml
	/// </summary>
	public partial class LockedCaseDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string LockReleaseRequestQueueName = "lockreleaserequests";
        private const string LockReleaseResponseQueueName = "lockreleaseresponses";

        private YellowstonePathology.Business.Domain.LockItemCollection m_LockItemCollection;
        

        public LockedCaseDialog()
		{
            this.m_LockItemCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLockedAccessionOrders();
            InitializeComponent();            

            DataContext = this;
		}

        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }                

		private void ButtonClearLock_Click(object sender, RoutedEventArgs e)
		{            
            if(this.ListViewLockedAccessionOrders.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Clearing a lock may cause data loss.  Are you sure you want to unlock this case?", "Possible data loss", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                {
                    foreach(YellowstonePathology.Business.Domain.LockItem lockItem in this.ListViewLockedAccessionOrders.SelectedItems)
                    {
                        YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(lockItem.KeyString, this);

                        if (accessionOrder.IsLockAquiredByMe == false)
                        {
                            accessionOrder.ReleaseLock();
                            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);

                            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("support@ypii.com", "Sid.Harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, "A lock wash cleared on case: " + accessionOrder.MasterAccessionNo + " by " + YellowstonePathology.Business.User.SystemIdentity.Instance.User.DisplayName);
                            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

                            Uri uri = new Uri("http://tempuri.org/");
                            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                            client.Credentials = credential;
                            client.Send(message);
                        }
                    }
                    this.m_LockItemCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLockedAccessionOrders();
                    this.NotifyPropertyChanged(string.Empty);
                }
            }
        }

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
		{
            this.m_LockItemCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLockedAccessionOrders();
            this.NotifyPropertyChanged(string.Empty);
		}

        public YellowstonePathology.Business.Domain.LockItemCollection LockItemCollection
        {
            get { return this.m_LockItemCollection; }
        }

        public YellowstonePathology.Business.Persistence.DocumentCollection Documents
        {
            get { return YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Documents; }
        }        
    }
}
