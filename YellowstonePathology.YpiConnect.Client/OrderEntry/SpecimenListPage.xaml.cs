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
	public partial class SpecimenListPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;
		ClientOrderDetailViewCollection m_ClientOrderDetailViewCollection;
		private bool m_ShowInactiveSpecimen;

		public SpecimenListPage(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder)
		{
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderDetailViewCollection = new ClientOrderDetailViewCollection(this.m_ClientOrder.ClientOrderDetailCollection, this.m_ShowInactiveSpecimen);                                                       

            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(SpecimenListPage_Loaded);            
        }

		private void SpecimenListPage_Loaded(object sender, RoutedEventArgs e)
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

		public ClientOrderDetailViewCollection ClientOrderDetailViewCollection
		{
			get { return this.m_ClientOrderDetailViewCollection; }
		}

		private void ButtonDeleteSpecimen_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this specimen?", "Delete specimen", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.OK)
			{
				if (this.ListBoxSpecimen.SelectedItems.Count != 0)
				{
					Hyperlink hyperlink = (Hyperlink)sender;
					ClientOrderDetailView clientOrderDetailView = (ClientOrderDetailView)hyperlink.Tag;
					YellowstonePathology.Business.Rules.ExecutionMessage executionMessage = new Business.Rules.ExecutionMessage();
					this.m_ClientOrder.ClientOrderDetailCollection.ClientRequestDeleteSpecimen(clientOrderDetailView.ClientOrderDetail, this.m_ClientOrder, executionMessage);
					this.m_ClientOrderDetailViewCollection.Reload(this.m_ShowInactiveSpecimen);
					MessageBox.Show(executionMessage.Message);
				}
			}
		}

		private void ListBoxOrderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = (ListBox)sender;
			string selectedOrderType = (string)listBox.SelectedItem;
			if (selectedOrderType == "Immediate Exam (with frozen section)" || selectedOrderType == "Immediate Exam (without frozen section)")
			{
				ClientOrderDetailView clientOrderDetailView = (ClientOrderDetailView)this.ListBoxSpecimen.SelectedItem;
				if (string.IsNullOrEmpty(clientOrderDetailView.ClientOrderDetail.CallbackNumber) == true)
				{
					CallbackNumberDialog callbackNumberDialog = new CallbackNumberDialog();
					Nullable<bool> result = callbackNumberDialog.ShowDialog();
					if (result == true)
					{
						foreach (YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrder.ClientOrderDetailCollection)
						{
							if (string.IsNullOrEmpty(clientOrderDetail.CallbackNumber) == true)
							{
								clientOrderDetail.CallbackNumber = callbackNumberDialog.CallbackNumber;
							}
						}
					}
				}
			}
		}

		protected void SelectCurrentListBoxItem(object sender, KeyboardFocusChangedEventArgs e)
		{
			ListBoxItem item = (ListBoxItem)sender;
			item.IsSelected = true;
		}

		private void TextBoxSpecimenDescription_Loaded(object sender, RoutedEventArgs e)
		{
				var textbox = sender as TextBox;
				if (textbox == null) return;
				textbox.Focus();
		}           

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
        {
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
        }

		private void HyperlinkNext_Click(object sender, RoutedEventArgs e)
        {
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
			Return(this, args);
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
