using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.MessageQueues
{
	public class DailyAMScheduleCommand : MessageCommand
	{
        DateTime m_LastRunTime;

        public DailyAMScheduleCommand()
        {
            this.Label = "Daily 6:00 AM Scheduled Commands";
            this.m_LastRunTime = DateTime.Parse("1/1/2010");
            this.ResubmitAfterExecution = true;
        }

        public DateTime LastRunTime
        {
            get { return this.m_LastRunTime; }
            set { this.m_LastRunTime = value; }
        }

		public override void Execute()
		{            
			try
			{
                base.Execute();
                DateTime nextRunTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 06:00");
                if (DateTime.Now > nextRunTime)
                {
                    if (this.m_LastRunTime < nextRunTime)
                    {                        
                        YellowstonePathology.Business.MessageQueues.HistologyMessageQueue histologyMessageQueue = new HistologyMessageQueue();
                        YellowstonePathology.Business.MessageQueues.CytologySlideDisposalCommand cytologySlideDisposalCommand = new CytologySlideDisposalCommand();
                        cytologySlideDisposalCommand.SetCommandData(DateTime.Today);
                        histologyMessageQueue.SendCommand(cytologySlideDisposalCommand);

						YellowstonePathology.Business.MessageQueues.SurgicalSpecimenDisposalCommand surgicalSpecimenDisposalCommand = new SurgicalSpecimenDisposalCommand();
						surgicalSpecimenDisposalCommand.SetCommandData(DateTime.Today);
						histologyMessageQueue.SendCommand(surgicalSpecimenDisposalCommand);                        

                        this.m_LastRunTime = DateTime.Now;
                    }                    
                }
			}
			catch (Exception e)
			{
				this.ErrorInExecution = true;
				this.ErrorMessage = e.Message;
			}
		}
	}
}
