using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class ReceiveCytologySpecimenPath
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private LoginPageWindow m_LoginPageWindow;		
		private YellowstonePathology.UI.Login.Receiving.ClientOrderReceivingHandler m_ClientOrderReceivingHandler;

		public ReceiveCytologySpecimenPath()
		{			
			this.m_LoginPageWindow = new LoginPageWindow();
			this.m_ClientOrderReceivingHandler = new Receiving.ClientOrderReceivingHandler(this.m_LoginPageWindow);
		}

		public void Start()
		{
			this.ShowSvhOrderIdLookupPage();
			this.m_LoginPageWindow.ShowDialog();
		}

		public YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder ClientOrder
		{
			get { return (YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder)this.m_ClientOrderReceivingHandler.ClientOrder; }
		}

		private void ShowSvhOrderIdLookupPage()
		{			
            //YellowstonePathology.UI.Login.Receiving.ClientOrderLookupPage clientOrderLookupPage = new Receiving.ClientOrderLookupPage(this.m_ClientOrderReceivingHandler.e);
            //clientOrderLookupPage.ClientOrderFound += new Receiving.ClientOrderLookupPage.ClientOrderFoundEventHandler(ClientOrderLookupPage_ClientOrderFound);
            //clientOrderLookupPage.MultipleClientOrdersFound += new Receiving.ClientOrderLookupPage.MultipleClientOrdersFoundEventHandler(ClientOrderLookupPage_MultipleClientOrdersFound);
            //clientOrderLookupPage.Back += new Receiving.ClientOrderLookupPage.BackEventHandler(ClientOrderLookupPage_Back);
            //this.m_LoginPageWindow.PageNavigator.Navigate(clientOrderLookupPage);
		}

        private void ClientOrderLookupPage_Back(object sender, EventArgs e)
        {
            this.EndPath(false);
        }

        private void ClientOrderLookupPage_MultipleClientOrdersFound(object sender, CustomEventArgs.ClientOrderCollectionReturnEventArgs e)
        {
            this.ShowClientOrderSelectionPage(e.ClientOrderCollection);
        }

        private void ClientOrderLookupPage_ClientOrderFound(object sender, CustomEventArgs.ClientOrderReturnEventArgs e)
        {
			YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(e.ClientOrder.ClientId);

            client.ClientLocationCollection.SetCurrentLocation(e.ClientOrder.ClientLocationId.Value);
            this.m_ClientOrderReceivingHandler.IFoundAClient(client);
            this.m_ClientOrderReceivingHandler.IFoundAClientOrder(e.ClientOrder);
            this.ShowConfirmClientOrderPage();
        }        

        private void ShowClientOrderSelectionPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            Receiving.ClientOrderSelectionPage clientOrderSelectionPage = new Receiving.ClientOrderSelectionPage(clientOrderCollection, this.m_LoginPageWindow.PageNavigator);
            clientOrderSelectionPage.Back += new Receiving.ClientOrderSelectionPage.BackEventHandler(ClientOrderSelectionPage_Back);
            clientOrderSelectionPage.ClientOrderSelected += new Receiving.ClientOrderSelectionPage.ClientOrderSelectedEventHandler(ClientOrderSelectionPage_ClientOrderSelected);            
            this.m_LoginPageWindow.PageNavigator.Navigate(clientOrderSelectionPage);
        }        

        private void ClientOrderSelectionPage_ClientOrderSelected(object sender, CustomEventArgs.ClientOrderReturnEventArgs e)
        {
			YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(e.ClientOrder.ClientId);

            client.ClientLocationCollection.SetCurrentLocation(e.ClientOrder.ClientLocationId.Value);
            this.m_ClientOrderReceivingHandler.IFoundAClient(client);
            this.m_ClientOrderReceivingHandler.IFoundAClientOrder(e.ClientOrder);
            this.ShowConfirmClientOrderPage();
        }        

        private void ClientOrderSelectionPage_Back(object sender, EventArgs e)
        {
            this.ShowSvhOrderIdLookupPage();            
        }        		

		private void ShowConfirmClientOrderPage()
		{
			//ClientOrderConfirmationPage clientOrderConfirmationPage = new ClientOrderConfirmationPage(this.m_ClientOrderReceivingHandler.ExpectedOrderType);
			//clientOrderConfirmationPage.Return += new ClientOrderConfirmationPage.ReturnEventHandler(ClientOrderConfirmationPage_Return);
			//this.m_LoginPageWindow.PageNavigator.Navigate(clientOrderConfirmationPage);
		}

		private void ClientOrderConfirmationPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
					this.EndPath(true);            
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Finish:
					this.ShowNPINotFoundPage();
					break;
			}
		}

		private void ShowNPINotFoundPage()
		{
			//ReceiveSpecimen.NPINotFoundPage nPINotFoundPage = new ReceiveSpecimen.NPINotFoundPage(this.m_ClientOrderReceivingHandler);
			//nPINotFoundPage.Return += new ReceiveSpecimen.NPINotFoundPage.ReturnEventHandler(NPINotFoundPage_Return);
			//this.m_LoginPageWindow.PageNavigator.Navigate(nPINotFoundPage);
		}

		private void NPINotFoundPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Finish:
					this.EndPath(false);            
					break;
			}
		}

		private void EndPath(bool useClientOrder)
		{
			this.m_LoginPageWindow.Close();			

			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs();
			if(useClientOrder == true)
			{
				args.PageNavigationDirectionEnum = UI.Navigation.PageNavigationDirectionEnum.Next;
			}
			else
			{
				args.PageNavigationDirectionEnum = UI.Navigation.PageNavigationDirectionEnum.Back;
			}
			Return(this, args);
		}
	}
}
