using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class FNAPath
	{
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;
		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;
		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty m_ClientOrderFNAProperty;
		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertyCollection m_ClientOrderFNAPropertyCollection;
		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderDetailFNAProperty m_ClientOrderDetailFNAProperty;

		public FNAPath(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Domain.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
		{
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderCollection = clientOrderCollection;
			this.FillClientOrderFNAPropertyCollection();
		}

		private void FillClientOrderFNAPropertyCollection()
		{
			//YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
			//this.m_ClientOrderFNAPropertyCollection = proxy.GetClientOrderFNAProperties(this.m_ClientOrder.ClientOrderId);
		}

		public void Start()
		{
			this.ShowFNASpecimenPage();
		}

		private void FNASpecimenListPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty clientOrderFNAProperty = (YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty)e.Data;
					foreach(YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty item in this.m_ClientOrderFNAPropertyCollection)
					{
						if (item.ClientOrderFNAPropertyId == clientOrderFNAProperty.ClientOrderFNAPropertyId)
						{
							this.m_ClientOrderFNAProperty = item;
							break;
						}
					}
					this.ShowFNASpecimenPassPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
					break;
				case Shared.PageNavigationDirectionEnum.Command:
					this.ShowFNASpecimenPage();
					break;
			}
		}

		private void ShowFNASpecimenPage()
		{
			FNASpecimenPage fNASpecimenPage = new FNASpecimenPage(this.m_ClientOrder, this.m_ClientOrderFNAPropertyCollection);
			fNASpecimenPage.Return += new FNASpecimenPage.ReturnEventHandler(FNASpecimenPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fNASpecimenPage);
		}

		private void FNASpecimenPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowOrderEntryPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					this.ShowOrderEntryPage();
					break;
				case Shared.PageNavigationDirectionEnum.Command:
					this.ShowFNASpecimenPassPage();
					break;
			}
		}

		private void ShowFNASpecimenPassPage()
		{
			FNASpecimenPassPage fNASpecimenPassPage = new FNASpecimenPassPage(this.m_ClientOrder, this.m_ClientOrderFNAPropertyCollection[this.m_ClientOrderFNAPropertyCollection.Count - 1], this.m_ClientOrderFNAPropertyCollection);
			fNASpecimenPassPage.Return += new FNASpecimenPassPage.ReturnEventHandler(FNASpecimenPassPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fNASpecimenPassPage);
		}

		private void FNASpecimenPassPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowScanContainerPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					this.ShowFNASpecimenPage();
					break;
				case Shared.PageNavigationDirectionEnum.Command:
					this.FNASpecimenPassPageCommand_Return(e);
					break;
			}
		}

		private void FNASpecimenPassPageCommand_Return(Shared.PageNavigationReturnEventArgs e)
		{
			PageNavigationCommandEnum pageNavigationCommandEnum = (PageNavigationCommandEnum)e.Data;
			switch (pageNavigationCommandEnum)
			{
				case PageNavigationCommandEnum.Finish:
					this.ShowOrderEntryPage();
					break;
			}
		}

		private void ShowScanContainerPage()
		{
            ScanContainerPage scanContainerPage = new ScanContainerPage(null, this.m_ClientOrder); //, "Scan the specimen.");
			scanContainerPage.Return += new ScanContainerPage.ReturnEventHandler(ScanContainerPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(scanContainerPage);
		}

		private void ScanContainerPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
					break;
				case Shared.PageNavigationDirectionEnum.Command:
					this.AddContainer(e.Data.ToString());
					this.SetClientOrderDetailFNAProperty();
					this.Save();
					ShowFNASpecimenPassPage();
					break;
			}
		}

		private void AddContainer(string containerId)
		{
            /* Must get client order detail from factory
			int passNumber = this.m_ClientOrderFNAProperty.ClientOrderDetailFNAPropertyCollection.Count;
			string description = "FNA Pass " + passNumber.ToString() + ": " + this.m_ClientOrderFNAProperty.SpecimenSource;
			string clientOrderDetailType = this.m_ClientOrder.GetClientOrderDetailType();
			YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetail = this.m_ClientOrder.ClientOrderDetailCollection.GetNew(containerId, this.m_ClientOrder.ClientOrderId, clientOrderDetailType, "YPICONNECT", description, "Bedside");
			clientOrderDetail.OrderedBy = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName;
			this.m_ClientOrder.ClientOrderDetailCollection.Add(clientOrderDetail);
			this.m_ClientOrder.ClientOrderDetailCollection.RenumberSpecimens();
            */
		}

		private void SetClientOrderDetailFNAProperty()
		{
			int idx = this.m_ClientOrder.ClientOrderDetailCollection.Count - 1;
			YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetail = this.m_ClientOrder.ClientOrderDetailCollection[idx];
		}

		private void ShowOrderEntryPage()
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
			Return(this, args);
		}

		private void Save()
		{
			YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy clientOrderServiceProxy = new Proxy.ClientOrderServiceProxy();
			YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAPropertySubmitter submitter = new Contract.Order.ClientOrderFNAPropertySubmitter(this.m_ClientOrderFNAPropertyCollection);

			if (submitter.HasChanges())
			{
				System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
				submitter.BeginSubmit(Business.Domain.Persistence.PropertyReaderFilterEnum.Normal);
				//YellowstonePathology.Business.Rules.MethodResult methodResult = clientOrderServiceProxy.SubmitChanges(submitter);
				//if (methodResult.Success == false)
				//{
				//	System.Windows.MessageBox.Show(methodResult.Message);
				//}
				//else
				//{
				//	submitter.EndSubmit();
				//}
				//System.Threading.Thread.Sleep(500);
				//System.Windows.Input.Mouse.OverrideCursor = null;
			}

			YellowstonePathology.YpiConnect.Contract.Order.ClientOrderSubmitter clientOrderSubmitter = new Contract.Order.ClientOrderSubmitter(this.m_ClientOrderCollection);

			if (clientOrderSubmitter.HasChanges())
			{
                /*
				System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
				clientOrderSubmitter.BeginSubmit(Business.Domain.Persistence.PropertyReaderFilterEnum.External);
				YellowstonePathology.Business.Rules.MethodResult methodResult = clientOrderServiceProxy.SubmitChanges(clientOrderSubmitter);
				if (methodResult.Success == false)
				{
					System.Windows.MessageBox.Show(methodResult.Message);
				}
				else
				{
					clientOrderSubmitter.EndSubmit();
				}
				System.Threading.Thread.Sleep(500);
				System.Windows.Input.Mouse.OverrideCursor = null;
                */
			}
		}
	}
}
