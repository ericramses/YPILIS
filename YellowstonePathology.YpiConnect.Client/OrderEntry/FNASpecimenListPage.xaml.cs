using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for OrderEntryWindow.xaml
    /// </summary>
	public partial class FNASpecimenListPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertyCollection m_ClientOrderFNAPropertyCollection;

		public FNASpecimenListPage(YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertyCollection clientOrderFNAPropertyCollection)
		{
			this.m_ClientOrderFNAPropertyCollection = clientOrderFNAPropertyCollection;

			InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(FNASpecimenListPage_Loaded);
		}

		private void FNASpecimenListPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertyCollection ClientOrderFNAPropertyCollection
		{
			get { return this.m_ClientOrderFNAPropertyCollection; }
		}

		private void HyperlinkNewSpecimen_Click(object sender, RoutedEventArgs e)
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Command, null);
			Return(this, args);
		}

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

		private void HyperlinkNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxClientOrderFNAProperties.SelectedItem != null)
			{
				Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, (YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty)this.ListBoxClientOrderFNAProperties.SelectedItem);
				Return(this, args);
			}
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
