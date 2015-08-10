using System;
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

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Interaction logic for LockedCaseDialog.xaml
	/// </summary>
	public partial class LockedCaseDialog : Window
	{
		private YellowstonePathology.Business.Domain.LockItemCollection m_LockItemCollection;

		public LockedCaseDialog()
		{
			this.FillLockItemCollection();
			InitializeComponent();
		}

		private void FillLockItemCollection()
		{
			this.m_LockItemCollection = YellowstonePathology.Business.Gateway.LockGateway.GetLocks();
			if (this.m_LockItemCollection == null)
			{
				this.m_LockItemCollection = new Business.Domain.LockItemCollection();
			}
			this.DataContext = this.m_LockItemCollection;
		}

		private void ButtonDelete_Click(object sender, RoutedEventArgs e)
		{
			if (this.listViewLockList.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Clearing a lock may cause data loss.  Are you sure you want to unlock this case?", "Possible data loss", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
				if (result == MessageBoxResult.Yes)
				{
					YellowstonePathology.Business.Domain.LockItem lockItem = (YellowstonePathology.Business.Domain.LockItem)this.listViewLockList.SelectedItem;
					YellowstonePathology.Business.User.SystemUser systemUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserByUserName(lockItem.LockedBy);
					YellowstonePathology.Business.Domain.KeyLock keyLock = new Business.Domain.KeyLock();
					keyLock.Key = lockItem.KeyString;
					YellowstonePathology.Business.Gateway.LockGateway.ReleaseLock(keyLock, systemUser);
					this.FillLockItemCollection();
				}
			}
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
		{
			this.FillLockItemCollection();
		}
	}
}
