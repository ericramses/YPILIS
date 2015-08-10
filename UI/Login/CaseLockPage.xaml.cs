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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for CaseLockPage.xaml
	/// </summary>
	public partial class CaseLockPage : UserControl, YellowstonePathology.Shared.Interface.IPersistPageChanges, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
                
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText = "Case Lock Page";
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.Domain.Lock m_Lock;
        private string m_PageMessage;
        

        public CaseLockPage(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, 
            YellowstonePathology.Business.Domain.Lock caseLock,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_PageNavigator = pageNavigator;			
            this.m_Lock = caseLock;
            this.m_AccessionOrder = accessionOrder;                       

			InitializeComponent();

			DataContext = this;            
		}

        public string PageMessage
        {
            get { return this.m_PageMessage; }            
        }

        public void AttemptCaseLock()
        {
            this.m_Lock.SetLockingMode(Business.Domain.LockModeEnum.AlwaysAttemptLock);
            this.m_Lock.SetLockable(this.m_AccessionOrder);

            if (this.m_Lock.LockAquired == false)
            {
                this.m_PageMessage = this.m_Lock.LockMessage;
                this.NotifyPropertyChanged("PageMessage");
                this.m_PageNavigator.Navigate(this);
            }
            else
            {
                this.Return(this, new UI.Navigation.PageNavigationReturnEventArgs());
            }
        }

        public YellowstonePathology.Business.Domain.Lock Lock
        {
            get { return this.m_Lock; }
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			Window.GetWindow(this).Close();
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void Save()
		{
		
		}

		public void UpdateBindingSources()
		{
		}
	}
}
