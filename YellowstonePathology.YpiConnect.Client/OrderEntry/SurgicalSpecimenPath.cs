using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class SurgicalSpecimenPath
	{
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		
		private YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder m_ClientOrder;
		private YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail m_ClientOrderDetail;
		private YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail m_ClientOrderDetailClone;
		private ClientOrderDetailWizardModeEnum m_ClientOrderDetailWizardMode;
		private bool m_ShowSurgicalClientOrderInformation;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;		

		public SurgicalSpecimenPath(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder clientOrder,
			YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail clientOrderDetail,
			ClientOrderDetailWizardModeEnum clientOrderDetailWizardMode,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{			
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderDetail = clientOrderDetail;
			this.m_ClientOrderDetailWizardMode = clientOrderDetailWizardMode;
			this.m_ObjectTracker = objectTracker;

			if (this.m_ClientOrderDetailWizardMode == ClientOrderDetailWizardModeEnum.AddNew)
			{
				this.m_ClientOrderDetail.OrderType = "Routine Surgical Pathology";
			}

			YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new Business.Persistence.ObjectCloner();
			this.m_ClientOrderDetailClone = (YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail)objectCloner.Clone(this.m_ClientOrderDetail);
		}

		public void Start()
		{
			if (string.IsNullOrEmpty(this.m_ClientOrder.PreOpDiagnosis) == true)
			{
				this.ShowSurgicalClientOrderInformationPage();
				this.m_ShowSurgicalClientOrderInformation = true;
			}
			else
			{
				this.ShowSpecimenDescriptionPage();
			}
		}

		private void ShowSurgicalClientOrderInformationPage()
		{
			SurgicalClientOrderInformationPage surgicalClientOrderInformationPage = new SurgicalClientOrderInformationPage(this.m_ClientOrder, this.m_ObjectTracker);
			surgicalClientOrderInformationPage.Return += new SurgicalClientOrderInformationPage.ReturnEventHandler(SurgicalClientOrderInformationPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(surgicalClientOrderInformationPage);
		}

		private void SurgicalClientOrderInformationPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowSpecimenDescriptionFromClientInformationPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					if (this.UpdateClientOrderDetailGoingBack() == true)
					{
						this.ShowOrderEntryPage(e);
					}
					break;
			}
		}

		private void ShowSpecimenDescriptionPage()
		{
			SpecimenDescriptionPage SpecimenDescriptionPage = new SpecimenDescriptionPage(this.m_ClientOrderDetailClone);
			SpecimenDescriptionPage.Return += new SpecimenDescriptionPage.ReturnEventHandler(SpecimenDescriptionPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(SpecimenDescriptionPage);
		}

		private void SpecimenDescriptionPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowScanContainerPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					if (this.UpdateClientOrderDetailGoingBack() == true)
					{
						this.ShowOrderEntryPage(e);
					}
					break;
			}
		}

		private void ShowSpecimenDescriptionFromClientInformationPage()
		{
			SpecimenDescriptionPage SpecimenDescriptionPage = new SpecimenDescriptionPage(this.m_ClientOrderDetailClone);
			SpecimenDescriptionPage.Return += new SpecimenDescriptionPage.ReturnEventHandler(ShowSpecimenDescriptionPageFromClientInformationPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(SpecimenDescriptionPage);
		}

		private void ShowSpecimenDescriptionPageFromClientInformationPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowScanContainerPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					this.ShowSurgicalClientOrderInformationPage();
					break;
			}
		}

		private void ShowScanContainerPage()
		{			
            ScanContainerPage scanContainerPage = new ScanContainerPage(this.m_ClientOrderDetailClone, this.m_ClientOrder);
            scanContainerPage.Return += new ScanContainerPage.ReturnEventHandler(ScanContainerPage_Return);
            ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(scanContainerPage);
		}

		private void ScanContainerPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					if (this.m_ShowSurgicalClientOrderInformation == true)
					{
						this.ShowSpecimenDescriptionFromClientInformationPage();
					}
					else
					{
						this.ShowSpecimenDescriptionPage();
					}
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

		private bool IsPlacentalQuestionnaireNeeded()
		{
			bool result = false;
			if (this.m_ClientOrderDetailClone.Description.ToUpper().Contains("PLACENTA") == true)
			{
				result = true;
			}
			return result;
		}		

		private bool UpdateClientOrderDetailGoingBack()
		{
			bool result = true;

			List<YellowstonePathology.Shared.ValidationResult> validationResults = new List<Shared.ValidationResult>();
			//validationResults.Add(this.m_ClientOrder.ClientOrderDetailCollection.IsDomainValid(this.m_ClientOrderDetailClone));			
			//validationResults.Add(this.m_ClientOrderDetailClone.IsDescriptionDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsCollectionDateDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsContainerIdDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsCallbackNumberDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsSpecialInstructionsDomainValid());

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
