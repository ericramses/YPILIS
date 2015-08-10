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
	public partial class FNASpecimenPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;
		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty m_ClientOrderFNAProperty;
		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertyCollection m_ClientOrderFNAPropertyCollection;

		public FNASpecimenPage(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertyCollection clientOrderFNAPropertyCollection)
		{
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderFNAPropertyCollection = clientOrderFNAPropertyCollection;
			this.CreateClientOrderFNAProperty();
            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(FNASpecimenPage_Loaded);            
        }

		private void FNASpecimenPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.TextBoxSpecimenDescription.Focus();
        }

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		private void CreateClientOrderFNAProperty()
		{
			if (this.m_ClientOrderFNAPropertyCollection.Count == 0)
			{
				this.m_ClientOrderFNAProperty = new Contract.Order.ClientOrderFNAProperty();
				this.m_ClientOrderFNAProperty.ClientOrderFNAPropertyId = Guid.NewGuid().ToString();
				this.m_ClientOrderFNAProperty.ClientOrderId = this.m_ClientOrder.ClientOrderId;
			}
			else
			{
				this.m_ClientOrderFNAProperty = this.m_ClientOrderFNAPropertyCollection[0];
			}
		}

		private void AddClientOrderFNAPropertyToCollection()
		{
			if (this.m_ClientOrderFNAPropertyCollection.Exists(this.ClientOrderFNAProperty.ClientOrderFNAPropertyId) == false)
			{
				this.m_ClientOrderFNAPropertyCollection.Add(this.m_ClientOrderFNAProperty);
			}
		}

		public YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty ClientOrderFNAProperty
		{
			get { return this.m_ClientOrderFNAProperty; }
		}

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
        {
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
        }

		private void HyperlinkNext_Click(object sender, RoutedEventArgs e)
        {
			this.AddClientOrderFNAPropertyToCollection();
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
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

		private void ButtonAddPass_Click(object sender, RoutedEventArgs e)
		{
			this.AddClientOrderFNAPropertyToCollection();
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Command, PageNavigationCommandEnum.AddPass);
			Return(this, args);

			/*
			YellowstonePathology.YpiConnect.Contract.Order.ClientOrderDetailFNAProperty clientOrderDetailFNAProperty = new Contract.Order.ClientOrderDetailFNAProperty();
			clientOrderDetailFNAProperty.ClientOrderDetailFNAPropertyId = Guid.NewGuid().ToString();
			clientOrderDetailFNAProperty.ClientOrderFNAPropertyId = this.m_ClientOrderFNAProperty.ClientOrderFNAPropertyId;
			this.m_ClientOrderFNAProperty.ClientOrderDetailFNAPropertyCollection.Add(clientOrderDetailFNAProperty);
			 * */
		}

		public void UpdateBindingSources()
		{
		}
	}
}
