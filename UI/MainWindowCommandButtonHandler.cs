﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class MainWindowCommandButtonHandler
    {
        private readonly object m_EventLockObject = new object();

        private EventHandler m_SaveEventHandler;
        private EventHandler m_ToggleAccessionLockEventHandler;
		private EventHandler m_ShowCaseDocumentEventHandler;
		private EventHandler m_ShowOrderFormEventHandler;
		private EventHandler m_ShowAmendmentDialogEventHandler;
		private EventHandler m_AssignCaseEventHandler;
		private EventHandler m_RemoveTabEventHandler;
		private EventHandler m_ApplicationClosingEventHandler;

        public delegate void StartProviderDistributionPathEventHandler(object sender, EventArgs e);
        public event StartProviderDistributionPathEventHandler StartProviderDistributionPath;

        public delegate void ShowAmendmentDialogEventHandler(object sender, EventArgs e);
        public event ShowAmendmentDialogEventHandler ShowAmendmentDialog;

        public MainWindowCommandButtonHandler()
        {
            
        }

        public void OnSave()
        {
            if (this.m_SaveEventHandler != null)
                this.m_SaveEventHandler.Invoke(this, EventArgs.Empty);
        }

        public void OnToggelEventLock()
        {
            if (this.m_ToggleAccessionLockEventHandler != null)
                this.m_ToggleAccessionLockEventHandler.Invoke(this, EventArgs.Empty);
        }

		public void OnShowCaseDocument()
		{
            if (this.m_ShowCaseDocumentEventHandler != null)
                this.m_ShowCaseDocumentEventHandler.Invoke(this, EventArgs.Empty);
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

		public void OnRemoveTab()
		{
			if (this.m_RemoveTabEventHandler != null)
				this.m_RemoveTabEventHandler.Invoke(this, EventArgs.Empty);
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

        public event EventHandler Save
        {
            add
            {
                lock(this.m_EventLockObject)
                    this.m_SaveEventHandler = value;
            }
            remove
            {
                lock(this.m_EventLockObject)
                    this.m_SaveEventHandler = null;
            }
        }

        public event EventHandler ToggleAccessionLock
        {
            add
            {
                lock (this.m_EventLockObject)
                    this.m_ToggleAccessionLockEventHandler = value;
            }
            remove
            {
                lock (this.m_EventLockObject)
                    this.m_ToggleAccessionLockEventHandler = null;
            }
        }

		public event EventHandler ShowCaseDocument
		{
			add
			{
				lock (this.m_EventLockObject)
					this.m_ShowCaseDocumentEventHandler = value;
			}
			remove
			{
				lock (this.m_EventLockObject)
					this.m_ShowCaseDocumentEventHandler = null;
			}
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

		public event EventHandler RemoveTab
		{
			add
			{
				lock (this.m_EventLockObject)
					this.m_RemoveTabEventHandler = value;
			}
			remove
			{
				lock (this.m_EventLockObject)
					this.m_RemoveTabEventHandler = null;
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
