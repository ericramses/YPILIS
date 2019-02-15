using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI.Billing
{
	public class AutomationListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private string m_ReportNo;
		private Nullable<DateTime> m_PostDate;
		private int m_ClientId;
		private string m_ClientName;        
		private string m_PanelSetName;
        private string m_PatientType;
        private string m_PrimaryInsuranceManual;
        private string m_PrimaryInsuranceADT;
        private string m_PrimaryInsuranceMapped;
        private string m_MedicalRecord;

        public AutomationListItem()
		{

		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        

        [PersistentProperty()]
        public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if (value != this.m_ReportNo)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

        [PersistentProperty()]
        public Nullable<DateTime> PostDate
		{
			get { return this.m_PostDate; }
			set
			{
				if (value != this.m_PostDate)
				{
					this.m_PostDate = value;
					this.NotifyPropertyChanged("PostDate");
				}
			}
		}

        [PersistentProperty()]
        public int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if (value != this.m_ClientId)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
				}
			}
		}        

        [PersistentProperty()]
        public string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if (value != this.m_ClientName)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
				}
			}
		}
        
        [PersistentProperty()]
        public string PanelSetName
		{
			get { return this.m_PanelSetName; }
			set
			{
				if (value != this.m_PanelSetName)
				{
					this.m_PanelSetName = value;
					this.NotifyPropertyChanged("PanelSetName");
				}
			}
		}

        [PersistentProperty()]
        public string PatientType
        {
            get { return this.m_PatientType; }
            set
            {
                if (value != this.m_PatientType)
                {
                    this.m_PatientType = value;
                    this.NotifyPropertyChanged("PatientType");
                }
            }
        }

        [PersistentProperty()]
        public string PrimaryInsuranceManual
        {
            get { return this.m_PrimaryInsuranceManual; }
            set
            {
                if (value != this.m_PrimaryInsuranceManual)
                {
                    this.m_PrimaryInsuranceManual = value;
                    this.NotifyPropertyChanged("PrimaryInsuranceManual");
                }
            }
        }

        [PersistentProperty()]
        public string PrimaryInsuranceADT
        {
            get { return this.m_PrimaryInsuranceADT; }
            set
            {
                if (value != this.m_PrimaryInsuranceADT)
                {
                    this.m_PrimaryInsuranceADT = value;
                    this.NotifyPropertyChanged("PrimaryInsuranceADT");
                }
            }
        }

        [PersistentProperty()]
        public string PrimaryInsuranceMapped
        {
            get { return this.m_PrimaryInsuranceMapped; }
            set
            {
                if (value != this.m_PrimaryInsuranceMapped)
                {
                    this.m_PrimaryInsuranceMapped = value;
                    this.NotifyPropertyChanged("PrimaryInsuranceMapped");
                }
            }
        }

        [PersistentProperty()]
        public string MedicalRecord
        {
            get { return this.m_MedicalRecord; }
            set
            {
                if (value != this.m_MedicalRecord)
                {
                    this.m_MedicalRecord = value;
                    this.NotifyPropertyChanged("MedicalRecord");
                }
            }
        }
    }
}
