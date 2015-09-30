﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Cytology
{
	public class ThinPrepPapSlidePrintingPath
	{
        private PrintSlideDialog m_PrintSlideDialog;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public ThinPrepPapSlidePrintingPath()
        {
            
        }

        public void Start()
        {
			this.m_PrintSlideDialog = new PrintSlideDialog();
            this.ShowScanSecurityBadgePage();
			this.m_PrintSlideDialog.ShowDialog();
        }        

        private void ShowScanSecurityBadgePage()
        {
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new Login.ScanSecurityBadgePage(System.Windows.Visibility.Visible);
			this.m_PrintSlideDialog.PageNavigator.Navigate(scanSecurityBadgePage);
            scanSecurityBadgePage.AuthentificationSuccessful += new Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            scanSecurityBadgePage.Close += new Login.ScanSecurityBadgePage.CloseEventHandler(ScanSecurityBadgePage_Close);
        }

        private void ScanSecurityBadgePage_Close(object sender, EventArgs e)
        {
			this.m_PrintSlideDialog.Close();
        }

        private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
        {
            this.m_SystemIdentity = e.SystemIdentity;
            this.ShowScanContainerPage();
        }

		private void ShowScanContainerPage()
        {
            YellowstonePathology.UI.Gross.ScanContainerPage scanContainerPage = new YellowstonePathology.UI.Gross.ScanContainerPage(this.m_SystemIdentity, "Please Scan Container");
            scanContainerPage.UseThisContainer += new YellowstonePathology.UI.Gross.ScanContainerPage.UseThisContainerEventHandler(ScanContainerPage_UseThisContainer);
            scanContainerPage.PageTimedOut += new YellowstonePathology.UI.Gross.ScanContainerPage.PageTimedOutEventHandler(ScanContainerPage_PageTimedOut);
            scanContainerPage.BarcodeWontScan += new YellowstonePathology.UI.Gross.ScanContainerPage.BarcodeWontScanEventHandler(ScanContainerPage_BarcodeWontScan);
            scanContainerPage.SignOut += new YellowstonePathology.UI.Gross.ScanContainerPage.SignOutEventHandler(ScanContainerPage_SignOut);
            scanContainerPage.ScanAliquot += ScanContainerPage_ScanAliquot;

            this.m_PrintSlideDialog.PageNavigator.Navigate(scanContainerPage);
        }

        private void ScanContainerPage_ScanAliquot(object sender, EventArgs e)
        {
            YellowstonePathology.UI.Cytology.ScanAliquotPage scanAliquotPage = new ScanAliquotPage(this.m_SystemIdentity, "Scan Aliquot");
            this.m_PrintSlideDialog.PageNavigator.Navigate(scanAliquotPage);
            scanAliquotPage.UseThisAliquotOrderId += ScanAliquotPage_UseThisAliquotOrderId;
        }

        private void ScanAliquotPage_UseThisAliquotOrderId(object sender, string aliquotOrderId)
        {
            this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByAliquotOrderId(aliquotOrderId);
            this.m_ObjectTracker = new Business.Persistence.ObjectTracker();

            if (this.m_AccessionOrder == null)
            {
                System.Windows.MessageBox.Show("The scanned aliquot was not found.");
                this.ShowScanContainerPage();
            }
            else
            {
                this.m_ObjectTracker.RegisterObject(this.m_AccessionOrder);
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(aliquotOrderId);
                this.AddMaterialTrackingLog(specimenOrder);
                this.ShowThinPrepPapSlidePrintingPage(specimenOrder);
            }
        }

        private void ScanContainerPage_SignOut(object sender, EventArgs e)
        {
            this.m_SystemIdentity = null;
            this.ShowScanSecurityBadgePage();
        }

        private void ScanContainerPage_PageTimedOut(object sender, EventArgs e)
        {
            this.ShowScanSecurityBadgePage();
        }

        private void ScanContainerPage_BarcodeWontScan(object sender, EventArgs e)
        {
            //Login.ReceiveSpecimen.BarcodeManualEntryPage containerManualEntryPage = new Login.ReceiveSpecimen.BarcodeManualEntryPage();
            //containerManualEntryPage.Return += new Login.ReceiveSpecimen.BarcodeManualEntryPage.ReturnEventHandler(BarcodeManualEntryPage_Return);
            //containerManualEntryPage.Back += new Login.ReceiveSpecimen.BarcodeManualEntryPage.BackEventHandler(ContainerManualEntryPage_Back);
            //this.m_HistologyGrossDialog.PageNavigator.Navigate(containerManualEntryPage);
        }        

        private void AddMaterialTrackingLog(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
            YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, specimenOrder.SpecimenOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
                thisLocation.LocationId, thisLocation.Description, this.m_SystemIdentity.User.UserId, this.m_SystemIdentity.User.UserName, "Container Scan", "Container Scanned At Gross", "Specimen", this.m_AccessionOrder.MasterAccessionNo, specimenOrder.Description, specimenOrder.ClientAccessioned);
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingLog);
            objectTracker.SubmitChanges(materialTrackingLog);
        }

        private void ScanContainerPage_UseThisContainer(object sender, string containerId)
		{
            this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByContainerId(containerId);
            this.m_ObjectTracker = new Business.Persistence.ObjectTracker();

            if (this.m_AccessionOrder == null)
            {
                System.Windows.MessageBox.Show("The scanned container was not found.");
                this.ShowScanContainerPage();
            }
            else
            {
                this.m_ObjectTracker.RegisterObject(this.m_AccessionOrder);
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(containerId);
                this.AddMaterialTrackingLog(specimenOrder);
                this.ShowThinPrepPapSlidePrintingPage(specimenOrder);
            }         
		}

		private void ScanContainerPage_Next(object sender, EventArgs e)
		{
			this.m_PrintSlideDialog.Close();
		}

		private void ShowThinPrepPapSlidePrintingPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{
			ThinPrepPapSlidePrintingPage thinPrepPapSlidePrintingPage = new ThinPrepPapSlidePrintingPage(specimenOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			thinPrepPapSlidePrintingPage.Finished += new ThinPrepPapSlidePrintingPage.FinishedEventHandler(ThinPrepPapSlidePrintingPage_Finished);
            thinPrepPapSlidePrintingPage.PageTimedOut += PageTimedOut;
			this.m_PrintSlideDialog.PageNavigator.Navigate(thinPrepPapSlidePrintingPage);
		}

        private void ThinPrepPapSlidePrintingPage_Finished(object sender, EventArgs e)
		{
			this.ShowScanContainerPage();
		}

        private void PageTimedOut(object sender, EventArgs eventArgs)
        {
            this.ShowScanSecurityBadgePage();
        }
	}
}
