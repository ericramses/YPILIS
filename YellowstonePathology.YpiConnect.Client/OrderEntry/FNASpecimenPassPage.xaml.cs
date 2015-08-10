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
	public partial class FNASpecimenPassPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;
		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty m_ClientOrderFNAProperty;
		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertyCollection m_ClientOrderFNAPropertyCollection;
		private ClientOrderDetailFNAPropertyViewCollection m_ClientOrderDetailFNAPropertyViewCollection;

		public FNASpecimenPassPage(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty clientOrderFNAProperty, YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertyCollection clientOrderFNAPropertyCollection)
		{
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderFNAProperty = clientOrderFNAProperty;
			this.m_ClientOrderFNAPropertyCollection = clientOrderFNAPropertyCollection;
			this.AddClientOrderDetailFNAProperty();
			this.m_ClientOrderDetailFNAPropertyViewCollection = new ClientOrderDetailFNAPropertyViewCollection();
			InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(FNASpecimenListPage_Loaded);
		}

		private void FNASpecimenListPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
			this.m_ClientOrderDetailFNAPropertyViewCollection.Refresh(this.m_ClientOrder, this.m_ClientOrderFNAProperty);
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string SpecimenSource
		{
			get { return this.m_ClientOrderFNAProperty.SpecimenSource; }
		}

		public ClientOrderDetailFNAPropertyView ClientOrderDetailFNAPropertyView
		{
			get { return this.m_ClientOrderDetailFNAPropertyViewCollection[this.m_ClientOrderDetailFNAPropertyViewCollection.Count - 1]; }
		}

		private void AddClientOrderDetailFNAProperty()
		{
			if (this.m_ClientOrderFNAProperty.ClientOrderDetailFNAPropertyCollection.Exists(string.Empty) == false)
			{
				YellowstonePathology.YpiConnect.Contract.Order.ClientOrderDetailFNAProperty clientOrderDetailFNAProperty = new Contract.Order.ClientOrderDetailFNAProperty();
				clientOrderDetailFNAProperty.ClientOrderDetailFNAPropertyId = Guid.NewGuid().ToString();
				clientOrderDetailFNAProperty.ClientOrderFNAPropertyId = this.m_ClientOrderFNAProperty.ClientOrderFNAPropertyId;
				int passNo = this.m_ClientOrderFNAProperty.ClientOrderDetailFNAPropertyCollection.Count + 1;
				clientOrderDetailFNAProperty.PassNo = passNo;
				this.m_ClientOrderFNAProperty.ClientOrderDetailFNAPropertyCollection.Add(clientOrderDetailFNAProperty);
			}
		}

		/*private void HyperlinkAddPass_Click(object sender, RoutedEventArgs e)
		{
			this.AddClientOrderDetailFNAProperty();
			int idx = this.m_ClientOrderFNAProperty.ClientOrderDetailFNAPropertyCollection.Count - 1;
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, this.m_ClientOrderFNAProperty.ClientOrderDetailFNAPropertyCollection[idx]);
			Return(this, args);
		}*/

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

		/*private void HyperlinkNewSpecimen_Click(object sender, RoutedEventArgs e)
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Command, PageNavigationCommandEnum.AddSpecimen);
			Return(this, args);
		}*/

		private void HyperlinkFinish_Click(object sender, RoutedEventArgs e)
		{
				Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Command, PageNavigationCommandEnum.Finish);
				Return(this, args);
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
            /*
			YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy clientOrderServiceProxy = new Proxy.ClientOrderServiceProxy();
			YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertySubmitter submitter = new Contract.Order.ClientOrderFNAPropertySubmitter(this.m_ClientOrderFNAPropertyCollection);

			if (submitter.HasChanges())
			{
				Mouse.OverrideCursor = Cursors.Wait;
				submitter.BeginSubmit(Business.Domain.Persistence.PropertyReaderFilterEnum.Normal);
				YellowstonePathology.Business.Rules.MethodResult methodResult = clientOrderServiceProxy.SubmitChanges(submitter);
				if (methodResult.Success == false)
				{
					System.Windows.MessageBox.Show(methodResult.Message);
				}
				else
				{
					submitter.EndSubmit();
				}
				System.Threading.Thread.Sleep(500);
				Mouse.OverrideCursor = null;
			}
            */
		}

		public void UpdateBindingSources()
		{
		}
	}
}
