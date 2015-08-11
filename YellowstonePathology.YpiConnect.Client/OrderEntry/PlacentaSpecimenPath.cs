using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class PlacentaSpecimenPath
	{
		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		
		private YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder m_ClientOrder;
		private YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail m_ClientOrderDetail;
		private YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail m_ClientOrderDetailClone;
		private ClientOrderDetailWizardModeEnum m_ClientOrderDetailWizardMode;
		private bool m_ShowSurgicalClientOrderInformation;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		private ScanContainerPage m_ScanContainerPage;

		public PlacentaSpecimenPath(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder clientOrder,
			YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail clientOrderDetail,
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
			this.m_ClientOrderDetailClone = (YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail)objectCloner.Clone(this.m_ClientOrderDetail);
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
				this.ShowPlacentaDescriptionPage();
			}
		}

		private void ShowSurgicalClientOrderInformationPage()
		{
			SurgicalClientOrderInformationPage surgicalClientOrderInformationPage = new SurgicalClientOrderInformationPage(this.m_ClientOrder, this.m_ObjectTracker);
			surgicalClientOrderInformationPage.Return += new SurgicalClientOrderInformationPage.ReturnEventHandler(SurgicalClientOrderInformationPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(surgicalClientOrderInformationPage);
		}

		private void SurgicalClientOrderInformationPage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					this.ShowPlacentaDescriptionFromClientInformationPage();
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					if (this.UpdateClientOrderDetailGoingBack() == true)
					{
						this.ShowOrderEntryPage(e);
					}
					break;
			}
		}

		private void ShowPlacentaDescriptionPage()
		{
			PlacentaDescriptionPage placentaDescriptionPage = new PlacentaDescriptionPage(this.m_ClientOrderDetailClone);
			placentaDescriptionPage.Return += new PlacentaDescriptionPage.ReturnEventHandler(PlacentaDescriptionPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(placentaDescriptionPage);
		}

		private void PlacentaDescriptionPage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					this.ShowPlacentalPathologyQuestionnairePage();
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					if (this.UpdateClientOrderDetailGoingBack() == true)
					{
						this.ShowOrderEntryPage(e);
					}
					break;
			}
		}

		private void ShowPlacentaDescriptionFromClientInformationPage()
		{
			PlacentaDescriptionPage placentaDescriptionPage = new PlacentaDescriptionPage(this.m_ClientOrderDetailClone);
			placentaDescriptionPage.Return += new PlacentaDescriptionPage.ReturnEventHandler(ShowPlacentaDescriptionPageFromClientInformationPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(placentaDescriptionPage);
		}

		private void ShowPlacentaDescriptionPageFromClientInformationPage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					this.ShowPlacentalPathologyQuestionnairePage();
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					this.ShowSurgicalClientOrderInformationPage();
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

		private void ScanContainerPage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					ShowPlacentalPathologyQuestionnaireTestOrderPage();
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					if (this.UpdateClientOrderDetailGoingForward() == true)
					{
						this.ShowOrderEntryPage(e);
					}
					break;
			}
		}

		private void ShowPlacentalPathologyQuestionnairePage()
		{
			PlacentalPathologyQuestionnairePage placentalPathologyQuestionnairePage = new PlacentalPathologyQuestionnairePage(this.m_ClientOrderDetailClone);
			placentalPathologyQuestionnairePage.Return += new PlacentalPathologyQuestionnairePage.ReturnEventHandler(PlacentalPathologyQuestionnairePage_Return);
			ApplicationNavigator.ApplicationContentFrame.Navigate(placentalPathologyQuestionnairePage);         
		}

		private void PlacentalPathologyQuestionnairePage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					if (this.m_ShowSurgicalClientOrderInformation == true)
					{
						this.ShowPlacentaDescriptionFromClientInformationPage();
					}
					else
					{
						this.ShowPlacentaDescriptionPage();
					}
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					this.ShowPlacentalPathologyQuestionnaireIndicationsPage();
					break;
			}
		}

		private void ShowPlacentalPathologyQuestionnaireIndicationsPage()
		{
			PlacentalPathologyQuestionnaireIndicationsPage placentalPathologyQuestionnaireIndicationsPage = new PlacentalPathologyQuestionnaireIndicationsPage(this.m_ClientOrderDetailClone);
			placentalPathologyQuestionnaireIndicationsPage.Return += new PlacentalPathologyQuestionnaireIndicationsPage.ReturnEventHandler(PlacentalPathologyQuestionnaireIndicationsPage_Return);
			ApplicationNavigator.ApplicationContentFrame.Navigate(placentalPathologyQuestionnaireIndicationsPage);
		}

		private void PlacentalPathologyQuestionnaireIndicationsPage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					ShowPlacentalPathologyQuestionnairePage();
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					this.ShowPlacentalPathologyQuestionnaireTestOrderPage();
					break;
			}
		}

		private void ShowPlacentalPathologyQuestionnaireTestOrderPage()
		{
			PlacentalPathologyQuestionnaireTestOrderPage placentalPathologyQuestionnaireTestOrderPage = new PlacentalPathologyQuestionnaireTestOrderPage(this.m_ClientOrderDetailClone);
			placentalPathologyQuestionnaireTestOrderPage.Return += new PlacentalPathologyQuestionnaireTestOrderPage.ReturnEventHandler(PlacentalPathologyQuestionnaireTestOrderPage_Return);
			ApplicationNavigator.ApplicationContentFrame.Navigate(placentalPathologyQuestionnaireTestOrderPage);
		}

		private void PlacentalPathologyQuestionnaireTestOrderPage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					this.ShowPlacentalPathologyQuestionnaireIndicationsPage();
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					this.ShowScanContainerPage();
					break;
			}
		}

		private void ShowOrderEntryPage(YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args)
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

			List<YellowstonePathology.Business.Validation.ValidationResult> validationResults = new List<Business.Validation.ValidationResult>();
			//validationResults.Add(this.m_ClientOrder.ClientOrderDetailCollection.IsDomainValid(this.m_ClientOrderDetailClone));			
			//validationResults.Add(this.m_ClientOrderDetailClone.IsDescriptionDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsCollectionDateDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsContainerIdDomainValid());			
			//validationResults.Add(this.m_ClientOrderDetailClone.IsClientFixationDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsFixationStartTimeDomainValid());
			//validationResults.Add(this.m_ClientOrderDetailClone.IsSpecialInstructionsDomainValid());

			foreach (Business.Validation.ValidationResult validationResult in validationResults)
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
