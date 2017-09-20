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
    /// Interaction logic for BlockCountMonitorPage.xaml
    /// </summary>
    public partial class BlockCountMonitorPage : UserControl, INotifyPropertyChanged, IMonitorPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Monitor.Model.BlockCount m_BlockCount;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public BlockCountMonitorPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void LoadData()
        {
            this.m_BlockCount = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetBlockCount();
            this.m_BlockCount.SetBozemanBlockCount();
            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.LoadData();
        }

        public YellowstonePathology.Business.Monitor.Model.BlockCount BlockCount
        {
            get { return this.m_BlockCount; }
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
