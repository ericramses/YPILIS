﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.View
{
	public class BillingSpecimenView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection m_PanelSetOrderCPTCodeCollection;
		private YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection m_ICD9BillingCodeCollection;
        private bool m_IsSelected;

		public BillingSpecimenView(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen,
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder,
			YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection,
			YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection icd9BillingCodeCollection)
		{
			this.m_SurgicalSpecimen = surgicalSpecimen;
			this.m_SpecimenOrder = specimenOrder;
			this.m_PanelSetOrderCPTCodeCollection = panelSetOrderCPTCodeCollection;
			this.m_ICD9BillingCodeCollection = icd9BillingCodeCollection;
		}

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance; }
        }

		public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen SurgicalSpecimen
		{
			get { return this.m_SurgicalSpecimen; }
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
			get { return this.m_SpecimenOrder; }
		}

		public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection PanelSetOrderCPTCodeCollection
		{
			get { return this.m_PanelSetOrderCPTCodeCollection; }
		}

		public YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection ICD9BillingCodeCollection
		{
			get { return this.m_ICD9BillingCodeCollection; }
		}

        public bool IsSelected
        {
            get { return this.m_IsSelected; }
            set
            {
                this.m_IsSelected = value;
                this.NotifyPropertyChanged(string.Empty);
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
