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

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for OrderBrowserPage.xaml
    /// </summary>
	public partial class OrderBrowserPage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        private OrderEntryUI m_OrderEntryUI;		

        public OrderBrowserPage()
        {                        
            this.m_OrderEntryUI = new OrderEntryUI();            

            InitializeComponent();         
            this.DataContext = this.m_OrderEntryUI;

			Loaded += new RoutedEventHandler(OrderBrowserPage_Loaded);
        }

		private void OrderBrowserPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}                                                   

        private void ListViewOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewOrders.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = (YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem)this.ListViewOrders.SelectedItem;
                this.HandleOrderDetailRequested(orderBrowserListItem);
            }
        }        

		private void HyperlinkRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_OrderEntryUI.RefreshSearch();
			if (ListViewOrders.Items.Count > 0)
			{
				ListViewOrders.ScrollIntoView(ListViewOrders.Items[0]);
			}
		}        
        
        private void ListViewOrders_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.ListViewOrders.SelectedItems.Count != 0)
                {
                    YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = (YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem)this.ListViewOrders.SelectedItem;
                    this.HandleOrderDetailRequested(orderBrowserListItem);
                }
            }
        }

        private void HandleOrderDetailRequested(YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem)
        {            
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = this.m_OrderEntryUI.ClientOrderServiceProxy.GetClientOrderByClientOrderId(orderBrowserListItem.ClientOrderId);
            if (clientOrder.PanelSetId.HasValue == false)
            {                
                if (clientOrder.ClientLocation.AllowMultipleOrderTypes == true)
                {
                    this.GoToOrderTypePage(clientOrder);
                }
                else
                {                    
                    YellowstonePathology.Business.ClientOrder.Model.ClientOrder specificClientOrder = YellowstonePathology.Business.ClientOrder.Model.ClientOrderFactory.GetClientOrder(clientOrder.ClientLocation.DefaultOrderPanelSetId);                    
                    specificClientOrder.Join(clientOrder);
                    specificClientOrder.PanelSetId = clientOrder.ClientLocation.DefaultOrderPanelSetId;
                    this.GoToOrderEntryPage(specificClientOrder);
                }
            }
            else
            {                
                this.GoToOrderEntryPage(clientOrder);
            }         
        }        

        private void GoToOrderTypePage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {
            OrderEntry.OrderTypePage orderTypePage = new OrderEntry.OrderTypePage(clientOrder);
            orderTypePage.Return += new OrderTypePage.ReturnEventHandler(OrderTypePage_Return);
            ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderTypePage);
        }

		private void OrderTypePage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
        {            
            switch (e.PageNavigationDirectionEnum)
            {
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
                    YellowstonePathology.Business.ClientOrder.Model.ClientOrder specificClientOrder = (YellowstonePathology.Business.ClientOrder.Model.ClientOrder)e.Data;
					this.GoToOrderEntryPage(specificClientOrder);
                    break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
                    ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(this);
                    break;
            }
        }        

		private void GoToOrderEntryPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {
			switch (clientOrder.PanelSetId)
			{
				case 13:
					this.ShowSurgicalOrderEntryPage(clientOrder);
					break;
				//case 15:
				//	this.ShowThinPrepPapOrderEntryPage(clientOrder);
				//	break;
				default:
					this.ShowOrderEntryPage(clientOrder);
					break;
			}
        }

		private void ShowOrderEntryPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Business.Persistence.ObjectTracker();
			objectTracker.RegisterObject(clientOrder);
			OrderEntry.OrderEntryPage orderEntryPage = new OrderEntry.OrderEntryPage(clientOrder, objectTracker);
			orderEntryPage.Return += new OrderEntryPage.ReturnEventHandler(OrderEntryPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderEntryPage);
		}

		private void ShowSurgicalOrderEntryPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Business.Persistence.ObjectTracker();
			objectTracker.RegisterObject(clientOrder);
			OrderEntry.SurgicalOrderEntryPage orderEntryPage = new OrderEntry.SurgicalOrderEntryPage((YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder)clientOrder, objectTracker);
			orderEntryPage.Return += new SurgicalOrderEntryPage.ReturnEventHandler(OrderEntryPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderEntryPage);
		}

		//private void ShowThinPrepPapOrderEntryPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		//{
		//	YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Persistence.ObjectTracker();
		//	objectTracker.RegisterObject(clientOrder);
		//	OrderEntry.ThinPrepPapOrderEntryPage orderEntryPage = new OrderEntry.ThinPrepPapOrderEntryPage((YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder)clientOrder, objectTracker);
		//	orderEntryPage.Return += new ThinPrepPapOrderEntryPage.ReturnEventHandler(OrderEntryPage_Return);
		//	ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderEntryPage);
		//}

		private void OrderEntryPage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(this);
		}        

        private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
        {
			HomePage homePage = new HomePage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(homePage);
        }        

        private void HyperDetails_Click(object sender, RoutedEventArgs e)
        {
			if (this.ListViewOrders.SelectedItems.Count != 0)
			{
				YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = (YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem)this.ListViewOrders.SelectedItem;
                this.HandleOrderDetailRequested(orderBrowserListItem);
			}
		}

		private void HyperSendMessage_Click(object sender, RoutedEventArgs e)
		{
			YpiConnect.Contract.Message message = new Contract.Message("ClientCommunication@ypii.com", YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			if (this.ListViewOrders.SelectedItem != null)
			{
				YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = (YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem)this.ListViewOrders.SelectedItem;                
                message.PatientName = orderBrowserListItem.PFirstName + " " + orderBrowserListItem.PLastName;
			}
			MessagePage messagePage = new MessagePage(message);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(messagePage);
		}

		private void HyperShipping_Click(object sender, RoutedEventArgs e)
		{
			ShippingPage shippingPage = new ShippingPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(shippingPage);
		}

		public void Save()
		{

		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void UpdateBindingSources()
		{
		}
	}
}
