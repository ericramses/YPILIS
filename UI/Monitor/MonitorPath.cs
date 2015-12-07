using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Office.Interop.Outlook;

namespace YellowstonePathology.UI.Monitor
{
	public class MonitorPath
	{        
        private static double TimerInterval = 1000 * 20;

        private Queue<System.Windows.Controls.UserControl> m_PageQueue;
        private System.Timers.Timer m_Timer;
		private YellowstonePathology.UI.Monitor.MonitorPageWindow m_MonitorPageWindow;        

		//private Microsoft.Office.Interop.Outlook.Application m_OutlookApplication;
		//private NameSpace m_OutlookNameSpace;
		//private MAPIFolder m_OutlookInbox;
		//private Items m_OutlookItems;
		
        public MonitorPath()
		{
        	//this.m_OutlookApplication = new Microsoft.Office.Interop.Outlook.Application();
			//this.m_OutlookNameSpace = outlookApplication.GetNamespace("MAPI");
			//this.m_Inbox = this.m_OutlookNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
			
            this.m_PageQueue = new Queue<System.Windows.Controls.UserControl>();            
            this.m_MonitorPageWindow = new MonitorPageWindow();            
		}               

        public void Start()
        {                    	
            this.ShowNextPage();
            this.StartTimer();
            this.m_MonitorPageWindow.Show();
        }
        
        private void CheckForUnreadAutopsyRequest()
        {        	    	
    		//this.m_Items = this.m_Inbox.Items;
    		//this.m_Items.ItemAdd += new ItemsEvents_ItemAddEventHandler(items_ItemAdd);
        }
        
        private void items_ItemAdd(object Item)
		{
		    //string filter = "USED CARS";
		    //MailItem mail = (MailItem)Item;
		    //if (Item != null)
		    //{
		        //if (mail.MessageClass == "IPM.Note" && mail.Subject.ToUpper().Contains(filter.ToUpper()))
		        //{
		            //mail.Move(this.m_OutlookNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderJunk));
		        //}
		    //}	
		}

        public void Load(MonitorPageLoadEnum monitorPage)
        {
            switch (monitorPage)
            {
                case MonitorPageLoadEnum.CytologyScreeningMonitor:
                    CytologyScreeningMonitorPage cytologyScreeningMonitorPage = new CytologyScreeningMonitorPage();                    
                    this.m_PageQueue.Enqueue(cytologyScreeningMonitorPage);
                    break;
                case MonitorPageLoadEnum.ReportDistributionMonitor:
                    ReportDistributionMonitorPage reportDistributionMonitorPage = new ReportDistributionMonitorPage();            
                    this.m_PageQueue.Enqueue(reportDistributionMonitorPage);
                    break;
                case MonitorPageLoadEnum.PendingTestMonitor:
                    PendingTestMonitorPage pendingTestMonitorPage = new PendingTestMonitorPage();
                    this.m_PageQueue.Enqueue(pendingTestMonitorPage);
                    break;
                case MonitorPageLoadEnum.MissingInformationMonitor:
                    MissingInformationMonitorPage missingInformationMonitorPage = new MissingInformationMonitorPage();
                    this.m_PageQueue.Enqueue(missingInformationMonitorPage);
                    break;
            }
        }

        public void LoadAllPages()
        {
            CytologyScreeningMonitorPage cytologyScreeningMonitorPage = new CytologyScreeningMonitorPage();            
            this.m_PageQueue.Enqueue(cytologyScreeningMonitorPage);

            ReportDistributionMonitorPage reportDistributionMonitorPage = new ReportDistributionMonitorPage();            
            this.m_PageQueue.Enqueue(reportDistributionMonitorPage);

            PendingTestMonitorPage pendingTestMonitorPage = new PendingTestMonitorPage();
            this.m_PageQueue.Enqueue(pendingTestMonitorPage);

            MissingInformationMonitorPage missingInformationPage = new MissingInformationMonitorPage();
            this.m_PageQueue.Enqueue(missingInformationPage);
        }

        public void StartTimer()
        {
            this.m_Timer = new System.Timers.Timer();
            this.m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);            
            this.m_Timer.Interval = TimerInterval;
            this.m_Timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime timerDailyStartTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 05:00");
            DateTime timerDailyEndTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 18:00");

            this.m_MonitorPageWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        if (DateTime.Now >= timerDailyStartTime && DateTime.Now <= timerDailyEndTime)
                        {
                            this.ShowNextPage();                                                 
                        }
                        else
                        {
                            GoodNightPage goodNightPage = new GoodNightPage();
                            this.m_MonitorPageWindow.PageNavigator.Navigate(goodNightPage);
                        }

                    })); 
        }

        private void ShowNextPage()
        {
            if (this.m_PageQueue.Count > 0)
            {
                System.Windows.Controls.UserControl userControl = this.m_PageQueue.Dequeue();
                IMonitorPage monitorPage = (IMonitorPage)userControl;
                monitorPage.Refresh();
                this.m_MonitorPageWindow.PageNavigator.Navigate(userControl);
                this.m_PageQueue.Enqueue(userControl);
            }            
        }
	}
}
