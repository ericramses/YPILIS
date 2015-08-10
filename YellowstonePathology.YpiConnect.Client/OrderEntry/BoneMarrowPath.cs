using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class BoneMarrowPath
	{
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        private YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;
        private YellowstonePathology.Domain.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;
		private OrderEntryUI m_OrderEntryUI;
		private BoneMarrowParameters m_BoneMarrowParameters;

        public BoneMarrowPath(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Domain.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
		{
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderCollection = clientOrderCollection;
			this.m_BoneMarrowParameters = new BoneMarrowParameters();
		}

		public void Start()
		{
			this.FillParameters();
			this.ShowBoneMarrowInformationPage();
		}

		private void FillParameters()
		{
            foreach (YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrder.ClientOrderDetailCollection)
			{
				if (clientOrderDetail.Description == this.m_BoneMarrowParameters.PeripheralBloodDetailDescription)
				{
					this.m_BoneMarrowParameters.HavePeripheralBlood = true;
					this.m_BoneMarrowParameters.PeripheralBloodContainerId = clientOrderDetail.ContainerId;
				}
				if (clientOrderDetail.Description == this.m_BoneMarrowParameters.BoneMarrowCoreDetailDescription)
				{
					this.m_BoneMarrowParameters.HaveBoneMarrowCore = true;
					this.m_BoneMarrowParameters.BoneMarrowCoreContainerId = clientOrderDetail.ContainerId;
				}
				if (clientOrderDetail.Description == this.m_BoneMarrowParameters.BoneMarrowAspirateDetailDescription)
				{
					this.m_BoneMarrowParameters.HaveBoneMarrowAspirate = true;
					this.m_BoneMarrowParameters.BoneMarrowAspirateContainerId = clientOrderDetail.ContainerId;
				}
			}
		}

		private void ShowBoneMarrowInformationPage()
		{
			BoneMarrowInformationPage boneMarrowInformationPage = new BoneMarrowInformationPage(this.m_BoneMarrowParameters);
			boneMarrowInformationPage.Return += new BoneMarrowInformationPage.ReturnEventHandler(BoneMarrowInformationPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(boneMarrowInformationPage);
		}

		private void BoneMarrowInformationPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					if (this.ShowScanContainerPage() == false)
					{
						this.ShowSpecimenListPage();
					}
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					ShowOrderEntryPage();
					break;
			}
		}

		private bool ShowScanContainerPage()
		{
			bool result = false;
			ScanContainerPage scanContainerPage = null;
			if (this.m_BoneMarrowParameters.NeedsBoneMarrowCoreScan == true)
			{
				result = true;
				scanContainerPage = new ScanContainerPage(null, this.m_ClientOrder); //, this.m_BoneMarrowParameters.BoneMarrowCoreScanComment);
				scanContainerPage.Return += new ScanContainerPage.ReturnEventHandler(BoneMarrowCoreScanContainerPage_Return);
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(scanContainerPage);
			}
			else if (this.m_BoneMarrowParameters.NeedsPeripheralBloodScan == true)
			{
				result = true;
                scanContainerPage = new ScanContainerPage(null, this.m_ClientOrder); //, this.m_BoneMarrowParameters.PeripheralBloodScanComment);
				scanContainerPage.Return += new ScanContainerPage.ReturnEventHandler(PeripheralBloodScanContainerPage_Return);
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(scanContainerPage);
			}
			else if (this.m_BoneMarrowParameters.NeedsBoneMarrowAspirateScan == true)
			{
				result = true;
                scanContainerPage = new ScanContainerPage(null, this.m_ClientOrder); //, this.m_BoneMarrowParameters.BoneMarrowAspirateScanComment);
				scanContainerPage.Return += new ScanContainerPage.ReturnEventHandler(BoneMarrowAspirateScanContainerPage_Return);
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(scanContainerPage);
			}

			return result;
		}

		private void BoneMarrowCoreScanContainerPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			string containerId = e.Data.ToString();
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
					break;
				case Shared.PageNavigationDirectionEnum.Command:
					this.m_BoneMarrowParameters.BoneMarrowCoreContainerId = containerId;
					this.AddContainer(containerId, this.m_BoneMarrowParameters.BoneMarrowCoreDetailDescription);
					if (this.ShowScanContainerPage() == false)
					{
						this.ShowSpecimenListPage();
					}
					break;
			}
		}

		private void PeripheralBloodScanContainerPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			string containerId = e.Data.ToString();
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
					break;
				case Shared.PageNavigationDirectionEnum.Command:
					this.m_BoneMarrowParameters.PeripheralBloodContainerId = containerId;
					this.AddContainer(containerId, this.m_BoneMarrowParameters.PeripheralBloodDetailDescription);
					if (this.ShowScanContainerPage() == false)
					{
						this.ShowSpecimenListPage();
					}
					break;
			}
		}

		private void BoneMarrowAspirateScanContainerPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			string containerId = e.Data.ToString();
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
					break;
				case Shared.PageNavigationDirectionEnum.Command:
					this.m_BoneMarrowParameters.BoneMarrowAspirateContainerId = containerId;
					this.AddContainer(containerId, this.m_BoneMarrowParameters.BoneMarrowAspirateDetailDescription);
					if (this.ShowScanContainerPage() == false)
					{
						this.ShowSpecimenListPage();
					}
					break;
			}
		}

		private void AddContainer(string containerId, string description)
		{
            //must get client order detail from factory.
			//string clientOrderDetailType = this.m_ClientOrder.GetOrderDetailType();
			//YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetail = this.m_ClientOrder.ClientOrderDetailCollection.GetNew(containerId, this.m_ClientOrder.ClientOrderId, clientOrderDetailType, "YPICONNECT", description, "Bedside");
			//clientOrderDetail.OrderedBy = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName;
			//this.m_ClientOrder.ClientOrderDetailCollection.Add(clientOrderDetail);
			//this.m_ClientOrder.ClientOrderDetailCollection.RenumberSpecimens();
		}

		private void ShowSpecimenListPage()
		{
			SpecimenListPage specimenListPage = new SpecimenListPage(this.m_ClientOrder);
			specimenListPage.Return += new SpecimenListPage.ReturnEventHandler(SpecimenListPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(specimenListPage);
		}

		private void SpecimenListPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
					break;
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowOrderEntryPage();
					break;
			}
		}

		private void ShowOrderEntryPage()
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
			Return(this, args);
		}
	}
}
