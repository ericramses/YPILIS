using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{
	public class ReceiveSpecimenPath
	{				
        private YellowstonePathology.UI.Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.UI.Login.Receiving.ClientOrderReceivingHandler m_ClientOrderReceivingHandler;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public ReceiveSpecimenPath()
        {
            this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.Blank);
            this.m_LoginPageWindow = new Receiving.LoginPageWindow();                        
        }

        public void Start()
        {           
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
				this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.SetClientOrderReceivingHandlerSystemIdentity(new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn));
				this.ShowClientLookupPage();
			}
			this.m_LoginPageWindow.ShowDialog();
        }

        private void ShowScanSecurityBadgePage()
        {
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			scanSecurityBadgePage.AuthentificationSuccessful += new ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            this.m_LoginPageWindow.PageNavigator.Navigate(scanSecurityBadgePage);
        }

		private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
        {
			this.SetClientOrderReceivingHandlerSystemIdentity(e.SystemIdentity);
          	this.ShowClientLookupPage();
        }

		private void SetClientOrderReceivingHandlerSystemIdentity(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_SystemIdentity = systemIdentity;
			this.m_ClientOrderReceivingHandler = new Receiving.ClientOrderReceivingHandler(this.m_SystemIdentity);
			System.Diagnostics.Debug.Assert(this.m_ClientOrderReceivingHandler.SystemIdentity.IsKnown, "The system identity must be known.");
			System.Diagnostics.Debug.Assert((this.m_ClientOrderReceivingHandler.SystemIdentity.User != null), "The system identity must be known.");
		}

		private void ShowClientLookupPage()
		{
			ClientLookupPage clientLookupPage = new ClientLookupPage();
			clientLookupPage.Return += new Login.ClientLookupPage.ReturnEventHandler(ClientLookupPage_Return);
			this.m_LoginPageWindow.PageNavigator.Navigate(clientLookupPage);
		}

		private void ClientLookupPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {			 
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
                    this.HandleClientFound(e);
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.m_LoginPageWindow.Close();
					break;
			}
		}

        private void HandleClientFound(UI.Navigation.PageNavigationReturnEventArgs e)
        {
            YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)e.Data;
            this.m_ClientOrderReceivingHandler.IFoundAClient(client);
			Receiving.GetClientOrderPath getClientOrderPath = new Receiving.GetClientOrderPath(this.m_ClientOrderReceivingHandler, this.m_LoginPageWindow.PageNavigator);
			getClientOrderPath.Return += new Receiving.GetClientOrderPath.ReturnEventHandler(GetClientOrderPath_Return);
			getClientOrderPath.Start();
        }

		private void GetClientOrderPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					if (this.ShowPlacentaQuestionairePage() == false)
					{
						this.ShowItemsReceivedPage();
					}
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.ShowClientLookupPage();
					break;
			}
		}

		private bool ShowPlacentaQuestionairePage()
		{
			bool result = false;
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrderReceivingHandler.ClientOrder.ClientOrderDetailCollection)
			{
				if (clientOrderDetail.OrderTypeCode == "PLCNT")
				{
					result = true;
					Receiving.PlacentalPathologyPage placentalPathologyPage = new Receiving.PlacentalPathologyPage(this.m_ClientOrderReceivingHandler.ClientOrder);
					placentalPathologyPage.Return += new Receiving.PlacentalPathologyPage.ReturnEventHandler(PlacentalPathologyPage_Return);
					this.m_LoginPageWindow.PageNavigator.Navigate(placentalPathologyPage);
					break;
				}
			}
			return result;
		}

		private void PlacentalPathologyPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.ShowItemsReceivedPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					Receiving.GetClientOrderPath getClientOrderPath = new Receiving.GetClientOrderPath(this.m_ClientOrderReceivingHandler, this.m_LoginPageWindow.PageNavigator);
					getClientOrderPath.Return += new Receiving.GetClientOrderPath.ReturnEventHandler(GetClientOrderPath_Return);
					getClientOrderPath.StartFromEnd();
					break;
			}
		}

		private void ShowItemsReceivedPage()
		{
            if (string.IsNullOrEmpty(this.m_ClientOrderReceivingHandler.ClientOrder.SpecialInstructions) == false)
            {
                SpecialInstructionsWindow specialInstructionsWindow = new SpecialInstructionsWindow(this.m_ClientOrderReceivingHandler.ClientOrder.SpecialInstructions);
                this.m_LoginPageWindow.PageNavigator.ShowSecondMonitorWindow(specialInstructionsWindow);
            }

            Receiving.ItemsReceivedPage itemsReceivedPage = new Receiving.ItemsReceivedPage(this.m_ClientOrderReceivingHandler);
            itemsReceivedPage.Next += new ItemsReceivedPage.NextEventHandler(ItemsReceivedPage_Next);
            itemsReceivedPage.Back += new ItemsReceivedPage.BackEventHandler(ItemsReceivedPage_Back);
            itemsReceivedPage.BarcodeWontScan += new ItemsReceivedPage.BarcodeWontScanEventHandler(ItemsReceivedPage_BarcodeWontScan);            
            itemsReceivedPage.ContainerScannedReceived += new ItemsReceivedPage.ContainScanReceivedEventHandler(ItemsReceivedPage_ContainerScannedReceived);
			itemsReceivedPage.ShowClientOrderDetailsPage += new ItemsReceivedPage.ShowClientOrderDetailsPageEventHandler(ItemsReceivedPage_ShowClientOrderDetailsPage);
            this.m_LoginPageWindow.PageNavigator.Navigate(itemsReceivedPage);
		}

        private void ItemsReceivedPage_Back(object sender, EventArgs e)
        {
            Receiving.GetClientOrderPath getClientOrderPath = new Receiving.GetClientOrderPath(this.m_ClientOrderReceivingHandler, this.m_LoginPageWindow.PageNavigator);
            getClientOrderPath.Return += new Receiving.GetClientOrderPath.ReturnEventHandler(GetClientOrderPath_Return);
            getClientOrderPath.StartFromEnd();
        }

        private void ItemsReceivedPage_Next(object sender, EventArgs e)
        {
            this.m_ClientOrderReceivingHandler.Save();
            this.StartReviewClientOrderPath();
        }

        private void ItemsReceivedPage_BarcodeWontScan(object sender, EventArgs e)
        {
            YellowstonePathology.UI.Login.ReceiveSpecimen.BarcodeManualEntryPage containerManualEntryPage = new YellowstonePathology.UI.Login.ReceiveSpecimen.BarcodeManualEntryPage();
            containerManualEntryPage.Return += new ReceiveSpecimen.BarcodeManualEntryPage.ReturnEventHandler(ContainerManualEntryPage_Return);
            containerManualEntryPage.Back += new ReceiveSpecimen.BarcodeManualEntryPage.BackEventHandler(ContainerManualEntryPage_Back);
            this.m_LoginPageWindow.PageNavigator.Navigate(containerManualEntryPage);						
        }

        private void ContainerManualEntryPage_Back()
        {
            this.ShowItemsReceivedPage();
        }

		private void ItemsReceivedPage_ContainerScannedReceived(object sender, YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.ReceiveContainerScan(containerBarcode.ToString());
        }

		private void ItemsReceivedPage_ShowClientOrderDetailsPage(object sender, CustomEventArgs.ClientOrderDetailReturnEventArgs e)
		{
			this.m_ClientOrderReceivingHandler.ResetTheSelectedClientOrderDetailToThisOne(e.ClientOrderDetail);
			this.ShowClientOrderDetailsPage(e.ClientOrderDetail);
		}

        private void ContainerManualEntryPage_Return(object sender, string containerId)
        {
            if (string.IsNullOrEmpty(containerId) == false)
            {
                this.ReceiveContainerScan(containerId);
            }            
        }

		private void ReceiveContainerScan(string containerId)
		{
			YellowstonePathology.UI.Login.ReceiveSpecimen.IFoundAContainerResult result = this.m_ClientOrderReceivingHandler.IFoundAContainer(containerId);
			if (result.OkToReceive == true)
			{
				this.ShowClientOrderDetailsPage(result.ClientOrderDetail);
			}
			else
			{
				System.Windows.MessageBox.Show(result.Message);
			}
		}

		private void ShowClientOrderDetailsPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{            
            Receiving.ClientOrderDetailsPage clientOrderDetailsPage = new Receiving.ClientOrderDetailsPage(this.m_LoginPageWindow.PageNavigator, clientOrderDetail, this.m_ClientOrderReceivingHandler.ClientOrder.SpecialInstructions, this.m_ClientOrderReceivingHandler.SystemIdentity);
            clientOrderDetailsPage.Next += new ClientOrderDetailsPage.NextEventHandler(ClientOrderDetailsPage_Next);
            clientOrderDetailsPage.Back += new ClientOrderDetailsPage.BackEventHandler(ClientOrderDetailsPage_Back);
            clientOrderDetailsPage.SaveClientOrderDetail += new ClientOrderDetailsPage.SaveClientOrderDetailEventHandler(ClientOrderDetailsPage_SaveClientOrderDetail);            
			this.m_LoginPageWindow.PageNavigator.Navigate(clientOrderDetailsPage);
		}

        private void ClientOrderDetailsPage_SaveClientOrderDetail(object sender, EventArgs e)
        {
            this.m_ClientOrderReceivingHandler.Save();
        }        

        private void ClientOrderDetailsPage_Back(object sender, EventArgs e)
        {
            this.ShowItemsReceivedPage();
        }

        private void ClientOrderDetailsPage_Next(object sender, EventArgs e)
        {
            this.ShowItemsReceivedPage();
        }			

		private void StartReviewClientOrderPath()
		{
			ReviewClientOrderPath reviewClientOrderPath = new ReviewClientOrderPath(this.m_LoginPageWindow.PageNavigator, this.m_ClientOrderReceivingHandler);
			reviewClientOrderPath.Return += new ReviewClientOrderPath.ReturnEventHandler(ReviewClientOrderPath_Return);
			reviewClientOrderPath.Back += new ReviewClientOrderPath.BackEventHandler(ReviewClientOrderPath_Back);
			reviewClientOrderPath.Finish += new ReviewClientOrderPath.FinishEventHandler(ReviewClientOrderPath_Finish);
			reviewClientOrderPath.Start();
		}

		private void ReviewClientOrderPath_Finish(object sender, EventArgs e)
		{
			this.m_LoginPageWindow.Close();
		}

		private void ReviewClientOrderPath_Back(object sender, EventArgs e)
		{
			this.ShowItemsReceivedPage();
		}

		private void ReviewClientOrderPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Command:
					this.HandleCommand(e);
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Finish:
					this.m_ClientOrderReceivingHandler = new ClientOrderReceivingHandler(this.m_SystemIdentity);
					this.ShowClientLookupPage();
					break;
			}
		}

		private void HandleCommand(UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch ((ReceiveSpecimen.ReceiveSpecimenCommandTypeEnum)e.Data)
			{
				case ReceiveSpecimen.ReceiveSpecimenCommandTypeEnum.Finalize:
                    this.m_ClientOrderReceivingHandler = new ClientOrderReceivingHandler(this.m_SystemIdentity);
					this.ShowClientLookupPage();
					break;
			}
		}
	}
}
