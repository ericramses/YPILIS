using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.MessageQueues
{
	public class CytologyScreeningAssignmentCommand : MessageCommand
	{
		string m_MasterAccessionNo;
        int m_AssignToId;

		YellowstonePathology.Business.Domain.Lock m_Lock;
        YellowstonePathology.Core.User.SystemIdentity m_SystemIdentity;
		YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		public void SetCommandData(string masterAccessionNo, int assignToId, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_SystemIdentity = new YellowstonePathology.Core.User.SystemIdentity(YellowstonePathology.Core.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
            this.MaximumRetryCount = 50;

			this.m_MasterAccessionNo = masterAccessionNo;
            this.m_AssignToId = assignToId;
			this.m_ExecutionStatus = executionStatus;

			this.Label = "Assign Cytology Screening: " + masterAccessionNo.ToString();
			this.m_Lock = new Domain.Lock(this.m_SystemIdentity);
		}

		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set { this.m_MasterAccessionNo = value; }
		}

        public int AssignToId
        {
            get { return this.m_AssignToId; }
            set { this.m_AssignToId = value; }
        }

		public override void Execute()
		{
			try
			{
                base.Execute();
				this.m_SystemIdentity = new YellowstonePathology.Core.User.SystemIdentity(YellowstonePathology.Core.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
				this.m_Lock = new Domain.Lock(this.m_SystemIdentity);
				this.m_Lock.SetLockingMode(Domain.LockModeEnum.AlwaysAttemptLock);
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(this.m_MasterAccessionNo);
				this.m_Lock.SetLockable(accessionOrder);

                if (this.m_Lock.LockAquired)
                {
                    YellowstonePathology.Business.Rules.Cytology.AssignScreening assignScreening = new YellowstonePathology.Business.Rules.Cytology.AssignScreening();
                    assignScreening.Execute(this.m_MasterAccessionNo, this.m_AssignToId, this.m_ExecutionStatus);
					this.m_Lock.ReleaseLock();
                }
                else
                {
					YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPAP();
					this.m_ExecutionStatus.AddMessage(panelSetOrder.ReportNo + " was not assigned as it is locked.", false);
                }                                
				this.HasExecuted = true;
			}
			catch (Exception e)
			{
                System.Windows.MessageBox.Show(e.Message);
				this.ErrorInExecution = true;
				this.ErrorMessage = e.Message;
				if (this.m_Lock.LockAquired == true)
				{
					this.m_Lock.ReleaseLock();
				}
			}
		}
	}
}
