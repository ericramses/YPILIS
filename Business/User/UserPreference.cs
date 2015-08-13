﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.User
{
    [PersistentClass("tblUserPreference", "YPIDATA")]
	public class UserPreference : INotifyPropertyChanged
    {
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_HostName;
		private string m_LocationId;
		private bool m_BarcodeScanEnabled;
        private string m_BarcodeScanPort;
        private Nullable<DateTime> m_LastLocalDataUpdate;
		private string m_LockMode;
        private string m_ContainerLabelPrinter;
        private string m_HistologySlideLabelPrinter;
		private string m_CytologySlideLabelPrinter;
		private string m_RequisitionPrinter;
		private string m_CassettePrinter;		
		private string m_SpecialStainAcknowledgementPrinter;
		private string m_StartupPage;
		private string m_MolecularLabelPrinter;
        private string m_MolecularLabelFormat;
		private string m_PageScanner;
        private string m_FacilityId;
		private bool m_ActivateNotificationAlert;
        private string m_AlertWaveFileName;
        private string m_AcknowledgeTasksFor;
        private string m_TecanImportExportPath;
        private string m_SlideMatePrinterPath;
		private string m_WeekdayProcessorRunId;
		private string m_WeekendProcessorRunId;
        private string m_CytologySlidePrinter;

        public UserPreference()
        {

        }

        public bool IsFacilitySet()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.m_FacilityId) == true) result = false;
            if (string.IsNullOrEmpty(this.m_LocationId) == true) result = false;
            return result;
        }

		[PersistentPrimaryKeyProperty(false)]
		public string HostName
		{
			get { return this.m_HostName; }
			set
			{
				if (this.m_HostName != value)
				{
					this.m_HostName = value;
					this.NotifyPropertyChanged("HostName");
				}
			}
		}

		[PersistentProperty()]
		public string LocationId
		{
			get { return this.m_LocationId; }
			set
			{
				if (this.m_LocationId != value)
				{
					this.m_LocationId = value;
					this.NotifyPropertyChanged("LocationId");
				}
			}
		}

        [PersistentProperty()]
        public bool BarcodeScanEnabled
        {
            get { return this.m_BarcodeScanEnabled; }
            set
            {
                if (this.m_BarcodeScanEnabled != value)
                {
                    this.m_BarcodeScanEnabled = value;
                    this.NotifyPropertyChanged("BarcodeScanEnabled");					
				}
            }
        }

        [PersistentProperty()]
        public string BarcodeScanPort
        {
            get { return this.m_BarcodeScanPort; }
            set 
            {
                if (this.m_BarcodeScanPort != value)
                {                    
                    this.m_BarcodeScanPort = value;
                    this.NotifyPropertyChanged("BarcodeScanPort");					
				}
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> LastLocalDataUpdate
        {
            get { return this.m_LastLocalDataUpdate; }
            set
            {
                if (this.m_LastLocalDataUpdate != value)
                {
                    this.m_LastLocalDataUpdate = value;
                    this.NotifyPropertyChanged("LastLocalDataUpdate");					
				}
            }
        }

        [PersistentProperty()]
		public string LockMode
		{
			get { return this.m_LockMode ; }
			set
			{
				if (this.m_LockMode != value)
				{
					this.m_LockMode = value;
					this.NotifyPropertyChanged("LockMode");					
				}
			}
		}

        [PersistentProperty()]
        public string ContainerLabelPrinter
        {
            get { return this.m_ContainerLabelPrinter; }
            set
            {
                if (this.m_ContainerLabelPrinter != value)
                {
                    this.m_ContainerLabelPrinter = value;
                    this.NotifyPropertyChanged("ContainerLabelPrinter");					
                }
            }
        }

        [PersistentProperty()]
        public string HistologySlideLabelPrinter
        {
			get { return this.m_HistologySlideLabelPrinter; }
            set
            {
				if (this.m_HistologySlideLabelPrinter != value)
                {
					this.m_HistologySlideLabelPrinter = value;
					this.NotifyPropertyChanged("HistologySlideLabelPrinter");					
                }
            }
        }

        [PersistentProperty()]
		public string CytologySlideLabelPrinter
		{
			get { return this.m_CytologySlideLabelPrinter; }
			set
			{
				if (this.m_CytologySlideLabelPrinter != value)
				{
					this.m_CytologySlideLabelPrinter = value;
					this.NotifyPropertyChanged("CytologySlideLabelPrinter");					
				}
			}
		}

        [PersistentProperty()]
		public string RequisitionPrinter
		{
			get { return this.m_RequisitionPrinter; }
			set
			{
				if (this.m_RequisitionPrinter != value)
				{
					this.m_RequisitionPrinter = value;
					this.NotifyPropertyChanged("RequisitionPrinter");					
				}
			}
		}

        [PersistentProperty()]
		public string CassettePrinter
		{
			get { return this.m_CassettePrinter; }
			set
			{
				if (this.m_CassettePrinter != value)
				{
					this.m_CassettePrinter = value;
					this.NotifyPropertyChanged("CassettePrinter");					
				}
			}
		}

        [PersistentProperty()]
        public string SpecialStainAcknowledgementPrinter
        {
            get { return this.m_SpecialStainAcknowledgementPrinter; }
            set
            {
                if (this.m_SpecialStainAcknowledgementPrinter != value)
                {
                    this.m_SpecialStainAcknowledgementPrinter = value;
                    this.NotifyPropertyChanged("SpecialStainAcknowledgementPrinter");
                }
            }
        }

        [PersistentProperty()]
		public string StartupPage
		{
			get { return this.m_StartupPage; }
			set
			{
				if (this.m_StartupPage != value)
				{
					this.m_StartupPage = value;
					this.NotifyPropertyChanged("StartupPage");					
				}
			}
		}

        [PersistentProperty()]
		public string MolecularLabelPrinter
		{
			get { return this.m_MolecularLabelPrinter; }
			set
			{
				if (this.m_MolecularLabelPrinter != value)
				{
					this.m_MolecularLabelPrinter = value;
					this.NotifyPropertyChanged("MolecularLabelPrinter");					
				}
			}
		}

        [PersistentProperty()]
        public string MolecularLabelFormat
        {
            get { return this.m_MolecularLabelFormat; }
            set
            {
                if (this.m_MolecularLabelFormat != value)
                {
                    this.m_MolecularLabelFormat = value;
                    this.NotifyPropertyChanged("MolecularLabelFormat");
                }
            }
        }

        [PersistentProperty()]
		public string PageScanner
		{
			get { return this.m_PageScanner; }
			set
			{
				if (this.m_PageScanner != value)
				{
					this.m_PageScanner = value;
					this.NotifyPropertyChanged("PageScanner");					
				}
			}
		}

        [PersistentProperty()]
        public string FacilityId
        {
            get { return this.m_FacilityId; }
            set
            {
                if (this.m_FacilityId != value)
                {
                    this.m_FacilityId = value;
                    this.NotifyPropertyChanged("FacilityId");
                }
            }
        }

		[PersistentProperty()]
		public bool ActivateNotificationAlert
		{
			get { return this.m_ActivateNotificationAlert; }
			set
			{
				if (this.m_ActivateNotificationAlert != value)
				{
					this.m_ActivateNotificationAlert = value;
					this.NotifyPropertyChanged("ActivateNotificationAlert");
				}
			}
		}

        [PersistentProperty()]
        public string AlertWaveFileName
        {
            get { return this.m_AlertWaveFileName; }
            set
            {
                if (this.m_AlertWaveFileName != value)
                {
                    this.m_AlertWaveFileName = value;
                    this.NotifyPropertyChanged("AlertWaveFileName");
                }
            }
        }

        [PersistentProperty()]
        public string AcknowledgeTasksFor
        {
            get { return this.m_AcknowledgeTasksFor; }
            set
            {
                if (this.m_AcknowledgeTasksFor != value)
                {
                    this.m_AcknowledgeTasksFor = value;
                    this.NotifyPropertyChanged("AcknowledgeTasksFor");
                }
            }
        }

        [PersistentProperty()]
        public string TecanImportExportPath
        {
            get { return this.m_TecanImportExportPath; }
            set
            {
                if (this.m_TecanImportExportPath != value)
                {
                    this.m_TecanImportExportPath = value;
                    this.NotifyPropertyChanged("TecanImportExportPath");
                }
            }
        }

        [PersistentProperty()]
        public string SlideMatePrinterPath
        {
            get { return this.m_SlideMatePrinterPath; }
            set
            {
                if (this.m_SlideMatePrinterPath != value)
                {
                    this.m_SlideMatePrinterPath = value;
                    this.NotifyPropertyChanged("SlideMatePrinterPath");
                }
            }
        }

		[PersistentProperty()]
		public string WeekdayProcessorRunId
		{
			get { return this.m_WeekdayProcessorRunId; }
			set
			{
				if (this.m_WeekdayProcessorRunId != value)
				{
					this.m_WeekdayProcessorRunId = value;
					this.NotifyPropertyChanged("WeekdayProcessorRunId");
				}
			}
		}

		[PersistentProperty()]
		public string WeekendProcessorRunId
		{
			get { return this.m_WeekendProcessorRunId; }
			set
			{
				if (this.m_WeekendProcessorRunId != value)
				{
					this.m_WeekendProcessorRunId = value;
					this.NotifyPropertyChanged("WeekendProcessorRunId");
				}
			}
		}

        [PersistentProperty()]
        public string CytologySlidePrinter
        {
            get { return this.m_CytologySlidePrinter; }
            set
            {
                if (this.m_CytologySlidePrinter != value)
                {
                    this.m_CytologySlidePrinter = value;
                    this.NotifyPropertyChanged("CytologySlidePrinter");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }	
	}
}
