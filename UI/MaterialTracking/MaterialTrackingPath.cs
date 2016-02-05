using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.MaterialTracking
{
	public class MaterialTrackingPath
	{		
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.UI.Login.LoginPageWindow m_LoginPageWindow;
        
        private bool m_UseMasterAccessionNo = false;
        private string m_MasterAccessionNo;

        private MaterialBatchPage m_MaterialBatchPage;

		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private bool m_StartNew;

        public MaterialTrackingPath()
		{
            
		}

        public MaterialTrackingPath(string masterAccessionNo)
        {
            this.m_UseMasterAccessionNo = true;
            this.m_MasterAccessionNo = masterAccessionNo;         
        }

		public MaterialTrackingPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_StartNew = true;
		}

		public void Start()
		{
			this.m_LoginPageWindow = new YellowstonePathology.UI.Login.LoginPageWindow(this.m_SystemIdentity);
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
				this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
				this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;

				if (this.m_StartNew == false)
				{
					this.ShowMaterialTrackingStartPage();
				}
				else
				{
					this.ShowMaterialTrackingCasePage();
				}
			}
			this.m_LoginPageWindow.ShowDialog();
		}

		private void ShowScanSecurityBadgePage()
		{
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new YellowstonePathology.UI.Login.ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			this.m_LoginPageWindow.PageNavigator.Navigate(scanSecurityBadgePage);
			scanSecurityBadgePage.AuthentificationSuccessful += new YellowstonePathology.UI.Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
		}

		private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
		{
			this.m_SystemIdentity = e.SystemIdentity;
			this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;
			if (this.m_StartNew == false)
			{
				this.ShowMaterialTrackingStartPage();
			}
			else
			{
				this.ShowMaterialTrackingCasePage();
			}
		}

		private void ShowMaterialTrackingCasePage()
		{
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogCollectionByMasterAccessionNo(this.m_AccessionOrder.MasterAccessionNo);
			MaterialTrackingCasePage materialTrackingCasePage = new MaterialTrackingCasePage(this.m_AccessionOrder, materialTrackingLogCollection, this.m_SystemIdentity);
			this.m_LoginPageWindow.PageNavigator.Navigate(materialTrackingCasePage);
		}

		private void ShowMaterialTrackingStartPage()
		{
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection materialTrackingBatchCollection = null;
            MaterialTrackingStartPage materialTrackingStartPage = null;

            if (this.m_UseMasterAccessionNo == true)
            {
                materialTrackingBatchCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingBatchCollectionByMasterAccessionNo(this.m_MasterAccessionNo);
                materialTrackingStartPage = new MaterialTrackingStartPage(materialTrackingBatchCollection, this.m_MasterAccessionNo, this.m_SystemIdentity);            
            }
            else
            {
                materialTrackingBatchCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingBatchCollection();
                materialTrackingStartPage = new MaterialTrackingStartPage(materialTrackingBatchCollection, this.m_SystemIdentity);            
            }
            
            materialTrackingStartPage.ViewBatch += new MaterialTrackingStartPage.ViewBatchEventHandler(MaterialTrackingStartPage_ViewBatch);                        
            this.m_LoginPageWindow.PageNavigator.Navigate(materialTrackingStartPage);
		}        

        private void MaterialTrackingStartPage_ViewBatch(object sender, YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs e)
        {            
            this.ShowMaterialBatchPage(e.MaterialTrackingBatch, e.MaterialTrackingLogCollection);            
        }

		private void ShowMaterialBatchPage(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch,
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection)
        {
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.RefreshMaterialTrackingBatch(materialTrackingBatch);
            this.m_MaterialBatchPage = new MaterialBatchPage(materialTrackingBatch, materialTrackingLogCollection, true, true, false, this.m_UseMasterAccessionNo, this.m_MasterAccessionNo, this.m_SystemIdentity);
            this.m_MaterialBatchPage.Back += new MaterialBatchPage.BackEventHandler(MaterialBatchPage_Back);
            this.m_MaterialBatchPage.Next += new MaterialBatchPage.NextEventHandler(MaterialBatchPage_Next);
            this.m_MaterialBatchPage.ShowTrackingDocument += new MaterialBatchPage.ShowTrackingDocumentEventHandler(MaterialBatchPage_ShowTrackingDocument);
            this.m_LoginPageWindow.PageNavigator.Navigate(this.m_MaterialBatchPage);
        }

        private void MaterialBatchPage_ShowTrackingDocument(object sender, YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs e)
        {            
            MaterialTrackingBatchSummary materialTrackingBatchSummary = new MaterialTrackingBatchSummary(e.MaterialTrackingBatch, e.MaterialTrackingLogViewCollection);
            YellowstonePathology.UI.XpsDocumentViewerPage xpsDocumentViewerPage = new XpsDocumentViewerPage(System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            xpsDocumentViewerPage.Next += new XpsDocumentViewerPage.NextEventHandler(XPSDocumentViewerPage_Next);            
            this.m_LoginPageWindow.PageNavigator.Navigate(xpsDocumentViewerPage);
            xpsDocumentViewerPage.LoadDocument(materialTrackingBatchSummary.FixedDocument);            
        }

        private void XPSDocumentViewerPage_Next(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.PageNavigator.Navigate(this.m_MaterialBatchPage);
        }

        private void MaterialBatchPage_Next(object sender, EventArgs e)
        {
            this.ShowMaterialTrackingStartPage();
        }

        private void MaterialBatchPage_Back(object sender, EventArgs e)
        {
            this.ShowMaterialTrackingStartPage();
        }        

        private void Finish()
        {

        }        		       		
	}
}
