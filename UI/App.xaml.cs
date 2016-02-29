using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Win32;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
		System.Timers.Timer m_Timer;

		public App()
		{            
			if (CheckDuplicateProcess())
			{

				MessageBox.Show("A LIS application is already running on this computer.\n\nThis application will now exit.",
					"Too many instances", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				App.Current.Shutdown(-1);
				return;
			}

            UI.AppMessaging.MessageQueues.Instance.CreateMessageQueuesIfNotExist();
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(YellowstonePathology.Business.Logging.EmailExceptionHandler.HandleException);
		}     
               

        protected override void OnStartup(StartupEventArgs e)
        {
            this.ReleaseLocksOnStartup();
            string startUpWindow = string.Empty;

			if (System.Environment.MachineName.ToUpper() == "CUTTINGA" || System.Environment.MachineName.ToUpper() == "CUTTINGB")// || System.Environment.MachineName.ToUpper() == "COMPILE")
            {                
                YellowstonePathology.UI.Cutting.CuttingStationPath cuttingStationPath = new Cutting.CuttingStationPath();
                cuttingStationPath.Start();
            }                        
            else if (System.Environment.MachineName.ToUpper() == "CYTOLOG2")// || System.Environment.MachineName.ToUpper() == "SIDHARDERWIN8")
            {
				YellowstonePathology.UI.Cytology.ThinPrepPapSlidePrintingPath thinPrepPapSlidePrintingPath = new Cytology.ThinPrepPapSlidePrintingPath();
				thinPrepPapSlidePrintingPath.Start();
			}             
			else
			{
				startUpWindow = "MainWindow.xaml";
				this.StartupUri = new System.Uri(startUpWindow, System.UriKind.Relative);
			}                        

			this.StartTimer();            
		}

        private void ReleaseLocksOnStartup()
        {
            Business.Domain.LockItemCollection lockItemCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLockedAccessionOrders();
            foreach(Business.Domain.LockItem lockItem in lockItemCollection)
            {
                if(lockItem.ComputerName == Environment.MachineName)
                {
                    Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(lockItem.KeyString, this);                    
                }
            }
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

		protected override void OnExit(ExitEventArgs e)
		{
			this.m_Timer.Stop();
			this.m_Timer.Dispose();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Flush();
            base.OnExit(e);
		}

        private void SetupApplicationFolders()
        {            
            if (Directory.Exists(YellowstonePathology.UI.Properties.Settings.Default.LocalApplicationFolder) == false)
            {
                Directory.CreateDirectory(YellowstonePathology.UI.Properties.Settings.Default.LocalApplicationFolder);
            }
            if (Directory.Exists(YellowstonePathology.UI.Properties.Settings.Default.LocalDictationFolder) == false)
            {
                Directory.CreateDirectory(YellowstonePathology.UI.Properties.Settings.Default.LocalDictationFolder);
            }
            if (Directory.Exists(YellowstonePathology.UI.Properties.Settings.Default.LocalDoneDictationFolder) == false)
            {
                Directory.CreateDirectory(YellowstonePathology.UI.Properties.Settings.Default.LocalDoneDictationFolder);
            }
            if (Directory.Exists(YellowstonePathology.UI.Properties.Settings.Default.LocalLoginDataFolder) == false)
            {
                Directory.CreateDirectory(YellowstonePathology.UI.Properties.Settings.Default.LocalLoginDataFolder);
            }            
        }                      

		private bool CheckDuplicateProcess()
		{            
			System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
			System.Diagnostics.Process[] openProcesses = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
			foreach(System.Diagnostics.Process process in openProcesses)
			{
				if ((currentProcess.SessionId == 0 && currentProcess.Id != process.Id) ||
					currentProcess.SessionId != 0 && currentProcess.SessionId == process.SessionId && currentProcess.Id != process.Id)
				{
					if (currentProcess.StartInfo.UserName == process.StartInfo.UserName)
					{
						return true;
					}
				}
			}         
			return false;
		}                    

		private void StartTimer()
		{
			this.m_Timer = new System.Timers.Timer();
			this.m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
						
            TimeSpan timeToNextEvent = new TimeSpan(0, 15, 0);
			this.m_Timer.Interval = timeToNextEvent.TotalMilliseconds;
			this.m_Timer.Enabled = true;
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
		}               
    }
}
