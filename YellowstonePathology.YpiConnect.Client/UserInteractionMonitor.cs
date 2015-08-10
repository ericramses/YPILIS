using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace YellowstonePathology.YpiConnect.Client
{
    public class UserInteractionMonitor
    {
        private static UserInteractionMonitor m_Instance;

		public event EventHandler TimedOut;

        private System.Windows.Threading.DispatcherTimer m_DispatcherTimer;
        private System.TimeSpan m_Timeout;
        private DateTime m_LastUserEvent;

        private System.Windows.Controls.Page m_CurrentPage;

        private UserInteractionMonitor()
        {
               
        }

        public static UserInteractionMonitor Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new UserInteractionMonitor();
                }
                return m_Instance;
            }
        }        

        public void Start()
        {
            if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableApplicationTimeout == true)
            {
                this.m_Timeout = TimeSpan.FromMinutes(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.ApplicationTimeoutMinutes);
				//this.m_Timeout = TimeSpan.FromSeconds(30);
				this.m_DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                this.m_DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
                this.m_DispatcherTimer.Interval = this.m_Timeout;
                this.m_DispatcherTimer.Start();
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (this.m_LastUserEvent != null)
            {
                TimeSpan distanceToLastEvent = DateTime.Now.Subtract(this.m_LastUserEvent);
                if (distanceToLastEvent > this.m_Timeout)
                {
                    if (this.TimedOut != null)
                    {
                        this.m_DispatcherTimer.Tick -= DispatcherTimer_Tick;
                        this.TimedOut(this, new EventArgs());
						if (this.m_DispatcherTimer != null)
						{
							this.m_DispatcherTimer.Stop();
						}
                    }
                }
            }
        }

        public void Register(System.Windows.Controls.Page page)
        {
            if (this.m_CurrentPage != null)
            {
                this.m_CurrentPage.MouseMove -= OnPageMouseMove;
                this.m_CurrentPage.KeyDown -= OnPageKeyDown;
            }

            if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableApplicationTimeout == true)
            {
                this.m_CurrentPage = page;
                this.m_CurrentPage.MouseMove += new System.Windows.Input.MouseEventHandler(OnPageMouseMove);
                this.m_CurrentPage.KeyDown += new System.Windows.Input.KeyEventHandler(OnPageKeyDown);
            }
        }

        private void OnPageKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.m_LastUserEvent = DateTime.Now;
        }        

        private void OnPageMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.m_LastUserEvent = DateTime.Now;
        }        
    }
}
