﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.MaterialTracking
{
	public partial class MaterialTrackingStartPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges
	{		
		public delegate void FacilitySelectionEventHandler(object sender, CustomEventArgs.FacilitySelectionReturnEventArgs e);
		public event FacilitySelectionEventHandler FacilitySelection;

		public delegate void ViewBatchEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs e);
        public event ViewBatchEventHandler ViewBatch;

		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection m_MaterialTrackingBatchCollection;
        private bool m_UseMasterAccessionNo;
        private string m_MasterAccessionNo;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		public MaterialTrackingStartPage(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection materialTrackingBatchCollection,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_MaterialTrackingBatchCollection = materialTrackingBatchCollection;
            this.m_SystemIdentity = systemIdentity;

			InitializeComponent();
            this.DataContext = this;
		}

		public MaterialTrackingStartPage(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection materialTrackingBatchCollection,
			string masterAccessionNo, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_MaterialTrackingBatchCollection = materialTrackingBatchCollection;
            this.m_UseMasterAccessionNo = true;
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_SystemIdentity = systemIdentity;

            InitializeComponent();
            this.DataContext = this;
        }

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection MaterialTrackingBatchCollection
        {
            get { return this.m_MaterialTrackingBatchCollection; }
        }

		private void HyperlinkNewOutbound_Click(object sender, RoutedEventArgs e)
		{
            this.FacilitySelection(this, new CustomEventArgs.FacilitySelectionReturnEventArgs("Sent"));
		}        

        private void HyperlinkSendMaterial_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.NullFacility();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, null, fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();

			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);
            objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
        }

        private void HyperlinkReceiveMaterial_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.NullFacility();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, null, fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
			objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
        }

        private void HyperlinkSendMaterialToDrClegg_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.PamCleggOffice();

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Clegg", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
			objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
        }

        private void HyperlinkReceiveMaterialFromDrClegg_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody();
            YellowstonePathology.Business.Facility.Model.Location fromLocation =  new YellowstonePathology.Business.Facility.Model.LocationDefinitions.PamCleggOffice();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Clegg", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
			objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
        }

        private void HyperlinkSendMaterialFromCodyToBillings_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.PamCleggOffice();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Billings", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
        }

        private void HyperlinkReceiveMaterialFromBillings_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteCody();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new Business.Facility.Model.LocationDefinitions.PamCleggOffice();

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Billings", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
        }

        private void HyperlinkSendMaterialToDrKurtzman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.MontanaDepartmentofJustice();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Kurtzman", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
        }

        private void HyperlinkReceiveMaterialFromDrKurtzman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.MontanaDepartmentofJustice();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Kurtzman", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));

        }

        private void HyperlinkSendMaterialToDrShannon_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.ButtePathology();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Shannon", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));

        }

        private void HyperlinkReceiveMaterialFromDrShannon_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.ButtePathology();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Shannon", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingBatch);

                YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            objectTracker.RegisterObject(materialTrackingLogCollection);

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
        }


        private void HyperlinkNewInbound_Click(object sender, RoutedEventArgs e)
		{
			this.FacilitySelection(this, new CustomEventArgs.FacilitySelectionReturnEventArgs("Received"));
		}

        private void HyperlinkViewSelectedBatch_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxMaterialTrackingBatch.SelectedItem != null)
            {
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch)this.ListBoxMaterialTrackingBatch.SelectedItem;
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = null;

                if (this.m_UseMasterAccessionNo == true)
                {
                    materialTrackingLogCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogCollectionByBatchIdMasterAccessionNo(materialTrackingBatch.MaterialTrackingBatchId, this.m_MasterAccessionNo);
                }
                else
                {
                    materialTrackingLogCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogCollectionByBatchId(materialTrackingBatch.MaterialTrackingBatchId);
                }

				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterObject(materialTrackingBatch);
                objectTracker.RegisterObject(materialTrackingLogCollection);
                this.ViewBatch(this, new CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection, objectTracker));
            }
            else
            {
                MessageBox.Show("Please selecte a batch to view.");
            }
        }

        private void HyperlinkDeleteSelectedBatch_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxMaterialTrackingBatch.SelectedItem != null)
            {
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch)this.ListBoxMaterialTrackingBatch.SelectedItem;
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterRootDelete(materialTrackingBatch);
                objectTracker.SubmitChanges(materialTrackingBatch);
            }
            else
            {
                MessageBox.Show("Please selecte a batch to delete.");
            }
        }        
		
		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            Window.GetWindow(this).Close();			
		}                     

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

        public void Save()
        {
            
        }

		public void UpdateBindingSources()
		{

		}
    }
}
