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
using System.Diagnostics;
//using LibGit2Sharp;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
		System.Timers.Timer m_Timer;
        bool SetLoc;

		public App()
		{            
			if (CheckDuplicateProcess())
			{

				MessageBox.Show("A LIS application is already running on this computer.\n\nThis application will now exit.",
					"Too many instances", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				App.Current.Shutdown(-1);
				return;
			}

            this.Exit += App_Exit;
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(YellowstonePathology.Business.Logging.EmailExceptionHandler.HandleException);
		}

        private void App_Exit(object sender, ExitEventArgs e)
        {
            Business.Test.AccessionLockCollection accessionLockCollection = new Business.Test.AccessionLockCollection();
            accessionLockCollection.ClearLocks();
            if (this.SetLoc == true)
            {
                System.Windows.Forms.Application.Restart();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.SetupJsonFile();
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ypilis.json";
            if (File.Exists(path) == false)
            {
                this.SetLoc = true;
                this.StartupUri = new System.Uri(@"UI\Common\UserPreferencesList.xaml", System.UriKind.Relative);
                this.StartTimer();
                base.OnStartup(e);
            }
            else
            {
                this.SetLoc = false;
                Store.AppDataStore.Instance.LoadData();

                Business.Test.AccessionLockCollection accessionLockCollection = new Business.Test.AccessionLockCollection();
                //accessionLockCollection.ClearLocks();

                string startUpWindow = string.Empty;

                if (System.Environment.MachineName.ToUpper() == "CUTTINGA" || System.Environment.MachineName.ToUpper() == "CUTTINGB")// || System.Environment.MachineName.ToUpper() == "COMPILE")
                {
                    YellowstonePathology.UI.Cutting.CuttingStationPath cuttingStationPath = new Cutting.CuttingStationPath();
                    cuttingStationPath.Start();
                }
                else if (System.Environment.MachineName.ToUpper() == "CYTOLOG2") // || System.Environment.MachineName.ToUpper() == "COMPILE")
                {
                    YellowstonePathology.UI.Cytology.ThinPrepPapSlidePrintingPath thinPrepPapSlidePrintingPath = new Cytology.ThinPrepPapSlidePrintingPath();
                    thinPrepPapSlidePrintingPath.Start();
                }
                else
                {
                    startUpWindow = @"UI\MainWindow.xaml";
                    this.StartupUri = new System.Uri(startUpWindow, System.UriKind.Relative);
                }

                EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(TextBox_GotFocus));
                base.OnStartup(e);


                this.StartTimer();
                this.SetupApplicationFolders();
            }
        }        

        public static bool HandledictionarySetup()
        {            
            try
            {
                System.IO.File.Copy(YellowstonePathology.Properties.Settings.Default.ServerDICFile, YellowstonePathology.Properties.Settings.Default.LocalDICFile, true);
                System.IO.File.Copy(YellowstonePathology.Properties.Settings.Default.ServerAFFFile, YellowstonePathology.Properties.Settings.Default.LocalAFFFile, true);
                return true;
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }            
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
            List<string> appDirectories = new List<string>();            
            appDirectories.Add(@"%USERPROFILE%\AppData\Local\ypi\");
            appDirectories.Add(YellowstonePathology.Properties.Settings.Default.MonitoredPropertyFolder);

            foreach (string appDir in appDirectories)
            {
                if (System.IO.Directory.Exists(Environment.ExpandEnvironmentVariables(appDir)) == false)
                {
                    try
                    {                        
                        System.IO.Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(appDir));
                    }
                    catch (Exception e)
                    {
                        Business.Logging.EmailExceptionHandler.HandleException(e.Message);
                    }                    
                }
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
        
        private void PullLisData()
        {
            string cmd = Environment.GetEnvironmentVariable("GitCmdPath");
            if (string.IsNullOrEmpty(cmd) == false)
            {
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo(cmd, "PullLisData.bat");
                info.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo = info;
                p.Start();
            }
        }

        private void SetupJsonFile()
        {
            string location = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetUserPreferenceLocation(Environment.MachineName);
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ypilis.json";
            File.WriteAllText(path, "{'location': '" + location + "'}");
        }
    }
}
