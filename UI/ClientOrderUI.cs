using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    public class ClientOrderUI
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        private DateTime m_ClientOrderDate;
        private string m_SpecimenDescriptionSearchString;

        private YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection m_OrderBrowserListItemCollection;
        private string m_ReportNo;
        private string m_SelectedItemCount;
        private object m_Writer;

        public ClientOrderUI(object writer)
        {
            this.m_Writer = writer;
            this.m_ClientOrderDate = DateTime.Today;

            this.GetClientOrderList();
        }

        public string SelectedItemCount
        {
            get { return this.m_SelectedItemCount; }
            set
            {
                if (this.m_SelectedItemCount != value)
                {
                    this.m_SelectedItemCount = value;
                    this.NotifyPropertyChanged("SelectedItemCount");
                }
            }
        }

        public YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection OrderBrowserListItemCollection
        {
            get { return this.m_OrderBrowserListItemCollection; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            set
            {
                this.m_AccessionOrder = value;
                this.NotifyPropertyChanged("AccessionOrder");
            }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            private set
            {
                this.m_ReportNo = value;
                this.NotifyPropertyChanged("ReportNo");
            }
        }

        public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
        {
            get { return this.m_CaseDocumentCollection; }
        }

        public string SpecimenDescriptionSearchString
        {
            get { return this.m_SpecimenDescriptionSearchString; }
            set
            {
                this.m_SpecimenDescriptionSearchString = value;
                NotifyPropertyChanged("SpeicmenDescriptionSearchString");
            }
        }

        public DateTime ClientOrderDate
        {
            get { return this.m_ClientOrderDate; }
            set
            {
                this.m_ClientOrderDate = value;
                NotifyPropertyChanged("ClientOrderDate");
            }
        }

        public void GetClientOrderList()
        {
            this.m_OrderBrowserListItemCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetOrderBrowserListItemsByOrderDate(this.m_ClientOrderDate);
            this.NotifyPropertyChanged("OrderBrowserListItemCollection");
        }

        public void GetClientOrderListByMasterAccessionNo(string masterAccessionNo)
        {
            this.m_OrderBrowserListItemCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetOrderBrowserListItemsByMasterAccessionNo(masterAccessionNo);
            this.NotifyPropertyChanged("OrderBrowserListItemCollection");
        }

        public void GetClientOrderListByPatientName(YellowstonePathology.Business.PatientName patientName)
        {
            this.m_OrderBrowserListItemCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetOrderBrowserListItemsByPatientName(patientName.LastName, patientName.FirstName);
            this.NotifyPropertyChanged("OrderBrowserListItemCollection");
        }

        public void GetHoldList()
        {
            this.m_OrderBrowserListItemCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetOrderBrowserListItemsByHoldStatus();
            this.NotifyPropertyChanged("OrderBrowserListItemCollection");
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
