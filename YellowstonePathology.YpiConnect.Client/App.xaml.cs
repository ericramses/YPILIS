using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public App()
		{
			this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(HandleException);

			if (CheckDuplicateProcess())
			{
				MessageBox.Show("A YPI Connect application is already running on this computer.\n\nThis application will now exit.",
					"Too many instances", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				App.Current.Shutdown(-1);
				return;
			}
		}

		public static void HandleException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			StringBuilder msg = new StringBuilder();
			msg.AppendLine(sender.GetType().Name);
			msg.AppendLine(e.Exception.GetType().Name);
			msg.AppendLine();
			msg.AppendLine(e.Exception.Message);
			msg.AppendLine(e.Exception.TargetSite.Name);
			msg.AppendLine(e.Exception.Source);
			msg.AppendLine();
			msg.AppendLine(e.Exception.StackTrace);

			YellowstonePathology.YpiConnect.Contract.Message message = new Contract.Message("Support@ypii.com", YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			message.ClientText = msg.ToString();
			YellowstonePathology.YpiConnect.Proxy.MessageServiceProxy messageServiceProxy = new Proxy.MessageServiceProxy();
			messageServiceProxy.Send(message);
		}

		private bool CheckDuplicateProcess()
		{
			System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
			System.Diagnostics.Process[] openProcesses = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
			foreach (System.Diagnostics.Process process in openProcesses)
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
	}
}
