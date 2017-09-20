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

        private YellowstonePathology.Business.Monitor.Model.BlockCountCollection m_BlockCountCollection;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public BlockCountMonitorPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void LoadData()
        {
            YellowstonePathology.Business.Monitor.Model.BlockCountCollection blockCountCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetBlockCountCollection();
            blockCountCollection.SetState();
            this.m_BlockCountCollection = blockCountCollection;
            this.m_BlockCountCollection[0].SetBozemanBlockCount();
            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.LoadData();
        }

        public YellowstonePathology.Business.Monitor.Model.BlockCountCollection BlockCountCollection
        {
            get { return this.m_BlockCountCollection; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewBlockCount_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewBlockCount.SelectedItem != null)
            {
                /*YellowstonePathology.Business.Monitor.Model.MissingInformation missingInformation = (YellowstonePathology.Business.Monitor.Model.MissingInformation)this.ListViewMissingInformation.SelectedItem;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(missingInformation.MasterAccessionNo, Window.GetWindow(this));

                YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.Show();

                YellowstonePathology.Business.Test.MissingInformation.MissingInformationTestOrder missingInformationTestOrder = (YellowstonePathology.Business.Test.MissingInformation.MissingInformationTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(missingInformation.ReportNo);
                YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
                resultPathFactory.Start(missingInformationTestOrder, accessionOrder, this.m_LoginPageWindow.PageNavigator, System.Windows.Window.GetWindow(this), Visibility.Collapsed);
                resultPathFactory.Finished += ResultPathFactory_Finished;*/
            }
        }

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }
    }
}
