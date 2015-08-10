using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.MessageQueues
{
    [Serializable()]
	public abstract class MessageCommand
	{
        private string m_Label;
        private bool m_HasExecuted;
        private string m_ErrorMessage;
        private bool m_ErrorInExecution;
        private int m_MaximumRetryCount;
        private int m_RetryCount;
        private bool m_ResubmitAfterExecution;        
        private DateTime m_TimeMessageSent;
        private DateTime m_LastExecutionTime;   
		private Guid m_MessageCommandId;

        public MessageCommand()
        {            
            this.m_HasExecuted = false;
            this.m_ErrorInExecution = false;
            this.m_ErrorMessage = string.Empty;
            this.m_MaximumRetryCount = 20;
            this.m_RetryCount = 0;            
            this.m_TimeMessageSent = DateTime.Now;
			this.m_MessageCommandId = Guid.NewGuid();
        }

		public virtual void SetExecutionData(object[] valuesToSet)
		{

		}

        public virtual void Execute()
        {
            this.TimeMessageSent = DateTime.Now;
        }

        public Guid MessageCommandId
        {
            get { return this.m_MessageCommandId; }
            set { this.m_MessageCommandId = value; }
        }

        public string Label
        {
            get { return this.m_Label; }
            set { this.m_Label = value; }
        }

        public bool HasExecuted
        {
            get { return this.m_HasExecuted; }
            set { this.m_HasExecuted = value; }
        }

        public bool ErrorInExecution
        {
            get { return this.m_ErrorInExecution; }
            set { this.m_ErrorInExecution = value; }
        }

        public string ErrorMessage
        {
            get { return this.m_ErrorMessage; }                        
            set { this.m_ErrorMessage = value; }
        }

        public int MaximumRetryCount
        {
            get { return this.m_MaximumRetryCount; }
            set { this.m_MaximumRetryCount = value; }
        }

        public int RetryCount
        {
            get { return this.m_RetryCount; }
            set { this.m_RetryCount = value; }
        }

        public bool ResubmitAfterExecution
        {
            get { return this.m_ResubmitAfterExecution; }
            set { this.m_ResubmitAfterExecution = value; }
        }

        public DateTime TimeMessageSent
        {
            get { return this.m_TimeMessageSent; }
            set { this.m_TimeMessageSent = value; }
        }

        public DateTime LastExecutionTime
		{
			get { return this.m_LastExecutionTime; }
			set { this.m_LastExecutionTime = value; }
		}        
	}
}
