using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
	public class AcknowledgeDailyTaskOrderPath
	{
		private YellowstonePathology.Business.Task.Model.TaskOrderCollection m_TaskOrderCollection;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		public AcknowledgeDailyTaskOrderPath(YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection)
		{
			this.m_TaskOrderCollection = taskOrderCollection;
		}        

		public void Start()
		{						
			this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
            this.AcknowledgeTasks();
		}		

        public void AcknowledgeTasks()
        {            
            foreach (YellowstonePathology.Business.Task.Model.TaskOrder taskOrder in this.m_TaskOrderCollection)
            {                
                if (taskOrder.Acknowledged == false)
                {
                    taskOrder.Acknowledged = true;
                    taskOrder.AcknowledgedDate = DateTime.Now;
                    taskOrder.AcknowledgedById = this.m_SystemIdentity.User.UserId;
                    taskOrder.AcknowledgedByInitials = this.m_SystemIdentity.User.Initials;
                }
            }
        }
	}
}
