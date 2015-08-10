using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class GenericSpecimenPath
	{
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetailClone;
		private ClientOrderDetailWizardModeEnum m_ClientOrderDetailWizardMode;
		private ScanContainerPage m_ScanContainerPage;

		public GenericSpecimenPath(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail, ClientOrderDetailWizardModeEnum clientOrderDetailWizardMode)
		{			
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderDetail = clientOrderDetail;
			this.m_ClientOrderDetailWizardMode = clientOrderDetailWizardMode;
			if (this.m_ClientOrderDetailWizardMode == ClientOrderDetailWizardModeEnum.AddNew)
			{
				this.m_ClientOrderDetail.OrderType = "Other";
			}
			YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new Business.Persistence.ObjectCloner();
			this.m_ClientOrderDetailClone = (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail)objectCloner.Clone(this.m_ClientOrderDetail);
		}

		public void Start()
		{
			ShowSpecimenDescriptionOtherPage();
		}

		private void ShowSpecimenDescriptionOtherPage()
		{
			SpecimenDescriptionOtherPage specimenDescriptionOtherPage = new SpecimenDescriptionOtherPage(this.m_ClientOrderDetailClone);
			specimenDescriptionOtherPage.Return += new SpecimenDescriptionOtherPage.ReturnEventHandler(SpecimenDescriptionOtherPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(specimenDescriptionOtherPage);
		}

		private void SpecimenDescriptionOtherPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					if (this.UpdateClientOrderDetailGoingBack() == true)
					{
						this.ShowOrderEntryPage(e);
					}
					break;
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowScanContainerPage();
					break;
			}
		}

		private void ShowScanContainerPage()
		{
			if (this.m_ScanContainerPage == null)
			{
                this.m_ScanContainerPage = new ScanContainerPage(this.m_ClientOrderDetailClone, this.m_ClientOrder);
				this.m_ScanContainerPage.Return += new ScanContainerPage.ReturnEventHandler(ScanContainerPage_Return);
			}
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(this.m_ScanContainerPage);
		}

		private void ScanContainerPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					ShowSpecimenDescriptionOtherPage();
					break;
				case Shared.PageNavigationDirectionEnum.Next:
					if (this.UpdateClientOrderDetailGoingForward() == true)
					{
						this.ShowOrderEntryPage(e);
					}
					break;
			}
		}

		private void ShowOrderEntryPage(Shared.PageNavigationReturnEventArgs args)
		{
			Return(this, args);
		}

		private bool UpdateClientOrderDetailGoingBack()
		{
			bool result = true;

			List<YellowstonePathology.Shared.ValidationResult> validationResults = new List<Shared.ValidationResult>();
			validationResults.Add(this.m_ClientOrder.ClientOrderDetailCollection.IsDomainValid(this.m_ClientOrderDetailClone));
			//validationResults.Add(this.m_ClientOrderDetailClone.IsDescriptionDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsCollectionDateDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsContainerIdDomainValid());

			foreach (Shared.ValidationResult validationResult in validationResults)
			{
				if (validationResult.IsValid == false)
				{
					result = false;
					break;
				}
			}

			if (result == false)
			{
				result = false;
				System.Windows.MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("If you continue going back, you will loose any data you have just entered.\r\n\r\nDo you want to go back?", "Possible information loss", System.Windows.MessageBoxButton.YesNo);
				if (messageBoxResult == System.Windows.MessageBoxResult.Yes)
				{
					result = true;
				}
			}
			else
			{
				this.m_ClientOrderDetail.Join(this.m_ClientOrderDetailClone);
				if (this.m_ClientOrderDetailWizardMode == ClientOrderDetailWizardModeEnum.AddNew)
				{
					this.m_ClientOrder.ClientOrderDetailCollection.Add(this.m_ClientOrderDetail);
				}
			}
			return result;
		}

		private bool UpdateClientOrderDetailGoingForward()
		{
			bool result = true;
			this.m_ClientOrderDetail.Join(this.m_ClientOrderDetailClone);
			if (this.m_ClientOrderDetailWizardMode == ClientOrderDetailWizardModeEnum.AddNew)
			{
				this.m_ClientOrder.ClientOrderDetailCollection.Add(this.m_ClientOrderDetail);
			}
			return result;
		}
	}
}
