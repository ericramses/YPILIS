using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class MainWindowCommandButtonHandler
    {
        private readonly object m_EventLockObject = new object();

        public delegate void ShowCaseDocumentEventHandler(object sender, EventArgs e);
        public event ShowCaseDocumentEventHandler ShowCaseDocument;

        private EventHandler m_ShowOrderFormEventHandler;
		private EventHandler m_AssignCaseEventHandler;		
		private EventHandler m_ApplicationClosingEventHandler;

        public delegate void StartProviderDistributionPathEventHandler(object sender, EventArgs e);
        public event StartProviderDistributionPathEventHandler StartProviderDistributionPath;

        public delegate void ShowAmendmentDialogEventHandler(object sender, EventArgs e);
        public event ShowAmendmentDialogEventHandler ShowAmendmentDialog;

        public delegate void SaveEventHandler(object sender, EventArgs e);
        public event SaveEventHandler Save;

        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler Refresh;

        public delegate void RemoveTabEventHandler(object sender, EventArgs e);
        public event RemoveTabEventHandler RemoveTab;

        public delegate void ShowMessagingDialogEventHandler(object sender, EventArgs e);
        public event ShowMessagingDialogEventHandler ShowMessagingDialog;

        public MainWindowCommandButtonHandler()
        {
            
        }

        public void OnSave()
        {
            if (this.Save != null) this.Save(this, EventArgs.Empty);
        }

        public void OnRefresh()
        {
            if (this.Refresh != null) this.Refresh(this, EventArgs.Empty);
        }

        public void OnShowCaseDocument()
		{
            if (this.ShowCaseDocument != null) this.ShowCaseDocument(this, EventArgs.Empty);
        }

        public void OnRemoveTab()
        {
            if (this.RemoveTab != null) this.RemoveTab(this, EventArgs.Empty);
        }

        public void OnShowMessagingDialog()
        {
            if (this.ShowMessagingDialog != null) this.ShowMessagingDialog(this, EventArgs.Empty);
        }

        public void OnShowOrderForm()
		{
			if (this.m_ShowOrderFormEventHandler != null)
				this.m_ShowOrderFormEventHandler.Invoke(this, EventArgs.Empty);
		}		

		public void OnAssignCase()
		{
			if (this.m_AssignCaseEventHandler != null)
				this.m_AssignCaseEventHandler.Invoke(this, EventArgs.Empty);
		}		

		public void OnApplicationClosing()
		{
			if (this.m_ApplicationClosingEventHandler != null)
				this.m_ApplicationClosingEventHandler.Invoke(this, EventArgs.Empty);
		}

        public void OnStartProviderDistributionPath()
        {
            if (this.StartProviderDistributionPath != null) this.StartProviderDistributionPath(this, EventArgs.Empty);
        }

        public void OnShowAmendmentDialog()
        {
            if (this.ShowAmendmentDialog != null) this.ShowAmendmentDialog(this, EventArgs.Empty);
        }                

		public event EventHandler ShowOrderForm
		{
			add
			{
				lock (this.m_EventLockObject)
					this.m_ShowOrderFormEventHandler = value;
			}
			remove
			{
				lock (this.m_EventLockObject)
					this.m_ShowOrderFormEventHandler = null;
			}
		}		

		public event EventHandler AssignCase
		{
			add
			{
				lock (this.m_EventLockObject)
					this.m_AssignCaseEventHandler = value;
			}
			remove
			{
				lock (this.m_EventLockObject)
					this.m_AssignCaseEventHandler = null;
			}
		}		

		public event EventHandler ApplicationClosing
		{
			add
			{
				lock (this.m_EventLockObject)
					this.m_ApplicationClosingEventHandler = value;
			}
			remove
			{
				lock (this.m_EventLockObject)
					this.m_ApplicationClosingEventHandler = null;
			}
		}        
	}
}
