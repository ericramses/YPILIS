using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class ThinPrepPapSpecimenPath
	{
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		
		private YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder m_ClientOrder;
        private YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder m_ClientOrderClone;

		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;        
		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetailClone;

		private ClientOrderDetailWizardModeEnum m_ClientOrderDetailWizardMode;
		private ScanContainerPage m_ScanContainerPage;

		public ThinPrepPapSpecimenPath(YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder clientOrder, YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetail, ClientOrderDetailWizardModeEnum clientOrderDetailWizardMode)
		{			
			this.m_ClientOrder = clientOrder;
			this.m_ClientOrderDetail = clientOrderDetail;			
			this.m_ClientOrderDetailWizardMode = clientOrderDetailWizardMode;
			if (this.m_ClientOrderDetailWizardMode == ClientOrderDetailWizardModeEnum.AddNew)
			{
				this.m_ClientOrderDetail.Description = "Thin Prep Fluid";
				this.m_ClientOrderDetail.OrderType = "Thin Prep Pap";
			}
			YellowstonePathology.Persistence.ObjectCloner objectCloner = new Persistence.ObjectCloner();
			this.m_ClientOrderDetailClone = (YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail)objectCloner.Clone(this.m_ClientOrderDetail);
			this.m_ClientOrderClone = (YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder)objectCloner.Clone(this.m_ClientOrder);
		}

		public void Start()
		{
			this.ShowCytologyTestOrderPage();
		}		

		private void ShowCytologyTestOrderPage()
		{
			CytologyTestOrderPage cytologyTestOrderPage = new CytologyTestOrderPage(this.m_ClientOrderClone);
			cytologyTestOrderPage.Return += new CytologyTestOrderPage.ReturnEventHandler(CytologyTestOrderPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(cytologyTestOrderPage);
		}

		private void CytologyTestOrderPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowCytologyIcd9CodingPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					if (this.UpdateClientOrderGoingBack() == true)
					{
						this.ShowOrderEntryPage(e);
					}
					break;
			}
		}

		private void ShowCytologyIcd9CodingPage()
		{
			CytologyScreeningTypePage cytologyScreeningTypePage = new CytologyScreeningTypePage(this.m_ClientOrderClone);
            cytologyScreeningTypePage.Return += new CytologyScreeningTypePage.ReturnEventHandler(CytologyScreeningTypePage_Return);
            ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(cytologyScreeningTypePage);
		}

        private void CytologyScreeningTypePage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
            CytologyScreeningTypePageEventArgs cytologyScreeningTypePageEventArgs = (CytologyScreeningTypePageEventArgs)e;
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					if (cytologyScreeningTypePageEventArgs.ManualEntryOfIcd9CodeRequired == true)
					{
						this.ShowCytologyIcd9EntryPage();
					}
					else
					{
						this.ShowCytologyClinicalHistoryPage();
					}
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					this.ShowCytologyTestOrderPage();
					break;
			}
		}

		private void ShowCytologyClinicalHistoryPage()
		{
			CytologyClinicalHistoryPage cytologyClinicalHistoryPage = new CytologyClinicalHistoryPage(this.m_ClientOrderClone);
			cytologyClinicalHistoryPage.Return += new CytologyClinicalHistoryPage.ReturnEventHandler(CytologyClinicalHistoryPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(cytologyClinicalHistoryPage);
		}

		private void CytologyClinicalHistoryPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowScanContainerPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					if (this.m_ClientOrderClone.ScreeningType == "Diagnostic Pap")
					{
						this.ShowCytologyIcd9EntryPage();
					}
					else
					{
						this.ShowCytologyIcd9CodingPage();
					}
					break;
			}
		}

		private void ShowCytologyIcd9EntryPage()
		{
			CytologyIcd9EntryPage cytologyIcd9EntryPage = new CytologyIcd9EntryPage(this.m_ClientOrderClone);
			cytologyIcd9EntryPage.Return += new CytologyIcd9EntryPage.ReturnEventHandler(CytologyIcd9EntryPage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(cytologyIcd9EntryPage);
		}

		private void CytologyIcd9EntryPage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowCytologyClinicalHistoryPage();
					break;
				case Shared.PageNavigationDirectionEnum.Back:
					this.ShowCytologyIcd9CodingPage();
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
					ShowCytologyClinicalHistoryPage();
					break;
				case Shared.PageNavigationDirectionEnum.Next:
					this.ShowCytologySpecimenSourcePage();
					break;
			}
		}

		private void ShowCytologySpecimenSourcePage()
		{
			CytologySpecimenSourcePage cytologySpecimenSourcePage = new CytologySpecimenSourcePage(this.m_ClientOrderDetailClone);
			cytologySpecimenSourcePage.Return += new CytologySpecimenSourcePage.ReturnEventHandler(CytologySpecimenSourcePage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(cytologySpecimenSourcePage);
		}

		private void CytologySpecimenSourcePage_Return(object sender, Shared.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case Shared.PageNavigationDirectionEnum.Back:
					this.ShowScanContainerPage();
					break;
				case Shared.PageNavigationDirectionEnum.Next:
					if (this.UpdateClientOrderGoingForward() == true)
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

		private bool UpdateClientOrderGoingBack()
		{
			bool result = true;

			List<YellowstonePathology.Shared.ValidationResult> validationResults = new List<Shared.ValidationResult>();
			validationResults.Add(this.m_ClientOrder.ClientOrderDetailCollection.IsDomainValid(this.m_ClientOrderDetailClone));
			validationResults.Add(this.m_ClientOrderClone.IsAbnormalBleedingDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsBirthControlDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsCervixPresentDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsHormoneTherapyDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsHysterectomyDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsIcd9CodeDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsLMPDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsNGCTTestingDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPostmenopausalDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPostpartumDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPrenatalDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPreviousAbnormalPapDateDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPreviousAbnormalPapDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPreviousBiopsyDateDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPreviousBiopsyDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPreviousNormalPapDateDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsPreviousNormalPapDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsReflexHPVDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsRoutineHPVTestingDomainValid());
			validationResults.Add(this.m_ClientOrderClone.IsScreeningTypeDomainValid());

			validationResults.Add(this.m_ClientOrderDetailClone.IsDescriptionDomainValid());
			validationResults.Add(this.m_ClientOrderDetailClone.IsCollectionDateDomainValid());
			validationResults.Add(this.m_ClientOrderDetailClone.IsContainerIdDomainValid());

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
				this.m_ClientOrder.Join(this.m_ClientOrderClone);
				this.m_ClientOrderDetail.Join(this.m_ClientOrderDetailClone);				
				if (this.m_ClientOrderDetailWizardMode == ClientOrderDetailWizardModeEnum.AddNew)
				{
					this.m_ClientOrder.ClientOrderDetailCollection.Add(this.m_ClientOrderDetail);
				}
			}
			return result;
		}

		private bool UpdateClientOrderGoingForward()
		{
			bool result = true;
			this.m_ClientOrder.Join(this.m_ClientOrderClone);
			this.m_ClientOrderDetail.Join(this.m_ClientOrderDetailClone);			
			if (this.m_ClientOrderDetailWizardMode == ClientOrderDetailWizardModeEnum.AddNew)
			{
				this.m_ClientOrder.ClientOrderDetailCollection.Add(this.m_ClientOrderDetail);
			}
			return result;
		}
	}
}
