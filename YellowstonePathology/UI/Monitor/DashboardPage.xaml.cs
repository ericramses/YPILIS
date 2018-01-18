using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Monitor
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : UserControl, INotifyPropertyChanged, IMonitorPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Monitor.Model.Dashboard m_Dashboard;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public DashboardPage()
        {
            this.m_Dashboard = new Business.Monitor.Model.Dashboard();
            InitializeComponent();
            this.DataContext = this;
        }        

        private void LoadData()
        {
            this.m_Dashboard = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullDashboard(this);            
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);

            Nullable<int> blockCount = this.m_Dashboard.GetBozemanBlockCount();
            if(blockCount.HasValue == true)
            {
                this.m_Dashboard.SetBozemanBlockCount(blockCount.Value);
                Store.RedisDB db = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.BozemanBlockCount);
                db.DataBase.StringSet(DateTime.Today.ToString("yyyyMMdd"), blockCount.Value);
            }

            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.m_Dashboard.LoadData();
        }

        public YellowstonePathology.Business.Monitor.Model.Dashboard Dashboard
        {
            get { return this.m_Dashboard; }
        }

        public DateTime DashboardDate
        {
            get { return DateTime.Now; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }
    }
}
