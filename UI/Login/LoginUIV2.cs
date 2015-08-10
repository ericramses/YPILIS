using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class LoginUIV2 : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
				
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemUserCollection m_LogUsers;
		private YellowstonePathology.Business.Task.Model.TaskOrderCollection m_TaskOrderCollection;
		private YellowstonePathology.Business.Task.Model.TaskOrderCollection m_DailyTaskOrderCollection;

		private DateTime m_AccessionOrderDate;
        private DateTime m_ClientOrderDate;		
        private string m_SpecimenDescriptionSearchString;

        private List<string> m_CaseTypeList;
		private List<string> m_TaskAcknowledgementTypeList;

		private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
        private string m_CurrentCaseType;
        private YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection m_OrderBrowserListItemCollection;
		private string m_ReportNo;

		public LoginUIV2()
		{			
			this.m_LogUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Log, true);
            this.m_CaseTypeList = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetCaseTypes();
			this.m_TaskAcknowledgementTypeList = YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.GetAll();
			this.m_AccessionOrderDate = DateTime.Today;
            this.m_ClientOrderDate = DateTime.Today;			

            this.GetClientOrderList();

            YellowstonePathology.UI.TaskNotifier.Instance.Notifier.Alert += new TaskNotifier.AlertEventHandler(Notifier_Alert);
		}

        private void Notifier_Alert(object sender, CustomEventArgs.TaskOrderCollectionReturnEventArgs e)
        {            
            this.m_TaskOrderCollection = e.TaskOrderCollection;
            this.NotifyPropertyChanged("TaskOrderCollection");
            YellowstonePathology.UI.TaskNotifier.Instance.Notifier.Alert -= Notifier_Alert;
        }        

        public YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection OrderBrowserListItemCollection
        {
            get { return this.m_OrderBrowserListItemCollection; }
        }

        public string CurrentCaseType
        {
            get { return this.m_CurrentCaseType; }
            set { this.m_CurrentCaseType = value; }
        }

        public List<string> CaseTypeList
        {
            get { return this.m_CaseTypeList; }
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

		public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
		{
			get { return this.m_ReportSearchList; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection LogUsers
		{
			get { return this.m_LogUsers; }
		}

		public YellowstonePathology.Business.Task.Model.TaskOrderCollection TaskOrderCollection
		{
			get { return this.m_TaskOrderCollection; }
		}

		public YellowstonePathology.Business.Task.Model.TaskOrderCollection DailyTaskOrderCollection
		{
			get { return this.m_DailyTaskOrderCollection; }
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

		public DateTime AccessionOrderDate
		{
			get { return this.m_AccessionOrderDate; }
			set
			{
				this.m_AccessionOrderDate = value;
				NotifyPropertyChanged("AccessionOrderDate");
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

		public List<string> TaskAcknowledgementTypeList
		{
			get { return this.m_TaskAcknowledgementTypeList; }
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

		public void GetReportSearchList()
		{
            List<int> panelSetIdList = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetPanelSetIdList(this.m_CurrentCaseType);

			panelSetIdList.Add(131);

            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByAccessionDate(this.m_AccessionOrderDate, panelSetIdList);
			this.NotifyPropertyChanged("ReportSearchList");
		}

        public void GetReportSearchListByReportNo(string reportNo)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByReportNo(reportNo);
            this.NotifyPropertyChanged("ReportSearchList");
        }        

        public void GetReportSearchListBySpecimenKeyword(string specimenDesription, DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListBySpecimenKeyword(specimenDesription, startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPanelSetFinalDate(DateTime panelSetFinalDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPanelSetFinalDate(panelSetFinalDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByNotPosted()
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByNotPosted();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPostDate(DateTime postDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPostDate(postDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPositiveHPylori(DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPositiveHPylori(startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByAutopsies()
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByAutopsies();
            this.NotifyPropertyChanged("ReportSearchList");
        }

		public void GetReportSearchListByMasterAccessionNo(string masterAccessionNo)
		{
			this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByMasterAccessionNo(masterAccessionNo);
			this.NotifyPropertyChanged("ReportSearchList");
		}

        public void GetReportSearchListByITAudit(YellowstonePathology.Business.Test.ITAuditPriorityEnum itAuditPriority)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByITAudit(itAuditPriority);
            this.NotifyPropertyChanged("ReportSearchList");
        }

		public void GetReportSearchListByPatientName(YellowstonePathology.Business.PatientName patientName)
		{
			this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPatientName(new List<object>(){patientName.LastName, patientName.FirstName});
			this.NotifyPropertyChanged("ReportSearchList");
		}

		public void GetAccessionOrder(string masterAccessionNo, string reportNo)
		{
			this.AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);            
			this.ReportNo = reportNo;
            this.m_CaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(this.AccessionOrder, reportNo);            
		}

		public bool GetAccessionOrderByContainerId(string containerId)
		{
			bool result = false;
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByContainerId(containerId);

			if (this.m_AccessionOrder != null)
			{
				if (this.m_AccessionOrder.PanelSetOrderCollection.Count != 0)
				{
					result = true;
					string reportNo = this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo;
					this.GetReportSearchListByReportNo(reportNo);
					this.NotifyPropertyChanged("ReportSearchList");
				}
			}

			return result;
		}

		public void GetAccessionOrderBySlideOrderId(string slideOrderId)
		{
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderBySlideOrderId(slideOrderId);
			if (this.m_AccessionOrder != null)
			{
				string reportNo = this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo;
				this.GetReportSearchListByReportNo(reportNo);
				this.NotifyPropertyChanged("ReportSearchList");
			}
		}

		public void GetTaskOrderCollection()
		{
			this.m_TaskOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetTaskOrderCollection(YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.Immediate);
			this.NotifyPropertyChanged("TaskOrderCollection");
		}

		public void GetTasksNotAcknowledged()
        {
            string assignedTo = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.AcknowledgeTasksFor;
			this.m_TaskOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetTasksNotAcknowledged(assignedTo, YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.Immediate);
            this.NotifyPropertyChanged("TaskOrderCollection");
        }

		public void GetDailyTaskOrderCollection()
		{
			this.m_DailyTaskOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetDailyTaskOrderCollection();
			this.NotifyPropertyChanged("DailyTaskOrderCollection");
		}

		public void ViewLabOrderLog(DateTime orderDate)
		{
			string rptpath = @"\\CFileServer\documents\Reports\Lab\LabOrdersLog\YEAR\MONTH\LabOrdersLog.FILEDATE.v1.xml";

			string rptName = rptpath.Replace("YEAR", orderDate.ToString("yyyy"));
			rptName = rptName.Replace("MONTH", orderDate.ToString("MMMM"));
			rptName = rptName.Replace("FILEDATE", orderDate.ToString("MM.dd.yy"));

			YellowstonePathology.Business.Reports.LabOrdersLog labOrdersLog = new YellowstonePathology.Business.Reports.LabOrdersLog();
			labOrdersLog.CreateReport(orderDate);

			string holdRptName = string.Empty;
			do
			{
				holdRptName = rptName;
				int vLocation = rptName.IndexOf(".v");
				int originalVersion = Convert.ToInt32(rptName.Substring(vLocation + 2, 1));
				int newVersion = originalVersion + 1;
				rptName = rptName.Replace(".v" + originalVersion.ToString(), ".v" + newVersion.ToString());
			} while (System.IO.File.Exists(rptName));

			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(holdRptName);
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
