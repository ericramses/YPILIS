using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StackExchange.Redis;
using System.IO;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private IDatabase m_DB;
        private ISubscriber m_SUB;

        public TestWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.m_DB = YellowstonePathology.Business.RedisConnection.Instance.GetDatabase();
            this.m_SUB = YellowstonePathology.Business.RedisConnection.Instance.GetSubscriber();
        }                

        private void Button_SubscribeClick(object sender, RoutedEventArgs e)
        {
            this.m_SUB.Subscribe("Messages", (channel, message) =>
            {
                MessageBox.Show((string)message);
            });
        }

        private void Button_PublishClick(object sender, RoutedEventArgs e)
        {
            string masterAccessionno = "16-12345";
            YellowstonePathology.Business.User.SystemUser user = YellowstonePathology.Business.User.SystemIdentity.Instance.User;
            UI.AppMessaging.AccessionLockMessage message = new AppMessaging.AccessionLockMessage(masterAccessionno, System.Environment.MachineName, user.UserName, AppMessaging.AccessionLockMessageIdEnum.ASK);
            string messageJSON = message.ToJSON();
            this.m_SUB.Publish(masterAccessionno, messageJSON);        
        }

        private void Button_GetTestClick(object sender, RoutedEventArgs e)
        {
            /*YellowstonePathology.Business.Logging.ScanLog resultLog = null;
            YellowstonePathology.Business.Logging.ScanLog scanlog = new Business.Logging.ScanLog("123", "Aliquot", "111", "Bubba Blat", "222", "Bubba TV", false);
            string sjson = scanlog.ToJSON();
            RedisResult putResult = this.m_DB.Execute("json.set", new object[] { "scanLog", ".", sjson });

            RedisResult redisResult = this.m_DB.Execute("json.get", new object[] { "scanLog" });

            if (redisResult.IsNull == false)
            {
                resultLog = scanlog.DeserializeJSON((string)redisResult);
            }*/

            YellowstonePathology.Business.Billing.Model.CptCodeCollection collection = Business.Billing.Model.CptCodeCollection.Instance;
            //YellowstonePathology.Business.Test.Model.TestCollection collection = Business.Test.Model.TestCollection.Instance;
        }
    }
}
